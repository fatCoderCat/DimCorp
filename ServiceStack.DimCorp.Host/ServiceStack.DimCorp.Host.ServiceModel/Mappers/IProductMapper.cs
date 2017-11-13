using System.Collections.Generic;

namespace ServiceStack.DimCorp.Host.ServiceModel.Mappers
{
    public interface IProductMapper
    {
        Core.Product ToProduct(CreateProduct request);
        Core.Product ToProduct(UpdateProduct request);
        ProductResponse ToProductResponse(Core.Product product);
        List<ProductResponse> ToProductResponseList(List<Core.Product> products);
    }
}