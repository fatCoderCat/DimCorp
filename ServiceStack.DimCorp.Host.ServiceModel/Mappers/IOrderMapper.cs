using System.Collections.Generic;

namespace ServiceStack.DimCorp.Host.ServiceModel.Mappers
{
    public interface IOrderMapper
    {
        Core.Order ToOrder(Order request);
        OrderResponse ToOrderResponse(Core.Order order);
        List<OrderResponse> ToOrderResponseList(List<Core.Order> orders);
        OrderItemResponse ToOrderItemResponse(int orderId, Core.OrderItem orderItem);
        List<OrderItemResponse> ToOrderItemResponseList(int orderId, List<Core.OrderItem> items);
    }
}
