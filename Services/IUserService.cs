﻿using salutaris.Models;
using salutaris.Utils;

namespace salutaris.Services;

public interface IUserService
{
    public Task<Result<User>> GetUserById(Guid id);

    public Task<Result<User>> GetUserByName(string name);


    public Task<Result<User>> CreateNewUser(User user);

    public Task<Result<IEnumerable<User>>> GetAllUsers();
}