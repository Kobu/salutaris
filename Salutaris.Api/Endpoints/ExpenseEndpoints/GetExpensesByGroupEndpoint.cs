using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.ExpenseEndpoints;

public class GetExpensesByGroupEndpoint : ResultEndpoint<GetGroupByIdRequest, List<ExpenseResponse>>
{

    private readonly IExpenseService _expenseService;
    public override void Configure()
    {
        Claims("UserId"); // allow only signed-in users to view this
        Get("expense/group/{id:guid}");
    }
    
    public GetExpensesByGroupEndpoint(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    protected override async Task<bool> HandleResult(GetGroupByIdRequest req)
    {
        var result = await _expenseService.GetExpensesByGroup(req.Id);
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