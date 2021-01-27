using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Juschubut.PdfDigitalSign
{
    public abstract class PdfX509Sign : PdfDigitalSign.PdfSign
    {
        public X509Certificate2 Certificate { get; private set; }

        public override byte[] Sign(byte[] pdf, bool detached)
        {
            this.Log("Iniciando firma digital");

            if (this.Certificate == null)
            {
                this.Log("Buscando certificado");

                this.Certificate = GetCertificate();

                if (this.Certificate == null)
                {
                    throw new Juschubut.PdfDigitalSign.Exceptions.CertificateNotFoundException(this.CertificateSelector);
                }
                else
                {
                    this.Log("Certificado encontrado");
                }
            }
            else
            {
                this.Log("Certificado existente");
            }

            this.Log("Iniciando firma digital X509");

            if (pdf == null || pdf.Length == 0)
            {
                this.Log("PDF Inválido");
                return null;
            }

            return SignX509(pdf, detached);
        }

        protected abstract byte[] SignX509(byte[] pdf, bool detached);

        private byte[] GetMsgHashed(Stream stream)
        {
            using (HashAlgorithm sha = new SHA1CryptoServiceProvider())
            {
                int read = 0;
                byte[] buff = new byte[8192];
                while ((read = stream.Read(buff, 0, 8192)) > 0)
                {
                    sha.TransformBlock(buff, 0, read, buff, 0);
                }
                sha.TransformFinalBlock(buff, 0, 0);

                return sha.Hash;
            }
        }

        private byte[] GetMsgDetached(Stream stream)
        {
            using (MemoryStream ss = new MemoryStream())
            {
                int read = 0;
                byte[] buff = new byte[8192];
                while ((read = stream.Read(buff, 0, 8192)) > 0)
                {
                    ss.Write(buff, 0, read);
                }

                return ss.ToArray();
            }
        }

        protected byte[] SignMessage(Stream stream, bool detached)
        {   
            if (detached)
                return this.SignMsg(this.GetMsgDetached(stream), detached);
            else 
                return this.SignMsg(this.GetMsgHashed(stream), detached);
        }

        //  Sign the message with the private key of the signer.
        private byte[] SignMsg(Byte[] msg, bool detached)
        {
            //  Place message in a ContentInfo object.
            //  This is required to build a SignedCms object.
            ContentInfo contentInfo = new ContentInfo(msg);

            //  Instantiate SignedCms object with the ContentInfo above.
            //  Has default SubjectIdentifierType IssuerAndSerialNumber.
            SignedCms signedCms = new SignedCms(contentInfo, detached);
            
            //  Formulate a CmsSigner object for the signer.
            CmsSigner cmsSigner = new CmsSigner(this.Certificate);

            // Include the following line if the top certificate in the
            // smartcard is not in the trusted list.
            cmsSigner.IncludeOption = X509IncludeOption.EndCertOnly;

            //  Sign the CMS/PKCS #7 message. The second argument is
            //  needed to ask for the pin.
            signedCms.ComputeSignature(cmsSigner, false);
            
            //  Encode the CMS/PKCS #7 message.
            return signedCms.Encode();
        }       

        private X509Certificate2 GetCertificate()
        {
            X509Store st = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            this.Log("Abriendo Store");

            st.Open(OpenFlags.ReadOnly);

            this.Log("Store abierto");

            X509Certificate2Collection col = st.Certificates;
            X509Certificate2 card = null;

            X509Certificate2Collection sel = null;

            this.Log("Recorriendo certificados");

            foreach (X509Certificate2 cert in col)
            {

                if (this.CertificateSelector.SearchType == SearchType.BySubjectName)
                {
                    if (cert.SubjectName.Name.ToUpper().IndexOf(string.Format("CN={0}", this.CertificateSelector.SearchValue).ToUpper()) >= 0 && IsValid(cert))
                    {
                        card = cert;
                        break;
                    }
                }
                else if (this.CertificateSelector.SearchType == SearchType.ByEmail)
                {
                    if (cert.SubjectName.Name.ToUpper().IndexOf(string.Format("E={0}", this.CertificateSelector.SearchValue).ToUpper()) >= 0 && IsValid(cert))
                    {
                        card = cert;
                        break;
                    }
                }
                else if (this.CertificateSelector.SearchType == SearchType.ByIssuerName)
                {
                    if (cert.IssuerName.Name.ToUpper().IndexOf(string.Format("CN={0}", this.CertificateSelector.SearchValue).ToUpper()) >= 0 && IsValid(cert))
                    {
                        card = cert;
                        break;
                    }
                }
                else if (this.CertificateSelector.SearchType == SearchType.BySerialNumber)
                {
                    if (cert.SerialNumber.Equals(this.CertificateSelector.SearchValue, StringComparison.InvariantCultureIgnoreCase) && IsValid(cert))
                    {
                        card = cert;
                        break;
                    }
                }
            }

            this.Log("Certificado automatico no encontrado. Iniciando solicitud usuario");

            if (this.CertificateSelector.SearchType == SearchType.ByUserInteface || (card == null && (sel == null || sel.Count == 0)))
            {
                sel = X509Certificate2UI.SelectFromCollection(col, "Certificates", "Seleccione un certificado", X509SelectionFlag.SingleSelection);
            }

            this.Log("Fin solicitud certificado usuario");

            if (sel != null && sel.Count > 0)
            {
                X509Certificate2Enumerator en = sel.GetEnumerator();
                en.MoveNext();

                if (IsValid(en.Current))
                    card = en.Current;
            }

            this.Log("Cerrando store");

            st.Close();
            return card;
        }

        private bool IsValid(X509Certificate2 cert)
        {
            DateTime now = DateTime.Now;

            return (cert.NotBefore <= now && cert.NotAfter >= now);
        }


    }
}
