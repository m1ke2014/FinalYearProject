using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace FinalYearProject.Controllers.Tests
{
    [TestClass()]
    public class ProductControllerTests
    {
        [TestMethod()]
        public void GoToCreateProductButton()
        {
            var controller = new ProductController();
            var result = controller.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }
    }
}