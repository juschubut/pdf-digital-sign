using System;
using System.Collections.Generic;
using System.Text;

namespace Juschubut.PdfDigitalSign
{


    public class Logger
    {
        public virtual void Debug(string texto)
        { 
        
        }
    }

    public class PrepareSignEventArgs : EventArgs
    {
        public CertificateInfo CertificateInfo { get; private set; }

        public PrepareSignEventArgs(CertificateInfo cetificateInfo)
        {
            this.CertificateInfo = cetificateInfo;
        }
    }

    public delegate void PrepareSignatureEventHandler(object sender, PrepareSignEventArgs args);

    public abstract class PdfSign
    {
        public Logger Logger { get; set; }

        public event PrepareSignatureEventHandler OnPrepareSignature;

        public SignatureAppearance Appearance { get; set; }

        public CertificateSelector CertificateSelector { get; set; }

        public abstract byte[] Sign(byte[] pdf, bool detached);

        public PdfSign()
        {
            this.Appearance = new SignatureAppearance();
            this.CertificateSelector = new CertificateSelector();
        }


        protected void Log(string text)
        {
            if (this.Logger != null)
                this.Logger.Debug(text);
        }

        protected void OnPrepareSignatureEvent(Juschubut.PdfDigitalSign.CertificateInfo cetificateInfo)
        {
            if (this.OnPrepareSignature != null)
            {
                this.OnPrepareSignature(this, new PrepareSignEventArgs(cetificateInfo));
            }
        }
    }
}
