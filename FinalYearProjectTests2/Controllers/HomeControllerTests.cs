using FinalYearProject.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace FinalYearProject.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void GoToStaffAreaButton()
        {
            var controller = new HomeController();
            var result = controller.StaffArea() as ViewResult;
            Assert.AreEqual("StaffArea", result.ViewName);
        }

        [TestMethod()]
        public void GoToCustomerAreaButton()
        {
            var controller = new HomeController();
            var result = controller.CustomerArea() as ViewResult;
            Assert.AreEqual("CustomerHome", result.ViewName);
        }
    }
}