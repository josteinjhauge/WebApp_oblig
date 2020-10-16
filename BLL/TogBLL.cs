using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oblig_2.DAL;
using Oblig_2.Model;

namespace Oblig_2.BLL
{
    public class TogBLL : ITogBLLRepository
    {
        private ITogRepository _repository;

        public TogBLL()
        {
            _repository = new TogDAL();
        }

        public TogBLL(ITogRepository stub)
        {
            _repository = stub;
        }
        public double LagreBestilling(Bestilling innBestilling)
        {
            return _repository.LagreBestilling(innBestilling);
        }

        public string LagreStasjon(Stasjon innStasjon)
        {
            return _repository.LagreStasjon(innStasjon);
        }

        public List<Stasjon> HentStasjoner()
        {
            List<Stasjon> alleStasjoner = _repository.HentStasjoner();
            return alleStasjoner;
        }

        public Stasjon HentEnStasjon(int SID)
        {
            Stasjon enStasjon = _repository.HentEnStasjon(SID);
            return enStasjon;
        }

        public Bestilling HentEnBestilling(int BID)
        {
            Bestilling enBestilling = _repository.HentEnBestilling(BID);
            return enBestilling;
        }

        public List<BestillingOversiktViewModel> HentBestillingerTilOversikt()
        {
            List<BestillingOversiktViewModel> alleBestillinger = _repository.HentBestillingerTilOversikt();
            return alleBestillinger;
        }

        public Pris HentPrisPrSone()
        {
            return _repository.HentPrisPrSone();
        }

        public bool SlettStasjon(int SID)
        {
            return _repository.SlettStasjon(SID);
        }

        public bool SlettBestilling(int BID)
        {
            return _repository.SlettBestilling(BID);
        }

        public bool EndreStasjon(int SID, Stasjon enStasjon)
        {
            return _repository.EndreStasjon(SID, enStasjon);
        }

        public bool EndreBestilling(int BID, Bestilling enBestilling)
        {
            return _repository.EndreBestilling(BID, enBestilling);
        }

        public bool EndrePris(Pris innPris)
        {
            return _repository.EndrePris(innPris);
        }
    }
}