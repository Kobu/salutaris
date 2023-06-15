#region

using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Models;

#endregion

namespace salutaris.Mapping;

public static class GroupMapper
{
    public static GroupResponse ToResponse(this Group group)
    {
        return new GroupResponse
        {
            Id = group.Id,
            GroupName = group.GroupName,
            CreatedAt = group.CreatedAt,
            UpdatedAt = group.UpdatedAt,
            CreatorId = group.CreatorId
        };
    }

    public static Group ToGroup(this CreateGroupRequest req, User groupCreator)
    {
        return new Group
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Creator = groupCreator,
            CreatorId = groupCreator.Id,
            GroupName = req.Name
        };
    }
}