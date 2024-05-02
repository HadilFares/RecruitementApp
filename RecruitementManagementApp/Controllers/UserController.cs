using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruitementManagementApp.Models;

namespace RecruitementManagementApp.Controllers
{
    public class UserController : Controller

    {
        private readonly RhmanagementDbContext _context;
        public UserController(RhmanagementDbContext context)
        {
            _context = context;
        }




        //private readonly List<User> _users = new List<User>(); // In a real application, you'd use a database.

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {

            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email is already taken.");
                    return View(user);
                }

                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login", "User");
            }
            return View(user);
        }

        //

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Validate the input parameters
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError(string.Empty, "Email and password are required");
                return View();
            }

            // Check if the user with the provided email exists
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null || !VerifyPassword(password, user.Password))
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password");
                return View();


            }
            if (user != null)
            {
                // Store user ID in session
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserRole", user.Role);
            }

            if (user.Role == "Rh")

            {
                if (user.profilecompleted == false)
                {
                    return RedirectToAction("CompeleteProfile", "Rh");
                }
                // Redirect to a protected resource or the home page after successful login
                // return RedirectToPage("Index", "Offres");

                return RedirectToAction("Index", "Formations");
            }
            else
            {
                if (user.profilecompleted == false)
                {
                    return RedirectToAction("CompeleteProfile", "Employee");
                }

                return RedirectToAction("AllFormations", "Employee");
            }
        
            /*else
            {
                if (user.profilecompleted == false)
                {
                    return RedirectToAction("CompeleteProfile", "Admin");
                }

                return RedirectToAction("AllFormations", "Admin");
            }
            */
        }
        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
            {
                // Implement your password verification logic here
                // For example, you can use a library like BCrypt or ASP.NET Core Identity PasswordHasher
                // For demonstration purposes, let's assume a simple string comparison
                return enteredPassword == storedPasswordHash;
            }
        public IActionResult Logout()
        {
       

            // Optionally, clear session data or perform additional logout-related tasks
            HttpContext.Session.Clear();
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            // Redirect to the login page or any other desired destination
            return RedirectToAction("Login", "User");
        }

        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
