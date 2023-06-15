#region

using Microsoft.EntityFrameworkCore;
using salutaris.Database;
using salutaris.Models;
using salutaris.Utils;

#endregion

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
            db.Groups.Attach(group);
            var userGroup = new UserGroup
            {
                UserId = user.Id,
                User = user,
                GroupId = group.Id,
                Group = group
            };

            group.UserGroups.Add(userGroup);
            await db.SaveChangesAsync();
            return Result<User>.Ok(user);
        }
        catch (Exception e)
        {
            return Result<User>.Err(e);
        }
    }

    public async Task<Result<List<User>>> GetGroupUsers(Guid groupId)
    {
        await using var db = new DatabaseContext();
        try
        {
            var group = await db.Groups
                .FirstOrDefaultAsync(group => group.Id == groupId);
            if (group is null)
            {
                return Result<List<User>>.Err("Group was not found");
            }

            var userGroups = db.UserGroups
                .Where(x => x.GroupId == groupId)
                .Include(x => x.User)
                .Select(x => x.User)
                .ToList();

            return Result<List<User>>.Ok(userGroups);
        }
        catch (Exception e)
        {
            return Result<List<User>>.Err(e);
        }
    }

    public async Task<Result<User>> GetGroupCreator(Guid groupId)
    {
        await using var db = new DatabaseContext();
        try
        {
            var group = await db.Groups
                .Include(x => x.Creator)
                .FirstOrDefaultAsync(group => group.Id == groupId);
            if (group is null)
            {
                return Result<User>.Err("Group was not found");
            }

            return Result<User>.Ok(group.Creator);
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
                .FirstOrDefaultAsync(group => group.Id == groupId);
            if (group is null)
            {
                return Result<Group>.Err("Group was not found");
            }

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
                .ToListAsync();
            return Result<List<Group>>.Ok(groups);
        }
        catch (Exception e)
        {
            return Result<List<Group>>.Err(e);
        }
    }
}