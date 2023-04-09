namespace ArtOrders.Context.Entities;

public enum UserRole
{
    Administrator = 0,
    Customer = 1, // Роли клиента и художника влияют на вид,
    Artist = 2    // но не на запрет действий!
}
