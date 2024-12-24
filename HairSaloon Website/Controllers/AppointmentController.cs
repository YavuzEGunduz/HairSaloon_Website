using HairSaloon_Website.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HairSaloon_Website.Controllers
{
    public class AppointmentController : Controller
    {

        public readonly Context _context;
        public AppointmentController(Context context)
        {
            _context = context;
        }
        public IActionResult AppointmentPage()
        {
            var employees = _context.Employees.ToList();
            return View(employees);
        }
    }
}
