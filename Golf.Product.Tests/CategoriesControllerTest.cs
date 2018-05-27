using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.OData.Results;
using Golf.Product.Controllers;
using Golf.Product.DataAccessLayer;
using Golf.Product.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Golf.Product.Tests
{
    [TestClass]
    public class CategoriesControllerTest
    {

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
    

            //Setup requried for DBSet
            var mockSet = new Mock<DbSet<Category>>();
            mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
 
            var mockContext = new Mock<GolfProductDbContext>();
            mockContext.Setup(m => m.Categories).Returns(mockSet.Object);

            var controller = new CategoriesController(mockContext.Object);

            //act
            var actionResult = controller.Get() as OkNegotiatedContentResult<DbSet<Category>>;
  

            //assert  
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<DbSet<Category>>)); 
            Assert.IsNotNull(actionResult?.Content);
            Assert.AreEqual(3,actionResult.Content.Count() );

            //I'm not testing for order, so no expectations on what order these will be returned in
            Assert.IsTrue(actionResult.Content.Any(c=>c.Description == categoryDescription1));
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


            //Setup requried for DBSet
            var mockSet = new Mock<DbSet<Category>>();
            mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<GolfProductDbContext>();
            mockContext.Setup(m => m.Categories).Returns(mockSet.Object);

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


            //Setup requried for DBSet
            var mockSet = new Mock<DbSet<Category>>();
            mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<GolfProductDbContext>();
            mockContext.Setup(m => m.Categories).Returns(mockSet.Object);

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


            //Setup requried for DBSet
            var mockSet = new Mock<DbSet<Category>>();
            mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<GolfProductDbContext>();
            mockContext.Setup(m => m.Categories).Returns(mockSet.Object);

            var controller = new CategoriesController(mockContext.Object);

            short bogusCategoryId = 999;
            //act
            var actionResult = controller.Get(bogusCategoryId);


            //assert  
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));


        }
    }
}
