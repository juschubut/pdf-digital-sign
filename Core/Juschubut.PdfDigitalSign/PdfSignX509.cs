using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Asn1;
using System.Collections.Generic;

namespace Juschubut.PdfDigitalSign
{
    public class PdfSignX509 : PdfSign
    {
        private X509Certificate2 _certificate = null;
        private Org.BouncyCastle.X509.X509Certificate[] _chain = null;

        public override byte[] Sign(byte[] pdf, bool detached)
        {
            if (_certificate == null)
            {
                _certificate = GetCertificate();

                if (_certificate == null)
                {
                    throw new Exceptions.CertificateNotFoundException(this.CertificateSelector);
                }

                _chain = GetChain();
            }

            PdfReader reader = new PdfReader(pdf);

            using (MemoryStream result = new MemoryStream())
            {
                PdfStamper stp = PdfStamper.CreateSignature(reader, result, '\0');
                PdfSignatureAppearance sap = stp.SignatureAppearance;

                sap.SetCrypto(null, _chain, null, null);

                this.OnPrepareSignatureEvent(
                   new CertificateInfo
                   {
                       CN = PdfPKCS7.GetSubjectFields(_chain[0]).GetField("CN"),
                       SerialNumber = PdfPKCS7.GetSubjectFields(_chain[0]).GetField("SN")
                   }
                   , sap);

                this.PrepareAppareance(sap);
                
                PdfSignature dic = null;

                if (detached)
                    dic = new PdfSignature(PdfName.ADOBE_PPKLITE, PdfName.ADBE_PKCS7_DETACHED);
                else 
                    dic = new PdfSignature(PdfName.ADOBE_PPKMS, PdfName.ADBE_PKCS7_SHA1);

                dic.Date = new PdfDate(sap.SignDate);
                dic.Name = PdfPKCS7.GetSubjectFields(_chain[0]).GetField("CN");
                
                this.PrepareSignature(dic, sap);
               
                sap.CryptoDictionary = dic;

                int csize = detached ? 10000 : 4000; 
                Hashtable exc = new Hashtable();
                exc[PdfName.CONTENTS] = csize * 2 + 2;
                sap.PreClose(exc);

                byte[] msg = null;
                if (detached)
                    msg = GetMsgDetached(sap);
                else
                    msg = GetMsgHashed(sap);

                byte[] pk = SignMsg(msg, _certificate, detached);

                byte[] outc = new byte[csize];

                PdfDictionary dic2 = new PdfDictionary();

                Array.Copy(pk, 0, outc, 0, pk.Length);

                dic2.Put(PdfName.CONTENTS, new PdfString(outc).SetHexWriting(true));
                sap.Close(dic2);

                return result.ToArray();
            }
        }
        
        private byte[] GetMsgHashed(PdfSignatureAppearance sap)
        {
            HashAlgorithm sha = new SHA1CryptoServiceProvider();

            Stream s = sap.RangeStream;
            int read = 0;
            byte[] buff = new byte[8192];
            while ((read = s.Read(buff, 0, 8192)) > 0)
            {
                sha.TransformBlock(buff, 0, read, buff, 0);
            }
            sha.TransformFinalBlock(buff, 0, 0);

            return sha.Hash;
        }
        
        private byte[] GetMsgDetached(PdfSignatureAppearance sap)
        {
            Stream s = sap.RangeStream;
            MemoryStream ss = new MemoryStream();
            int read = 0;
            byte[] buff = new byte[8192];
            while ((read = s.Read(buff, 0, 8192)) > 0)
            {
                ss.Write(buff, 0, read);
            }

            return ss.ToArray();
        }

        //  Sign the message with the private key of the signer.
        private byte[] SignMsg(Byte[] msg, X509Certificate2 signerCert, bool detached)
        {
            //  Place message in a ContentInfo object.
            //  This is required to build a SignedCms object.
            ContentInfo contentInfo = new ContentInfo(msg);

            //  Instantiate SignedCms object with the ContentInfo above.
            //  Has default SubjectIdentifierType IssuerAndSerialNumber.
            SignedCms signedCms = new SignedCms(contentInfo, detached);

            //  Formulate a CmsSigner object for the signer.
            CmsSigner cmsSigner = new CmsSigner(signerCert);

            // Include the following line if the top certificate in the
            // smartcard is not in the trusted list.
            cmsSigner.IncludeOption = X509IncludeOption.WholeChain;

            //  Sign the CMS/PKCS #7 message. The second argument is
            //  needed to ask for the pin.
            signedCms.ComputeSignature(cmsSigner, false);

            //  Encode the CMS/PKCS #7 message.
            return signedCms.Encode();
        }

        private X509Certificate2 GetCertificate()
        {
            X509Store st = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            st.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection col = st.Certificates;
            X509Certificate2 card = null;

            X509Certificate2Collection sel = null;


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


            if (this.CertificateSelector.SearchType == SearchType.ByUserInteface || (card == null && (sel == null || sel.Count == 0)))
            {
                sel = X509Certificate2UI.SelectFromCollection(col, "Certificates", "Seleccione un certificado", X509SelectionFlag.SingleSelection);
            }

            if (sel != null && sel.Count > 0)
            {
                X509Certificate2Enumerator en = sel.GetEnumerator();
                en.MoveNext();

                if (IsValid(en.Current))
                    card = en.Current;
            }

            st.Close();
            return card;
        }

        private Org.BouncyCastle.X509.X509Certificate[] GetChain()
        {
            Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
             return new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(_certificate.RawData) };
        }

        private bool IsValid(X509Certificate2 cert)
        { 
            DateTime now = DateTime.Now;

            return (cert.NotBefore <= now && cert.NotAfter >= now);
        }
    }
}
