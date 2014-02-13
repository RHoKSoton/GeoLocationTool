namespace GeoLocationTool
{
    partial class FormManualMatch
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
            this.txtProvince = new System.Windows.Forms.TextBox();
            this.txtBarangay = new System.Windows.Forms.TextBox();
            this.txtMunicipality = new System.Windows.Forms.TextBox();
            this.cboProvinceSuggestion = new System.Windows.Forms.ComboBox();
            this.cboProvince = new System.Windows.Forms.ComboBox();
            this.cboMunicipalitySuggestion = new System.Windows.Forms.ComboBox();
            this.cboMunicipality = new System.Windows.Forms.ComboBox();
            this.cboBarangaySuggestion = new System.Windows.Forms.ComboBox();
            this.cboBarangay = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnMainScreen = new System.Windows.Forms.Button();
            this.btnUseOriginal = new System.Windows.Forms.Button();
            this.btnUseManual = new System.Windows.Forms.Button();
            this.btnUseSuggestion = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.txtRowCount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSelectedIndex = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 191);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1366, 367);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Manual Replacement";
            // 
            // txtProvince
            // 
            this.txtProvince.Location = new System.Drawing.Point(164, 40);
            this.txtProvince.Name = "txtProvince";
            this.txtProvince.ReadOnly = true;
            this.txtProvince.Size = new System.Drawing.Size(240, 22);
            this.txtProvince.TabIndex = 2;
            // 
            // txtBarangay
            // 
            this.txtBarangay.Location = new System.Drawing.Point(804, 40);
            this.txtBarangay.Name = "txtBarangay";
            this.txtBarangay.ReadOnly = true;
            this.txtBarangay.Size = new System.Drawing.Size(269, 22);
            this.txtBarangay.TabIndex = 3;
            // 
            // txtMunicipality
            // 
            this.txtMunicipality.Location = new System.Drawing.Point(441, 43);
            this.txtMunicipality.Name = "txtMunicipality";
            this.txtMunicipality.ReadOnly = true;
            this.txtMunicipality.Size = new System.Drawing.Size(315, 22);
            this.txtMunicipality.TabIndex = 4;
            // 
            // cboProvinceSuggestion
            // 
            this.cboProvinceSuggestion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProvinceSuggestion.FormattingEnabled = true;
            this.cboProvinceSuggestion.Location = new System.Drawing.Point(164, 128);
            this.cboProvinceSuggestion.Name = "cboProvinceSuggestion";
            this.cboProvinceSuggestion.Size = new System.Drawing.Size(240, 24);
            this.cboProvinceSuggestion.TabIndex = 5;
            this.cboProvinceSuggestion.SelectedIndexChanged += new System.EventHandler(this.cboProvinceSuggestion_SelectedIndexChanged);
            // 
            // cboProvince
            // 
            this.cboProvince.FormattingEnabled = true;
            this.cboProvince.Location = new System.Drawing.Point(164, 83);
            this.cboProvince.Name = "cboProvince";
            this.cboProvince.Size = new System.Drawing.Size(240, 24);
            this.cboProvince.TabIndex = 6;
            this.cboProvince.SelectedIndexChanged += new System.EventHandler(this.cboProvince_SelectedIndexChanged);
            // 
            // cboMunicipalitySuggestion
            // 
            this.cboMunicipalitySuggestion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMunicipalitySuggestion.FormattingEnabled = true;
            this.cboMunicipalitySuggestion.Location = new System.Drawing.Point(441, 128);
            this.cboMunicipalitySuggestion.Name = "cboMunicipalitySuggestion";
            this.cboMunicipalitySuggestion.Size = new System.Drawing.Size(315, 24);
            this.cboMunicipalitySuggestion.TabIndex = 7;
            this.cboMunicipalitySuggestion.SelectedIndexChanged += new System.EventHandler(this.cboMunicipalitySuggestion_SelectedIndexChanged);
            // 
            // cboMunicipality
            // 
            this.cboMunicipality.FormattingEnabled = true;
            this.cboMunicipality.Location = new System.Drawing.Point(441, 83);
            this.cboMunicipality.Name = "cboMunicipality";
            this.cboMunicipality.Size = new System.Drawing.Size(315, 24);
            this.cboMunicipality.TabIndex = 8;
            this.cboMunicipality.SelectedIndexChanged += new System.EventHandler(this.cboMunicipality_SelectedIndexChanged);
            // 
            // cboBarangaySuggestion
            // 
            this.cboBarangaySuggestion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBarangaySuggestion.FormattingEnabled = true;
            this.cboBarangaySuggestion.Location = new System.Drawing.Point(804, 128);
            this.cboBarangaySuggestion.Name = "cboBarangaySuggestion";
            this.cboBarangaySuggestion.Size = new System.Drawing.Size(269, 24);
            this.cboBarangaySuggestion.TabIndex = 9;
            // 
            // cboBarangay
            // 
            this.cboBarangay.FormattingEnabled = true;
            this.cboBarangay.Location = new System.Drawing.Point(804, 83);
            this.cboBarangay.Name = "cboBarangay";
            this.cboBarangay.Size = new System.Drawing.Size(269, 24);
            this.cboBarangay.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Province";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(438, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "Municipality";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(801, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Barangay";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Suggestions";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 171);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Unmatched Rows";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Original Entry";
            // 
            // btnMainScreen
            // 
            this.btnMainScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMainScreen.Location = new System.Drawing.Point(1238, 564);
            this.btnMainScreen.Name = "btnMainScreen";
            this.btnMainScreen.Size = new System.Drawing.Size(117, 32);
            this.btnMainScreen.TabIndex = 19;
            this.btnMainScreen.Text = "Main Screen";
            this.btnMainScreen.UseVisualStyleBackColor = true;
            this.btnMainScreen.Click += new System.EventHandler(this.btnMainScreen_Click);
            // 
            // btnUseOriginal
            // 
            this.btnUseOriginal.Location = new System.Drawing.Point(1106, 33);
            this.btnUseOriginal.Name = "btnUseOriginal";
            this.btnUseOriginal.Size = new System.Drawing.Size(75, 32);
            this.btnUseOriginal.TabIndex = 20;
            this.btnUseOriginal.Text = "Use this";
            this.btnUseOriginal.UseVisualStyleBackColor = true;
            this.btnUseOriginal.Click += new System.EventHandler(this.btnUseOriginal_Click);
            // 
            // btnUseManual
            // 
            this.btnUseManual.Location = new System.Drawing.Point(1106, 78);
            this.btnUseManual.Name = "btnUseManual";
            this.btnUseManual.Size = new System.Drawing.Size(75, 32);
            this.btnUseManual.TabIndex = 21;
            this.btnUseManual.Text = "Use This";
            this.btnUseManual.UseVisualStyleBackColor = true;
            this.btnUseManual.Click += new System.EventHandler(this.btnUseManual_Click);
            // 
            // btnUseSuggestion
            // 
            this.btnUseSuggestion.Location = new System.Drawing.Point(1106, 123);
            this.btnUseSuggestion.Name = "btnUseSuggestion";
            this.btnUseSuggestion.Size = new System.Drawing.Size(75, 32);
            this.btnUseSuggestion.TabIndex = 22;
            this.btnUseSuggestion.Text = "Use This";
            this.btnUseSuggestion.UseVisualStyleBackColor = true;
            this.btnUseSuggestion.Click += new System.EventHandler(this.btnUseSuggestion_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(1203, 33);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 32);
            this.btnNext.TabIndex = 23;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(1303, 33);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(75, 32);
            this.btnPrev.TabIndex = 24;
            this.btnPrev.Text = "Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // txtRowCount
            // 
            this.txtRowCount.Location = new System.Drawing.Point(304, 166);
            this.txtRowCount.Name = "txtRowCount";
            this.txtRowCount.Size = new System.Drawing.Size(79, 22);
            this.txtRowCount.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(204, 171);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 17);
            this.label8.TabIndex = 26;
            this.label8.Text = "Row Count:";
            // 
            // txtSelectedIndex
            // 
            this.txtSelectedIndex.Location = new System.Drawing.Point(1278, 163);
            this.txtSelectedIndex.Name = "txtSelectedIndex";
            this.txtSelectedIndex.Size = new System.Drawing.Size(100, 22);
            this.txtSelectedIndex.TabIndex = 27;
            // 
            // FormManualMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1390, 608);
            this.Controls.Add(this.txtSelectedIndex);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtRowCount);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnUseSuggestion);
            this.Controls.Add(this.btnUseManual);
            this.Controls.Add(this.btnUseOriginal);
            this.Controls.Add(this.btnMainScreen);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboBarangay);
            this.Controls.Add(this.cboBarangaySuggestion);
            this.Controls.Add(this.cboMunicipality);
            this.Controls.Add(this.cboMunicipalitySuggestion);
            this.Controls.Add(this.cboProvince);
            this.Controls.Add(this.cboProvinceSuggestion);
            this.Controls.Add(this.txtMunicipality);
            this.Controls.Add(this.txtBarangay);
            this.Controls.Add(this.txtProvince);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormManualMatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manual Match";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormSuggestions_Load);
            this.Shown += new System.EventHandler(this.FormManualMatch_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProvince;
        private System.Windows.Forms.TextBox txtBarangay;
        private System.Windows.Forms.TextBox txtMunicipality;
        private System.Windows.Forms.ComboBox cboProvinceSuggestion;
        private System.Windows.Forms.ComboBox cboProvince;
        private System.Windows.Forms.ComboBox cboMunicipalitySuggestion;
        private System.Windows.Forms.ComboBox cboMunicipality;
        private System.Windows.Forms.ComboBox cboBarangaySuggestion;
        private System.Windows.Forms.ComboBox cboBarangay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnMainScreen;
        private System.Windows.Forms.Button btnUseOriginal;
        private System.Windows.Forms.Button btnUseManual;
        private System.Windows.Forms.Button btnUseSuggestion;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.TextBox txtRowCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSelectedIndex;
    }
}