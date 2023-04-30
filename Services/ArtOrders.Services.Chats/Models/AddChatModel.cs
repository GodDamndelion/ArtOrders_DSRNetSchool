namespace ArtOrders.Services.Chats;

using AutoMapper;
using ArtOrders.Context.Entities;
using FluentValidation;

public class AddChatModel
{
    public int? OrderId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ArtistId { get; set; }
    public string Name { get; set; }
}

public class AddChatModelValidator : AbstractValidator<AddChatModel>
{
    public AddChatModelValidator() { }
}

public class AddChatModelProfile : Profile
{
    public AddChatModelProfile()
    {
        CreateMap<AddChatModel, Chat>();
    }
}
