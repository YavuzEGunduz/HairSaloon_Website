using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HairSaloon_Website.Data;
using HairSaloon_Website.Models;
using HairSaloon_Website.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace HairSaloon_Website.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Context _context;

        public EmployeeController(Context context)
        {
            _context = context;

        }
        public IActionResult Staff() 
        {
            var stafflist = _context.Employees
                .Include(e => e.EmployeeProcess)
                .ThenInclude(ep => ep.Process)
                .ToList(); return View(stafflist); 
        }
        public IActionResult AdminStaff()
        {

            var stafflist = _context.Employees.ToList();
            return View(stafflist);
        }

            [HttpGet]
            public IActionResult AddEmployee()
            {
                ViewBag.Processes = _context.Processes.ToList();
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> AddEmployee(Employee employee, int[] selectedProcesses)
            {
                // Görsel işlemleri
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

                // Çalışan kaydı
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

            if (selectedProcesses != null && selectedProcesses.Any()) 
            {
                foreach (var processId in selectedProcesses) 
                {
                    var employeeProcess = new EmployeeProcess 
                    {
                        EmployeeId = employee.Id, ProcessId = processId 
                    };
                    _context.EmployeeProcesess.Add(employeeProcess); } }
            await _context.SaveChangesAsync();

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
