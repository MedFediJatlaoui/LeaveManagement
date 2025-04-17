using System.ComponentModel.DataAnnotations;
using LeaveManagement.Enums;

namespace LeaveManagement.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        [Required]
        public LeaveType LeaveType { get; set; }      
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public LeaveStatus Status { get; set; }       
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }

        public Employee Employee { get; set; }
    }

}
