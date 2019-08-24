using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TowerOfHanoi.Data;
using TowerOfHanoi.Models;

namespace TowerOfHanoi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OptimalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OptimalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Optimals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Optimal.Include(o => o.Configuration);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Optimals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optimal = await _context.Optimal
                .Include(o => o.Configuration)
                .FirstOrDefaultAsync(m => m.OptimalID == id);
            if (optimal == null)
            {
                return NotFound();
            }

            return View(optimal);
        }

        // GET: Optimals/Create
        public IActionResult Create()
        {
            ViewData["ConfigurationID"] = new SelectList(_context.Configuration, "ConfigurationID", "ConfigurationID");
            return View();
        }

        // POST: Optimals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OptimalID,DiskNr,MovesO,ConfigurationID")] Optimal optimal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(optimal);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Variations");
            }
            ViewData["ConfigurationID"] = new SelectList(_context.Configuration, "ConfigurationID", "ConfigurationID", optimal.ConfigurationID);
            return View(optimal);
        }

        // GET: Optimals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optimal = await _context.Optimal.FindAsync(id);
            if (optimal == null)
            {
                return NotFound();
            }
            ViewData["ConfigurationID"] = new SelectList(_context.Configuration, "ConfigurationID", "ConfigurationID", optimal.ConfigurationID);
            return View(optimal);
        }

        // POST: Optimals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OptimalID,DiskNr,MovesO,ConfigurationID")] Optimal optimal)
        {
            if (id != optimal.OptimalID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(optimal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OptimalExists(optimal.OptimalID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Variations");
            }
            ViewData["ConfigurationID"] = new SelectList(_context.Configuration, "ConfigurationID", "ConfigurationID", optimal.ConfigurationID);
            return View(optimal);
        }

        // GET: Optimals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optimal = await _context.Optimal
                .Include(o => o.Configuration)
                .FirstOrDefaultAsync(m => m.OptimalID == id);
            if (optimal == null)
            {
                return NotFound();
            }

            return View(optimal);
        }

        // POST: Optimals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var optimal = await _context.Optimal.FindAsync(id);
            _context.Optimal.Remove(optimal);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Variations");
        }

        private bool OptimalExists(int id)
        {
            return _context.Optimal.Any(e => e.OptimalID == id);
        }
    }
}
