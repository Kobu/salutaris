﻿using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Models;

namespace salutaris.Mapping;

public static class ExpenseMapper
{
    public static Expense ToExpense(this CreateExpenseRequest request, Group group, User user)
    {
        return new()
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
        return new()
        {
            Id = expense.Id,
            CreatedAt = expense.CreatedAt,
            UpdatedAt = expense.UpdatedAt,
            GroupId = expense.Group.Id,
            Item = expense.Item,
            Price = expense.Price,
            Currency = expense.Currency,
            UserId = expense.UserId
        };
    }

}