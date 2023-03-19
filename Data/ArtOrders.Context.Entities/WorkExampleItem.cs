using System.Diagnostics.CodeAnalysis;

namespace ArtOrders.Context.Entities;

public class WorkExampleItem : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public int ImageId { get; set; }
    public virtual Image Image { get; set; }

    public string? Description { get; set; } //[AllowNull] Необязательно...
}
