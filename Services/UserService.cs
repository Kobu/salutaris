using salutaris.Models;
using salutaris.Repositories;

namespace salutaris.Services;

public class UserService : IUserService
{
    private readonly UserRepository _userRepository = new();

    public async Task<User> GetUserById(Guid id)
    {
        return await _userRepository.GetUserById(id);
    }

    public async Task<User> GetUserByName(string name)
    {
        return await _userRepository.GetUserByName(name);
    }

    public async Task<User> CreateNewUser(User user)
    {
        return await _userRepository.CreateUser(user);
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _userRepository.GetAllUsers();
    }
}