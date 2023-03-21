namespace ArtOrders.Services.Orders;

using AutoMapper;
using ArtOrders.Context.Entities;
using FluentValidation;

public class UpdateOrderModel
{
    public string Name { get; set; }
    public OrderStatus Status { get; set; }
    // TODO: Вынести в отдельный Update!
    //public string? CurrentResultImageLink { get; set; }
    public int EditsNumber { get; set; }
    public string Description { get; set; }
}

public class UpdateOrderModelValidator : AbstractValidator<UpdateOrderModel>
{
    public UpdateOrderModelValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty().WithMessage("Order name is required.");

        RuleFor(o => o.Description)
            .NotEmpty().WithMessage("Order description is required.");
    }
}

public class UpdateOrderModelProfile : Profile
{
    public UpdateOrderModelProfile()
    {
        CreateMap<UpdateOrderModel, Order>();
    }
}
