using Juschubut.PdfDigitalSign;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Juschubut.WinPdfDigitalSign
{

    public class SignLogger : PdfDigitalSign.Logger
    {
        public override void Debug(string texto)
        {
            base.Debug(texto);

            WebHelper.Status(Juschubut.PdfSign.Common.StatusCode.Debug, texto);
        }
    }

    public class App
    {
        public static string ImagenFirmaDefault
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + "firma-default.png";
            }
        }

        private static SignSetup _setup = null;

        public static SignSetup Setup { get { return _setup; } }

        public static void Initialize()
        {
            var param = GetParameters();

            string signID = param["signid"];

            _setup = new SignSetup(param);

            // Solo para loguear datos del querystring
            GetParameters();

            Log.Debug(string.Format("Setup"));
            Log.Debug(string.Format("UrlServer: {0}", _setup.UrlServer));
            Log.Debug(string.Format("UrlToUploadFile: {0}", _setup.UrlToUploadFile));
            Log.Debug(string.Format("ModoOculto: {0}", _setup.ModoOculto));
            Log.Debug(string.Format("PreFirma: {0}", _setup.PreFirma));
            Log.Debug(string.Format("PostFirma: {0}", _setup.PostFirma));
            Log.Debug(string.Format("Layout: {0}", _setup.Layout));
            Log.Debug(string.Format("PosicionXFirma: {0}", _setup.PosicionXFirma));
            Log.Debug(string.Format("PosicionYFirma: {0}", _setup.PosicionYFirma));



            if (!string.IsNullOrEmpty(param["archivo"]))
            {
                var archivo = new PdfSign.Common.ArchivoMetadata
                {
                    Nombre = "Documento",
                    Url = param["archivo"],
                    ID = 1
                };

                _setup.Archivos.Add(archivo);

                Log.Debug(string.Format("Archivo a firmar {0}", archivo.Url));
            }
        }        

        private static System.Collections.Specialized.NameValueCollection GetParameters()
        {
            var result = new System.Collections.Specialized.NameValueCollection();

            string queryString = GetQueryString();
            StringBuilder sb = new StringBuilder();

            Log.Debug(string.Format("Cargando parametros. {0}", queryString));

            if (!string.IsNullOrEmpty(queryString))
            {
                Uri uri = new Uri(queryString);

                var qs = System.Web.HttpUtility.ParseQueryString(uri.Query);

                foreach (string name in qs)
                {
                    result[name] = qs[name];
                }
            }

            return result;
        }

        private static string GetQueryString()
        {
            /*
            string signID = "305b8742-522f-4561-9dfb-b842c527f1a7";

            string posicion = "&pxfirma=50&pyfirma=100&anchofirma=300&altofirma=150";

            var url = "http://apptt/juschubutpdfsign/Juschubut.WinPdfDigitalSign.application?signID=" + signID + "&modo=oculto&prefirma=Firmado Digitalmente Por&postfirma=Juez Penal&layout=0&firmante=0" + posicion + "&urlServer=http://tw-rap15/PdfSignServer&debug=true";

             return url;*/
     
            if (AppDomain.CurrentDomain != null
              && AppDomain.CurrentDomain.SetupInformation != null
              && AppDomain.CurrentDomain.SetupInformation.ActivationArguments != null
              && AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData != null)
            {
                var activationData = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData;

                
                if (activationData != null)
                {
                    return activationData.FirstOrDefault();
                }
            }

            Log.Debug("QueryString: Vacio");

            return string.Empty;
            

            //return "http://apptt/juschubutpdfsign/Juschubut.WinPdfDigitalSign.application?signID=a1232cf9-de12-496e-b372-4182b9c34c00&file=http://localhost/PdfSignClient/File/DownloadPdf/123&result=http://localhost/PdfSignClient/File/Upload/123&hidden=false";
        }

        public static string GetImageSignature(string firma)
        {
            string file = string.Format("{0}{1}.png", AppDomain.CurrentDomain.BaseDirectory, firma);

            Log.Debug(string.Format("Verificando existencia de firma. {0}", file));

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);

            if (!fileInfo.Exists || fileInfo.Length == 0 || Math.Abs((fileInfo.LastWriteTime - DateTime.Now).Minutes) > 10)
            {
                Log.Debug("No existe copia local");

                string url = Properties.Settings.Default.GetFirma.TrimEnd('/') + "?id=" + firma + "&token=" + Properties.Settings.Default.SecurityToken;

                bool result = WebHelper.DownloadFile(WebHelper.GetUrl(url), file);

                // Intento 1) Servidor intranet
                if (result)
                {
                    Log.Debug(string.Format("Firma existente. {0}", firma));

                    if (result)
                        fileInfo = new System.IO.FileInfo(file);
                }
                else
                {
                    // Intento 2) Servidor dmz
                    result = WebHelper.DownloadFile(WebHelper.GetUrlDMZ(url), file);

                    if (result)
                    {
                        Log.Debug(string.Format("Firma existente en dmz. {0}", firma));

                        if (result)
                            fileInfo = new System.IO.FileInfo(file);
                    }
                }
            }

            if (fileInfo != null && fileInfo.Exists)
                return fileInfo.FullName;
            else 
                return null;
        }

        private static string GetImageDefaultSignature()
        {
            var file = App.ImagenFirmaDefault;

            Log.Debug(string.Format("Verificando existencia de firma default. {0}", file));

            var fileInfo = new FileInfo(file);

            if (fileInfo.Exists)
                return fileInfo.FullName;
            else 
                return null;
        }

        public static PdfDigitalSign.PdfSign CrearFirmador(Func<string> onImageSignatureNotFound, Action<string> onImageSignatureDetected = null)
        {
            var pdfSign = new PdfDigitalSignITextSharp.PdfSignITextSharp();

            pdfSign.Logger = Log.Logger;
            pdfSign.Appearance.PreFirma = App.Setup.GetPreFirma();
            pdfSign.Appearance.PostFirma = App.Setup.GetPostFirma();
            pdfSign.Appearance.Layout = App.Setup.Layout;
            pdfSign.Appearance.NumeroFirma = App.Setup.NumeroFirmante;
            pdfSign.Appearance.FirmaOlografa = App.Setup.FirmaOlografa;
            pdfSign.Appearance.X = App.Setup.PosicionXFirma;
            pdfSign.Appearance.Y = App.Setup.PosicionYFirma;
            pdfSign.Appearance.Height = App.Setup.AltoFirma;
            pdfSign.Appearance.Width = App.Setup.AnchoFirma;
            pdfSign.Appearance.SignatureImageDefault = App.ImagenFirmaDefault;

            if (App.Setup.FirmaOlografa)
            {
                if (string.IsNullOrWhiteSpace(App.Setup.FirmaOlografaImagen))
                {
                    pdfSign.OnPrepareSignature += (object sender, PdfDigitalSign.PrepareSignEventArgs args) =>
                    {
                        string fileName = null;

                        // Intento 1: Trata de obtener firma ológrafa por numero de serie
                        // Intento 2: Trata de obtener firma ológrafa por CN
                        if (!string.IsNullOrEmpty(args.CertificateInfo.SerialNumber))
                        {
                            fileName = GetImagenFirmaRRHH(args.CertificateInfo.SerialNumber, args.CertificateInfo.CN);
                        }

                        // Intento 3: Pide archivo de firma ológrafa al usuario
                        if (fileName == null)
                        {
                            if (onImageSignatureNotFound != null)
                                fileName = onImageSignatureNotFound();
                        }

                        // Intento 4: Utiliza una imagen por defecto
                        if (fileName == null)
                            fileName = App.GetImageDefaultSignature();

                        if (fileName != null)
                        {
                            pdfSign.Appearance.SignatureImage = fileName;

                            if (onImageSignatureDetected != null)
                                onImageSignatureDetected(fileName);
                        }
                    };
                }
                else
                {
                    var fileInfo = new FileInfo(App.Setup.FirmaOlografaImagen);

                    if (fileInfo.Exists)
                        pdfSign.Appearance.SignatureImage = fileInfo.FullName;
                }
            }

            return pdfSign;
        }

        public static string GetImagenFirmaRRHH(string cuil, string cn)
        {
            string fileName = null;
            // Intento 1: Trata de obtener firma ológrafa por cuil
            if (!string.IsNullOrEmpty(cuil))
            {
                fileName = GetImagenFirmaPorCUIL(cuil);
            }

            // Intento 2: Trata de obtener firma ológrafa por CN
            if (fileName == null)
            {
                if (!string.IsNullOrEmpty(cn))
                {
                    fileName = GetImagenFirmaPorCN(cn);
                }
            }

            return fileName;
        }

        private static string GetImagenFirmaPorCUIL(string cuil)
        {
            if (cuil == null)
                return null;

            string img = cuil.Trim().Replace(" ", "_");

            if (!img.StartsWith("CUIL"))
                img = "CUIL_" + img;

            return App.GetImageSignature(img);
        }

        private static string GetImagenFirmaPorCN(string cn)
        {
            if (cn == null)
                return null;

            string img = "CN_" + cn.Trim().Replace(" ", "_");

            return App.GetImageSignature(img);
        }

        public static Result Firmar(PdfDigitalSign.PdfSign pdfSign, string pdfInput, string pdfOutput)
        {
            var result = new Result();
            Exception exception = null;
            string message = "";

            Log.Debug("Iniciando proceso de firmado");

            try
            {
                WebHelper.Status(Juschubut.PdfSign.Common.StatusCode.Firmando);

                WebHelper.Status(PdfSign.Common.StatusCode.Debug, string.Format("Firmar.PdfInput: {0}", pdfInput));

                byte[] pdfContent = File.ReadAllBytes(pdfInput);

                WebHelper.Status(PdfSign.Common.StatusCode.Debug, string.Format("Firmar.ReadAllBytes: {0}", pdfContent.Length));

                pdfContent = pdfSign.Sign(pdfContent, true);
                
                WebHelper.Status(PdfSign.Common.StatusCode.Debug, string.Format("Firmar.Guardano: {0}", pdfOutput));

                File.WriteAllBytes(pdfOutput, pdfContent);

                WebHelper.Status(PdfSign.Common.StatusCode.Debug, "Firmar.Guardano.Fin");

                pdfSign = null;

                result.IsSuccess = true;

                return result;
            }
            catch (Juschubut.PdfDigitalSign.Exceptions.CertificateNotFoundException ex)
            {
                exception = ex;
                message = "No se seleccionó ningun certificado válido";
            }
            catch (Exception ex)
            {
                exception = ex;
                message = "No se pudo firmar el documento";
            }

            result.IsSuccess = false;
            result.Message = message;

            string errorMessage = string.Format("{0}. Error: {1}", message, exception.Message);

            Log.Error(errorMessage);

            WebHelper.Status(PdfSign.Common.StatusCode.Error, errorMessage);

            return result;
        }
    }

    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
