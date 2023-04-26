using ArtOrders.Context.Entities;

namespace ArtOrders.Web;

public class OrderListItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ArtistId { get; set; }
    public string Status { get; set; } //В Json идёт string от enum!!!
    public int ChatId { get; set; }
    public string? CurrentResultImageLink { get; set; }
    public int EditsNumber { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
}
