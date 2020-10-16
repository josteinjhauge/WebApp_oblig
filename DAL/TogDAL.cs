using NLog;
using Oblig_2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Oblig_2.DAL
{
    public class TogDAL : ITogRepository
    {
        private static Logger logger = LogManager.GetLogger("fileLogger");

        public double LagreBestilling(Bestilling innBestilling)
        {
            using (var db = new TogContext())
            {
                try
                {
                    Bestillinger nyBestilling = new Bestillinger();

                    Stasjoner fraStasjon = db.Stasjoner.Find(innBestilling.fraStasjon);
                    Stasjoner tilStasjon = db.Stasjoner.Find(innBestilling.tilStasjon);

                    int antSoner;

                    if (fraStasjon.Sone <= tilStasjon.Sone)
                    {
                        antSoner = tilStasjon.Sone - fraStasjon.Sone + 1;
                    }
                    else
                    {
                        antSoner = fraStasjon.Sone - tilStasjon.Sone + 1;
                    }

                    Priser prisen = db.Pris.Find(1);
                    double prisPrSone = prisen.SonePris;
                    double pris = antSoner * prisPrSone;

                    nyBestilling.Dato = innBestilling.dato;
                    nyBestilling.Tid = innBestilling.tid;
                    nyBestilling.FraStasjon = fraStasjon;
                    nyBestilling.TilStasjon = tilStasjon;
                    nyBestilling.Pris = pris;

                    db.Bestillinger.Add(nyBestilling);
                    db.SaveChanges();
                    logger.Info("Logger lagring av bestilling: " + "Dato: " + nyBestilling.Dato + "\n" +
                        "Tid: " + nyBestilling.Tid + "\n" +
                        "Fra Stasjon: " + nyBestilling.FraStasjon.Navn + "\n" +
                        "Til Stasjon: " + nyBestilling.TilStasjon.Navn + "\n" +
                        "Pris: " + nyBestilling.Pris.ToString());
                    return 1;               
                }
                catch (Exception e)
                {
                    logger.Error(e, "Feil med lagring av bestilling, se Error - melding");
                    return 0;
                }
            }
        }

        public string LagreStasjon(Stasjon innStasjon)
        {
            using (var db = new TogContext())
            {
                try
                {
                    var eksistererStasjon = db.Stasjoner.Where(e => e.Navn == innStasjon.navn).FirstOrDefault();
                    if (eksistererStasjon == null)
                    {
                        Stasjoner nyStasjon = new Stasjoner
                        {
                            Navn = innStasjon.navn,
                            Sone = innStasjon.sone
                        };

                        db.Stasjoner.Add(nyStasjon);
                        db.SaveChanges();

                        logger.Info("Logger lagring av stasjon: " + "Navn på stasjon: " + nyStasjon.Navn + "\n" + 
                            "Sone: " + nyStasjon.Sone.ToString());
                        return "LagtTilNy";
                    }
                    else
                    {
                        return "FinnesFraFør";
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e, "Feil med lagring av en stasjon, se Error - melding");
                    return "Feil";
                }
            }
        }

        public List<Stasjon> HentStasjoner()
        {
            try
            {
                using (var db = new TogContext())
                {
                    List<Stasjon> alleStasjoner = db.Stasjoner.Select(s => new Stasjon()
                    {
                        stasjonId = s.StasjonerId,
                        navn = s.Navn,
                        sone = s.Sone
                    }
                    ).ToList();
                    return alleStasjoner;
                }
            }
            catch(Exception e)
            {
                logger.Error(e, "Feil med hent av stasjoner, se Error - melding");
                return null;
            }
            
        }

        public Stasjon HentEnStasjon(int SID)
        {
            using(var db = new TogContext())
            {
                Stasjoner finnStasjon = db.Stasjoner.Find(SID);

                if (finnStasjon != null)
                {
                    Stasjon enStasjon = new Stasjon()
                    {
                        stasjonId = finnStasjon.StasjonerId,
                        navn = finnStasjon.Navn,
                        sone = finnStasjon.Sone
                    };
                    return enStasjon;
                }
                else
                {
                    logger.Error("Kunne ikke hente en stasjon med stasjonsId " + SID);
                    return null;
                }
            }
        }

        public Bestilling HentEnBestilling(int BID)
        {
            using (var db = new TogContext())
            {
                Bestillinger finnBestilling = db.Bestillinger.Find(BID);

                try
                {
                    if (finnBestilling != null)
                    {
                        Bestilling enBestilling = new Bestilling()
                        {
                            bestillingId = finnBestilling.BestillingerId,
                            dato = finnBestilling.Dato,
                            tid = finnBestilling.Tid,
                            fraStasjon = finnBestilling.FraStasjon.StasjonerId,
                            tilStasjon = finnBestilling.TilStasjon.StasjonerId,
                            pris = finnBestilling.Pris
                        };

                        return enBestilling;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e, "Feil med henting av en bestilling , se Error - melding");
                    return null;
                }
            }
        }

        public List<BestillingOversiktViewModel> HentBestillingerTilOversikt()
        {
            try
            {
                using (var db = new TogContext())
                {
                    List<BestillingOversiktViewModel> alleBestillinger = db.Bestillinger.Select(b => new BestillingOversiktViewModel()
                    {
                        bestillingId = b.BestillingerId,
                        dato = b.Dato,
                        tid = b.Tid,
                        fraStasjon = b.FraStasjon.Navn,
                        tilStasjon = b.TilStasjon.Navn,
                        pris = b.Pris
                    }
                    ).ToList();
                    return alleBestillinger;
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Feil med hent av bestillinger til oversikt, se Error - melding");
                return null;
            }
            
        }

        public Pris HentPrisPrSone() 
        {
            try
            {
                using (var db = new TogContext())
                {
                    Priser prisen = db.Pris.Find(1);

                    Pris utPris = new Pris
                    {
                        sonePris = prisen.SonePris
                    };

                    return utPris;
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Feil med hent av pris, se Error - melding");
                return null;
            }           
        }

        public bool SlettStasjon(int SID)
        {
            try
            { 
                using(var db = new TogContext())
                {
                    Stasjoner stasjonen = db.Stasjoner.FirstOrDefault(s => s.StasjonerId == SID);

                    // Dette er kun for loggen sin del
                    Stasjon stasjonenSomSlettes = new Stasjon()
                    {
                        stasjonId = stasjonen.StasjonerId,
                        navn = stasjonen.Navn,
                        sone = stasjonen.Sone
                    };

                    db.Stasjoner.Remove(stasjonen);
                    db.SaveChanges();

                    logger.Info("Logger sletting av stasjon: " + stasjonenSomSlettes.navn + "\n" +
                        "ID: " + stasjonenSomSlettes.stasjonId.ToString());
                    return true;
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Feil med sletting av stasjon, se Error - melding");
                return false;
            }
        }

        public bool SlettBestilling(int BID)
        {
            try
            {
                using (var db = new TogContext())
                {
                    Bestillinger bestillingen = db.Bestillinger.FirstOrDefault(b => b.BestillingerId == BID);

                    // Dette er kun for loggen sin del
                    Bestilling bestillingenSomSlettes = new Bestilling()
                    {
                        bestillingId = bestillingen.BestillingerId,
                        dato = bestillingen.Dato,
                        tid = bestillingen.Tid,
                        fraStasjon = bestillingen.FraStasjon.StasjonerId,
                        tilStasjon = bestillingen.TilStasjon.StasjonerId,
                        pris = bestillingen.Pris
                    };
                    Stasjoner fraStasjon = db.Stasjoner.Find(bestillingen.FraStasjon.StasjonerId);
                    Stasjoner tilStasjon = db.Stasjoner.Find(bestillingen.TilStasjon.StasjonerId);

                    db.Bestillinger.Remove(bestillingen);
                    db.SaveChanges();

                    logger.Info("Logger slettet bestilling: " + "Dato: " + bestillingenSomSlettes.dato + "\n" +
                        "Tid: " + bestillingenSomSlettes.tid + "\n" +
                        "Fra Stasjon: " + fraStasjon.Navn + "\n" +
                        "Til Stasjon: " + tilStasjon.Navn + "\n" +
                        "ID: " + bestillingenSomSlettes.bestillingId.ToString());
                    return true;
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Feil med sletting av bestilling, se Error-meldig");
                return false;
            }
        }

        public bool EndreStasjon(int SID, Stasjon enStasjon)
        {
            try
            {
                using (var db = new TogContext())
                {
                    Stasjoner endretStasjon = db.Stasjoner.Find(SID);

                    // Dette er kun for loggen sin del
                    Stasjon gammelStasjon = new Stasjon()
                    {
                        navn = endretStasjon.Navn,
                        sone = endretStasjon.Sone
                    };

                    endretStasjon.Navn = enStasjon.navn;
                    endretStasjon.Sone = enStasjon.sone;
                    db.SaveChanges();

                    logger.Info("Logger endring av stasjon med stasjonId: " + endretStasjon.StasjonerId + "\n" + 
                        "Fra Navn: " + gammelStasjon.navn + " Til Navn: " + endretStasjon.Navn + "\n" +
                        "Fra Sone: " + gammelStasjon.sone + " Til Sone: " + endretStasjon.Sone);
                    return true;
                }
            }
            catch(Exception e)
            {
                logger.Error(e, "Feil med endring av stasjon, se Error-melding");
                return false;
            }
        }

        public bool EndreBestilling(int BID, Bestilling enBestilling)
        {
            try
            {
                using (var db = new TogContext())
                {
                    Bestillinger endretBestilling = db.Bestillinger.Find(BID);

                    // Dette er kun for loggen sin del
                    BestillingOversiktViewModel gammelBestilling = new BestillingOversiktViewModel()
                    {
                        dato = endretBestilling.Dato,
                        tid = endretBestilling.Tid,
                        fraStasjon = endretBestilling.FraStasjon.Navn,
                        tilStasjon = endretBestilling.TilStasjon.Navn,
                        pris = endretBestilling.Pris
                    };

                    Stasjoner fraStasjon = db.Stasjoner.Find(enBestilling.fraStasjon);
                    Stasjoner tilStasjon = db.Stasjoner.Find(enBestilling.tilStasjon);

                    endretBestilling.Dato = enBestilling.dato;
                    endretBestilling.Tid = enBestilling.tid;
                    endretBestilling.FraStasjon = fraStasjon;
                    endretBestilling.TilStasjon = tilStasjon;

                    int antSoner;
                    if (fraStasjon.Sone <= tilStasjon.Sone)
                    {
                        antSoner = tilStasjon.Sone - fraStasjon.Sone + 1;
                    }
                    else
                    {
                        antSoner = fraStasjon.Sone - tilStasjon.Sone + 1;
                    }

                    Priser prisen = db.Pris.Find(1);
                    double prisPrSone = prisen.SonePris;
                    double TOTpris = antSoner * prisPrSone;
                    endretBestilling.Pris = TOTpris;

                    db.SaveChanges();

                    logger.Info("Logger endring av bestilling med bestillingId: " + endretBestilling.BestillingerId + "\n" +
                        "Fra Dato: " + gammelBestilling.dato + " Til Dato: " + endretBestilling.Dato + "\n" +
                        "Fra Sone: " + gammelBestilling.tid + " Til Sone: " + endretBestilling.Tid + "\n" +
                        "Fra fra stasjon: " + gammelBestilling.fraStasjon + " Til fra stasjon: " + endretBestilling.FraStasjon.Navn + "\n" +
                        "Fra til stasjon: " + gammelBestilling.tilStasjon + " Til til stasjon: " + endretBestilling.TilStasjon.Navn);
                    return true;
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Feil med endring av bestilling, se Error-melding");
                return false;
            }
        }

        public bool EndrePris(Pris innPris)
        {
            try
            {
                using (var db = new TogContext())
                {
                    Priser endretPris = db.Pris.Find(1);

                    // Dette er kun for loggen sin del
                    Pris gammelPris = new Pris()
                    {
                        sonePris = endretPris.SonePris
                    };

                    endretPris.SonePris = innPris.sonePris;
                    db.SaveChanges();

                    logger.Info("Prisen er endret fra " + gammelPris.sonePris + " til " + endretPris.SonePris);

                    return true;
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Feil med endring av pris, se Error-melding");
                return false;
            }
        }
    }
}