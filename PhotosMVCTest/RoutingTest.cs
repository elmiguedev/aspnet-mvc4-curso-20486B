using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotosMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace PhotosMVCTest
{
    [TestClass]
    public class RoutingTest
    {
        [TestMethod]
        public void Test_Default_Router_ControllerOnly()
        {
            var context = new FakeHttpContextForRouting(requestUrl: "~/ControllerName");
            var routes = new RouteCollection();

            RouteConfig.RegisterRoutes(routes);
            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("ControllerName", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
        }

        [TestMethod]
        public void Test_Photo_Route_With_PhotoID()
        {
            var context = new FakeHttpContextForRouting(requestUrl: "~/photo/2");
            var routes = new RouteCollection();

            RouteConfig.RegisterRoutes(routes);
            RouteData routeData = routes.GetRouteData(context);
            Assert.IsNotNull(routeData);

            Assert.AreEqual("Photo", routeData.Values["controller"]);

            Assert.AreEqual("Display", routeData.Values["action"]);

            Assert.AreEqual("2", routeData.Values["id"]);

        }

        [TestMethod]
        public void Test_Photo_Title_Route()
        {
            var context = new FakeHttpContextForRouting(requestUrl: "~/photo/title/my%20title");
            var routes = new RouteCollection();

            RouteConfig.RegisterRoutes(routes);
            RouteData routeData = routes.GetRouteData(context);
            Assert.IsNotNull(routeData);

            Assert.AreEqual("Photo", routeData.Values["controller"]);

            Assert.AreEqual("DisplayByTitle", routeData.Values["action"]);

            Assert.AreEqual("my%20title", routeData.Values["title"]);
        }
    }
}
