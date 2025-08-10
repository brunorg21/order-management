using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OrderManagement.Infra.DataAccess;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Repositories;
using OrderManagement.Infra.DataAccess.Repositories;
using OrderManagement.Infra.Services.ServiceBus;
using OrderManagement.Domain.Services;
using Azure.Messaging.ServiceBus;
using OrderManagement.Domain.Notifications;
using OrderManagement.Infra.Notifications.Order;

namespace OrderManagement.Infra
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);
            AddQueue(services, configuration);
            AddSignalR(services);
        }

        public static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");

            services.AddDbContext<OrderManagementDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
        }
        public static void AddQueue(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("ServiceBus:UpdateOrderQueue");

            var client = new ServiceBusClient(connectionString, new ServiceBusClientOptions
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            });
            var sender = client.CreateSender("order");

            var updateOrderStatusQueue = new UpdateOrderStatusQueue(sender);
            var updateOrderStatusProcessor = new UpdateOrderStatusProcessor(client.CreateProcessor("order"));

            services.AddSingleton(updateOrderStatusProcessor);
            services.AddScoped<IUpdateOrderStatusQueue>(options => updateOrderStatusQueue);
        }

        public static void AddSignalR(IServiceCollection services)
        {
            services.AddScoped<IOrderNotifier, SignalROrderStatusNotifier>();
            services.AddSignalR();
        }
    }
}
