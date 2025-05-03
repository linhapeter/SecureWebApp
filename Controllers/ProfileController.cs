using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureWebApp.Data;
using SecureWebApp.Models;

public class ProfileController : Controller
{
    private readonly AppDbContext _context;

    public ProfileController(AppDbContext context)
    {
        _context = context;
    }

    
    public IActionResult Index()
    {
        return View();
    }

    
    [HttpPost]
    public IActionResult ChangePassword(string username, string newPassword)
    {
        
        var sql = $"UPDATE Users SET Password = '{newPassword}' WHERE UserName = '{username}'";

        
        _context.Database.ExecuteSqlRaw(sql);

        ViewBag.Message = "Password changed (unsafely!)";
        return View("Index");
    }
}