using LeaveManagement.Data;
using LeaveManagement.DTOs;
using LeaveManagement.Enums;

namespace LeaveManagement.Services
{
    public interface ILeaveRequestService
    {
        Task<IEnumerable<LeaveRequestDto>> GetAllLeaveRequestsAsync();
        Task<LeaveRequestDto> CreateLeaveRequestAsync(LeaveRequestDto leaveRequestDto);
        Task<LeaveRequestDto?> GetLeaveRequestByIdAsync(int id);

        Task<bool> UpdateLeaveRequestAsync(int id, LeaveRequestDto leaveRequestDto);

        Task<bool> DeleteLeaveRequestAsync(int id);
        Task<PaginatedResult<LeaveRequestDto>> FilterLeaveRequestsAsync(
    int? employeeId, LeaveType? leaveType, LeaveStatus? status, DateTime? startDate, DateTime? endDate,
    string? keyword, int page, int pageSize, string sortBy, string sortOrder);
    }
}
