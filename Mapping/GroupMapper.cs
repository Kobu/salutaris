using salutaris.Contracts.Responses;
using salutaris.Models;

namespace salutaris.Mapping;

public static class GroupMapper
{

    public static GroupResponse ToResponse(this Group group)
    {
        return new GroupResponse
        {
            id = group.Id,
            GroupName = group.GroupName,
            CreatedAt = group.CreatedAt,
            UpdatedAt = group.UpdatedAt,
            Creator = new()
            {
                id = group.Creator.Id,
                Name = group.Creator.Name,
                CreatedAt = group.Creator.CreatedAt,
                UpdatedAt = group.Creator.UpdatedAt
            }
        };
    }
}