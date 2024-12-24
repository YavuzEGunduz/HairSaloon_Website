namespace HairSaloon_Website.Models
{
    public class EmployeeProcess
    {

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int ProcessId { get; set; }
        public Process Process { get; set; }
    }
}
