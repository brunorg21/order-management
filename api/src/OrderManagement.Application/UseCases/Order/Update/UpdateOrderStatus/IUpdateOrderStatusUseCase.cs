using OrderManagement.Communication.Enums;

namespace OrderManagement.Application.UseCases.Order.Update.UpdateOrderStatus
{
    public interface IUpdateOrderStatusUseCase
    {
        Task Execute(Guid id, OrderStatusType orderStatusType);
    }
}
