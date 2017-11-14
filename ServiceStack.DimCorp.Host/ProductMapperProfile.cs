using AutoMapper;

namespace ServiceStack.DimCorp.Host.ServiceModel.Mappers
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<Status, Core.Status>();
            CreateMap<Core.Status, Status>();
            CreateMap<CreateProduct, Core.Product>();
            CreateMap<UpdateProduct, Core.Product>();
            CreateMap<Core.Product, ProductResponse>();
            CreateMap<ProductResponse, Core.Product>();
        }
    }
}
