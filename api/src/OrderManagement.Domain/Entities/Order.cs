using OrderManagement.Domain.Enums;

namespace OrderManagement.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Customer { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public OrderStatusType OrderStatus { get; set; } = OrderStatusType.PENDING;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
