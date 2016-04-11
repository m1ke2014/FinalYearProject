using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace FinalYearProject.Controllers.Tests
{
    [TestClass()]
    public class ChemicalControllerTests
    {
        [TestMethod()]
        public void OpenCreateChemicalScreenButton()
        {
            var controller = new ChemicalController();
            var result = controller.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }
    }
}