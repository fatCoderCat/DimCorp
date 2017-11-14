using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.DimCorp.Host.ServiceModel;

namespace ServiceStack.DimCorp.Host.Tests
{
    [TestClass]
    public class OrderItemsServiceTests
    {
        [TestMethod]
        public void GetOrderItemsByOrderId()
        {
            //Arrange
            int orderId = 1;
            var itemsLink = $"/orders/{orderId}/items";

            var client = new XmlServiceClient("http://localhost:50712");

            //Act
            var items = client.Get<OrderItemsResponse>(itemsLink);

            //Assert
            Assert.IsNotNull(items);
            Assert.IsNotNull(items.Items);
            Assert.AreEqual(2, items.Items.Count);
        }

        [TestMethod]
        public void GetOrderItemByOrderId()
        {
            //Arrange
            var itemId = 2;
            var client = new XmlServiceClient("http://localhost:50712");
            var itemLink = "/orders/1/items/2";

            //Act
            var item = client.Get<OrderItemResponse>(itemLink);

            //Assert
            Assert.IsNotNull(item);
            Assert.AreEqual(itemId, item.Id);
        }
    }
}
