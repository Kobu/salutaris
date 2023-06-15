﻿#region

using salutaris.Models;
using salutaris.Repositories;
using salutaris.Utils;

#endregion

namespace salutaris.Services;

public class ExpenseService : IExpenseService
{
    private readonly ExpenseRepository _expenseRepository = new();

    public async Task<Result<Expense>> CreateExpense(Expense expense)
    {
        return await _expenseRepository.CreateExpense(expense);
    }

    public async Task<Result<Expense>> GetExpenseById(Guid id)
    {
        return await _expenseRepository.GetExpenseById(id);
    }

    public async Task<Result<List<Expense>>> GetExpensesByGroup(Guid groupId)
    {
        return await _expenseRepository.GetExpensesByGroup(groupId);
    }

    public async Task<Result<List<Expense>>> GetExpensesByUser(Guid userId)
    {
        return await _expenseRepository.GetExpensesByUser(userId);
    }

    public async Task<Result<List<Expense>>> GetExpensesByGroupsByUser(Guid groupId, Guid userId)
    {
        var byGroup = await GetExpensesByGroup(groupId);
        if (byGroup.IsErr)
        {
            return byGroup;
        }

        var result = byGroup.Data
            .Where(expense => expense.UserId == userId)
            .ToList();
        return Result<List<Expense>>.Ok(result);
    }
}