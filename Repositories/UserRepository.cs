using System.Collections.Immutable;
using System.Data.Entity;
using salutaris.Database;
using salutaris.Models;

namespace salutaris.Repositories;

public class UserRepository
{
    public async Task<User> CreateUser(User user)
    {
        await using var db = new DatabaseContext();
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();

        return user;
    }

    public async Task<User> GetUserById(Guid id)
    {
        await using var db = new DatabaseContext();
        return await db.Users.FirstAsync(x => x.Id == id) ?? throw new InvalidOperationException("User not found");
    }

    public async Task<User> GetUserByName(string name)
    {
        await using var db = new DatabaseContext();
        return await db.Users.FirstAsync(x => x.Name == name) ??
               throw new InvalidOperationException("User not found");
    }

    public async Task<List<User>> GetAllUsers()
    {
        await using var db = new DatabaseContext();
        return await db.Users.ToListAsync();
    }
}