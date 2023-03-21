namespace ArtOrders.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class DbSeeder
{
    private static IServiceScope ServiceScope(IServiceProvider serviceProvider) => serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
    private static MainDbContext DbContext(IServiceProvider serviceProvider) => ServiceScope(serviceProvider).ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>().CreateDbContext();

    //private static readonly string masterUserName = "Admin";
    //private static readonly string masterUserEmail = "admin@dsrnetscool.com";
    //private static readonly string masterUserPassword = "Pass123#";

    //private static void ConfigureAdministrator(IServiceScope scope)
    //{
    //    //    var defaults = scope.ServiceProvider.GetService<IDefaultsSettings>();

    //    //    if (defaults != null)
    //    //    {
    //    //        var userService = scope.ServiceProvider.GetService<IUserService>();
    //    //        if (addAdmin && (!userService?.Any() ?? false))
    //    //        {
    //    //            var user = userService.Create(new CreateUserModel
    //    //            {
    //    //                Type = UserType.ForPortal,
    //    //                Status = UserStatus.Active,
    //    //                Name = defaults.AdministratorName,
    //    //                Password = defaults.AdministratorPassword,
    //    //                Email = defaults.AdministratorEmail,
    //    //                IsEmailConfirmed = !defaults.AdministratorEmail.IsNullOrEmpty(),
    //    //                Phone = null,
    //    //                IsPhoneConfirmed = false,
    //    //                IsChangePasswordNeeded = true
    //    //            })
    //    //                .GetAwaiter()
    //    //                .GetResult();

    //    //            userService.SetUserRoles(user.Id, Infrastructure.User.UserRole.Administrator).GetAwaiter().GetResult();
    //    //        }
    //    //    }
    //}

    public static void Execute(IServiceProvider serviceProvider, bool addDemoData, bool addAdmin = true)
    {
        using var scope = ServiceScope(serviceProvider);
        ArgumentNullException.ThrowIfNull(scope);

        //if (addAdmin)
        //{
        //    ConfigureAdministrator(scope);
        //}

        if (addDemoData)
        {
            Task.Run(async () =>
            {
                await ConfigureDemoData(serviceProvider);
            });
        }
    }

    private static async Task ConfigureDemoData(IServiceProvider serviceProvider)
    {
        await AddDemoData(serviceProvider);
    }

    private static async Task AddDemoData(IServiceProvider serviceProvider)
    {
        await using var context = DbContext(serviceProvider);

        // TODO: Доделать инициализацию БД
        if (context.Messages.Any() || context.Users.Any() || context.Images.Any() || context.PriceListItems.Any() || context.WorkExampleItems.Any() || context.Orders.Any() || context.Chats.Any())
            return;

        var i = new Entities.Image()
        {
            Link = "LocalData\\Images\\PHGAvatarImage.jpg",
            User = null,
            WorkExampleItem = null,
            Order = null
        };
        context.Images.Add(i);

        var u1 = new Entities.User()
        {
            Nickname = "P.H.G.",
            Avatar = i,
            Role = Entities.UserRole.Artist,
            Description = "Если кратко — я полуподвальный дровер, который шакалит арты и мангу.\n@pumpheadguy"
        };
        context.Users.Add(u1);

        var u2 = new Entities.User()
        {
            Nickname = "Damndelion",
            Role = Entities.UserRole.Customer
        };
        context.Users.Add(u2);

        var c1 = new Entities.Chat()
        {
            Customer = u2,
            Artist = u1
        };
        context.Chats.Add(c1);

        var o1 = new Entities.Order()
        {
            Name = "Sans",
            Customer = u2,
            Artist = u1,
            Status = Entities.OrderStatus.AtWork,
            //Chat = c1,
            EditsNumber = 0,
            Description = "Санс стоит в Пустоте и заряжает Гастер бластер, который объят синим пламенем"
        };
        context.Orders.Add(o1);

        c1.Order = o1;



        //var c1 = new Entities.Category()
        //{
        //    Title = "Classic"
        //};
        //context.Categories.Add(c1);

        //context.Books.Add(new Entities.Book()
        //{
        //    Title = "Tom Soyer",
        //    Description = "description description description description ",
        //    Author = a1,
        //    Categories = new List<Entities.Category>() { c1 },
        //});

        //context.Books.Add(new Entities.Book()
        //{
        //    Title = "War and peace",
        //    Description = "description description description description ",
        //    Author = a2,
        //    Categories = new List<Entities.Category>() { c1 },
        //});

        context.SaveChanges();
    }
}