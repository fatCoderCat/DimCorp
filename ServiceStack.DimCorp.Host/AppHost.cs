using AutoMapper;
using Funq;
using ServiceStack.Caching;
//using ServiceStack.Caching.Memcached; //Check availability at core version
using ServiceStack.DimCorp.Dal;
using ServiceStack.DimCorp.Host.ServiceInterface;
using ServiceStack.DimCorp.Host.ServiceModel;
using ServiceStack.DimCorp.Host.ServiceModel.Mappers;
using ServiceStack.DimCorp.Host.Validation;
using ServiceStack.Validation;

namespace ServiceStack.DimCorp.Host
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("Order Management", typeof(MyServices).Assembly)
        {
            //TODO: refactor. do we really need route extensions?
            Routes
                .Add<GetProducts>("/products", "GET", "Returns Products")
                .Add<GetProduct>("/products/{Id}", "GET", "Returns a Product")
                .Add<CreateProduct>("/products", "POST", "Creates a Product")
                .Add<UpdateProduct>("/products/{Id}", "PUT", "Updates a Product")
                .Add<DeleteProduct>("/products/{Id}", "DELETE", "Deletes a Product");

            Routes
                .Add<GetOrder>("/orders/{Id}", "GET", "Returns an Order")
                .Add<GetOrders>("/orders", "GET", "Returns Orders")
                .Add<Order>("/orders", "POST", "Creates an Order")
                .Add<Order>("/orders/{Id}", "PUT", "Updates an Order")
                .Add<DeleteOrder>("/orders/{Id}", "DELETE", "Deletes an Order");

            Routes
                .Add<GetOrderItem>("/orders/{OrderId}/items/{ItemId}", "GET", "Get an OderItem")
                .Add<GetOrderItems>("/orders/{OrderId}/items", "GET", "Get a list of OrderItems");
        }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        public override void Configure(Container container)
        {
            Plugins.Add(new ValidationFeature());
            Plugins.Add(new PostmanFeature());

            //Products
            container.Register<IProductRepository>(new ProductRepository());
            container.Register<IProductMapper>(new ProductMapper());
            
            //Orders
            container.Register<IOrderRepository>(new OrderRepository());
            container.Register<IOrderMapper>(new OrderMapper());
            container.Register<IStatusRepository>(new StatusRepository());

            //Validators
            container.RegisterValidator(typeof(CreateProductValidator));
            container.RegisterValidator(typeof(UpdateProductValidator));
            container.RegisterValidator(typeof(OrderValidator));

            container.Register<ICacheClient>(arg => new MemoryCacheClient());
        }

        //public override IServiceRunner<TRequest> CreateServiceRunner<TRequest>(ActionContext actionContext)
        //{
        //    return new LoggingServiceRunner<TRequest>(this, actionContext);
        //}
    }

    //public class LoggingServiceRunner<T> : ServiceRunner<T>
    //{
    //    public LoggingServiceRunner(IAppHost appHost, ActionContext actionContext) : base(appHost, actionContext)
    //    {
    //        Console.WriteLine(actionContext.Id);
    //    }
    //}
}