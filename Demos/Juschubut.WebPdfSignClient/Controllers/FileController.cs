using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Juschubut.WebPdfSignClient.Controllers
{
    public class FileController : Controller
    {
        /*
        public JsonResult SignInfo(string id)
        {
            Juschubut.PdfSign.Common.SignSetup<int> model = new PdfSign.Common.SignSetup<int>();

            model.SignID = id;
            model.UrlToDownloadFile = string.Format("http://{0}{1}", Request.Url.Host, Url.Content("~/File/DownloadPdf/" + id));

            return Json(model, JsonRequestBehavior.AllowGet);
        }*/

        public ActionResult DownloadPdf(string id)
        {
            return File("~/Content/Demos/demo1.pdf", "application/pdf");
        }

        public JsonResult Upload(string id)
        {
            string uniqueID = id;

            var fileUpload = Request.Files[0];
            string uploadPath = Server.MapPath("~/Temp");

            uploadPath = uploadPath.TrimEnd('\\') + "\\";

            string fullPath = Path.Combine(uploadPath, uniqueID);

            using (var fs = new FileStream(fullPath, FileMode.Create))
            {
                var buffer = new byte[fileUpload.InputStream.Length];
                fileUpload.InputStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, buffer.Length);
            }

            return Json(new
            {
                IsSuccess = true,
                uniqueID = uniqueID
            },
            JsonRequestBehavior.AllowGet);
        }
    }
}