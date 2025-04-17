using LeaveManagement.DTOs;
using LeaveManagement.Enums;
using LeaveManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _service;

        public LeaveRequestController(ILeaveRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllLeaveRequestsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetLeaveRequestByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LeaveRequestDto dto)
        {
            var created = await _service.CreateLeaveRequestAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LeaveRequestDto dto)
        {
            var success = await _service.UpdateLeaveRequestAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteLeaveRequestAsync(id);
            return success ? NoContent() : NotFound();
        }
        [HttpGet("filter")]
        public async Task<IActionResult> FilterLeaveRequests(
    [FromQuery] int? employeeId,
    [FromQuery] LeaveType? leaveType,
    [FromQuery] LeaveStatus? status,
    [FromQuery] DateTime? startDate,
    [FromQuery] DateTime? endDate,
    [FromQuery] string? keyword,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string sortBy = "StartDate",
    [FromQuery] string sortOrder = "asc")
        {
            var result = await _service.FilterLeaveRequestsAsync(
                employeeId, leaveType, status, startDate, endDate, keyword, page, pageSize, sortBy, sortOrder);

            return Ok(result);
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetLeaveSummaryReport(
        [FromQuery] int year,
        [FromQuery] string? department = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null)
        {
            var result = await _service.GetLeaveSummaryReportAsync(year, department, startDate, endDate);
            return Ok(result);
        }
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveLeaveRequest(int id)
        {
            var success = await _service.ApproveLeaveRequestAsync(id);
            return success ? NoContent() : BadRequest("Only pending requests can be approved.");
        }
    }
}