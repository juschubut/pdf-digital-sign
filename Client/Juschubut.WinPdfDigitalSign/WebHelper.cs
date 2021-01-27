using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Juschubut.WinPdfDigitalSign
{
    public class WebHelper
    {


        public static T Post<T>(string url, Dictionary<string, string> data)
        {
            if (!url.StartsWith("http"))
                url = GetUrl(url);

            var request = System.Net.WebRequest.Create(url);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            if (data != null)
            {
                var postData = "";

                foreach (KeyValuePair<string, string> kvp in data)
                {
                    postData += string.Format("{0}={1}&", kvp.Key, kvp.Value);
                }

                postData = postData.TrimEnd('&');


                var dataBytes = Encoding.ASCII.GetBytes(postData);
                request.ContentLength = dataBytes.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(dataBytes, 0, dataBytes.Length);
                }
            }
            else
            {
                request.ContentLength = 0;
            }

            var response = request.GetResponse() as System.Net.HttpWebResponse;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string result = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result);
            }

            return default(T);
        }

        public static T Post<T>(string url)
        {
            return Post<T>(url, null);
        }

        public static bool DownloadPdf(string fileName)
        {
            Log.Debug("Bajando pdf");

            var file = App.Setup.File;

            bool result = DownloadFile(file.Url, fileName);

            if (result)
            {
                WebHelper.Status(Juschubut.PdfSign.Common.StatusCode.ArchivoDescargado, file.Url);

                file.CodigoStatus = Juschubut.PdfSign.Common.StatusCode.ArchivoDescargado.ToString();
                WebHelper.Status(Juschubut.PdfSign.Common.StatusCode.Debug, "File downloaded");
            }

            return result;
        }

        public static bool DownloadFile(string url, string fileName)
        {
            Log.Debug(string.Format("Descargando archivo. {0}", url));

            try
            {
                var tempFileName = fileName + "_temp";

                using (WebClient client = new WebClient())
                {
                    client.Credentials = CredentialCache.DefaultCredentials;
                    client.DownloadFile(url, tempFileName);
                }

                FileInfo info = new FileInfo(tempFileName);

                if (info.Exists && info.Length > 0)
                {
                    if (System.IO.File.Exists(fileName))
                        System.IO.File.Delete(fileName);

                    info.MoveTo(fileName);

                    Log.Debug("Archivo bajado");
                    return true;
                }
                else
                {
                    Log.Debug("Archivo no encontrado");
                    return false;
                }                
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("Error descargando archivo. Error: {0}", ex.Message);

                Status(Juschubut.PdfSign.Common.StatusCode.Error, errorMessage);

                Log.Error(errorMessage);

                return false;
            }
        }

        public static bool UploadFile(string fileName)
        {
            Log.Debug("Subiendo archivo firamdo");
            
            try
            {
                var file = App.Setup.File;

                WebHelper.Status(Juschubut.PdfSign.Common.StatusCode.DocumentoFirmado);

                using (WebClient client = new WebClient())
                {
                    string url = GetUrl(App.Setup.UrlToUploadFile);

                    url += string.Format("?signID={0}&fileId={1}", App.Setup.SignID, file.ID);

                    client.UploadFile(url, fileName);
                }

                Log.Debug("Archivo firmado subido exitosamente");

                WebHelper.Status(Juschubut.PdfSign.Common.StatusCode.Debug, "Documento actualizado");

                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("Error subiendo archivo firmado. Error: {0}", ex.Message);

                Log.Error(errorMessage);
            }

            return false;
        }

        public static void Status(PdfSign.Common.StatusCode statusCode)
        {
            Status(statusCode, "");
        }

        public static void Status(PdfSign.Common.StatusCode statusCode, string message)
        {
            if (App.Setup != null && App.Setup.IsModoIntegrado)
            {
                try
                {
                    Post<object>(Properties.Settings.Default.StatusUrl,
                         new Dictionary<string, string> 
                    {
                       { "SignID", App.Setup.SignID },
                       { "FileID", App.Setup.FileID.ToString() },
                       { "StatusCode", statusCode.ToString()},
                       { "Message", message}
                    });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }
            }

            Log.Debug(string.Format("[{0}] - {1}", statusCode, message));
        }

        internal static bool ExisteFirma(string imagenFirma)
        {
            try
            {
                Log.Debug(string.Format("Verificando firma holografica en servidor. {0}", imagenFirma));

                return Post<bool>(
                    Properties.Settings.Default.GetFirma,
                        new Dictionary<string, string> 
                {
                    { "imageSign", imagenFirma}
                });

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }


            return false;
        }

        public static PdfSign.Common.SignSetup GetMetadata()
        {
            try
            {
                return Post<PdfSign.Common.SignSetup>(Properties.Settings.Default.GetMetadata,
                        new Dictionary<string, string> 
                {
                    { "signID", App.Setup.SignID }
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

            return null;
        }

        public static string GetUrl(string path)
        {
            return App.Setup.UrlServer + path;
        }

        public static string GetUrlDMZ(string path)
        {
            return Properties.Settings.Default.WebPdfSignServerDMZ + path;
        }
    }
}
