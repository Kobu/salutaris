﻿using FastEndpoints;

namespace salutaris.Contracts.Requests;

public class GetGroupByIdRequest
{
    public Guid Id { get; init; } = default;
    [FromClaim] 
    public Guid UserId { get; set; }
}