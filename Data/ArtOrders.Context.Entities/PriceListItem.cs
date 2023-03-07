namespace ArtOrders.Context.Entities;

internal class PriceListItem : BaseEntity
{
    public int? UserId { get; set; }
    public virtual User User { get; set; }

    public string Name { get; set; }
    public int Price { get; set; }
}
