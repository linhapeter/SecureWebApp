using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureWebApp.Data;
using SecureWebApp.Models;
using System.Diagnostics;

namespace SecureWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
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

                var user = _context.Users
                     .FromSqlRaw($"SELECT * FROM Users WHERE UserName = '{model.Username}' AND Password = '{model.Password}'")
                     .FirstOrDefault();

                if (user != null)
                {
                    
                    Response.Cookies.Append("IsAuthenticated", "true");
                    Response.Cookies.Append("UserName", user.UserName);
                    Response.Cookies.Append("UserRole", user.Role);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return RedirectToAction("Login");
        }
    }
}