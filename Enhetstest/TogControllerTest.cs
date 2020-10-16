using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Oblig_2.BLL;
using Oblig_2.Controllers;
using Oblig_2.DAL;
using Oblig_2.Model;

namespace Oblig_2.Enhetstest
{
    [TestClass]
    public class TogControllerTest
    {
        [TestMethod]
        public void Stasjoner()
        {
            //Arrenge
            var controller = new TogController(new TogBLL(new TogDALStub()));
            var stasjonListe = new List<Stasjon>();
            var stasjon = new Stasjon()
            {
                navn = "Oslo",
                sone = 1
            };
            stasjonListe.Add(stasjon);
            stasjonListe.Add(stasjon);
            stasjonListe.Add(stasjon);

            //Act
            var actionResult = (ViewResult)controller.Stasjoner();
            var resultat = (List<Stasjon>)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(stasjonListe[i].navn, resultat[i].navn);
                Assert.AreEqual(stasjonListe[i].sone, resultat[i].sone);
            }
        }

        [TestMethod]
        public void Oversikt()
        {
            //Arrenge
            var controller = new TogController(new TogBLL(new TogDALStub()));
            var bestillingListe = new List<BestillingOversiktViewModel>();
            var bestilling = new BestillingOversiktViewModel()
            {
                dato = "18/03/1998",
                tid = "14:00",
                fraStasjon = "kolbotn",
                tilStasjon = "Oslo S",
                pris = 100,
            };

            bestillingListe.Add(bestilling);
            bestillingListe.Add(bestilling);
            bestillingListe.Add(bestilling);

            //Act
            var actionResult = (ViewResult)controller.Oversikt();
            var resultat = (List<BestillingOversiktViewModel>)actionResult.Model;
            // Assert

            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(bestillingListe[i].dato, resultat[i].dato);
                Assert.AreEqual(bestillingListe[i].tid, resultat[i].tid);
                Assert.AreEqual(bestillingListe[i].fraStasjon, resultat[i].fraStasjon);
                Assert.AreEqual(bestillingListe[i].tilStasjon, resultat[i].tilStasjon);
                Assert.AreEqual(bestillingListe[i].pris, resultat[i].pris);

            }
        }

        [TestMethod]
        public void GetPrisPrSone()
        {
            //Arrenge
            var controller = new TogController(new TogBLL(new TogDALStub()));

            //Act
            var resultat = controller.GetPrisPrSone();

            //Assert
            Assert.AreEqual(resultat, "50");
        }

        [TestMethod]
        public void AdminBestillSession()
        {
            //Arrenge
            var SessionMock = new TestControllerBuilder();
            var controller = new TogController();
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = true;

            //Act
            var resultat = (ViewResult)controller.AdminBestill();

            //Assert
            Assert.AreEqual("", resultat.ViewName);
        }

        [TestMethod]
        public void AdminBestill()
        {
            //Arrenge
            var controller = new TogController(new TogBLL(new TogDALStub()));
            var bestillingListe = new List<BestillingOversiktViewModel>();
            var bestilling = new BestillingOversiktViewModel()
            {
                dato = "18/03/1998",
                tid = "14:00",
                fraStasjon = "kolbotn",
                tilStasjon = "Oslo S",
                pris = 100,
            };

            bestillingListe.Add(bestilling);
            bestillingListe.Add(bestilling);
            bestillingListe.Add(bestilling);

            //Act
            var actionResult = (RedirectToRouteResult)controller.AdminBestill();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LoggInn");
        }

        [TestMethod]
        public void AdminStasjonSession()
        {
            //Arrenge
            var SessionMock = new TestControllerBuilder();
            var controller = new TogController();
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = true;

            //Act
            var resultat = (ViewResult)controller.AdminStasjon();

            //Assert
            Assert.AreEqual("", resultat.ViewName);
        }

        [TestMethod]
        public void AdminStasjon()
        {
            //Arrenge
            var controller = new TogController(new TogBLL(new TogDALStub()));
            var bestillingListe = new List<BestillingOversiktViewModel>();
            var bestilling = new BestillingOversiktViewModel()
            {
                dato = "18/03/1998",
                tid = "14:00",
                fraStasjon = "kolbotn",
                tilStasjon = "Oslo S",
                pris = 100,
            };

            bestillingListe.Add(bestilling);
            bestillingListe.Add(bestilling);
            bestillingListe.Add(bestilling);

            //Act
            var actionResult = (RedirectToRouteResult)controller.AdminStasjon();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LoggInn");
        }

        [TestMethod]
        public void SlettStasjon()
         {
            //Arrenge
            var controller = new TogController(new TogBLL(new TogDALStub()));
            var stasjon = new Stasjon()
            {
                stasjonId = 1,
                navn = "Kolbotn",
                sone = 1
            };

             //Act
            var actionResult = (RedirectToRouteResult)controller.SlettStasjon(1);
            

            //Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminStasjon");
         }

        [TestMethod]
        public void SlettBestilling()
         {
            //Arrenge
            var controller = new TogController(new TogBLL(new TogDALStub()));
            var bestilling = new Bestilling()
            {
                bestillingId = 1,
                dato = "18/03/2020",
                tid = "23:00",
                fraStasjon = 1,
                tilStasjon = 4,
            };

            //Act
            var actionResult = (RedirectToRouteResult)controller.SlettBestilling(1);

            //Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminBestill");
        }

        [TestMethod]
        public void EndreStasjonSession()
        {
            //Arrenge
            var SessionMock = new TestControllerBuilder();
            var controller = new TogController();
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = true;

            //Act
            var actionResult = (RedirectToRouteResult)controller.EndreStasjon(1);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LoggInn");
        }

        [TestMethod]
        public void EndreStasjon()
         {
            // Arrange
            var controller = new TogController(new TogBLL(new TogDALStub()));

            var innStasjon = new Stasjon()
             {
                navn = "Oslo S",
                sone = 1
             };
             // Act
             var actionResultat = (RedirectToRouteResult)controller.EndreStasjon(1, innStasjon);

             // Assert
             Assert.AreEqual(actionResultat.RouteName, "");
             Assert.AreEqual(actionResultat.RouteValues.Values.First(), "LoggInn");
         }

        [TestMethod]
        public void EndreBestillingSession()
        {
            //Arrenge
            var SessionMock = new TestControllerBuilder();
            var controller = new TogController();
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = true;

            //Act
            var actionResult = (RedirectToRouteResult)controller.EndreBestilling(1);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LoggInn");
        }

        [TestMethod]
        public void EndreBestilling()
         {
            // Arrange
            var controller = new TogController(new TogBLL(new TogDALStub()));

             // Act
             var actionResultat = (RedirectToRouteResult)controller.EndreBestilling(1);

             // Assert
             Assert.AreEqual(actionResultat.RouteName, "");
             Assert.AreEqual(actionResultat.RouteValues.Values.First(), "LoggInn");
         }

        [TestMethod]
        public void EndrePrisSession()
        {
            //Arrenge
            var SessionMock = new TestControllerBuilder();
            var controller = new TogController();
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = true;

            //Act
            var resultat = (ViewResult)controller.EndrePris();

            // Assert
            Assert.AreEqual("", resultat.ViewName);
        }

        [TestMethod]
        public void EndrePrisTilAdmin()
        {
            // Arrange
            var controller = new TogController(new TogBLL(new TogDALStub()));

            // Act
            var actionResultat = (RedirectToRouteResult)controller.EndrePris();

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "LoggInn");
        }

        [TestMethod]
        public void EndrePrisFeilValidering()
        {
            //Arrenge
            var controller = new TogController(new TogBLL(new TogDALStub()));
            var innPris = new Priser();
            controller.ViewData.ModelState.AddModelError("Pris", "Fyll ut pris");

            //Act
            var actionResultat = (RedirectToRouteResult)controller.EndrePris();

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "LoggInn");
        }

        [TestMethod]
        public void NyStasjonSession()
        {
            //Arrenge
            var SessionMock = new TestControllerBuilder();
            var controller = new TogController();
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = true;

            //Act
            var resultat = (ViewResult)controller.NyStasjon();

            // Assert
            Assert.AreEqual("", resultat.ViewName);
        }

        [TestMethod]
        public void NyStasjon()
        {
            // Arrange
            var controller = new TogController(new TogBLL(new TogDALStub()));

            // Act
            var actionResultat = (RedirectToRouteResult)controller.NyStasjon();

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "LoggInn");
        }

        [TestMethod]
        public void NyStasjonValidering()
        {
            //Arrenge
            var controller = new TogController(new TogBLL(new TogDALStub()));
            var innStasjon = new Stasjon();
            controller.ViewData.ModelState.AddModelError("Stasjon", "Fyll ut stasjon");

            //Act
            var actionResultat = (RedirectToRouteResult)controller.NyStasjon();

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "LoggInn");
        }
    }
}
