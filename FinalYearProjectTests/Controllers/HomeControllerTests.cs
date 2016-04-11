using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web;
using FinalYearProject.Controllers;

namespace FinalYearProject.Controllers.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void GoToStaffAreaButton()
        {
            var homeController = new HomeController();
            var result = homeController.StaffArea() as ViewResult;
            Assert.AreEqual("StaffArea", result.ViewName);
        }
    }
}