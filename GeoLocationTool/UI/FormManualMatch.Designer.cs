namespace GeoLocationTool.UI
{
    partial class FormManualMatch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.txtRowCount = new System.Windows.Forms.TextBox();
            this.txtSelectedIndex = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnApplyAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView1, 13);
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 212);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1239, 365);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Manual Replacement";
            // 
            // txtProvince
            // 
            this.txtProvince.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.txtProvince, 2);
            this.txtProvince.Location = new System.Drawing.Point(172, 45);
            this.txtProvince.Name = "txtProvince";
            this.txtProvince.ReadOnly = true;
            this.txtProvince.Size = new System.Drawing.Size(260, 22);
            this.txtProvince.TabIndex = 0;
            this.txtProvince.TabStop = false;
            // 
            // txtBarangay
            // 
            this.txtBarangay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBarangay.Location = new System.Drawing.Point(718, 45);
            this.txtBarangay.Name = "txtBarangay";
            this.txtBarangay.ReadOnly = true;
            this.txtBarangay.Size = new System.Drawing.Size(260, 22);
            this.txtBarangay.TabIndex = 0;
            this.txtBarangay.TabStop = false;
            // 
            // txtMunicipality
            // 
            this.txtMunicipality.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMunicipality.Location = new System.Drawing.Point(445, 45);
            this.txtMunicipality.Name = "txtMunicipality";
            this.txtMunicipality.ReadOnly = true;
            this.txtMunicipality.Size = new System.Drawing.Size(260, 22);
            this.txtMunicipality.TabIndex = 0;
            this.txtMunicipality.TabStop = false;
            // 
            // cboProvinceSuggestion
            // 
            this.cboProvinceSuggestion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.cboProvinceSuggestion, 2);
            this.cboProvinceSuggestion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProvinceSuggestion.FormattingEnabled = true;
            this.cboProvinceSuggestion.Location = new System.Drawing.Point(172, 82);
            this.cboProvinceSuggestion.Name = "cboProvinceSuggestion";
            this.cboProvinceSuggestion.Size = new System.Drawing.Size(260, 24);
            this.cboProvinceSuggestion.TabIndex = 1;
            this.cboProvinceSuggestion.SelectedIndexChanged += new System.EventHandler(this.cboProvinceSuggestion_SelectedIndexChanged);
            // 
            // cboProvince
            // 
            this.cboProvince.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.cboProvince, 2);
            this.cboProvince.FormattingEnabled = true;
            this.cboProvince.Location = new System.Drawing.Point(172, 120);
            this.cboProvince.Name = "cboProvince";
            this.cboProvince.Size = new System.Drawing.Size(260, 24);
            this.cboProvince.TabIndex = 5;
            this.cboProvince.SelectedIndexChanged += new System.EventHandler(this.cboProvince_SelectedIndexChanged);
            // 
            // cboMunicipalitySuggestion
            // 
            this.cboMunicipalitySuggestion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMunicipalitySuggestion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMunicipalitySuggestion.FormattingEnabled = true;
            this.cboMunicipalitySuggestion.Location = new System.Drawing.Point(445, 82);
            this.cboMunicipalitySuggestion.Name = "cboMunicipalitySuggestion";
            this.cboMunicipalitySuggestion.Size = new System.Drawing.Size(260, 24);
            this.cboMunicipalitySuggestion.TabIndex = 2;
            this.cboMunicipalitySuggestion.SelectedIndexChanged += new System.EventHandler(this.cboMunicipalitySuggestion_SelectedIndexChanged);
            // 
            // cboMunicipality
            // 
            this.cboMunicipality.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMunicipality.FormattingEnabled = true;
            this.cboMunicipality.Location = new System.Drawing.Point(445, 120);
            this.cboMunicipality.Name = "cboMunicipality";
            this.cboMunicipality.Size = new System.Drawing.Size(260, 24);
            this.cboMunicipality.TabIndex = 6;
            this.cboMunicipality.SelectedIndexChanged += new System.EventHandler(this.cboMunicipality_SelectedIndexChanged);
            // 
            // cboBarangaySuggestion
            // 
            this.cboBarangaySuggestion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboBarangaySuggestion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBarangaySuggestion.FormattingEnabled = true;
            this.cboBarangaySuggestion.Location = new System.Drawing.Point(718, 82);
            this.cboBarangaySuggestion.Name = "cboBarangaySuggestion";
            this.cboBarangaySuggestion.Size = new System.Drawing.Size(260, 24);
            this.cboBarangaySuggestion.TabIndex = 3;
            // 
            // cboBarangay
            // 
            this.cboBarangay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboBarangay.FormattingEnabled = true;
            this.cboBarangay.Location = new System.Drawing.Point(718, 120);
            this.cboBarangay.Name = "cboBarangay";
            this.cboBarangay.Size = new System.Drawing.Size(260, 24);
            this.cboBarangay.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label2, 2);
            this.label2.Location = new System.Drawing.Point(172, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Province";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(445, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "Town";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(718, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Village";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Suggestions";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(163, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Unmatched Rows Count:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Original Entry";
            // 
            // btnMainScreen
            // 
            this.btnMainScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.btnMainScreen, 2);
            this.btnMainScreen.Location = new System.Drawing.Point(1086, 174);
            this.btnMainScreen.Name = "btnMainScreen";
            this.btnMainScreen.Size = new System.Drawing.Size(156, 32);
            this.btnMainScreen.TabIndex = 10;
            this.btnMainScreen.Text = "Main Screen";
            this.btnMainScreen.UseVisualStyleBackColor = true;
            this.btnMainScreen.Click += new System.EventHandler(this.btnMainScreen_Click);
            // 
            // btnUseOriginal
            // 
            this.btnUseOriginal.Location = new System.Drawing.Point(991, 40);
            this.btnUseOriginal.Name = "btnUseOriginal";
            this.btnUseOriginal.Size = new System.Drawing.Size(75, 32);
            this.btnUseOriginal.TabIndex = 0;
            this.btnUseOriginal.Text = "Use this";
            this.btnUseOriginal.UseVisualStyleBackColor = true;
            this.btnUseOriginal.Click += new System.EventHandler(this.btnUseOriginal_Click);
            // 
            // btnUseManual
            // 
            this.btnUseManual.Location = new System.Drawing.Point(991, 116);
            this.btnUseManual.Name = "btnUseManual";
            this.btnUseManual.Size = new System.Drawing.Size(75, 32);
            this.btnUseManual.TabIndex = 8;
            this.btnUseManual.Text = "Use This";
            this.btnUseManual.UseVisualStyleBackColor = true;
            this.btnUseManual.Click += new System.EventHandler(this.btnUseManual_Click);
            // 
            // btnUseSuggestion
            // 
            this.btnUseSuggestion.Location = new System.Drawing.Point(991, 78);
            this.btnUseSuggestion.Name = "btnUseSuggestion";
            this.btnUseSuggestion.Size = new System.Drawing.Size(75, 32);
            this.btnUseSuggestion.TabIndex = 4;
            this.btnUseSuggestion.Text = "Use This";
            this.btnUseSuggestion.UseVisualStyleBackColor = true;
            this.btnUseSuggestion.Click += new System.EventHandler(this.btnUseSuggestion_Click);
            // 
            // txtRowCount
            // 
            this.txtRowCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRowCount.Location = new System.Drawing.Point(172, 179);
            this.txtRowCount.Name = "txtRowCount";
            this.txtRowCount.ReadOnly = true;
            this.txtRowCount.Size = new System.Drawing.Size(116, 22);
            this.txtRowCount.TabIndex = 0;
            this.txtRowCount.TabStop = false;
            // 
            // txtSelectedIndex
            // 
            this.txtSelectedIndex.Location = new System.Drawing.Point(1079, 583);
            this.txtSelectedIndex.Name = "txtSelectedIndex";
            this.txtSelectedIndex.Size = new System.Drawing.Size(1, 22);
            this.txtSelectedIndex.TabIndex = 27;
            this.txtSelectedIndex.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 13;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtSelectedIndex, 10, 8);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtProvince, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label3, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtMunicipality, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtBarangay, 6, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnUseOriginal, 8, 2);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.cboProvinceSuggestion, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cboProvince, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.cboMunicipality, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.cboMunicipalitySuggestion, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.cboBarangay, 6, 4);
            this.tableLayoutPanel1.Controls.Add(this.cboBarangaySuggestion, 6, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnUseManual, 8, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnUseSuggestion, 8, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnMainScreen, 11, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtRowCount, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnApplyAll, 8, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1245, 608);
            this.tableLayoutPanel1.TabIndex = 28;
            // 
            // btnApplyAll
            // 
            this.btnApplyAll.Location = new System.Drawing.Point(991, 174);
            this.btnApplyAll.Name = "btnApplyAll";
            this.btnApplyAll.Size = new System.Drawing.Size(75, 32);
            this.btnApplyAll.TabIndex = 9;
            this.btnApplyAll.Text = "Apply All";
            this.btnApplyAll.UseVisualStyleBackColor = true;
            this.btnApplyAll.Click += new System.EventHandler(this.btnApplyAll_Click);
            // 
            // FormManualMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 608);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormManualMatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Geo Location Tool - Manual Match";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormManualMatch_Load);
            this.Shown += new System.EventHandler(this.FormManualMatch_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TextBox txtRowCount;
        private System.Windows.Forms.TextBox txtSelectedIndex;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnApplyAll;
    }
}