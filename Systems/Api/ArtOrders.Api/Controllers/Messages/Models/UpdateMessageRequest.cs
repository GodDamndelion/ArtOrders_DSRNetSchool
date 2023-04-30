namespace ArtOrders.API.Controllers.Models;

using AutoMapper;
using ArtOrders.Services.Messages;
using FluentValidation;
using ArtOrders.Context.Entities;

public class UpdateMessageRequest
{
    public string Text { get; set; } = string.Empty;
    public int? ImageId { get; set; }
}

public class UpdateMessageRequestValidator : AbstractValidator<UpdateMessageRequest>
{
    public UpdateMessageRequestValidator()
    {
        RuleFor(c => c.Text)
            .NotNull().WithMessage("Text is required.")
            .NotEmpty().WithMessage("Text is required.");
    }
}

public class UpdateMessageRequestProfile : Profile
{
    public UpdateMessageRequestProfile()
    {
        CreateMap<UpdateMessageRequest, UpdateMessageModel>();
    }
}
