using Azure.Messaging.ServiceBus;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Services;

namespace OrderManagement.Infra.Services.ServiceBus
{
    internal class UpdateOrderStatusQueue : IUpdateOrderStatusQueue
    {
        private readonly ServiceBusSender _serviceBusSender;
        public UpdateOrderStatusQueue(ServiceBusSender serviceBusSender)
        {
            _serviceBusSender = serviceBusSender;
        }
        public async Task SendMessage(Order order)
        {
            await _serviceBusSender.SendMessageAsync(
                new ServiceBusMessage(order.Id.ToString())
                );

        }
    }
}
