using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Juschubut.WebPdfSignServer.Choique
{
    public class ChoiqueService
    {
        /// <summary>
        /// Devuelve la imagen de la firma holográfica de una persona
        /// </summary>
        /// <param name="serialNumber">Numero de serie del certificado digital</param>
        /// <returns>Ruta donde se encuentra la firma</returns>
        public static string DownloadFirmaByDocumento(long documento)
        {
            if (documento < 0)
                return null;

            var temp = string.Format("~/Temp/choique_{0}_{1}.png", documento, DateTime.Now.Ticks);
            temp = HttpContext.Current.Server.MapPath(temp);

            string url = Properties.Settings.Default.UrlChoiqueFirma;

            if (string.IsNullOrEmpty(url))
            {
                Juschubut.Logger.Log.Debug("Suspendiendo busqueda en choique por no estar configurada");
                return null;
            }

            url = url.Replace("{documento}", documento.ToString());

            if (WebHelper.DownloadFile(url, temp))
            {
                Juschubut.Logger.Log.Debug("Firma-Choique encontrada");
                return temp;
            }
            else
            {
                Juschubut.Logger.Log.Debug("Firma-Choique NO encontrada");
                return null;
            }
        }
    }
}