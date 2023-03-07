namespace ArtOrders.Context.Entities;

internal class Order : BaseEntity
{
    public string Name { get; set; }

    public int CustomerId { get; set; }
    public virtual User Customer { get; set; }

    public int ArtistId { get; set; }
    public virtual User Artist { get; set; }

    public OrderStatus Status { get; set; }
    public string ChatLink { get; set; }

    public int? CurrentResultId { get; set; }
    public virtual Image CurrentResultImage { get; set; }

    public int EditsNumber { get; set; }

    public virtual Chat Chat { get; set; }
}
