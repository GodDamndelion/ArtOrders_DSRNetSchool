namespace ArtOrders.Api.Controllers.Models;

using AutoMapper;
using ArtOrders.Services.Chats;
using FluentValidation;

public class ChatResponse
{
    public int Id { get; set; }
    public int? OrderId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ArtistId { get; set; }
    public string Name { get; set; }
}

public class ChatResponseValidator : AbstractValidator<ChatResponse>
{
    public ChatResponseValidator() { }
}

public class ChatResponseProfile : Profile
{
    public ChatResponseProfile()
    {
        CreateMap<ChatModel, ChatResponse>();
    }
}
