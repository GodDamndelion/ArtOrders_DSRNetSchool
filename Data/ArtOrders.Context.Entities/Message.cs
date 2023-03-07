namespace ArtOrders.Context.Entities;

internal class Message : BaseEntity
{
    public int ChatId { get; set; }
    public virtual Chat Chat { get; set; }

    public int? UserId { get; set; }
    public virtual User User { get; set; }

    public string Text { get; set; }
    
    public int? ImageId { get; set; }
    public virtual Image Image { get; set; }

    public DateTime Date { get; set; }
}
