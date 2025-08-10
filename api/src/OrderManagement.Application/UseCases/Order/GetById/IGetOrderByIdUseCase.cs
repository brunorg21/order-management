using OrderManagement.Communication.Responses;

namespace OrderManagement.Application.UseCases.Order.GetById
{
    public interface IGetOrderByIdUseCase
    {
        Task<ResponseOrderJson> Execute(Guid id);
    }
}
