namespace ArtOrders.API.Controllers.Models;

using AutoMapper;
using ArtOrders.Services.Messages;

public class MessageResponse
{
    public int Id { get; set; }
    public int ChatId { get; set; }
    public Guid? UserId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string? ImageLink { get; set; }
    public DateTime Date { get; set; }
}

public class MessageResponseProfile : Profile
{
    public MessageResponseProfile()
    {
        CreateMap<MessageModel, MessageResponse>();
    }
}
