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

            aircraftManufacturersManagementView.Setup(a => a.Model)
                .Returns(new AircraftManufacturersManagementViewModel());

            ModelStateDictionary = new ModelStateDictionary();

            aircraftManufacturersManagementView.Setup(a => a.ModelState)
                .Returns(ModelStateDictionary);

            return aircraftManufacturersManagementView;
        } 
    }
}
