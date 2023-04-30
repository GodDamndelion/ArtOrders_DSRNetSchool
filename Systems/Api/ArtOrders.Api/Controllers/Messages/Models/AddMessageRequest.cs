namespace ArtOrders.API.Controllers.Models;

using AutoMapper;
using ArtOrders.Services.Messages;
using FluentValidation;

public class AddMessageRequest
{
    public int ChatId { get; set; }
    public string Text { get; set; } = string.Empty;
    public int? ImageId { get; set; }
}

public class AddMessageRequestValidator : AbstractValidator<AddMessageRequest>
{
    public AddMessageRequestValidator()
    {
        RuleFor(c => c.ChatId)
            .NotNull().WithMessage("Chat is required.");

        RuleFor(c => c.Text)
            .NotNull().WithMessage("Text is required.")
            .NotEmpty().WithMessage("Text is required.");
    }
}

public class AddMessageRequestProfile : Profile
{
    public AddMessageRequestProfile()
    {
        CreateMap<AddMessageRequest, AddMessageModel>();
    }
}
