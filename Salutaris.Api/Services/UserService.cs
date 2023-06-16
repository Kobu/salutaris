#region

using salutaris.Models;
using salutaris.Repositories;
using salutaris.Utils;

#endregion

namespace salutaris.Services;

public class UserService : IUserService
{
    private readonly UserRepository _userRepository = new();

    public async Task<Result<User>> GetUserById(Guid id)
    {
        return await _userRepository.GetUserById(id);
    }

    public async Task<Result<User?>> GetUserByName(string name)
    {
        return await _userRepository.GetUserByUsername(name);
    }

    public async Task<Result<User>> CreateNewUser(User user)
    {
        var userExists = await GetUserByName(user.Name);
        if (userExists.IsErr)
        {
            return Result<User>.Err(userExists.Error);
        }

        if (userExists.Data is not null)
        {
            return Result<User>.Err($"User with name '{user.Name}' already exists");
        }

        return await _userRepository.CreateUser(user);
    }

    public async Task<Result<IEnumerable<User>>> GetAllUsers()
    {
        return await _userRepository.GetAllUsers();
    }

    public async Task<Result<User>> AuthenticateUserByUsername(string username, string password)
    {
        var user = await _userRepository.GetUserByUsername(username);
        if (user.IsErr)
        {
            return Result<User>.Err(user.Error);
        }

        if (user.Data is null || user.Data.Password != password)
        {
            return Result<User>.Err("Invalid credentials");
        }

        return Result<User>.Ok(user.Data);
    }

    public async Task<Result<User>> AuthenticateUserById(Guid userId, string password)
    {
        var user = await _userRepository.GetUserById(userId);
        if (user.IsErr)
        {
            return Result<User>.Err(user.Error);
        }

        if (user.Data.Password != password)
        {
            return Result<User>.Err("Invalid credentials");
        }

        return Result<User>.Ok(user.Data);
    }
}