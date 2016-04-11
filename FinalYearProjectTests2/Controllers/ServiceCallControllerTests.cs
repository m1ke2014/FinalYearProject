using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace FinalYearProject.Controllers.Tests
{
    [TestClass()]
    public class ServiceCallControllerTests
    {
        [TestMethod()]
        public void GoToCreateServiceCallButton()
        {
            var controller = new ServiceCallController();
            var result = controller.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }
    }
}