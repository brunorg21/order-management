using AutoMapper;
using OrderManagement.Communication.Requests;
using OrderManagement.Communication.Responses;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Extensions;

namespace OrderManagement.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<RequestOrderJson, Order>();
            CreateMap<Order, ResponseOrderJson>()
                .ForMember(dest => dest.OrderStatus, options => options.MapFrom(src => src.OrderStatus.OrderStatusToString()));
        }
    }
}
