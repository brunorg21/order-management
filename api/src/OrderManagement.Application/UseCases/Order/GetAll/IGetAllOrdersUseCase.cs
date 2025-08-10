using OrderManagement.Communication.Responses;

namespace OrderManagement.Application.UseCases.Order.GetAll
{
    public interface IGetAllOrdersUseCase
    {
        Task<ResponseOrderListJson> Execute();
    }
}
