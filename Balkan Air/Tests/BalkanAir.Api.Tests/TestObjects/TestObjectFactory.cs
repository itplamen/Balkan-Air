namespace BalkanAir.Api.Tests.TestObjects
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;

    using Data.Models;
    using Services.Data.Contracts;
    using Web.Areas.Api.Models.AircraftManufacturers;
    using Web.Areas.Api.Models.Aircrafts;
    using Web.Areas.Api.Models.Airports;
    using Web.Areas.Api.Models.Categories;
    using Web.Areas.Api.Models.Countries;

    public static class TestObjectFactory
    {
        private const int CORRECT_ID = 1;
        private const int NOT_FOUND_ID = 2;

        private static IQueryable<AircraftManufacturer> aircraftManufacturers = new List<AircraftManufacturer>()
        {
            new AircraftManufacturer()
            {
                Id = 1,
                Name = "Manufacturer Test"
            }
        }.AsQueryable();

        private static AircraftManufacturer nullableAircraftManufacturer = null;

        private static IQueryable<Aircraft> aircrafts = new List<Aircraft>()
        {
            new Aircraft()
            {
                Id = 1,
                Model = "Aircraft Model Test",
                TotalSeats = 1
            }
        }.AsQueryable();

        private static Aircraft nullabelAircraft = null;

        private static IQueryable<Airport> airports = new List<Airport>()
        {
            new Airport()
            {   Id = 1,
                Name = "Test Name",
                Abbreviation = "ABC",
                CountryId = 1
            }
        }.AsQueryable();

        private static Airport nullableAirport = null;

        private static IQueryable<Category> categories = new List<Category>()
        {
            new Category()
            {
                Id = 1,
                Name = "Category Test"
            }
        }.AsQueryable();

        private static Category nullableCategory = null;

        private static IQueryable<Country> countries = new List<Country>()
        {
            new Country()
            {
                Id = 1,
                Name = "Country Test",
                Abbreviation = "CT"
            }
        }.AsQueryable();

        private static Country nullableCountry = null;

        public static IAircraftManufacturersServices GetAircraftManufacturersServices()
        {
            var aircraftManufacturersServices = new Mock<IAircraftManufacturersServices>();

            aircraftManufacturersServices.Setup(am => am.AddManufacturer(
                    It.IsAny<AircraftManufacturer>()))
                .Returns(1);

            aircraftManufacturersServices.Setup(am => am.GetAll())
                .Returns(aircraftManufacturers);

            aircraftManufacturersServices.Setup(am => am.UpdateManufacturer(
                    It.Is<int>(i => i >= NOT_FOUND_ID),
                    It.IsAny<AircraftManufacturer>()))
                .Returns(nullableAircraftManufacturer);

            aircraftManufacturersServices.Setup(am => am.UpdateManufacturer(
                    It.Is<int>(i => i == CORRECT_ID),
                    It.IsAny<AircraftManufacturer>()))
                .Returns(new AircraftManufacturer() { Id = 1 });

            aircraftManufacturersServices.Setup(am => am.DeleteManufacturer(
                    It.Is<int>(i => i >= NOT_FOUND_ID)))
                .Returns(nullableAircraftManufacturer);

            aircraftManufacturersServices.Setup(am => am.DeleteManufacturer(
                    It.Is<int>(i => i == CORRECT_ID)))
                .Returns(new AircraftManufacturer() { Id = 1 });

            return aircraftManufacturersServices.Object;
        }

        public static IAircraftsServices GetAircraftsServices()
        {
            var aircraftsServices = new Mock<IAircraftsServices>();

            aircraftsServices.Setup(a => a.AddAircraft(
                    It.IsAny<Aircraft>()))
                .Returns(1);

            aircraftsServices.Setup(a => a.GetAll())
                .Returns(aircrafts);

            aircraftsServices.Setup(a => a.UpdateAircraft(
                    It.Is<int>(i => i >= NOT_FOUND_ID),
                    It.IsAny<Aircraft>()))
                .Returns(nullabelAircraft);

            aircraftsServices.Setup(a => a.UpdateAircraft(
                    It.Is<int>(i => i == CORRECT_ID),
                    It.IsAny<Aircraft>()))
                .Returns(new Aircraft() { Id = 1 });

            aircraftsServices.Setup(a => a.DeleteAircraft(
                    It.Is<int>(i => i >= NOT_FOUND_ID)))
                .Returns(nullabelAircraft);

            aircraftsServices.Setup(a => a.DeleteAircraft(
                It.Is<int>(i => i == CORRECT_ID)))
                .Returns(new Aircraft() { Id = 1 });

            return aircraftsServices.Object;
        }

        public static IAirportsServices GetAirportsServices()
        {
            var airportsServices = new Mock<IAirportsServices>();

            airportsServices.Setup(a => a.AddAirport(
                    It.IsAny<Airport>()))
                .Returns(1);

            airportsServices.Setup(a => a.GetAll())
                .Returns(airports);

            airportsServices.Setup(a => a.UpdateAirport(
                    It.Is<int>(i => i >= NOT_FOUND_ID),
                    It.IsAny<Airport>()))
                .Returns(nullableAirport);
            
            airportsServices.Setup(a => a.UpdateAirport(
                    It.Is<int>(i => i == CORRECT_ID),
                    It.IsAny<Airport>()))
                .Returns(new Airport() { Id = 1 });

            airportsServices.Setup(a => a.DeleteAirport(
                    It.Is<int>(i => i >= NOT_FOUND_ID)))
                .Returns(nullableAirport);

            airportsServices.Setup(a => a.DeleteAirport(
                    It.Is<int>(i => i == CORRECT_ID)))
                .Returns(new Airport() { Id = 1 });

            return airportsServices.Object;
        }

        public static ICategoriesServices GetCategoriesServices()
        {
            var categoriesServices = new Mock<ICategoriesServices>();

            categoriesServices.Setup(c => c.AddCategory(
                    It.IsAny<Category>()))
                .Returns(1);

            categoriesServices.Setup(c => c.GetAll())
                .Returns(categories);

            categoriesServices.Setup(c => c.UpdateCategory(
                    It.Is<int>(i => i >= NOT_FOUND_ID),
                    It.IsAny<Category>()))
                .Returns(nullableCategory);

            categoriesServices.Setup(c => c.UpdateCategory(
                It.Is<int>(i => i == CORRECT_ID),
                It.IsAny<Category>()))
                .Returns(new Category() { Id = 1 });

            categoriesServices.Setup(c => c.DeleteCategory(
                    It.Is<int>(i => i >= NOT_FOUND_ID)))
                .Returns(nullableCategory);

            categoriesServices.Setup(c => c.DeleteCategory(
                It.Is<int>(i => i == CORRECT_ID)))
                .Returns(new Category() { Id = 1 });

            return categoriesServices.Object;
        }

        public static ICountriesServices GetCountriesServices()
        {
            var countriesServices = new Mock<ICountriesServices>();

            countriesServices.Setup(c => c.AddCountry(
                    It.IsAny<Country>()))
                .Returns(1);

            countriesServices.Setup(c => c.GetAll())
                .Returns(countries);

            countriesServices.Setup(c => c.UpdateCountry(
                    It.Is<int>(i => i >= NOT_FOUND_ID),
                    It.IsAny<Country>()))
                .Returns(nullableCountry);

            countriesServices.Setup(c => c.UpdateCountry(
                    It.Is<int>(i => i == CORRECT_ID),
                    It.IsAny<Country>()))
                .Returns(new Country() { Id = 1 });

            countriesServices.Setup(c => c.DeleteCountry(
                    It.Is<int>(i => i >= NOT_FOUND_ID)))
                .Returns(nullableCountry);

            countriesServices.Setup(c => c.DeleteCountry(
                It.Is<int>(i => i == CORRECT_ID)))
                .Returns(new Country() { Id = 1 });

            return countriesServices.Object;
        }

        public static AircraftManufacturerRequestModel GetInvalidAircraftManufacturerRequestModel()
        {
            return new AircraftManufacturerRequestModel() { Name = "TOOOOOOOOOO LOOOOONG NAAAAAAAAME TEST" };
        }

        public static AircraftManufacturerRequestModel GetValidAircraftManufacturerRequestModel()
        {
            return new AircraftManufacturerRequestModel() { Name = "Manufacturer Test" };
        }

        public static UpdateAircraftManufacturerRequestModel GetInvalidUpdateAircraftManufacturerRequestModel()
        {
            return new UpdateAircraftManufacturerRequestModel() { Name = "OOOOOOOOOO LOOOOONG NAAAAAAAAME TEST" };
        }

        public static UpdateAircraftManufacturerRequestModel GetValidUpdateAircraftManufacturerRequestModel()
        {
            return new UpdateAircraftManufacturerRequestModel()
            {
                Id = 1,
                Name = "Manufacturer Test",
                IsDeleted = true
            };
        }

        public static AircraftRequesModel GetInvalidAircraftRequesModel()
        {
            return new AircraftRequesModel() { Model = "Model Test" };
        }

        public static AircraftRequesModel GetValidAircraftRequesModel()
        {
            return new AircraftRequesModel()
            {
                Model = "Model Test",
                TotalSeats = 180,
                AircraftManufacturerId = 1
            };
        }

        public static UpdateAircraftRequestModel GetInvalidUpdateAircraftRequestModel()
        {
            return new UpdateAircraftRequestModel() { Model = "Model Test" };
        }

        public static UpdateAircraftRequestModel GetValidUpdateAircraftRequestModel()
        {
            return new UpdateAircraftRequestModel()
            {
                Id = 1,
                Model = "Model Test",
                TotalSeats = 180,
                IsDeleted = true,
                AircraftManufacturerId = 1
            };
        }

        public static AirportRequestModel GetInvalidAirportRequesModel()
        {
            return new AirportRequestModel() { Abbreviation = "abc" };
        }

        public static AirportRequestModel GetValidAirportRequesModel()
        {
            return new AirportRequestModel()
            {
                Name = "Airport Test",
                Abbreviation = "ABC",
                CountryId = 1
            };
        }

        public static UpdateAirportRequestModel GetInvalidUpdateAirportRequestModel()
        {
            return new UpdateAirportRequestModel() { Abbreviation = "abc" };
        }

        public static UpdateAirportRequestModel GetValidUpdateAirportRequestModel()
        {
            return new UpdateAirportRequestModel()
            {
                Id = 1,
                Name = "Test Name Update",
                Abbreviation = "ABC",
                IsDeleted = false,
                CountryId = 1
            };
        }

        // Return category without name, so that the model can be invalid.
        public static CategoryRequestModel GetInvalidCategoryRequesModel()
        {
            return new CategoryRequestModel();
        }

        public static CategoryRequestModel GetValidCategoryRequesModel()
        {
            return new CategoryRequestModel() { Name = "Category Test" };
        }

        public static UpdateCategoryRequestModel GetInvalidUpdateCategoryRequestModel()
        {
            return new UpdateCategoryRequestModel() {IsDeleted = false };
        }

        public static UpdateCategoryRequestModel GetValidUpdateCategoryRequestModel()
        {
            return new UpdateCategoryRequestModel()
            {
                Id = 1,
                Name = "Category Test",
                IsDeleted = true
            };
        }

        public static CountryRequestModel GetInvalidCountryRequesModel()
        {
            return new CountryRequestModel() { Abbreviation = "PP" };
        }

        public static CountryRequestModel GetValidCountryRequesModel()
        {
            return new CountryRequestModel()
            {
                Name = "Country Test",
                Abbreviation = "CT"
            };
        }

        public static UpdateCountryRequestModel GetInvalidUpdateCountryRequestModel()
        {
            return new UpdateCountryRequestModel() { Abbreviation = "PP" };
        }

        public static UpdateCountryRequestModel GetValidUpdateCountryRequestModel()
        {
            return new UpdateCountryRequestModel()
            {
                Id = 1,
                Name = "Country Test",
                Abbreviation = "CT",
                IsDeleted = true
            };
        }
    } 
}
