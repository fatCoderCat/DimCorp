using System.Collections.Generic;
using AutoMapper;

namespace ServiceStack.DimCorp.Host.ServiceModel.Mappers
{
    public class OrderMapper : IOrderMapper
    {
        public Core.Order ToOrder(Order request)
        {
            return Mapper.Map<Core.Order>(request);
        }

        public OrderResponse ToOrderResponse(Core.Order order)
        {
            var orderResponse = Mapper.Map<OrderResponse>(order);

            var orderSelfLink = $"orders/{order.Id}";
            orderResponse.Links = new List<Link> {SelfLink(orderSelfLink)};
            orderResponse.Items.ForEach(x =>
            {
                var productId = x.Product.Id;
                var productLink = $"products/{productId}";
                var itemsLink = $"{orderSelfLink}/items/{x.Id}";
                x.Product.Links = new List<Link> {SelfLink(productLink)};
                x.Links = new List<Link> {SelfLink(itemsLink)};
            });

            return orderResponse;
        }

        public List<OrderResponse> ToOrderResponseList(List<Core.Order> orders)
        {
            var orderResponseList = new List<OrderResponse>();
            orders.ForEach(x => orderResponseList.Add(ToOrderResponse(x)));
            return orderResponseList;
        }

        public OrderItemResponse ToOrderItemResponse(int orderId, Core.OrderItem orderItem)
        {
            var orderItemResponse = Mapper.Map<OrderItemResponse>(orderItem);
            var productId = orderItemResponse.Product.Id;
            var orderLink = $"orders/{orderId}";
            var itemsLink = $"/items/{orderItem.Id}";
            var productLink = $"products/{productId}";
            
            orderItemResponse.Links.Add(SelfLink(orderLink + itemsLink));
            orderItemResponse.Links.Add(ParentLink(orderLink));
            orderItemResponse.Product.Links.Add(SelfLink(productLink));

            return orderItemResponse;
        }

        public List<OrderItemResponse> ToOrderItemResponseList(int orderId, List<Core.OrderItem> items)
        {
            var orderItemResponseList = new List<OrderItemResponse>();
            items.ForEach(x => orderItemResponseList.Add(ToOrderItemResponse(orderId, x)));
            return orderItemResponseList;
        }

        private Link SelfLink(string url)
        {
            return new Link
            {
                Title = "self",
                Rel = "self",
                Href = url
            };
        }

        private Link ParentLink(string url)
        {
            return new Link
            {
                Title = "parent",
                Rel = "parent",
                Href = url
            };
        }
    }
}