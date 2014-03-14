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
            this.txtLevel1Original = new System.Windows.Forms.TextBox();
            this.txtLevel3Original = new System.Windows.Forms.TextBox();
            this.txtLevel2Original = new System.Windows.Forms.TextBox();
            this.cboLevel1Suggestion = new System.Windows.Forms.ComboBox();
            this.cboLevel1Manual = new System.Windows.Forms.ComboBox();
            this.cboLevel2Suggestion = new System.Windows.Forms.ComboBox();
            this.cboLevel2Manual = new System.Windows.Forms.ComboBox();
            this.cboLevel3Suggestion = new System.Windows.Forms.ComboBox();
            this.cboLevel3Manual = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnMainScreen = new System.Windows.Forms.Button();
            this.btnUseManual = new System.Windows.Forms.Button();
            this.btnUseSuggestion = new System.Windows.Forms.Button();
            this.txtRowCount = new System.Windows.Forms.TextBox();
            this.txtSelectedIndex = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnApplyAll = new System.Windows.Forms.Button();
            this.chkUnmatchedOnly = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView1, 13);
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 202);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1239, 403);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "All Names:";
            // 
            // txtLevel1Original
            // 
            this.txtLevel1Original.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.txtLevel1Original, 2);
            this.txtLevel1Original.Location = new System.Drawing.Point(107, 40);
            this.txtLevel1Original.Name = "txtLevel1Original";
            this.txtLevel1Original.ReadOnly = true;
            this.txtLevel1Original.Size = new System.Drawing.Size(260, 22);
            this.txtLevel1Original.TabIndex = 0;
            this.txtLevel1Original.TabStop = false;
            // 
            // txtLevel3Original
            // 
            this.txtLevel3Original.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLevel3Original.Location = new System.Drawing.Point(647, 40);
            this.txtLevel3Original.Name = "txtLevel3Original";
            this.txtLevel3Original.ReadOnly = true;
            this.txtLevel3Original.Size = new System.Drawing.Size(260, 22);
            this.txtLevel3Original.TabIndex = 0;
            this.txtLevel3Original.TabStop = false;
            // 
            // txtLevel2Original
            // 
            this.txtLevel2Original.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLevel2Original.Location = new System.Drawing.Point(377, 40);
            this.txtLevel2Original.Name = "txtLevel2Original";
            this.txtLevel2Original.ReadOnly = true;
            this.txtLevel2Original.Size = new System.Drawing.Size(260, 22);
            this.txtLevel2Original.TabIndex = 0;
            this.txtLevel2Original.TabStop = false;
            // 
            // cboLevel1Suggestion
            // 
            this.cboLevel1Suggestion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.cboLevel1Suggestion, 2);
            this.cboLevel1Suggestion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLevel1Suggestion.FormattingEnabled = true;
            this.cboLevel1Suggestion.Location = new System.Drawing.Point(107, 71);
            this.cboLevel1Suggestion.Name = "cboLevel1Suggestion";
            this.cboLevel1Suggestion.Size = new System.Drawing.Size(260, 24);
            this.cboLevel1Suggestion.TabIndex = 1;
            this.cboLevel1Suggestion.SelectedIndexChanged += new System.EventHandler(this.cboLevel1Suggestion_SelectedIndexChanged);
            // 
            // cboLevel1Manual
            // 
            this.cboLevel1Manual.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.cboLevel1Manual, 2);
            this.cboLevel1Manual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLevel1Manual.FormattingEnabled = true;
            this.cboLevel1Manual.Location = new System.Drawing.Point(107, 109);
            this.cboLevel1Manual.Name = "cboLevel1Manual";
            this.cboLevel1Manual.Size = new System.Drawing.Size(260, 24);
            this.cboLevel1Manual.TabIndex = 5;
            this.cboLevel1Manual.SelectedIndexChanged += new System.EventHandler(this.cboLevel1Manual_SelectedIndexChanged);
            // 
            // cboLevel2Suggestion
            // 
            this.cboLevel2Suggestion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLevel2Suggestion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLevel2Suggestion.FormattingEnabled = true;
            this.cboLevel2Suggestion.Location = new System.Drawing.Point(377, 72);
            this.cboLevel2Suggestion.Name = "cboLevel2Suggestion";
            this.cboLevel2Suggestion.Size = new System.Drawing.Size(260, 24);
            this.cboLevel2Suggestion.TabIndex = 2;
            this.cboLevel2Suggestion.SelectedIndexChanged += new System.EventHandler(this.cboLevel2Suggestion_SelectedIndexChanged);
            // 
            // cboLevel2Manual
            // 
            this.cboLevel2Manual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLevel2Manual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLevel2Manual.FormattingEnabled = true;
            this.cboLevel2Manual.Location = new System.Drawing.Point(377, 110);
            this.cboLevel2Manual.Name = "cboLevel2Manual";
            this.cboLevel2Manual.Size = new System.Drawing.Size(260, 24);
            this.cboLevel2Manual.TabIndex = 6;
            this.cboLevel2Manual.SelectedIndexChanged += new System.EventHandler(this.cboLevel2Manual_SelectedIndexChanged);
            // 
            // cboLevel3Suggestion
            // 
            this.cboLevel3Suggestion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLevel3Suggestion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLevel3Suggestion.FormattingEnabled = true;
            this.cboLevel3Suggestion.Location = new System.Drawing.Point(647, 72);
            this.cboLevel3Suggestion.Name = "cboLevel3Suggestion";
            this.cboLevel3Suggestion.Size = new System.Drawing.Size(260, 24);
            this.cboLevel3Suggestion.TabIndex = 3;
            // 
            // cboLevel3Manual
            // 
            this.cboLevel3Manual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLevel3Manual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLevel3Manual.FormattingEnabled = true;
            this.cboLevel3Manual.Location = new System.Drawing.Point(647, 110);
            this.cboLevel3Manual.Name = "cboLevel3Manual";
            this.cboLevel3Manual.Size = new System.Drawing.Size(260, 24);
            this.cboLevel3Manual.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label2, 2);
            this.label2.Location = new System.Drawing.Point(107, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Admin 1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(377, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "Admin 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(647, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Admin 3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Suggestions:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 171);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Row Count:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Original Entry:";
            // 
            // btnMainScreen
            // 
            this.btnMainScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.btnMainScreen, 2);
            this.btnMainScreen.Location = new System.Drawing.Point(1086, 164);
            this.btnMainScreen.Name = "btnMainScreen";
            this.btnMainScreen.Size = new System.Drawing.Size(156, 32);
            this.btnMainScreen.TabIndex = 10;
            this.btnMainScreen.Text = "Main Screen";
            this.btnMainScreen.UseVisualStyleBackColor = true;
            this.btnMainScreen.Click += new System.EventHandler(this.btnMainScreen_Click);
            // 
            // btnUseManual
            // 
            this.btnUseManual.Location = new System.Drawing.Point(917, 106);
            this.btnUseManual.Name = "btnUseManual";
            this.btnUseManual.Size = new System.Drawing.Size(75, 32);
            this.btnUseManual.TabIndex = 8;
            this.btnUseManual.Text = "Use This";
            this.btnUseManual.UseVisualStyleBackColor = true;
            this.btnUseManual.Click += new System.EventHandler(this.btnUseManual_Click);
            // 
            // btnUseSuggestion
            // 
            this.btnUseSuggestion.Location = new System.Drawing.Point(917, 68);
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
            this.txtRowCount.Location = new System.Drawing.Point(107, 169);
            this.txtRowCount.Name = "txtRowCount";
            this.txtRowCount.ReadOnly = true;
            this.txtRowCount.Size = new System.Drawing.Size(116, 22);
            this.txtRowCount.TabIndex = 0;
            this.txtRowCount.TabStop = false;
            // 
            // txtSelectedIndex
            // 
            this.txtSelectedIndex.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSelectedIndex.Location = new System.Drawing.Point(647, 169);
            this.txtSelectedIndex.Name = "txtSelectedIndex";
            this.txtSelectedIndex.Size = new System.Drawing.Size(82, 22);
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
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtLevel1Original, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label3, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtLevel2Original, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtLevel3Original, 6, 2);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.cboLevel1Suggestion, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cboLevel1Manual, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.cboLevel2Manual, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.cboLevel2Suggestion, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.cboLevel3Manual, 6, 4);
            this.tableLayoutPanel1.Controls.Add(this.cboLevel3Suggestion, 6, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnUseManual, 8, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnUseSuggestion, 8, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnMainScreen, 11, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtRowCount, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnApplyAll, 8, 6);
            this.tableLayoutPanel1.Controls.Add(this.chkUnmatchedOnly, 4, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtSelectedIndex, 6, 6);
            this.tableLayoutPanel1.Controls.Add(this.button1, 9, 3);
            this.tableLayoutPanel1.Controls.Add(this.button2, 9, 4);
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
            this.btnApplyAll.Location = new System.Drawing.Point(917, 164);
            this.btnApplyAll.Name = "btnApplyAll";
            this.btnApplyAll.Size = new System.Drawing.Size(75, 32);
            this.btnApplyAll.TabIndex = 9;
            this.btnApplyAll.Text = "Match All";
            this.btnApplyAll.UseVisualStyleBackColor = true;
            this.btnApplyAll.Click += new System.EventHandler(this.btnApplyAll_Click);
            // 
            // chkUnmatchedOnly
            // 
            this.chkUnmatchedOnly.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkUnmatchedOnly.AutoSize = true;
            this.chkUnmatchedOnly.Location = new System.Drawing.Point(377, 169);
            this.chkUnmatchedOnly.Name = "chkUnmatchedOnly";
            this.chkUnmatchedOnly.Size = new System.Drawing.Size(173, 21);
            this.chkUnmatchedOnly.TabIndex = 28;
            this.chkUnmatchedOnly.Text = "Show Unmatched Only";
            this.chkUnmatchedOnly.UseVisualStyleBackColor = true;
            this.chkUnmatchedOnly.CheckedChanged += new System.EventHandler(this.chkUnmatchedOnly_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(998, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 32);
            this.button1.TabIndex = 29;
            this.button1.Text = "Undo";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(998, 106);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 32);
            this.button2.TabIndex = 30;
            this.button2.Text = "Undo";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // FormManualMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 608);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormManualMatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Multi Level Geo-Coder Tool - Manual Match";
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
        private System.Windows.Forms.TextBox txtLevel1Original;
        private System.Windows.Forms.TextBox txtLevel3Original;
        private System.Windows.Forms.TextBox txtLevel2Original;
        private System.Windows.Forms.ComboBox cboLevel1Suggestion;
        private System.Windows.Forms.ComboBox cboLevel1Manual;
        private System.Windows.Forms.ComboBox cboLevel2Suggestion;
        private System.Windows.Forms.ComboBox cboLevel2Manual;
        private System.Windows.Forms.ComboBox cboLevel3Suggestion;
        private System.Windows.Forms.ComboBox cboLevel3Manual;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnMainScreen;
        private System.Windows.Forms.Button btnUseManual;
        private System.Windows.Forms.Button btnUseSuggestion;
        private System.Windows.Forms.TextBox txtRowCount;
        private System.Windows.Forms.TextBox txtSelectedIndex;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnApplyAll;
        private System.Windows.Forms.CheckBox chkUnmatchedOnly;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}