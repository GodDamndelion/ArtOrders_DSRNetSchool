namespace ArtOrders.Services.Users;

using AutoMapper;
using ArtOrders.Context.Entities;

// TODO: Переделать UserAccountModel (сделано?)

public class UserAccountModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int? AvatarId { get; set; }
    public UserRole Role { get; set; }
    public string? Description { get; set; }
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

        CreateMap<UserAccountModel, User>()
            .ForMember(s => s.Nickname, o => o.MapFrom(d => d.Name));
    }
}
