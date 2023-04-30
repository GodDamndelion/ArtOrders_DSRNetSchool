namespace ArtOrders.Services.Chats;

using AutoMapper;
using ArtOrders.Context.Entities;
using FluentValidation;

public class ChatModel
{
    public int Id { get; set; }
    public int? OrderId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ArtistId { get; set; }
    public string Name { get; set; }
}

public class ChatModelProfile : Profile
{
    public ChatModelProfile()
    {
        CreateMap<Chat, ChatModel>();
    }
}
