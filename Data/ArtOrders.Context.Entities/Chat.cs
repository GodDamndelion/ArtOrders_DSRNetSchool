using System.Diagnostics.CodeAnalysis;

namespace ArtOrders.Context.Entities;

public class Chat : BaseEntity
{
    [AllowNull]
    public int? OrderId { get; set; }
    public virtual Order Order { get; set; }

    [AllowNull] // TODO: В БД всё равно не null?
    public Guid? CustomerId { get; set; }
    public virtual User Customer { get; set; }

    [AllowNull]
    public Guid? ArtistId { get; set; }
    public virtual User Artist { get; set; }

    public virtual ICollection<Message> Messages { get; set; }
}
