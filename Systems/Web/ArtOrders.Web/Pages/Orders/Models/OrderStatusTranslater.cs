namespace ArtOrders.Web;
public static class OrderStatusTranslater
{
    public static Dictionary<string, string> dictionary = new Dictionary<string, string>()
    {
        {"pending",                         "На рассмотрении" },
        {"waitingForPrepayment",            "Ждёт предоплаты" },
        {"atWork",                          "В работе" },
        {"readyAndWaitingForPayment",       "Готов и ждёт оплаты" },
        {"completed",                       "Завершён" },
        {"cencelled",                       "Отменён" },
        {"onEditing",                       "В правке" },
        {"readyAndWaitingForConfirmation",  "Готов и ждёт подтверждения" }
    };
}
