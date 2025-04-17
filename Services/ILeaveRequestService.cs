using LeaveManagement.DTOs;

namespace LeaveManagement.Services
{
    public interface ILeaveRequestService
    {
        Task<IEnumerable<LeaveRequestDto>> GetAllLeaveRequestsAsync();
        Task<LeaveRequestDto> CreateLeaveRequestAsync(LeaveRequestDto leaveRequestDto);
        Task<LeaveRequestDto?> GetLeaveRequestByIdAsync(int id);

        Task<bool> UpdateLeaveRequestAsync(int id, LeaveRequestDto leaveRequestDto);

        Task<bool> DeleteLeaveRequestAsync(int id);
    }
}
