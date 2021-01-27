using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Juschubut.WinPdfDigitalSign
{
    public partial class FrmModoIntegrado : Form
    {
        private PdfDigitalSign.PdfSign _pdfSign;


        private PdfDigitalSign.PdfSign Signer
        {
            get
            {
                if (_pdfSign == null)
                {
                    _pdfSign = App.CrearFirmador(null);
                    _pdfSign.Logger = new SignLogger();
                }

                return _pdfSign;
            }
        }


        public FrmModoIntegrado()
        {
            InitializeComponent();

            Log.Reset();
            lnkLog.Text = string.Format("{0} (v.{1})", lnkLog.Text, FileVersionInfo.GetVersionInfo(GetType().Assembly.Location).FileVersion);

            this.Visible = !App.Setup.ModoOculto;

            Status("Iniciando Firma digital...");
        }

        private void Status(string text) 
        {
            lblStatus.Text = text;
            Application.DoEvents();

            Log.Debug(text);

            txtLog.Text = Log.GetLogs();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.Visible = !App.Setup.ModoOculto;

            if (this.Visible)
                this.Focus();

            timer.Enabled = false;

            Status("Iniciando Firma digital...");

            if (App.Setup.Archivos.Count == 0)
            {
                Status("Descargando metadata...");

                WebHelper.Status(PdfSign.Common.StatusCode.Debug, "Descargando metadata");

                var metadata = WebHelper.GetMetadata();

                if (metadata != null)
                {
                    App.Setup.Archivos = metadata.Archivos;
                }
            }

            if (App.Setup.Archivos == null || App.Setup.Archivos.Count == 0)
            {
                MensajeError("No se encontraron archivos para firmar.");
                return;                
            }

            string templateStatus = "{1} - {0}...";

            if (App.Setup.Archivos.Count == 0)
                templateStatus = "{0}...";

            if (App.Setup.Archivos.Count == 0)
            {
                WebHelper.Status(PdfSign.Common.StatusCode.Error, "No se encontraron archivos para firmar");
                return;
            }

            foreach (var file in App.Setup.Archivos)
            {
                App.Setup.FileID = file.ID;

                if (string.IsNullOrEmpty(file.Url))
                {
                    continue;
                }

                string inputFile = Path.GetTempFileName();
                string outputFile = Path.GetTempFileName();

                Status(string.Format(templateStatus, "Descargando", file.Nombre));

                if (WebHelper.DownloadPdf(inputFile))
                {
                    Application.DoEvents();

                    file.CodigoStatus = Juschubut.PdfSign.Common.StatusCode.Firmando.ToString();

                    WebHelper.Status(Juschubut.PdfSign.Common.StatusCode.Debug, "Preparando para firmar");

                    Application.DoEvents();

                    Status(string.Format(templateStatus, "Firmando", file.Nombre));

                    var result = App.Firmar(this.Signer, inputFile, outputFile);

                    Application.DoEvents();

                    if (result.IsSuccess)
                    {
                        WebHelper.Status(Juschubut.PdfSign.Common.StatusCode.Debug, "Documento firmado");

                        Status(string.Format(templateStatus, "Terminando firma", file.Nombre));

                        WebHelper.UploadFile(outputFile);
                    }
                    else
                    {
                        WebHelper.Status(PdfSign.Common.StatusCode.Error, "No se pudo firmar el documento actual");

                        MensajeError(result.Message);
                        return;
                    }
                }
                else 
                {
                    WebHelper.Status(PdfSign.Common.StatusCode.Error, "No se pudo descagar el documento para firmar");
                    return;
                }
            }

            WebHelper.Status(Juschubut.PdfSign.Common.StatusCode.Completado);
            
            Environment.ExitCode = 1;
            Application.Exit();
        }

        private void MensajeError(string mensaje)
        {
            WebHelper.Status(PdfSign.Common.StatusCode.Debug, mensaje);
            WebHelper.Status(PdfSign.Common.StatusCode.Error, "Problemas con la firma");

            Status(mensaje);

            btnCerrar.Visible = true;
            this.Visible = true;
        }

        private void lnkLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtLog.Visible)
            {
                this.Size = new Size(this.Size.Width, 110);
                txtLog.Visible = false;
            }
            else
            {
                this.Size = new Size(this.Size.Width, 260);
                txtLog.Text = Log.GetLogs();
                txtLog.Visible = true;
            }
        }
    }
}
