namespace OrderManagement.Communication.Requests
{
    public class RequestOrderJson
    {
        public string Customer { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public decimal Value { get; set; }
    }
}
