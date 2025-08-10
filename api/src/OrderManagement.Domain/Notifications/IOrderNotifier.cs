using OrderManagement.Domain.Enums;

namespace OrderManagement.Domain.Notifications
{
    public interface IOrderNotifier
    {
        Task Notify(Guid id, OrderStatusType status);
    }
}
