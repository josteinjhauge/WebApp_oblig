using Oblig_2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblig_2.DAL
{
    public class AdminDALStub : IAdminRepository
    {
        public bool GyldigAdmin(Admin innAdmin)
        {
            if (innAdmin.navn == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void CreateAdmin(Admin innAdmin)
        {
            throw new NotImplementedException();
        }
    }
}
