using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nodiceweb;
using nodiceweb.Controllers;
using nodiceweb.Models;

namespace nodiceweb.Tests.Controllers
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
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        /*
                [TestMethod]
                public void IndexOrdersByName()
                {
                    var context = new FakeTeamContext
                    {
                        Teams =
                            {
                                new Team { Name = "BBB"},
                                new Team { Name = "AAA"},
                                new Team { Name = "ZZZ"},
                            }
                    };

                    var controller = new TeamsController();
                    var result = controller.Index();

                    Assert.IsInstanceOfType(result.ViewData.Model, typeof(IEnumerable<Team>));

                    var departments = (IEnumerable<Team>)result.ViewData.Model;
                    Assert.AreEqual("AAA", departments.ElementAt(0).Name);
                    Assert.AreEqual("BBB", departments.ElementAt(1).Name);
                    Assert.AreEqual("ZZZ", departments.ElementAt(2).Name);
                }
                    */
    }

}
