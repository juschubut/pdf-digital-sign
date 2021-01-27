using System;
using System.Collections.Generic;
using System.Text;

namespace Juschubut.PdfDigitalSign.Exceptions
{
    public class CertificateNotFoundException : PdfDigitalSignException 
    {
        private CertificateSelector _selector;

        public CertificateNotFoundException(CertificateSelector selector)
        {
            _selector = selector;
        }

        public override string Message
        {
            get
            {
                return string.Format("No se encontro el certificado para realizar la firma (Tipo: {0}, Valor: {1})", _selector.SearchType, _selector.SearchValue); ;
            }
        }

    }
}
