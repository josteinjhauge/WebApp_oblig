using Oblig_2.BLL;
using Oblig_2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace Oblig_2.Controllers
{
    public class AdminController : Controller
    {

        private IAdminBLLRepository _adminBLL;


        public AdminController()
        {
            _adminBLL = new AdminBLL();
        }

        public AdminController(IAdminBLLRepository stub)
        {
            _adminBLL = stub;
        }

        public ActionResult LoggInn()
        {   
            return View();  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoggInn(Admin innAdmin)
        {
            if (ModelState.IsValid)
            {
                if (_adminBLL.GyldigAdmin(innAdmin))
                {
                    Session["LoggetInn"] = true;

                    return RedirectToAction("AdminIndex");
                }
                else
                {
                    Session["LoggetInn"] = false;

                    MessageBox.Show("Innlogging feilet! \nVennligst oppgi gyldig admin", "",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return View();
                }
            }
            return View();
        }

        public ActionResult LoggUt()
        {
            Session["LoggetInn"] = false;
            return RedirectToAction("BestillReise", "Tog");
        }

        public ActionResult AdminIndex()
        {
            try
            {
                if ((bool)Session["LoggetInn"] == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("LoggInn");
                }
            }
            catch(Exception e)
            {
                return RedirectToAction("LoggInn");
            }
        }

        /*
        public ActionResult LagAdmin(Admin innAdmin)
        {
            _adminBLL.CreateAdmin(innAdmin);
            return View();
        }
        */
    }
}