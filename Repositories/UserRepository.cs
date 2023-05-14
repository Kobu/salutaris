using System.Collections.Immutable;
using salutaris.Database;
using salutaris.Models;

namespace salutaris.Repositories;

public class UserRepository
{
    public bool CreateUser(UserModel user)
    {
        using (var db = new DatabaseContext())
        {
            db.Users.Add(user);
        }

        return true;
    }

    public UserModel GetUserById(Guid id)
    {
        using (var db = new DatabaseContext())
        {
            return db.Users.FirstOrDefault(x => x.Id == id) ?? throw new InvalidOperationException("User not found");
        }
    }

    public UserModel GetUserByName(string name)
    {
        using (var db = new DatabaseContext())
        {
            return db.Users.FirstOrDefault(x => x.Name == name) ??
                   throw new InvalidOperationException("User not found");
        }
    }

    public ImmutableList<UserModel> GetAllUsers()
    {
        using (var db = new DatabaseContext())
        {
            return db.Users.ToImmutableList();
        }
    }
}