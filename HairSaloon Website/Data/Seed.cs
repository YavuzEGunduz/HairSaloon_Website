using Microsoft.AspNetCore.Identity;
using HairSaloon_Website.Models;
using HairSaloon_Website.Data;
using HairSaloon_Website.Data.Enum;

namespace HairSaloon_Website.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<Context>();

                context.Database.EnsureCreated();

                if (!context.Employees.Any())
                {
                    context.Employees.AddRange(new List<Employee>()
                    {
                        new Employee()
                        {
                            Name = "Ahmet",
                            Age = 23,
                            Review = 4,
                            Working_hours = 4,

                        }


                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
