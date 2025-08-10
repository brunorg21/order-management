using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Services
{
    public interface IUpdateOrderStatusQueue
    {
        Task SendMessage(Order order);
    }
}
