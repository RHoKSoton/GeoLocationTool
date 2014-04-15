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
            this.btnMatchData = new System.Windows.Forms.Button();
            this.btnLoadInputFile = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMunicipality = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnManualMatch = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rdoImportCsv = new System.Windows.Forms.RadioButton();
            this.rdoImportTabDelim = new System.Windows.Forms.RadioButton();
            this.btnSelectOutputFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOutputFileName = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cboLevel3 = new System.Windows.Forms.ComboBox();
            this.cboLevel2 = new System.Windows.Forms.ComboBox();
            this.cboLevel1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView1, 8);
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(4, 277);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1236, 443);
            this.dataGridView1.TabIndex = 27;
            // 
            // btnMatchData
            // 
            this.btnMatchData.Location = new System.Drawing.Point(4, 175);
            this.btnMatchData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMatchData.Name = "btnMatchData";
            this.btnMatchData.Size = new System.Drawing.Size(148, 33);
            this.btnMatchData.TabIndex = 9;
            this.btnMatchData.Text = "Match All";
            this.btnMatchData.UseVisualStyleBackColor = true;
            this.btnMatchData.Click += new System.EventHandler(this.btnMatchData_Click);
            // 
            // btnLoadInputFile
            // 
            this.btnLoadInputFile.Location = new System.Drawing.Point(4, 14);
            this.btnLoadInputFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLoadInputFile.Name = "btnLoadInputFile";
            this.btnLoadInputFile.Size = new System.Drawing.Size(148, 33);
            this.btnLoadInputFile.TabIndex = 1;
            this.btnLoadInputFile.Text = "Load Input Data";
            this.btnLoadInputFile.UseVisualStyleBackColor = true;
            this.btnLoadInputFile.Click += new System.EventHandler(this.btnLoadInputFile_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txtFileName, 2);
            this.txtFileName.Location = new System.Drawing.Point(714, 19);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(370, 22);
            this.txtFileName.TabIndex = 8;
            this.txtFileName.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(637, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Input File:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(180, 61);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Level 1";
            // 
            // lblMunicipality
            // 
            this.lblMunicipality.AutoSize = true;
            this.lblMunicipality.Location = new System.Drawing.Point(358, 61);
            this.lblMunicipality.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMunicipality.Name = "lblMunicipality";
            this.lblMunicipality.Size = new System.Drawing.Size(118, 17);
            this.lblMunicipality.TabIndex = 16;
            this.lblMunicipality.Text = "Level 2 (optional)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(536, 61);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 17);
            this.label6.TabIndex = 18;
            this.label6.Text = "Level 3 (optional)";
            // 
            // btnManualMatch
            // 
            this.btnManualMatch.Location = new System.Drawing.Point(4, 226);
            this.btnManualMatch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnManualMatch.Name = "btnManualMatch";
            this.btnManualMatch.Size = new System.Drawing.Size(148, 33);
            this.btnManualMatch.TabIndex = 10;
            this.btnManualMatch.Text = "Manual Match";
            this.btnManualMatch.UseVisualStyleBackColor = true;
            this.btnManualMatch.Click += new System.EventHandler(this.btnManualMatch_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.btnLoadInputFile, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblMunicipality, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnMatchData, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.rdoImportCsv, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.rdoImportTabDelim, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtFileName, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.cboLevel1, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.cboLevel2, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.cboLevel3, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnManualMatch, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.btnSelectOutputFile, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label1, 4, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtOutputFileName, 5, 6);
            this.tableLayoutPanel1.Controls.Add(this.comboBox1, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnBack, 7, 8);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 13;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1244, 724);
            this.tableLayoutPanel1.TabIndex = 20;
            // 
            // rdoImportCsv
            // 
            this.rdoImportCsv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoImportCsv.AutoSize = true;
            this.rdoImportCsv.Location = new System.Drawing.Point(180, 20);
            this.rdoImportCsv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoImportCsv.Name = "rdoImportCsv";
            this.rdoImportCsv.Size = new System.Drawing.Size(170, 21);
            this.rdoImportCsv.TabIndex = 2;
            this.rdoImportCsv.TabStop = true;
            this.rdoImportCsv.Text = "Csv";
            this.rdoImportCsv.UseVisualStyleBackColor = true;
            // 
            // rdoImportTabDelim
            // 
            this.rdoImportTabDelim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoImportTabDelim.AutoSize = true;
            this.rdoImportTabDelim.Location = new System.Drawing.Point(358, 20);
            this.rdoImportTabDelim.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoImportTabDelim.Name = "rdoImportTabDelim";
            this.rdoImportTabDelim.Size = new System.Drawing.Size(170, 21);
            this.rdoImportTabDelim.TabIndex = 3;
            this.rdoImportTabDelim.TabStop = true;
            this.rdoImportTabDelim.Text = "Tab Delimited";
            this.rdoImportTabDelim.UseVisualStyleBackColor = true;
            // 
            // btnSelectOutputFile
            // 
            this.btnSelectOutputFile.Location = new System.Drawing.Point(4, 124);
            this.btnSelectOutputFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelectOutputFile.Name = "btnSelectOutputFile";
            this.btnSelectOutputFile.Size = new System.Drawing.Size(148, 33);
            this.btnSelectOutputFile.TabIndex = 8;
            this.btnSelectOutputFile.Text = "Select Output File";
            this.btnSelectOutputFile.UseVisualStyleBackColor = true;
            this.btnSelectOutputFile.Click += new System.EventHandler(this.btnSelectOutputFile_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(625, 132);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 32;
            this.label1.Text = "Output File:";
            // 
            // txtOutputFileName
            // 
            this.txtOutputFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txtOutputFileName, 2);
            this.txtOutputFileName.Location = new System.Drawing.Point(714, 129);
            this.txtOutputFileName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOutputFileName.Name = "txtOutputFileName";
            this.txtOutputFileName.ReadOnly = true;
            this.txtOutputFileName.Size = new System.Drawing.Size(370, 22);
            this.txtOutputFileName.TabIndex = 31;
            this.txtOutputFileName.TabStop = false;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Location = new System.Drawing.Point(1092, 175);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(148, 33);
            this.btnBack.TabIndex = 12;
            this.btnBack.Text = "Gazetteer Data";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(714, 61);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 17);
            this.label3.TabIndex = 35;
            this.label3.Text = "Level 4 (optional)";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label5, 2);
            this.label5.Location = new System.Drawing.Point(4, 85);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "Select Column Names:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(714, 82);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(170, 24);
            this.comboBox1.TabIndex = 7;
            // 
            // cboLevel3
            // 
            this.cboLevel3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLevel3.FormattingEnabled = true;
            this.cboLevel3.Location = new System.Drawing.Point(536, 82);
            this.cboLevel3.Margin = new System.Windows.Forms.Padding(4);
            this.cboLevel3.Name = "cboLevel3";
            this.cboLevel3.Size = new System.Drawing.Size(170, 24);
            this.cboLevel3.TabIndex = 6;
            // 
            // cboLevel2
            // 
            this.cboLevel2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLevel2.FormattingEnabled = true;
            this.cboLevel2.Location = new System.Drawing.Point(358, 82);
            this.cboLevel2.Margin = new System.Windows.Forms.Padding(4);
            this.cboLevel2.Name = "cboLevel2";
            this.cboLevel2.Size = new System.Drawing.Size(170, 24);
            this.cboLevel2.TabIndex = 5;
            // 
            // cboLevel1
            // 
            this.cboLevel1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLevel1.FormattingEnabled = true;
            this.cboLevel1.Location = new System.Drawing.Point(180, 82);
            this.cboLevel1.Margin = new System.Windows.Forms.Padding(4);
            this.cboLevel1.Name = "cboLevel1";
            this.cboLevel1.Size = new System.Drawing.Size(170, 24);
            this.cboLevel1.TabIndex = 4;
            // 
            // FormLoadData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 724);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormLoadData";
            this.Text = "Multi Level Geo-Coder Tool   Input Data";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLoadData_FormClosing);
            this.Load += new System.EventHandler(this.FormLoadData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnMatchData;
        private System.Windows.Forms.Button btnLoadInputFile;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMunicipality;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnManualMatch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.RadioButton rdoImportCsv;
        private System.Windows.Forms.RadioButton rdoImportTabDelim;
        private System.Windows.Forms.TextBox txtOutputFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectOutputFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboLevel1;
        private System.Windows.Forms.ComboBox cboLevel2;
        private System.Windows.Forms.ComboBox cboLevel3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
    }
}

