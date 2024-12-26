using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HairSaloon_Website.Data;
using HairSaloon_Website.Models;
using Microsoft.EntityFrameworkCore;

namespace HairSaloon_Website.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly Context _context;

        public AppointmentController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var appointments = _context.Appointments.Include(a => a.aEmployee).ToList();
            return View(appointments);
        }

        [HttpGet]
        public IActionResult AddAppointment()
        {
            ViewData["Processes"] = _context.Processes.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddAppointment(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.aUserId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id;
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["Processes"] = _context.Processes.ToList();
            return View(appointment);
        }

        public IActionResult DetailsAppointment(int id)
        {
            var appointment = _context.Appointments.Include(a => a.aEmployee).FirstOrDefault(a => a.aId == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        [HttpGet]
        public IActionResult BringAppointment(int id)
        {
            var appointment = _context.Appointments.Find(id);
            return View("EditAppointment", appointment);
        }

        [HttpPost]
        public IActionResult EditAppointment(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Appointments.Update(appointment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["Employees"] = _context.Employees.ToList();
            return View(appointment);
        }

        public IActionResult DeleteAppointment(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
