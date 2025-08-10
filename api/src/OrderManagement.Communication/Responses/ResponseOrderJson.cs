using OrderManagement.Communication.Enums;

namespace OrderManagement.Communication.Responses
{
    public class ResponseOrderJson
    {
        public Guid Id { get; set; }
        public string Customer { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
