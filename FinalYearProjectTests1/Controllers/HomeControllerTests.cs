using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinalYearProject.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void StaffAreaTest()
        {
            var homecontroller = new HomeController();
            var result = homecontroller.StaffArea() as ViewResult;
            Assert.AreEqual("StaffArea", result.ViewName);
        }
    }
}