using System;
using Microsoft.EntityFrameworkCore.Migrations;


namespace LeaveManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "FullName", "JoiningDate" },
                values: new object[,]
                {
                    { 3, "Finance", "Alice Johnson", new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Logistics", "John Doe", new DateTime(2022, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.UpdateData(
                table: "LeaveRequests",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndDate", "Reason", "Status" },
                values: new object[] { new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spring break", "Approved" });

            migrationBuilder.InsertData(
                table: "LeaveRequests",
                columns: new[] { "Id", "CreatedAt", "EmployeeId", "EndDate", "LeaveType", "Reason", "StartDate", "Status" },
                values: new object[,]
                {
                    { 2, new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Annual", "Family vacation", new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending" },
                    { 3, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sick", "Flu", new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Approved" },
                    { 4, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Annual", "Winter trip", new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Approved" },
                    { 5, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Annual", "Summer holiday", new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Approved" },
                    { 6, new DateTime(2025, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sick", "Back pain", new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending" },
                    { 7, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Annual", "Family event", new DateTime(2025, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Approved" },
                    { 8, new DateTime(2025, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Annual", "Trip to mountains", new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Approved" },
                    { 9, new DateTime(2025, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sick", "Migraine", new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Approved" },
                    { 10, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Annual", "Cousin wedding", new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending" },
                    { 11, new DateTime(2025, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Annual", "Short trip", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Approved" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LeaveRequests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LeaveRequests",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LeaveRequests",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LeaveRequests",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LeaveRequests",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LeaveRequests",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "LeaveRequests",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "LeaveRequests",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "LeaveRequests",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "LeaveRequests",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "LeaveRequests",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndDate", "Reason", "Status" },
                values: new object[] { new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vacation", "Pending" });
        }
    }
}
