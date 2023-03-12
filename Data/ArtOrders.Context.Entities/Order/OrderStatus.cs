namespace ArtOrders.Context.Entities;

public enum OrderStatus
{
    Pending = 0,                        // На рассмотрении
    WaitingForPrepayment = 1,           // Ждёт предоплаты
    AtWork = 2,                         // В работе
    ReadyAndWaitingForPayment = 3,      // Готов и ждёт оплаты
    Completed = 4,                      // Завершён
    Cencelled = 5,                      // Отменён
    OnEditing = 6,                      // В правке
    ReadyAndWaitingForConfirmation = 7  // Готов и ждёт подтверждения (После оплаты и/или после правки для перехода в Completed или OnEditing)
}
