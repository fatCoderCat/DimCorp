using AutoMapper;

namespace ServiceStack.DimCorp.Host.ServiceModel.Mappers
{
    public class OrdermapperProfile : Profile
    {
        public OrdermapperProfile()
        {
            CreateMap<Core.Status, Status>();
            CreateMap<Status, Core.Status>();
            CreateMap<Order, Core.Order>();
            CreateMap<OrderItem, Core.OrderItem>();
            CreateMap<Product, Core.Product>();
            CreateMap<Core.Order, OrderResponse>();
            CreateMap<Core.OrderItem, OrderItemResponse>();
            CreateMap<Core.Product, ProductResponse>();
        }
    }
}
