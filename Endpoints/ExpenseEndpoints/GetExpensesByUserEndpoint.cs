using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.ExpenseEndpoints;

[HttpGet("expense/user/{id:guid}")]
[AllowAnonymous]
public class GetExpensesByUserEndpoint : ResultEndpoint<GetUserRequest, List<ExpenseResponse>>
{

    private readonly IExpenseService _expenseService;

    public GetExpensesByUserEndpoint(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    protected override async Task<bool> HandleResult(GetUserRequest req)
    {
        var result = await _expenseService.GetExpensesByUser(req.Id);
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