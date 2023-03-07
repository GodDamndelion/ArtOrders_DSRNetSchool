namespace ArtOrders.Context.Entities;

internal class WorkExampleItem : BaseEntity
{
    public int UserId { get; set; }
    public virtual User User { get; set; }

    public int ImageId { get; set; }
    public virtual Image Image { get; set; }
}
