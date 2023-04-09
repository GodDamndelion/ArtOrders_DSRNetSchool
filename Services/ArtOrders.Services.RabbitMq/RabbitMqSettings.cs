namespace ArtOrders.Services.RabbitMq;

public class RabbitMqSettings
{
    public string Uri { get; private set; } // В Uri можно прописывать UserName и Password, но здесь они выделены отдельно для удобства
    public string UserName { get; private set; }
    public string Password { get; private set; }
}
