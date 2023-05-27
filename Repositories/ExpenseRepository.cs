using Microsoft.EntityFrameworkCore;
using salutaris.Database;
using salutaris.Models;
using salutaris.Services;
using salutaris.Utils;

namespace salutaris.Repositories;

public class ExpenseRepository
{
 

    public async Task<Result<Expense>> CreateExpense(Expense expense)
    {
        await using var db = new DatabaseContext();

        db.Attach(expense.Group);
        db.Attach(expense.User);
        await db.Expenses.AddAsync(expense);
        await db.SaveChangesAsync();

        var result = await db.Expenses
            .Include(x=>x.Group)
            .Include(x=> x.User)
            .FirstAsync(x => x.Id == expense.Id);
        
        return Result<Expense>.Ok(result);
    }

    public async Task<Result<Expense>> GetExpenseById(Guid id)
    {
        await using var db = new DatabaseContext();
        var result = await db.Expenses
            .Include(x=>x.Group)
            .Include(x=> x.User)
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
}