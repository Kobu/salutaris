using salutaris.Models;
using salutaris.Utils;

namespace salutaris.Services;

public interface IGroupService
{
    public Task<Result<Group>> CreateGroup(Group group);

    public Task<Result<bool>> DeleteGroup(Guid groupId, Guid creatorId);
    public Task<Result<User>> JoinGroup(Group group, User user);
    public Task<Result<Group>> GetGroupById(Guid groupId);
    public Task<Result<List<Group>>> GetAllGroups();
    public  Task<Result<List<User>>> GetGroupUsers(Guid groupId);
    public  Task<Result<Group>> GetGroupFullInfo(Guid groupId);
}