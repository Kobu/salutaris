#region

using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

#endregion

namespace salutaris.Endpoints.ExpenseEndpoints;

public class GetExpensesByGroupEndpoint : ResultEndpoint<GetGroupByIdRequest, List<ExpenseResponseFull>>
{
    private readonly IExpenseService _expenseService;

    public GetExpensesByGroupEndpoint(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    public override void Configure()
    {
        Claims("UserId"); // allow only signed-in users to view this
        Get("expense/group/{id:guid}");
    }

    protected override async Task<bool> HandleResult(GetGroupByIdRequest req)
    {
        var result = await _expenseService.GetExpensesByGroup(req.Id);
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