using FluentValidation;

namespace ArtOrders.Web;

public class ChatModel
{
    public int? OrderId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ArtistId { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class ChatModelValidator : AbstractValidator<ChatModel>
{
    public ChatModelValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Chat name is required.");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<ChatModel>.CreateWithOptions((ChatModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}