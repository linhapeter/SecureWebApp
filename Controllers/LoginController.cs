using Microsoft.AspNetCore.Mvc;
using SecureWebApp.Data;
using SecureWebApp.Models;

namespace SecureWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // WARNING: In production, always hash passwords and verify hashes!
                var user = _context.Users.FirstOrDefault(u =>
                    u.UserName == model.Username &&
                    u.Password == model.Password);

                if (user != null)
                {
                    // Store user info in session
                    _httpContextAccessor.HttpContext.Session.SetString("Username", user.UserName);
                    _httpContextAccessor.HttpContext.Session.SetString("Role", user.Role);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}