#region

using salutaris.Models;
using salutaris.Utils;

#endregion

namespace salutaris.Services;

public interface IExpenseService
{
    public Task<Result<Expense>> CreateExpense(Expense expense);
    public Task<Result<Expense>> GetExpenseById(Guid id);
    public Task<Result<List<Expense>>> GetExpensesByGroup(Guid groupId);
    public Task<Result<List<Expense>>> GetExpensesByUser(string username);
    public Task<Result<List<Expense>>> GetExpensesByGroupsByUser(Guid groupId, string username);
}