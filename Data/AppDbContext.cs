using Microsoft.EntityFrameworkCore;
using SecureWebApp.Models;
namespace SecureWebApp.Data;
public class AppDbContext : DbContext
{

    public IConfiguration _config { get; init; }

    public AppDbContext(IConfiguration config)
    {
        _config = config;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config.GetConnectionString("DatabaseConnection"));
    }

    public DbSet<User> Users { get; set; }
}
