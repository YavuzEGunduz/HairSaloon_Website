
using System.ComponentModel.DataAnnotations;

namespace HairSaloon_Website.Models
{
    public class Process
    {
        [Key]
        public int Id { get; set; }
        public string pName { get; set; }
        public int Price { get; set; }

        public int Time { get; set; }

        public ICollection<EmployeeProcess> EmployeeProcess{ get; set; }

    }
}
