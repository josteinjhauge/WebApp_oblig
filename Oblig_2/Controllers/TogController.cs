using Oblig_2.Model;
using Oblig_2.BLL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Windows.Forms;


namespace Oblig_2.Controllers
{
    public class TogController : Controller
    {
        private ITogBLLRepository _togBLL;

        public TogController()
        {
            _togBLL = new TogBLL();
        }

        public TogController(ITogBLLRepository stub)
        {
            _togBLL = stub;
        }

        public ActionResult BestillReise()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BestillReise(Bestilling innBestilling)
        {
            if (innBestilling.fraStasjon == innBestilling.tilStasjon &&
                 innBestilling.fraStasjon > 0 && innBestilling.tilStasjon > 0)
            {
                MessageBox.Show("Du har valgt å dra fra og til samme stasjon." + "\n" +
                    "Vennligst velg forskjellige stasjoner!", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                double status = _togBLL.LagreBestilling(innBestilling);

                if (status == 0)
                {
                    MessageBox.Show("Billetten kunne ikke registreres." + "\n" +
                        "Sjekk at alle feltene er fylt ut riktig og prøv igjen!", "",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Billetten er registrert!", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return View();
        }

        public ActionResult Stasjoner()
        {
            List<Stasjon> alleStasjoner = _togBLL.HentStasjoner();
            return View(alleStasjoner);
        }

        public ActionResult Oversikt()
        {
            List<BestillingOversiktViewModel> alleBestillinger = _togBLL.HentBestillingerTilOversikt();
            return View(alleBestillinger);
        }

        public ActionResult Tilbake()
        {
            return RedirectToAction("BestillReise");
        }

        public string GetStasjoner() 
        {
            var stasjonList = _togBLL.HentStasjoner();
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(stasjonList);
        }

        [HttpPost]
        public ActionResult RegnUtPris(string fraStasjonID, string tilStasjonID)
        {
            int fraStasjonIDint = Int32.Parse(fraStasjonID);
            int tilStasjonIDint = Int32.Parse(tilStasjonID);

            Stasjon fraStasjon = _togBLL.HentEnStasjon(fraStasjonIDint);
            Stasjon tilStasjon = _togBLL.HentEnStasjon(tilStasjonIDint);

            int antSoner;
            if (fraStasjon.sone <= tilStasjon.sone)
            {
                antSoner = tilStasjon.sone - fraStasjon.sone + 1;

            }
            else
            {
                antSoner = fraStasjon.sone - tilStasjon.sone + 1;
            }

            Pris prisen = _togBLL.HentPrisPrSone();
            double prisPrSone = prisen.sonePris;

            var pris = antSoner * prisPrSone;

            return Json(pris);
        }

        public string GetPrisPrSone()
        {
            Pris prisen = _togBLL.HentPrisPrSone();
            var prisPrSone = prisen.sonePris;
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(prisPrSone);
        }

        public ActionResult AdminBestill()
        {
            try
            {
                if ((bool)Session["LoggetInn"] == true)
                {
                    List<BestillingOversiktViewModel> alleBestillinger = _togBLL.HentBestillingerTilOversikt();
                    return View(alleBestillinger);
                }
                return RedirectToAction("LoggInn","Admin");
            }
            catch (Exception e)
            {
                return RedirectToAction("LoggInn","Admin");
            }
        }

        public ActionResult AdminStasjon()
        {
            try
            {
                if ((bool)Session["LoggetInn"] == true)
                {
                    List<Stasjon> alleStasjoner = _togBLL.HentStasjoner();
                    return View(alleStasjoner);
                }
                return RedirectToAction("LoggInn", "Admin");
            }
            catch (Exception e)
            {
                return RedirectToAction("LoggInn", "Admin");
            }
        }

        public ActionResult SlettStasjon(int SID)
        {
            bool status = _togBLL.SlettStasjon(SID);

            if (status == true)
            {
                MessageBox.Show("Stasjonen er slettet", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Stasjonen kunne ikke slettes", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return RedirectToAction("AdminStasjon");
        }

        public ActionResult SlettBestilling(int BID)
        {
            bool status = _togBLL.SlettBestilling(BID);
            if (status == true)
            {
                MessageBox.Show("Bestillingen er slettet", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Bestillingen kunne ikke slettes", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return RedirectToAction("AdminBestill");
        }

        public ActionResult EndreStasjon(int SID)
        {
            try
            {
                if ((bool)Session["LoggetInn"] == true)
                {
                    Stasjon enStasjon = _togBLL.HentEnStasjon(SID);
                    if (enStasjon != null)
                    {
                        return View(enStasjon);
                    }
                    else
                    {
                        MessageBox.Show("Stasjonen kan ikke endres", "",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return RedirectToAction("AdminStasjon", "Admin");
                    }
                }
                return RedirectToAction("LoggInn", "Admin");
            }
            catch (Exception e)
            {
                return RedirectToAction("LoggInn", "Admin");
            }
        }

        [HttpPost]
        public ActionResult EndreStasjon(int SID, Stasjon enStasjon)
        {
            try
            {
                if ((bool)Session["LoggetInn"] == true)
                {
                    bool endretStasjon = _togBLL.EndreStasjon(SID, enStasjon);
                    if (endretStasjon)
                    {
                        return RedirectToAction("AdminStasjon");
                    }
                    else
                    {
                        MessageBox.Show("Stasjonen kunne ikke endres", "",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return View();
                    }
                }
                return RedirectToAction("LoggInn", "Admin");
            }
            catch (Exception e)
            {
                return RedirectToAction("LoggInn", "Admin");
            }
        }

        public ActionResult EndreBestilling(int BID)
        {
            try
            {
                if ((bool)Session["LoggetInn"] == true)
                {
                    Bestilling enBestilling = _togBLL.HentEnBestilling(BID);
                    if (enBestilling != null)
                    {
                        return View(enBestilling);
                    }
                    else
                    {
                        MessageBox.Show("Bestillingen kan ikke endres", "",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return RedirectToAction("AdminBestill", "Admin");
                    }
                }
                return RedirectToAction("LoggInn", "Admin");
            }
            catch (Exception e)
            {
                return RedirectToAction("LoggInn", "Admin");
            }
        }

        [HttpPost]
        public ActionResult EndreBestilling(int BID, Bestilling enBestilling)
        {
            try
            {
                if ((bool)Session["LoggetInn"] == true)
                {
                    bool endretBestilling = _togBLL.EndreBestilling(BID, enBestilling);
                    if (endretBestilling)
                    {
                        return RedirectToAction("AdminBestill");
                    }
                    else
                    {
                        MessageBox.Show("Bestillingen kunne ikke endres", "",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return View();
                    }
                }
                return RedirectToAction("LoggInn", "Admin");
            }
            catch (Exception e)
            {
                return RedirectToAction("LoggInn", "Admin");
            }
        }

        public ActionResult EndrePris()
        {
            try
            {
                if ((bool)Session["LoggetInn"] == true)
                {
                    return View();
                }
                return RedirectToAction("LoggInn", "Admin");
            }
            catch (Exception e)
            {
                return RedirectToAction("LoggInn", "Admin");
            }
        }

        [HttpPost]
        public ActionResult EndrePris(Pris innPris)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if ((bool)Session["LoggetInn"] == true)
                    {
                        bool endretPris = _togBLL.EndrePris(innPris);
                        if (endretPris)
                        {
                            MessageBox.Show("Prisen er endret for fremtidige bestillinger", "",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return View();
                        }
                        else
                        {
                            MessageBox.Show("Prisen kunne ikke endres", "",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return View();
                        }
                    }
                    return RedirectToAction("LoggInn", "Admin");
                }
                catch (Exception e)
                {
                    return RedirectToAction("LoggInn", "Admin");
                }
            }
            return View();
        }

        public ActionResult NyStasjon()
        {
            try
            {
                if ((bool)Session["LoggetInn"] == true)
                {
                    return View();
                }
                return RedirectToAction("LoggInn", "Admin");
            }
            catch (Exception e)
            {
                return RedirectToAction("LoggInn", "Admin");
            } 
        }

        [HttpPost]
        public ActionResult NyStasjon(Stasjon innStasjon)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if ((bool)Session["LoggetInn"] == true)
                    {
                        string lagretStatus = _togBLL.LagreStasjon(innStasjon);

                        if (lagretStatus.Equals("LagtTilNy"))
                        {
                            MessageBox.Show("Stasjonen er lagt til", "", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            return View();
                        }
                        else if (lagretStatus.Equals("FinnesFraFør"))
                        {
                            MessageBox.Show("Stasjonen finnes allerede i databasen", "",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return View();
                        }
                        else
                        {
                            MessageBox.Show("Lagringen sporet av i svingen", "",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return View();
                        }
                    }
                    return RedirectToAction("LoggInn", "Admin");
                }
                catch (Exception e)
                {
                    return RedirectToAction("LoggInn", "Admin");
                }
            }
            return View();
        }
    }
}