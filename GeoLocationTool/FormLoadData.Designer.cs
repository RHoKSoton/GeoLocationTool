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
            this.btnReadExcelFile = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWorksheetName = new System.Windows.Forms.TextBox();
            this.btnGetCodeData = new System.Windows.Forms.Button();
            this.btnMatchData = new System.Windows.Forms.Button();
            this.btnReadCsv = new System.Windows.Forms.Button();
            this.btnSaveExcel = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udProvince)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udMunicipality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udBarangay)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReadExcelFile
            // 
            this.btnReadExcelFile.Enabled = false;
            this.btnReadExcelFile.Location = new System.Drawing.Point(12, 121);
            this.btnReadExcelFile.Name = "btnReadExcelFile";
            this.btnReadExcelFile.Size = new System.Drawing.Size(147, 33);
            this.btnReadExcelFile.TabIndex = 3;
            this.btnReadExcelFile.Text = "Read Excel File";
            this.btnReadExcelFile.UseVisualStyleBackColor = true;
            this.btnReadExcelFile.Click += new System.EventHandler(this.btnReadExcel_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(3, 333);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(910, 378);
            this.dataGridView1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(172, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Worksheet name:";
            // 
            // txtWorksheetName
            // 
            this.txtWorksheetName.Enabled = false;
            this.txtWorksheetName.Location = new System.Drawing.Point(301, 122);
            this.txtWorksheetName.Name = "txtWorksheetName";
            this.txtWorksheetName.Size = new System.Drawing.Size(227, 22);
            this.txtWorksheetName.TabIndex = 3;
            // 
            // btnGetCodeData
            // 
            this.btnGetCodeData.Location = new System.Drawing.Point(12, 9);
            this.btnGetCodeData.Name = "btnGetCodeData";
            this.btnGetCodeData.Size = new System.Drawing.Size(147, 33);
            this.btnGetCodeData.TabIndex = 0;
            this.btnGetCodeData.Text = "Read Location File";
            this.btnGetCodeData.UseVisualStyleBackColor = true;
            this.btnGetCodeData.Click += new System.EventHandler(this.btnReadLocation_Click);
            // 
            // btnMatchData
            // 
            this.btnMatchData.Location = new System.Drawing.Point(12, 215);
            this.btnMatchData.Name = "btnMatchData";
            this.btnMatchData.Size = new System.Drawing.Size(147, 33);
            this.btnMatchData.TabIndex = 7;
            this.btnMatchData.Text = "Auto Match";
            this.btnMatchData.UseVisualStyleBackColor = true;
            this.btnMatchData.Click += new System.EventHandler(this.btnMatchData_Click);
            // 
            // btnReadCsv
            // 
            this.btnReadCsv.Location = new System.Drawing.Point(12, 64);
            this.btnReadCsv.Name = "btnReadCsv";
            this.btnReadCsv.Size = new System.Drawing.Size(147, 33);
            this.btnReadCsv.TabIndex = 1;
            this.btnReadCsv.Text = "Read CSV file";
            this.btnReadCsv.UseVisualStyleBackColor = true;
            this.btnReadCsv.Click += new System.EventHandler(this.btnReadCsv_Click);
            // 
            // btnSaveExcel
            // 
            this.btnSaveExcel.Enabled = false;
            this.btnSaveExcel.Location = new System.Drawing.Point(210, 280);
            this.btnSaveExcel.Name = "btnSaveExcel";
            this.btnSaveExcel.Size = new System.Drawing.Size(157, 33);
            this.btnSaveExcel.TabIndex = 9;
            this.btnSaveExcel.Text = "Save as Excel";
            this.btnSaveExcel.UseVisualStyleBackColor = true;
            this.btnSaveExcel.Click += new System.EventHandler(this.btnSaveExcel_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(302, 64);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(598, 22);
            this.txtFileName.TabIndex = 8;
            this.txtFileName.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(172, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Input File:";
            // 
            // btnSaveCsv
            // 
            this.btnSaveCsv.Location = new System.Drawing.Point(12, 280);
            this.btnSaveCsv.Name = "btnSaveCsv";
            this.btnSaveCsv.Size = new System.Drawing.Size(147, 33);
            this.btnSaveCsv.TabIndex = 8;
            this.btnSaveCsv.Text = "Save as csv";
            this.btnSaveCsv.UseVisualStyleBackColor = true;
            this.btnSaveCsv.Click += new System.EventHandler(this.btnSaveCsv_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(172, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Location Data File:";
            // 
            // txtLocationFileName
            // 
            this.txtLocationFileName.Location = new System.Drawing.Point(301, 14);
            this.txtLocationFileName.Name = "txtLocationFileName";
            this.txtLocationFileName.ReadOnly = true;
            this.txtLocationFileName.Size = new System.Drawing.Size(598, 22);
            this.txtLocationFileName.TabIndex = 12;
            this.txtLocationFileName.TabStop = false;
            // 
            // udProvince
            // 
            this.udProvince.Location = new System.Drawing.Point(193, 177);
            this.udProvince.Name = "udProvince";
            this.udProvince.Size = new System.Drawing.Size(120, 22);
            this.udProvince.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Province column number:";
            // 
            // lblMunicipality
            // 
            this.lblMunicipality.AutoSize = true;
            this.lblMunicipality.Location = new System.Drawing.Point(344, 180);
            this.lblMunicipality.Name = "lblMunicipality";
            this.lblMunicipality.Size = new System.Drawing.Size(136, 17);
            this.lblMunicipality.TabIndex = 16;
            this.lblMunicipality.Text = "Municipality Column:";
            // 
            // udMunicipality
            // 
            this.udMunicipality.Location = new System.Drawing.Point(493, 177);
            this.udMunicipality.Name = "udMunicipality";
            this.udMunicipality.Size = new System.Drawing.Size(120, 22);
            this.udMunicipality.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(632, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 17);
            this.label6.TabIndex = 18;
            this.label6.Text = "Barangay column:";
            // 
            // udBarangay
            // 
            this.udBarangay.Location = new System.Drawing.Point(769, 177);
            this.udBarangay.Name = "udBarangay";
            this.udBarangay.Size = new System.Drawing.Size(120, 22);
            this.udBarangay.TabIndex = 6;
            // 
            // btnFuzzyMatch
            // 
            this.btnFuzzyMatch.Location = new System.Drawing.Point(210, 215);
            this.btnFuzzyMatch.Name = "btnFuzzyMatch";
            this.btnFuzzyMatch.Size = new System.Drawing.Size(157, 33);
            this.btnFuzzyMatch.TabIndex = 19;
            this.btnFuzzyMatch.Text = "Manual Match";
            this.btnFuzzyMatch.UseVisualStyleBackColor = true;
            this.btnFuzzyMatch.Click += new System.EventHandler(this.btnFuzzyMatch_Click);
            // 
            // FormLoadData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 723);
            this.Controls.Add(this.btnFuzzyMatch);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.udBarangay);
            this.Controls.Add(this.lblMunicipality);
            this.Controls.Add(this.udMunicipality);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.udProvince);
            this.Controls.Add(this.txtLocationFileName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSaveCsv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnSaveExcel);
            this.Controls.Add(this.btnReadCsv);
            this.Controls.Add(this.btnMatchData);
            this.Controls.Add(this.btnGetCodeData);
            this.Controls.Add(this.txtWorksheetName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnReadExcelFile);
            this.Name = "FormLoadData";
            this.Text = "Geo Location Tool - Prototype";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormLoadData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udProvince)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udMunicipality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udBarangay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReadExcelFile;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWorksheetName;
        private System.Windows.Forms.Button btnGetCodeData;
        private System.Windows.Forms.Button btnMatchData;
        private System.Windows.Forms.Button btnReadCsv;
        private System.Windows.Forms.Button btnSaveExcel;
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
    }
}

