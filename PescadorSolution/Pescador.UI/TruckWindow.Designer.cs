namespace Pescador.UI
{
    partial class TruckWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TruckWindow));
            this.TrucksFormTablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddDestinations = new System.Windows.Forms.Button();
            this.DriverNameLabel = new System.Windows.Forms.Label();
            this.chkLstDestinations = new System.Windows.Forms.CheckedListBox();
            this.LicensePlateLabel = new System.Windows.Forms.Label();
            this.LicencePlateTextBox = new System.Windows.Forms.TextBox();
            this.DriverNameTextBox = new System.Windows.Forms.TextBox();
            this.DriverDocumentNumberLabel = new System.Windows.Forms.Label();
            this.DriverMobileNumberLabel = new System.Windows.Forms.Label();
            this.DriverDocumentNumberTextBox = new System.Windows.Forms.TextBox();
            this.DriverMobileNumberTextBox = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblErrors = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TrucksFormTablePanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TrucksFormTablePanel
            // 
            this.TrucksFormTablePanel.ColumnCount = 2;
            this.TrucksFormTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.TrucksFormTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.2439F));
            this.TrucksFormTablePanel.Controls.Add(this.label1, 0, 4);
            this.TrucksFormTablePanel.Controls.Add(this.btnAddDestinations, 1, 5);
            this.TrucksFormTablePanel.Controls.Add(this.DriverNameLabel, 0, 1);
            this.TrucksFormTablePanel.Controls.Add(this.chkLstDestinations, 1, 4);
            this.TrucksFormTablePanel.Controls.Add(this.LicensePlateLabel, 0, 0);
            this.TrucksFormTablePanel.Controls.Add(this.LicencePlateTextBox, 1, 0);
            this.TrucksFormTablePanel.Controls.Add(this.DriverNameTextBox, 1, 1);
            this.TrucksFormTablePanel.Controls.Add(this.DriverDocumentNumberLabel, 0, 2);
            this.TrucksFormTablePanel.Controls.Add(this.DriverMobileNumberLabel, 0, 3);
            this.TrucksFormTablePanel.Controls.Add(this.DriverDocumentNumberTextBox, 1, 2);
            this.TrucksFormTablePanel.Controls.Add(this.DriverMobileNumberTextBox, 1, 3);
            this.TrucksFormTablePanel.Location = new System.Drawing.Point(6, 14);
            this.TrucksFormTablePanel.Name = "TrucksFormTablePanel";
            this.TrucksFormTablePanel.RowCount = 5;
            this.TrucksFormTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.TrucksFormTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.TrucksFormTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.TrucksFormTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.TrucksFormTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.TrucksFormTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.TrucksFormTablePanel.Size = new System.Drawing.Size(317, 292);
            this.TrucksFormTablePanel.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Destinos";
            // 
            // btnAddDestinations
            // 
            this.btnAddDestinations.Location = new System.Drawing.Point(129, 265);
            this.btnAddDestinations.Name = "btnAddDestinations";
            this.btnAddDestinations.Size = new System.Drawing.Size(185, 23);
            this.btnAddDestinations.TabIndex = 6;
            this.btnAddDestinations.Text = "Agregar más destinos...";
            this.btnAddDestinations.UseVisualStyleBackColor = true;
            this.btnAddDestinations.Click += new System.EventHandler(this.btnAddDestinations_Click);
            // 
            // DriverNameLabel
            // 
            this.DriverNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.DriverNameLabel.AutoSize = true;
            this.DriverNameLabel.Location = new System.Drawing.Point(27, 35);
            this.DriverNameLabel.Name = "DriverNameLabel";
            this.DriverNameLabel.Size = new System.Drawing.Size(96, 13);
            this.DriverNameLabel.TabIndex = 2;
            this.DriverNameLabel.Text = "Nombre Conductor";
            // 
            // chkLstDestinations
            // 
            this.chkLstDestinations.CheckOnClick = true;
            this.chkLstDestinations.FormattingEnabled = true;
            this.chkLstDestinations.Location = new System.Drawing.Point(129, 115);
            this.chkLstDestinations.Name = "chkLstDestinations";
            this.chkLstDestinations.Size = new System.Drawing.Size(185, 139);
            this.chkLstDestinations.TabIndex = 5;
            // 
            // LicensePlateLabel
            // 
            this.LicensePlateLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LicensePlateLabel.AutoSize = true;
            this.LicensePlateLabel.Location = new System.Drawing.Point(89, 7);
            this.LicensePlateLabel.Name = "LicensePlateLabel";
            this.LicensePlateLabel.Size = new System.Drawing.Size(34, 13);
            this.LicensePlateLabel.TabIndex = 0;
            this.LicensePlateLabel.Text = "Placa";
            // 
            // LicencePlateTextBox
            // 
            this.LicencePlateTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LicencePlateTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.LicencePlateTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.LicencePlateTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.LicencePlateTextBox.Location = new System.Drawing.Point(129, 4);
            this.LicencePlateTextBox.MaxLength = 20;
            this.LicencePlateTextBox.Name = "LicencePlateTextBox";
            this.LicencePlateTextBox.Size = new System.Drawing.Size(93, 20);
            this.LicencePlateTextBox.TabIndex = 1;
            // 
            // DriverNameTextBox
            // 
            this.DriverNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DriverNameTextBox.Location = new System.Drawing.Point(129, 32);
            this.DriverNameTextBox.Name = "DriverNameTextBox";
            this.DriverNameTextBox.Size = new System.Drawing.Size(185, 20);
            this.DriverNameTextBox.TabIndex = 2;
            // 
            // DriverDocumentNumberLabel
            // 
            this.DriverDocumentNumberLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.DriverDocumentNumberLabel.AutoSize = true;
            this.DriverDocumentNumberLabel.Location = new System.Drawing.Point(83, 63);
            this.DriverDocumentNumberLabel.Name = "DriverDocumentNumberLabel";
            this.DriverDocumentNumberLabel.Size = new System.Drawing.Size(40, 13);
            this.DriverDocumentNumberLabel.TabIndex = 6;
            this.DriverDocumentNumberLabel.Text = "Cédula";
            // 
            // DriverMobileNumberLabel
            // 
            this.DriverMobileNumberLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.DriverMobileNumberLabel.AutoSize = true;
            this.DriverMobileNumberLabel.Location = new System.Drawing.Point(84, 91);
            this.DriverMobileNumberLabel.Name = "DriverMobileNumberLabel";
            this.DriverMobileNumberLabel.Size = new System.Drawing.Size(39, 13);
            this.DriverMobileNumberLabel.TabIndex = 3;
            this.DriverMobileNumberLabel.Text = "Celular";
            // 
            // DriverDocumentNumberTextBox
            // 
            this.DriverDocumentNumberTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DriverDocumentNumberTextBox.Location = new System.Drawing.Point(129, 60);
            this.DriverDocumentNumberTextBox.Name = "DriverDocumentNumberTextBox";
            this.DriverDocumentNumberTextBox.Size = new System.Drawing.Size(185, 20);
            this.DriverDocumentNumberTextBox.TabIndex = 3;
            // 
            // DriverMobileNumberTextBox
            // 
            this.DriverMobileNumberTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DriverMobileNumberTextBox.Location = new System.Drawing.Point(129, 88);
            this.DriverMobileNumberTextBox.Name = "DriverMobileNumberTextBox";
            this.DriverMobileNumberTextBox.Size = new System.Drawing.Size(185, 20);
            this.DriverMobileNumberTextBox.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(243, 335);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 24);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Guardar Camión";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.SaveTruckButton_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(162, 335);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblErrors
            // 
            this.lblErrors.AutoSize = true;
            this.lblErrors.ForeColor = System.Drawing.Color.Red;
            this.lblErrors.Location = new System.Drawing.Point(12, 366);
            this.lblErrors.Name = "lblErrors";
            this.lblErrors.Size = new System.Drawing.Size(80, 13);
            this.lblErrors.TabIndex = 9;
            this.lblErrors.Text = "Error Messages";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TrucksFormTablePanel);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 317);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalle";
            // 
            // TruckWindow
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(355, 388);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblErrors);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TruckWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Agregar Camión";
            this.Load += new System.EventHandler(this.TruckWindow_Load);
            this.TrucksFormTablePanel.ResumeLayout(false);
            this.TrucksFormTablePanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TrucksFormTablePanel;
        private System.Windows.Forms.Label DriverNameLabel;
        private System.Windows.Forms.Label LicensePlateLabel;
        private System.Windows.Forms.Label DriverDocumentNumberLabel;
        private System.Windows.Forms.TextBox DriverDocumentNumberTextBox;
        private System.Windows.Forms.TextBox DriverMobileNumberTextBox;
        private System.Windows.Forms.Label DriverMobileNumberLabel;
        private System.Windows.Forms.TextBox LicencePlateTextBox;
        private System.Windows.Forms.TextBox DriverNameTextBox;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblErrors;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddDestinations;
        private System.Windows.Forms.CheckedListBox chkLstDestinations;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}