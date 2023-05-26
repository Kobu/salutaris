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
}