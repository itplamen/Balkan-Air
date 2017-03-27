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
    public class NewsRouteTests
    {
        private const string CREATE_PATH = "/Api/News/Create/";
        private const string GET_PATH_WITH_INVALID_ACTION = "/Api/New/";
        private const string GET_PATH = "/Api/News/";
        private const string GET_LATEST_NEWS_PATH = "/Api/News/Latest/";
        private const string UPDATE_PATH = "/Api/News/Update/";
        private const string DELETE_PATH = "/Api/News/Delete/";

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void CreateShouldThrowExceptionWithRouteDoesNotExistWhenHttpMethodIsInvalid()
        {
            var newsRequestModel = TestObjectFactoryDataTransferModels.GetValidNewsRequestModel();
            string jsonContent = JsonConvert.SerializeObject(newsRequestModel);

            var invalidHttpMethod = HttpMethod.Get;

            MyWebApi
                .Routes()
                .ShouldMap(CREATE_PATH)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(invalidHttpMethod)
                .To<NewsController>(n => n.Create(newsRequestModel));
        }

        [TestMethod]
        public void CreateShouldMapCorrectAction()
        {
            var newsRequestModel = TestObjectFactoryDataTransferModels.GetValidNewsRequestModel();
            string jsonContent = JsonConvert.SerializeObject(newsRequestModel);

            MyWebApi
                .Routes()
                .ShouldMap(CREATE_PATH)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Post)
                .To<NewsController>(n => n.Create(newsRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetAllShouldThrowExceptionWithRouteDoesNotExistWhenActionIsInvalid()
        {
            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH_WITH_INVALID_ACTION)
                .WithHttpMethod(HttpMethod.Get)
                .To<NewsController>(n => n.All());
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetAllShouldThrowExceptionWithRouteDoesNotExistWhenControllerIsInvalid()
        {
            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(n => n.All());
        }

        [TestMethod]
        public void GetAllShouldMapCorrectAction()
        {
            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH)
                .WithHttpMethod(HttpMethod.Get)
                .To<NewsController>(n => n.All());
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
                .To<NewsController>(n => n.Get(negativeId));
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
                .To<NewsController>(n => n.Get(methodId));
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
                .To<NewsController>(n => n.Get(1));
        }

        [TestMethod]
        public void GetByIdShouldMapCorrectAction()
        {
            var validId = 1;

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + validId)
                .WithHttpMethod(HttpMethod.Get)
                .To<NewsController>(n => n.Get(validId));
        }

        [TestMethod]
        public void GetLatestNewsShouldMapCorrectActionWhenCountIsValid()
        {
            var validCount = 1;

            MyWebApi
                .Routes()
                .ShouldMap(GET_LATEST_NEWS_PATH + validCount)
                .WithHttpMethod(HttpMethod.Get)
                .To<NewsController>(n => n.GetLatestNews(validCount));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetLatestNewsShouldThrowExceptionWithRouteDoesNotExistWhenCountIsNotSet()
        {
            var validCategory = "category";

            MyWebApi
                .Routes()
                .ShouldMap(GET_LATEST_NEWS_PATH + validCategory)
                .WithHttpMethod(HttpMethod.Get)
                .To<NewsController>(n => n.GetLatesByCategory(1, validCategory));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetLatestNewsShouldThrowExceptionWithRouteDoesNotExistWhenCategoryIsNotSet()
        {
            var validCount = 1;

            MyWebApi
                .Routes()
                .ShouldMap(GET_LATEST_NEWS_PATH + validCount)
                .WithHttpMethod(HttpMethod.Get)
                .To<NewsController>(n => n.GetLatesByCategory(validCount, string.Empty));
        }

        [TestMethod]
        public void GetLatestNewsShouldMapCorrectActionWhenCountAndCategoryAreValid()
        {
            var validCount = 1;
            var validCategory = "category";

            MyWebApi
                .Routes()
                .ShouldMap(GET_LATEST_NEWS_PATH + validCount + "/" + validCategory)
                .WithHttpMethod(HttpMethod.Get)
                .To<NewsController>(n => n.GetLatesByCategory(validCount, validCategory));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void UpdateShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNegative()
        {
            var updateRequestModel = TestObjectFactoryDataTransferModels.GetValidUpdateNewsRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateRequestModel);

            var negativeId = -1;

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + negativeId)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Put)
                .To<NewsController>(n => n.Update(negativeId, updateRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void UpdateShouldThrowExceptionWithDifferenParameterWhenIdDoesNotMatch()
        {
            var updateRequestModel = TestObjectFactoryDataTransferModels.GetValidUpdateNewsRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateRequestModel);

            var pathId = 1;
            var methodId = 2;

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + pathId)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Put)
                .To<NewsController>(n => n.Update(methodId, updateRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void UpdateShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNotInteger()
        {
            var updateRequestModel = TestObjectFactoryDataTransferModels.GetValidUpdateNewsRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateRequestModel);

            var notIntegerId = "a";

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + notIntegerId)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Put)
                .To<NewsController>(n => n.Update(updateRequestModel.Id, updateRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void UpdateShouldThrowExceptionWithRouteDoesNotExistWhenHttpMethodIsInvalid()
        {
            var updateRequestModel = TestObjectFactoryDataTransferModels.GetValidUpdateNewsRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateRequestModel);

            var invalidHttpMethod = HttpMethod.Post;

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + updateRequestModel.Id)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(invalidHttpMethod)
                .To<NewsController>(n => n.Update(updateRequestModel.Id, updateRequestModel));
        }

        [TestMethod]
        public void UpdateShouldMapCorrectAction()
        {
            var updateRequestModel = TestObjectFactoryDataTransferModels.GetValidUpdateNewsRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateRequestModel);

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + updateRequestModel.Id)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Put)
                .To<NewsController>(n => n.Update(updateRequestModel.Id, updateRequestModel));
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
                .To<NewsController>(n => n.Delete(negativeId));
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
                .To<NewsController>(n => n.Delete(methodId));
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
                .To<NewsController>(n => n.Delete(1));
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
                .To<NewsController>(n => n.Delete(validId));
        }

        [TestMethod]
        public void DeleteShouldMapCorrectAction()
        {
            var validId = 1;

            MyWebApi
                .Routes()
                .ShouldMap(DELETE_PATH + validId)
                .WithHttpMethod(HttpMethod.Delete)
                .To<NewsController>(n => n.Delete(validId));
        }
    }
}
