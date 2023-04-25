namespace ArtOrders.Services.Chats;

using AutoMapper;
using ArtOrders.Context.Entities;
using FluentValidation;

public class ChatModel
{
    public int? OrderId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ArtistId { get; set; }
}

public class ChatModelValidator : AbstractValidator<ChatModel>
{
    public ChatModelValidator() { }
}

public class ChatModelProfile : Profile
{
    public ChatModelProfile()
    {
        CreateMap<ChatModel, Chat>();
        CreateMap<Chat, ChatModel>();
    }
}
