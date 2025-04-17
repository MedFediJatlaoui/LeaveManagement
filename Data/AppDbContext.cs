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
                new Employee { Id = 1, FullName = "Mohamed Fedi Jatlaoui", Department = "HR", JoiningDate = new DateTime(2025, 3, 15) },
                new Employee { Id = 2, FullName = "Bob Marley", Department = "IT", JoiningDate = new DateTime(2021, 5, 15) },
                new Employee { Id = 3, FullName = "Alice Johnson", Department = "Finance", JoiningDate = new DateTime(2023, 8, 1) },
                new Employee { Id = 4, FullName = "John Doe", Department = "Logistics", JoiningDate = new DateTime(2022, 11, 5) }
            );

            modelBuilder.Entity<LeaveRequest>().HasData(
                // Mohamed Fedi - 10 days Annual
                new LeaveRequest
                {
                    Id = 1,
                    EmployeeId = 1,
                    LeaveType = LeaveType.Annual,
                    StartDate = new DateTime(2025, 4, 10),
                    EndDate = new DateTime(2025, 4, 14),
                    Status = LeaveStatus.Approved,
                    Reason = "Spring break",
                    CreatedAt = new DateTime(2025, 4, 1)
                },
                new LeaveRequest
                {
                    Id = 2,
                    EmployeeId = 1,
                    LeaveType = LeaveType.Annual,
                    StartDate = new DateTime(2025, 8, 1),
                    EndDate = new DateTime(2025, 8, 5),
                    Status = LeaveStatus.Pending,
                    Reason = "Family vacation",
                    CreatedAt = new DateTime(2025, 7, 20)
                },
                new LeaveRequest
                {
                    Id = 3,
                    EmployeeId = 1,
                    LeaveType = LeaveType.Sick,
                    StartDate = new DateTime(2025, 5, 2),
                    EndDate = new DateTime(2025, 5, 4),
                    Status = LeaveStatus.Approved,
                    Reason = "Flu",
                    CreatedAt = new DateTime(2025, 5, 1)
                },

                // Bob Marley - 15 days Annual + 2 sick
                new LeaveRequest
                {
                    Id = 4,
                    EmployeeId = 2,
                    LeaveType = LeaveType.Annual,
                    StartDate = new DateTime(2025, 1, 10),
                    EndDate = new DateTime(2025, 1, 15),
                    Status = LeaveStatus.Approved,
                    Reason = "Winter trip",
                    CreatedAt = new DateTime(2025, 1, 5)
                },
                new LeaveRequest
                {
                    Id = 5,
                    EmployeeId = 2,
                    LeaveType = LeaveType.Annual,
                    StartDate = new DateTime(2025, 7, 10),
                    EndDate = new DateTime(2025, 7, 18),
                    Status = LeaveStatus.Approved,
                    Reason = "Summer holiday",
                    CreatedAt = new DateTime(2025, 7, 1)
                },
                new LeaveRequest
                {
                    Id = 6,
                    EmployeeId = 2,
                    LeaveType = LeaveType.Sick,
                    StartDate = new DateTime(2025, 2, 15),
                    EndDate = new DateTime(2025, 2, 16),
                    Status = LeaveStatus.Pending,
                    Reason = "Back pain",
                    CreatedAt = new DateTime(2025, 2, 14)
                },

                // Alice Johnson - 6 days Annual + 3 sick
                new LeaveRequest
                {
                    Id = 7,
                    EmployeeId = 3,
                    LeaveType = LeaveType.Annual,
                    StartDate = new DateTime(2025, 3, 25),
                    EndDate = new DateTime(2025, 3, 28),
                    Status = LeaveStatus.Approved,
                    Reason = "Family event",
                    CreatedAt = new DateTime(2025, 3, 20)
                },
                new LeaveRequest
                {
                    Id = 8,
                    EmployeeId = 3,
                    LeaveType = LeaveType.Annual,
                    StartDate = new DateTime(2025, 12, 2),
                    EndDate = new DateTime(2025, 12, 3),
                    Status = LeaveStatus.Approved,
                    Reason = "Trip to mountains",
                    CreatedAt = new DateTime(2025, 11, 30)
                },
                new LeaveRequest
                {
                    Id = 9,
                    EmployeeId = 3,
                    LeaveType = LeaveType.Sick,
                    StartDate = new DateTime(2025, 4, 5),
                    EndDate = new DateTime(2025, 4, 7),
                    Status = LeaveStatus.Approved,
                    Reason = "Migraine",
                    CreatedAt = new DateTime(2025, 4, 4)
                },

                // John Doe - 9 days Annual
                new LeaveRequest
                {
                    Id = 10,
                    EmployeeId = 4,
                    LeaveType = LeaveType.Annual,
                    StartDate = new DateTime(2025, 6, 10),
                    EndDate = new DateTime(2025, 6, 14),
                    Status = LeaveStatus.Pending,
                    Reason = "Cousin wedding",
                    CreatedAt = new DateTime(2025, 6, 1)
                },
                new LeaveRequest
                {
                    Id = 11,
                    EmployeeId = 4,
                    LeaveType = LeaveType.Annual,
                    StartDate = new DateTime(2025, 9, 1),
                    EndDate = new DateTime(2025, 9, 5),
                    Status = LeaveStatus.Approved,
                    Reason = "Short trip",
                    CreatedAt = new DateTime(2025, 8, 25)
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
