using HairSaloon_Website.Data;
using HairSaloon_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSaloon_Website.Controllers
{
    public class UserController : Controller
    {

            public readonly Context _context;
            public UserController(Context context)
            {
                _context = context;
            }

            public IActionResult AdminUserList()
            {
                List<User> cst = _context.Users.ToList();

                return View(cst);
            }

    }
    
}
