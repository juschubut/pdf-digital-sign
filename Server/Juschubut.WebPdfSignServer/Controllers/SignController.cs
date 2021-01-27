using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Juschubut.WebPdfSignServer.Controllers
{
    public class SignController : Controller
    {
        public JsonResult Setup(Juschubut.PdfSign.Common.SignSetup setup)
        {
            string signID = Guid.NewGuid().ToString();

            Juschubut.Logger.Log.Info(string.Format("Setup {0}", signID));

            InitialStatus status = new InitialStatus
            {
                SignID = signID,
                Fecha = DateTime.Now,
                Codigo = PdfSign.Common.StatusCode.Iniciado
            };

            Sign sign = new Sign
            {
                SignID = signID,
                Status = new List<Status>(new Status[] { status })
            };

            if (setup != null)
                sign.Setup = setup;

            if (sign.Setup == null)
                sign.Setup = new PdfSign.Common.SignSetup();

            if (sign.Setup.Archivos == null || sign.Setup.Archivos.Count == 0)
            {
                setup.Archivos.Add(sign.CrearArchivoDefault());
            }

            for (int i = 0; i < sign.Setup.Archivos.Count; i++)
            {
                sign.Setup.Archivos[i].ID = i + 1;

            }

            Repository.Set(sign);

            Juschubut.Logger.Log.Debug("Setup Result");
            Juschubut.Logger.Log.Debug(Newtonsoft.Json.JsonConvert.SerializeObject(sign));

            return Status(sign);
        }

        public ActionResult Get(string uniqueID)
        {
            return File("~/Temp/" + string.Format("{0}.pdf", uniqueID), "application/pdf");
        }

        public JsonResult Status(string id)
        {
            var sign = Repository.Get(id);

            if (sign != null)
            {
                return Status(sign);
            }
            else
            {
                Status status = new Status
                {
                    Fecha = DateTime.Now,
                    Codigo = PdfSign.Common.StatusCode.Error,
                    Descripcion = "Tarea de firma no encontrada"
                };

                return Status(new Sign
                {
                    SignID = id,
                    Status = new List<Status>(new Status[] { status })
                });
            }
        }

        private JsonResult Status(Sign sign)
        {
            var status = sign.Status.Where(x => x.Codigo != PdfSign.Common.StatusCode.Debug).LastOrDefault();

            return Json(new
            {
                signID = sign.SignID,

                archivos = sign.Setup.Archivos.Select(x => FormatearArchivo(x)),

                archivoActual = FormatearArchivo(sign.ArchivoActual),

                ultimoStatus = FormatearStatus(status),

                status = sign.Status.Select(x => FormatearStatus(x)).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        private dynamic FormatearStatus(Status status)
        {
            if (status == null)
            {
                return new
               {
                   fecha = DateTime.Now,
                   codigo = PdfSign.Common.StatusCode.Unset.ToString(),
                   descripcion = "-"
               };
            }
            else
            {
                return new
                {
                    fecha = status.Fecha,
                    codigo = status.Codigo.ToString(),
                    descripcion = status.Descripcion
                };
            }
        }

        private dynamic FormatearArchivo(PdfSign.Common.ArchivoMetadata archivo)
        {
            if (archivo == null)
                return null;
            else
                return new
                {
                    id = archivo.ClientID,
                    nombre = archivo.Nombre,
                    status = new
                    {
                        codigo = archivo.CodigoStatus,
                        descripcion = archivo.DescripcionStatus
                    },
                    urlArchivoFirmado = archivo.UrlArchivoFirmado
                };
        }

    }
}
