using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Juschubut.PdfDigitalSign;

namespace Juschubut.WinPdfDigitalSign
{
	public partial class FrmFirmador : Form
	{
		private string _fileName = null;
		private string _signedFileName = null;
		private PersonalData _personalData = null;
		private const int LAYOUT_CUSTOM = -1;
		private Control _viewer = null;

		private int LayoutFirma
		{
			get
			{
				if (cboLayout.SelectedIndex == cboLayout.Items.Count - 1)
					return LAYOUT_CUSTOM;
				else
					return cboLayout.SelectedIndex + 1;
			}
		}

		public FrmFirmador()
		{
			InitializeComponent();
		}

		private void FrmFirmador_Load(object sender, EventArgs e)
		{
			cboLayout.SelectedIndex = 0;
			cboFirmante.SelectedIndex = 0;

			CargarPersonalData();

			this.Text += " - v." + FileVersionInfo.GetVersionInfo(GetType().Assembly.Location).FileVersion;
		}

		private void FrmFirmador_FormClosing(object sender, FormClosingEventArgs e)
		{
			PdfViewerClose();
		}

		private void btnAbrirDocumento_Click(object sender, EventArgs e)
		{
			AbrirDocumento();
		}

		private void AbrirDocumento()
		{
			DialogResult result = openFileDialog.ShowDialog();

			if (result == System.Windows.Forms.DialogResult.OK)
			{
				_fileName = openFileDialog.FileName;
				_signedFileName = null;

				MostrarDocumento(_fileName);
			}
		}

		private void MostrarDocumento(string fileName)
		{
			PdfViewerOpen(fileName);
		}

		private void btnFirmar_Click(object sender, EventArgs e)
		{
			GuardarPersonalData();

			Log.Reset();

			if (string.IsNullOrEmpty(_fileName))
			{
				MessageBox.Show(this, "Debe abrir el documento que desea firmar.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

				AbrirDocumento();
				return;
			}

			if (Firmar())
			{
				MostrarDocumento(_signedFileName);

				_fileName = _signedFileName;
			}

			txtLog.Text = Log.GetLogs();
		}

		private void btnGuardar_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(_signedFileName))
			{
				MessageBox.Show(this, "Debe firmar el documento antes de poder guardarlo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			DialogResult result = saveFileDialog.ShowDialog();

			if (result == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					File.Copy(_signedFileName, saveFileDialog.FileName, true);

					Application.Exit();
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, "No se pudo guardar correctamente el documento firmado.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

					WebHelper.Status(PdfSign.Common.StatusCode.Error, string.Format("Error copiando archivo firmado. Error: {0}", ex.Message));
				}
			}
		}

		private void IniciarFirma()
		{
			Log.Debug("Inicio de firma automatica");

			string tempFile = Path.GetTempFileName();

			if (WebHelper.DownloadPdf(tempFile))
			{
				_fileName = tempFile;

				if (App.Setup.ModoOculto)
				{
					this.Visible = false;
				}
				else
				{
					MostrarDocumento(_fileName);
				}

				WebHelper.Status(Juschubut.PdfSign.Common.StatusCode.Debug, "Preparando para firmar");

				if (Firmar())
				{
					WebHelper.Status(Juschubut.PdfSign.Common.StatusCode.Debug, "Documento firmado");

					WebHelper.UploadFile(_signedFileName);

					WebHelper.Status(Juschubut.PdfSign.Common.StatusCode.Completado);

					if (App.Setup.ModoOculto)
					{
						Application.Exit();
					}
				}
				else
				{
					WebHelper.Status(PdfSign.Common.StatusCode.Error, "Problemas con la firma");
				}
			}

			this.Visible = true;

		}

		private bool Firmar()
		{
			_signedFileName = System.IO.Path.GetTempFileName();

			LoadSetup();

			if (App.Setup.Layout != LAYOUT_CUSTOM && App.Setup.NumeroFirmante > App.Setup.Layout)
			{
				MessageBox.Show(this, "El orden de la firma no puede ser mayor a la cantidad de firmantes", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			var firmador = App.CrearFirmador(GetImagenFirmaPropia);
			var result = App.Firmar(firmador, _fileName, _signedFileName);

			if (!result.IsSuccess)
			{
				MessageBox.Show(this, result.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			return result.IsSuccess;
		}

		private void LoadSetup()
		{
			App.Setup.Layout = this.LayoutFirma;
			App.Setup.NumeroFirmante = cboFirmante.SelectedIndex + 1;
			App.Setup.PostFirma = txtCargo.Text.Trim();
			App.Setup.PreFirma = txtLeyenda.Text.Trim();
			App.Setup.PosicionXFirma = ToInt(txtX.Text);
			App.Setup.PosicionYFirma = ToInt(txtY.Text);
			App.Setup.AltoFirma = ToInt(txtAlto.Text);
			App.Setup.AnchoFirma = ToInt(txtAncho.Text);
			App.Setup.FirmaOlografa = chkFirmaOlografa.Checked;

			if (App.Setup.FirmaOlografa && chkFirmaOlografaPropia.Checked)
				App.Setup.FirmaOlografaImagen = lblFirmaOlografaPath.Text;
		}

		private string GetImagenFirmaPropia()
		{
			MessageBox.Show(this, "Seleccione la firma ológrafa que desea utilizar para firmar el documento. Ésta firma debera ser en formato .PNG, y para una correcta visualización contener fondo transparente.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

			var result = openFileDialogFirma.ShowDialog(this);

			if (result == System.Windows.Forms.DialogResult.OK)
			{
				FileInfo file = new FileInfo(openFileDialogFirma.FileName);

				if (file.Exists)
				{
					return file.FullName;
				}
			}

			return null;
		}

		private void cboLayout_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshLayout();
		}

		private void RefreshLayout()
		{
			int layout = this.LayoutFirma;
			int firmante = cboFirmante.SelectedIndex + 1;

			if (layout == LAYOUT_CUSTOM)
			{
				picLayout.Visible = false;
				txtX.Enabled = true;
				txtY.Enabled = true;
				txtAncho.Enabled = true;
				txtAlto.Enabled = true;

				if (!tabOpciones.TabPages.Contains(tabPosicion))
					tabOpciones.TabPages.Add(tabPosicion);

				return;
			}

			if (tabOpciones.TabPages.Contains(tabPosicion))
				tabOpciones.TabPages.Remove(tabPosicion);


			txtX.Enabled = false;
			txtY.Enabled = false;
			txtAncho.Enabled = false;
			txtAlto.Enabled = false;

			picLayout.Visible = true;

			if (layout < 0 || firmante < 0)
				return;

			picLayout.Image = imageListLayout.Images[string.Format("{0}-{1}", layout, firmante)];
		}

		private void cboFirmante_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshLayout();
		}

		private int ToInt(string stringVal, int defValue = 0)
		{
			int val = defValue;

			try
			{
				int.TryParse(stringVal, out val);
			}
			catch
			{ }

			return val;
		}

		private void chkAgregarFirmaOlografa_CheckStateChanged(object sender, EventArgs e)
		{
			chkFirmaOlografaPropia.Visible = chkFirmaOlografa.Checked;
			btnFirmaOlografaExaminar.Visible = chkFirmaOlografa.Checked;
			btnBuscarFirmaRegistrada.Visible = chkFirmaOlografa.Checked;
			btnFirmaOlografaLimpiarCache.Visible = chkFirmaOlografa.Checked;

			picFirmaOlografa.Visible = chkFirmaOlografa.Checked;

			if (chkFirmaOlografaPropia.Visible)
				chkFirmaOlografaPropia_CheckedChanged(null, null);
		}

		private void chkFirmaOlografaPropia_CheckedChanged(object sender, EventArgs e)
		{
			btnFirmaOlografaExaminar.Visible = chkFirmaOlografaPropia.Checked;
			btnFirmaOlografaQuitar.Visible = btnFirmaOlografaExaminar.Visible && !string.IsNullOrEmpty(lblFirmaOlografaPath.Text);
			btnBuscarFirmaRegistrada.Visible = !chkFirmaOlografaPropia.Checked;
			btnFirmaOlografaLimpiarCache.Visible = !chkFirmaOlografaPropia.Checked;
			lblFirmaOlografaPath.Visible = chkFirmaOlografaPropia.Checked;

			if (chkFirmaOlografaPropia.Checked && !string.IsNullOrEmpty(lblFirmaOlografaPath.Text))
			{
				var info = new FileInfo(lblFirmaOlografaPath.Text);

				if (info.Exists)
				{
					if (!MostrarFirmaOlografa(info.FullName))
					{
						lblFirmaOlografaPath.Text = "";
					}
				}
			}
		}

		private void btnBuscarFirmaRegistrada_Click(object sender, EventArgs e)
		{
			Log.Reset();

			Cursor.Current = Cursors.WaitCursor;

			LoadSetup();

			string fileName = null;

			var pdfSign = App.CrearFirmador(null, (fn) =>
			{
				fileName = fn;
			});

			bool status = false;

			try
			{

				byte[] pdfContent = File.ReadAllBytes("pdf-vacio.pdf");
				pdfSign.Sign(pdfContent, true);
				status = true;
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
			}

			Cursor.Current = Cursors.Default;

			if (status && !MostrarFirmaOlografa(fileName))
				MessageBox.Show(this, "No se encontró firma ológrafa para el certificado seleccionado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

			if (!string.IsNullOrEmpty(fileName) && fileName == App.ImagenFirmaDefault)
			{
				string text = string.Format("No se encontró firma ológrafa para el certificado seleccionado.{0}Se mostrará la imagen que se visualiza.{0}{0}Si no desea firmar el documento con la imagen que se visualiza desmarque la opcion \"{1}\".", System.Environment.NewLine, chkFirmaOlografa.Text);
				MessageBox.Show(this, text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			txtLog.Text = Log.GetLogs();
		}

		private void btnFirmaOlografaExaminar_Click(object sender, EventArgs e)
		{
			var fileName = GetImagenFirmaPropia();

			if (fileName != null)
			{
				try
				{
					picFirmaOlografa.Image = System.Drawing.Image.FromFile(fileName);
					lblFirmaOlografaPath.Text = fileName;

					var info = new FileInfo(fileName);

					var kbSize = info.Length / 1024;

					if (kbSize > 100)
					{
						MessageBox.Show("El tamaño de la imágen es demasiado grande. Se recomiendo utilizar imágenes de firma ológrafa no mayores a 100Kb. Puede disminuir el tamaño o calidad de la imagen para obtener un archivo mas pequeño.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}

				}
				catch (Exception ex)
				{
					Log.Error(ex.Message);
					MessageBox.Show("No se puede cargar la imagen seleccionada.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}

				chkFirmaOlografaPropia_CheckedChanged(null, null);
			}
		}

		private void btnFirmaOlografaQuitar_Click(object sender, EventArgs e)
		{
			lblFirmaOlografaPath.Text = "";
			picFirmaOlografa.Image = null;
			chkFirmaOlografaPropia_CheckedChanged(null, null);
		}

		private bool MostrarFirmaOlografa(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
				return false;
			try
			{
				Log.Debug(string.Format("Cargando firma ológrafa. {0}", fileName));
				picFirmaOlografa.Image = System.Drawing.Image.FromFile(fileName);

				return true;
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
				return false;
			}

		}

		private void GuardarPersonalData()
		{
			_personalData.Cargo = txtCargo.Text.Trim();
			_personalData.Leyenda = txtLeyenda.Text.Trim();
			_personalData.FirmaOlografa = chkFirmaOlografa.Checked;
			_personalData.FirmaOlografaPropia = chkFirmaOlografaPropia.Checked;
			_personalData.FirmaOlografaPropiaPath = lblFirmaOlografaPath.Text.Trim();
			_personalData.PosicionAlto = txtAlto.Text.Trim();
			_personalData.PosicionAncho = txtAncho.Text.Trim();
			_personalData.PosicionX = txtX.Text.Trim();
			_personalData.PosicionY = txtY.Text.Trim();

			_personalData.Save();
		}

		private void CargarPersonalData()
		{
			_personalData = PersonalData.Load();

			if (_personalData == null)
				_personalData = new PersonalData();

			if (!string.IsNullOrEmpty(_personalData.Cargo))
			{
				txtCargo.Text = _personalData.Cargo;
				txtLeyenda.Text = _personalData.Leyenda;
				chkFirmaOlografa.Checked = _personalData.FirmaOlografa;
				chkFirmaOlografaPropia.Checked = _personalData.FirmaOlografaPropia;
				lblFirmaOlografaPath.Text = _personalData.FirmaOlografaPropiaPath;
				txtAlto.Text = _personalData.PosicionAlto;
				txtAncho.Text = _personalData.PosicionAncho;
				txtX.Text = _personalData.PosicionX;
				txtY.Text = _personalData.PosicionY;

				chkAgregarFirmaOlografa_CheckStateChanged(null, null);
				chkFirmaOlografaPropia_CheckedChanged(null, null);
			}
		}

		private void InicializarPDFViewer()
		{
			bool ok = false;
			pnlErrorPDF.Visible = false;

			try
			{
				Type t = Type.GetType("AxAcroPDFLib.AxAcroPDF, AxInterop.AcroPDFLib");
				var viewer = Activator.CreateInstance(t) as Control;

				if (viewer != null)
				{
					pnlPdfViewer.Controls.Add(viewer);

					viewer.SetBounds(pnlPdfViewer.Bounds.X, pnlPdfViewer.Bounds.Y, pnlPdfViewer.Bounds.Width, pnlPdfViewer.Bounds.Height);
					viewer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

					_viewer = viewer;

					ok = true;
				}
			}
			catch
			{ }

			if (!ok)
			{
				pnlErrorPDF.Visible = true;
			}
		}

		private void PdfViewerClose()
		{
			if (_viewer != null)
			{
				var viewer = _viewer as AxAcroPDFLib.AxAcroPDF;

				viewer.src = "";
				viewer.Dispose();
			}
		}

		private void PdfViewerOpen(string fileName)
		{
			lblPDF.Text = fileName;

			if (_viewer == null)
				InicializarPDFViewer();

			if (_viewer != null)
			{
				var viewer = _viewer as AxAcroPDFLib.AxAcroPDF;

				viewer.src = fileName;
				viewer.setShowToolbar(false);
				viewer.setPageMode("none");
				viewer.ResumeLayout();
				viewer.Select();
			}
		}

		private void lnkDescargarAcrobatReader_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("https://get.adobe.com/es/reader/");
		}

		private void btnFirmaOlografaLimpiarCache_Click(object sender, EventArgs e)
		{
			try
			{
				Log.Reset();

				var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

				Log.Debug(string.Format("Directorio actual: {0}", dir.FullName));

				var files = dir.GetFiles("DNI_*");

				if (files != null)
				{
					Log.Debug(string.Format("{0} archivos cacheados", files.Length));

					foreach (var f in files)
					{
						Log.Debug(string.Format("Limpiando {0}", f.Name));

						File.Delete(f.FullName);
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
			}

			txtLog.Text = Log.GetLogs();
		}
	}
}
