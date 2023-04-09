namespace ArtOrders.Services.Tasks;

using ArtOrders.Consts;
using ArtOrders.Services.RabbitMq;
using System.Threading.Tasks;

public class TaskService : ITaskService
{
    private readonly IRabbitMq rabbitMq;

    public TaskService(IRabbitMq rabbitMq)
    {
        this.rabbitMq = rabbitMq;
    }

    public async Task SendEmail(SendEmailTaskModel email)
    {
        await rabbitMq.PushAsync(RabbitMqTaskQueueNames.SEND_EMAIL, email);
    }
}
