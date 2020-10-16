using Oblig_2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblig_2.DAL
{
    public interface IAdminRepository
    {
        bool GyldigAdmin(Admin innAdmin);

        void CreateAdmin(Admin innAdmin);
    }
}
