using salutaris.Models;
using salutaris.Repositories;
using salutaris.Utils;

namespace salutaris.Services;

public class UserService : IUserService
{
    private readonly UserRepository _userRepository = new();

    public async Task<Result<User>> GetUserById(Guid id)
    {
        return await _userRepository.GetUserById(id);
    }

    public async Task<Result<User>> GetUserByName(string name)
    {
        return await _userRepository.GetUserByName(name);
    }

    public async Task<Result<User>> CreateNewUser(User user)
    {
        // TODO check if user with given name already exists
        return await _userRepository.CreateUser(user);
    }

    public async Task<Result<IEnumerable<User>>> GetAllUsers()
    {
        return await _userRepository.GetAllUsers();
    }

    public async Task<Result<bool>> AuthenticateUser(Guid userId, string password)
    {
        var user = await _userRepository.GetUserById(userId);
        if (user.IsErr)
        {
            return Result<bool>.Err(user.Error);
        }

        return Result<bool>.Ok(user.Data.Password == password);
    }
}