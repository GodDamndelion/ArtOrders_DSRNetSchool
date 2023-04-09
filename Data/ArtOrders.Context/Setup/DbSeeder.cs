namespace ArtOrders.Context;

using ArtOrders.Context.Entities;
using ArtOrders.Services.Users;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class DbSeeder
{
    private static IServiceScope ServiceScope(IServiceProvider serviceProvider) => serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
    private static MainDbContext DbContext(IServiceProvider serviceProvider) => ServiceScope(serviceProvider).ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>().CreateDbContext();

    //private static readonly string masterUserName = "admin@dsr.com";
    private static readonly string masterUserEmail = "bestadmin@dsr.com";
    private static readonly string masterUserPassword = "BestAdmin1234!";

    private static void ConfigureAdministrator(IServiceScope scope)
    {
        // TODO: Данные админа вынести в настройки!?
        //var defaults = scope.ServiceProvider.GetService<IDefaultsSettings>();

        //if (defaults != null)
        //{
            var userService = scope.ServiceProvider.GetService<IUserService>();
            ArgumentNullException.ThrowIfNull(userService);
            
            using var context = DbContext(scope.ServiceProvider);
            if (context.Users.FirstOrDefault(u => u.Email == masterUserEmail) == null)
            {
                var user = userService.Create(new RegisterUserAccountModel
                {
                    Name = "Administrator",
                    Email = masterUserEmail,
                    Password = masterUserPassword,
                    Role = UserRole.Administrator,
                    Description = "Я здесь артами командую!",
                })
                .GetAwaiter()
                .GetResult(); // TODO: Тут мне результат и не нужен...

                //userService.SetUserRoles(user.Id, Infrastructure.User.UserRole.Administrator).GetAwaiter().GetResult();
            }
        //}
    }

    public static void Execute(IServiceProvider serviceProvider, bool addDemoData, bool addAdmin = true)
    {
        using var scope = ServiceScope(serviceProvider);
        ArgumentNullException.ThrowIfNull(scope);

        if (addAdmin)
        {
            ConfigureAdministrator(scope);
        }

        if (addDemoData)
        {
            ConfigureDemoData(scope);
        }
    }

    private static void ConfigureDemoData(IServiceScope scope)
    {
        AddDemoData(scope);
    }

    private static void AddDemoData(IServiceScope scope)
    {
        // Асинхронность тут не работает.
        using var context = DbContext(scope.ServiceProvider);

        var userService = scope.ServiceProvider.GetService<IUserService>();
        ArgumentNullException.ThrowIfNull(userService);

        // TODO: Доделать инициализацию БД
        if (context.Messages.Any() /*|| context.Users.Any()*/ || context.Images.Any() || context.PriceListItems.Any() || context.WorkExampleItems.Any() || context.Orders.Any() || context.Chats.Any())
            return;

        var i = new Image()
        {
            Link = "LocalData\\Images\\PHGAvatarImage.jpg",
            User = null,
            WorkExampleItem = null,
            Order = null
        };
        context.Images.Add(i);
        context.SaveChanges();

        var ua1 = userService.Create(new RegisterUserAccountModel
        {
            Name = "P.H.G.",
            Email = "PHG@dsr.com",
            Password = "pumpheadguy",
            AvatarId = context.Images.First(x => x.Link == i.Link).Id,
            Role = UserRole.Artist,
            Description = "Если кратко — я полуподвальный дровер, который шакалит арты и мангу.\n@pumpheadguy",
        })
        .GetAwaiter()
        .GetResult();
        

        //var u1 = new User()
        //{
        //    Nickname = "P.H.G.",
        //    Avatar = i,
        //    Role = UserRole.Artist,
        //    Description = "Если кратко — я полуподвальный дровер, который шакалит арты и мангу.\n@pumpheadguy"
        //};
        //context.Users.Add(u1);

        var ua2 = userService.Create(new RegisterUserAccountModel
        {
            Name = "Damndelion",
            Email = "Damndelion@dsr.com",
            Password = "password",
            Role = UserRole.Customer,
        })
        .GetAwaiter()
        .GetResult();

        //var u2 = new User()
        //{
        //    Nickname = "Damndelion",
        //    Role = UserRole.Customer
        //};
        //context.Users.Add(u2);






        //var u1 = context.Users.First(u => u.Email == ua1.Email);
        //var u2 = context.Users.First(u => u.Email == ua2.Email);

        var c1 = new Chat()
        {
            CustomerId = ua2.Id,
            ArtistId = ua1.Id
        };
        context.Chats.Add(c1);

        var o1 = new Order()
        {
            Name = "Sans",
            CustomerId = ua2.Id,
            ArtistId = ua1.Id,
            Status = OrderStatus.AtWork,
            //Chat = c1,
            EditsNumber = 0,
            Description = "Санс стоит в Пустоте и заряжает Гастер бластер, который объят синим пламенем"
        };
        context.Orders.Add(o1);

        c1.Order = o1;








        //var c1 = new Category()
        //{
        //    Title = "Classic"
        //};
        //context.Categories.Add(c1);

        //context.Books.Add(new Book()
        //{
        //    Title = "Tom Soyer",
        //    Description = "description description description description ",
        //    Author = a1,
        //    Categories = new List<Category>() { c1 },
        //});

        //context.Books.Add(new Book()
        //{
        //    Title = "War and peace",
        //    Description = "description description description description ",
        //    Author = a2,
        //    Categories = new List<Category>() { c1 },
        //});

        context.SaveChanges();
    }
}
