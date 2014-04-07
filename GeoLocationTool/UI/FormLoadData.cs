// FormLoadData.cs

namespace GeoLocationTool.UI
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using MultiLevelGeoCoder;
    using MultiLevelGeoCoder.Logic;

    /// <summary>
    /// Main form: displays options and loads the input data into a grid.
    /// </summary>
    public partial class FormLoadData : Form
    {
        #region Fields

        private readonly IGeoCoder geoCoder;

        #endregion Fields

        #region Constructors

        public FormLoadData(IGeoCoder geoCoder)
        {
            if (geoCoder == null) throw new ArgumentNullException("geoCoder");
            InitializeComponent();
            this.geoCoder = geoCoder;
        }

        #endregion Constructors

        #region Methods

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Owner.Show();
                Hide();
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
                        "Gazetteer data missing, please read in a gazetteer file.", "Missing");
                    return;
                }

                if (geoCoder.InputData == null)
                {
                    MessageBox.Show("Input data missing, please read in an input file.", "Missing");
                    return;
                }

                if (string.IsNullOrEmpty(geoCoder.OutputFileName))
                {
                    SelectOutputFileName();
                }

                if (string.IsNullOrEmpty(geoCoder.OutputFileName))
                {
                    MessageBox.Show("Output file missing, please select the output file.", "Missing");
                    return;
                }
                SetColumnNames();
                FormManualMatch formManualMatch = new FormManualMatch(geoCoder);
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
                        "Gazetteer data missing, please read in a gazetteer file.", "Missing");
                    return;
                }

                if (geoCoder.InputData == null)
                {
                    MessageBox.Show("Input data missing, please read in an input file.", "Missing");
                    return;
                }

                SetColumnNames();
                geoCoder.CodeAll();
                DisplayData();
                SaveOutputFile();
            }
            catch (Exception ex)
            {
                ErrorHandler.Process(
                    "A problem occurred with the data matching process.",
                    ex);
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
                ErrorHandler.Process("Error setting output file name.", ex);
            }
        }

        private void DisplayColumnNameLists()
        {
            InputColumnNames defaultColumnNames = geoCoder.DefaultInputColumnNames();
            cboLevel1.DataSource = geoCoder.AllInputColumnNames();

            //level 2 and 3 columns are optional so display a blank row at the top
            IList<string> columnNames2 = geoCoder.AllInputColumnNames();
            columnNames2.Insert(0, string.Empty);
            cboLevel2.DataSource = columnNames2;

            IList<string> columnNames3 = geoCoder.AllInputColumnNames();
            columnNames3.Insert(0, string.Empty);
            cboLevel3.DataSource = columnNames3;

            // set defaults if they exist in the input sheet
            cboLevel1.SelectedIndex = cboLevel1.FindStringExact(defaultColumnNames.Level1);
            cboLevel2.SelectedIndex = cboLevel2.FindStringExact(defaultColumnNames.Level2);
            cboLevel3.SelectedIndex = cboLevel3.FindStringExact(defaultColumnNames.Level3);
        }

        private void DisplayData()
        {
            dataGridView1.DataSource = geoCoder.InputData;
            dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
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
            if (isTab)
            {
                geoCoder.LoadInputFileTabDelim(path);
            }
            else
            {
                geoCoder.LoadInputFileCsv(path);
            }

            DisplayData();
        }

        private void SaveOutputFile()
        {
            try
            {
                if (string.IsNullOrEmpty(geoCoder.OutputFileName))
                {
                    SelectOutputFileName();
                }

                if (!string.IsNullOrEmpty(geoCoder.OutputFileName))
                {
                    geoCoder.SaveOutputFile();
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

        private string SelectFile(string filter)
        {
            txtFileName.Clear();
            var path = UiHelper.GetFileName(filter).Trim();
            txtFileName.Text = path;
            return path;
        }

        private void SelectOutputFileName()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.AddExtension = true;
                dialog.DefaultExt = "csv";
                dialog.Filter = "CSV(*.csv)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    geoCoder.OutputFileName = dialog.FileName;
                    txtOutputFileName.Text = geoCoder.OutputFileName;
                }
            }
        }

        private void SetColumnNames()
        {
            InputColumnNames inputColumnNames = new InputColumnNames();
            inputColumnNames.Level1 = cboLevel1.SelectedValue as string;
            inputColumnNames.Level2 = cboLevel2.SelectedValue as string;
            inputColumnNames.Level3 = cboLevel3.SelectedValue as string;

            geoCoder.SetInputColumns(inputColumnNames);
        }

        private void SetDefaults()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.ReadOnly = true;
            rdoImportCsv.Checked = true;
        }

        #endregion Methods
    }
}