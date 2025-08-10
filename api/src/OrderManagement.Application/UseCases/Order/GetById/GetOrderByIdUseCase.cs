using AutoMapper;
using OrderManagement.Communication.Responses;
using OrderManagement.Domain.Repositories;
using OrderManagement.Exception;

namespace OrderManagement.Application.UseCases.Order.GetById
{
    internal class GetOrderByIdUseCase : IGetOrderByIdUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetOrderByIdUseCase(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<ResponseOrderJson> Execute(Guid id)
        {
            var order = await _orderRepository.GetById(id);

            if(order is null)
            {
                throw new NotFoundException("Pedido não encontrado.");
            }

            var orderResponse = _mapper.Map<ResponseOrderJson>(order);

            return orderResponse;
        }
    }
}
