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
            return RedirectToAction("ApoIndex");
        }

        [HttpGet]
        public JsonResult GetProcessesByEmployee(int employeeId)
        {
            var processes = _context.EmployeeProcesess
                                    .Where(ep => ep.EmployeeId == employeeId)
                                    .Include(ep => ep.Process)
                                    .Select(ep => new { ep.Process.Id, ep.Process.pName, ep.Process.Price })
                                    .ToList();
            return Json(processes);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminApo()
        {
            var appointments = _context.Appointments
                .Include(a => a.Process)
                .Include(a => a.Employee)
                .ToList();

            return View(appointments);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteAppointment(int id, string returnUrl)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null) 
            { 
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync(); 
            } // Geri dönülecek URL'yi kontrol edin ve yönlendirme yapın
              if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) 
            {
                return Redirect(returnUrl); 
            }
            return RedirectToAction("ApoIndex"); 
        }
        }
}
