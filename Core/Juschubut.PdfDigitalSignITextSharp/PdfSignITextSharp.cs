using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Juschubut.PdfDigitalSign;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Juschubut.PdfDigitalSignITextSharp
{
    public class PdfSignITextSharp : PdfX509Sign
    {
        private Org.BouncyCastle.X509.X509Certificate _chain = null;

        protected override byte[] SignX509(byte[] pdf, bool detached)
        {
            _chain = GetChain();
            
            using (PdfReader reader = new PdfReader(pdf))
            {
                
                using (MemoryStream result = new MemoryStream())
                {
                    using (PdfStamper stp = PdfStamper.CreateSignature(reader, result, '\0', null, true))
                    {
                        
                        PdfSignatureAppearance sap = stp.SignatureAppearance;

                        sap.Certificate = _chain;

                        var certificateInfo = new Juschubut.PdfDigitalSign.CertificateInfo(this.Certificate);

                        this.OnPrepareSignatureEvent(certificateInfo);

                        sap.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.GRAPHIC;
                        PdfSignature dic = null;

                        if (detached)
                            dic = new PdfSignature(PdfName.ADOBE_PPKLITE, PdfName.ADBE_PKCS7_DETACHED);
                        else
                            dic = new PdfSignature(PdfName.ADOBE_PPKMS, PdfName.ADBE_PKCS7_SHA1);

                        dic.Name = certificateInfo.CN;

                        this.AddSignature(reader, sap, certificateInfo);

                        sap.CryptoDictionary = dic;
                        
                        int csize = detached ? 10000 : 4000;

                        var exc = new Dictionary<PdfName, int>();
                        exc.Add(PdfName.CONTENTS, csize * 2 + 2);
                        sap.PreClose(exc);

                        byte[] pk;

                        using (Stream stream = sap.GetRangeStream())
                        {
                            pk = this.SignMessage(stream, detached);
                        }

                        byte[] outc = new byte[csize];

                        PdfDictionary dic2 = new PdfDictionary();
                        
                        Array.Copy(pk, 0, outc, 0, pk.Length);

                        dic2.Put(PdfName.CONTENTS, new PdfString(outc).SetHexWriting(true));
                        sap.Close(dic2);

                        outc = null;
                        pk = null;

                        return result.ToArray();
                    }
                }                
            }
        }

        private void AddSignature(PdfReader pdf, PdfSignatureAppearance sap, Juschubut.PdfDigitalSign.CertificateInfo certificate)
        {
            int anchoPagina = (int)pdf.GetPageSize(1).Width;

            var rectangle = this.Appearance.GetRectangle(anchoPagina);

            var signatureRect = new iTextSharp.text.Rectangle((float)rectangle.X, (float)rectangle.Y, (float)(rectangle.X + rectangle.Width), (float)(rectangle.Y + rectangle.Height));

            int pagina = pdf.NumberOfPages;

            if (this.Appearance.Page.HasValue && this.Appearance.Page.Value <= pdf.NumberOfPages)
                pagina = this.Appearance.Page.Value;

            sap.SetVisibleSignature(signatureRect, pagina, null);

            sap.SignDate = DateTime.Now;
            sap.Acro6Layers = true;

            sap.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.DESCRIPTION;

            if (!string.IsNullOrEmpty(this.Appearance.SignatureImage))
            {
                var fileInfo = new FileInfo(this.Appearance.SignatureImage);

                if (!fileInfo.Exists)
                {
                    this.Log(string.Format("No se encontro archivo de firma ológrafa ({0})", this.Appearance.SignatureImage));
                }
                else if (fileInfo.Length == 0)
                {
                    this.Log(string.Format("El archivo de firma ológrafa está vacio ({0})", this.Appearance.SignatureImage));
                }
                else
                {
                    try
                    {
                        sap.SignatureGraphic = iTextSharp.text.Image.GetInstance(new Uri(this.Appearance.SignatureImage));
                    }
                    catch (Exception ex)
                    {
                        Log(string.Format("Error al leer el archivo de firma ológrafo. El archivo esta dañado, no es un archivo .png, o no se puede tener acceso de lectura. {0}", this.Appearance.SignatureImage));

                        StringBuilder sb = new StringBuilder();

                        var ex2 = ex;

                        do
                        {
                            sb.AppendLine(ex2.Message);

                            ex2 = ex2.InnerException;
                        }
                        while (ex2 != null);

                        Log("Error: " + sb.ToString());


                        if (!string.IsNullOrEmpty(this.Appearance.SignatureImageDefault))
                        {
                            try
                            {
                                Log("Cargando firma por default");

                                sap.SignatureGraphic = iTextSharp.text.Image.GetInstance(new Uri(this.Appearance.SignatureImageDefault));
                            }
                            catch (Exception ex3)
                            {
                                Log(ex3.Message);
                            }
                        }
                    }

                    sap.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.GRAPHIC;
                    sap.SignatureGraphic.Alignment = iTextSharp.text.Image.ALIGN_TOP;
                }
            }

            // Descripcion de la Firma
            iTextSharp.text.Font textFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, (float)10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

            var phrase = new iTextSharp.text.Phrase();

            phrase.Font = textFont;
            int fontPre = 6;
            int fontFirma =  this.Appearance.Height < 100 ? 8 : 10;
            int fontPost = this.Appearance.Height < 100 ? 7 : 10;

            if (this.Appearance.PreFirma != null)
                AddTexto(phrase, this.Appearance.PreFirma, iTextSharp.text.Font.NORMAL, fontPre);

            AddTexto(phrase, certificate.CN, iTextSharp.text.Font.BOLD, fontFirma);

            if (this.Appearance.PostFirma != null)
                AddTexto(phrase, this.Appearance.PostFirma, iTextSharp.text.Font.NORMAL, fontPost);

            PdfTemplate n2 = sap.GetLayer(0);
            ColumnText ct = new ColumnText(n2);

            float x = n2.BoundingBox.Left;
            float y = n2.BoundingBox.Top;
            float w = n2.BoundingBox.Width;
            float h = n2.BoundingBox.Height;

            float x1 = x;
            float y1 = y;
            float x2 = x + w;
            float y2 = y - h;

            // new working code: crreate a table
            PdfPTable table = new PdfPTable(1);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            table.SetWidths(new[] { 1 });
            table.WidthPercentage = 100;
            table.AddCell(new PdfPCell(phrase)
            {
                HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,
                VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM,
                FixedHeight = y1,
                Border = iTextSharp.text.Rectangle.NO_BORDER
            });
            ct.SetSimpleColumn(x1, y1, x2, y2);
            ct.AddElement(table);
            ct.Go();
        }

        private void AddTexto(iTextSharp.text.Phrase phrase, IEnumerable<string> textos, int fontType, int fontSize)
        {
            foreach (string text in textos)
            {
                AddTexto(phrase, text, fontType, fontSize);
            }
        }

        private void AddTexto(iTextSharp.text.Phrase phrase, string texto, int fontType, int fontSize)
        {
            iTextSharp.text.Font textFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, (float)fontSize, fontType, iTextSharp.text.BaseColor.BLACK);

            phrase.Add(new iTextSharp.text.Chunk(texto, textFont));
            phrase.Add(new iTextSharp.text.Chunk(Environment.NewLine, textFont));
        }

        private Org.BouncyCastle.X509.X509Certificate GetChain()
        {
            Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
            return cp.ReadCertificate(this.Certificate.RawData);
        }
    }
}
