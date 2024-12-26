using HairSaloon_Website.Data;
using HairSaloon_Website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HairSaloon_Website.Controllers
{
    public class EmployeeController : Controller
    {
        public readonly Context _context;
        public EmployeeController(Context context)
        {
            _context = context;
        }

        //[Authorize]
        public IActionResult Staff()
        {
           
            var stafflist=_context.Employees.ToList();
            return View(stafflist);
        }

        public IActionResult AdminStaff()
        {

            var stafflist = _context.Employees.ToList();
            return View(stafflist);
        }
        [HttpGet] 
        public IActionResult AddEmployee() 
        { 
            return View(); 
        }

        [HttpPost] 
        public IActionResult AddEmployee(Employee employee) 
        { 
            if (employee.ImageFile != null && employee.ImageFile.Length > 0) 
            { 
                var fileName = Path.GetFileName(employee.ImageFile.FileName); 
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName); 
                using (var stream = new FileStream(filePath, FileMode.Create)) 
                { 
                    employee.ImageFile.CopyTo(stream); 
                }
                employee.ImageUrl = "/images/" + fileName; 
            }
            _context.Employees.Add(employee); 
            _context.SaveChanges(); 
            return RedirectToAction("AdminStaff"); 
        }

        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction("AdminStaff");
        }

        public IActionResult BringEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            return View("BringEmployee",employee);
        }

        [HttpPost]
        public IActionResult EditEmployee(Employee updatedEmployee)
        {
            var employee =_context.Employees.Find(updatedEmployee.Id);
            employee .Name = updatedEmployee.Name;
            employee .Age = updatedEmployee.Age;
            employee .Working_hours = updatedEmployee.Working_hours;
            employee .ImageUrl = updatedEmployee.ImageUrl;
            _context.SaveChanges();
            return RedirectToAction("AdminStaff");
        }


    }
} 
