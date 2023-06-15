#region

using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

#endregion

namespace salutaris.Endpoints.ExpenseEndpoints;

public class GetExpenseByGroupByUser : ResultEndpoint<GetExpenseByGroupByUserRequest, List<ExpenseResponse>>
{
    private readonly IExpenseService _expenseService;

    public GetExpenseByGroupByUser(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    public override void Configure()
    {
        Get("expense/group/{groupId:guid}/{userId:guid}");
        Claims("UserId");
    }

    protected override async Task<bool> HandleResult(GetExpenseByGroupByUserRequest req)
    {
        if (req.InvokingUser != req.UserId)
        {
            return await HandleErr("Unauthorized access", StatusCodes.Status401Unauthorized);
        }

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