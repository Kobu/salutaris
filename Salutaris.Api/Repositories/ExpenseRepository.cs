﻿#region

using Microsoft.EntityFrameworkCore;
using salutaris.Database;
using salutaris.Models;
using salutaris.Utils;

#endregion

namespace salutaris.Repositories;

public class ExpenseRepository
{
    public async Task<Result<Expense>> CreateExpense(Expense expense)
    {
        try
        {
            await using var db = new DatabaseContext();

            await db.Expenses.AddAsync(expense);
            await db.SaveChangesAsync();

            var result = await db.Expenses
                .Include(x => x.Group)
                .Include(x => x.User)
                .FirstAsync(x => x.Id == expense.Id);

            return Result<Expense>.Ok(result);
        }
        catch (Exception e)
        {
            return Result<Expense>.Err(e);
        }
    }

    public async Task<Result<Expense>> GetExpenseById(Guid id)
    {
        await using var db = new DatabaseContext();
        var result = await db.Expenses
            .Include(x => x.Group)
            .Include(x => x.User)
            .FirstOrDefaultAsync(expense => expense.Id == id);

        if (result is null)
        {
            return Result<Expense>.Err("Expense was not found");
        }

        return Result<Expense>.Ok(result);
    }

    public async Task<Result<List<Expense>>> GetExpensesByGroup(Guid groupId)
    {
        try
        {
            await using var db = new DatabaseContext();
            var result = db.Expenses
                .Include(x => x.Group)
                .Include(x => x.User)
                .Where(expense => expense.GroupId == groupId)
                .ToList();

            return Result<List<Expense>>.Ok(result);
        }
        catch (Exception e)
        {
            return Result<List<Expense>>.Err(e);
        }
    }

    public async Task<Result<List<Expense>>> GetExpensesByUser(string username)
    {
        try
        {
            await using var db = new DatabaseContext();
            var result = db.Expenses
                .Include(x => x.Group)
                .Include(x => x.User)
                .Where(expense => expense.User.Name == username)
                .ToList();

            return Result<List<Expense>>.Ok(result);
        }
        catch (Exception e)
        {
            return Result<List<Expense>>.Err(e);
        }
    }
}