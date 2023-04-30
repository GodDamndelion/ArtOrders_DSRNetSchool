namespace ArtOrders.Services.Orders;

using AutoMapper;
using ArtOrders.Context.Entities;

public class OrderModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ArtistId { get; set; }
    public OrderStatus Status { get; set; }
    public int ChatId { get; set; }
    public string? CurrentResultImageLink { get; set; }
    public int EditsNumber { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
}

public class OrderModelProfile : Profile
{
    public OrderModelProfile()
    {
        CreateMap<Order, OrderModel>()
            .ForMember(om => om.ChatId, opt => opt.MapFrom(o => o.Chat.Id))
            .ForMember(om => om.CurrentResultImageLink, opt => opt.MapFrom(o => o.CurrentResultImage.Link));
    }
}
