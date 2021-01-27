using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juschubut.PdfSign.Common
{
    public class SignSetup 
    {
        public List<ArchivoMetadata> Archivos { get; set; }

        public SignSetup()
        {
            this.Archivos = new List<ArchivoMetadata>();
        }
    }

    public class ArchivoMetadata
    {
        public int ID { get; set; }
        public string ClientID { get; set; }
        public string Nombre { get; set; }
        public string Url { get; set; }
        public string UrlArchivoFirmado { get; set; }
        public string CodigoStatus { get; set; }
        public string DescripcionStatus { get; set; }
    }
}
