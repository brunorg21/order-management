using FluentValidation;
using OrderManagement.Communication.Requests;

namespace OrderManagement.Application.UseCases.Order
{
    public class OrderValidator : AbstractValidator<RequestOrderJson>
    {
        public OrderValidator()
        {
            RuleFor(x => x.Value).GreaterThan(0).WithMessage("O valor do pedido deve ser maior que zero.");
        }
    }
}
