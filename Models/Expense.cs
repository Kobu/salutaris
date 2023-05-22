﻿namespace salutaris.Models;

public class Expense : BaseModel
{
    public Group Group { get; set; } = default!;
    public User User { get; set; } = default!;
    public string Item { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public string Currency { get; set; } = default!;
}