#region

using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;
using salutaris.Utils;

#endregion

namespace salutaris.Endpoints.ExpenseEndpoints;

public class CreateExpenseEndpoint : ResultEndpoint<CreateExpenseRequest, ExpenseResponse>
{
    private readonly IExpenseService _expenseService;
    private readonly IGroupService _groupService;
    private readonly IUserService _userService;

    public CreateExpenseEndpoint(IExpenseService expenseService, IUserService userService, IGroupService groupService)
    {
        _expenseService = expenseService;
        _userService = userService;
        _groupService = groupService;
    }

    public override void Configure()
    {
        Claims("UserId");
        Post("expense");
    }

    protected override async Task<bool> HandleResult(CreateExpenseRequest req)
    {
        var group = await _groupService.GetGroupById(req.GroupId);
        if (group.IsErr)
        {
            return await HandleErr(group);
        }

        var user = await _userService.GetUserById(req.UserId);
        if (user.IsErr)
        {
            return await HandleErr(user);
        }

        var userInGroup = await _groupService.UserBelongsToGroup(req.GroupId, req.UserId);
        if (userInGroup.IsErr || !userInGroup.Data)
        {
            return await HandleErr(Result<object>.Err("User does not belong to this group"));
        }

        var expense = req.ToExpense(group.Data, user.Data);

        var result = await _expenseService.CreateExpense(expense);
        if (result.IsErr)
        {
            return await HandleErr(result);
        }

        return await HandleOk(result.Data.ToResponse());
    }
}