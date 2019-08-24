using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    [Authorize]
    public class ScoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScoresController(ApplicationDbContext context)
        {
            _context = context;
        }


        /* public async Task<IActionResult> View(string searchString)
         {

             var scores = _context.Score.Include(s => s.IdentityUser).Include(s=>s.Optimal).ThenInclude(s=>s.Configuration).ThenInclude(c => c.Variation).
                 OrderBy(s=>s.Optimal.Configuration.Variation.VariationID).
                 ThenBy(s=>s.Optimal.ConfigurationID).ThenByDescending(s=>s.Optimal.DiskNr).ThenBy(s=>s.Moves).ThenBy(s=> s.Elapsed).AsNoTracking();

             if (!String.IsNullOrEmpty(searchString))
             {
                 scores = scores.Where(s => s.IdentityUser.UserName.Contains(searchString));
             }

             return View(await scores.ToListAsync()) ;
         }*/

        [AllowAnonymous]
        public async Task<IActionResult> View1(int? movieGenre, string searchString, int? conFilter, int? diskFilter)
        {

            List<Variation> variationList = new List<Variation>();

            variationList = (from variation in _context.Variation select variation).ToList();
            variationList.Insert(0, new Variation { VariationID = 0, Code = "All" });

            List<Optimal> optimalList = new List<Optimal>();

            optimalList = _context.Optimal
            .GroupBy(p => p.DiskNr)
            .Select(g => g.First()).ToList();


            ViewBag.ListofVariation = variationList;
            ViewBag.ListofOptimals = optimalList;
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Variation
                                            orderby m.VariationID
                                            select m.Code;
            IQueryable<string> conQuery = from m in _context.Configuration
                                          orderby m.ConfigurationID
                                          select m.navodilo;
            IQueryable<int> optQuery = from m in _context.Optimal
                                       orderby m.OptimalID
                                       select m.DiskNr;

            /*<var scores = _context.Score.Include(s => s.IdentityUser).Include(s => s.Optimal).ThenInclude(s => s.Configuration).ThenInclude(c => c.Variation).
                OrderBy(s => s.Optimal.Configuration.Variation.VariationID).
                ThenBy(s => s.Optimal.ConfigurationID).ThenByDescending(s => s.Optimal.DiskNr).ThenBy(s => s.Moves).ThenBy(s => s.Elapsed).AsNoTracking();
                */

            var scores = from m in _context.Score
                         select m;
            if (movieGenre != null && movieGenre != 0)
            {
                scores = scores.Where(x => x.Optimal.Configuration.Variation.VariationID == movieGenre);
            }

          
            if (diskFilter != null)
            {
                scores = scores.Where(x => x.Optimal.DiskNr == diskFilter);
            }

            if (conFilter != null && conFilter != 0)
            {
                scores = scores.Where(x => x.Optimal.Configuration.ConfigurationID == conFilter);
            }
            if (conFilter != null)
            {
                if (conFilter > 0)
                {
                    scores = scores.Where(x => x.Optimal.Configuration.ConfigurationID == conFilter);
                }
            }

          
            if (!string.IsNullOrEmpty(searchString))
            {
                scores = scores.Where(s => s.IdentityUser.UserName.Contains(searchString));
            }
            var movieGenreVM = new ScoreBoard
            {
                Variations = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Configurations = new SelectList(await conQuery.Distinct().ToListAsync()),
             
                Optimals = new SelectList(await optQuery.Distinct().ToListAsync()),
                Scores = await scores.Include(s => s.IdentityUser).Include(s => s.Optimal).ThenInclude(s => s.Configuration).ThenInclude(c => c.Variation).
                OrderBy(s => s.Optimal.Configuration.Variation.VariationID).
                ThenBy(s => s.Optimal.ConfigurationID).ThenByDescending(s => s.Optimal.DiskNr).ThenBy(s => s.Moves).ThenBy(s => s.Elapsed).ToListAsync()
            };

            return View(movieGenreVM);
        }
     
        [AllowAnonymous]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }


        [AllowAnonymous]
        public async Task<IActionResult> Variations()
        {
            var viewModel = new ScoreIndexData();
            viewModel.Scores = await _context.Score
                  .Include(i => i.IdentityUser)
                  .Include(i => i.Optimal)
                    .ThenInclude(i => i.Configuration)

                  .AsNoTracking()
                  .OrderBy(i => i.IdentityUserID)
                  .ToListAsync();


            viewModel.Configurations = await _context.Configuration
                .Include(i => i.Optimals)
                .AsNoTracking()
                  .ToListAsync();

            viewModel.Variations = await _context.Variation
                .Include(i => i.Configurations)
                 .ThenInclude(i => i.Optimals)
                .AsNoTracking()
                  .ToListAsync();

            viewModel.Optimals = await _context.Optimal
                .Include(i => i.Configuration)
                .AsNoTracking()
                .ToListAsync();



            return View(viewModel);

          
        }


        [AllowAnonymous]
        // GET: Scores
        public async Task<IActionResult> Index(int? id, int? courseID)
        {
            var viewModel = new ScoreIndexData();
            viewModel.Scores = await _context.Score
                  .Include(i => i.IdentityUser)
                  .Include(i => i.Optimal)
                    .ThenInclude(i => i.Configuration)

                  .AsNoTracking()
                  .OrderBy(i => i.IdentityUserID)
                  .ToListAsync();

            if (id != null)
            {
                ViewData["ConfigurationID"] = id.Value;
                Score score = viewModel.Scores.Where(
                    i => i.OptimalID == id.Value).Single();

            }

            viewModel.Configurations = await _context.Configuration
                .Include(i => i.Optimals)
                .AsNoTracking()
                  .ToListAsync();

            viewModel.Variations = await _context.Variation
                .Include(i => i.Configurations)
                 .ThenInclude(i => i.Optimals)
                .AsNoTracking()
                  .ToListAsync();

            viewModel.Optimals = await _context.Optimal
                .Include(i => i.Configuration)
                .AsNoTracking()
                .ToListAsync();



            return View(viewModel);

            /*var applicationDbContext = _context.Score.Include(s => s.Variation);
            return View(await applicationDbContext.ToListAsync()); */
        }
        [AllowAnonymous]
        [Route("scores/results/{cId}/{towerN}")]
        public async Task<IActionResult> Results(int cId, int towerN)
        {

            //test.Scores =  _context.Score.Include(s=>s.IdentityUser).Include(s => s.Optimal).ThenInclude(o=> o.Configuration).Where(s => s.Optimal.DiskNr == towerN && s.Optimal.ConfigurationID == cId).OrderBy(s=> s.Moves).ThenBy(s=> s.Elapsed).Take(10);
            var scores = _context.Score.Include(s => s.IdentityUser).Include(s => s.Optimal).ThenInclude(o => o.Configuration).Where(s => s.Optimal.DiskNr == towerN && s.Optimal.ConfigurationID == cId).OrderBy(s => s.Moves).ThenBy(s => s.Elapsed).Take(10).ToList();
            List<ScoreTable> tblScores = new List<ScoreTable>();

            foreach (var score in scores)
            {
                tblScores.Add(new ScoreTable()
                {
                    ScoreID = score.ScoreID,
                    Elapsed = score.Elapsed,
                    UserName = score.IdentityUser.UserName,
                    Moves = score.Moves

                }); 
            }

            return Json(tblScores);

        }
        [AllowAnonymous]
        [Route("scores/demo2/{fullName}/{diskNr}")]
        public async Task<IActionResult> Demo2(int fullName, int diskNr)
        {
            var viewModel = new ScoreIndexData();


            viewModel.Configurations = await _context.Configuration
                .Include(i => i.Optimals)
                .AsNoTracking()
                  .ToListAsync();

            viewModel.Optimals = await _context.Optimal
                .Include(i => i.Configuration)
                .AsNoTracking()
                .ToListAsync();
            var x = "test";


            if (fullName != null)
            {
                ViewData["ConfigurationID"] = fullName;
                Configuration configuration = viewModel.Configurations.Where(
                    i => i.ConfigurationID == fullName).Single();

                ViewData["DiskNr"] = diskNr;
                Optimal Optimal = viewModel.Optimals.Where(o => o.ConfigurationID == fullName && o.DiskNr == diskNr).Single();

                //foreach (d in variation.Optimals){d. }
                //var result = this.Json(variation, JsonRequestBehavior.AllowGet);
                List<Optimal> optimals = configuration.Optimals.ToList();

                //var h = optimals.ToArray();
                Optimal[] array = configuration.Optimals.Cast<Optimal>().ToArray();

                Object[] ArrayOfObjects = new Object[] { Optimal.OptimalID, configuration.ConfigurationID, configuration.start, configuration.end };
                var list = new[] { configuration.ConfigurationID, configuration.start, Optimal.MovesO, Optimal.OptimalID }.ToList(); ;
                
                //foreach (var drat in configuration.Optimals) { if (drat.DiskNr == diskNr ) { list.Add(drat.MovesO);} list.Add(drat.OptimalID); };
                var d = DateTime.Now.ToString("HH:mm:ss.ff");
                return (Json(list));

            }

            return new JsonResult("test");
        }


        public async Task<IActionResult> Test(int? id)
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
            return View(optimal);
        }


        // GET: Scores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Score
                .Include(s => s.Optimal)
                .FirstOrDefaultAsync(m => m.ScoreID == id);
            if (score == null)
            {
                return NotFound();
            }

            return View(score);
        }


        // GET: Scores/Create
        public IActionResult Create()
        {
            ViewData["OptimalID"] = new SelectList(_context.Optimal, "OptimalID", "OptimalID");
            return View();
        }

        // POST: Scores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScoreID,Moves,IdentityUserID,OptimalID,Elapsed")] Score score)
        {
            if (ModelState.IsValid)
            {
                _context.Add(score);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OptimalID"] = new SelectList(_context.Configuration, "OptimalID", "OptimalID", score.OptimalID);
            return View(score);
        }

        // GET: Scores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Score.FindAsync(id);
            if (score == null)
            {
                return NotFound();
            }
            ViewData["OptimalID"] = new SelectList(_context.Configuration, "OptimalID", "OptimalID", score.Optimal);
            return View(score);
        }

        // POST: Scores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScoreID,Moves,Id,OptimalID,Elapsed")] Score score)
        {
            if (id != score.ScoreID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(score);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScoreExists(score.ScoreID))
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
            ViewData["ConfigurationID"] = new SelectList(_context.Configuration, "OptimalID", "OptimalID", score.OptimalID);
            return View(score);
        }

        // GET: Scores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Score
                .Include(s => s.Optimal)
                .FirstOrDefaultAsync(m => m.ScoreID == id);
            if (score == null)
            {
                return NotFound();
            }

            return View(score);
        }

        // POST: Scores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var score = await _context.Score.FindAsync(id);
            _context.Score.Remove(score);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScoreExists(int id)
        {
            return _context.Score.Any(e => e.ScoreID == id);
        }


        /*[AllowAnonymous]
        public IActionResult View()
        {
            List<Variation> variationList = new List<Variation>();

            variationList = (from variation in _context.Variation select variation).ToList();
            variationList.Insert(0, new Variation { VariationID = 0, Code = "Vse" });

            List<Optimal> optimalList = new List<Optimal>();

            optimalList = _context.Optimal
            .GroupBy(p => p.DiskNr)
            .Select(g => g.First()).ToList();


            ViewBag.ListofVariation = variationList;
            ViewBag.ListofOptimals = optimalList;
            return View();
        }*/

        [AllowAnonymous]
        public JsonResult GetConfiguration(int VariationID)
        {
            List<Configuration> configurationList = new List<Configuration>();

            configurationList = (from configuration in _context.Configuration
                                 where configuration.VariationID == VariationID
                                 select configuration).ToList();
            configurationList.Insert(0, new Configuration { ConfigurationID = 0, navodilo ="All" });
            return Json(new SelectList(configurationList, "ConfigurationID", "navodilo"));

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        [AllowAnonymous]

        public IActionResult EmailExists([Bind(Prefix = "Input.Email")] String email)
        {
            try
            {
                return Json(!IsEmailExists(email));
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        private bool IsEmailExists(string email)
            => _context.Users.Any(e => e.Email == email);



        [AllowAnonymous]

        public IActionResult UserNameExists([Bind(Prefix = "Input.UserName")] String username)
        {
            try
            {
                return Json(!IsUserNameExists(username));
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        private bool IsUserNameExists(string username)
            => _context.Users.Any(e => e.UserName == username);



        /*public JsonResult GetOptimal(int ConfigurationID)
        {
            List<Optimal> optimalList = new List<Optimal>();

            optimalList = (from optimal in _context.Optimal
                                 where optimal.ConfigurationID == ConfigurationID
                                 select optimal).ToList();
            optimalList.Insert(0, new Optimal { OptimalID = 0, DiskNr = 0 });
            return Json(new SelectList(optimalList, "OptimalID", "DiskNr"));

        }
        */
    }







}
