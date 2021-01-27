using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juschubut.PdfSign.Common
{
    public enum StatusCode
    {
        Iniciado, 
        ArchivoDescargado, 
        Firmando, 
        DocumentoFirmado, 
        Completado,
        Error,
        Unset, 
        Debug
    }
}
