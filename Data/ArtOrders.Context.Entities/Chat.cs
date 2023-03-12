namespace ArtOrders.Context.Entities;

public class Chat : BaseEntity
{
    public int? OrderId { get; set; }
    public virtual Order Order { get; set; }

    public int CustomerId { get; set; }
    public virtual User Customer { get; set; }

    public int ArtistId { get; set; }
    public virtual User Artist { get; set; }

    public virtual ICollection<Message> Messages { get; set; }
}
