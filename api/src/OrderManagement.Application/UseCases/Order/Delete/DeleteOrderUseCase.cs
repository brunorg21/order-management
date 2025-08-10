using OrderManagement.Domain.Repositories;
using OrderManagement.Exception;

namespace OrderManagement.Application.UseCases.Order.Delete
{
    internal class DeleteOrderUseCase : IDeleteOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteOrderUseCase(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Execute(Guid id)
        {
            var order = await _orderRepository.GetById(id, false);

            if(order is null)
            {
                throw new NotFoundException("Pedido não encontrado.");
            }
            
            _orderRepository.Delete(order);

            await _unitOfWork.Commit();

        }
    }
}
