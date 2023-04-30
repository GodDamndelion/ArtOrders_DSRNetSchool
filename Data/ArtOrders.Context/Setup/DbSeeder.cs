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
        if (context.Messages.Any() || context.Images.Any() || context.PriceListItems.Any() || context.WorkExampleItems.Any() || context.Orders.Any() || context.Chats.Any())
            return;

        var i = new Image()
        {
            Link = "LocalData/Images/PHGAvatarImage.jpg",
            User = null,
            WorkExampleItem = null,
            Order = null
        };
        context.Images.Add(i);

        var iPHG1 = new Image()
        {
            Link = "LocalData/Images/PHGImage1.jpg",
            User = null,
            WorkExampleItem = null,
            Order = null
        };
        context.Images.Add(iPHG1);

        var iPHG2 = new Image()
        {
            Link = "LocalData/Images/PHGImage2.jpg",
            User = null,
            WorkExampleItem = null,
            Order = null
        };
        context.Images.Add(iPHG2);

        var iPHG3 = new Image()
        {
            Link = "LocalData/Images/PHGImage3.jpg",
            User = null,
            WorkExampleItem = null,
            Order = null
        };
        context.Images.Add(iPHG3);

        var ia1 = new Image()
        {
            Link = @"LocalData/Images/Avatar1.png",
            User = null,
            WorkExampleItem = null,
            Order = null
        };
        context.Images.Add(ia1);

        var ia2 = new Image()
        {
            Link = @"LocalData/Images/Avatar2.png",
            User = null,
            WorkExampleItem = null,
            Order = null
        };
        context.Images.Add(ia2);

        var ia3 = new Image()
        {
            Link = @"LocalData/Images/Avatar3.png",
            User = null,
            WorkExampleItem = null,
            Order = null
        };
        context.Images.Add(ia3);

        var ia4 = new Image()
        {
            Link = @"LocalData/Images/Avatar4.png",
            User = null,
            WorkExampleItem = null,
            Order = null
        };
        context.Images.Add(ia4);

        var ia7 = new Image()
        {
            Link = @"LocalData/Images/Avatar7.png",
            User = null,
            WorkExampleItem = null,
            Order = null
        };
        context.Images.Add(ia7);

        var iamnyam = new Image()
        {
            Link = "LocalData/Images/Лягушка пластиковая Ам-Ням.png",
            User = null,
            WorkExampleItem = null,
            Order = null
        };
        context.Images.Add(iamnyam);
        context.SaveChanges();



        var u1 = userService.Create(new RegisterUserAccountModel
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

        var u2 = userService.Create(new RegisterUserAccountModel
        {
            Name = "Damndelion",
            Email = "Damndelion@dsr.com",
            Password = "password",
            AvatarId = context.Images.First(x => x.Link == iamnyam.Link).Id,
            Role = UserRole.Customer,
        })
        .GetAwaiter()
        .GetResult();

        var ua1 = userService.Create(new RegisterUserAccountModel
        {
            Name = "Artist1",
            Email = "Artist1@dsr.com",
            Password = "Artist1",
            AvatarId = context.Images.First(x => x.Link == ia1.Link).Id,
            Role = UserRole.Artist,
            Description = "I am artist 1",
        })
        .GetAwaiter()
        .GetResult();

        var ua2 = userService.Create(new RegisterUserAccountModel
        {
            Name = "Artist2",
            Email = "Artist2@dsr.com",
            Password = "Artist2",
            AvatarId = context.Images.First(x => x.Link == ia2.Link).Id,
            Role = UserRole.Artist,
            Description = "I am artist 2",
        })
        .GetAwaiter()
        .GetResult();

        var ua3 = userService.Create(new RegisterUserAccountModel
        {
            Name = "Artist3",
            Email = "Artist3@dsr.com",
            Password = "Artist3",
            AvatarId = context.Images.First(x => x.Link == ia3.Link).Id,
            Role = UserRole.Artist,
            Description = "I am artist 3",
        })
        .GetAwaiter()
        .GetResult();

        var ua4 = userService.Create(new RegisterUserAccountModel
        {
            Name = "Artist4",
            Email = "Artist4@dsr.com",
            Password = "Artist4",
            AvatarId = context.Images.First(x => x.Link == ia4.Link).Id,
            Role = UserRole.Artist,
            Description = "I am artist 4",
        })
        .GetAwaiter()
        .GetResult();

        var ua5 = userService.Create(new RegisterUserAccountModel
        {
            Name = "Artist5",
            Email = "Artist5@dsr.com",
            Password = "Artist5",
            Role = UserRole.Artist,
        })
        .GetAwaiter()
        .GetResult();

        var ua6 = userService.Create(new RegisterUserAccountModel
        {
            Name = "Artist6",
            Email = "Artist6@dsr.com",
            Password = "Artist6",
            Role = UserRole.Artist,
        })
        .GetAwaiter()
        .GetResult();

        var ua7 = userService.Create(new RegisterUserAccountModel
        {
            Name = "Artist7",
            Email = "Artist7@dsr.com",
            Password = "Artist7",
            AvatarId = context.Images.First(x => x.Link == ia7.Link).Id,
            Role = UserRole.Artist,
        })
        .GetAwaiter()
        .GetResult();



        var o1 = new Order()
        {
            Name = "Sans",
            CustomerId = u2.Id,
            ArtistId = u1.Id,
            Status = OrderStatus.AtWork,
            //Chat = c1,
            EditsNumber = 0,
            Description = "Санс стоит в Пустоте и заряжает Гастер бластер, который объят синим пламенем",
            Date = DateTime.UtcNow,
        };
        context.Orders.Add(o1);

        var c1 = new Chat()
        {
            CustomerId = u2.Id,
            ArtistId = u1.Id,
            Order = o1,
            Name = o1.Name
        };
        context.Chats.Add(c1);



        var m1c1 = new Message()
        {
            Chat = c1,
            UserId = u2.Id,
            Date = DateTime.UtcNow,
            Text = "Привет, Сева! Я тебя нашёл))) Нарисуй Санса, подажуйста! Крутого такого! Я всё в описании написал!"
        };
        context.Messages.Add(m1c1);

        var m2c1 = new Message()
        {
            Chat = c1,
            UserId = u1.Id,
            Date = DateTime.UtcNow,
            Text = "Паша, ты? Ну привет. Мог бы в вк просто написать, зачем заказ то делать?"
        };
        context.Messages.Add(m2c1);

        var m3c1 = new Message()
        {
            Chat = c1,
            UserId = u2.Id,
            Date = DateTime.UtcNow,
            Text = "Ну так чтобы твои труды были не напрасны)"
        };
        context.Messages.Add(m3c1);

        var m4c1 = new Message()
        {
            Chat = c1,
            UserId = u2.Id,
            Date = DateTime.UtcNow,
            Text = "К тому же хочу проверить, как этот сервис работает..."
        };
        context.Messages.Add(m4c1);

        var m5c1 = new Message()
        {
            Chat = c1,
            UserId = u1.Id,
            Date = DateTime.UtcNow,
            Text = "Ладно..."
        };
        context.Messages.Add(m5c1);

        var m6c1 = new Message()
        {
            Chat = c1,
            UserId = u2.Id,
            Date = DateTime.UtcNow,
            Text = "Ладно"
        };
        context.Messages.Add(m6c1);

        var m7c1 = new Message()
        {
            Chat = c1,
            UserId = u1.Id,
            Date = DateTime.UtcNow,
            Text = "ладно"
        };
        context.Messages.Add(m7c1);

        var m8c1 = new Message()
        {
            Chat = c1,
            UserId = u2.Id,
            Date = DateTime.UtcNow,
            Text = "ладно"
        };
        context.Messages.Add(m8c1);

        var m9c1 = new Message()
        {
            Chat = c1,
            UserId = u1.Id,
            Date = DateTime.UtcNow,
            Text = "Достал..."
        };
        context.Messages.Add(m9c1);

        var m10c1 = new Message()
        {
            Chat = c1,
            UserId = u2.Id,
            Date = DateTime.UtcNow,
            Text = ")"
        };
        context.Messages.Add(m10c1);

        context.SaveChanges();
    }
}
