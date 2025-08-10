using OrderManagement.Domain.Enums;

namespace OrderManagement.Domain.Extensions
{
    public static class OrderStatusTypeExtension
    {
        public static string OrderStatusToString(this OrderStatusType status)
        {
            return status switch
            {
                OrderStatusType.PENDING => "Pendente",
                OrderStatusType.PROCESSING => "Processando",
                OrderStatusType.COMPLETED => "Finalizado",
                _ => string.Empty
            };
        }
    }
}
