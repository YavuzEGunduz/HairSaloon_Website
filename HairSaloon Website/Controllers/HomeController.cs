using System.Diagnostics;
using HairSaloon_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSaloon_Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       public IActionResult Index1()
        {
            var stf = new List<Employee>()
            {
                new Employee(){Id=1,Age=28,Name="osman",Review=3,Speciality="Painting",Working_hours=3},
                new Employee(){Id=1,Age=28,Name="aslan",Review=3,Speciality="Painting",Working_hours=3},
                new Employee(){Id=1,Age=28,Name="kosaln",Review=3,Speciality="Painting",Working_hours=3}


            };
            return View(stf);
        }

        }
}
