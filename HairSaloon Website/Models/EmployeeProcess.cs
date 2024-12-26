using System.ComponentModel.DataAnnotations;

namespace HairSaloon_Website.Models
{
    public class EmployeeProcess
    {
        [Key]
        public int EmployeeProcessId { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int ProcessId { get; set; }
        public Process Process { get; set; }
    }
}
