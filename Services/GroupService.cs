using System.Collections.Immutable;
using salutaris.Models;
using salutaris.Repositories;
using salutaris.Utils;

namespace salutaris.Services;

public class GroupService : IGroupService


{
    private readonly GroupRepository _groupRepository = new();

    public async Task<Result<Group>> CreateGroup(Group group)
    {
        return await _groupRepository.CreateGroup(group);
    }

    public async Task<Result<bool>> DeleteGroup(Guid groupId, Guid creatorId)
    {
        var group = await GetGroupById(groupId);
        if (group.IsErr) return Result<bool>.Err(group.Error);

        if (group.Data.Creator.Id != creatorId)
            return Result<bool>.Err(new Exception("This user is not authorized to delete the group"));

        return await _groupRepository.DeleteGroup(group.Data);
    }

    public async Task<Result<User>> JoinGroup(Group group, User user)
    {
        var groupUsers = await _groupRepository.GetGroupUsers(group.Id);
        if (groupUsers.IsErr)
        {
            return Result<User>.Err(groupUsers.Error);
        }

        var userInGroup = groupUsers.Data
            .FirstOrDefault(groupUser => groupUser.Id == user.Id);
        
        if (userInGroup is not null)
            return Result<User>.Err("User is already in the group");

        return await _groupRepository.JoinGroup(group, user);
    }

    public async Task<Result<Group>> GetGroupById(Guid groupId)
    {
        return await _groupRepository.GetGroupById(groupId);
    }

    public async Task<Result<List<Group>>> GetAllGroups()
    {
        return await _groupRepository.GetAllGroups();
    }

    public async Task<Result<List<User>>> GetGroupUsers(Guid groupId)
    {
        return await _groupRepository.GetGroupUsers(groupId);
    }

    public async Task<Result<Group>> GetGroupFullInfo(Guid groupId)
    {
        var group = await GetGroupById(groupId);
        if (group.IsErr)
        {
            return group;
        }

        var groupCreator = await _groupRepository.GetGroupCreator(groupId);
        if (groupCreator.IsErr)
        {
            return Result<Group>.Err(groupCreator.Error);
        }

        group.Data.Creator = groupCreator.Data;
        return group;
    }
}