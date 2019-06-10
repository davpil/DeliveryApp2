using DeliveryApp.Synchro.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Helpers
{
    public class MD5Helper
    {
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for(int i=0; i<bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }

            return hash.ToString();
        }

        public static string GenerateMD5Sum(List<IStringifyable> entity, bool raw = false)
        {
            string val = "";
            foreach (IStringifyable ent in entity)
            {
                val += ":";
                val += ent.Stringify();
            }

            if (raw)
            {
                return val;
            }

            return MD5Hash(val);
        }
    }
}
