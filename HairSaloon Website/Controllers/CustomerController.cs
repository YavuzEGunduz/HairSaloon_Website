using HairSaloon_Website.Data;
using HairSaloon_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSaloon_Website.Controllers
{
    public class CustomerController : Controller
    {

            public readonly Context _context;
            public CustomerController(Context context)
            {
                _context = context;
            }

            public IActionResult AdminCustomerList()
            {
                List<Customer> cst = _context.Customers.ToList();

                return View(cst);
            }

    }
    
}
