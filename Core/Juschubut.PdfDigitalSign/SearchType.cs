using System;
using System.Collections.Generic;
using System.Text;

namespace Juschubut.PdfDigitalSign
{
    public enum SearchType
    {
        ByUserInteface = -1, 
        BySubjectName = 1,
        ByIssuerName = 2,
        BySerialNumber = 4,
        ByEmail = 8
    }
}
