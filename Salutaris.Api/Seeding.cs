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
        var items = new[] { "socks", "soap", "plastic bags", "BMW X6", "toothpaste", "milk", "eggs" };


        var group1users = (await GroupService.GetGroupUsers(Group1.Id)).Data;
        for (var i = 0; i < 10;  i++)
        {
            var user1 = group1users[random.Next(group1users.Count)];
            await ExpenseService.CreateExpense(new Expense
            {
                Id = Guid.NewGuid(),
                Item = items[random.Next(items.Length)],
                Price = random.Next(3, 400),
                Currency = "EUR",
                UserId = user1.Id,
                GroupId = Group1.Id,
            });
        }

        var group2users = (await GroupService.GetGroupUsers(Group2.Id)).Data;
        for (var i = 0; i < 10; i++)
        {
            var user2 = group2users[random.Next(group2users.Count)];
            await ExpenseService.CreateExpense(new Expense
            {
                Id = Guid.NewGuid(),
                Item = items[random.Next(items.Length)],
                Price = random.Next(3, 400),
                Currency = "EUR",
                UserId = user2.Id,
                User = user2,
                GroupId = Group2.Id,
                Group = Group2
            });
        }

        var group3users = (await GroupService.GetGroupUsers(Group3.Id)).Data;
        for (var i = 0; i < 10; i++)
        {
            var user3 = group3users[random.Next(group3users.Count)];
            await ExpenseService.CreateExpense(new Expense
            {
                Id = Guid.NewGuid(),
                Item = items[random.Next(items.Length)],
                Price = random.Next(3, 400),
                Currency = "EUR",
                UserId = user3.Id,
                User = user3,
                GroupId = Group3.Id,
                Group = Group3
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
    }
}