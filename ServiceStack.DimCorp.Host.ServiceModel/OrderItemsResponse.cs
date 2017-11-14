using System.Collections.Generic;

namespace ServiceStack.DimCorp.Host.ServiceModel
{
    public class OrderItemsResponse
    {
        public List<OrderItemResponse> Items { get; set; }
    }
}