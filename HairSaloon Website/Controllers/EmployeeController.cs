using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HairSaloon_Website.Data;
using HairSaloon_Website.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace HairSaloon_Website.Controllers
{
    [Authorize]
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
                .ToList();
            return View(stafflist);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminStaff()
        {
            var stafflist = _context.Employees.ToList();
            return View(stafflist);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddEmployee()
        {
            ViewBag.Processes = _context.Processes.ToList(); // Süreçleri ViewBag ile gönderiyoruz.
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddEmployee(Employee employee, List<int> selectedProcesses)
        {
            // Null kontrolü
            if (employee == null)
            {
                ModelState.AddModelError("", "Çalışan bilgileri eksik.");
                ViewBag.Processes = _context.Processes.ToList();
                return View(employee);
            }

            // Resim yükleme işlemi
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

            // Çalışanı kaydet
            _context.Employees.Add(employee);
            _context.SaveChanges();

            // Seçilen süreçleri kontrol et
            if (selectedProcesses != null)
            {
                foreach (var processId in selectedProcesses)
                {
                    var employeeProcess = new EmployeeProcess
                    {
                        EmployeeId = employee.Id,
                        ProcessId = processId
                    };
                    _context.EmployeeProcesess.Add(employeeProcess);
                }
            }

            _context.SaveChanges();

            return RedirectToAction("AdminStaff");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction("AdminStaff");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult BringEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            return View("BringEmployee", employee);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditEmployee(Employee updatedEmployee)
        {
            var employee = _context.Employees.Find(updatedEmployee.Id);
            employee.Name = updatedEmployee.Name;
            employee.Age = updatedEmployee.Age;
            employee.StartHour = updatedEmployee.StartHour;
            employee.EndHour = updatedEmployee.EndHour;
            _context.SaveChanges();
            return RedirectToAction("AdminStaff");
        }
    }
}
