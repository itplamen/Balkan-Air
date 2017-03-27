namespace BalkanAir.Api.Tests.RouteTests
{
    using System.Net.Http;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MyTested.WebApi;
    using MyTested.WebApi.Exceptions;

    using Newtonsoft.Json;

    using Controllers;
    using TestObjects;

    [TestClass]
    public class FaresRouteTests
    {
        private const string CREATE_PATH = "/Api/Fares/Create/";
        private const string GET_PATH_WITH_INVALID_ACTION = "/Api/Fare/";
        private const string GET_PATH = "/Api/Fares/";
        private const string UPDATE_PATH = "/Api/Fares/Update/";
        private const string DELETE_PATH = "/Api/Fares/Delete/";

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void CreateShouldThrowExceptionWithRouteDoesNotExistWhenHttpMethodIsInvalid()
        {
            var fareRequestModel = TestObjectFactoryDataTransferModels.GetValidFareRequestModel();
            string jsonContent = JsonConvert.SerializeObject(fareRequestModel);

            var invalidHttpMethod = HttpMethod.Get;

            MyWebApi
                .Routes()
                .ShouldMap(CREATE_PATH)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(invalidHttpMethod)
                .To<FaresController>(f => f.Create(fareRequestModel));
        }

        [TestMethod]
        public void CreateShouldMapCorrectAction()
        {
            var fareRequestModel = TestObjectFactoryDataTransferModels.GetValidFareRequestModel();
            string jsonContent = JsonConvert.SerializeObject(fareRequestModel);

            MyWebApi
                .Routes()
                .ShouldMap(CREATE_PATH)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Post)
                .To<FaresController>(f => f.Create(fareRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetAllShouldThrowExceptionWithRouteDoesNotExistWhenActionIsInvalid()
        {
            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH_WITH_INVALID_ACTION)
                .WithHttpMethod(HttpMethod.Get)
                .To<FaresController>(f => f.All());
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetAllShouldThrowExceptionWithRouteDoesNotExistWhenControllerIsInvalid()
        {
            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.All());
        }

        [TestMethod]
        public void GetAllShouldMapCorrectAction()
        {
            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH)
                .WithHttpMethod(HttpMethod.Get)
                .To<FaresController>(f => f.All());
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByIdShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNegative()
        {
            var negativeId = -1;

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + negativeId)
                .WithHttpMethod(HttpMethod.Get)
                .To<FaresController>(f => f.Get(negativeId));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByIdShouldThrowExceptionWithDifferenParameterWhenIdDoesNotMatch()
        {
            var pathId = 1;
            var methodId = 2;

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + pathId)
                .WithHttpMethod(HttpMethod.Get)
                .To<FaresController>(f => f.Get(methodId));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByIdShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNotInteger()
        {
            var notIntegerId = "a";

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + notIntegerId)
                .WithHttpMethod(HttpMethod.Get)
                .To<FaresController>(f => f.Get(1));
        }

        [TestMethod]
        public void GetByIdShouldMapCorrectAction()
        {
            var validId = 1;

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + validId)
                .WithHttpMethod(HttpMethod.Get)
                .To<FaresController>(f => f.Get(validId));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByRouteShouldThrowExceptionWithRouteDoesNotExistWhenOriginAbbreviationIsTooLong()
        {
            var longOriginAbbreviation = "Too long abbreviation";
            var validDestinationAbbreviation = "sof";

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + longOriginAbbreviation + "/" + validDestinationAbbreviation)
                .WithHttpMethod(HttpMethod.Get)
                .To<FaresController>(f => f.GetByRoute(longOriginAbbreviation, validDestinationAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByRouteShouldThrowExceptionWithRouteDoesNotExistWhenOriginAbbreviationIsTooShort()
        {
            var shortOriginAbbreviation = "s";
            var validDestinationAbbreviation = "sof";

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + shortOriginAbbreviation + "/" + validDestinationAbbreviation)
                .WithHttpMethod(HttpMethod.Get)
                .To<FaresController>(f => f.GetByRoute(shortOriginAbbreviation, validDestinationAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByRouteShouldThrowExceptionWithRouteDoesNotExistWhenOriginAbbreviationIsNull()
        {
            var nullableOriginAbbreviation = string.Empty;
            var validDestinationAbbreviation = "sof";

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + nullableOriginAbbreviation + "/" + validDestinationAbbreviation)
                .WithHttpMethod(HttpMethod.Get)
                .To<FaresController>(f => f.GetByRoute(nullableOriginAbbreviation, validDestinationAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByRouteShouldThrowExceptionWithRouteDoesNotExistWhenOriginAbbreviationIsNotString()
        {
            var invalidOriginAbbreviation = "1";
            var validDestinationAbbreviation = "sof";

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + invalidOriginAbbreviation + "/" + validDestinationAbbreviation)
                .WithHttpMethod(HttpMethod.Get)
                .To<FaresController>(f => f.GetByRoute(invalidOriginAbbreviation, validDestinationAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByRouteShouldThrowExceptionWithRouteDoesNotExistWhenDestinationAbbreviationIsTooLong()
        {
            var validOriginAbbreviation = "sof";
            var longDestinationAbbreviation = "Tooo long abbreviation";

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + validOriginAbbreviation + "/" + longDestinationAbbreviation)
                .WithHttpMethod(HttpMethod.Get)
                .To<FaresController>(f => f.GetByRoute(validOriginAbbreviation, longDestinationAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByRouteShouldThrowExceptionWithRouteDoesNotExistWhenDestinationAbbreviationIsTooShort()
        {
            var validOriginAbbreviation = "sof";
            var shortDestinationAbbreviation = "s";

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + validOriginAbbreviation + "/" + shortDestinationAbbreviation)
                .WithHttpMethod(HttpMethod.Get)
                .To<FaresController>(f => f.GetByRoute(validOriginAbbreviation, shortDestinationAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByRouteShouldThrowExceptionWithRouteDoesNotExistWhenDestinationAbbreviationIsNull()
        {
            var validOriginAbbreviation = "sof";
            var nullableDestinationAbbreviation = string.Empty;

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + validOriginAbbreviation + "/" + nullableDestinationAbbreviation)
                .WithHttpMethod(HttpMethod.Get)
                .To<FaresController>(f => f.GetByRoute(validOriginAbbreviation, nullableDestinationAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByRouteShouldThrowExceptionWithRouteDoesNotExistWhenDestinationAbbreviationIsNotString()
        {
            var validOriginAbbreviation = "sof";
            var notStringDestinationAbbreviation = "1";

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + validOriginAbbreviation + "/" + notStringDestinationAbbreviation)
                .WithHttpMethod(HttpMethod.Get)
                .To<FaresController>(f => f.GetByRoute(validOriginAbbreviation, notStringDestinationAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void UpdateShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNegative()
        {
            var updateRequestModel = TestObjectFactoryDataTransferModels.GetValidUpdateFareRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateRequestModel);

            var negativeId = -1;

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + negativeId)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Put)
                .To<FaresController>(f => f.Update(negativeId, updateRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void UpdateShouldThrowExceptionWithDifferenParameterWhenIdDoesNotMatch()
        {
            var updateRequestModel = TestObjectFactoryDataTransferModels.GetValidUpdateFareRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateRequestModel);

            var pathId = 1;
            var methodId = 2;

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + pathId)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Put)
                .To<FaresController>(f => f.Update(methodId, updateRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void UpdateShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNotInteger()
        {
            var updateRequestModel = TestObjectFactoryDataTransferModels.GetValidUpdateFareRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateRequestModel);

            var notIntegerId = "a";

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + notIntegerId)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Put)
                .To<FaresController>(f => f.Update(updateRequestModel.Id, updateRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void UpdateShouldThrowExceptionWithRouteDoesNotExistWhenHttpMethodIsInvalid()
        {
            var updateRequestModel = TestObjectFactoryDataTransferModels.GetValidUpdateFareRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateRequestModel);

            var invalidHttpMethod = HttpMethod.Post;

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + updateRequestModel.Id)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(invalidHttpMethod)
                .To<FaresController>(f => f.Update(updateRequestModel.Id, updateRequestModel));
        }

        [TestMethod]
        public void UpdateShouldMapCorrectAction()
        {
            var updateRequestModel = TestObjectFactoryDataTransferModels.GetValidUpdateFareRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateRequestModel);

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + updateRequestModel.Id)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Put)
                .To<FaresController>(f => f.Update(updateRequestModel.Id, updateRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void DeleteShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNegative()
        {
            var negativeId = -1;

            MyWebApi
                .Routes()
                .ShouldMap(DELETE_PATH + negativeId)
                .WithHttpMethod(HttpMethod.Delete)
                .To<FaresController>(f => f.Delete(negativeId));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void DeleteShouldThrowExceptionWithDifferenParameterWhenIdDoesNotMatch()
        {
            var pathId = 1;
            var methodId = 2;

            MyWebApi
                .Routes()
                .ShouldMap(DELETE_PATH + pathId)
                .WithHttpMethod(HttpMethod.Delete)
                .To<AirportsController>(a => a.Delete(methodId));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void DeleteShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNotInteger()
        {
            var notIntegerId = "a";

            MyWebApi
                .Routes()
                .ShouldMap(DELETE_PATH + notIntegerId)
                .WithHttpMethod(HttpMethod.Delete)
                .To<FaresController>(f => f.Delete(1));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void DeleteShouldThrowExceptionWithRouteDoesNotExistWhenHttpMethodIsInvalid()
        {
            var validId = 1;
            var invalidHttpMethod = HttpMethod.Post;

            MyWebApi
                .Routes()
                .ShouldMap(DELETE_PATH + validId)
                .WithHttpMethod(invalidHttpMethod)
                .To<FaresController>(f => f.Delete(validId));
        }

        [TestMethod]
        public void DeleteShouldMapCorrectAction()
        {
            var validId = 1;

            MyWebApi
                .Routes()
                .ShouldMap(DELETE_PATH + validId)
                .WithHttpMethod(HttpMethod.Delete)
                .To<FaresController>(f => f.Delete(validId));
        }
    }
}
