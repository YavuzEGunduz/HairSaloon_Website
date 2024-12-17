using HairSaloon_Website.Models;
using Microsoft.EntityFrameworkCore;

namespace HairSaloon_Website.Data
{
    public class Context : DbContext
    {   

        public Context(DbContextOptions<Context> options) : base(options)
        { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }


    }
}
