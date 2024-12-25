
using HairSaloon_Website.Models;
using Microsoft.AspNetCore.Identity;
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
            base.OnModelCreating(modelBuilder);

            // IdentityUserLogin<string> için birincil anahtar tanımı
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(x => new { x.LoginProvider, x.ProviderKey });
            });

            // Diğer Identity tablosu türleri için de yapılandırma
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
            });
        }

    }
}
