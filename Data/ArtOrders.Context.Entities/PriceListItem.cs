namespace ArtOrders.Context.Entities;

public class PriceListItem : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public string Name { get; set; }
    public int Price { get; set; }
}
