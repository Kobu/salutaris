using Microsoft.EntityFrameworkCore;
using salutaris.Models;

namespace salutaris.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "database2.db");
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Group> Groups { get; set; }

    public DbSet<Expense> Expenses { get; set; }

    public string DbPath { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.Groups)
            .WithMany(e => e.Users);

        modelBuilder.Entity<Group>()
            .HasOne(e => e.Creator);

        modelBuilder.Entity<Group>()
            .HasMany(e => e.Users);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }
}