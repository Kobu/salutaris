using Microsoft.EntityFrameworkCore;
using salutaris.Database;
using salutaris.Models;
using salutaris.Utils;

namespace salutaris.Repositories;

public class UserRepository
{
    public async Task<Result<User>> CreateUser(User user)
    {
        await using var db = new DatabaseContext();

        try
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            return Result<User>.Ok(user);
        }
        catch (Exception e)
        {
            return Result<User>.Err(e);
        }
    }

    public async Task<Result<User?>> GetUserByName(string username)
    {
        await using var db = new DatabaseContext();
        try
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Name == username);
            return Result<User?>.Ok(user);
        }
        catch (Exception e)
        {
            return Result<User?>.Err(e);
        }
    }

    public async Task<Result<User>> GetUserById(Guid id)
    {
        await using var db = new DatabaseContext();
        try
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
            {
                return Result<User>.Err(new Exception($"User of id '{id}' was not found"));
            }
            return Result<User>.Ok(user);
        }
        catch (Exception e)
        {
            return Result<User>.Err(e);
        }
    }

    public async Task<Result<IEnumerable<User>>> GetAllUsers()
    {
        await using var db = new DatabaseContext();
        return Result<IEnumerable<User>>.Ok(await db.Users.ToListAsync());
    }
}