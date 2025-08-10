using Microsoft.AspNetCore.SignalR;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Extensions;
using OrderManagement.Domain.Notifications;
using OrderManagement.Infra.Services.Hubs;

namespace OrderManagement.Infra.Notifications.Order
{
    internal class SignalROrderStatusNotifier : IOrderNotifier
    {
        private readonly IHubContext<OrderStatusHub> _hubContext;
        public SignalROrderStatusNotifier(IHubContext<OrderStatusHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task Notify(Guid id, OrderStatusType status)
        {
            await _hubContext.Clients.All.SendAsync("UpdateOrderStatus", new { OrderId = id, NewStatus = status.OrderStatusToString() } );
        }
    }
}
