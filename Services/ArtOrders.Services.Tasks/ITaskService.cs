namespace ArtOrders.Services.Tasks;

using System.Threading.Tasks;

public interface ITaskService
{
    Task SendEmail(SendEmailTaskModel email);
}
