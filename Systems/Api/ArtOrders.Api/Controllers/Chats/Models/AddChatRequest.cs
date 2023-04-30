namespace ArtOrders.Api.Controllers.Models;

using AutoMapper;
using ArtOrders.Services.Chats;
using FluentValidation;

public class AddChatRequest
{
    public int? OrderId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ArtistId { get; set; }
    public string Name { get; set; }
}

public class AddChatRequestValidator : AbstractValidator<AddChatRequest>
{
    public AddChatRequestValidator() { }
}

public class AddChatRequestProfile : Profile
{
    public AddChatRequestProfile()
    {
        CreateMap<AddChatRequest, AddChatModel>();
    }
}
