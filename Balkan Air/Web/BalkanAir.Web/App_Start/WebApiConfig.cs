namespace BalkanAir.Web.App_Start
{
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Routing;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            RegisterDefaultRoutes(config);
            RegisterAirportAndCountryRountes(config);
            RegisterFlightRoutes(config);
            RegisterNewsRoutes(config);
        }

        public static void RegisterDefaultRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApiGet",
                routeTemplate: "Api/{controller}",
                defaults: new { action = "All" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute(
                name: "DefaultApiGetWithId",
                routeTemplate: "Api/{controller}/{id}",
                defaults: new { action = "Get" },
                constraints: new { id = @"\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute(
               name: "DefaultApiPost",
               routeTemplate: "Api/{controller}/{action}",
               defaults: new { action = "Create" },
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute(
                name: "DefaultApiPutAndDeleteWithId",
                routeTemplate: "Api/{controller}/{action}/{id}",
                defaults: null,
                constraints: new { id = @"\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Put, HttpMethod.Delete) });
        }

        public static void RegisterAirportAndCountryRountes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "GetAirportAndCountryByAbbreviation",
                routeTemplate: "Api/{controller}/{abbreviation}",
                defaults: new { action = "GetByAbbreviation" },
                constraints: new { abbreviation = @"\b[a-zA-Z]{2,3}\b", httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
        }

        public static void RegisterFlightRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "GetFlightByFlightNumber",
                routeTemplate: "Api/Flights/{flightNumber}",
                defaults: new { controller = "Flights", action = "GetByFlightNumber" },
                constraints: new { flightNumber = @"^[a-zA-Z0-9]+$", httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute(
               name: "GetFlightByFlightStatus",
               routeTemplate: "Api/{controller}/Status/{flightStatus}",
               defaults: new { controller = "Flights", action = "GetByFlightStatus" },
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute(
               name: "GetFlightByDepartureAirport",
               routeTemplate: "Api/{controller}/Departures/{airportAbbreviation}",
               defaults: new { controller = "Flights", action = "GetByDepartureAirport" },
               constraints: new { airportAbbreviation = @"\b[a-zA-Z]{3}\b", httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute(
               name: "GetFlightByArrivalAirport",
               routeTemplate: "Api/{controller}/Arrivals/{airportAbbreviation}",
               defaults: new { controller = "Flights", action = "GetByArrivalAirport" },
               constraints: new { airportAbbreviation = @"\b[a-zA-Z]{3}\b", httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute(
               name: "GetFlightByRoute",
               routeTemplate: "Api/{controller}/Route/{departureAbbreviation}/{arrivalAbbreviation}",
               defaults: new { controller = "Flights", action = "GetByRoute" },
               constraints: new
               {
                   departureAbbreviation = @"\b[a-zA-Z]{3}\b",
                   arrivalAbbreviation = @"\b[a-zA-Z]{3}\b",
                   httpMethod = new HttpMethodConstraint(HttpMethod.Get)
               });

            config.Routes.MapHttpRoute(
               name: "GetFlightByDateTime",
               routeTemplate: "Api/{controller}/DateTime/{dateTime}",
               defaults: new { controller = "Flights", action = "GetFromDateTime" },
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
        }

        public static void RegisterNewsRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
               name: "GetLatestNews",
               routeTemplate: "Api/{controller}/Latest/{count}",
               defaults: new { controller = "News", action = "GetLatestNews", count = RouteParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute(
               name: "GetLatestNewsByCategory",
               routeTemplate: "Api/{controller}/Latest/{count}/{category}",
               defaults: new { controller = "News", action = "GetLatesByCategory" },
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
        }
    }
}