using System;
using System.Net;
using ServiceStack.DimCorp.Dal;
using ServiceStack.DimCorp.Host.ServiceModel;
using ServiceStack.DimCorp.Host.ServiceModel.Mappers;

namespace ServiceStack.DimCorp.Host.ServiceInterface
{
    [EnableCors]
    public class OrderService : Service
    {
        public IOrderRepository OrderRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public IStatusRepository StatusRepository { get; set; }
        public IOrderMapper OrderMapper { get; set; }

        public OrdersResponse Get(GetOrders request)
        {
            var orders = OrderRepository.GetAllOrders();

            return new OrdersResponse
            {
                Orders = OrderMapper.ToOrderResponseList(orders)
            };
        }

        public OrderResponse Get(GetOrder request)
        {
            var domainObject = OrderRepository.GetById(request.Id);
            if (domainObject != null)
                return OrderMapper.ToOrderResponse(domainObject);

            Response.StatusCode = (int) HttpStatusCode.NotFound;
            return null;
        }

        public OrderResponse Post(Order request)
        {
            var newOrder = OrderMapper.ToOrder(request);
            newOrder.Status = StatusRepository.GetById(request.Status.Id);

            newOrder.Items.ForEach(x =>
            {
                x.Product = ProductRepository.GetById(x.Product.Id);
            });

            newOrder = OrderRepository.Add(newOrder);
            var response = OrderMapper.ToOrderResponse(newOrder);

            Response.AddHeader("Location", $"{Request.AbsoluteUri}/{newOrder.Id}");
            Response.StatusCode = (int) HttpStatusCode.Created;

            return response;
        }

        public OrderResponse Put(Order request)
        {
            var domainObject = OrderRepository.GetById(request.Id);
            if (domainObject == null)
            {
                Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }

            var updatedOrder = OrderMapper.ToOrder(request);
            updatedOrder.Status = StatusRepository.GetById(request.Status.Id);
            updatedOrder.Items.ForEach(x =>
            {
                x.Product = ProductRepository.GetById(x.Product.Id);
            });

            var order = OrderRepository.Update(updatedOrder);

            return OrderMapper.ToOrderResponse(order);
        }

        public HttpResult Delete(DeleteOrder request)
        {
            var domainObject = OrderRepository.GetById(request.Id);
            if (domainObject == null)
            {
                Response.StatusCode = (int) HttpStatusCode.NotFound;
            }
            else
            {
                OrderRepository.Delete(request.Id);
                Response.StatusCode = (int) HttpStatusCode.NoContent;
            }

            return null;
        }
    }
}
