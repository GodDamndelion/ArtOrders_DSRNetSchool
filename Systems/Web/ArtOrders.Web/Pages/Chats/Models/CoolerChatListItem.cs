namespace ArtOrders.Web;

public class CoolerChatListItem
{
    public int Id { get; set; }
    public int? OrderId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ArtistId { get; set; }
    public string Name { get; set; } = string.Empty;
    public UserListItem User { get; set; }
}
