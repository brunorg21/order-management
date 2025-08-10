
using Azure.Messaging.ServiceBus;
using OrderManagement.Application.UseCases.Order.Update.UpdateOrderStatus;
using OrderManagement.Infra.Services.ServiceBus;

namespace OrderManagement.Api.BackgroundServices
{
    public class UpdateOrderStatusService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ServiceBusProcessor _serviceBusProcessor;
        public UpdateOrderStatusService(IServiceProvider services, UpdateOrderStatusProcessor serviceProcessor)
        {
            _services = services;
            _serviceBusProcessor = serviceProcessor.GetServiceBusProcessor();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _serviceBusProcessor.ProcessMessageAsync += ProcessMessageAsync;
            _serviceBusProcessor.ProcessErrorAsync += ProcessErrorAsync;

            await _serviceBusProcessor.StartProcessingAsync(stoppingToken);
        }
        private async Task ProcessErrorAsync(ProcessErrorEventArgs args)
        {
            Console.WriteLine($"Error processing message: {args.Exception.Message}");
            await Task.CompletedTask;
        }
        private async Task ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            var message = args.Message.Body.ToString();

            Guid orderId = Guid.Parse(message);

            var scope = _services.CreateScope();

            var useCase = scope.ServiceProvider.GetRequiredService<IUpdateOrderStatusUseCase>();

            await useCase.Execute(orderId, Communication.Enums.OrderStatusType.PROCESSING);

            await Task.Delay(TimeSpan.FromSeconds(10));

            await useCase.Execute(orderId, Communication.Enums.OrderStatusType.COMPLETED);

            await args.CompleteMessageAsync(args.Message);
        }
    }
}
