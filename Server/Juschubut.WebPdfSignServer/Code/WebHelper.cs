using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Juschubut.WebPdfSignServer
{
    public class WebHelper
    {
        public static bool DownloadFile(string url, string fileName)
        {
            Juschubut.Logger.Log.Debug(string.Format("Descargando archivo. {0}", url));

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Credentials = CredentialCache.DefaultCredentials;
                    client.DownloadFile(url, fileName);
                }

                FileInfo info = new FileInfo(fileName);

                if (info.Exists && info.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);

                return false;
            }
        }
    }
}