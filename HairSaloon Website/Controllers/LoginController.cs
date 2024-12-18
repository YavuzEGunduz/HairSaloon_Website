using HairSaloon_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSaloon_Website.Controllers
{
    public class LoginController : Controller
    {
        private static List<User> Users = new List<User>();

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }        
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
    }
}
/*
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var customer = email.FirstOrDefault(u => u.e && u.Password == password);
            if (customer != null)
            {
                TempData["Message"] = $"Hoşgeldiniz, {email}!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Hatalı kullanıcı adı veya şifre!";
                return View();
            }
        }



        [HttpPost]
        public IActionResult Register(string email, string password)
        {
            if (email.Any(u => u.Email == email))
            {
                ViewBag.Error = "Bu kullanıcı adı zaten kullanılıyor!";
                return View();
            }

            email.Add(new User { Email = email, Password = password });
            TempData["Message"] = "Kayıt başarılı! Giriş yapabilirsiniz.";
            return RedirectToAction("Login");
        }
    }
}*/
