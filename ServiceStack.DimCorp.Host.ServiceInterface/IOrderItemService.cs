using System.Collections.Generic;
using System.Linq;
using System.Net;
using ServiceStack.DimCorp.Dal;
using ServiceStack.DimCorp.Host.ServiceModel;
using ServiceStack.DimCorp.Host.ServiceModel.Mappers;

namespace ServiceStack.DimCorp.Host.ServiceInterface
{
    public class OrderItemService : Service
    {
        public IOrderRepository OrderRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public IStatusRepository StatusRepository { get; set; }
        public IOrderMapper OrderMapper { get; set; }

        public OrderItemResponse Get(GetOrderItem request)
        {
            var order = OrderRepository.GetById(request.OrderId);

            Core.OrderItem orderItem = order.Items.FirstOrDefault(x => x.Id == request.ItemId);

            if (orderItem == null)
            {
                Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }

            return OrderMapper.ToOrderItemResponse(order.Id, orderItem);
        }

        public OrderItemsResponse Get(GetOrderItems request)
        {
            var order = OrderRepository.GetById(request.OrderId);
            List<Core.OrderItem> orderItems = order.Items;

            if (orderItems == null || orderItems.Count == 0)
            {
                Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }
            else
            {
                return new OrderItemsResponse
                {
                    Items = OrderMapper.ToOrderItemResponseList(request.OrderId, orderItems)
                };
            }
        }
    }
}
