namespace BalkanAir.Tests.Common.TestObjects
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Routing;

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
            request.RequestUri = new Uri(this.baseUrl + url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            request.Method = HttpMethod.Get;

            var response = this.client.SendAsync(request).Result;
            return response;
        }

        public Task<HttpResponseMessage> CreateGetRequestAsync(string requestUrl, string mediaType = "application/json")
        {
            var url = requestUrl;
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(this.baseUrl + url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            request.Method = HttpMethod.Get;

            var response = this.client.SendAsync(request);
            return response;
        }

        public HttpResponseMessage CreatePostRequest(string requestUrl, object data, string mediaType = "application/json")
        {
            var url = requestUrl;
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(this.baseUrl + url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(JsonConvert.SerializeObject(data));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);

            var response = this.client.SendAsync(request).Result;
            return response;
        }

        public HttpResponseMessage CreatePutRequest(string requestUrl, object data, string mediaType = "application/json")
        {
            var url = requestUrl;
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(this.baseUrl + url);
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
            request.RequestUri = new Uri(this.baseUrl + url);
            request.Method = HttpMethod.Delete;

            var response = this.client.SendAsync(request).Result;
            return response;
        }

        private void AddHttpRoutes(HttpRouteCollection routeCollection)
        {
            var routes = this.GetRoutes();
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
                    }),
                new Route(
                    "GetFlightByFlightNumber",
                    "Api/Flights/{flightNumber}",
                    new { controller = "Flights", action = "GetByFlightNumber" },
                    new { flightNumber = @"^[a-zA-Z0-9]+$", httpMethod = new HttpMethodConstraint(HttpMethod.Get) }),
                new Route(
                    "GetFlightByFlightStatus",
                    "Api/{controller}/Status/{flightStatus}",
                    new { controller = "Flights", action = "GetByFlightStatus" },
                    new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }),
                new Route(
                    "GetFlightByDepartureAirport",
                    "Api/{controller}/Departures/{airportAbbreviation}",
                    new { controller = "Flights", action = "GetByDepartureAirport" },
                    new { airportAbbreviation = @"\b[a-zA-Z]{3}\b", httpMethod = new HttpMethodConstraint(HttpMethod.Get) }),
                new Route(
                    "GetFlightByArrivalAirport",
                    "Api/{controller}/Arrivals/{airportAbbreviation}",
                    new { controller = "Flights", action = "GetByArrivalAirport" },
                    new { airportAbbreviation = @"\b[a-zA-Z]{3}\b", httpMethod = new HttpMethodConstraint(HttpMethod.Get) }),
                new Route(
                    "GetFlightByRoute",
                    "Api/{controller}/Route/{originAbbreviation}/{destinationAbbreviation}",
                    new { controller = "Flights", action = "GetByRoute" },
                    new
                    {
                        originAbbreviation = @"\b[a-zA-Z]{3}\b",
                        destinationAbbreviation = @"\b[a-zA-Z]{3}\b",
                        httpMethod = new HttpMethodConstraint(HttpMethod.Get)
                    }),
                new Route(
                    "GetFlightByDateTime",
                    "Api/{controller}/DateTime/{dateTime}",
                    new { controller = "Flights", action = "GetByDepartureDateTime" },
                    new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }),
                new Route(
                    "GetLatestNews",
                    "Api/{controller}/Latest/{count}",
                    new { controller = "News", action = "GetLatestNews" },
                    new { count = @"\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get) }),
                new Route(
                    "GetLatestNewsByCategory",
                    "Api/{controller}/Latest/{count}/{category}",
                    new { controller = "News", action = "GetLatesByCategory" },
                    new { count = @"\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get) }),
                new Route(
                    "GetByType",
                    "Api/{controller}/Type/{type}",
                    new { controller = "TravelClasses", action = "GetByType" },
                    new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }),
                new Route(
                    "GetByAircraftId",
                    "Api/{controller}/AircraftId/{aircraftId}",
                    new { controller = "TravelClasses", action = "GetByAircraftId" },
                    new { aircraftId = @"\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get) }),
                new Route(
                    "GetUsersByGender",
                    "Api/{controller}/Gender/{gender}",
                    new { controller = "Users", action = "GetUsersByGender" },
                    new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }),
                new Route(
                    "GetUsersByNationality",
                    "Api/{controller}/Nationality/{nationality}",
                    new { controller = "Users", action = "GetUsersByNationality" },
                    new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) })
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
