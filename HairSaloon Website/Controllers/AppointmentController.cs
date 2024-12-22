using Microsoft.AspNetCore.Mvc;

namespace HairSaloon_Website.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult AppointmentPage()
        {
            return View();
        }
    }
}
