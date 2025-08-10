using AutoMapper;
using OrderManagement.Communication.Responses;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Application.UseCases.Order.GetAll
{
    public class GetAllOrdersUseCase : IGetAllOrdersUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetAllOrdersUseCase(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<ResponseOrderListJson> Execute()
        {
            var orders = await _orderRepository.GetAll();

            return new ResponseOrderListJson { Orders = _mapper.Map<List<ResponseOrderJson>>(orders) };

        }
    }
}
