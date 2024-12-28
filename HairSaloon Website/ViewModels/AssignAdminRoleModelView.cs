using System.ComponentModel.DataAnnotations;

namespace HairSaloon_Website.ViewModels
{
    public class AssignAdminRoleViewModel
    {
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
    }
}
