using System.Linq.Expressions;
using AutoMapper;
using LeaveManagement.Data;
using LeaveManagement.DTOs;
using LeaveManagement.Enums;
using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LeaveRequestService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LeaveRequestDto>> GetAllLeaveRequestsAsync()
        {
            var requests = await _context.LeaveRequests.ToListAsync();
            return _mapper.Map<IEnumerable<LeaveRequestDto>>(requests);
        }

        public async Task<LeaveRequestDto> GetLeaveRequestByIdAsync(int id)
        {
            var request = await _context.LeaveRequests.FindAsync(id);
            return request == null ? null : _mapper.Map<LeaveRequestDto>(request);
        }

        public async Task<LeaveRequestDto> CreateLeaveRequestAsync(LeaveRequestDto dto)
        {
            // Business Rule 1: No overlapping leave dates per employee
            bool overlaps = await _context.LeaveRequests.AnyAsync(lr =>
                lr.EmployeeId == dto.EmployeeId &&
                lr.StartDate < dto.EndDate &&
                dto.StartDate < lr.EndDate);
            if (overlaps)
                throw new InvalidOperationException("Employee already has a leave request that overlaps with the given dates.");

            // Business Rule 2: Max 20 annual leave days per year
            if (dto.LeaveType == "Annual")
            {
                var year = dto.StartDate.Year;
                var annualLeaves = await _context.LeaveRequests
                    .Where(lr => lr.EmployeeId == dto.EmployeeId && lr.LeaveType == LeaveType.Annual && lr.StartDate.Year == year)
                    .ToListAsync();

                int totalDays = annualLeaves.Sum(lr => (lr.EndDate - lr.StartDate).Days + 1);
                int requestedDays = (dto.EndDate - dto.StartDate).Days + 1;

                if (totalDays + requestedDays > 20)
                    throw new InvalidOperationException("Exceeded maximum 20 annual leave days for the year.");
            }

            // Business Rule 3: Sick leave requires a non-empty reason
            if (dto.LeaveType == "Sick" && string.IsNullOrWhiteSpace(dto.Reason))
            {
                throw new InvalidOperationException("Sick leave requires a reason.");
            }

            var entity = _mapper.Map<LeaveRequest>(dto);
            entity.CreatedAt = DateTime.UtcNow; // Static value to avoid migration issues
            _context.LeaveRequests.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<LeaveRequestDto>(entity);
        }

        public async Task<bool> UpdateLeaveRequestAsync(int id, LeaveRequestDto dto)
        {
            var entity = await _context.LeaveRequests.FindAsync(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLeaveRequestAsync(int id)
        {
            var entity = await _context.LeaveRequests.FindAsync(id);
            if (entity == null) return false;

            _context.LeaveRequests.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private static Expression<Func<LeaveRequest, object>> GetSortExpression(string sortBy)
        {
            return sortBy.ToLower() switch
            {
                "employeeid" => lr => lr.EmployeeId,
                "leavetype" => lr => lr.LeaveType,
                "status" => lr => lr.Status,
                "startdate" => lr => lr.StartDate,
                "enddate" => lr => lr.EndDate,
                "reason" => lr => lr.Reason,
                "createdat" => lr => lr.CreatedAt,
                _ => lr => lr.Id
            };
        }

        public async Task<PaginatedResult<LeaveRequestDto>> FilterLeaveRequestsAsync(
            int? employeeId, LeaveType? leaveType, LeaveStatus? status, DateTime? startDate, DateTime? endDate,
            string? keyword, int page, int pageSize, string sortBy, string sortOrder)
        {
            var query = _context.LeaveRequests.AsQueryable();

            // Filtrage
            if (employeeId.HasValue)
                query = query.Where(lr => lr.EmployeeId == employeeId.Value);

            if (leaveType.HasValue)
                query = query.Where(lr => lr.LeaveType == leaveType.Value);

            if (status.HasValue)
                query = query.Where(lr => lr.Status == status.Value);

            if (startDate.HasValue)
                query = query.Where(lr => lr.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(lr => lr.EndDate <= endDate.Value);

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(lr => lr.Reason.Contains(keyword));

            // Tri
            if (sortOrder.ToLower() == "desc")
            {
                query = query.OrderByDescending(GetSortExpression(sortBy));
            }
            else
            {
                query = query.OrderBy(GetSortExpression(sortBy));
            }

            // Pagination
            var totalItems = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // Mapping
            var mappedItems = _mapper.Map<List<LeaveRequestDto>>(items);

            return new PaginatedResult<LeaveRequestDto>
            {
                Items = mappedItems,
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}
