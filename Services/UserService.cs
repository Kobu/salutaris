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
        return await _userRepository.CreateUser(user);
    }

    public async Task<Result<IEnumerable<User>>> GetAllUsers()
    {
        return await _userRepository.GetAllUsers();
    }
}