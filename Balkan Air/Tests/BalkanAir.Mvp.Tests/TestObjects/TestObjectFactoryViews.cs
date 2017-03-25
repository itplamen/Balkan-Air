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
    }
}
