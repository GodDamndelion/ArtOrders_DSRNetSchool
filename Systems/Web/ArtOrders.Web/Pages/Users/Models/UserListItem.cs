namespace ArtOrders.Web;

public class UserListItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int? AvatarId { get; set; }
    public string Role { get; set; } //В Json идёт string от enum!!!
    public string? Description { get; set; }
}
