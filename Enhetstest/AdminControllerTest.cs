using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Oblig_2.BLL;
using Oblig_2.Controllers;
using Oblig_2.DAL;
using Oblig_2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Oblig_2.Enhetstest
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void LoggInnVisView()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminDALStub()));

            // Act
            var actionResult = (ViewResult)controller.LoggInn();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void LoggInnSessionRiktig()
        {
            //Arrenge
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = true;

            //Act
            var resultat = (ViewResult)controller.LoggInn();

            //Assert
            Assert.AreEqual("", resultat.ViewName);
        }

        [TestMethod]
        public void LoggInnSessionFeil()
        {
            //Arrenge
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = false;

            //Act
            var resultat = (ViewResult)controller.LoggInn();

            //Assert
            Assert.AreEqual("", resultat.ViewName);
        }

        [TestMethod]
        public void LoggInnFeilValidering()
        {
            //Arrenge
            var controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var innAdmin = new Admin();
            controller.ViewData.ModelState.AddModelError("Navn", "Fyll ut brukernavn");

            //Act
            var resultat = (ViewResult)controller.LoggInn(innAdmin);

            //Assert
            Assert.IsTrue(resultat.ViewData.ModelState.Count == 1);
            Assert.AreEqual(resultat.ViewName, "");
        }

        [TestMethod]
        public void LoggUt()
        {
            //Arrenge
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = false;

            //Act
            var resultat = (RedirectToRouteResult)controller.LoggUt();

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "BestillReise");
        }

        [TestMethod]
        public void LoggUtFeil()
        {
            //Arrenge
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = true;

            //Act
            var resultat = (RedirectToRouteResult)controller.LoggUt();

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "BestillReise");
        }

        [TestMethod]
        public void AdminIndex()
        {
            //Arrenge
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController();
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = true;

            //Act
            var resultat = (ViewResult)controller.AdminIndex();

            //Assert
            Assert.AreEqual("", resultat.ViewName);
        }
    }
}
