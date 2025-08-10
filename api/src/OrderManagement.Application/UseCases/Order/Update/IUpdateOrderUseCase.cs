using OrderManagement.Communication.Requests;
using OrderManagement.Communication.Responses;

namespace OrderManagement.Application.UseCases.Order.Update
{
    public interface IUpdateOrderUseCase
    {
        Task<ResponseOrderJson> Execute(RequestOrderJson request, Guid id);
    }
}
