using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace HairSaloon_Website.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public int ProcessId { get; set; }
        public Process Process { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
