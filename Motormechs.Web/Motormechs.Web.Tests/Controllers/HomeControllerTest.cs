using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Motormechs.Web;
using Motormechs.Web.Controllers;

namespace Motormechs.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AboutUs()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.AboutUs() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void ContactUs()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.ContactUs() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
