using System;
using System.Linq;
using System.Threading;
using Golf.Product.DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Golf.Product.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            GolfProductDbContext ctx = new GolfProductDbContext();
            foreach (var ctxFamily in ctx.Families.Include("Category"))
            {
                Assert.IsTrue(ctxFamily.Category != null);
            }
  
        }
    }
}
