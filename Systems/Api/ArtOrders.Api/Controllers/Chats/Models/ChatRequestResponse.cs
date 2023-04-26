namespace ArtOrders.Api.Controllers.Models;

using AutoMapper;
using ArtOrders.Services.Chats;
using FluentValidation;

public class ChatRequestResponse
{
    public int? OrderId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ArtistId { get; set; }
    public string Name { get; set; }
}

public class ChatRequestResponseValidator : AbstractValidator<ChatRequestResponse>
{
    public ChatRequestResponseValidator() { }
}

public class ChatRequestResponseProfile : Profile
{
    public ChatRequestResponseProfile()
    {
        CreateMap<ChatRequestResponse, ChatModel>();
        CreateMap<ChatModel, ChatRequestResponse>();
    }
}
