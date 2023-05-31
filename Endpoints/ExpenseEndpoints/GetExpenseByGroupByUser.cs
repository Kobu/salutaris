using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.ExpenseEndpoints;

[HttpGet("expense/group/{groupId:guid}/{userId:guid}")]
[AllowAnonymous]
public class GetExpenseByGroupByUser : ResultEndpoint<GetExpenseByGroupByUserRequest, List<ExpenseResponse>>
{

    private readonly IExpenseService _expenseService;

    public GetExpenseByGroupByUser(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    protected override async Task<bool> HandleResult(GetExpenseByGroupByUserRequest req)
    {
        var result = await _expenseService.GetExpensesByGroupsByUser(req.GroupId, req.UserId);
        if (result.IsErr)
        {
            return await HandleErr(result);
        }

        var response = result.Data
            .Select(expense => expense.ToResponse())
            .ToList();
        
        return await HandleOk(response);
    }
}