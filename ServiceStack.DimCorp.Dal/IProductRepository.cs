using System.Collections.Generic;
using ServiceStack.DimCorp.Core;

namespace ServiceStack.DimCorp.Dal
{
    public interface IProductRepository
    {
        Product GetById(int id);
        List<Product> GetAll();
        Product Add(Product product);
        Product Update(Product product);
        void Delete(int id);
    }
}
