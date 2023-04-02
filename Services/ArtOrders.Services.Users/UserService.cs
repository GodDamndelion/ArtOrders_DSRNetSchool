namespace ArtOrders.Services.Users;

using AutoMapper;
using ArtOrders.Common.Exceptions;
using ArtOrders.Common.Validator;
using ArtOrders.Context.Entities;
//using ArtOrders.Services.Actions;
//using ArtOrders.Services.EmailSender;
using Microsoft.AspNetCore.Identity;

// TODO: Доделать действия и почту

public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    //private readonly IAction action;
    private readonly IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator; // Validator в Сервисе без Контроллера работать не хочет.

    public UserService(
        IMapper mapper,
        UserManager<User> userManager,
        //IAction action,
        IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator)
    {
        this.mapper = mapper;
        this.userManager = userManager;
        //this.action = action;
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
            //Status = UserStatus.Active,
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

        //await action.SendEmail(new EmailModel
        //{
        //    Email = model.Email,
        //    Subject = "ArtOrders notification",
        //    Message = "You are registered"
        //});

        // Returning the created user
        return mapper.Map<UserAccountModel>(user);
    }
}
