using Oblig_2.DAL;
using Oblig_2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblig_2.BLL
{
    public class AdminBLL : IAdminBLLRepository
    {
        private IAdminRepository _repository;

        public AdminBLL()
        {
            _repository = new AdminDAL();
        }

        public AdminBLL(IAdminRepository stub)
        {
            _repository = stub;
        }
        public bool GyldigAdmin(Admin innAdmin)
        {
            return _repository.GyldigAdmin(innAdmin); 
        }

        public void CreateAdmin(Admin innAdmin)
        {
            _repository.CreateAdmin(innAdmin);
        } 
    }
}