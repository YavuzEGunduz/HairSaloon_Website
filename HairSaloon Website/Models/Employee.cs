﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairSaloon_Website.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Age { get; set; }

        public ICollection<EmployeeProcess>? EmployeeProcess { get; set; }
        public TimeSpan WorkStartTime { get; set; }  
        public TimeSpan WorkEndTime { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped] 
        public IFormFile ImageFile { get; set; }


    }
}
