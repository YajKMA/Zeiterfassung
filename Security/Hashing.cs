using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    public class Hashing
    {
        public string GetHash(string input)
        {
            if (input != null)
            {
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                //Hash value erstellen
                var phash = new Rfc2898DeriveBytes(input, salt, 100000);
                byte[] hash = phash.GetBytes(20);

                //Vorbereitung fürs salzen
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);

                //Ordentliche Menge Salz raufpacken und in einem string speichern
                string savedPasswordHash = Convert.ToBase64String(hashBytes);
                return savedPasswordHash;
            }
            else
            {
                throw new InvalidOperationException("Password is null du hund");
            }
            
        }
    }
}
