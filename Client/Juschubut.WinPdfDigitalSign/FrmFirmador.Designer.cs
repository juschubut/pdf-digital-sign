namespace Juschubut.WinPdfDigitalSign
{
    partial class FrmFirmador
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFirmador));
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtCargo = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cboLayout = new System.Windows.Forms.ComboBox();
			this.picLayout = new System.Windows.Forms.PictureBox();
			this.cboFirmante = new System.Windows.Forms.ComboBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label10 = new System.Windows.Forms.Label();
			this.txtAlto = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.txtAncho = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.txtY = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtX = new System.Windows.Forms.TextBox();
			this.btnGuardar = new System.Windows.Forms.Button();
			this.btnAbrirDocumento = new System.Windows.Forms.Button();
			this.btnFirmar = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.imageListLayout = new System.Windows.Forms.ImageList(this.components);
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.openFileDialogFirma = new System.Windows.Forms.OpenFileDialog();
			this.tabOpciones = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.tabLeyenda = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.txtLeyenda = new System.Windows.Forms.TextBox();
			this.tabFirma = new System.Windows.Forms.TabPage();
			this.lblFirmaOlografaPath = new System.Windows.Forms.Label();
			this.btnFirmaOlografaQuitar = new System.Windows.Forms.Button();
			this.chkFirmaOlografaPropia = new System.Windows.Forms.CheckBox();
			this.btnFirmaOlografaExaminar = new System.Windows.Forms.Button();
			this.picFirmaOlografa = new System.Windows.Forms.PictureBox();
			this.btnBuscarFirmaRegistrada = new System.Windows.Forms.Button();
			this.chkFirmaOlografa = new System.Windows.Forms.CheckBox();
			this.tabPosicion = new System.Windows.Forms.TabPage();
			this.tabLogs = new System.Windows.Forms.TabPage();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.pnlPdfViewer = new System.Windows.Forms.Panel();
			this.pnlErrorPDF = new System.Windows.Forms.Panel();
			this.lblPDF = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label6 = new System.Windows.Forms.Label();
			this.lnkDescargarAcrobatReader = new System.Windows.Forms.LinkLabel();
			this.btnFirmaOlografaLimpiarCache = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.picLayout)).BeginInit();
			this.panel1.SuspendLayout();
			this.tabOpciones.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.tabLeyenda.SuspendLayout();
			this.tabFirma.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picFirmaOlografa)).BeginInit();
			this.tabPosicion.SuspendLayout();
			this.tabLogs.SuspendLayout();
			this.pnlPdfViewer.SuspendLayout();
			this.pnlErrorPDF.SuspendLayout();
			this.SuspendLayout();
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(10, 13);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(130, 20);
			this.label4.TabIndex = 9;
			this.label4.Text = "Cargo";
			this.toolTip1.SetToolTip(this.label4, "Juez Penal");
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 41);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(130, 17);
			this.label2.TabIndex = 5;
			this.label2.Text = "Cantidad Firmantes";
			// 
			// txtCargo
			// 
			this.txtCargo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCargo.Location = new System.Drawing.Point(141, 10);
			this.txtCargo.Margin = new System.Windows.Forms.Padding(4);
			this.txtCargo.Name = "txtCargo";
			this.txtCargo.Size = new System.Drawing.Size(252, 23);
			this.txtCargo.TabIndex = 10;
			this.toolTip1.SetToolTip(this.txtCargo, "Juez Penal");
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 69);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 17);
			this.label3.TabIndex = 5;
			this.label3.Text = "Orden";
			// 
			// cboLayout
			// 
			this.cboLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLayout.FormattingEnabled = true;
			this.cboLayout.Items.AddRange(new object[] {
            "Una firma",
            "Dos firmas",
            "Tres firmas",
            "Cuatro firmas",
            "Cinco firmas",
            "Seis firmas",
            "Una firma posición específica"});
			this.cboLayout.Location = new System.Drawing.Point(141, 37);
			this.cboLayout.Name = "cboLayout";
			this.cboLayout.Size = new System.Drawing.Size(252, 24);
			this.cboLayout.TabIndex = 7;
			this.cboLayout.SelectedIndexChanged += new System.EventHandler(this.cboLayout_SelectedIndexChanged);
			// 
			// picLayout
			// 
			this.picLayout.Location = new System.Drawing.Point(400, 9);
			this.picLayout.Name = "picLayout";
			this.picLayout.Size = new System.Drawing.Size(70, 80);
			this.picLayout.TabIndex = 8;
			this.picLayout.TabStop = false;
			// 
			// cboFirmante
			// 
			this.cboFirmante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFirmante.FormattingEnabled = true;
			this.cboFirmante.Items.AddRange(new object[] {
            "Primera firma",
            "Segunda firma",
            "Tercera firma",
            "Cuatra firma",
            "Quinta firma",
            "Sexta firma"});
			this.cboFirmante.Location = new System.Drawing.Point(141, 65);
			this.cboFirmante.Name = "cboFirmante";
			this.cboFirmante.Size = new System.Drawing.Size(252, 24);
			this.cboFirmante.TabIndex = 7;
			this.cboFirmante.SelectedIndexChanged += new System.EventHandler(this.cboFirmante_SelectedIndexChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.txtAlto);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.txtAncho);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.txtY);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.txtX);
			this.panel1.Location = new System.Drawing.Point(6, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(206, 57);
			this.panel1.TabIndex = 11;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(4, 29);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(36, 17);
			this.label10.TabIndex = 19;
			this.label10.Text = "Alto:";
			this.toolTip1.SetToolTip(this.label10, "Juez Penal");
			// 
			// txtAlto
			// 
			this.txtAlto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAlto.Location = new System.Drawing.Point(45, 26);
			this.txtAlto.Margin = new System.Windows.Forms.Padding(4);
			this.txtAlto.Name = "txtAlto";
			this.txtAlto.Size = new System.Drawing.Size(38, 23);
			this.txtAlto.TabIndex = 9;
			this.txtAlto.Text = "150";
			this.toolTip1.SetToolTip(this.txtAlto, "Alto de la firma");
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(114, 29);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(52, 17);
			this.label9.TabIndex = 17;
			this.label9.Text = "Ancho:";
			this.toolTip1.SetToolTip(this.label9, "Juez Penal");
			// 
			// txtAncho
			// 
			this.txtAncho.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAncho.Location = new System.Drawing.Point(166, 26);
			this.txtAncho.Margin = new System.Windows.Forms.Padding(4);
			this.txtAncho.Name = "txtAncho";
			this.txtAncho.Size = new System.Drawing.Size(38, 23);
			this.txtAncho.TabIndex = 10;
			this.txtAncho.Text = "400";
			this.toolTip1.SetToolTip(this.txtAncho, "Ancho de la firma");
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(145, 5);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(21, 17);
			this.label8.TabIndex = 15;
			this.label8.Text = "Y:";
			this.toolTip1.SetToolTip(this.label8, "Juez Penal");
			// 
			// txtY
			// 
			this.txtY.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtY.Location = new System.Drawing.Point(166, 2);
			this.txtY.Margin = new System.Windows.Forms.Padding(4);
			this.txtY.Name = "txtY";
			this.txtY.Size = new System.Drawing.Size(38, 23);
			this.txtY.TabIndex = 8;
			this.txtY.Text = "0";
			this.toolTip1.SetToolTip(this.txtY, "Posición Y");
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(19, 5);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(21, 17);
			this.label7.TabIndex = 13;
			this.label7.Text = "X:";
			this.toolTip1.SetToolTip(this.label7, "Juez Penal");
			// 
			// txtX
			// 
			this.txtX.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtX.Location = new System.Drawing.Point(45, 2);
			this.txtX.Margin = new System.Windows.Forms.Padding(4);
			this.txtX.Name = "txtX";
			this.txtX.Size = new System.Drawing.Size(38, 23);
			this.txtX.TabIndex = 7;
			this.txtX.Text = "0";
			this.toolTip1.SetToolTip(this.txtX, "Posición X");
			// 
			// btnGuardar
			// 
			this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnGuardar.Location = new System.Drawing.Point(775, 463);
			this.btnGuardar.Margin = new System.Windows.Forms.Padding(4);
			this.btnGuardar.Name = "btnGuardar";
			this.btnGuardar.Size = new System.Drawing.Size(148, 28);
			this.btnGuardar.TabIndex = 4;
			this.btnGuardar.Text = "Guardar";
			this.btnGuardar.UseVisualStyleBackColor = true;
			this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
			// 
			// btnAbrirDocumento
			// 
			this.btnAbrirDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAbrirDocumento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAbrirDocumento.Location = new System.Drawing.Point(16, 463);
			this.btnAbrirDocumento.Margin = new System.Windows.Forms.Padding(4);
			this.btnAbrirDocumento.Name = "btnAbrirDocumento";
			this.btnAbrirDocumento.Size = new System.Drawing.Size(148, 28);
			this.btnAbrirDocumento.TabIndex = 4;
			this.btnAbrirDocumento.Text = "Abrir Documento";
			this.btnAbrirDocumento.UseVisualStyleBackColor = true;
			this.btnAbrirDocumento.Click += new System.EventHandler(this.btnAbrirDocumento_Click);
			// 
			// btnFirmar
			// 
			this.btnFirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFirmar.Location = new System.Drawing.Point(620, 463);
			this.btnFirmar.Margin = new System.Windows.Forms.Padding(4);
			this.btnFirmar.Name = "btnFirmar";
			this.btnFirmar.Size = new System.Drawing.Size(148, 28);
			this.btnFirmar.TabIndex = 4;
			this.btnFirmar.Text = "Firmar Documento";
			this.btnFirmar.UseVisualStyleBackColor = true;
			this.btnFirmar.Click += new System.EventHandler(this.btnFirmar_Click);
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "documento.pdf";
			this.openFileDialog.Filter = "Pdf|*.pdf";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "pdf";
			this.saveFileDialog.FileName = "documento.pdf";
			this.saveFileDialog.Filter = "Pdf|*.pdf";
			this.saveFileDialog.Title = "Guardar documento firmado como...";
			// 
			// imageListLayout
			// 
			this.imageListLayout.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListLayout.ImageStream")));
			this.imageListLayout.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListLayout.Images.SetKeyName(0, "1-1");
			this.imageListLayout.Images.SetKeyName(1, "2-1");
			this.imageListLayout.Images.SetKeyName(2, "2-2");
			this.imageListLayout.Images.SetKeyName(3, "3-1");
			this.imageListLayout.Images.SetKeyName(4, "3-2");
			this.imageListLayout.Images.SetKeyName(5, "3-3");
			this.imageListLayout.Images.SetKeyName(6, "4-1");
			this.imageListLayout.Images.SetKeyName(7, "4-2");
			this.imageListLayout.Images.SetKeyName(8, "4-3");
			this.imageListLayout.Images.SetKeyName(9, "4-4");
			this.imageListLayout.Images.SetKeyName(10, "5-1");
			this.imageListLayout.Images.SetKeyName(11, "5-2");
			this.imageListLayout.Images.SetKeyName(12, "5-3");
			this.imageListLayout.Images.SetKeyName(13, "5-4");
			this.imageListLayout.Images.SetKeyName(14, "5-5");
			this.imageListLayout.Images.SetKeyName(15, "6-1");
			this.imageListLayout.Images.SetKeyName(16, "6-2");
			this.imageListLayout.Images.SetKeyName(17, "6-3");
			this.imageListLayout.Images.SetKeyName(18, "6-4");
			this.imageListLayout.Images.SetKeyName(19, "6-5");
			this.imageListLayout.Images.SetKeyName(20, "6-6");
			// 
			// toolTip1
			// 
			this.toolTip1.IsBalloon = true;
			this.toolTip1.ShowAlways = true;
			this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.toolTip1.ToolTipTitle = "Ejemplo";
			// 
			// openFileDialogFirma
			// 
			this.openFileDialogFirma.Filter = "Firma ológrafa|*.png";
			this.openFileDialogFirma.Title = "Seleccione la firma ológrafa. Se recomienda un tamaño de 300x250 pixeles.";
			// 
			// tabOpciones
			// 
			this.tabOpciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabOpciones.Controls.Add(this.tabGeneral);
			this.tabOpciones.Controls.Add(this.tabLeyenda);
			this.tabOpciones.Controls.Add(this.tabFirma);
			this.tabOpciones.Controls.Add(this.tabPosicion);
			this.tabOpciones.Controls.Add(this.tabLogs);
			this.tabOpciones.Location = new System.Drawing.Point(3, 321);
			this.tabOpciones.Name = "tabOpciones";
			this.tabOpciones.SelectedIndex = 0;
			this.tabOpciones.Size = new System.Drawing.Size(934, 135);
			this.tabOpciones.TabIndex = 19;
			// 
			// tabGeneral
			// 
			this.tabGeneral.BackColor = System.Drawing.SystemColors.Control;
			this.tabGeneral.Controls.Add(this.label4);
			this.tabGeneral.Controls.Add(this.label2);
			this.tabGeneral.Controls.Add(this.txtCargo);
			this.tabGeneral.Controls.Add(this.cboFirmante);
			this.tabGeneral.Controls.Add(this.label3);
			this.tabGeneral.Controls.Add(this.picLayout);
			this.tabGeneral.Controls.Add(this.cboLayout);
			this.tabGeneral.Location = new System.Drawing.Point(4, 25);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(926, 106);
			this.tabGeneral.TabIndex = 3;
			this.tabGeneral.Text = "General";
			// 
			// tabLeyenda
			// 
			this.tabLeyenda.BackColor = System.Drawing.SystemColors.Control;
			this.tabLeyenda.Controls.Add(this.label1);
			this.tabLeyenda.Controls.Add(this.txtLeyenda);
			this.tabLeyenda.Location = new System.Drawing.Point(4, 25);
			this.tabLeyenda.Name = "tabLeyenda";
			this.tabLeyenda.Padding = new System.Windows.Forms.Padding(3);
			this.tabLeyenda.Size = new System.Drawing.Size(926, 106);
			this.tabLeyenda.TabIndex = 0;
			this.tabLeyenda.Text = "Leyenda";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 41);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 17);
			this.label1.TabIndex = 7;
			this.label1.Text = "Leyenda";
			// 
			// txtLeyenda
			// 
			this.txtLeyenda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLeyenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLeyenda.Location = new System.Drawing.Point(136, 37);
			this.txtLeyenda.Margin = new System.Windows.Forms.Padding(4);
			this.txtLeyenda.Name = "txtLeyenda";
			this.txtLeyenda.Size = new System.Drawing.Size(306, 23);
			this.txtLeyenda.TabIndex = 8;
			this.txtLeyenda.Text = "Firmado digitalmente el {fecha-hora} por";
			// 
			// tabFirma
			// 
			this.tabFirma.BackColor = System.Drawing.SystemColors.Control;
			this.tabFirma.Controls.Add(this.btnFirmaOlografaLimpiarCache);
			this.tabFirma.Controls.Add(this.lblFirmaOlografaPath);
			this.tabFirma.Controls.Add(this.btnFirmaOlografaQuitar);
			this.tabFirma.Controls.Add(this.chkFirmaOlografaPropia);
			this.tabFirma.Controls.Add(this.btnFirmaOlografaExaminar);
			this.tabFirma.Controls.Add(this.picFirmaOlografa);
			this.tabFirma.Controls.Add(this.btnBuscarFirmaRegistrada);
			this.tabFirma.Controls.Add(this.chkFirmaOlografa);
			this.tabFirma.Location = new System.Drawing.Point(4, 25);
			this.tabFirma.Name = "tabFirma";
			this.tabFirma.Padding = new System.Windows.Forms.Padding(3);
			this.tabFirma.Size = new System.Drawing.Size(926, 106);
			this.tabFirma.TabIndex = 1;
			this.tabFirma.Text = "Firma ológrafa";
			// 
			// lblFirmaOlografaPath
			// 
			this.lblFirmaOlografaPath.AutoSize = true;
			this.lblFirmaOlografaPath.Location = new System.Drawing.Point(7, 86);
			this.lblFirmaOlografaPath.Name = "lblFirmaOlografaPath";
			this.lblFirmaOlografaPath.Size = new System.Drawing.Size(0, 17);
			this.lblFirmaOlografaPath.TabIndex = 25;
			// 
			// btnFirmaOlografaQuitar
			// 
			this.btnFirmaOlografaQuitar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnFirmaOlografaQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFirmaOlografaQuitar.Location = new System.Drawing.Point(389, 58);
			this.btnFirmaOlografaQuitar.Margin = new System.Windows.Forms.Padding(4);
			this.btnFirmaOlografaQuitar.Name = "btnFirmaOlografaQuitar";
			this.btnFirmaOlografaQuitar.Size = new System.Drawing.Size(159, 28);
			this.btnFirmaOlografaQuitar.TabIndex = 24;
			this.btnFirmaOlografaQuitar.Text = "Quitar";
			this.btnFirmaOlografaQuitar.UseVisualStyleBackColor = true;
			this.btnFirmaOlografaQuitar.Visible = false;
			this.btnFirmaOlografaQuitar.Click += new System.EventHandler(this.btnFirmaOlografaQuitar_Click);
			// 
			// chkFirmaOlografaPropia
			// 
			this.chkFirmaOlografaPropia.AutoSize = true;
			this.chkFirmaOlografaPropia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.chkFirmaOlografaPropia.Location = new System.Drawing.Point(10, 62);
			this.chkFirmaOlografaPropia.Name = "chkFirmaOlografaPropia";
			this.chkFirmaOlografaPropia.Size = new System.Drawing.Size(189, 21);
			this.chkFirmaOlografaPropia.TabIndex = 23;
			this.chkFirmaOlografaPropia.Text = "Utilizar una imagen propia";
			this.chkFirmaOlografaPropia.UseVisualStyleBackColor = true;
			this.chkFirmaOlografaPropia.CheckedChanged += new System.EventHandler(this.chkFirmaOlografaPropia_CheckedChanged);
			// 
			// btnFirmaOlografaExaminar
			// 
			this.btnFirmaOlografaExaminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnFirmaOlografaExaminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFirmaOlografaExaminar.Location = new System.Drawing.Point(216, 58);
			this.btnFirmaOlografaExaminar.Margin = new System.Windows.Forms.Padding(4);
			this.btnFirmaOlografaExaminar.Name = "btnFirmaOlografaExaminar";
			this.btnFirmaOlografaExaminar.Size = new System.Drawing.Size(159, 28);
			this.btnFirmaOlografaExaminar.TabIndex = 22;
			this.btnFirmaOlografaExaminar.Text = "Examinar";
			this.btnFirmaOlografaExaminar.UseVisualStyleBackColor = true;
			this.btnFirmaOlografaExaminar.Visible = false;
			this.btnFirmaOlografaExaminar.Click += new System.EventHandler(this.btnFirmaOlografaExaminar_Click);
			// 
			// picFirmaOlografa
			// 
			this.picFirmaOlografa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.picFirmaOlografa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picFirmaOlografa.Location = new System.Drawing.Point(571, 3);
			this.picFirmaOlografa.Name = "picFirmaOlografa";
			this.picFirmaOlografa.Size = new System.Drawing.Size(337, 98);
			this.picFirmaOlografa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picFirmaOlografa.TabIndex = 21;
			this.picFirmaOlografa.TabStop = false;
			// 
			// btnBuscarFirmaRegistrada
			// 
			this.btnBuscarFirmaRegistrada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnBuscarFirmaRegistrada.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnBuscarFirmaRegistrada.Location = new System.Drawing.Point(216, 26);
			this.btnBuscarFirmaRegistrada.Margin = new System.Windows.Forms.Padding(4);
			this.btnBuscarFirmaRegistrada.Name = "btnBuscarFirmaRegistrada";
			this.btnBuscarFirmaRegistrada.Size = new System.Drawing.Size(332, 28);
			this.btnBuscarFirmaRegistrada.TabIndex = 20;
			this.btnBuscarFirmaRegistrada.Text = "Buscar firma ológrafa registrada en RRHH >>";
			this.btnBuscarFirmaRegistrada.UseVisualStyleBackColor = true;
			this.btnBuscarFirmaRegistrada.Click += new System.EventHandler(this.btnBuscarFirmaRegistrada_Click);
			// 
			// chkFirmaOlografa
			// 
			this.chkFirmaOlografa.AutoSize = true;
			this.chkFirmaOlografa.Checked = true;
			this.chkFirmaOlografa.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkFirmaOlografa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.chkFirmaOlografa.Location = new System.Drawing.Point(10, 26);
			this.chkFirmaOlografa.Name = "chkFirmaOlografa";
			this.chkFirmaOlografa.Size = new System.Drawing.Size(166, 21);
			this.chkFirmaOlografa.TabIndex = 19;
			this.chkFirmaOlografa.Text = "Agregar firma ológrafa";
			this.chkFirmaOlografa.UseVisualStyleBackColor = true;
			this.chkFirmaOlografa.CheckStateChanged += new System.EventHandler(this.chkAgregarFirmaOlografa_CheckStateChanged);
			// 
			// tabPosicion
			// 
			this.tabPosicion.BackColor = System.Drawing.SystemColors.Control;
			this.tabPosicion.Controls.Add(this.panel1);
			this.tabPosicion.Location = new System.Drawing.Point(4, 25);
			this.tabPosicion.Name = "tabPosicion";
			this.tabPosicion.Size = new System.Drawing.Size(926, 106);
			this.tabPosicion.TabIndex = 2;
			this.tabPosicion.Text = "Posición";
			// 
			// tabLogs
			// 
			this.tabLogs.BackColor = System.Drawing.SystemColors.Control;
			this.tabLogs.Controls.Add(this.txtLog);
			this.tabLogs.Location = new System.Drawing.Point(4, 25);
			this.tabLogs.Name = "tabLogs";
			this.tabLogs.Padding = new System.Windows.Forms.Padding(3);
			this.tabLogs.Size = new System.Drawing.Size(926, 106);
			this.tabLogs.TabIndex = 4;
			this.tabLogs.Text = "Logs";
			// 
			// txtLog
			// 
			this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLog.BackColor = System.Drawing.SystemColors.Control;
			this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtLog.Location = new System.Drawing.Point(6, 6);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtLog.Size = new System.Drawing.Size(902, 94);
			this.txtLog.TabIndex = 0;
			// 
			// pnlPdfViewer
			// 
			this.pnlPdfViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlPdfViewer.Controls.Add(this.pnlErrorPDF);
			this.pnlPdfViewer.Location = new System.Drawing.Point(2, 1);
			this.pnlPdfViewer.Name = "pnlPdfViewer";
			this.pnlPdfViewer.Size = new System.Drawing.Size(937, 318);
			this.pnlPdfViewer.TabIndex = 11;
			// 
			// pnlErrorPDF
			// 
			this.pnlErrorPDF.Controls.Add(this.lblPDF);
			this.pnlErrorPDF.Controls.Add(this.linkLabel1);
			this.pnlErrorPDF.Controls.Add(this.label6);
			this.pnlErrorPDF.Controls.Add(this.lnkDescargarAcrobatReader);
			this.pnlErrorPDF.Location = new System.Drawing.Point(158, 25);
			this.pnlErrorPDF.Name = "pnlErrorPDF";
			this.pnlErrorPDF.Size = new System.Drawing.Size(565, 265);
			this.pnlErrorPDF.TabIndex = 20;
			this.pnlErrorPDF.Visible = false;
			// 
			// lblPDF
			// 
			this.lblPDF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lblPDF.Location = new System.Drawing.Point(55, 146);
			this.lblPDF.Name = "lblPDF";
			this.lblPDF.Size = new System.Drawing.Size(459, 58);
			this.lblPDF.TabIndex = 5;
			// 
			// linkLabel1
			// 
			this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
			this.linkLabel1.Location = new System.Drawing.Point(51, 56);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(463, 90);
			this.linkLabel1.TabIndex = 4;
			this.linkLabel1.Text = "Para poder visualizar el documento PDF necesita tener instalado Acrobat Reader. D" +
    "e todas formas Ud. podrá firmar el documento que acaba de seleccionar.";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.Red;
			this.label6.Location = new System.Drawing.Point(54, 33);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(125, 23);
			this.label6.TabIndex = 3;
			this.label6.Text = "ATENCIÓN:";
			// 
			// lnkDescargarAcrobatReader
			// 
			this.lnkDescargarAcrobatReader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lnkDescargarAcrobatReader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lnkDescargarAcrobatReader.LinkArea = new System.Windows.Forms.LinkArea(51, 132);
			this.lnkDescargarAcrobatReader.Location = new System.Drawing.Point(55, 204);
			this.lnkDescargarAcrobatReader.Name = "lnkDescargarAcrobatReader";
			this.lnkDescargarAcrobatReader.Size = new System.Drawing.Size(402, 46);
			this.lnkDescargarAcrobatReader.TabIndex = 1;
			this.lnkDescargarAcrobatReader.TabStop = true;
			this.lnkDescargarAcrobatReader.Text = "Si lo desea, puede descargarla Acrobat Reader desde https://get.adobe.com/es/read" +
    "er/";
			this.lnkDescargarAcrobatReader.UseCompatibleTextRendering = true;
			this.lnkDescargarAcrobatReader.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDescargarAcrobatReader_LinkClicked);
			// 
			// btnFirmaOlografaLimpiarCache
			// 
			this.btnFirmaOlografaLimpiarCache.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnFirmaOlografaLimpiarCache.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFirmaOlografaLimpiarCache.Location = new System.Drawing.Point(216, 58);
			this.btnFirmaOlografaLimpiarCache.Margin = new System.Windows.Forms.Padding(4);
			this.btnFirmaOlografaLimpiarCache.Name = "btnFirmaOlografaLimpiarCache";
			this.btnFirmaOlografaLimpiarCache.Size = new System.Drawing.Size(192, 28);
			this.btnFirmaOlografaLimpiarCache.TabIndex = 26;
			this.btnFirmaOlografaLimpiarCache.Text = "Limpiar cache";
			this.btnFirmaOlografaLimpiarCache.UseVisualStyleBackColor = true;
			this.btnFirmaOlografaLimpiarCache.Click += new System.EventHandler(this.btnFirmaOlografaLimpiarCache_Click);
			// 
			// FrmFirmador
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(941, 502);
			this.Controls.Add(this.pnlPdfViewer);
			this.Controls.Add(this.tabOpciones);
			this.Controls.Add(this.btnAbrirDocumento);
			this.Controls.Add(this.btnGuardar);
			this.Controls.Add(this.btnFirmar);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "FrmFirmador";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Poder Judicial del Chubut - Firma Digital";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmFirmador_FormClosing);
			this.Load += new System.EventHandler(this.FrmFirmador_Load);
			((System.ComponentModel.ISupportInitialize)(this.picLayout)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tabOpciones.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.tabGeneral.PerformLayout();
			this.tabLeyenda.ResumeLayout(false);
			this.tabLeyenda.PerformLayout();
			this.tabFirma.ResumeLayout(false);
			this.tabFirma.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picFirmaOlografa)).EndInit();
			this.tabPosicion.ResumeLayout(false);
			this.tabLogs.ResumeLayout(false);
			this.tabLogs.PerformLayout();
			this.pnlPdfViewer.ResumeLayout(false);
			this.pnlErrorPDF.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnFirmar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnAbrirDocumento;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboLayout;
        private System.Windows.Forms.ComboBox cboFirmante;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ImageList imageListLayout;
        private System.Windows.Forms.PictureBox picLayout;
        private System.Windows.Forms.TextBox txtCargo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.OpenFileDialog openFileDialogFirma;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtAlto;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAncho;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtX;
		private System.Windows.Forms.TabControl tabOpciones;
		private System.Windows.Forms.TabPage tabLeyenda;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtLeyenda;
		private System.Windows.Forms.TabPage tabFirma;
		private System.Windows.Forms.Button btnBuscarFirmaRegistrada;
		private System.Windows.Forms.CheckBox chkFirmaOlografa;
		private System.Windows.Forms.TabPage tabPosicion;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.PictureBox picFirmaOlografa;
		private System.Windows.Forms.TabPage tabLogs;
		private System.Windows.Forms.TextBox txtLog;
		private System.Windows.Forms.CheckBox chkFirmaOlografaPropia;
		private System.Windows.Forms.Button btnFirmaOlografaExaminar;
		private System.Windows.Forms.Button btnFirmaOlografaQuitar;
		private System.Windows.Forms.Label lblFirmaOlografaPath;
		private System.Windows.Forms.Panel pnlPdfViewer;
		private System.Windows.Forms.Panel pnlErrorPDF;
		private System.Windows.Forms.LinkLabel lnkDescargarAcrobatReader;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label lblPDF;
		private System.Windows.Forms.Button btnFirmaOlografaLimpiarCache;
	}
}

