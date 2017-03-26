namespace BalkanAir.Mvp.Tests.TestObjects
{
    using System.Web.ModelBinding;

    using Moq;

    using Models.Administration;
    using ViewContracts.Administration;

    public static class TestObjectFactoryViews
    {
        public static ModelStateDictionary ModelStateDictionary { get; set; }

        public static Mock<IAircraftManufacturersManagementView> GetAircraftManufacturersManagementView()
        {
            var aircraftManufacturersManagementView = new Mock<IAircraftManufacturersManagementView>();

            ModelStateDictionary = new ModelStateDictionary();

            aircraftManufacturersManagementView.Setup(a => a.Model)
                .Returns(new AircraftManufacturersManagementViewModel());
 
            aircraftManufacturersManagementView.Setup(a => a.ModelState)
                .Returns(ModelStateDictionary);

            return aircraftManufacturersManagementView;
        } 

        public static Mock<IAircraftsManagementView> GetAircraftsManagementView()
        {
            var aircraftsManagementView = new Mock<IAircraftsManagementView>();

            ModelStateDictionary = new ModelStateDictionary();

            aircraftsManagementView.Setup(a => a.Model)
                .Returns(new AircraftsManagementViewModels());

            aircraftsManagementView.Setup(a => a.ModelState)
                .Returns(ModelStateDictionary);

            return aircraftsManagementView;
        }

        public static Mock<IAirportsManagementView> GetAirportsManagementView()
        {
            var airportsManagementView = new Mock<IAirportsManagementView>();

            ModelStateDictionary = new ModelStateDictionary();

            airportsManagementView.Setup(a => a.Model)
                .Returns(new AirportsManagementViewModel());

            airportsManagementView.Setup(a => a.ModelState)
                .Returns(ModelStateDictionary);

            return airportsManagementView;
        }

        public static Mock<IBaggageManagementView> GetBaggageManagementView()
        {
            var baggageManagementView = new Mock<IBaggageManagementView>();

            ModelStateDictionary = new ModelStateDictionary();

            baggageManagementView.Setup(b => b.Model)
                .Returns(new BaggageManagementViewModel());

            baggageManagementView.Setup(b => b.ModelState)
                .Returns(ModelStateDictionary);

            return baggageManagementView;
        }

        public static Mock<IBookingsManagementView> GetBookingsManagementView()
        {
            var bookingsManagementView = new Mock<IBookingsManagementView>();

            ModelStateDictionary = new ModelStateDictionary();

            bookingsManagementView.Setup(b => b.Model)
                .Returns(new BookingsManagementViewModel());

            bookingsManagementView.Setup(b => b.ModelState)
                .Returns(ModelStateDictionary);

            return bookingsManagementView;
        }

        public static Mock<ICategoriesManagementView> GetCategoriesManagementView()
        {
            var categoriesManagementView = new Mock<ICategoriesManagementView>();

            ModelStateDictionary = new ModelStateDictionary();

            categoriesManagementView.Setup(b => b.Model)
                .Returns(new CategoriesManagementViewModel());

            categoriesManagementView.Setup(b => b.ModelState)
                .Returns(ModelStateDictionary);

            return categoriesManagementView;
        }

        public static Mock<IFlightLegsManagementView> GetFlightLegsManagementView()
        {
            var flightLegsManagementView = new Mock<IFlightLegsManagementView>();

            ModelStateDictionary = new ModelStateDictionary();

            flightLegsManagementView.Setup(f => f.Model)
                .Returns(new FlightLegsManagementViewModel());

            flightLegsManagementView.Setup(f => f.ModelState)
                .Returns(ModelStateDictionary);

            return flightLegsManagementView;
        }
    }
}
