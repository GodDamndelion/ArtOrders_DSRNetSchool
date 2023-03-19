namespace ArtOrders.Context.Entities;

public class Chat : BaseEntity
{
    public int? OrderId { get; set; }
    public virtual Order? Order { get; set; }

    public Guid? CustomerId { get; set; }
    public virtual User? Customer { get; set; }

    public Guid? ArtistId { get; set; }
    public virtual User? Artist { get; set; }

    public virtual ICollection<Message> Messages { get; set; }
}
