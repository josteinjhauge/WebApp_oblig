using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Oblig_2.Model;

namespace Oblig_2.DAL
{
    public class AdminDAL : IAdminRepository
    {
        private static Logger logger = LogManager.GetLogger("fileLogger");

        public bool GyldigAdmin(Admin innAdmin)
        {
            using (var db = new AdminContext())
            {
                try
                {
                    Adminen funnetAdmin = db.Adminen.FirstOrDefault(b => b.Navn == innAdmin.navn);

                    if (funnetAdmin != null)
                    {
                        byte[] passordForTest = GetHash(innAdmin.passord, funnetAdmin.Salt);
                        bool riktigBruker = funnetAdmin.Passord.SequenceEqual(passordForTest);
                        logger.Info("Logger Gyldig Admin: " + funnetAdmin.Navn);
                        //Logger ikke passord da dette skal være sjult
                        return riktigBruker;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch(Exception e)
                {
                    logger.Error(e, "Feil med gyldig admin , se Error - melding");
                    return false;
                }
            }
        }

        private byte[] GetHash(string innPassord, byte[] innSalt)
        {
            const int keyLength = 24;
            var pbkdf2 = new Rfc2898DeriveBytes(innPassord, innSalt, 500);
            return pbkdf2.GetBytes(keyLength);
        }

        private static byte[] GetSalt()
        {
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csprng.GetBytes(salt);
            return salt;
        }

        // Denne ble kun brukt til å lage adminen, og er derfor ikke i bruk lengre
        public void CreateAdmin(Admin innAdmin)
        {
            using (var db = new AdminContext())
            {
                try
                {
                    var nyAdmin = new Adminen();
                    byte[] salt = GetSalt();
                    byte[] hash = GetHash(innAdmin.passord, salt);
                    nyAdmin.Navn = innAdmin.navn;
                    nyAdmin.Passord = hash;
                    nyAdmin.Salt = salt;
                    db.Adminen.Add(nyAdmin);
                    db.SaveChanges();
                    logger.Info("Logger av create admin, men ikke i bruk: " + nyAdmin.Navn);
                }
                catch (Exception e)
                {
                    logger.Error(e, "Noe gikk galt med create av admin");
                    Debug.WriteLine(e.StackTrace);
                }
            }
        }
    }
}