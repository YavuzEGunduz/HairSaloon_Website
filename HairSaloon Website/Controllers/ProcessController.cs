using HairSaloon_Website.Data;
using HairSaloon_Website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HairSaloon_Website.Controllers
{
    [Authorize]
    public class ProcessController : Controller
    {
        private readonly Context _context;

        public ProcessController(Context context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ProcessList()
        {
            var processes = _context.Processes.ToList();
            return View(processes);
        }

        public IActionResult PublicProcessList()
        {
            var processes = _context.Processes.ToList();
            return View(processes);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddProcess()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddProcess(Process process)
        {
                _context.Processes.Add(process);
                _context.SaveChanges();
                return RedirectToAction("ProcessList");
   
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteProcess(int id)
        {
            var process = _context.Processes.Find(id);
            if (process != null)
            {
                _context.Processes.Remove(process);
                _context.SaveChanges();
            }
            return RedirectToAction("ProcessList");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditProcess(int id)
        {
            var process = _context.Processes.Find(id);
            if (process == null)
            {
                return NotFound();
            }
            return View(process);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditProcess(Process updatedProcess)
        {
            if (ModelState.IsValid)
            {
                var process = _context.Processes.Find(updatedProcess.Id);
                if (process != null)
                {
                    process.pName = updatedProcess.pName;
                    process.Price = updatedProcess.Price;
                    process.Time = updatedProcess.Time;
                    _context.SaveChanges();
                }
                return RedirectToAction("ProcessList");
            }
            return View(updatedProcess);
        }
    }

}

