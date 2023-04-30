namespace ArtOrders.Api.Controllers.Models;

using AutoMapper;
using ArtOrders.Services.Chats;
using FluentValidation;

public class UpdateChatRequest
{
    public int? OrderId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ArtistId { get; set; }
    public string Name { get; set; }
}

public class UpdateChatRequestValidator : AbstractValidator<UpdateChatRequest>
{
    public UpdateChatRequestValidator() { }
}

public class UpdateChatRequestProfile : Profile
{
    public UpdateChatRequestProfile()
    {
        CreateMap<UpdateChatRequest, UpdateChatModel>();
    }
}
