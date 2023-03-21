namespace ArtOrders.API.Controllers.Models;

using AutoMapper;
using ArtOrders.Services.Orders;
using FluentValidation;
using ArtOrders.Context.Entities;

public class UpdateOrderRequest
{
    public string Name { get; set; }
    public OrderStatus Status { get; set; }
    // TODO: Вынести в отдельный Update!
    //public string? CurrentResultImageLink { get; set; }
    public int EditsNumber { get; set; }
    public string Description { get; set; }
}

public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
{
    public UpdateOrderRequestValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty().WithMessage("Order name is required.");

        RuleFor(o => o.Description)
            .NotEmpty().WithMessage("Order description is required.");
    }
}

public class UpdateOrderRequestProfile : Profile
{
    public UpdateOrderRequestProfile()
    {
        CreateMap<UpdateOrderRequest, UpdateOrderModel>();
    }
}
