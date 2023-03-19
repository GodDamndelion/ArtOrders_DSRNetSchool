namespace ArtOrders.Services.Orders;

using AutoMapper;
using ArtOrders.Context.Entities;

public class OrderModel
{
    public string Name { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ArtistId { get; set; }
    public OrderStatus Status { get; set; }
    public string ChatLink { get; set; }
    public int? CurrentResultId { get; set; }
    public int EditsNumber { get; set; }
    public string Description { get; set; }
}

public class OrderModelProfile : Profile
{
    public OrderModelProfile()
    {
        CreateMap<Order, OrderModel>();
    }
}
