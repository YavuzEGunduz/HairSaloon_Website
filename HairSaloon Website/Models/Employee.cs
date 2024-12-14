using System.ComponentModel.DataAnnotations;

namespace HairSaloon_Website.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Age { get; set; }
        public string Speciality { get; set; }

        public float Review { get; set; }
        public int Working_hours{get; set;}


    }
}
