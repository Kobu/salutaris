#region

using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

#endregion

namespace salutaris.Endpoints.ExpenseEndpoints;

public class GetExpensesByUserEndpoint : ResultEndpoint<GetUserRequest, List<ExpenseResponseFull>>
{
    private readonly IExpenseService _expenseService;

    public GetExpensesByUserEndpoint(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    public override void Configure()
    {
        Claims("UserId"); // allow only signed-in users to view this
        Get("expense/user/{username}");
    }

    protected override async Task<bool> HandleResult(GetUserRequest req)
    {
        var result = await _expenseService.GetExpensesByUser(req.Username);
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