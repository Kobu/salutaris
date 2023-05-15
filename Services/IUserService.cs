using System.Collections.Immutable;
using salutaris.Models;

namespace salutaris.Services;

public interface IUserService
{
    public  Task<User> GetUserById(Guid id);

    public Task<User> GetUserByName(string name);


    public Task<User> CreateNewUser(User user);

    public Task<List<User>> GetAllUsers();
}