using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class User : IdentityUser<string>
{
    [Key]
    public new int Id { get; set; }
    public new string Email { get; set; }
    public new string Password { get; set; }
    public string Name_Surname { get; set; }
    public string Phone { get; set; }
}
