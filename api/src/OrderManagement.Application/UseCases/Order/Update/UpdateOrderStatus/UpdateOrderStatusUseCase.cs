using OrderManagement.Communication.Enums;
using OrderManagement.Domain.Notifications;
using OrderManagement.Domain.Repositories;
using OrderManagement.Exception;

namespace OrderManagement.Application.UseCases.Order.Update.UpdateOrderStatus
{
    internal class UpdateOrderStatusUseCase : IUpdateOrderStatusUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderNotifier _orderNotifier;
        public UpdateOrderStatusUseCase(
            IOrderRepository orderRepository, 
            IUnitOfWork unitOfWork,
            IOrderNotifier orderNotifier)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _orderNotifier = orderNotifier;
        }
        public async Task Execute(Guid id, OrderStatusType orderStatusType)
        {
            var order = await _orderRepository.GetById(id, false);

            if(order is null)
            {
                throw new NotFoundException("Pedido não encontrado.");
            }

            order.OrderStatus = (Domain.Enums.OrderStatusType)orderStatusType;

            _orderRepository.Update(order);

            await _unitOfWork.Commit();

            await _orderNotifier.Notify(order.Id, (Domain.Enums.OrderStatusType)orderStatusType);
        }
    }
}
