using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.ExpenseEndpoints;

public class GetExpensesByUserEndpoint : ResultEndpoint<GetUserRequest, List<ExpenseResponseFull>>
{

    private readonly IExpenseService _expenseService;
    public override void Configure()
    {
        Claims("UserId"); // allow only signed-in users to view this
        Get("expense/user/{id:guid}");
    }

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
            .Select(expense => expense.ToResponseFull())
            .ToList();
        
        return await HandleOk(response);
    }
}