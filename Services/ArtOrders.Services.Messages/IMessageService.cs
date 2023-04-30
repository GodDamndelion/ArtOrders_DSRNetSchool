namespace ArtOrders.Services.Messages;

public interface IMessageService
{
    Task<IEnumerable<MessageModel>> GetMessages(int offset = 0, int limit = 100);
    Task<MessageModel> GetMessage(int messageId);
    Task<MessageModel> AddMessage(AddMessageModel model);
    Task UpdateMessage(int id, UpdateMessageModel model);
    Task DeleteMessage(int messageId);
}
