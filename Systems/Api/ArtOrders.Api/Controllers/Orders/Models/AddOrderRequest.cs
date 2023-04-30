namespace ArtOrders.API.Controllers.Models;

using AutoMapper;
using ArtOrders.Services.Orders;
using FluentValidation;

public class AddOrderRequest
{
    public string Name { get; set; }
    public Guid? CustomerId { get; set; } //Заполняется автоматически
    public Guid? ArtistId { get; set; } //Заполняется автоматически
    public string Description { get; set; }
}

public class AddOrderRequestValidator : AbstractValidator<AddOrderRequest>
{
    public AddOrderRequestValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty().WithMessage("Order name is required.");

        RuleFor(o => o.Description)
            .NotEmpty().WithMessage("Order description is required.");
    }
}

public class AddOrderRequestProfile : Profile
{
    public AddOrderRequestProfile()
    {
        CreateMap<AddOrderRequest, AddOrderModel>();
    }
}
