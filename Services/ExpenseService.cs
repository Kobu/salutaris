using salutaris.Models;
using salutaris.Repositories;
using salutaris.Utils;

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
}