using LeaveManagement.Models;
using LeaveManagement.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LeaveManagement.Data
{
    public class LeaveRequestRepository : Repository<LeaveRequest>
    {
        private readonly AppDbContext _context;

        public LeaveRequestRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> HasOverlappingLeave(int employeeId, DateTime start, DateTime end)
        {
            return await _context.LeaveRequests.AnyAsync(lr =>
                lr.EmployeeId == employeeId &&
                lr.StartDate < end &&
                start < lr.EndDate);
        }

        public async Task<int> GetTotalAnnualLeaveDays(int employeeId, int year)
        {
            var annualLeaves = await _context.LeaveRequests
                .Where(lr => lr.EmployeeId == employeeId && lr.LeaveType == LeaveType.Annual && lr.StartDate.Year == year)
                .ToListAsync();

            return annualLeaves.Sum(lr => (lr.EndDate - lr.StartDate).Days + 1);
        }

        public IQueryable<LeaveRequest> Query()
        {
            return _context.LeaveRequests.Include(lr => lr.Employee).AsQueryable();
        }

        public IQueryable<LeaveRequest> Filter(
            IQueryable<LeaveRequest> query,
            int? employeeId,
            LeaveType? leaveType,
            LeaveStatus? status,
            DateTime? startDate,
            DateTime? endDate,
            string? keyword)
        {
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
            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.Where(lr => lr.Reason.Contains(keyword));

            return query;
        }

        public IQueryable<IGrouping<Employee, LeaveRequest>> GroupByEmployee(IQueryable<LeaveRequest> query)
        {
            return query.GroupBy(lr => lr.Employee);
        }

        public IQueryable<LeaveSummaryDto> GenerateLeaveSummaryReport(IQueryable<IGrouping<Employee, LeaveRequest>> grouped)
        {
            return grouped.Select(g => new LeaveSummaryDto
            {
                Employee = g.Key.FullName,
                TotalLeaves = g.Count(),
                AnnualLeaves = g.Count(lr => lr.LeaveType == LeaveType.Annual),
                SickLeaves = g.Count(lr => lr.LeaveType == LeaveType.Sick)
            });
        }

        public IQueryable<LeaveRequest> Sort(IQueryable<LeaveRequest> query, string sortBy, string sortOrder)
        {
            var sortExpression = GetSortExpression(sortBy);
            return sortOrder.ToLower() == "desc"
                ? query.OrderByDescending(sortExpression)
                : query.OrderBy(sortExpression);
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
    }
}
