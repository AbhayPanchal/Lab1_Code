﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Authorization;

// I, Abhay Panchal, student number 000813104, certify that this material is my
// original work. No other person's work has been used without due
// acknowledgement and I have not made my work available to anyone else.

namespace Lab1.Controllers
{
    [Authorize(Roles = "Manager,Employee")]
    public class EmployersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employers
        [Authorize(Roles = "Manager,Employee")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Employer.ToListAsync());
        }

        // GET: Employers/Details/5
        [Authorize(Roles = "Manager,Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employer == null)
            {
                return NotFound();
            }

            var employer = await _context.Employer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employer == null)
            {
                return NotFound();
            }

            return View(employer);
        }

        // GET: Employers/Create
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,Website,IncorporatedDate")] Employer employer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employer);
        }

        // GET: Employers/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employer == null)
            {
                return NotFound();
            }

            var employer = await _context.Employer.FindAsync(id);
            if (employer == null)
            {
                return NotFound();
            }
            return View(employer);
        }

        // POST: Employers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,Website,IncorporatedDate")] Employer employer)
        {
            if (id != employer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployerExists(employer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employer);
        }

        // GET: Employers/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employer == null)
            {
                return NotFound();
            }

            var employer = await _context.Employer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employer == null)
            {
                return NotFound();
            }

            return View(employer);
        }

        // POST: Employers/Delete/5
        [Authorize(Roles = "Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employer == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Employer'  is null.");
            }
            var employer = await _context.Employer.FindAsync(id);
            if (employer != null)
            {
                _context.Employer.Remove(employer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployerExists(int id)
        {
          return _context.Employer.Any(e => e.Id == id);
        }
    }
}
