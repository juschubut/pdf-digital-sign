using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Juschubut.WebPdfSignServer.Models
{
    public class StatusSetModel
    {
        public string SignID { get; set; }
        public int? FileID { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }
}