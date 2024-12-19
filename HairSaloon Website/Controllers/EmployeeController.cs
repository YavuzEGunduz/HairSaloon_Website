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

        [Authorize]
        public IActionResult Stuff()
        {
            List<Employee> stuff = _context.Employees.ToList();

            return View(stuff);
        }

        public IActionResult AdminStuff()
        {
            List<Employee> stuffad = _context.Employees.ToList();

            return View(stuffad);
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
            return RedirectToAction("AdminStuff");

        }


        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return RedirectToAction("AdminList");
        }

        public IActionResult EditEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult EditEmployee(Employee updatedEmployee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(updatedEmployee);
                _context.SaveChanges();
                return RedirectToAction("AdminList");
            }
            return View(updatedEmployee);
        }


    }
}
