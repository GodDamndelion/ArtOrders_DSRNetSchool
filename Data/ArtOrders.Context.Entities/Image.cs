namespace ArtOrders.Context.Entities;

public class Image : BaseEntity
{
    public string Link { get; set; }

    //Вот эти придётся сделать
    public virtual ICollection<Message> Messages { get; set; }
    public virtual User User { get; set; }
    public virtual WorkExampleItem WorkExampleItem { get; set; }
    public virtual Order Order { get; set; }
}
