using Microsoft.EntityFrameworkCore;
using salutaris.Models;

namespace salutaris.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "database.db");
    }

    public DbSet<User> Users { get; set; }

    public string DbPath { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }
}