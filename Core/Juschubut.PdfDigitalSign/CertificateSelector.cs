using System;
using System.Collections.Generic;
using System.Text;

namespace Juschubut.PdfDigitalSign
{
    public class CertificateSelector
    {
        private string _searchValue = "";
        private SearchType _searchType = SearchType.BySerialNumber;

        public string SearchValue
        {
            get { return _searchValue; }
            set { _searchValue = value; }
        }

        public SearchType SearchType
        {
            get { return _searchType; }
            set
            {
                _searchType = value;
            }
        }
    }
}
