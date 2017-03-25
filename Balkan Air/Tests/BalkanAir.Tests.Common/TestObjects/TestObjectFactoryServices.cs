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

        public static Mock<ILegInstancesServices> GetFlightServices()
        {
            var flightsServices = new Mock<ILegInstancesServices>();

            flightsServices.Setup(f => f.AddLegInstance(
                    It.IsAny<LegInstance>()))
                .Returns(VALID_ID);

            flightsServices.Setup(f => f.GetAll())
                .Returns(TestObjectFactoryDataModels.Flights);

            flightsServices.Setup(f => f.UpdateLegInstance(
                    It.Is<int>(i => i != VALID_ID),
                    It.IsAny<LegInstance>()))
                .Returns<LegInstance>(null);

            flightsServices.Setup(f => f.UpdateLegInstance(
                    It.Is<int>(i => i == VALID_ID),
                    It.IsAny<LegInstance>()))
                .Returns(new LegInstance() { Id = VALID_ID });

            flightsServices.Setup(f => f.DeleteLegInstance(
                    It.Is<int>(i => i != VALID_ID)))
                .Returns<LegInstance>(null);

            flightsServices.Setup(f => f.DeleteLegInstance(
                    It.Is<int>(i => i == VALID_ID)))
                .Returns(new LegInstance() { Id = VALID_ID });

            return flightsServices;
        }
    }
}
