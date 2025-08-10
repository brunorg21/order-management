using Azure.Messaging.ServiceBus;

namespace OrderManagement.Infra.Services.ServiceBus
{
    public class UpdateOrderStatusProcessor
    {
        private readonly ServiceBusProcessor _serviceBusProcessor;
        public UpdateOrderStatusProcessor(ServiceBusProcessor serviceBusProcessor)
        {
            _serviceBusProcessor = serviceBusProcessor;
        }

        public ServiceBusProcessor GetServiceBusProcessor()
        {
            return _serviceBusProcessor;
        }
    }
}
