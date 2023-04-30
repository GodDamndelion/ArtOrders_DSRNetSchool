namespace ArtOrders.Services.Messages;

using AutoMapper;
using ArtOrders.Context.Entities;

public class MessageModel
{
    public int Id { get; set; }
    public int ChatId { get; set; }
    public Guid? UserId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string? ImageLink { get; set; }
    public DateTime Date { get; set; }
}

public class MessageModelProfile : Profile
{
    public MessageModelProfile()
    {
        CreateMap<Message, MessageModel>()
            .ForMember(mm => mm.ImageLink, e => e.MapFrom(m => (m.Image != null ? m.Image.Link : null)));
    }
}
