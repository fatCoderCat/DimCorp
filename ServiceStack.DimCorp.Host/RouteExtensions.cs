using ServiceStack.Web;

namespace ServiceStack.DimCorp.Host
{
    public static class RouteExtensions
    {
        public static IServiceRoutes Add<T>(this IServiceRoutes routes, string restPath, string verbs, string summary)
        {
            return routes.Add(typeof(T), restPath, verbs, summary, string.Empty);
        }

        public static IServiceRoutes Add<T>(this IServiceRoutes routes, string restPath, string verbs, string summary, string notes)
        {
            return routes.Add(typeof(T), restPath, verbs, summary, notes);
        }
    }
}