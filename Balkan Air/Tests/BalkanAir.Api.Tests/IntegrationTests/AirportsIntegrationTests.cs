namespace BalkanAir.Api.Tests.IntegrationTests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Web.Http;
    using System.Web.Http.Routing;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Data.Repositories.Contracts;
    using Data.Models;
    using Moq;
    using Services.Data.Contracts;
    using System.Linq;

    [TestClass]
    public class AirportsIntegrationTests
    {
        private static Random rand = new Random();
        private string inMemoryServerUrl = "http://localhost:12345";

        [TestMethod]
        public void GetByIdShouldReturnCorrectResponse()
        {
            //var config = new HttpConfiguration();
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApiGet",
            //    routeTemplate: "Api/{controller}",
            //    defaults: new { action = "All" },
            //    constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            //);

            //config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            //var httpServder = new HttpServer(config);
            //var httpInvoker = new HttpMessageInvoker(httpServder);

            //using (httpInvoker)
            //{
            //    var request = new HttpRequestMessage()
            //    {
            //        RequestUri = new Uri("http://test.com/api/airports/"),
            //        Method = HttpMethod.Get
            //    };

            //    var result = httpInvoker.SendAsync(request, CancellationToken.None).Result;

            //    Assert.IsNotNull(result);
            //    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            //}

            //var repo = new Mock<IRepository<Airport>>();
            //Airport[] airprots = { new Airport(), new Airport(), new Airport() };

            //repo.Setup(a => a.All())
            //    .Returns(airprots.AsQueryable());

            //var server = new InMemoryHttpServer<Airport>(this.inMemoryServerUrl, repo.Object);

            //var response = server.CreateGetRequest("/api/airports");

            //Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            //Assert.IsNotNull(response.Content);
        }
    }
}
