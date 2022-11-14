using AutoMapper;
using TestExercise.Models;
using TestExercise.Models.Dto;

namespace TestExercise.Configuration
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Provider, ProviderDto>();
                config.CreateMap<ProviderDto, Provider>();
                config.CreateMap<OrderItem, OrderItemDto>();
                config.CreateMap<OrderItemDto, OrderItem>();
                config.CreateMap<Order, OrderDto>();
                config.CreateMap<OrderDto, Order>();

                config.CreateMap<IEnumerable<OrderItem>,IEnumerable<OrderItemDto>>();
                config.CreateMap<IEnumerable<OrderItemDto>,IEnumerable<OrderItem>>();

                config.CreateMap<IQueryable<Order>,IQueryable<OrderDto>>();
                config.CreateMap<IQueryable<OrderDto>,IQueryable<Order>>();

            });
            return mappingConfig;
        }
    }
}
