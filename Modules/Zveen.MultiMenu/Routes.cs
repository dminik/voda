using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Mvc.Routes;

namespace Orchard.Blogs
{
    public class Routes : IRouteProvider
    {

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[] {
                new RouteDescriptor {
                    Route = new Route(
                        "ZveenMenu/superfishCss/{id}",
                        new RouteValueDictionary {
                            {"area", "Zveen.MultiMenu"},
                            {"controller", "MultiMenu"},
                            {"action", "superfishCss"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "Zveen.MultiMenu"}
                        },
                        new MvcRouteHandler())
                },
                new RouteDescriptor {
                    Route = new Route(
                        "ZveenMenu/superfishVerticalCss/{id}",
                        new RouteValueDictionary {
                            {"area", "Zveen.MultiMenu"},
                            {"controller", "MultiMenu"},
                            {"action", "superfishVerticalCss"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "Zveen.MultiMenu"}
                        },
                        new MvcRouteHandler())
                },
                new RouteDescriptor {
                    Route = new Route(
                        "ZveenMenu/style/{id}",
                        new RouteValueDictionary {
                            {"area", "Zveen.MultiMenu"},
                            {"controller", "MultiMenu"},
                            {"action", "style"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "Zveen.MultiMenu"}
                        },
                        new MvcRouteHandler())
                }
            };
        }
    }
}