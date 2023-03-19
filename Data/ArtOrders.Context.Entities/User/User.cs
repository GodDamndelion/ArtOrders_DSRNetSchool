namespace ArtOrders.Context.Entities;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<Guid>
{
    public int? AvatarId { get; set; }
    public virtual Image? Avatar { get; set; }

    public string Nickname { get; set; }
    public UserRole Role { get; set; }
    public string? Description { get; set; }

    public virtual ICollection<Chat> ChatsUserСreated { get; set; }
    public virtual ICollection<Chat> ChatsСreatedWithUser { get; set; }
    public virtual ICollection<Order> OrdersUserOrdered { get; set; }
    public virtual ICollection<Order> OrdersOrderedToUser { get; set; }
    public virtual ICollection<WorkExampleItem> WorkExamples { get; set; }
    public virtual ICollection<PriceListItem> PriceList { get; set; }

    //Вот эти придётся сделать
    public virtual ICollection<Message> Messages { get; set; }
}
