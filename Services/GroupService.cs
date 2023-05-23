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
        if (group.IsErr)
        {
            return Result<bool>.Err(group.Error);
        }

        if (group.Data.Creator.Id != creatorId)
        {
            return Result<bool>.Err(new Exception("This user is not authorized to delete the group"));
        }

        return await _groupRepository.DeleteGroup(group.Data);

    }

    public async Task<Result<User>> JoinGroup(Guid groupId, User userToJoin)
    {
        var group = await GetGroupById(groupId);
        if (group.IsErr)
        {
            return Result<User>.Err(group.Error);
        }

        return await _groupRepository.JoinGroup(group.Data, userToJoin);

    }

    public async Task<Result<Group>> GetGroupById(Guid groupId)
    {
        return await _groupRepository.GetGroupById(groupId);
    }
    
    public async Task<Result<List<Group>>> GetAllGroups()
    {
        return await _groupRepository.GetAllGroups();
    }

}