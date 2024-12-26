﻿using HairSaloon_Website.Data;
using HairSaloon_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSaloon_Website.Controllers
{
    public class ProcessController : Controller
    {
        private readonly Context _context;

        public ProcessController(Context context)
        {
            _context = context;
        }

        public IActionResult ProcessList()
        {
            var processes = _context.Processes.ToList();
            return View(processes);
        }

        [HttpGet]
        public IActionResult AddProcess()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProcess(Process process)
        {
                _context.Processes.Add(process);
                _context.SaveChanges();
                return RedirectToAction("ProcessList");
   
        }

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

        [HttpGet]
        public IActionResult BringProcess(int id)
        {
            var process = _context.Processes.Find(id);
            if (process == null)
            {
                return NotFound();
            }
            return View("BringProcess", process);
        }

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