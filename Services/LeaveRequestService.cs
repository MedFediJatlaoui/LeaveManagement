using AutoMapper;
using LeaveManagement.Data;
using LeaveManagement.DTOs;
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
    }
}
