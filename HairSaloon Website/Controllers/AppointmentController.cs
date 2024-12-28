using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HairSaloon_Website.Data;
using HairSaloon_Website.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HairSaloon_Website.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AppointmentController(Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> ApoIndex()
        {
            var user = await _userManager.GetUserAsync(User);
            var appointments = _context.Appointments
                .Where(a => a.UserId == user.Id) // Yalnızca giriş yapmış kullanıcının randevuları
                .Include(a => a.Process)
                .Include(a => a.Employee)
                .Include(a => a.User)
                .ToList();
            return View(appointments);
        }

        [HttpGet]
        public IActionResult AddAppointment()
        {
            ViewBag.Processes = _context.Processes.ToList();
            ViewBag.Employees = _context.Employees.ToList(); // Processes yerine bu kullanılır
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment(Appointment appointment)
        {
            var user = await _userManager.GetUserAsync(User);
            appointment.UserId = user.Id;

            var employee = await _context.Employees.FindAsync(appointment.EmployeeId);
            var process = await _context.Processes.FindAsync(appointment.ProcessId);

            if (employee == null || process == null)
            {
                ModelState.AddModelError("", "Geçersiz çalışan veya süreç seçimi.");
                ViewBag.Processes = _context.Processes.ToList();
                ViewBag.Employees = _context.Employees.ToList();
                return View(appointment);
            }

            var appointmentEndTime = appointment.Date.AddMinutes(process.Time);

            // Çalışan saatleri içinde mi kontrolü
            if (appointment.Date.TimeOfDay < employee.StartHour || appointmentEndTime.TimeOfDay > employee.EndHour)
            {
                ModelState.AddModelError("", "Çalışan çalışma saatleri dışında randevu alamazsınız.");
                ViewBag.Processes = _context.Processes.ToList();
                ViewBag.Employees = _context.Employees.ToList();
                return View(appointment);
            }

            // Çakışan randevu kontrolü
            var conflictingAppointment = _context.Appointments.Any(a =>
                a.EmployeeId == appointment.EmployeeId &&
                ((appointment.Date >= a.Date && appointment.Date < a.Date.AddMinutes(a.Process.Time)) || // Yeni randevu, mevcut bir randevu içinde mi?
                 (appointmentEndTime > a.Date && appointmentEndTime <= a.Date.AddMinutes(a.Process.Time)) || // Yeni randevu sonu, mevcut bir randevu içinde mi?
                 (appointment.Date <= a.Date && appointmentEndTime >= a.Date.AddMinutes(a.Process.Time)))); // Yeni randevu, mevcut bir randevuyu kapsıyor mu?

            if (conflictingAppointment)
            {
                ModelState.AddModelError("", "Seçilen zaman diliminde başka bir randevu mevcut.");
                ViewBag.Processes = _context.Processes.ToList();
                ViewBag.Employees = _context.Employees.ToList();
                return View(appointment);
            }

            // Randevuyu kaydet
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction("ApoIndex");
        }


        public JsonResult GetEmployeesByProcess(int processId)
        {
            if (!_context.Processes.Any(p => p.Id == processId))
            {
                return Json(new { error = "Hatalı süreç ID'si." });
            }

            var employees = _context.EmployeeProcesess
                                    .Where(ep => ep.ProcessId == processId)
                                    .Include(ep => ep.Employee)
                                    .Select(ep => new { ep.Employee.Id, ep.Employee.Name })
                                    .ToList();

            return Json(employees);
        }


        public JsonResult GetAvailableEmployees(DateTime appointmentDate, int processId)
        {
            if (!_context.Processes.Any(p => p.Id == processId))
            {
                return Json(new { error = "Hatalı süreç ID'si." });
            }

            var process = _context.Processes.FirstOrDefault(p => p.Id == processId);

            // O tarih ve saatte çalışanlar müsait olup olmadığını kontrol et
            var employees = _context.EmployeeProcesess
                                    .Where(ep => ep.ProcessId == processId)
                                    .Include(ep => ep.Employee)
                                    .Where(ep => !_context.Appointments.Any(a =>
                                        a.EmployeeId == ep.EmployeeId &&
                                        a.Date < appointmentDate.AddMinutes(-process.Time) &&
                                        a.Date.AddMinutes(a.Process.Time) > appointmentDate))
                                    .Select(ep => new { ep.Employee.Id, ep.Employee.Name })
                                    .ToList();

            return Json(employees);
        }



        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminApo()
        {
            var appointments = _context.Appointments
                .Include(a => a.Process)
                .Include(a => a.Employee)
                .Include(a => a.User)
                .ToList();
            return View(appointments);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteAppointment(int id, string returnUrl)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("ApoIndex");
        }
    }
}
