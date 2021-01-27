using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juschubut.WinPdfDigitalSign
{
    public class SignSetup : Juschubut.PdfSign.Common.SignSetup
    {
        public SignSetup(NameValueCollection qs) 
        {
            this.Archivos = new List<PdfSign.Common.ArchivoMetadata>();

            if (qs == null)
                return;

            this.SignID = qs["signid"];
            this.UrlServer = qs["urlServer"];
            this.UrlToUploadFile = string.Format(Properties.Settings.Default.UploadUrl, this.SignID);
            this.ModoOculto = "oculto".Equals(qs["modo"], StringComparison.InvariantCultureIgnoreCase);
            this.PostFirma = qs["postfirma"];
            this.PreFirma = qs["prefirma"];
            this.Layout = GetInt(qs["layout"], 1);
            this.NumeroFirmante = GetInt(qs["firmante"], 1);
            this.IsDebugging = "true".Equals(qs["debug"]) || !IsModoIntegrado;

            this.AltoFirma = GetInt(qs["altofirma"], 150);
            this.AnchoFirma = GetInt(qs["anchofirma"], 400);
            this.PosicionXFirma = GetInt(qs["pxfirma"], 0);
            this.PosicionYFirma = GetInt(qs["pyfirma"], 0);

            if (string.IsNullOrEmpty(this.UrlServer))
            {
                this.UrlServer = Properties.Settings.Default.WebPdfSignServer;
            }

            this.UrlServer = this.UrlServer.TrimEnd('/') + "/";

            if (!string.IsNullOrEmpty(qs["archivo"]))
            {
                this.Archivos.Add(new PdfSign.Common.ArchivoMetadata 
                {
                    ID = 1,
                    Url = qs["archivo"],
                    Nombre = "Documento"
                });

                this.FileID = 1;
            }
        }         

        public string SignID { get; set; }

        public int FileID { get; set; }

        public bool IsModoIntegrado
        {
            get
            {
                return !string.IsNullOrEmpty(this.SignID);
            }
        }

        public Juschubut.PdfSign.Common.ArchivoMetadata GetFile(int fileId)
        {
            return this.Archivos.Find(x => x.ID == fileId);
        }

        public Juschubut.PdfSign.Common.ArchivoMetadata File
        {
            get { return GetFile(this.FileID); }
        }

        public string UrlToUploadFile { get; set; }

        public string UrlServer { get; set; }

        public bool ModoOculto { get; set; }

        public string PreFirma { get; set; }

        public string PostFirma { get; set; }

        public int Layout { get; set; }

        public int AltoFirma { get; set; }

        public int AnchoFirma { get; set; }

        public int PosicionXFirma { get; set; }

        public int PosicionYFirma { get; set; }

        public int NumeroFirmante { get; set; }

        public bool FirmaOlografa { get; set; } = true;

        public string FirmaOlografaImagen { get; set; }

        public bool IsDebugging { get; set; } = true;

        public string[] GetPreFirma()
        {
            return Split(this.PreFirma);
        }

        public string[] GetPostFirma()
        {
            return Split(this.PostFirma);
        }

        private string[] Split(string text)
        {
            var info = new List<string>();

            if (text != null)
            {
                if (text.IndexOf("{fecha-hora}") >= 0)
                    text = text.Replace("{fecha-hora}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                if (text.IndexOf("{fecha}") >= 0)
                    text = text.Replace("{fecha}", DateTime.Now.ToString("dd/MM/yyyy"));


                string[] aux = text.Split('|');

                foreach (string item in aux)
                {
                    if (!string.IsNullOrEmpty(item))
                        info.Add(item);
                }
            }

            return info.ToArray();
        }

        private int GetInt(string value, int defValue)
        {
            if (string.IsNullOrEmpty(value))
                return defValue;

            try
            {
                int val = defValue;

                int.TryParse(value, out val);

                return val;

            }
            catch
            {
                return defValue;
            }
        }

        
    }
}
