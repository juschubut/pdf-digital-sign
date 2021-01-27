using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juschubut.PdfSign.Common
{
    public class SignStatus
    {
        public string SignID { get; set; }

        public object SetupData { get; set; }

        public List<ArchivoMetadata> Archivos { get; set; }

        public ArchivoMetadata ArchivoActual { get; set; }

        public StatusModel UltimoStatus { get; set; }

        public StatusModel[] Status { get; set; }
    }

    public class StatusModel
    {
        public DateTime Fecha { get; set; }

        public string Codigo { get; set; }

        public string Descripcion { get; set; }
    }
}
