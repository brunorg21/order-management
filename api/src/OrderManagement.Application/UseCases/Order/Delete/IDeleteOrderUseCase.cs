namespace OrderManagement.Application.UseCases.Order.Delete
{
    public interface IDeleteOrderUseCase
    {
        Task Execute(Guid id);
    }
}
