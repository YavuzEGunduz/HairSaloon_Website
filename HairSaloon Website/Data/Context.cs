
using HairSaloon_Website.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace HairSaloon_Website.Data
{
    public class Context : IdentityDbContext
    {   

        public Context(DbContextOptions<Context> options) : base(options)
        { }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Process> Processes { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<EmployeeProcess> employeesProcess { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder buildmodel)
        {
            base.OnModelCreating(buildmodel);

            buildmodel.Entity<EmployeeProcess>()
                .HasKey(x => x.EmployeeProcessId);

            buildmodel.Entity<EmployeeProcess>()
                .HasOne(x => x.Employee)
                .WithMany(x => x.EmployeeProcess)
                .HasForeignKey(x => x.EmployeeId);
            
            buildmodel.Entity<EmployeeProcess>()
                .HasOne(x => x.Process)
                .WithMany(x => x.EmployeeProcess)
                .HasForeignKey(x => x.ProcessId);

            buildmodel.Entity<EmployeeProcess>()
                .HasOne(x => x.Employee)
                .WithMany(x => x.EmployeeProcess)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);


        }

    }
    

}
