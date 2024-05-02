using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruitementManagementApp.Models;
using System.Net.NetworkInformation;

namespace RecruitementManagementApp.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly RhmanagementDbContext _context;
        public EmployeeController(RhmanagementDbContext context)
        {
            _context = context;
        }
        // GET: CandidatController
        public ActionResult Index()
        {
            return View();
        }
        //TempData["message"]="Postuled successfully"


        [HttpGet]
        public IActionResult CompeleteProfile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CompeleteProfile(Employee cd)
{
    var userId = HttpContext.Session.GetInt32("UserId");
    if (cd.DateNaiss != null||cd.Email != null)
    {


        if (userId.HasValue)
        {

            cd.IdEmployee = userId.Value;
            _context.Employees.Add(cd);
            _context.SaveChanges();
            var user = _context.Users.Find(userId);

            if (user != null)
            {
                user.profilecompleted = true;
                _context.SaveChanges();

            }

        }


        return RedirectToAction("AllFormations", "Employee");
    }
    return View(cd);
}

        public async Task<IActionResult> AllFormations()
        {


            var RhmanagementDbContext = _context.Formations.Include(o => o.LeRh).Where(o=>o.published);

            return View(await RhmanagementDbContext.ToListAsync());
        }



        public async Task<IActionResult> FilterFormation(string searchString)
        {
            var allformations = await _context.Formations.Include(o => o.LeRh).Where(o => o.published==true).ToListAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allformations.Where(n => n.Title.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();
                return View("AllFormations",filteredResult);
            }

            return View("AllFormations", allformations);
        }


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

            return View();
        }

        [Route("Employee/Postuler/{IdFormation}")]

        public async Task<IActionResult> Postuler(int? IdFormation)
        {
            if (IdFormation == null || _context.Formations == null)
            {
                return NotFound();
            }
            var userId = HttpContext.Session.GetInt32("UserId");

            var RhmanagementDbContext = _context.Formations.Include(o => o.LeRh).Where(o => o.published==true);

            bool hasAlreadyApplied = _context.employeeFormation
             .Any(co => co.IdEmployee == userId && co.IdFormation == IdFormation);

            if (hasAlreadyApplied)
            {
                TempData["ErrorMessage"] = "You have already applied for this job offer.";
                return RedirectToAction("AllFormations", await RhmanagementDbContext.ToListAsync());
            }

            EmployeeFormation oc = new EmployeeFormation
            {
                IdEmployee = userId.Value ,
                IdFormation = (int)IdFormation,
                Status = ApplicationStatus.EnAttente
            };
            _context.employeeFormation.Add(oc);
            _context.SaveChanges();
           
          //  var recruitmentDbContext = _context.Offres.Include(o => o.LeRh);
            TempData["SuccessMessage"] = "Operation was successful.";


            return View("AllFormations", await RhmanagementDbContext.ToListAsync());
        }


        public async Task<IActionResult> Suivremesdemandes( )
        {
            
            var userId = HttpContext.Session.GetInt32("UserId");


            var demandes = _context.employeeFormation
         .Where(co => co.IdEmployee == userId)
         .Include(co => co.Formation.LeRh);
         

            return View ("Suivremesdemandes", demandes);

          
        }




        // GET: CandidatController/Details/5

        public async Task<IActionResult> DetailsProfile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.IdEmployee == userId);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
        // GET: test/Edit/5
        public async Task<IActionResult> EditProfile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(userId);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile([Bind("IdEmployee,DateNaiss,Email")] Employee employee)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId != employee.IdEmployee)
            {
                return NotFound();
            }

           try
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.IdEmployee))
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
            catch { 
            return View(employee);}
        }
        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.IdEmployee == id)).GetValueOrDefault();
        }


    }
}
