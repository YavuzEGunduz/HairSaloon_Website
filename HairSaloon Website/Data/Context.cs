using HairSaloon_Website.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HairSaloon_Website.Data
{
    public class Context : IdentityDbContext
    {   

        public Context(DbContextOptions<Context> options) : base(options)
        { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
