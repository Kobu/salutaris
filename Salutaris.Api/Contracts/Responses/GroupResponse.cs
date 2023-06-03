﻿namespace salutaris.Contracts.Responses;

public class GroupResponse
{
    public Guid id { get; init; }
    public string GroupName { get; init; } = default!;

    public DateTime CreatedAt { get; init; } = default!;

    public DateTime UpdatedAt { get; init; } = default!;

    public Guid CreatorId { get; init; } = default!;
}