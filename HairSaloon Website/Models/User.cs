
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HairSaloon_Website.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name_Surname{ get; set; }
        public string Email { get; set; }
        public string Password{ get; set; }
        public string Phone { get; set; }
        public float Price { get; set; }

    }

}
