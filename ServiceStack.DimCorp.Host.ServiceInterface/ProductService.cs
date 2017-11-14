using System;
using System.Net;
using ServiceStack.DimCorp.Core;
using ServiceStack.DimCorp.Dal;
using ServiceStack.DimCorp.Host.ServiceModel;
using ServiceStack.DimCorp.Host.ServiceModel.Mappers;

namespace ServiceStack.DimCorp.Host.ServiceInterface
{
    public class ProductService : Service
    {
        public IProductMapper ProductMapper { get; set; }
        public IProductRepository ProductRepository { get; set; }

        public object Get(GetProduct request)
        {
            //Use it, or simple cache client: Request.GetCacheClient()
            //return Request.ToOptimizedResultUsingCache(
            //    Cache,
            //    UrnId.Create<ProductResponse>(request.Id),
            //    TimeSpan.FromSeconds(20),
            //    () => ProductMapper.ToProductResponse(ProductRepository.GetById(request.Id)));

            var product = ProductRepository.GetById(request.Id);
            if (product != null)
                return ProductMapper.ToProductResponse(product);

            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return default(ProductResponse);
        }

        public ProductsResponse Get(GetProducts request)
        {
            var products = ProductRepository.GetAll();

            return new ProductsResponse
            {
                Products = ProductMapper.ToProductResponseList(products)
            };
        }

        ProductResponse Post(CreateProduct request)
        {
            var domainProduct = ProductMapper.ToProduct(request);
            var newProduct = ProductRepository.Add(domainProduct);
            var response = ProductMapper.ToProductResponse(newProduct);

            Response.AddHeader("Location", $"{Request.AbsoluteUri}/{newProduct.Id}");
            Response.StatusCode = (int) HttpStatusCode.Created;

            return response;
        }

        ProductResponse Put(UpdateProduct request)
        {
            var domainObject = ProductRepository.GetById(request.Id);
            if (domainObject == null)
            {
                Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }

            var domainProduct = ProductMapper.ToProduct(request);
            var updateProduct = ProductRepository.Update(domainProduct);
            return ProductMapper.ToProductResponse(updateProduct);
        }

        public HttpResult Delete(DeleteProduct request)
        {
            //Request.RemoveFromCache(this.Cache,
            //    UrnId.Create<GetProduct>(request.Id));

            var domainObject = ProductRepository.GetById(request.Id);
            if (domainObject == null)
            {
                Response.StatusCode = (int) HttpStatusCode.NotFound;
            }
            else
            {
                ProductRepository.Delete(request.Id);
                Response.StatusCode = (int) HttpStatusCode.NoContent;
            }
            return null;
        }
    }
}
