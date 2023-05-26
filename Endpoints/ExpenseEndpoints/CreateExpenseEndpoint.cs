using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.ExpenseEndpoints;

[HttpPost("expense")]
[AllowAnonymous]
public class CreateExpenseEndpoint : ResultEndpoint<CreateExpenseRequest, ExpenseResponse>
{
    private readonly IExpenseService _expenseService;
    private readonly IUserService _userService;
    private readonly IGroupService _groupService;
    

    public CreateExpenseEndpoint(IExpenseService expenseService, IUserService userService, IGroupService groupService)
    {
        _expenseService = expenseService;
        _userService = userService;
        _groupService = groupService;
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

        var expense = req.ToExpense(group.Data, user.Data);
        
        var result = await _expenseService.CreateExpense(expense);
        if (result.IsErr)
        {
            return await HandleErr(result);
        }

        return await HandleOk(result.Data.ToResponse());
    }
}