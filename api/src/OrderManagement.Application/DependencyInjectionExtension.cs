using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Application.AutoMapper;
using OrderManagement.Application.UseCases.Order.Delete;
using OrderManagement.Application.UseCases.Order.GetAll;
using OrderManagement.Application.UseCases.Order.GetById;
using OrderManagement.Application.UseCases.Order.Register;
using OrderManagement.Application.UseCases.Order.Update;
using OrderManagement.Application.UseCases.Order.Update.UpdateOrderStatus;

namespace OrderManagement.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddUseCases(services);
            AddAutoMapper(services);
        }

        public static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterOrderUseCase, RegisterOrderUseCase>();
            services.AddScoped<IGetOrderByIdUseCase, GetOrderByIdUseCase>();
            services.AddScoped<IGetAllOrdersUseCase, GetAllOrdersUseCase>();
            services.AddScoped<IUpdateOrderUseCase, UpdateOrderUseCase>();
            services.AddScoped<IDeleteOrderUseCase, DeleteOrderUseCase>();
            services.AddScoped<IUpdateOrderStatusUseCase, UpdateOrderStatusUseCase>();
        }
        public static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<AutoMapping>();
            });
        }
    }
}
