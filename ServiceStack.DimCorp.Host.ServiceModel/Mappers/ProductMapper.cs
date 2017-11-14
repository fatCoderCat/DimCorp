using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ServiceStack.DimCorp.Core;

namespace ServiceStack.DimCorp.Host.ServiceModel.Mappers
{
    public class ProductMapper : IProductMapper
    {
        public Core.Product ToProduct(CreateProduct request)
        {
            return Mapper.Map<Core.Product>(request);
        }

        public Core.Product ToProduct(UpdateProduct request)
        {
            return Mapper.Map<Core.Product>(request);
        }

        public ProductResponse ToProductResponse(Core.Product product)
        {
            var productResponse = Mapper.Map<ProductResponse>(product);
            productResponse.Links = new List<Link>
            {
                new Link
                {
                    Title = "self",
                    Rel = "self",
                    Href = $"products/{product.Id}"
                }
            };
            return productResponse;
        }

        public List<ProductResponse> ToProductResponseList(List<Core.Product> products)
        {
            var productRsponseList = new List<ProductResponse>();
            products.ForEach(x => productRsponseList.Add(ToProductResponse(x)));
            return productRsponseList;
        }
        
        public ProductsResponse ToProductsResponse(Core.PagedListResult<Core.Product> products)
        {
            var productList = ToProductResponseList(products.Entities.ToList());
            var productResponse = new ProductsResponse {Products = productList};

            SelfLink(products, productResponse);
            NextLink(products, productResponse);
            PreviousLink(products, productResponse);
            FirstLink(products, productResponse);
            LastLink(products, productResponse);

            return productResponse;
        }

        private void LastLink(PagedListResult<Core.Product> products, ProductsResponse productResponse)
        {
            throw new System.NotImplementedException();
        }

        private void FirstLink(PagedListResult<Core.Product> products, ProductsResponse productResponse)
        {
            throw new System.NotImplementedException();
        }

        private void PreviousLink(PagedListResult<Core.Product> products, ProductsResponse productResponse)
        {
            throw new System.NotImplementedException();
        }

        private void SelfLink(PagedListResult<Core.Product> products, ProductsResponse productResponse)
        {
            throw new System.NotImplementedException();
        }
        
        private void NextLink(PagedListResult<Core.Product> products, ProductsResponse productResponse)
        {
            if (products.HasNext)
            {
                productResponse.Links.Add(NewLink("next", $"products?page={products.Page + 1}&size={products.Size}"));
            }
        }

        private Link NewLink(string next, string s)
        {
            throw new System.NotImplementedException();
        }
    }
}