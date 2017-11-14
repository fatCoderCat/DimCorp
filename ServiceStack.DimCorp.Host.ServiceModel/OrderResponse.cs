using System;
using System.Collections.Generic;

namespace ServiceStack.DimCorp.Host.ServiceModel
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public bool IsTakeAway { get; set; }
        public DateTime CreationDate { get; set; }
        public Status Status { get; set; }
        public List<OrderItemResponse> Items { get; set; }
        public List<Link> Links { get; set; }
    }
}