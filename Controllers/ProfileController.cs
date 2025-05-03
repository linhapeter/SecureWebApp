using Microsoft.AspNetCore.Mvc;
using SecureWebApp.Data;
using SecureWebApp.Models;

public class ProfileController : Controller
{
    private readonly AppDbContext _context;

    public ProfileController(AppDbContext context)
    {
        _context = context;
    }

    // No authentication check - anyone can access
    public IActionResult Index()
    {
        return View();
    }

    // Password change with ZERO security
    [HttpPost]
    public IActionResult ChangePassword(string newPassword)
    {
        
        var username = Request.Cookies["Username"];

        
        var user = _context.Users.FirstOrDefault(u => u.UserName == username);
        if (user != null)
        {
            user.Password = newPassword; 
            _context.SaveChanges();

            
            ViewBag.Message = "Password changed";
        }

        return View("Index");
    }
}