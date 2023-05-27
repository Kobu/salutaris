﻿using salutaris.Models;
using salutaris.Utils;

namespace salutaris.Services;

public interface IExpenseService
{
    public Task<Result<Expense>> CreateExpense(Expense expense);
    
    public Task<Result<Expense>> GetExpenseById(Guid id);

}