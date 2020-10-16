using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oblig_2.Model;

namespace Oblig_2.BLL
{
    public interface ITogBLLRepository
    {
        double LagreBestilling(Bestilling innBestilling);

        string LagreStasjon(Stasjon innStasjon);

        List<Stasjon> HentStasjoner();

        Stasjon HentEnStasjon(int SID);

        Bestilling HentEnBestilling(int BID);

        List<BestillingOversiktViewModel> HentBestillingerTilOversikt();

        Pris HentPrisPrSone();

        bool SlettStasjon(int SID);

        bool SlettBestilling(int BID);

        bool EndreStasjon(int SID, Stasjon enStasjon);

        bool EndreBestilling(int BID, Bestilling enBestillig);

        bool EndrePris(Pris innPris);
    }
}
