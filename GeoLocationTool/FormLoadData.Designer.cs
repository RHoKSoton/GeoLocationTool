namespace GeoLocationTool
{
    partial class FormLoadData
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWorksheetName = new System.Windows.Forms.TextBox();
            this.btnGetCodeData = new System.Windows.Forms.Button();
            this.btnMatchData = new System.Windows.Forms.Button();
            this.btnReadCsv = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSaveCsv = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLocationFileName = new System.Windows.Forms.TextBox();
            this.udProvince = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMunicipality = new System.Windows.Forms.Label();
            this.udMunicipality = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.udBarangay = new System.Windows.Forms.NumericUpDown();
            this.btnFuzzyMatch = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rdoImportCsv = new System.Windows.Forms.RadioButton();
            this.rdoImportExcel = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.tlpSaveOptions = new System.Windows.Forms.TableLayoutPanel();
            this.rdoSaveAsCsv = new System.Windows.Forms.RadioButton();
            this.rdoSaveAsExcel = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udProvince)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udMunicipality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udBarangay)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpSaveOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView1, 7);
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(3, 283);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1239, 437);
            this.dataGridView1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(480, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Worksheet name:";
            // 
            // txtWorksheetName
            // 
            this.txtWorksheetName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtWorksheetName.Enabled = false;
            this.txtWorksheetName.Location = new System.Drawing.Point(606, 101);
            this.txtWorksheetName.Name = "txtWorksheetName";
            this.txtWorksheetName.Size = new System.Drawing.Size(159, 22);
            this.txtWorksheetName.TabIndex = 4;
            // 
            // btnGetCodeData
            // 
            this.btnGetCodeData.Location = new System.Drawing.Point(3, 13);
            this.btnGetCodeData.Name = "btnGetCodeData";
            this.btnGetCodeData.Size = new System.Drawing.Size(147, 33);
            this.btnGetCodeData.TabIndex = 0;
            this.btnGetCodeData.Text = "Read Location File";
            this.btnGetCodeData.UseVisualStyleBackColor = true;
            this.btnGetCodeData.Click += new System.EventHandler(this.btnReadLocation_Click);
            // 
            // btnMatchData
            // 
            this.btnMatchData.Location = new System.Drawing.Point(3, 195);
            this.btnMatchData.Name = "btnMatchData";
            this.btnMatchData.Size = new System.Drawing.Size(147, 33);
            this.btnMatchData.TabIndex = 8;
            this.btnMatchData.Text = "Match";
            this.btnMatchData.UseVisualStyleBackColor = true;
            this.btnMatchData.Click += new System.EventHandler(this.btnMatchData_Click);
            // 
            // btnReadCsv
            // 
            this.btnReadCsv.Location = new System.Drawing.Point(3, 62);
            this.btnReadCsv.Name = "btnReadCsv";
            this.btnReadCsv.Size = new System.Drawing.Size(147, 33);
            this.btnReadCsv.TabIndex = 1;
            this.btnReadCsv.Text = "Read Input File";
            this.btnReadCsv.UseVisualStyleBackColor = true;
            this.btnReadCsv.Click += new System.EventHandler(this.btnReadCsv_Click);
            // 
            // txtFileName
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtFileName, 4);
            this.txtFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFileName.Location = new System.Drawing.Point(480, 62);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(762, 22);
            this.txtFileName.TabIndex = 8;
            this.txtFileName.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(342, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Input File:";
            // 
            // btnSaveCsv
            // 
            this.btnSaveCsv.Location = new System.Drawing.Point(3, 244);
            this.btnSaveCsv.Name = "btnSaveCsv";
            this.btnSaveCsv.Size = new System.Drawing.Size(147, 33);
            this.btnSaveCsv.TabIndex = 10;
            this.btnSaveCsv.Text = "Save ";
            this.btnSaveCsv.UseVisualStyleBackColor = true;
            this.btnSaveCsv.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(342, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Location Data File:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLocationFileName
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtLocationFileName, 4);
            this.txtLocationFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLocationFileName.Location = new System.Drawing.Point(480, 13);
            this.txtLocationFileName.Name = "txtLocationFileName";
            this.txtLocationFileName.ReadOnly = true;
            this.txtLocationFileName.Size = new System.Drawing.Size(762, 22);
            this.txtLocationFileName.TabIndex = 12;
            this.txtLocationFileName.TabStop = false;
            // 
            // udProvince
            // 
            this.udProvince.Location = new System.Drawing.Point(189, 156);
            this.udProvince.Name = "udProvince";
            this.udProvince.Size = new System.Drawing.Size(120, 22);
            this.udProvince.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(189, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Province Column";
            // 
            // lblMunicipality
            // 
            this.lblMunicipality.AutoSize = true;
            this.lblMunicipality.Location = new System.Drawing.Point(342, 136);
            this.lblMunicipality.Name = "lblMunicipality";
            this.lblMunicipality.Size = new System.Drawing.Size(132, 17);
            this.lblMunicipality.TabIndex = 16;
            this.lblMunicipality.Text = "Municipality Column";
            // 
            // udMunicipality
            // 
            this.udMunicipality.Location = new System.Drawing.Point(342, 156);
            this.udMunicipality.Name = "udMunicipality";
            this.udMunicipality.Size = new System.Drawing.Size(120, 22);
            this.udMunicipality.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(480, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 17);
            this.label6.TabIndex = 18;
            this.label6.Text = "Barangay Column";
            // 
            // udBarangay
            // 
            this.udBarangay.Location = new System.Drawing.Point(480, 156);
            this.udBarangay.Name = "udBarangay";
            this.udBarangay.Size = new System.Drawing.Size(120, 22);
            this.udBarangay.TabIndex = 7;
            // 
            // btnFuzzyMatch
            // 
            this.btnFuzzyMatch.Location = new System.Drawing.Point(189, 195);
            this.btnFuzzyMatch.Name = "btnFuzzyMatch";
            this.btnFuzzyMatch.Size = new System.Drawing.Size(147, 33);
            this.btnFuzzyMatch.TabIndex = 9;
            this.btnFuzzyMatch.Text = "Manual Match";
            this.btnFuzzyMatch.UseVisualStyleBackColor = true;
            this.btnFuzzyMatch.Click += new System.EventHandler(this.btnManualMatch_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnGetCodeData, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnFuzzyMatch, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.btnReadCsv, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.udProvince, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.udMunicipality, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblMunicipality, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.label6, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.udBarangay, 3, 7);
            this.tableLayoutPanel1.Controls.Add(this.btnMatchData, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtFileName, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtLocationFileName, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSaveCsv, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.tlpSaveOptions, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.rdoImportExcel, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.rdoImportCsv, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtWorksheetName, 4, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 12;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1245, 723);
            this.tableLayoutPanel1.TabIndex = 20;
            // 
            // rdoImportCsv
            // 
            this.rdoImportCsv.AutoSize = true;
            this.rdoImportCsv.Location = new System.Drawing.Point(189, 101);
            this.rdoImportCsv.Name = "rdoImportCsv";
            this.rdoImportCsv.Size = new System.Drawing.Size(52, 21);
            this.rdoImportCsv.TabIndex = 2;
            this.rdoImportCsv.TabStop = true;
            this.rdoImportCsv.Text = "Csv";
            this.rdoImportCsv.UseVisualStyleBackColor = true;
            // 
            // rdoImportExcel
            // 
            this.rdoImportExcel.AutoSize = true;
            this.rdoImportExcel.Enabled = false;
            this.rdoImportExcel.Location = new System.Drawing.Point(342, 101);
            this.rdoImportExcel.Name = "rdoImportExcel";
            this.rdoImportExcel.Size = new System.Drawing.Size(62, 21);
            this.rdoImportExcel.TabIndex = 3;
            this.rdoImportExcel.TabStop = true;
            this.rdoImportExcel.Text = "Excel";
            this.rdoImportExcel.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 136);
            this.label5.Name = "label5";
            this.tableLayoutPanel1.SetRowSpan(this.label5, 2);
            this.label5.Size = new System.Drawing.Size(180, 56);
            this.label5.TabIndex = 15;
            this.label5.Text = "Select Location Column Numbers:";
            // 
            // tlpSaveOptions
            // 
            this.tlpSaveOptions.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tlpSaveOptions, 2);
            this.tlpSaveOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.63158F));
            this.tlpSaveOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.36842F));
            this.tlpSaveOptions.Controls.Add(this.rdoSaveAsCsv, 0, 0);
            this.tlpSaveOptions.Controls.Add(this.rdoSaveAsExcel, 1, 0);
            this.tlpSaveOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSaveOptions.Location = new System.Drawing.Point(189, 244);
            this.tlpSaveOptions.Name = "tlpSaveOptions";
            this.tlpSaveOptions.RowCount = 1;
            this.tlpSaveOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSaveOptions.Size = new System.Drawing.Size(285, 33);
            this.tlpSaveOptions.TabIndex = 22;
            // 
            // rdoSaveAsCsv
            // 
            this.rdoSaveAsCsv.AutoSize = true;
            this.rdoSaveAsCsv.Location = new System.Drawing.Point(3, 3);
            this.rdoSaveAsCsv.Name = "rdoSaveAsCsv";
            this.rdoSaveAsCsv.Size = new System.Drawing.Size(52, 21);
            this.rdoSaveAsCsv.TabIndex = 20;
            this.rdoSaveAsCsv.TabStop = true;
            this.rdoSaveAsCsv.Text = "Csv";
            this.rdoSaveAsCsv.UseVisualStyleBackColor = true;
            // 
            // rdoSaveAsExcel
            // 
            this.rdoSaveAsExcel.AutoSize = true;
            this.rdoSaveAsExcel.Enabled = false;
            this.rdoSaveAsExcel.Location = new System.Drawing.Point(153, 3);
            this.rdoSaveAsExcel.Name = "rdoSaveAsExcel";
            this.rdoSaveAsExcel.Size = new System.Drawing.Size(62, 21);
            this.rdoSaveAsExcel.TabIndex = 21;
            this.rdoSaveAsExcel.TabStop = true;
            this.rdoSaveAsExcel.Text = "Excel";
            this.rdoSaveAsExcel.UseVisualStyleBackColor = true;
            // 
            // FormLoadData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 723);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormLoadData";
            this.Text = "Geo Location Tool - Prototype";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormLoadData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udProvince)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udMunicipality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udBarangay)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlpSaveOptions.ResumeLayout(false);
            this.tlpSaveOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWorksheetName;
        private System.Windows.Forms.Button btnGetCodeData;
        private System.Windows.Forms.Button btnMatchData;
        private System.Windows.Forms.Button btnReadCsv;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSaveCsv;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLocationFileName;
        private System.Windows.Forms.NumericUpDown udProvince;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMunicipality;
        private System.Windows.Forms.NumericUpDown udMunicipality;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown udBarangay;
        private System.Windows.Forms.Button btnFuzzyMatch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RadioButton rdoImportCsv;
        private System.Windows.Forms.RadioButton rdoImportExcel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rdoSaveAsCsv;
        private System.Windows.Forms.RadioButton rdoSaveAsExcel;
        private System.Windows.Forms.TableLayoutPanel tlpSaveOptions;
    }
}

