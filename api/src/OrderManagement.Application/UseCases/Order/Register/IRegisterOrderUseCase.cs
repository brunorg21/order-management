using OrderManagement.Communication.Requests;
using OrderManagement.Communication.Responses;

namespace OrderManagement.Application.UseCases.Order.Register
{
    public interface IRegisterOrderUseCase
    {
        Task<ResponseOrderJson> Execute(RequestOrderJson request);
    }
}
