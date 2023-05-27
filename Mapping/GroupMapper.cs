using salutaris.Contracts.Responses;
using salutaris.Models;

namespace salutaris.Mapping;

public static class GroupMapper
{

    public static GroupResponse ToResponse(this Group group)
    {
        return new()
        {
            id = group.Id,
            GroupName = group.GroupName,
            CreatedAt = group.CreatedAt,
            UpdatedAt = group.UpdatedAt,
            CreatorId = group.CreatorId,
        };
    }
}