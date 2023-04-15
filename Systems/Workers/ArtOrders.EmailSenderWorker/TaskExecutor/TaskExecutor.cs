namespace ArtOrders.EmailSenderWorker;

using ArtOrders.Consts;
using ArtOrders.Services.EmailSender;
using ArtOrders.Services.RabbitMq;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

public class TaskExecutor : ITaskExecutor
{
    private readonly ILogger<TaskExecutor> logger;
    private readonly IServiceProvider serviceProvider;
    private readonly IRabbitMq rabbitMq;

    public TaskExecutor(
        ILogger<TaskExecutor> logger,
        IServiceProvider serviceProvider,
        IRabbitMq rabbitMq
    )
    {
        this.logger = logger;
        this.serviceProvider = serviceProvider;
        this.rabbitMq = rabbitMq;
    }

    private async Task Execute<T>(Func<T, Task> action)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();

            var service = scope.ServiceProvider.GetService<T>();
            if (service != null)
                await action(service);
            else
                logger.LogError($"Error: {action} wasn`t resolved");
        }
        catch (Exception e)
        {
            logger.LogError($"Error: {RabbitMqTaskQueueNames.SEND_EMAIL}: {e.Message}");
            throw;
        }
    }

    public void Start()
    {
        // Работает без маппера! (Следить за соответствием EmailModel и SendEmailTaskModel)
        rabbitMq.Subscribe<EmailModel>(RabbitMqTaskQueueNames.SEND_EMAIL, async data
            => await Execute<IEmailSender>(async service =>
            {
                logger.LogDebug($"RABBITMQ::: {RabbitMqTaskQueueNames.SEND_EMAIL}: {data.Email} {data.Message}");
                await service.Send(data);
            }));
    }
}