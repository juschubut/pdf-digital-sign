using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Juschubut.WebPdfSignServer.Controllers
{
    public class AdminController : Controller
    {
        public JsonResult Status(Models.StatusSetModel request)
        {
            var sign = Repository.Get(request.SignID);

            Juschubut.PdfSign.Common.StatusCode statusCode = PdfSign.Common.StatusCode.Unset;

            Enum.TryParse<Juschubut.PdfSign.Common.StatusCode>(request.StatusCode, out statusCode);

            Status status = new Status
            {
                Fecha = DateTime.Now,
                Codigo = statusCode,
                Descripcion = request.Message
            };

            if (sign == null)
            {
                sign = new Sign
                {
                    SignID = request.SignID, 
                };

                sign.Setup.Archivos.Add(sign.CrearArchivoDefault());

                Repository.Set(sign);
            }

            var file = sign.Setup.Archivos.Find(x => x.ID == request.FileID.GetValueOrDefault(1));

            if (file != null)
            {
                if (file.CodigoStatus != PdfSign.Common.StatusCode.DocumentoFirmado.ToString())
                {
                    file.CodigoStatus = request.StatusCode;
                    file.DescripcionStatus = request.Message;
                }

                sign.ArchivoActual = file;
            }

            sign.Status.Add(status);

            Repository.Set(sign);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public FileResult GetFirma(string id, string token)
        {
            if (string.IsNullOrEmpty(id))
                id = Request.RequestContext.RouteData.Values["id"].ToString();

            if (!ValidarToken(token))
                return File(new byte[] { }, System.Web.MimeMapping.GetMimeMapping("firma.png"));

            Juschubut.Logger.Log.Debug(string.Format("Admin.GetFirma -> id: {0}", id));

            var documento = GetNumeroDocumento(id);

            if (documento == 0)
            {
                Juschubut.Logger.Log.Debug(string.Format("Admin.GetFirma -> Documento inválido: {0}", id));

                return File(new byte[] { }, System.Web.MimeMapping.GetMimeMapping("firma.png"));
            }

            // Primero busco si existe firma local
            string path = GetFirmaOlografaPath(documento);
            FileInfo fileInfo = null;

            // Busco en choique
            Juschubut.Logger.Log.Debug(string.Format("Admin.GetFirma -> id: {0} - Buscando firma choique", id));

            var choiquePath = Choique.ChoiqueService.DownloadFirmaByDocumento(documento);

            Juschubut.Logger.Log.Debug(string.Format("Admin.GetFirma -> id: {0} - Resultado Buscando firma choique -> {1}", id,  choiquePath));

            if (choiquePath != null)
            {
                fileInfo = new FileInfo(choiquePath);

                if (fileInfo.Exists)
                {
                    CopyFile(fileInfo.FullName, path);
                    fileInfo = new FileInfo(path);
                }
            }

            // Si no existe busco copia local
            if (fileInfo == null || !fileInfo.Exists)
            {
                fileInfo = new FileInfo(path);
            }

            if (fileInfo.Exists)
                Juschubut.Logger.Log.Debug(string.Format("Admin.GetFirma -> id: {0} - Se encontro firma -> {1}", id, fileInfo.FullName));
            else
                Juschubut.Logger.Log.Debug(string.Format("Admin.GetFirma -> id: {0} - NO Se encontro firma ", id));

            DateTime expiracion = DateTime.Now.AddMinutes(-15);

            Logger.Log.Debug(string.Format("Admin.GetFirma -> Existe firma para {0} - {1}", path, fileInfo.Exists));

            if (fileInfo.Exists)
            { 
                return File(path, System.Web.MimeMapping.GetMimeMapping(path));
            }
            else 
            {
                return File(new byte[]{}, System.Web.MimeMapping.GetMimeMapping("firma.png"));
            }
        }
 
        public ActionResult UploadFirma(long documento, string token)
        {
            Logger.Log.Debug(string.Format("Admin.UploadFirma -> doc: {0}", documento));

            if (!ValidarToken(token))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Token Inválido");

            var fullPath = GetFirmaOlografaPath(documento);

            if (Request.Files == null || Request.Files.Count == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se encontró archivo que desea subir");

            var fileUpload = Request.Files[0];

            Logger.Log.Debug(string.Format("Admin.UploadFirma -> subiendo archivo doc:{0}", documento));

            using (var fs = new FileStream(fullPath, FileMode.Create))
            {
                var buffer = new byte[fileUpload.InputStream.Length];
                fileUpload.InputStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, buffer.Length);
            }

            Logger.Log.Debug(string.Format("Admin.UploadFirma -> archivo subido doc:{0}", documento));

            return new HttpStatusCodeResult(HttpStatusCode.OK, "No se encontró archivo que desea subir");
        }

        public ActionResult ReplicarFirma(long? documento)
        {
            Logger.Log.Debug(string.Format("Admin.ReplicarFirma -> doc: {0}", documento));

            if (Properties.Settings.Default.IsDMZMode)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Method");

            var result = new List<ServiceResponse>();

            if (documento.GetValueOrDefault() > 0)
                result.Add(ReplicarDocumento(documento.GetValueOrDefault()));
            else
            {
                var documentos = GetDocumentosParaReplicar();

                foreach (var doc in documentos)
                {
                    result.Add(ReplicarDocumento(doc));
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Upload(string signID, int fileId)
        {
            LogDebug(signID, string.Format("Admin.Upload -> fileId: {0}", fileId));

            var sign = Repository.Get(signID);

            var file = sign.Setup.Archivos.Find(x => x.ID == fileId);

            string fileKey = string.Format("{0}_{1}", sign.SignID, file.ID);
            string urlResult = "";

            if (file != null)
            {
                urlResult = string.Format("{0}://{1}{2}?uniqueID={3}", Request.Url.Scheme, Request.Url.Host, Url.Content("~/Sign/Get/"), fileKey);

                file.UrlArchivoFirmado = urlResult;
                file.CodigoStatus = Juschubut.PdfSign.Common.StatusCode.DocumentoFirmado.ToString();
                sign.ArchivoActual = file;
            }

            var fileUpload = Request.Files[0];
            string uploadPath = Server.MapPath("~/Temp");

            uploadPath = uploadPath.TrimEnd('\\') + "\\";

            string fullPath = Path.Combine(uploadPath, fileKey + ".pdf" );

            using (var fs = new FileStream(fullPath, FileMode.Create))
            {
                var buffer = new byte[fileUpload.InputStream.Length];
                fileUpload.InputStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, buffer.Length);
            }

            LogDebug(signID, string.Format("Admin.Upload.Fin -> sign.Setup: {0}", Newtonsoft.Json.JsonConvert.SerializeObject(sign.Setup)));

            Repository.Set(sign);

            return Json(new
            {
                IsSuccess = true
            },
            JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMetadata(string signID)
        {
            var sign = Repository.Get(signID);

            return Json(sign.Setup, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(string id)
        {
            var sign = Repository.Get(id);

            if (sign == null)
            {
                sign = new Sign
                {
                    SignID = "Inválido"
                };
            }

            return View(sign);
        }

        private void LogDebug(string signID, string text)
        {
             Juschubut.Logger.Log.Debug(string.Format("[{0}] - {1}", signID, text));
        }

        private void CopyFile(string source, string destination)
        {
            try
            {
                System.IO.File.Copy(source, destination);
            }
            catch (Exception ex)
            {
                Juschubut.Logger.Log.Error(string.Format("Error copiando archivo de firma. {0}", ex.Message), ex);
            }
        }

        private bool ValidarToken(string token)
        {
            if (!Properties.Settings.Default.IsDMZMode)
                return true;

            if (string.IsNullOrEmpty(token))
                token = "";

            if (string.IsNullOrEmpty(Properties.Settings.Default.DMZToken))
                return true;

            if (token != Properties.Settings.Default.DMZToken)
            {
                Logger.Log.Debug(string.Format("Token invalido: {0}", token));
                return false;
            }

            return true;
        }

        private ServiceResponse CrearResponse(int codigo, string mensaje)
        {
            Logger.Log.Debug(mensaje);

            return new ServiceResponse
            {
                Status = codigo > 0,
                StatusCode = codigo,
                Message = mensaje
            };
        }

        private long GetNumeroDocumento(string id)
        {
            if (string.IsNullOrEmpty(id))
                return 0;

            var prefix = "CUIL_";

            var doc = id.Replace(prefix, "");

            // Ejemplo de numero de CUIL: 20270921680
            if (doc.Length == 11)
                    doc = doc.Substring(2, doc.Length - 3);

            long documento = 0;

            long.TryParse(doc, out documento);

            return documento;
        }

        private string GetFirmaOlografaPath(long doc)
        {
            return HttpContext.Server.MapPath(string.Format("~/Content/Firmas/DNI_{0}.png", doc));
        }

        private ServiceResponse ReplicarDocumento(long documento)
        {
            var choiquePath = Choique.ChoiqueService.DownloadFirmaByDocumento(documento);

            if (string.IsNullOrEmpty(choiquePath))
            {
                return CrearResponse(10, "No se encontró firma ológrafa en choique");
            }

            FileInfo fileInfo = new FileInfo(choiquePath);

            if (!fileInfo.Exists || fileInfo.Length == 0)
            {
                return CrearResponse(10, string.Format("{0} - No se encontró firma ológrafa en choique", documento));
            }

            if (string.IsNullOrEmpty(Properties.Settings.Default.DMZUrl))
            {
                return CrearResponse(-20, string.Format("{0} - No se encontró url de la dmz", documento));
            }

            var baseUrl = Properties.Settings.Default.DMZUrl.TrimEnd('/');


            var url = string.Format("{0}/Admin/UploadFirma?documento={1}&token={2}", baseUrl, documento, Properties.Settings.Default.DMZToken);

            Logger.Log.Debug(string.Format("Replicando hacia {0}", url));

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.UploadFile(url, "POST", fileInfo.FullName);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                return CrearResponse(-30, string.Format("{0} - Error replicando firma a la DMZ.", documento));
            }

            return CrearResponse(1, string.Format("{0} - OK", documento));
        }

        private List<long> GetDocumentosParaReplicar()
        {
            var list = new List<long>();

            var path = HttpContext.Server.MapPath("~/Content/Firmas/");

            var dir = new DirectoryInfo(path);

            var files = dir.GetFiles("DNI_*.png");

            foreach (var f in files)
            {
                var aux = f.Name;

                aux = aux.Replace("DNI_", "");
                aux = aux.Replace(".png", "");


                long doc = 0;

                long.TryParse(aux, out doc);

                if (doc > 0)
                    list.Add(doc);
            }
            return list;
        }

        public class ServiceResponse
        { 
            public bool Status { get; set; }
            public int StatusCode { get; set; }
            public string Message { get; set; }
        }

    }
}
