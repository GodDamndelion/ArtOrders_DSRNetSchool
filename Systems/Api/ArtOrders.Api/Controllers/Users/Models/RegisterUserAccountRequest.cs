﻿namespace ArtOrders.API.Controllers.Models;

using AutoMapper;
using ArtOrders.Services.Users;
using FluentValidation;
using ArtOrders.Context.Entities;

public class RegisterUserAccountRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int? AvatarId { get; set; }
    public UserRole? Role { get; set; } = UserRole.Customer;
    public string? Description { get; set; }
}

public class RegisterUserAccountRequestValidator : AbstractValidator<RegisterUserAccountRequest>
{
    public RegisterUserAccountRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("User name is required.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MaximumLength(50).WithMessage("Password is long.");
    }
}

public class RegisterUserAccountRequestProfile : Profile
{
    public RegisterUserAccountRequestProfile()
    {
        CreateMap<RegisterUserAccountRequest, RegisterUserAccountModel>();
    }
}
