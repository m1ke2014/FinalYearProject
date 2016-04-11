using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace FinalYearProject.Controllers.Tests
{
    [TestClass()]
    public class FaultControllerTests
    {
        [TestMethod()]
        public void GoToCreateFaultButton()
        {
            var controller = new FaultController();
            var result = controller.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }

    }
}