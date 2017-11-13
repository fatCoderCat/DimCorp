using System.Collections.Generic;
using ServiceStack.DimCorp.Core;

namespace ServiceStack.DimCorp.Dal
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        Order GetById(int requestId);
        Order Add(Order newOrder);
        Order Update(Order updatedOrder);
        void Delete(int requestId);
    }
}
