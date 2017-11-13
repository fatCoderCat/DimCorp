using System.Collections.Generic;
using System.Linq;
using ServiceStack.DimCorp.Core;

namespace ServiceStack.DimCorp.Dal
{
    public class ProductRepository : IProductRepository
    {
        private static readonly List<Product> Products = new List<Product>()
        {
            new Product {Id = 1, Name = "wine", Status = null, Version = 1},
            new Product {Id = 2, Name = "bread", Status = null, Version = 1}
        };

        public Product GetById(int id)
        {
            return Products.FirstOrDefault(x => x.Id == id);
        }

        public List<Product> GetAll()
        {
            return Products.ToList();
        }

        public Product Add(Product product)
        {
            if (product.Id == 0)
            {
                var lastId = Products.Max(x => x.Id);
                product.Id = lastId + 1;
            }

            Products.Add(product);

            return product;
        }

        public Product Update(Product product)
        {
            var existedProd = GetById(product.Id);
            if (existedProd == null)
                return null;

            Products.Remove(existedProd);
            Products.Add(product);

            return product;
        }

        public void Delete(int id)
        {
            var existedProd = GetById(id);
            if (existedProd != null)
                Products.Remove(existedProd);
        }

        public PagedListResult<Product> GetPaged(int page, int size)
        {
            var skip = (page == 0 || page == 1) ? 0 : (page - 1) * size;
            var take = size;

            IQueryable<Product> sequence = Products.AsQueryable();

            var resultCount = sequence.Count();

            var result = (take > 0)
                ? sequence.Skip(skip).Take(take).ToList()
                : sequence.ToList();

            var hasNext = (skip > 0 || take > 0) && (skip + take < resultCount);

            return new PagedListResult<Product>
            {
                Page = page,
                Size = size,
                Entities = result,
                HasNext = hasNext,
                HasPrevious = skip > 0,
                Count = resultCount
            };
        }
    }
}
