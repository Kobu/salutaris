#region

using salutaris.Models;
using salutaris.Services;

#endregion

namespace salutaris;

public class Seeding
{
    private readonly IExpenseService ExpenseService = new ExpenseService();

    private readonly Group Group1;


    private readonly Group Group2;
    private readonly Group Group3;
    private readonly IGroupService GroupService = new GroupService();

    private readonly User User1 = new()
    {
        Id = Guid.Parse("59906db3-9545-4d12-805b-630406d09002"),
        Name = "Kobu",
        Password = "password"
    };

    private readonly User User2 = new()
    {
        Id = Guid.Parse("03a3eb13-04f2-4304-b31a-dda9fdc95e2f"),
        Name = "Grep",
        Password = "hello"
    };

    private readonly User User3 = new()
    {
        Id = Guid.Parse("80da3177-75a7-434c-892a-826a8cff8509"),
        Name = "a_user",
        Password = "123"
    };


    private readonly IUserService UserService = new UserService();

    public Seeding()

    {
        Group1 = new Group
        {
            Id = Guid.Parse("9dc1d2a5-6ef7-4119-8b99-671a6955d1e9"),
            Creator = User1,
            CreatorId = User1.Id,
            GroupName = "Koblizna 16 crackhouse"
        };
        ;
        Group2 = new Group
        {
            Id = Guid.Parse("9bdc8835-9217-498f-9e61-dfba5e8ead84"),
            Creator = User1,
            CreatorId = User1.Id,
            GroupName = "Discord Server Expenses"
        };
        Group3 = new Group
        {
            Id = Guid.Parse("7f296a7b-740a-477b-a57c-5fb83b2e30cf"),
            Creator = User2,
            CreatorId = User2.Id,
            GroupName = "Friends Group"
        };
    }

    public async void SeedData()
    {
        await SeedUsers();
        await SeedGroups();
        await SeedUserGroups();
        await SeedExpenses();
    }

    private async Task SeedUsers()
    {
        await UserService.CreateNewUser(User1);
        await UserService.CreateNewUser(User2);
        await UserService.CreateNewUser(User3);
    }

    private async Task SeedGroups()
    {
        await GroupService.CreateGroup(Group1);
        await GroupService.CreateGroup(Group2);
        await GroupService.CreateGroup(Group3);
    }

    private async Task SeedExpenses()
    {
        var random = new Random();
        var items = new[]
        {
            "socks", "soap", "plastic bags", "BMW X6", "toothpaste", "milk", "eggs", "greps", "toilet paper", "paper",
            "whiskey", "child"
        };


        var group1users = (await GroupService.GetGroupUsers(Group1.Id)).Data;
        for (var i = 0; i < group1users.Count * 5; i++)
        {
            var randomDate = DateTime.Now.AddDays(random.Next(10));
            var user1 = group1users[random.Next(group1users.Count)];
            await ExpenseService.CreateExpense(new Expense
            {
                Id = Guid.NewGuid(),
                Item = items[random.Next(items.Length)],
                Price = random.Next(3, 400),
                Currency = "EUR",
                UserId = user1.Id,
                GroupId = Group1.Id,
                CreatedAt = randomDate,
                UpdatedAt = randomDate
            });
        }

        var group2users = (await GroupService.GetGroupUsers(Group2.Id)).Data;
        for (var i = 0; i < group2users.Count * 5; i++)
        {
            var randomDate = DateTime.Now.AddDays(random.Next(10));
            var user2 = group2users[random.Next(group2users.Count)];
            await ExpenseService.CreateExpense(new Expense
            {
                Id = Guid.NewGuid(),
                Item = items[random.Next(items.Length)],
                Price = random.Next(3, 400),
                Currency = "EUR",
                UserId = user2.Id,
                GroupId = Group2.Id,
                CreatedAt = randomDate,
                UpdatedAt = randomDate
            });
        }

        var group3users = (await GroupService.GetGroupUsers(Group3.Id)).Data;
        for (var i = 0; i < group3users.Count * 5; i++)
        {
            var user3 = group3users[random.Next(group3users.Count)];
            var randomDate = DateTime.Now.AddDays(random.Next(10));
            await ExpenseService.CreateExpense(new Expense

            {
                Id = Guid.NewGuid(),
                Item = items[random.Next(items.Length)],
                Price = random.Next(3, 400),
                Currency = "EUR",
                UserId = user3.Id,
                GroupId = Group3.Id,
                CreatedAt = randomDate,
                UpdatedAt = randomDate
            });
        }
    }

    private async Task SeedUserGroups()
    {
        await GroupService.JoinGroup(Group1, User1);
        await GroupService.JoinGroup(Group2, User1);
        await GroupService.JoinGroup(Group3, User1);

        await GroupService.JoinGroup(Group1, User2);
        await GroupService.JoinGroup(Group2, User2);

        await GroupService.JoinGroup(Group1, User3);

        var user1 = new User
        {
            Name = "Mike Tyson",
            Password = "spinal"
        };
        await UserService.CreateNewUser(user1);
        await GroupService.JoinGroup(Group1, user1);
        var user = new User
        {
            Name = "Elon Musk",
            Password = "spacex"
        };
        await UserService.CreateNewUser(user);
        await GroupService.JoinGroup(Group1, user);
        var user2 = new User()
        {
            Name = "Leonardo Dicaprio",
            Password = "oscar"
        };
        await UserService.CreateNewUser(user2);
        await GroupService.JoinGroup(Group1, user2);


        var user3 = new User()
        {
            Name = "Benes",
            Password = "algo"
        };
        await UserService.CreateNewUser(user3);
        await GroupService.JoinGroup(Group2, user3);
        var user4 = new User ()
        {
            Name = "Bartek",
            Password = "meh"
        };
        await UserService.CreateNewUser(user4);
        await GroupService.JoinGroup(Group2, user4);

        var user5 = new User ()
        {
            Name = "Test-user",
            Password = "password"
        };
        await UserService.CreateNewUser(user5);
        await GroupService.JoinGroup(Group3, user5);
    }
}