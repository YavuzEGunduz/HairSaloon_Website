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
            List<Employee> staff = _context.Employees.ToList();

            return View(staff);
        }

        public IActionResult AdminStaff()
        {
            List<Employee> staffad = _context.Employees.ToList();

            return View(staffad);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {

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
            employee .Review = updatedEmployee.Review;
            employee .ImageUrl = updatedEmployee.ImageUrl;
            _context.SaveChanges();
            return RedirectToAction("AdminStaff");
        }


    }
} 
