using Microsoft.EntityFrameworkCore;
using LeaveManagement.Models;
using LeaveManagement.Enums;

namespace LeaveManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, FullName = "Mohamed Fedi Jatlaoui", Department = "HR", JoiningDate = DateTime.Parse("2025-03-15") },
                new Employee { Id = 2, FullName = "Bob Marley", Department = "IT", JoiningDate = DateTime.Parse("2021-05-15") }
            );

            modelBuilder.Entity<LeaveRequest>().HasData(
                new LeaveRequest
                {
                    Id = 1,
                    EmployeeId = 1,
                    LeaveType = LeaveType.Annual,
                    StartDate = new DateTime(2025, 4, 10),
                    EndDate = new DateTime(2025, 4, 10),
                    Status = LeaveStatus.Pending,
                    Reason = "Vacation",
                    CreatedAt = new DateTime(2025, 4, 10)
                }
            );

            modelBuilder.Entity<LeaveRequest>()
                .Property(l => l.LeaveType)
                .HasConversion<string>();

            modelBuilder.Entity<LeaveRequest>()
                .Property(l => l.Status)
                .HasConversion<string>();
        }
    }
}
