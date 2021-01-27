using Juschubut.PdfSign.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace Juschubut.WebPdfSignServer
{
    public class Sign
    {
        public string SignID { get; set; }

        public List<Status> Status { get; set; }

        public ArchivoMetadata ArchivoActual { get; set; }

        public SignSetup Setup { get; set; }

        public Sign()
        {
            this.Status = new List<Status>();
            this.Setup = new SignSetup();
        }

        public ArchivoMetadata CrearArchivoDefault() 
        {
            return new ArchivoMetadata
            {
                ID = 1,
                Nombre = "Documento", 
                ClientID = Guid.NewGuid().ToString()
            };
        }
    }

    public class Status
    {
        public Juschubut.PdfSign.Common.StatusCode Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
    }

    public class InitialStatus : Status
    {
        public string SignID { get; set; }
    }

    public class Repository 
    {
        private static string GetPath(string signID)
        {
            string uploadPath = HttpContext.Current.Server.MapPath("~/Temp");

            return $"{uploadPath.TrimEnd('\\')}\\_sign-dat-{signID}.txt";
        }

        public static Sign Get(string signID)
        {
            try
            {
                var path = GetPath(signID);

                if (File.Exists(path))
                {
                    var json = File.ReadAllText(path);

                    if (json != null)
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<Sign>(json);
                    }
                }
            }
            catch { }

            return null;
        }

        public static void Set(Sign sign)
        {
            try
            {
                var path = GetPath(sign.SignID);

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(sign);
                File.WriteAllText(path, json);
            }
            catch { }
        }
    }
}