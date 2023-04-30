namespace ArtOrders.Services.Chats;

using AutoMapper;
using ArtOrders.Context.Entities;
using FluentValidation;

public class UpdateChatModel
{
    public int? OrderId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ArtistId { get; set; }
    public string Name { get; set; }
}

public class UpdateChatModelValidator : AbstractValidator<UpdateChatModel>
{
    public UpdateChatModelValidator() { }
}

public class UpdateChatModelProfile : Profile
{
    public UpdateChatModelProfile()
    {
        CreateMap<UpdateChatModel, Chat>();
    }
}
