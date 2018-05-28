using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using System.Web.OData;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Query;
using System.Web.OData.Results;
using System.Web.OData.Routing;
using System.Web.Routing;
using Golf.Product.Controllers;
using Golf.Product.DataAccessLayer;
using Golf.Product.Helpers;
using Golf.Product.Model;
using Microsoft.OData.Edm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Golf.Product.Tests
{
    [TestClass]
    public class CategoriesControllerTest
    {
        private static Mock<DbSet<Category>> GetMockDbSet(IQueryable<Category> data)
        {
            //Setup requried for DBSet
            var mockSet = new Mock<DbSet<Category>>();
            mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);

            return mockSet;
        }


        [TestMethod]
        public void GetAllCategoriesReturnsData_AndHttpOK()
        {
            var categoryDescription1 = "Test Description 1";
            var categoryDescription2 = "Test Description 2";
            var categoryDescription3 = "Test Description 3";


            //arrange
            var data = new List<Category>()
            {
                new Category()
                    {Description = categoryDescription1},
                new Category()
                    {Description = categoryDescription2},
                new Category()
                    {Description = categoryDescription3}
            }.AsQueryable();

            var mockContext = new Mock<GolfProductDbContext>();
            mockContext.Setup(m => m.Categories).Returns(GetMockDbSet(data).Object);

            var controller = new CategoriesController(mockContext.Object);

            //act
            var actionResult = controller.Get() as OkNegotiatedContentResult<DbSet<Category>>;


            //assert  
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<DbSet<Category>>));
            Assert.IsNotNull(actionResult?.Content);
            Assert.AreEqual(3, actionResult.Content.Count());

            //I'm not testing for order, so no expectations on what order these will be returned in
            Assert.IsTrue(actionResult.Content.Any(c => c.Description == categoryDescription1));
            Assert.IsTrue(actionResult.Content.Any(c => c.Description == categoryDescription2));
            Assert.IsTrue(actionResult.Content.Any(c => c.Description == categoryDescription3));
        }

        [TestMethod]
        public void GetAllCategoriesWithNoDataReturnsHttpOK()
        {
            //arrange
            var data = new List<Category>()
            {
            }.AsQueryable();


            var mockContext = new Mock<GolfProductDbContext>();
            mockContext.Setup(m => m.Categories).Returns(GetMockDbSet(data).Object);

            var controller = new CategoriesController(mockContext.Object);

            //act
            var actionResult = controller.Get() as OkNegotiatedContentResult<DbSet<Category>>;


            //assert  
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<DbSet<Category>>));
            Assert.IsNotNull(actionResult?.Content);
            Assert.AreEqual(0, actionResult.Content.Count());
        }

        [TestMethod]
        public void GetSingleCategoryReturnsSingleMatch_AndHttpOK()
        {
            short categoryId = 12;
            string categoryDescription = "Some Description";

            //arrange
            var data = new List<Category>()
            {
                new Category()
                    {Description = categoryDescription, CategoryId = categoryId},
                new Category()
                    {Description = "Dont find me", CategoryId = 1}
            }.AsQueryable();


            var mockContext = new Mock<GolfProductDbContext>();
            mockContext.Setup(m => m.Categories).Returns(GetMockDbSet(data).Object);

            var controller = new CategoriesController(mockContext.Object);

            //act
            var actionResult = controller.Get(categoryId) as OkNegotiatedContentResult<SingleResult<Category>>;


            //assert  
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<SingleResult<Category>>));
            Assert.IsNotNull(actionResult?.Content);

            //I'm not testing for order, so no expectations on what order these will be returned in
            Assert.IsTrue(actionResult.Content.Queryable.Single().CategoryId == categoryId);
            Assert.IsTrue(actionResult.Content.Queryable.Single().Description == categoryDescription);
        }

        [TestMethod]
        public void GetSingleCategoryReturnsNoMatch_AndHttpNotFound()
        {
            short categoryId = 12;
            string categoryDescription = "Some Description";

            //arrange
            var data = new List<Category>()
            {
                new Category()
                    {Description = categoryDescription, CategoryId = categoryId},
                new Category()
                    {Description = "Dont find me", CategoryId = 1}
            }.AsQueryable();


            var mockContext = new Mock<GolfProductDbContext>();
            mockContext.Setup(m => m.Categories).Returns(GetMockDbSet(data).Object);

            var controller = new CategoriesController(mockContext.Object);

            short bogusCategoryId = 999;
            //act
            var actionResult = controller.Get(bogusCategoryId);


            //assert  
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }


        #region TestConverageForGetCategoryProperty

        

        [TestMethod]
        public void GetCategoryProperty_Match_AndHttpOK()
        {
            short categoryId = 12;
            string categoryDescription = "Some Description";

            //arrange
            var data = new List<Category>()
            {
                new Category()
                    {Description = categoryDescription, CategoryId = categoryId},
                new Category()
                    {Description = "Dont find me", CategoryId = 1}
            }.AsQueryable();


            var mockContext = new Mock<GolfProductDbContext>();
            mockContext.Setup(m => m.Categories).Returns(GetMockDbSet(data).Object);

            var controller = new CategoriesController(mockContext.Object);

         

            var actionResult = controller.GetCategoryProperty(categoryId);


            //assert  
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<SingleResult<Category>>));
            //Assert.IsNotNull(actionResult?.Content);

            ////I'm not testing for order, so no expectations on what order these will be returned in
            //Assert.IsTrue(actionResult.Content.Queryable.Single().CategoryId == categoryId);
            //Assert.IsTrue(actionResult.Content.Queryable.Single().Description == categoryDescription);
        }

        [TestMethod]
        public void GetCategoryProperty_NoMatch_AndHttpNotFound()
        {
            short categoryId = 12;
            string categoryDescription = "Some Description";

            //arrange
            var data = new List<Category>()
            {
                new Category()
                    {Description = categoryDescription, CategoryId = categoryId},
                new Category()
                    {Description = "Dont find me", CategoryId = 1}
            }.AsQueryable();


            var mockContext = new Mock<GolfProductDbContext>();
            mockContext.Setup(m => m.Categories).Returns(GetMockDbSet(data).Object);

            var controller = new CategoriesController(mockContext.Object);

            short bogusCategoryId = 999;
            //act
            var actionResult = controller.GetCategoryProperty(bogusCategoryId);


            //assert  
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetSingleCategory_ReturnsMatch_UsePropertyRoute_AndHttpOK()
        {
            //TODO: Refactor this to test the actual method and routing seperately.
            //Could probably mock the controller to verify that the routing works
            //and then test the actual controller seperately

            // Arrange
            short categoryId = 12;
            string categoryDescription = "Some Description";

            //arrange
            var data = new List<Category>()
            {
                new Category()
                    {Description = categoryDescription, CategoryId = categoryId},
                new Category()
                    {Description = "Dont find me", CategoryId = 1}
            }.AsQueryable();


            var mockContext = new Mock<GolfProductDbContext>();
            mockContext.Setup(m => m.Categories).Returns(GetMockDbSet(data).Object);

            var controller = new CategoriesController(mockContext.Object);

            var config = new HttpConfiguration();


            WebApiConfig.Register(config);

            config.EnableDependencyInjection();
            config.EnsureInitialized();

            //we aren't going to make a real web request, the url will be passed to the controller to determine 
            //routing only
            string url = $"http://localhost/odata/Categories({categoryId})/Description";


            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.SetConfiguration(config);

            //Act
            var routeData = config.Routes.GetRouteData(request);


            Assert.AreEqual("Categories", routeData.Values["controller"]);


            request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;


            var controllerSelector = config.Services.GetHttpControllerSelector();
            var ctrlDescriptor = controllerSelector.SelectController(request);


            HttpControllerContext ctx =
                new HttpControllerContext(config, routeData, request)
                {
                    Controller = controller,
                    ControllerDescriptor = ctrlDescriptor
                };


            var result = controller.ExecuteAsync(ctx, CancellationToken.None).Result;

            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);

            var content = result.Content as System.Net.Http.ObjectContent<string>;

            Assert.IsNotNull(content);

            Assert.AreEqual(categoryDescription, content.Value);
        }


        #endregion TestConverageForGetCategoryProperty
    }
}