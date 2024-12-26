using System;
using System.ComponentModel.DataAnnotations;

namespace HairSaloon_Website.Models
{
    public class Appointment
    {
        [Key]
        public int aId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime aDate { get; set; }

        [Required]
        public int aProcessId { get; set; }
        public Process aProcess { get; set; }

        [Required]
        public int aUserId { get; set; }
        public User aUser { get; set; }

        [Required]
        public int aEmployeeId { get; set; }
        public Employee aEmployee { get; set; }
    }
}
