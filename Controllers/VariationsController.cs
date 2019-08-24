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
    public class VariationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VariationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Variations
        public async Task<IActionResult> Index(int? id, int? courseID)
        {
            var ViewModel = new ScoreIndexData();
            ViewModel.Variations = await _context.Variation.Include(v => v.Configurations).ThenInclude(c => c.Optimals).AsNoTracking().ToListAsync();
            if (id != null)
            {
                ViewData["VariationID"] = id.Value;
                Variation variation = ViewModel.Variations.Where(v => v.VariationID == id.Value).Single();
                ViewModel.Configurations = _context.Configuration.Where(c => c.VariationID == id.Value);
              

            }
            if (courseID != null)
            {
                ViewData["ConfigurationID"] = courseID.Value;
                ViewModel.Optimals = _context.Optimal.Where(x => x.ConfigurationID == courseID);


            }



            return View(ViewModel);
        }

        // GET: Variations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variation = await _context.Variation
                .FirstOrDefaultAsync(m => m.VariationID == id);
            if (variation == null)
            {
                return NotFound();
            }

            return View(variation);
        }

        // GET: Variations/Create
        public IActionResult Create()
        {
            var variations = _context.Variation;
            ViewBag.codes = variations;
            return View();
        }

        // POST: Variations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VariationID,Connections,VarPic,TowerN,Code,Directed")] Variation variation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(variation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(variation);
        }

        // GET: Variations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variation = await _context.Variation.FindAsync(id);
            if (variation == null)
            {
                return NotFound();
            }
            return View(variation);
        }

        // POST: Variations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VariationID,Connections,VarPic,TowerN,Code,Directed")] Variation variation)
        {
            if (id != variation.VariationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(variation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VariationExists(variation.VariationID))
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
            return View(variation);
        }

        // GET: Variations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variation = await _context.Variation
                .FirstOrDefaultAsync(m => m.VariationID == id);
            if (variation == null)
            {
                return NotFound();
            }

            return View(variation);
        }

        // POST: Variations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var variation = await _context.Variation.FindAsync(id);
            _context.Variation.Remove(variation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VariationExists(int id)
        {
            return _context.Variation.Any(e => e.VariationID == id);
        }
    }
}
