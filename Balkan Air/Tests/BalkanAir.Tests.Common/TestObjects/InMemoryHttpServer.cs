namespace BalkanAir.Tests.Common.TestObjects
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http;
    using System.Web.Http.Routing;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using Data.Repositories.Contracts;

    public class InMemoryHttpServer<T>
        where T : class
    {
        private readonly HttpClient client;
        private string baseUrl;

        public InMemoryHttpServer(string baseUrl, IRepository<T> repository)
        {
            this.baseUrl = baseUrl;

            var config = new HttpConfiguration();
            this.AddHttpRoutes(config.Routes);
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            var resolver = new DependencyResolver<T>();
            resolver.Repository = repository;
            config.DependencyResolver = resolver;

            var server = new HttpServer(config);
            this.client = new HttpClient(server);
        }

        public HttpResponseMessage CreateGetRequest(string requestUrl, string mediaType = "application/json")
        {
            var url = requestUrl;
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(baseUrl + url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            request.Method = HttpMethod.Get;

            var response = this.client.SendAsync(request).Result;
            return response;
        }

        public Task<HttpResponseMessage> CreateGetRequestAsync(string requestUrl, string mediaType = "application/json")
        {
            var url = requestUrl;
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(baseUrl + url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            request.Method = HttpMethod.Get;

            var response = this.client.SendAsync(request);
            return response;
        }

        public HttpResponseMessage CreatePostRequest(string requestUrl, object data, string mediaType = "application/json")
        {
            var url = requestUrl;
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(baseUrl + url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(JsonConvert.SerializeObject(data));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);

            var response = this.client.SendAsync(request).Result;
            return response;
        }

        public HttpResponseMessage post(string requestUrl, IEnumerable<KeyValuePair<string, string>> postData)
        {
            var content = new FormUrlEncodedContent(postData);
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");


            var url = requestUrl;
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(baseUrl + url);

            request.Method = HttpMethod.Post;
            request.Content = new StringContent(JsonConvert.SerializeObject(content));

            var response = this.client.SendAsync(request).Result;
            return response;
        }

        public HttpResponseMessage CreatePutRequest(string requestUrl, object data, string mediaType = "application/json")
        {
            var url = requestUrl;
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(baseUrl + url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            request.Method = HttpMethod.Put;
            request.Content = new StringContent(JsonConvert.SerializeObject(data));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);

            var response = this.client.SendAsync(request).Result;
            return response;
        }

        public HttpResponseMessage CreateDeleteRequest(string requestUrl)
        {
            var url = requestUrl;
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(baseUrl + url);
            request.Method = HttpMethod.Delete;

            var response = this.client.SendAsync(request).Result;
            return response;
        }

        private void AddHttpRoutes(HttpRouteCollection routeCollection)
        {
            var routes = GetRoutes();
            routes.ForEach(r => routeCollection.MapHttpRoute(r.Name, r.Template, r.Defaults, r.Constraints));
        }

        private List<Route> GetRoutes()
        {
            return new List<Route>
            {
                new Route(
                    "DefaultApiGetAll",
                    "Api/{controller}",
                    new { action = "All" },
                    new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }),
                new Route(
                    "DefaultApiGetWithId",
                    "Api/{controller}/{id}",
                    new { action = "Get" },
                    new { id = @"\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get) }),
                new Route(
                    "GetAirportAndCountryByAbbreviation",
                    "Api/{controller}/{abbreviation}",
                    new { action = "GetByAbbreviation" },
                    new { abbreviation = @"\b[a-zA-Z]{2,3}\b", httpMethod = new HttpMethodConstraint(HttpMethod.Get) }),
                new Route(
                    "GetFareByRouteAbbreviations",
                    "Api/{controller}/{originAbbreviation}/{destinationAbbreviation}",
                    new { action = "GetByRoute" },
                    new
                    {
                        originAbbreviation = @"\b[a-zA-Z]{3}\b",
                        destinationAbbreviation = @"\b[a-zA-Z]{3}\b",
                        httpMethod = new HttpMethodConstraint(HttpMethod.Get)
                    })
            }; 
        }

        private class Route
        {
            public Route(string name, string template, object defaults, object constraints)
            {
                this.Name = name;
                this.Template = template;
                this.Defaults = defaults;
                this.Constraints = constraints;
            }

            public string Name { get; set; }

            public object Defaults { get; set; }

            public string Template { get; set; }

            public object Constraints { get; set; }
        }
    }
}
