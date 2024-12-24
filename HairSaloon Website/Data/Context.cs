using HairSaloon_Website.Data.Enum;
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

        public DbSet<Process> Processs { get; set; }
        public DbSet<EmployeeProcess> employeesProcess { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key configuration for the join table
            modelBuilder.Entity<EmployeeProcess>()
                .HasKey(ec => new { ec.EmployeeId, ec.ProcessId });

            // Many-to-Many relationship configuration
            modelBuilder.Entity<EmployeeProcess>()
                .HasOne(ec => ec.Employee)
                .WithMany(e => e.EmployeeProcess)
                .HasForeignKey(ec => ec.EmployeeId);

            modelBuilder.Entity<EmployeeProcess>()
                .HasOne(ec => ec.Process)
                .WithMany(c => c.EmployeeProcess)
                .HasForeignKey(ec => ec.ProcessId);
        }
    }
}
