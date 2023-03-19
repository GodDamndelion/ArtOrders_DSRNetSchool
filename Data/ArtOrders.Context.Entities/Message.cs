using System.Diagnostics.CodeAnalysis;

namespace ArtOrders.Context.Entities;

public class Message : BaseEntity
{
    public int ChatId { get; set; }
    public virtual Chat Chat { get; set; }

    public Guid? UserId { get; set; }
    public virtual User? User { get; set; }

    public string Text { get; set; }

    public int? ImageId { get; set; }
    public virtual Image? Image { get; set; }

    public DateTime Date { get; set; }
}
