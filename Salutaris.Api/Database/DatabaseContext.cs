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
    public DbSet<UserGroup> UserGroups { get; set; }

    public string DbPath { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Name)
            .IsUnique();
        
        modelBuilder.Entity<UserGroup>()
            .HasKey(ug => new { ug.UserId, ug.GroupId });

        modelBuilder.Entity<UserGroup>()
            .HasOne(ug => ug.User)
            .WithMany(u => u.UserGroups)
            .HasForeignKey(ug => ug.UserId);

        modelBuilder.Entity<UserGroup>()
            .HasOne(ug => ug.Group)
            .WithMany(g => g.UserGroups)
            .HasForeignKey(ug => ug.GroupId);

        modelBuilder.Entity<Expense>()
            .HasOne(e => e.User)
            .WithMany(u => u.Expenses)
            .HasForeignKey(e => e.UserId);

        modelBuilder.Entity<Expense>()
            .HasOne(e => e.Group)
            .WithMany(g => g.Expenses)
            .HasForeignKey(e => e.GroupId);

        modelBuilder.Entity<Group>()
            .HasOne(g => g.Creator)
            .WithMany()
            .HasForeignKey(g => g.CreatorId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.EnableSensitiveDataLogging();
        options.UseSqlite($"Data Source={DbPath}");
    }
}