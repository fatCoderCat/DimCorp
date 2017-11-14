using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.Testing;
using ServiceStack.DimCorp.Host.ServiceModel;
using ServiceStack.DimCorp.Host.ServiceInterface;

namespace ServiceStack.DimCorp.Host.Tests
{
    [TestClass]
    public class UnitTests : IDisposable
    {
        private readonly ServiceStackHost _appHost;

        public UnitTests()
        {
            _appHost = new BasicAppHost(typeof(MyServices).Assembly)
            {
                ConfigureContainer = container =>
                {
                    //Add your IoC dependencies here
                }
            }
            .Init();
        }
        
        [TestMethod]
        public void Test_Method1()
        {
            var service = _appHost.Container.Resolve<MyServices>();

            var response = (HelloResponse)service.Any(new Hello { Name = "World" });

            Assert.AreEqual(response.Result, "Hello, World!");
        }

        public void Dispose()
        {
            _appHost.Dispose();
        }
    }
}
