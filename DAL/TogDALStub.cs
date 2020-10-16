using Oblig_2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblig_2.DAL
{
    public class TogDALStub : ITogRepository
    { 
        public double LagreBestilling(Bestilling innBestilling)
        { 
            if (innBestilling.dato == "")
            {
                return 1;
            }
            else
            {
                return 0;
            }      
        }

        public string LagreStasjon(Stasjon innStasjon)
        {
            if(innStasjon == null)
            {
                return "Feil";
            }
            else
            {
                return "Ny Lagt til";
            }
        }

        public List<Stasjon> HentStasjoner()
        {
            var stasjonListe = new List<Stasjon>();
            var stasjon = new Stasjon()
            {
                navn = "Oslo",
                sone = 1
            };
            stasjonListe.Add(stasjon);
            stasjonListe.Add(stasjon);
            stasjonListe.Add(stasjon);

            return stasjonListe;
        }

        public Stasjon HentEnStasjon(int SID)
        {
            if (SID == 0)
            {
                var enStasjon = new Stasjon();
                enStasjon.stasjonId = 0;
                return enStasjon;
            }
            else
            {
                var enStasjon = new Stasjon()
                {
                    stasjonId = 1,
                    navn = "Oslo S",
                    sone = 1
                };
                return enStasjon;
            }
        }

        public Bestilling HentEnBestilling(int BID)
        {
            if (BID == 0)
            {
                var enBestilling = new Bestilling();
                enBestilling.bestillingId = 0;
                return enBestilling;
            }
            else
            {
                var enBestilling = new Bestilling()
                {
                    bestillingId = 1,
                    dato = "31/12/1999",
                    tid = "18:00",
                    fraStasjon = 1,
                    tilStasjon = 2,
                    pris = 50
                };
                return enBestilling;
            }
        }

        public List<BestillingOversiktViewModel> HentBestillingerTilOversikt()
        {
            var bestillingListe = new List<BestillingOversiktViewModel>();
            var bestillinger = new BestillingOversiktViewModel()
             {
                dato = "18/03/1998",
                tid = "14:00",
                fraStasjon = "kolbotn",
                tilStasjon = "Oslo S",
                pris = 100,
            };
             bestillingListe.Add(bestillinger);
             bestillingListe.Add(bestillinger);
             bestillingListe.Add(bestillinger);

            return bestillingListe ;
        }

        public Pris HentPrisPrSone()
        {
            var prisPrSone = new Pris()
            {
                sonePris = 50
            };

            return prisPrSone;
        }

        public bool SlettStasjon(int SID)
        {
           if(SID == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool SlettBestilling(int BID)
        {
            if (BID == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool EndreStasjon(int SID, Stasjon enStasjon)
        {
            if (SID == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool EndreBestilling(int BID, Bestilling enBestilling)
        {
            if (BID == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool EndrePris(Pris innPris)
        {
            if (innPris == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
