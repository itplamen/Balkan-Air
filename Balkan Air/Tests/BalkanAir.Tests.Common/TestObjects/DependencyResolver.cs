namespace BalkanAir.Tests.Common.TestObjects
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http.Dependencies;

    using Api.Controllers;
    using Data.Repositories.Contracts;

    public class DependencyResolver<T> : IDependencyResolver 
        where T : class
    {
        private IRepository<T> repository;

        public IRepository<T> Repository
        {
            get { return this.repository; }
            set { this.repository = value; }
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(AircraftManufacturersController))
            {
                return new AircraftManufacturersController(TestObjectFactoryServices.GetAircraftManufacturersServices().Object);
            }

            if (serviceType == typeof(AircraftsController))
            {
                return new AircraftsController(TestObjectFactoryServices.GetAircraftsServices().Object);
            }

            if (serviceType == typeof(AirportsController))
            {
                return new AirportsController(TestObjectFactoryServices.GetAirportsServices().Object);
            }

            if (serviceType == typeof(CategoriesController))
            {
                return new CategoriesController(TestObjectFactoryServices.GetCategoriesServices().Object);
            }

            if (serviceType == typeof(CountriesController))
            {
                return new CountriesController(TestObjectFactoryServices.GetCountriesServices().Object);
            }

            if (serviceType == typeof(FaresController))
            {
                return new FaresController(TestObjectFactoryServices.GetFaresServices().Object);
            }

            if (serviceType == typeof(FlightsController))
            {
                return new FlightsController(TestObjectFactoryServices.GetLegInstancesServices().Object);
            }

            if (serviceType == typeof(NewsController))
            {
                return new NewsController(
                    TestObjectFactoryServices.GetNewsServices().Object,
                    TestObjectFactoryServices.GetCategoriesServices().Object);
            }

            if (serviceType == typeof(RoutesController))
            {
                return new RoutesController(TestObjectFactoryServices.GetRoutesServices().Object);
            }

            if (serviceType == typeof(TravelClassesController))
            {
                return new TravelClassesController(TestObjectFactoryServices.GetTravelClassesServices().Object);
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }

        public void Dispose()
        {
        }
    }
}
