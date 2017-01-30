namespace BalkanAir.Api.Tests.IntegrationTests
{
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Routing;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Threading;
    using System.Net;

    [TestClass]
    public class AirportsIntegrationTests
    {
        [TestMethod]
        public void GetByIdShouldReturnCorrectResponse()
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApiGet",
                routeTemplate: "Api/{controller}",
                defaults: new { action = "All" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            var httpServder = new HttpServer(config);
            var httpInvoker = new HttpMessageInvoker(httpServder);

            using (httpInvoker)
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("http://test.com/api/airports/"),
                    Method = HttpMethod.Get
                };

                var result = httpInvoker.SendAsync(request, CancellationToken.None).Result;

                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            }
        }
    }
}
