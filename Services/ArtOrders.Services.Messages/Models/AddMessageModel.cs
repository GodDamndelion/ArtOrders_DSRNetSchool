namespace ArtOrders.Services.Messages;

using AutoMapper;
using ArtOrders.Context.Entities;
using FluentValidation;

public class AddMessageModel
{
    public int ChatId { get; set; }
    public Guid? UserId { get; set; }
    public string Text { get; set; } = string.Empty;
    public int? ImageId { get; set; }
    public DateTime Date { get; set; }
}

public class AddMessageModelValidator : AbstractValidator<AddMessageModel>
{
    public AddMessageModelValidator()
    {
        RuleFor(c => c.ChatId)
            .NotNull().WithMessage("Chat is required.");

        RuleFor(c => c.Date)
            .NotNull().WithMessage("Date is required.");

        RuleFor(c => c.Text)
            .NotNull().WithMessage("Text is required.")
            .NotEmpty().WithMessage("Text is required.");
    }
}

public class AddMessageModelProfile : Profile
{
    public AddMessageModelProfile()
    {
        CreateMap<AddMessageModel, Message>();
    }
}
