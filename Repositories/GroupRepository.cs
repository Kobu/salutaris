using Microsoft.EntityFrameworkCore;
using salutaris.Database;
using salutaris.Models;
using salutaris.Utils;

namespace salutaris.Repositories;

public class GroupRepository
{
    public async Task<Result<Group>> CreateGroup(Group group)
    {
        await using var db = new DatabaseContext();
        await db.Groups.AddAsync(group);
        db.Users.Attach(group.Creator);
        await db.SaveChangesAsync();

        return Result<Group>.Ok(group);
    }

    public async Task<Result<bool>> DeleteGroup(Group group)
    {
        await using var db = new DatabaseContext();
        try
        {
            db.Groups.Remove(group);
            await db.SaveChangesAsync();
            return Result<bool>.Ok(true);
        }
        catch (Exception e)
        {
            return Result<bool>.Err(e);
        }
    }

    public async Task<Result<User>> JoinGroup(Group group, User user)
    {
        await using var db = new DatabaseContext();
        try
        {
            // TODO group creator cannot join his own group :(
            db.Groups.Attach(group);
            group.Users.Add(user);
            await db.SaveChangesAsync();
            return Result<User>.Ok(user);
        }
        catch (Exception e)
        {
            return Result<User>.Err(e);
        }
    }


    public async Task<Result<Group>> GetGroupById(Guid groupId)
    {
        await using var db = new DatabaseContext();
        try
        {
            var group = await db.Groups
                .Include(x => x.Creator)
                .Include(x => x.Users)
                .FirstOrDefaultAsync(group => group.Id == groupId);
            if (group is null) return Result<Group>.Err("Group was not found");

            return Result<Group>.Ok(group);
        }
        catch (Exception e)
        {
            return Result<Group>.Err(e);
        }
    }

    public async Task<Result<List<Group>>> GetAllGroups()
    {
        await using var db = new DatabaseContext();
        try
        {
            var groups = await db.Groups
                .Include(group => group.Creator)
                .ToListAsync();
            return Result<List<Group>>.Ok(groups);
        }
        catch (Exception e)
        {
            return Result<List<Group>>.Err(e);
        }
    }
}