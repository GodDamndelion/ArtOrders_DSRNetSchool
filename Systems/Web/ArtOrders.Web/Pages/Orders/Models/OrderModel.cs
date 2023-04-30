namespace ArtOrders.Web;

using ArtOrders.Context.Entities;
using FluentValidation;

public class OrderModel
{
    public int? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? CustomerId { get; set; }
    public Guid? ArtistId { get; set; }
    public OrderStatus Status { get; set; }
    public int ChatId { get; set; }
    public string? CurrentResultImageLink { get; set; }
    public int EditsNumber { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}

public class OrderModelValidator : AbstractValidator<OrderModel>
{
    public OrderModelValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty().WithMessage("Order name is required")
            ;

        RuleFor(o => o.Description)
            .NotEmpty().WithMessage("Description is required")
            ;
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<OrderModel>.CreateWithOptions((OrderModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
