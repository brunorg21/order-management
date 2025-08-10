using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> Add(Order order);
        Task<Order?> GetById(Guid id, bool withAsNoTracking = true);
        void Update(Order order);
        void Delete(Order order);   
        Task<List<Order>> GetAll();
    }
}
