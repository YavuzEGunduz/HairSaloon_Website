using Microsoft.AspNetCore.Mvc;
using HairSaloon_Website.Models;
using HairSaloon_Website.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

namespace HairSaloon_Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [HttpGet] public IActionResult Login() 
        {
            return View(); 
        }
        [HttpPost] public async Task<IActionResult> Login(LoginViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {

                        ModelState.AddModelError(string.Empty, "User not found.");
                    }
                }
            }
               
            return View(model); 
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var users = new IdentityUser()
                {
                    UserName = model.Name_Surname,
                    Email = model.Email,
                };
                var result = await userManager.CreateAsync(users, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(users,false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                }
                
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> UserList()
        {
            var users = userManager.Users.ToList();
            var userViewModels = users.Select(user => new UserViewModel
            {
                UserName = user.UserName,
                Email = user.Email // Şifre gösterimi önerilmez
            }).ToList();
            
            return View(userViewModels);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AssignAdminRole()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> AssignAdminRole(AssignAdminRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.UserEmail);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                    return View(model);
                }

                var result = await userManager.AddToRoleAsync(user, "Admin");
                if (result.Succeeded)
                {
                    ViewBag.Message = "Admin role assigned successfully.";
                    return View();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
    }
}

