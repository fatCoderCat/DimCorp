using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.DimCorp.Host.ServiceModel;

namespace ServiceStack.DimCorp.Host.Tests
{
    [TestClass]
    public class OrderServiceTets
    {
        [TestMethod]
        public void GetOrdersById()
        {
            //Arrange
            const int orderId = 1;
            var client = new XmlServiceClient("http://localhost:50712/");

            //Act
            var order = client.Get<OrderResponse>($"/orders/{orderId}");

            //Assert
            Assert.IsNotNull(order);
            Assert.AreEqual(orderId, order.Id);
        }

        [TestMethod]
        public void GetAlOrders()
        {
            //Arrange
            var client = new XmlServiceClient("http://localhost:50712/");

            //Act
            var orders = client.Get<OrdersResponse>("/orders");

            //Assert
            Assert.IsNotNull(orders);
            Assert.IsTrue(orders.Orders.Count > 0);
        }

        [TestMethod]
        public void CreateNewOrder()
        {
            //Arrange 
            WebHeaderCollection headers = null;
            HttpStatusCode statusCode = 0;
            const string site = "http://locahost:50712";
            const string orders = "/orders";
            const string url = site + orders;

            var client = new XmlServiceClient(site)
            {
                ResponseFilter = httpRes =>
                {
                    headers = httpRes.Headers;
                    statusCode = httpRes.StatusCode;
                }
            };

            var newOrder = new Order
            {
                CreationDate = DateTime.Now,
                IsTakeAway = true,
                Status = new Status { Id = 1 }, //Active
                Items = new List<OrderItem>
                {
                    new OrderItem { Product = new Product { Id = 1 }, Quantity =  10},
                    new OrderItem { Product = new Product { Id = 2 }, Quantity =  10}
                }
            };

            //Act
            var order = client.Post<OrderResponse>(orders, newOrder);

            //Assert 
            Assert.AreEqual($"{url}/{order.Id}", headers["Location"]);
            Assert.AreEqual(HttpStatusCode.Created, statusCode);
            Assert.AreEqual(2, order.Items.Count);
            Assert.AreEqual(1, order.Status.Id);
        }

        [TestMethod]
        public void UpdateOrder()
        {
            //Arrange
            HttpStatusCode statusCode = 0;
            const string site = "http://locahost:50712";
            const string ordersLink = "/orders/1";
            const int newProductId = 5;
            var newCreationDate = new DateTime(2013, 08, 08);

            var client = new XmlServiceClient(site)
            {
                ResponseFilter = httpRes =>
                {
                    statusCode = httpRes.StatusCode;
                }
            };

            var updateOrder = new Order
            {
                CreationDate = newCreationDate,
                IsTakeAway = false,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = 6,
                        Product = new Product { Id = newProductId },
                        Quantity = 100
                    }
                },
                Status = new Status { Id = 1 }
            };

            //Act
            var orderResponse = client.Put<OrderResponse>(ordersLink, updateOrder);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
            Assert.AreEqual(newCreationDate, orderResponse.CreationDate);
            Assert.IsFalse(orderResponse.IsTakeAway);
            Assert.AreEqual(1, orderResponse.Items.Count);
            Assert.AreEqual(newProductId, orderResponse.Items[0].Product.Id);
        }

        [TestMethod]
        public void DeleteOrder()
        {
            //Arrange
            HttpStatusCode statusCode = 0;
            const string site = "http://localhost:50712";

            var client = new XmlServiceClient(site)
            {
                ResponseFilter = httpRes =>
                {
                    statusCode = httpRes.StatusCode;
                }
            };

            //Act 
            client.Delete<HttpResult>("/orders/2");

            //Assert
            Assert.AreEqual(HttpStatusCode.NoContent, statusCode);
        }
    }
}
