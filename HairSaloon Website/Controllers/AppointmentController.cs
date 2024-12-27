using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HairSaloon_Website.Data;
using HairSaloon_Website.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HairSaloon_Website.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AppointmentController(Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var appointments = _context.Appointments
                .Include(a => a.Process)
                .Include(a => a.Employee)
                .ToList(); 
            return View(appointments);
        }

        [HttpGet]
        public IActionResult AddAppointment()
        {
            ViewBag.Employees = _context.Employees.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment(Appointment appointment)
        {
            var user = await _userManager.GetUserAsync(User);

            appointment.UserId = user.Id;


                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            

        }

        [HttpGet]
        public JsonResult GetProcessesByEmployee(int employeeId)
        {
            var processes = _context.EmployeeProcesess
                                    .Where(ep => ep.EmployeeId == employeeId)
                                    .Include(ep => ep.Process)
                                    .Select(ep => new { ep.Process.Id, ep.Process.pName })
                                    .ToList();
            return Json(processes);
        }
    }
}
