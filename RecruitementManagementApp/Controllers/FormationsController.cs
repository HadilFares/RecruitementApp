using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using RecruitementManagementApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RecruitementManagementApp.Controllers
{
    public class FormationsController : Controller
    {
        private readonly RhmanagementDbContext _context;

        public FormationsController(RhmanagementDbContext context)
        {
            _context = context;
        }




        public async Task<IActionResult> FilterFormation(string searchString)
        {
            var allformations = await _context.Formations.Include(o => o.LeRh).ToListAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allformations.Where(n => n.Title.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();
                return View("Index", filteredResult);
            }

            return View("Index", allformations);
        }


       /* public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            var recruitementDbContext = _context.Offres.Where(o => o.nameRh == userId.Value);

            return View(await recruitementDbContext.ToListAsync());
        }*/



        public IActionResult Index(string titre, string type)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            IQueryable list = _context.Formations.Include(o => o.LeRh).Where(o => o.IdRh == userId.Value);
            var lestypes = new List<string> { "Publier", "Archiver" };
            ViewBag.type = new SelectList(lestypes);

            if (titre != null && type != null)
            {
                if (type == "Publier")
                {
                    list = from m in _context.Formations.Include(o => o.LeRh).Where(o => o.IdRh == userId.Value && o.Title.Contains(titre)
                           && o.published == true)
                           select m;

                }
                else
                {
                    list = from m in _context.Formations.Include(o => o.LeRh).Where(o => o.IdRh == userId.Value && o.Title.Contains(titre)
                           && o.archived == true)
                           select m;

                }

            }
            else if (titre == null && type == null)
            {
                list = from m in _context.Formations.Include(o => o.LeRh).Where(o => o.IdRh == userId.Value)
                       select m;
            }

            else if (titre == null)
            {
                if (type == "Publier")
                {
                    list = from m in _context.Formations.Include(o => o.LeRh).Where(o => o.IdRh == userId.Value
                           && o.published == true)
                           select m;

                }
                else
                {
                    list = from m in _context.Formations.Include(o => o.LeRh).Where(o => o.IdRh == userId.Value
                           && o.archived == true)
                           select m;

                }
            }
            else if (type == null)
            {
                list = from m in _context.Formations.Include(o => o.LeRh)
                       where m.IdRh == userId.Value && m.Title.Contains(titre)
                       select m;
            }
            return View(list);
        }



       






        // GET: Offres


        // GET: Offres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Formations == null)
            {
                return NotFound();
            }

            var formation = await _context.Formations
                .Include(o => o.LeRh)
                .FirstOrDefaultAsync(m => m.IdFormation == id);
            if (formation == null)
            {
                return NotFound();
            }

            return View(formation);
        }

        // GET: Offres/Create
        public IActionResult Create()
        {
            ViewData["IdRh"] = new SelectList(_context.RHs, "IdRh", "Name");
            return View();
        }

        // POST: Offres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFormation,Title,Description,published,archived,IdRh")] Formation formation)
        {
            /*if (ModelState.IsValid)
            {
               
            }*/
            var userId = HttpContext.Session.GetInt32("UserId");

            ViewData["IdRh"] = new SelectList(_context.RHs, "IdRh", "Name", formation.IdRh);
            try
            { 
                formation.IdRh = userId.Value;
                _context.Add(formation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View(formation);
            }
        }

        // GET: Offres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Formations == null)
            {
                return NotFound();
            }

            var formation = await _context.Formations.FindAsync(id);
            if (formation == null)
            {
                return NotFound();
            }
            ViewData["nameRh"] = new SelectList(_context.RHs, "IdRh", "Name", formation.IdRh);
            return View(formation);
        }

        // POST: Offres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFormation,Title,Description,published,archived,IdRh")] Formation formation)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (id != formation.IdFormation)
            {
                return NotFound();
            }

            
                try
                { formation.IdRh=userId.Value;
                    _context.Update(formation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormationExists(formation.IdFormation))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["IdRh"] = new SelectList(_context.RHs, "IdRh", "Name", formation.IdRh);
            return View(formation);
        }

        // GET: Offres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Formations == null)
            {
                return NotFound();
            }

            var formation = await _context.Formations
                .Include(o => o.LeRh)
                .FirstOrDefaultAsync(m => m.IdFormation == id);
            if (formation == null)
            {
                return NotFound();
            }

            return View(formation);
        }

        // POST: Offres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Formations == null)
            {
                return Problem("Entity set 'RhmanagementDbContext.Formations'  is null.");
            }
            var formation = await _context.Formations.FindAsync(id);
            if (formation != null)
            {
                _context.Formations.Remove(formation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormationExists(int id)
        {
          return (_context.Formations?.Any(e => e.IdFormation == id)).GetValueOrDefault();
        }
    }
}
