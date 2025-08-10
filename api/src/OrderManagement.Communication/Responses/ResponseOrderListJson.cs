namespace OrderManagement.Communication.Responses
{
    public class ResponseOrderListJson
    {
        public List<ResponseOrderJson> Orders { get; set; } = new List<ResponseOrderJson>();    
    }
}
