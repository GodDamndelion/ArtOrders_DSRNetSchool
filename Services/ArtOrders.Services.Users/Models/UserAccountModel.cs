﻿namespace ArtOrders.Services.Users;

using AutoMapper;
using ArtOrders.Context.Entities;

// TODO: Переделать UserAccountModel

public class UserAccountModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class UserAccountModelProfile : Profile
{
    public UserAccountModelProfile()
    {
        CreateMap<User, UserAccountModel>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Nickname))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
            ;
    }
}
