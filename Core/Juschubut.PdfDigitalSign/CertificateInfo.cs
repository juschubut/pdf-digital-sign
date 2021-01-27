using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Juschubut.PdfDigitalSign
{
    public class CertificateInfo
    {
        public string CN { get; set; }
        public string SerialNumber { get; set; }

        public CertificateInfo()
        { }

        public CertificateInfo(X509Certificate2 cert)
        {
            var dic = GetDictionary(cert.Subject);

            if (dic.ContainsKey("CN"))
                this.CN = dic["CN"];

            if (dic.ContainsKey("SERIALNUMBER"))
                this.SerialNumber = dic["SERIALNUMBER"];
        }

        private Dictionary<string, string> GetDictionary(string text)
        {
            string[] aux = text.Split(',');

            var dic = new Dictionary<string,string>();

            if (aux != null)
            {
                foreach (string item in aux)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        string[] vals = item.Trim().Split('=');

                        if (vals != null && vals.Length > 1)
                        {
                            dic.Add(vals[0], vals[1]);
                        }
                    }
                }
            }

            return dic;

        }



    }
}
