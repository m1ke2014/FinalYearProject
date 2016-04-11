using FinalYearProject.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace FinalYearProject.Controllers.Tests
{
    [TestClass()]
    public class DeclarationOfConformitiesControllerTests
    {
        [TestMethod()]
        public void GoToSubmitConfirmTest()
        {
            var controller = new DeclarationOfConformitiesController();
            var result = controller.SubmitConfirm() as ViewResult;
            Assert.AreEqual("SubmitConfirm", result.ViewName);
        }

        [TestMethod()]
        public void GoToCustomerHomeTest()
        {
            var controller = new DeclarationOfConformitiesController();
            var result = controller.DirectToHome() as ViewResult;
            Assert.AreEqual("CustomerHome", result.ViewName);
        }
    }
}