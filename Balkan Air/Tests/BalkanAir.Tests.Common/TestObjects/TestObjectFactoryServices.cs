namespace BalkanAir.Tests.Common.TestObjects
{
    using Moq;

    using Data.Models;
    using Services.Data.Contracts;

    public static class TestObjectFactoryServices
    {
        private const int VALID_ID = 1;

        public static Mock<IAircraftManufacturersServices> GetAircraftManufacturersServices()
        {
            var aircraftManufacturersServices = new Mock<IAircraftManufacturersServices>();
            aircraftManufacturersServices.Setup(am => am.AddManufacturer(
                    It.IsAny<AircraftManufacturer>()))
                .Returns(VALID_ID);

            aircraftManufacturersServices.Setup(am => am.GetAll())
                .Returns(TestObjectFactoryDataModels.AircraftManufacturers);

            aircraftManufacturersServices.Setup(am => am.GetManufacturer(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<AircraftManufacturer>(null);

            aircraftManufacturersServices.Setup(am => am.GetManufacturer(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new AircraftManufacturer() { Id = VALID_ID });

            aircraftManufacturersServices.Setup(am => am.UpdateManufacturer(
                    It.Is<int>(i => i != VALID_ID),
                    It.IsAny<AircraftManufacturer>()))
                .Returns<AircraftManufacturer>(null);

            aircraftManufacturersServices.Setup(am => am.UpdateManufacturer(
                    It.Is<int>(i => i == VALID_ID),
                    It.IsAny<AircraftManufacturer>()))
                .Returns(new AircraftManufacturer() { Id = VALID_ID });

            aircraftManufacturersServices.Setup(am => am.DeleteManufacturer(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<AircraftManufacturer>(null);

            aircraftManufacturersServices.Setup(am => am.DeleteManufacturer(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new AircraftManufacturer() { Id = VALID_ID });

            return aircraftManufacturersServices;
        }

        public static Mock<IAircraftsServices> GetAircraftsServices()
        {
            var aircraftsServices = new Mock<IAircraftsServices>();

            aircraftsServices.Setup(a => a.AddAircraft(
                    It.IsAny<Aircraft>()))
                .Returns(VALID_ID);

            aircraftsServices.Setup(a => a.GetAll())
                .Returns(TestObjectFactoryDataModels.Aircrafts);

            aircraftsServices.Setup(a => a.GetAircraft(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<Aircraft>(null);

            aircraftsServices.Setup(a => a.GetAircraft(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new Aircraft() { Id = VALID_ID });

            aircraftsServices.Setup(a => a.UpdateAircraft(
                    It.Is<int>(i => i != VALID_ID),
                    It.IsAny<Aircraft>()))
                .Returns<Aircraft>(null);

            aircraftsServices.Setup(a => a.UpdateAircraft(
                    It.Is<int>(i => i == VALID_ID),
                    It.IsAny<Aircraft>()))
                .Returns(new Aircraft() { Id = VALID_ID });

            aircraftsServices.Setup(a => a.DeleteAircraft(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<Aircraft>(null);

            aircraftsServices.Setup(a => a.DeleteAircraft(
                It.Is<int>(i => i == VALID_ID)))
                .Returns(new Aircraft() { Id = VALID_ID });

            return aircraftsServices;
        }

        public static Mock<IAirportsServices> GetAirportsServices()
        {
            var airportsServices = new Mock<IAirportsServices>();

            airportsServices.Setup(a => a.AddAirport(
                    It.IsAny<Airport>()))
                .Returns(VALID_ID);

            airportsServices.Setup(a => a.GetAll())
                .Returns(TestObjectFactoryDataModels.Airports);

            airportsServices.Setup(a => a.GetAirport(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<Airport>(null);

            airportsServices.Setup(a => a.GetAirport(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new Airport() { Id = VALID_ID });

            airportsServices.Setup(a => a.UpdateAirport(
                    It.Is<int>(i => i != VALID_ID),
                    It.IsAny<Airport>()))
                .Returns<Airport>(null);

            airportsServices.Setup(a => a.UpdateAirport(
                    It.Is<int>(i => i == VALID_ID),
                    It.IsAny<Airport>()))
                .Returns(new Airport() { Id = VALID_ID });

            airportsServices.Setup(a => a.DeleteAirport(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<Airport>(null);

            airportsServices.Setup(a => a.DeleteAirport(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new Airport() { Id = VALID_ID });

            return airportsServices;
        }

        public static Mock<IBaggageServices> GetBaggageServices()
        {
            var baggageServices = new Mock<IBaggageServices>();

            baggageServices.Setup(b => b.AddBaggage(
                    It.IsAny<Baggage>()))
                .Returns(VALID_ID);

            baggageServices.Setup(b => b.GetAll())
                .Returns(TestObjectFactoryDataModels.Baggage);

            baggageServices.Setup(b => b.GetBaggage(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<Baggage>(null);

            baggageServices.Setup(b => b.GetBaggage(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new Baggage() { Id = VALID_ID });

            baggageServices.Setup(b => b.UpdateBaggage(
                    It.Is<int>(i => i != VALID_ID),
                    It.IsAny<Baggage>()))
                .Returns<Baggage>(null);

            baggageServices.Setup(b => b.UpdateBaggage(
                    It.Is<int>(i => i == VALID_ID),
                    It.IsAny<Baggage>()))
                .Returns(new Baggage() { Id = VALID_ID });

            baggageServices.Setup(a => a.DeleteBaggage(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<Baggage>(null);

            baggageServices.Setup(b => b.DeleteBaggage(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new Baggage() { Id = VALID_ID });

            return baggageServices;
        }

        public static Mock<IBookingsServices> GetBookingsServices()
        {
            var bookingsServices = new Mock<IBookingsServices>();

            bookingsServices.Setup(b => b.AddBooking(
                    It.IsAny<Booking>()))
                .Returns(VALID_ID);

            bookingsServices.Setup(b => b.GetAll())
                .Returns(TestObjectFactoryDataModels.Bookings);

            bookingsServices.Setup(b => b.GetBooking(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<Booking>(null);

            bookingsServices.Setup(b => b.GetBooking(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new Booking() { Id = VALID_ID });

            bookingsServices.Setup(b => b.UpdateBooking(
                    It.Is<int>(i => i != VALID_ID),
                    It.IsAny<Booking>()))
                .Returns<Booking>(null);

            bookingsServices.Setup(b => b.UpdateBooking(
                    It.Is<int>(i => i == VALID_ID),
                    It.IsAny<Booking>()))
                .Returns(new Booking() { Id = VALID_ID });

            bookingsServices.Setup(b => b.DeleteBooking(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<Booking>(null);

            bookingsServices.Setup(b => b.DeleteBooking(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new Booking() { Id = VALID_ID });

            return bookingsServices;
        }

        public static Mock<ICategoriesServices> GetCategoriesServices()
        {
            var categoriesServices = new Mock<ICategoriesServices>();

            categoriesServices.Setup(c => c.AddCategory(
                    It.IsAny<Category>()))
                .Returns(VALID_ID);

            categoriesServices.Setup(c => c.GetAll())
                .Returns(TestObjectFactoryDataModels.Categories);

            categoriesServices.Setup(c => c.GetCategory(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<Category>(null);

            categoriesServices.Setup(c => c.GetCategory(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new Category() { Id = VALID_ID });

            categoriesServices.Setup(c => c.UpdateCategory(
                    It.Is<int>(i => i != VALID_ID),
                    It.IsAny<Category>()))
                .Returns<Category>(null);

            categoriesServices.Setup(c => c.UpdateCategory(
                It.Is<int>(i => i == VALID_ID),
                It.IsAny<Category>()))
                .Returns(new Category() { Id = VALID_ID });

            categoriesServices.Setup(c => c.DeleteCategory(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<Category>(null);

            categoriesServices.Setup(c => c.DeleteCategory(
                It.Is<int>(i => i == VALID_ID)))
                .Returns(new Category() { Id = VALID_ID });

            return categoriesServices;
        }

        public static Mock<ICountriesServices> GetCountriesServices()
        {
            var countriesServices = new Mock<ICountriesServices>();

            countriesServices.Setup(c => c.AddCountry(
                    It.IsAny<Country>()))
                .Returns(VALID_ID);

            countriesServices.Setup(c => c.GetAll())
                .Returns(TestObjectFactoryDataModels.Countries);

            countriesServices.Setup(c => c.GetCountry(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<Country>(null);

            countriesServices.Setup(c => c.GetCountry(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new Country() { Id = VALID_ID });

            countriesServices.Setup(c => c.UpdateCountry(
                    It.Is<int>(i => i != VALID_ID),
                    It.IsAny<Country>()))
                .Returns<Country>(null);

            countriesServices.Setup(c => c.UpdateCountry(
                    It.Is<int>(i => i == VALID_ID),
                    It.IsAny<Country>()))
                .Returns(new Country() { Id = VALID_ID });

            countriesServices.Setup(c => c.DeleteCountry(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<Country>(null);

            countriesServices.Setup(c => c.DeleteCountry(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new Country() { Id = VALID_ID });

            return countriesServices;
        }

        public static Mock<IFaresServices> GetFaresServices()
        {
            var faresServices = new Mock<IFaresServices>();

            faresServices.Setup(f => f.AddFare(
                    It.IsAny<Fare>()))
                .Returns(VALID_ID);

            faresServices.Setup(f => f.GetAll())
                .Returns(TestObjectFactoryDataModels.Fares);

            faresServices.Setup(f => f.GetFare(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<Fare>(null);

            faresServices.Setup(f => f.GetFare(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new Fare() { Id = VALID_ID });

            faresServices.Setup(f => f.UpdateFare(
                    It.Is<int>(i => i != VALID_ID),
                    It.IsAny<Fare>()))
                .Returns<Fare>(null);

            faresServices.Setup(f => f.UpdateFare(
                    It.Is<int>(i => i == VALID_ID),
                    It.IsAny<Fare>()))
                .Returns(new Fare() { Id = VALID_ID });

            faresServices.Setup(f => f.DeleteFare(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<Fare>(null);

            faresServices.Setup(f => f.DeleteFare(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new Fare() { Id = VALID_ID });

            return faresServices;
        }

        public static Mock<IFlightsServices> GetFlightsServices()
        {
            var flightsServices = new Mock<IFlightsServices>();

            flightsServices.Setup(f => f.AddFlight(
                    It.IsAny<Flight>()))
                .Returns(VALID_ID);

            flightsServices.Setup(f => f.GetAll())
                .Returns(TestObjectFactoryDataModels.Flights);

            flightsServices.Setup(f => f.GetFlight(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<Flight>(null);

            flightsServices.Setup(a => a.GetFlight(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new Flight() { Id = VALID_ID });

            flightsServices.Setup(a => a.UpdateFlight(
                    It.Is<int>(i => i != VALID_ID),
                    It.IsAny<Flight>()))
                .Returns<Flight>(null);

            flightsServices.Setup(a => a.UpdateFlight(
                    It.Is<int>(i => i == VALID_ID),
                    It.IsAny<Flight>()))
                .Returns(new Flight() { Id = VALID_ID });

            flightsServices.Setup(a => a.DeleteFlight(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<Flight>(null);

            flightsServices.Setup(a => a.DeleteFlight(
                It.Is<int>(i => i == VALID_ID)))
                .Returns(new Flight() { Id = VALID_ID });

            return flightsServices;
        }

        public static Mock<IFlightLegsServices> GetFlightLegsServices()
        {
            var flightLegsServices = new Mock<IFlightLegsServices>();

            flightLegsServices.Setup(f => f.AddFlightLeg(
                    It.IsAny<FlightLeg>()))
                .Returns(VALID_ID);

            flightLegsServices.Setup(f => f.GetAll())
                .Returns(TestObjectFactoryDataModels.FlightLegs);

            flightLegsServices.Setup(f => f.GetFlightLeg(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<FlightLeg>(null);

            flightLegsServices.Setup(a => a.GetFlightLeg(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new FlightLeg() { Id = VALID_ID });

            flightLegsServices.Setup(a => a.UpdateFlightLeg(
                    It.Is<int>(i => i != VALID_ID),
                    It.IsAny<FlightLeg>()))
                .Returns<FlightLeg>(null);

            flightLegsServices.Setup(a => a.UpdateFlightLeg(
                    It.Is<int>(i => i == VALID_ID),
                    It.IsAny<FlightLeg>()))
                .Returns(new FlightLeg() { Id = VALID_ID });

            flightLegsServices.Setup(a => a.DeleteFlightLeg(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<FlightLeg>(null);

            flightLegsServices.Setup(a => a.DeleteFlightLeg(
                It.Is<int>(i => i == VALID_ID)))
                .Returns(new FlightLeg() { Id = VALID_ID });

            return flightLegsServices;
        }

        public static Mock<ILegInstancesServices> GetLegInstancesServices()
        {
            var legInstancesServices = new Mock<ILegInstancesServices>();

            legInstancesServices.Setup(l => l.AddLegInstance(
                    It.IsAny<LegInstance>()))
                .Returns(VALID_ID);

            legInstancesServices.Setup(l => l.GetAll())
                .Returns(TestObjectFactoryDataModels.LegInstances);

            legInstancesServices.Setup(l => l.GetLegInstance(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<LegInstance>(null);

            legInstancesServices.Setup(l => l.GetLegInstance(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new LegInstance() { Id = VALID_ID });

            legInstancesServices.Setup(l => l.UpdateLegInstance(
                    It.Is<int>(i => i != VALID_ID),
                    It.IsAny<LegInstance>()))
                .Returns<LegInstance>(null);

            legInstancesServices.Setup(l => l.UpdateLegInstance(
                    It.Is<int>(i => i == VALID_ID),
                    It.IsAny<LegInstance>()))
                .Returns(new LegInstance() { Id = VALID_ID });

            legInstancesServices.Setup(l => l.DeleteLegInstance(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<LegInstance>(null);

            legInstancesServices.Setup(l => l.DeleteLegInstance(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new LegInstance() { Id = VALID_ID });

            return legInstancesServices;
        }

        public static Mock<INewsServices> GetNewsServices()
        {
            var newsServices = new Mock<INewsServices>();

            newsServices.Setup(n => n.AddNews(
                    It.IsAny<News>()))
                .Returns(VALID_ID);

            newsServices.Setup(n => n.GetAll())
                .Returns(TestObjectFactoryDataModels.News);

            newsServices.Setup(n => n.GetNews(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<News>(null);

            newsServices.Setup(n => n.GetNews(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new News() { Id = VALID_ID });

            newsServices.Setup(n => n.UpdateNews(
                    It.Is<int>(i => i != VALID_ID),
                    It.IsAny<News>()))
                .Returns<News>(null);

            newsServices.Setup(n => n.UpdateNews(
                    It.Is<int>(i => i == VALID_ID),
                    It.IsAny<News>()))
                .Returns(new News() { Id = VALID_ID });

            newsServices.Setup(n => n.DeleteNews(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<News>(null);

            newsServices.Setup(n => n.DeleteNews(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new News() { Id = VALID_ID });

            return newsServices;
        }

        public static Mock<IRoutesServices> GetRoutesServices()
        {
            var routesServices = new Mock<IRoutesServices>();

            routesServices.Setup(r => r.AddRoute(
                    It.IsAny<Route>()))
                .Returns(VALID_ID);

            routesServices.Setup(r => r.GetAll())
                .Returns(TestObjectFactoryDataModels.Routes);

            routesServices.Setup(r => r.GetRoute(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<Route>(null);

            routesServices.Setup(r => r.GetRoute(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new Route() { Id = VALID_ID });

            routesServices.Setup(r => r.UpdateRoute(
                    It.Is<int>(i => i != VALID_ID),
                    It.IsAny<Route>()))
                .Returns<Route>(null);

            routesServices.Setup(l => l.UpdateRoute(
                    It.Is<int>(i => i == VALID_ID),
                    It.IsAny<Route>()))
                .Returns(new Route() { Id = VALID_ID });

            routesServices.Setup(l => l.DeleteRoute(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<Route>(null);

            routesServices.Setup(r => r.DeleteRoute(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new Route() { Id = VALID_ID });

            return routesServices;
        }
    }
}
