namespace ArtOrders.API.Controllers.Models;

using AutoMapper;
using ArtOrders.Services.Users;
using ArtOrders.Context.Entities;

// TODO: Переделать UserAccountResponse (сделано?)

public class UserAccountResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int? AvatarId { get; set; }
    public UserRole Role { get; set; }
    public string? Description { get; set; }
}

public class UserAccountResponseProfile : Profile
{
    public UserAccountResponseProfile()
    {
        CreateMap<UserAccountModel, UserAccountResponse>();
    }
}
