namespace ArtOrders.API.Controllers.Models;

using AutoMapper;
using ArtOrders.Services.Orders;
using ArtOrders.Context.Entities;

public class OrderResponse
{
    /// <summary>
    /// Order Id
    /// </summary>
    public int Id { get; set; }
    public string Name { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ArtistId { get; set; }
    public OrderStatus Status { get; set; }
    public string ChatLink { get; set; }
    public string? CurrentResultImageLink { get; set; }
    public int EditsNumber { get; set; }
    public string Description { get; set; }
}

public class OrderResponseProfile : Profile
{
    public OrderResponseProfile()
    {
        CreateMap<OrderModel, OrderResponse>();
    }
}
