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
    private readonly IUserService _userService;

    public GetExpenseByGroupByUser(IExpenseService expenseService, IUserService userService)
    {
        _expenseService = expenseService;
        _userService = userService;
    }

    public override void Configure()
    {
        Get("expense/group/{groupId:guid}/{username}");
        Claims("UserId"); // allow only signed-in users to view this
    }

    protected override async Task<bool> HandleResult(GetExpenseByGroupByUserRequest req)
    {
        var user = await _userService.GetUserByName(req.InvokingUser);
        if (user.IsErr || user.Data is null)
        {
            return await HandleErr("Something went wrong");
        }

        if (user.Data.Name != req.Username)
        {
            return await HandleErr("Unauthorized access", StatusCodes.Status401Unauthorized);
        }
        
        var result = await _expenseService.GetExpensesByGroupsByUser(req.GroupId, req.Username);
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