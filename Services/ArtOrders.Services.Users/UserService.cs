﻿namespace ArtOrders.Services.Users;

using AutoMapper;
using ArtOrders.Common.Exceptions;
using ArtOrders.Common.Validator;
using ArtOrders.Context.Entities;
using ArtOrders.Services.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly ITaskService taskService;
    private readonly IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator; // Validator в Сервисе без Контроллера работать не хочет.

    public UserService(
        IMapper mapper,
        UserManager<User> userManager,
        ITaskService taskService,
        IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator)
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.taskService = taskService;
        this.registerUserAccountModelValidator = registerUserAccountModelValidator;
    }

    public async Task<UserAccountModel> Create(RegisterUserAccountModel model)
    {
        registerUserAccountModelValidator.Check(model);

        // Find user by email
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user != null)
            throw new ProcessException($"User account with email {model.Email} already exist.");

        // Create user account
        user = new User()
        {
            Nickname = model.Name,
            UserName = model.Email,  // Это логин. Регистрация и вход будут осуществляться через email
            Email = model.Email,
            EmailConfirmed = true, // Так как это учебный проект, то сразу считаем, что почта подтверждена. В реальном проекте, скорее всего, надо будет ее подтвердить через ссылку в письме
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
            AvatarId = model.AvatarId,
            Role = model.Role,
            Description = model.Description,
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new ProcessException($"Creating user account is wrong. {String.Join(", ", result.Errors.Select(s => s.Description))}");

        await taskService.SendEmail(new SendEmailTaskModel
        {
            Email = model.Email,
            Subject = "ArtOrders notification",
            Message = "You are registered"
        });

        // Returning the created user
        return mapper.Map<UserAccountModel>(user);
    }

    public async Task<IEnumerable<UserAccountModel>> GetArtists(int offset = 0, int limit = 10)
    {
        var users = userManager
                    .Users
                    .Include(u => u.Avatar)
                    .Where(user => user.Role == UserRole.Artist)
                    .Skip(Math.Max(offset, 0))
                    .Take(Math.Max(0, Math.Min(limit, 1000)));

        var data = (await users.ToListAsync()).Select(user => mapper.Map<UserAccountModel>(user));

        return data;
    }

    public async Task<UserAccountModel> GetUser(Guid id)
    {
        var user = await userManager.Users.Include(u => u.Avatar).FirstOrDefaultAsync(u => u.Id.Equals(id));

        var data = mapper.Map<UserAccountModel>(user);

        return data;
    }
}
