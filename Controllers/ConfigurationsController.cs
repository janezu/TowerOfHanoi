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
    public class ConfigurationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConfigurationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Configurations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Configuration.Include(c => c.Variation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Configurations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuration = await _context.Configuration
                .Include(c => c.Variation)
                .FirstOrDefaultAsync(m => m.ConfigurationID == id);
            if (configuration == null)
            {
                return NotFound();
            }

            return View(configuration);
        }

        // GET: Configurations/Create
        public IActionResult Create()
        {
            ViewData["VariationID"] = new SelectList(_context.Variation, "VariationID", "Code");
            return View();
        }

        // POST: Configurations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConfigurationID,start,end,navodilo,conPic,VariationID")] Configuration configuration, int? o1, int? o2, int? o3, int? o4, int? o5, int? o6, int? o7, int? o8)
        {
            var p1 = 0;
            var p2 = 0;
            var p3 = 0;
            var p4 = 0;
            var p5 = 0;
            var p6 = 0;
            var p7 = 0;
            var p8 = 0;

            if (o1 != null) { p1 = o1.Value; }
            if (o2 != null) { p2 = o2.Value; }
            if (o3 != null) { p3 = o3.Value; }
            if (o4 != null) { p4 = o4.Value; }
            if (o5 != null) { p5 = o5.Value; }
            if (o6 != null) { p6 = o6.Value; }
            if (o7 != null) { p7 = o7.Value; }
            if (o8 != null) { p8 = o8.Value; }

            if (ModelState.IsValid)

            {
                var Optimals = new Optimal[]

          {
         

            new Optimal {ConfigurationID=configuration.ConfigurationID, DiskNr=1, MovesO=p1},
            new Optimal {ConfigurationID=configuration.ConfigurationID, DiskNr=2, MovesO=p2},
            new Optimal {ConfigurationID=configuration.ConfigurationID, DiskNr=3, MovesO=p3},
            new Optimal {ConfigurationID=configuration.ConfigurationID, DiskNr=4, MovesO=p4},
            new Optimal {ConfigurationID=configuration.ConfigurationID, DiskNr=5, MovesO=p5},
            new Optimal {ConfigurationID=configuration.ConfigurationID, DiskNr=6, MovesO=p6},
            new Optimal {ConfigurationID=configuration.ConfigurationID, DiskNr=7, MovesO=p7},
            new Optimal {ConfigurationID=configuration.ConfigurationID, DiskNr=8, MovesO=p8},
            new Optimal {ConfigurationID=configuration.ConfigurationID, DiskNr=9, MovesO=0},
            new Optimal {ConfigurationID=configuration.ConfigurationID, DiskNr=10, MovesO=0},
            new Optimal {ConfigurationID=configuration.ConfigurationID, DiskNr=11, MovesO=0},
            new Optimal {ConfigurationID=configuration.ConfigurationID, DiskNr=12, MovesO=0},
            new Optimal {ConfigurationID=configuration.ConfigurationID, DiskNr=13, MovesO=0},
            new Optimal {ConfigurationID=configuration.ConfigurationID, DiskNr=14, MovesO=0},
            new Optimal {ConfigurationID=configuration.ConfigurationID, DiskNr=15, MovesO=0},
          };
                foreach (Optimal o in Optimals)
                {
                    _context.Optimal.Add(o);
                }
              



                _context.Add(configuration);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Variations");
            }
            ViewData["VariationID"] = new SelectList(_context.Variation, "VariationID", "VariationID", configuration.VariationID);
            return View(configuration);
        }

        // GET: Configurations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuration = await _context.Configuration.FindAsync(id);
            if (configuration == null)
            {
                return NotFound();
            }
            ViewData["VariationID"] = new SelectList(_context.Variation, "VariationID", "VariationID", configuration.VariationID);
            return View(configuration);
        }

        // POST: Configurations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConfigurationID,start,end,navodilo,conPic,VariationID")] Configuration configuration)
        {
            if (id != configuration.ConfigurationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configuration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigurationExists(configuration.ConfigurationID))
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
            ViewData["VariationID"] = new SelectList(_context.Variation, "VariationID", "VariationID", configuration.VariationID);
            return View(configuration);
        }

        // GET: Configurations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuration = await _context.Configuration
                .Include(c => c.Variation)
                .FirstOrDefaultAsync(m => m.ConfigurationID == id);
            if (configuration == null)
            {
                return NotFound();
            }

            return View(configuration);
        }

        // POST: Configurations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var configuration = await _context.Configuration.FindAsync(id);
            _context.Configuration.Remove(configuration);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Variations");
        }

        private bool ConfigurationExists(int id)
        {
            return _context.Configuration.Any(e => e.ConfigurationID == id);
        }
    }
}
