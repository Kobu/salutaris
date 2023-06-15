#region

using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Models;

#endregion

namespace salutaris.Mapping;

public static class ExpenseMapper
{
    public static Expense ToExpense(this CreateExpenseRequest request, Group group, User user)
    {
        return new Expense
        {
            Item = request.Item,
            Price = request.Price,
            Currency = request.Currency,
            UserId = user.Id,
            User = user,
            GroupId = group.Id,
            Group = group
        };
    }

    public static ExpenseResponse ToResponse(this Expense expense)
    {
        return new ExpenseResponse
        {
            Id = expense.Id,
            CreatedAt = expense.CreatedAt,
            UpdatedAt = expense.UpdatedAt,
            GroupId = expense.GroupId,
            Item = expense.Item,
            Price = expense.Price,
            Currency = expense.Currency,
            UserId = expense.UserId
        };
    }

    public static ExpenseResponseFull ToResponseFull(this Expense expense)
    {
        return new ExpenseResponseFull
        {
            Id = expense.Id,
            CreatedAt = expense.CreatedAt,
            UpdatedAt = expense.UpdatedAt,
            GroupId = expense.GroupId,
            Item = expense.Item,
            Price = expense.Price,
            Currency = expense.Currency,
            UserId = expense.UserId,
            GroupName = expense.Group.GroupName,
            UserName = expense.User.Name
        };
    }
}