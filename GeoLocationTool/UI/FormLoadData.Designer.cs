namespace GeoLocationTool.UI
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
            this.btnMatchData = new System.Windows.Forms.Button();
            this.btnLoadInputFile = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSaveCsv = new System.Windows.Forms.Button();
            this.udProvince = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMunicipality = new System.Windows.Forms.Label();
            this.udMunicipality = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.udBarangay = new System.Windows.Forms.NumericUpDown();
            this.btnManualMatch = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.tlpSaveOptions = new System.Windows.Forms.TableLayoutPanel();
            this.rdoSaveAsCsv = new System.Windows.Forms.RadioButton();
            this.rdoSaveAsExcel = new System.Windows.Forms.RadioButton();
            this.rdoSaveAsTabDelim = new System.Windows.Forms.RadioButton();
            this.rdoImportCsv = new System.Windows.Forms.RadioButton();
            this.rdoImportTabDelim = new System.Windows.Forms.RadioButton();
            this.rdoImportExcel = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
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
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView1, 8);
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(3, 244);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1239, 476);
            this.dataGridView1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(703, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Worksheet name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtWorksheetName
            // 
            this.txtWorksheetName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWorksheetName.Enabled = false;
            this.txtWorksheetName.Location = new System.Drawing.Point(828, 28);
            this.txtWorksheetName.Name = "txtWorksheetName";
            this.txtWorksheetName.Size = new System.Drawing.Size(159, 22);
            this.txtWorksheetName.TabIndex = 4;
            // 
            // btnMatchData
            // 
            this.btnMatchData.Location = new System.Drawing.Point(3, 156);
            this.btnMatchData.Name = "btnMatchData";
            this.btnMatchData.Size = new System.Drawing.Size(147, 33);
            this.btnMatchData.TabIndex = 8;
            this.btnMatchData.Text = "Match";
            this.btnMatchData.UseVisualStyleBackColor = true;
            this.btnMatchData.Click += new System.EventHandler(this.btnMatchData_Click);
            // 
            // btnLoadInputFile
            // 
            this.btnLoadInputFile.Location = new System.Drawing.Point(3, 23);
            this.btnLoadInputFile.Name = "btnLoadInputFile";
            this.btnLoadInputFile.Size = new System.Drawing.Size(147, 33);
            this.btnLoadInputFile.TabIndex = 1;
            this.btnLoadInputFile.Text = "Load Input Data";
            this.btnLoadInputFile.UseVisualStyleBackColor = true;
            this.btnLoadInputFile.Click += new System.EventHandler(this.btnLoadInputFile_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txtFileName, 4);
            this.txtFileName.Location = new System.Drawing.Point(487, 62);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(755, 22);
            this.txtFileName.TabIndex = 8;
            this.txtFileName.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(334, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Input File:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSaveCsv
            // 
            this.btnSaveCsv.Location = new System.Drawing.Point(3, 205);
            this.btnSaveCsv.Name = "btnSaveCsv";
            this.btnSaveCsv.Size = new System.Drawing.Size(147, 33);
            this.btnSaveCsv.TabIndex = 10;
            this.btnSaveCsv.Text = "Save ";
            this.btnSaveCsv.UseVisualStyleBackColor = true;
            this.btnSaveCsv.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // udProvince
            // 
            this.udProvince.Location = new System.Drawing.Point(181, 117);
            this.udProvince.Name = "udProvince";
            this.udProvince.Size = new System.Drawing.Size(120, 22);
            this.udProvince.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(181, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Province";
            // 
            // lblMunicipality
            // 
            this.lblMunicipality.AutoSize = true;
            this.lblMunicipality.Location = new System.Drawing.Point(334, 97);
            this.lblMunicipality.Name = "lblMunicipality";
            this.lblMunicipality.Size = new System.Drawing.Size(42, 17);
            this.lblMunicipality.TabIndex = 16;
            this.lblMunicipality.Text = "Town";
            // 
            // udMunicipality
            // 
            this.udMunicipality.Location = new System.Drawing.Point(334, 117);
            this.udMunicipality.Name = "udMunicipality";
            this.udMunicipality.Size = new System.Drawing.Size(120, 22);
            this.udMunicipality.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(487, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 17);
            this.label6.TabIndex = 18;
            this.label6.Text = "Village";
            // 
            // udBarangay
            // 
            this.udBarangay.Location = new System.Drawing.Point(487, 117);
            this.udBarangay.Name = "udBarangay";
            this.udBarangay.Size = new System.Drawing.Size(120, 22);
            this.udBarangay.TabIndex = 7;
            // 
            // btnManualMatch
            // 
            this.btnManualMatch.Location = new System.Drawing.Point(181, 156);
            this.btnManualMatch.Name = "btnManualMatch";
            this.btnManualMatch.Size = new System.Drawing.Size(147, 33);
            this.btnManualMatch.TabIndex = 9;
            this.btnManualMatch.Text = "Manual Match";
            this.btnManualMatch.UseVisualStyleBackColor = true;
            this.btnManualMatch.Click += new System.EventHandler(this.btnManualMatch_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.90909F));
            this.tableLayoutPanel1.Controls.Add(this.btnLoadInputFile, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.udProvince, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.udMunicipality, 3, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblMunicipality, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.label6, 4, 6);
            this.tableLayoutPanel1.Controls.Add(this.udBarangay, 4, 7);
            this.tableLayoutPanel1.Controls.Add(this.btnMatchData, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnSaveCsv, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.tlpSaveOptions, 2, 10);
            this.tableLayoutPanel1.Controls.Add(this.rdoImportCsv, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.rdoImportTabDelim, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.rdoImportExcel, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtWorksheetName, 6, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtFileName, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.button1, 3, 8);
            this.tableLayoutPanel1.Controls.Add(this.button2, 4, 8);
            this.tableLayoutPanel1.Controls.Add(this.btnManualMatch, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this.btnBack, 7, 10);
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
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 97);
            this.label5.Name = "label5";
            this.tableLayoutPanel1.SetRowSpan(this.label5, 2);
            this.label5.Size = new System.Drawing.Size(147, 56);
            this.label5.TabIndex = 15;
            this.label5.Text = "Select Column Numbers:";
            // 
            // tlpSaveOptions
            // 
            this.tlpSaveOptions.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tlpSaveOptions, 3);
            this.tlpSaveOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 147F));
            this.tlpSaveOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 147F));
            this.tlpSaveOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSaveOptions.Controls.Add(this.rdoSaveAsCsv, 0, 0);
            this.tlpSaveOptions.Controls.Add(this.rdoSaveAsExcel, 2, 0);
            this.tlpSaveOptions.Controls.Add(this.rdoSaveAsTabDelim, 1, 0);
            this.tlpSaveOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSaveOptions.Location = new System.Drawing.Point(178, 202);
            this.tlpSaveOptions.Margin = new System.Windows.Forms.Padding(0);
            this.tlpSaveOptions.Name = "tlpSaveOptions";
            this.tlpSaveOptions.RowCount = 1;
            this.tlpSaveOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSaveOptions.Size = new System.Drawing.Size(522, 39);
            this.tlpSaveOptions.TabIndex = 22;
            // 
            // rdoSaveAsCsv
            // 
            this.rdoSaveAsCsv.AutoSize = true;
            this.rdoSaveAsCsv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdoSaveAsCsv.Location = new System.Drawing.Point(3, 3);
            this.rdoSaveAsCsv.Name = "rdoSaveAsCsv";
            this.rdoSaveAsCsv.Size = new System.Drawing.Size(141, 33);
            this.rdoSaveAsCsv.TabIndex = 20;
            this.rdoSaveAsCsv.TabStop = true;
            this.rdoSaveAsCsv.Text = "Csv";
            this.rdoSaveAsCsv.UseVisualStyleBackColor = true;
            // 
            // rdoSaveAsExcel
            // 
            this.rdoSaveAsExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoSaveAsExcel.AutoSize = true;
            this.rdoSaveAsExcel.Enabled = false;
            this.rdoSaveAsExcel.Location = new System.Drawing.Point(297, 9);
            this.rdoSaveAsExcel.Name = "rdoSaveAsExcel";
            this.rdoSaveAsExcel.Size = new System.Drawing.Size(222, 21);
            this.rdoSaveAsExcel.TabIndex = 21;
            this.rdoSaveAsExcel.TabStop = true;
            this.rdoSaveAsExcel.Text = "Excel";
            this.rdoSaveAsExcel.UseVisualStyleBackColor = true;
            // 
            // rdoSaveAsTabDelim
            // 
            this.rdoSaveAsTabDelim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoSaveAsTabDelim.AutoSize = true;
            this.rdoSaveAsTabDelim.Enabled = false;
            this.rdoSaveAsTabDelim.Location = new System.Drawing.Point(150, 9);
            this.rdoSaveAsTabDelim.Name = "rdoSaveAsTabDelim";
            this.rdoSaveAsTabDelim.Size = new System.Drawing.Size(141, 21);
            this.rdoSaveAsTabDelim.TabIndex = 22;
            this.rdoSaveAsTabDelim.TabStop = true;
            this.rdoSaveAsTabDelim.Text = "Tab Delimited";
            this.rdoSaveAsTabDelim.UseVisualStyleBackColor = true;
            // 
            // rdoImportCsv
            // 
            this.rdoImportCsv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoImportCsv.AutoSize = true;
            this.rdoImportCsv.Location = new System.Drawing.Point(181, 29);
            this.rdoImportCsv.Name = "rdoImportCsv";
            this.rdoImportCsv.Size = new System.Drawing.Size(147, 21);
            this.rdoImportCsv.TabIndex = 2;
            this.rdoImportCsv.TabStop = true;
            this.rdoImportCsv.Text = "Csv";
            this.rdoImportCsv.UseVisualStyleBackColor = true;
            // 
            // rdoImportTabDelim
            // 
            this.rdoImportTabDelim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoImportTabDelim.AutoSize = true;
            this.rdoImportTabDelim.Location = new System.Drawing.Point(334, 29);
            this.rdoImportTabDelim.Name = "rdoImportTabDelim";
            this.rdoImportTabDelim.Size = new System.Drawing.Size(147, 21);
            this.rdoImportTabDelim.TabIndex = 23;
            this.rdoImportTabDelim.TabStop = true;
            this.rdoImportTabDelim.Text = "Tab Delimited";
            this.rdoImportTabDelim.UseVisualStyleBackColor = true;
            // 
            // rdoImportExcel
            // 
            this.rdoImportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoImportExcel.AutoSize = true;
            this.rdoImportExcel.Enabled = false;
            this.rdoImportExcel.Location = new System.Drawing.Point(487, 29);
            this.rdoImportExcel.Name = "rdoImportExcel";
            this.rdoImportExcel.Size = new System.Drawing.Size(210, 21);
            this.rdoImportExcel.TabIndex = 3;
            this.rdoImportExcel.TabStop = true;
            this.rdoImportExcel.Text = "Excel";
            this.rdoImportExcel.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(334, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 23);
            this.button1.TabIndex = 24;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(487, 156);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 23);
            this.button2.TabIndex = 25;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Location = new System.Drawing.Point(1095, 205);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(147, 33);
            this.btnBack.TabIndex = 26;
            this.btnBack.Text = "Geographical Data";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // FormLoadData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 723);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormLoadData";
            this.Text = "Multi Level Geo Coder Tool - Input Data";
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
        private System.Windows.Forms.Button btnMatchData;
        private System.Windows.Forms.Button btnLoadInputFile;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSaveCsv;
        private System.Windows.Forms.NumericUpDown udProvince;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMunicipality;
        private System.Windows.Forms.NumericUpDown udMunicipality;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown udBarangay;
        private System.Windows.Forms.Button btnManualMatch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RadioButton rdoImportCsv;
        private System.Windows.Forms.RadioButton rdoImportExcel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rdoSaveAsCsv;
        private System.Windows.Forms.RadioButton rdoSaveAsExcel;
        private System.Windows.Forms.TableLayoutPanel tlpSaveOptions;
        private System.Windows.Forms.RadioButton rdoSaveAsTabDelim;
        private System.Windows.Forms.RadioButton rdoImportTabDelim;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnBack;
    }
}

