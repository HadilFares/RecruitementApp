using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecruitementManagementApp.Models;

namespace RecruitementManagementApp.Controllers
{
    public class RhController : Controller
    {
        private readonly RhmanagementDbContext _context;

        public RhController(RhmanagementDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult CompeleteProfile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CompeleteProfile(Rh rh)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (ModelState.IsValid)
            {
                if (userId.HasValue)
                {
                    rh.IdRh = userId.Value;

                    _context.RHs.Add(rh);
                    _context.SaveChanges();
                    var user = _context.Users.Find(userId);
                    if (user != null)
                    {
                        user.profilecompleted = true;
                        _context.SaveChanges();
                    }



                }



                return RedirectToAction("Index", "Formations");
            }
            return View(rh);
        }


        public async Task<IActionResult> lesemployeesSelonFormation()
        {
           
            var userId = HttpContext.Session.GetInt32("UserId");
            var demandes = _context.employeeFormation
            .Where(co => co.Formation.IdRh == userId.Value)
             .Include(co => co.Formation.employeeFormations)
             .ThenInclude(co => co.Employee)
             .ToList();

            var employeeIds = demandes.Select(co => co.Employee.IdEmployee).ToList();
            var users = _context.Users.Where(u => employeeIds.Contains(u.Id)).ToList();

            // Use ViewData to pass the list of users to the view
            ViewData["Users"] = new SelectList(users, "Id", "Name", "LastName", "Numero");
            ViewBag.SelectedUser = users.FirstOrDefault();

            return View("lesemployeesSelonFormation", demandes);



        }



     

        //[Route("Rh/Edit/{codeOffre&IdCandidat}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? IdFormation, int? IdEmployee)
        {
            if (IdFormation == null || IdEmployee == null || _context.employeeFormation == null)
            {
                return NotFound();
            }

            var formationemployee = await _context.employeeFormation.Include(o => o.Employee)
                .Include(o => o.Formation)
                .FirstOrDefaultAsync(m => m.IdEmployee == IdEmployee && m.IdFormation == IdFormation); ;
            if (formationemployee == null)
            {
                return NotFound();
            }
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "Id", "Id", formationemployee.IdEmployee);
            ViewData["IdFormation"] = new SelectList(_context.Formations, "Id", "Id", formationemployee.IdFormation);
            return View(formationemployee);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdEmployee,IdFormation,Status")] EmployeeFormation employeeFormation)
        {


            try
            {
                _context.Update(employeeFormation);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               
            }
            return RedirectToAction(nameof(lesemployeesSelonFormation));

            
        }



        public async Task<IActionResult> DetailsProfile()
        {

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || _context.RHs == null)
            {
                return NotFound();
            }

            var recruteur = await _context.RHs
                .FirstOrDefaultAsync(m => m.IdRh == userId);
            if (recruteur == null)
            {
                return NotFound();
            }

            return View(recruteur);
        }



        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || _context.RHs == null)
            {
                return NotFound();
            }

            var rh = await _context.RHs.FindAsync(userId);
            if (rh == null)
            {
                return NotFound();
            }
            return View(rh);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile([Bind("IdRh,Name,adresse,Numero")] Rh rh)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != rh.IdRh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RhExists(rh.IdRh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(DetailsProfile));
            }
            return View(rh);
        }
        private bool RhExists(int id)
        {
            return (_context.RHs?.Any(e => e.IdRh == id)).GetValueOrDefault();
        }
    }
}
