namespace ArtOrders.Context.Entities;

public class Order : BaseEntity
{
    public string Name { get; set; }

    public Guid? CustomerId { get; set; }
    public virtual User? Customer { get; set; }

    public Guid? ArtistId { get; set; }
    public virtual User? Artist { get; set; }

    public OrderStatus Status { get; set; }

    public int? CurrentResultId { get; set; }
    public virtual Image? CurrentResultImage { get; set; }

    public int EditsNumber { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }

    public virtual Chat Chat { get; set; }
}
