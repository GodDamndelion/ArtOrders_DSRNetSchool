namespace ArtOrders.Services.Orders;

using AutoMapper;
using ArtOrders.Context.Entities;
using FluentValidation;

public class AddOrderModel
{
    public string Name { get; set; }
    public Guid? CustomerId { get; set; } //Заполняется автоматически
    public Guid? ArtistId { get; set; } //Заполняется автоматически
    public string Description { get; set; }
    public DateTime Date { get; set; }
}

public class AddOrderModelValidator : AbstractValidator<AddOrderModel>
{
    public AddOrderModelValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty().WithMessage("Order name is required.");

        RuleFor(o => o.Description)
            .NotEmpty().WithMessage("Order description is required.");
    }
}

public class AddOrderModelProfile : Profile
{
    public AddOrderModelProfile()
    {
        CreateMap<AddOrderModel, Order>();
    }
}
