using AutoMapper;
using OrderManagement.Communication.Requests;
using OrderManagement.Communication.Responses;
using OrderManagement.Domain.Repositories;
using OrderManagement.Exception;

namespace OrderManagement.Application.UseCases.Order.Update
{
    internal class UpdateOrderUseCase : IUpdateOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateOrderUseCase(IUnitOfWork unitOfWork, IOrderRepository orderRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<ResponseOrderJson> Execute(RequestOrderJson request, Guid id)
        {
            Validate(request);

            var order = await _orderRepository.GetById(id, false);

            if(order is null)
            {
                throw new NotFoundException("Pedido não encontrado.");
            }

            order.Value = request.Value;
            order.Customer = request.Customer;
            order.Product = request.Product;

             _orderRepository.Update(order);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponseOrderJson>(order);
        }
        private void Validate(RequestOrderJson request)
        {
            var validator = new OrderValidator();

            var result = validator.Validate(request);

            if(!result.IsValid)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage)
                    .ToList();

                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
