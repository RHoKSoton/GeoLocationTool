﻿// FormLoadInput.cs
namespace GeoLocationTool.UI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;

    using MultiLevelGeoCoder;
    using MultiLevelGeoCoder.Logic;

    /// <summary>
    /// Main form: displays options and loads the input data into a grid.
    /// </summary>
    public partial class FormLoadInput : Form
    {
        #region Fields

        private readonly IGeoCoder geoCoder;

        #endregion Fields

        #region Constructors

        public FormLoadInput(IGeoCoder geoCoder)
        {
            if (geoCoder == null) throw new ArgumentNullException("geoCoder");
            InitializeComponent();
            this.geoCoder = geoCoder;
        }

        #endregion Constructors

        #region Methods

        private static string SelectFile(string filter)
        {
            return UiHelper.GetFileName(filter).Trim();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {                  
                Hide();
                Owner.Show();       
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Could not load Gazetteer Data form.", ex);
            }
        }

        private void btnLoadInputFile_Click(object sender, EventArgs e)
        {
            try
            {
                const string csvFilter = "csv files (*.csv)|*.csv";
                const string tabFilter =
                    "tab delimited files (*.tsv,*.txt,*.tab)|*.tsv; *.txt; *.tab";
                string filter = csvFilter;
                bool isTab = rdoImportTabDelim.Checked;

                if (isTab)
                {
                    filter = tabFilter;
                }

                var path = SelectFile(filter);
                if (!String.IsNullOrWhiteSpace(path))
                {
                    LoadFile(path, isTab);
                    DisplayColumnNameLists();
                    txtOutputFileName.Clear();

                    EnableButtons(true);

                    // disable Manual Match form untill Match All has been run
                    btnManualMatch.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Could not read file.", ex);
            }
        }

        private void btnManualMatch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!geoCoder.IsGazetteerInitialised())
                {
                    MessageBox.Show(
                        "Gazetteer data missing, please read in a gazetteer file.",
                        "Missing");
                    return;
                }

                if (geoCoder.InputData == null)
                {
                    MessageBox.Show(
                        "Input data missing, please read in an input file.",
                        "Missing");
                    return;
                }

                SetColumnNames();
                FormManualMatch formManualMatch = new FormManualMatch(
                    geoCoder,
                    dataGridView1);
                formManualMatch.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorHandler.Process(
                    "A problem occurred with the Manual Match screen load.",
                    ex);
            }
        }

        private void btnMatchData_Click(object sender, EventArgs e)
        {
            try
            {
                if (!geoCoder.IsGazetteerInitialised())
                {
                    MessageBox.Show(
                        "Gazetteer data missing, please read in a gazetteer file.",
                        "Missing");
                    return;
                }

                if (geoCoder.InputData == null)
                {
                    MessageBox.Show(
                        "Input data missing, please read in an input file.",
                        "Missing");
                    return;
                }

                SetColumnNames();

                // disconnect the data grid untill the coding is complete
                dataGridView1.DataSource = null;

                Cursor = Cursors.WaitCursor;
                geoCoder.AddAllLocationCodes();
                btnManualMatch.Enabled = true;
                DisplayData();
            }
            catch (Exception ex)
            {
                ErrorHandler.Process(
                    "A problem occurred with the data matching process.",
                    ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (geoCoder.MatchSaved)
                {
                    MessageBox.Show(
                        "Please 'Match All' before saving.",
                        "Inconsistent Data");
                }
                else
                {
                    SaveOutputFile();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("A problem occurred with the output save.", ex);
            }
        }

        private void btnSelectOutputFile_Click(object sender, EventArgs e)
        {
            try
               {
             SelectOutputFileName();
              }
              catch (Exception ex)
               {
              ErrorHandler.Process("Error selecting output file name.", ex);
               }
        }

        private void DisplayColumnNameLists()
        {
            InputColumnHeaders defaultColumnHeaders = geoCoder.DefaultInputColumnHeaders();
            cboLevel1.DataSource = geoCoder.InputColumnHeaders();

            //level 2 and 3 columns are optional so display a blank row at the top
            IList<string> columnNames2 = geoCoder.InputColumnHeaders();
            columnNames2.Insert(0, string.Empty);
            cboLevel2.DataSource = columnNames2;

            IList<string> columnNames3 = geoCoder.InputColumnHeaders();
            columnNames3.Insert(0, string.Empty);
            cboLevel3.DataSource = columnNames3;

            // set defaults if they exist in the input sheet
            cboLevel1.SelectedIndex = cboLevel1.FindStringExact(defaultColumnHeaders.Level1);
            cboLevel2.SelectedIndex = cboLevel2.FindStringExact(defaultColumnHeaders.Level2);
            cboLevel3.SelectedIndex = cboLevel3.FindStringExact(defaultColumnHeaders.Level3);
        }

        private void DisplayData()
        {
            dataGridView1.DataSource = geoCoder.InputData;
            FormatGrid();
        }

        private void EnableButtons(bool enabled)
        {
            btnMatchData.Enabled = enabled;
            btnSaveOutput.Enabled = enabled;
            btnSelectOutputFile.Enabled = enabled;
        }

        private void FormatGrid()
        {
            dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.AllCells;
            DataGridViewColumnCollection columnCollection = dataGridView1.Columns;
            DataGridViewColumn lastVisibleColumn =
                columnCollection.GetLastColumn(
                    DataGridViewElementStates.Visible,
                    DataGridViewElementStates.None);
            if (lastVisibleColumn != null)
                lastVisibleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void FormLoadData_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (geoCoder.InputData == null) return;
                if (geoCoder.InputData.GetChanges() != null)
                {
                    DialogResult result = MessageBox.Show(
                        "Unsaved changes, would you like to save the changes?",
                        "Unsaved Changes",
                        MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        SaveOutputFile();
                        e.Cancel = true;
                    }
                }
            }
        }

        private void FormLoadData_Load(object sender, EventArgs e)
        {
            SetDefaults();
        }

        private void LoadFile(string path, bool isTab)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                txtFileName.Clear();
                if (isTab)
                {
                    geoCoder.LoadInputFileTabDelim(path);
                }
                else
                {
                    geoCoder.LoadInputFileCsv(path);
                }

                txtFileName.Text = path;
                DisplayData();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SaveOutputFile()
        {
            try
            {
                if (geoCoder.InputData == null)
                {
                    MessageBox.Show(
                        "Input data missing, please read in an input file.",
                        "Missing");
                    return;
                }

                if (string.IsNullOrEmpty(txtOutputFileName.Text))
                {
                    SelectOutputFileName();
                }

                if (!string.IsNullOrEmpty(txtOutputFileName.Text))
                {
                    geoCoder.SaveOutputFile(txtOutputFileName.Text);
                    MessageBox.Show("Output file saved");
                }
                else
                {
                    MessageBox.Show("Output file not saved");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("File save error.", ex);
            }
        }

        private void SelectOutputFileName()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.AddExtension = true;
                dialog.DefaultExt = "csv";
                dialog.Filter = "CSV(*.csv)|*.*";
                dialog.Title = "Select File";

                // set default output filename
                string inputFileName =
                    Path.GetFileNameWithoutExtension(txtFileName.Text.Trim());
                dialog.FileName = string.Concat(inputFileName, "_matched");

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputFileName.Text = dialog.FileName;
                }
            }
        }

        private void SetColumnNames()
        {
            InputColumnHeaders inputColumnHeaders = new InputColumnHeaders();
            inputColumnHeaders.Level1 = cboLevel1.SelectedValue as string;
            inputColumnHeaders.Level2 = cboLevel2.SelectedValue as string;
            inputColumnHeaders.Level3 = cboLevel3.SelectedValue as string;

            geoCoder.SetInputColumns(inputColumnHeaders);
        }

        private void SetDefaults()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.ReadOnly = true;
            rdoImportCsv.Checked = true;
            EnableButtons(false);
            btnManualMatch.Enabled = false;
        }

        #endregion Methods
    }
}