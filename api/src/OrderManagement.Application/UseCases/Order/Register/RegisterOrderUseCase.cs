using AutoMapper;
using OrderManagement.Communication.Requests;
using OrderManagement.Communication.Responses;
using OrderManagement.Domain.Repositories;
using OrderManagement.Domain.Services;
using OrderManagement.Exception;

namespace OrderManagement.Application.UseCases.Order.Register
{
    internal class RegisterOrderUseCase : IRegisterOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUpdateOrderStatusQueue _queue;
        public RegisterOrderUseCase(
            IUnitOfWork unitOfWork, 
            IOrderRepository orderRepository, 
            IMapper mapper, 
            IUpdateOrderStatusQueue queue)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _queue = queue;
        }
        public async Task<ResponseOrderJson> Execute(RequestOrderJson request)
        {
            Validate(request);

            var entity = _mapper.Map<Domain.Entities.Order>(request);

            var order = await _orderRepository.Add(entity);

            await _unitOfWork.Commit();

            await _queue.SendMessage(order);

            var orderResponse = _mapper.Map<ResponseOrderJson>(order);

            return orderResponse;
        }

        private void Validate(RequestOrderJson request)
        {
            var validator = new OrderValidator();

            var result = validator.Validate(request);

            if(!result.IsValid)
            {
                List<string> errors = result.Errors.Select(error => error.ErrorMessage)
                    .ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
