namespace ArtOrders.Services.Tasks;

using AutoMapper;
using ArtOrders.Services.EmailSender;

public class SendEmailTaskModel
{
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
}

public class SendEmailTaskModelProfile : Profile
{
    public SendEmailTaskModelProfile()
    {
        CreateMap<SendEmailTaskModel, EmailModel>();
    }
}