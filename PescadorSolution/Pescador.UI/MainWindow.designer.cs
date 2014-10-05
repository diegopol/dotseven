namespace Pescador.UI
{
    partial class MainWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ConnectionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSubStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.ConnectionSubStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TrucksGridPanel = new System.Windows.Forms.Panel();
            this.TrucksGridView = new System.Windows.Forms.DataGridView();
            this.LicencePlateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DriverNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MobilePhoneColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fieldDestinos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrucksGridTitle = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.TrucksFormPanel = new System.Windows.Forms.Panel();
            this.btnConfig = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnDeleteTruck = new System.Windows.Forms.Button();
            this.btnEditTruck = new System.Windows.Forms.Button();
            this.btnAddTruck = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.TrucksGridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrucksGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.TrucksGridTitle.SuspendLayout();
            this.TrucksFormPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectionStatusLabel,
            this.toolStripSubStatus,
            this.ConnectionSubStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 445);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(863, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ConnectionStatusLabel
            // 
            this.ConnectionStatusLabel.AutoToolTip = true;
            this.ConnectionStatusLabel.DoubleClickEnabled = true;
            this.ConnectionStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ConnectionStatusLabel.Image = global::Pescador.UI.Properties.Resources.status_offline_icon;
            this.ConnectionStatusLabel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ConnectionStatusLabel.IsLink = true;
            this.ConnectionStatusLabel.LinkColor = System.Drawing.Color.Black;
            this.ConnectionStatusLabel.Name = "ConnectionStatusLabel";
            this.ConnectionStatusLabel.Size = new System.Drawing.Size(102, 17);
            this.ConnectionStatusLabel.Text = "Desconectado";
            this.ConnectionStatusLabel.ToolTipText = "Click para ver más detalles";
            this.ConnectionStatusLabel.VisitedLinkColor = System.Drawing.Color.Black;
            this.ConnectionStatusLabel.Click += new System.EventHandler(this.ConnectionStatusLabelClick);
            // 
            // toolStripSubStatus
            // 
            this.toolStripSubStatus.Name = "toolStripSubStatus";
            this.toolStripSubStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // ConnectionSubStatusLabel
            // 
            this.ConnectionSubStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.ConnectionSubStatusLabel.Name = "ConnectionSubStatusLabel";
            this.ConnectionSubStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // TrucksGridPanel
            // 
            this.TrucksGridPanel.Controls.Add(this.TrucksGridView);
            this.TrucksGridPanel.Controls.Add(this.TrucksGridTitle);
            this.TrucksGridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrucksGridPanel.Location = new System.Drawing.Point(0, 33);
            this.TrucksGridPanel.Name = "TrucksGridPanel";
            this.TrucksGridPanel.Size = new System.Drawing.Size(863, 412);
            this.TrucksGridPanel.TabIndex = 3;
            // 
            // TrucksGridView
            // 
            this.TrucksGridView.AllowUserToAddRows = false;
            this.TrucksGridView.AllowUserToDeleteRows = false;
            this.TrucksGridView.AllowUserToResizeRows = false;
            this.TrucksGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TrucksGridView.BackgroundColor = System.Drawing.Color.White;
            this.TrucksGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TrucksGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LicencePlateColumn,
            this.DriverNameColumn,
            this.DocumentNumberColumn,
            this.MobilePhoneColumn,
            this.fieldDestinos,
            this.StatusColumn});
            this.TrucksGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.TrucksGridView.GridColor = System.Drawing.Color.White;
            this.TrucksGridView.Location = new System.Drawing.Point(0, 40);
            this.TrucksGridView.MultiSelect = false;
            this.TrucksGridView.Name = "TrucksGridView";
            this.TrucksGridView.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TrucksGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.TrucksGridView.RowHeadersVisible = false;
            this.TrucksGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TrucksGridView.Size = new System.Drawing.Size(863, 372);
            this.TrucksGridView.TabIndex = 0;
            this.TrucksGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.TrucksGridViewCellMouseDoubleClick);
            this.TrucksGridView.Paint += new System.Windows.Forms.PaintEventHandler(this.TrucksGridViewPaint);
            // 
            // LicencePlateColumn
            // 
            this.LicencePlateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.LicencePlateColumn.ContextMenuStrip = this.contextMenuStrip1;
            this.LicencePlateColumn.FillWeight = 5F;
            this.LicencePlateColumn.HeaderText = "Placa";
            this.LicencePlateColumn.Name = "LicencePlateColumn";
            this.LicencePlateColumn.ReadOnly = true;
            this.LicencePlateColumn.Width = 110;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editarToolStripMenuItem,
            this.eliminarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 48);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Image = global::Pescador.UI.Properties.Resources.edit;
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.editarToolStripMenuItem.Text = "Editar";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.EditarToolStripMenuItemClick);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::Pescador.UI.Properties.Resources.delete;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.EliminarToolStripMenuItemClick);
            // 
            // DriverNameColumn
            // 
            this.DriverNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DriverNameColumn.ContextMenuStrip = this.contextMenuStrip1;
            this.DriverNameColumn.FillWeight = 30F;
            this.DriverNameColumn.HeaderText = "Conductor";
            this.DriverNameColumn.MinimumWidth = 30;
            this.DriverNameColumn.Name = "DriverNameColumn";
            this.DriverNameColumn.ReadOnly = true;
            // 
            // DocumentNumberColumn
            // 
            this.DocumentNumberColumn.ContextMenuStrip = this.contextMenuStrip1;
            this.DocumentNumberColumn.FillWeight = 15F;
            this.DocumentNumberColumn.HeaderText = "Cédula";
            this.DocumentNumberColumn.Name = "DocumentNumberColumn";
            this.DocumentNumberColumn.ReadOnly = true;
            // 
            // MobilePhoneColumn
            // 
            this.MobilePhoneColumn.ContextMenuStrip = this.contextMenuStrip1;
            this.MobilePhoneColumn.FillWeight = 15F;
            this.MobilePhoneColumn.HeaderText = "Celular";
            this.MobilePhoneColumn.Name = "MobilePhoneColumn";
            this.MobilePhoneColumn.ReadOnly = true;
            // 
            // fieldDestinos
            // 
            this.fieldDestinos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fieldDestinos.ContextMenuStrip = this.contextMenuStrip1;
            this.fieldDestinos.HeaderText = "Destinos";
            this.fieldDestinos.Name = "fieldDestinos";
            this.fieldDestinos.ReadOnly = true;
            // 
            // StatusColumn
            // 
            this.StatusColumn.ContextMenuStrip = this.contextMenuStrip1;
            this.StatusColumn.FillWeight = 20F;
            this.StatusColumn.HeaderText = "Estado";
            this.StatusColumn.MinimumWidth = 20;
            this.StatusColumn.Name = "StatusColumn";
            this.StatusColumn.ReadOnly = true;
            this.StatusColumn.Width = 63;
            // 
            // TrucksGridTitle
            // 
            this.TrucksGridTitle.Controls.Add(this.label1);
            this.TrucksGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.TrucksGridTitle.Location = new System.Drawing.Point(0, 0);
            this.TrucksGridTitle.Name = "TrucksGridTitle";
            this.TrucksGridTitle.Size = new System.Drawing.Size(863, 40);
            this.TrucksGridTitle.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(863, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Listado de Camiones";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrucksFormPanel
            // 
            this.TrucksFormPanel.Controls.Add(this.btnConfig);
            this.TrucksFormPanel.Controls.Add(this.btnAbout);
            this.TrucksFormPanel.Controls.Add(this.btnDeleteTruck);
            this.TrucksFormPanel.Controls.Add(this.btnEditTruck);
            this.TrucksFormPanel.Controls.Add(this.btnAddTruck);
            this.TrucksFormPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TrucksFormPanel.Location = new System.Drawing.Point(0, 0);
            this.TrucksFormPanel.Name = "TrucksFormPanel";
            this.TrucksFormPanel.Size = new System.Drawing.Size(863, 33);
            this.TrucksFormPanel.TabIndex = 9;
            // 
            // btnConfig
            // 
            this.btnConfig.AllowDrop = true;
            this.btnConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfig.FlatAppearance.BorderSize = 0;
            this.btnConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfig.Image = global::Pescador.UI.Properties.Resources.config;
            this.btnConfig.Location = new System.Drawing.Point(813, 4);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(25, 23);
            this.btnConfig.TabIndex = 4;
            this.btnConfig.UseVisualStyleBackColor = false;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.AllowDrop = true;
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbout.FlatAppearance.BorderSize = 0;
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.Image = global::Pescador.UI.Properties.Resources.about;
            this.btnAbout.Location = new System.Drawing.Point(835, 4);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(25, 23);
            this.btnAbout.TabIndex = 3;
            this.btnAbout.UseVisualStyleBackColor = false;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnDeleteTruck
            // 
            this.btnDeleteTruck.Image = global::Pescador.UI.Properties.Resources.delete;
            this.btnDeleteTruck.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnDeleteTruck.Location = new System.Drawing.Point(224, 4);
            this.btnDeleteTruck.Name = "btnDeleteTruck";
            this.btnDeleteTruck.Size = new System.Drawing.Size(103, 23);
            this.btnDeleteTruck.TabIndex = 2;
            this.btnDeleteTruck.Text = "Eliminar Camión";
            this.btnDeleteTruck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteTruck.UseVisualStyleBackColor = true;
            this.btnDeleteTruck.Click += new System.EventHandler(this.BtnDeleteTruckClick);
            // 
            // btnEditTruck
            // 
            this.btnEditTruck.Image = global::Pescador.UI.Properties.Resources.edit;
            this.btnEditTruck.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnEditTruck.Location = new System.Drawing.Point(120, 4);
            this.btnEditTruck.Name = "btnEditTruck";
            this.btnEditTruck.Size = new System.Drawing.Size(97, 23);
            this.btnEditTruck.TabIndex = 1;
            this.btnEditTruck.Text = "Editar Camion";
            this.btnEditTruck.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnEditTruck.UseVisualStyleBackColor = true;
            this.btnEditTruck.Click += new System.EventHandler(this.BtnEditTruckClick);
            // 
            // btnAddTruck
            // 
            this.btnAddTruck.Image = ((System.Drawing.Image)(resources.GetObject("btnAddTruck.Image")));
            this.btnAddTruck.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnAddTruck.Location = new System.Drawing.Point(4, 4);
            this.btnAddTruck.Name = "btnAddTruck";
            this.btnAddTruck.Size = new System.Drawing.Size(110, 23);
            this.btnAddTruck.TabIndex = 0;
            this.btnAddTruck.Text = "Agregar Camión";
            this.btnAddTruck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddTruck.UseVisualStyleBackColor = true;
            this.btnAddTruck.Click += new System.EventHandler(this.BtnAddTruckClick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 467);
            this.Controls.Add(this.TrucksGridPanel);
            this.Controls.Add(this.TrucksFormPanel);
            this.Controls.Add(this.statusStrip1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pescador";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.TrucksGridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TrucksGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.TrucksGridTitle.ResumeLayout(false);
            this.TrucksFormPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel TrucksGridPanel;
        private System.Windows.Forms.DataGridView TrucksGridView;
        private System.Windows.Forms.Panel TrucksFormPanel;
        private System.Windows.Forms.Panel TrucksGridTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddTruck;
        private System.Windows.Forms.Button btnEditTruck;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.Button btnDeleteTruck;
        private System.Windows.Forms.DataGridViewTextBoxColumn LicencePlateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DriverNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MobilePhoneColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldDestinos;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusColumn;
        private System.Windows.Forms.ToolStripStatusLabel toolStripSubStatus;
        private System.Windows.Forms.ToolStripStatusLabel ConnectionSubStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel ConnectionStatusLabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnConfig;
    }
}

