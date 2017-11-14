using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.DimCorp.Host.ServiceModel;

namespace ServiceStack.DimCorp.Host.Tests
{
    [TestClass]
    public class ProductServiceTest
    {
        [TestMethod]
        public void GetProducctByProductId_ReturnValidProduct()
        {
            //Arrange
            var productId = 1;
            var client = new JsonServiceClient("http://localhost:50712/");

            //Act
            var product = client.Get<ProductResponse>($"/products/{productId}");

            //Assert
            Assert.IsNotNull(product);
            Assert.AreEqual(productId, product.Id);
        }

        [TestMethod]
        public void GetAllProducts_ReturnValidProductsList()
        {
            //Arrange
            var productId = 1;
            var client = new JsonServiceClient("http://localhost:50712/");

            //Act
            var products = client.Get<ProductsResponse>("/products");

            //Assert
            Assert.IsNotNull(products);
            Assert.IsTrue(products.Products.Count > 0);
        }

        [TestMethod]
        public void CreateNewProduct_ReturnsObjectAnd201CreatedStatus()
        {
            //Arrange
            WebHeaderCollection headers = null;
            HttpStatusCode statusCode = 0;
            const string productName = "Cappuccino";
            const string site = "http://localhost:50712";
            const string products = "/products";
            const string url = site + products;

            var client = new JsonServiceClient(site)
            {
                ResponseFilter = httpRes =>
                {
                    headers = httpRes.Headers;
                    statusCode = httpRes.StatusCode;
                }
            };

            var newProduct = new CreateProduct
            {
                Name = productName,
                Status = new Status { Id = 1 }
            };

            //Act
            var product = client.Post<ProductResponse>(products, newProduct);

            //Assert
            Assert.AreEqual($"{url}/{product.Id}", headers["Location"]);
            Assert.AreEqual(HttpStatusCode.Created, statusCode);
            Assert.AreEqual(productName, product.Name);
        }

        [TestMethod]
        public void UpdateProduct_ReturnUpdateObject()
        {
            //Arrange
            HttpStatusCode statusCode = 0;
            const string productName = "white wine";
            const string site = "http://localhost:50712";
            const string productLink = "/products/2";

            var client = new JsonServiceClient(site)
            {
                ResponseFilter = httpRes =>
                {
                    statusCode = httpRes.StatusCode;
                }
            };

            var updateProduct = new UpdateProduct
            {
                Name = productName,
                Status = new Status { Id = 2 } // 2 = Inactive
            };

            //Act 
            var product = client.Put<ProductResponse>(productLink, updateProduct);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
            Assert.AreEqual(productName, product.Name);
            Assert.AreEqual(2, product.Status);
        }

        [TestMethod]
        public void DeleteProduct_ReturnNoContent()
        {
            //Arrange
            HttpStatusCode statusCode = 0;
            const string site = "http://localhost:50712";

            var client = new JsonServiceClient(site)
            {
                ResponseFilter = httpRes =>
                {
                    statusCode = httpRes.StatusCode;
                }
            };

            //Act
            client.Delete<HttpResult>("/products/5");

            //Assert
            Assert.AreEqual(HttpStatusCode.NoContent, statusCode);
        }
    }
}
