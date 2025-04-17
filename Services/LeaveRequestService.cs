using AutoMapper;
using LeaveManagement.Data;
using LeaveManagement.DTOs;
using LeaveManagement.Enums;
using LeaveManagement.Models;
using LeaveManagement.Services;
using Microsoft.EntityFrameworkCore;

public class LeaveRequestService : ILeaveRequestService
{
    private readonly LeaveRequestRepository _repository;
    private readonly IMapper _mapper;

    public LeaveRequestService(LeaveRequestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LeaveRequestDto>> GetAllLeaveRequestsAsync()
    {
        var requests = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<LeaveRequestDto>>(requests);
    }

    public async Task<LeaveRequestDto?> GetLeaveRequestByIdAsync(int id)
    {
        var request = await _repository.GetByIdAsync(id);
        return request == null ? null : _mapper.Map<LeaveRequestDto>(request);
    }

    public async Task<LeaveRequestDto> CreateLeaveRequestAsync(LeaveRequestDto dto)
    {
        if (await _repository.HasOverlappingLeave(dto.EmployeeId, dto.StartDate, dto.EndDate))
            throw new InvalidOperationException("Leave dates overlap with an existing request.");

        if (dto.LeaveType == "Annual")
        {
            var year = dto.StartDate.Year;
            var requestedDays = (dto.EndDate - dto.StartDate).Days + 1;
            var totalDays = await _repository.GetTotalAnnualLeaveDays(dto.EmployeeId, year);
            if (totalDays + requestedDays > 20)
                throw new InvalidOperationException("Exceeded maximum 20 annual leave days for the year.");
        }
        if(string.IsNullOrWhiteSpace(dto.Status))
        { dto.Status = "Pending"; }

        if (dto.LeaveType == "Sick" && string.IsNullOrWhiteSpace(dto.Reason))
            throw new InvalidOperationException("Sick leave requires a reason.");

        var entity = _mapper.Map<LeaveRequest>(dto);
        entity.CreatedAt = DateTime.UtcNow;

        await _repository.AddAsync(entity);

        return _mapper.Map<LeaveRequestDto>(entity);
    }

    public async Task<bool> UpdateLeaveRequestAsync(int id, LeaveRequestDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return false;

        _mapper.Map(dto, entity);
        await _repository.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> DeleteLeaveRequestAsync(int id)
    {
       

        await _repository.DeleteAsync(id);
        return true;
    }

    public async Task<bool> ApproveLeaveRequestAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null || entity.Status != LeaveStatus.Pending)
            return false;

        entity.Status = LeaveStatus.Approved;
        await _repository.UpdateAsync(entity);

        return true;
    }

    public async Task<PaginatedResult<LeaveRequestDto>> FilterLeaveRequestsAsync(
        int? employeeId, LeaveType? leaveType, LeaveStatus? status,
        DateTime? startDate, DateTime? endDate, string? keyword,
        int page, int pageSize, string sortBy, string sortOrder)
    {
        var query = _repository.Query();
        query = _repository.Filter(query, employeeId, leaveType, status, startDate, endDate, keyword);
        query = _repository.Sort(query, sortBy, sortOrder);

        var totalItems = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedResult<LeaveRequestDto>
        {
            Items = _mapper.Map<List<LeaveRequestDto>>(items),
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<IEnumerable<LeaveSummaryDto>> GetLeaveSummaryReportAsync(
        int year, string? department, DateTime? startDate, DateTime? endDate)
    {
        var query = _repository.Query().Where(lr => lr.StartDate.Year == year);

        if (!string.IsNullOrEmpty(department))
            query = query.Where(lr => lr.Employee.Department == department);
        if (startDate.HasValue)
            query = query.Where(lr => lr.StartDate >= startDate.Value);
        if (endDate.HasValue)
            query = query.Where(lr => lr.EndDate <= endDate.Value);

        var grouped = _repository.GroupByEmployee(query);
        var summaryQuery = _repository.GenerateLeaveSummaryReport(grouped);

        return await summaryQuery.ToListAsync();
    }
}
