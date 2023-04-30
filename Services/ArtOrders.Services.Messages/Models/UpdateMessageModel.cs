namespace ArtOrders.Services.Messages;

using AutoMapper;
using ArtOrders.Context.Entities;
using FluentValidation;

public class UpdateMessageModel
{
    public string Text { get; set; } = string.Empty;
    public int? ImageId { get; set; }
}

public class UpdateMessageModelValidator : AbstractValidator<UpdateMessageModel>
{
    public UpdateMessageModelValidator()
    {
        RuleFor(c => c.Text)
            .NotNull().WithMessage("Text is required.")
            .NotEmpty().WithMessage("Text is required.");
    }
}

public class UpdateMessageModelProfile : Profile
{
    public UpdateMessageModelProfile()
    {
        CreateMap<UpdateMessageModel, Message>();
    }
}
