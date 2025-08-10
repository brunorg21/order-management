namespace OrderManagement.Exception
{
    public abstract class OrderManagementException : SystemException
    {
        protected OrderManagementException(string message) : base(message) { }
        public abstract int StatusCode { get; }

        public abstract List<string> GetErrors();
    }
}
