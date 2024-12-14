using System.ComponentModel.DataAnnotations;

namespace HairSaloon_Website.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public string Gender{ get; set; }
        public string Email { get; set; }

        public string Last_visit { get; set; }
        public int Visited_time { get; set; }
    }
}
