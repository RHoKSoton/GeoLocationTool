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
            InitializeComponent();
            this.geoCoder = geoCoder;
        }

        #endregion Constructors

        #region Methods

        private void btnBack_Click(object sender, EventArgs e)
        {
            Owner.Show();
            Hide();
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
                    DisplayColumnHeaderList();
                }
                
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Could not read file.", ex);
            }
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

            dataGridView1.DataSource = geoCoder.InputRecords;
            dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            SetColumnStyle();
        }

        private string SelectFile(string filter)
        {
            txtFileName.Clear();
            var path = UiHelper.GetFileName(filter).Trim();
            txtFileName.Text = path;
            return path;
        }

        private void DisplayColumnHeaderList()
        {         
            comboBox1.DataSource = geoCoder.AllInputColumnNames();
        }

        private void btnManualMatch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!geoCoder.IsGazetteerInitialised())
                {
                    MessageBox.Show(
                        "Gazetteer data missing, please read in a gazetteer file.");
                    return;
                }

                if (geoCoder.InputRecords == null)
                {
                    MessageBox.Show("Input data missing, please read in an input file.");
                    return;
                }

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
                        "Gazetteer data missing, please read in a gazetteer file.");
                    return;
                }

                if (geoCoder.InputRecords == null)
                {
                    MessageBox.Show("Input data missing, please read in an input file.");
                    return;
                }

                SetColumnHeaders();
                geoCoder.MatchAll();
                dataGridView1.DataSource = geoCoder.InputRecords;
            }
            catch (Exception ex)
            {
                ErrorHandler.Process(
                    "A problem occurred with the data matching process.",
                    ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAsCsv();
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Error saving file.", ex);
            }
        }

        private void FormLoadData_Load(object sender, EventArgs e)
        {
            SetDefaults();
        }

        private void SaveAsCsv()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.AddExtension = true;
                dialog.DefaultExt = "csv";
                dialog.Filter = "CSV(*.csv)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    geoCoder.SaveToCsvFile(dialog.FileName);
                }
            }
        }

        private void SetColumnStyle()
        {
            //todo any style needed here?
        }

        private void SetDefaults()
        {
            udBarangay.DecimalPlaces = 0;
            udProvince.DecimalPlaces = 0;
            udMunicipality.DecimalPlaces = 0;
            udProvince.Value = 1;
            udMunicipality.Value = 2;
            udBarangay.Value = 3;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.ReadOnly = true;
            rdoImportCsv.Checked = true;
        }

        private void SetColumnHeaders()
        {
            ColumnHeaderIndices headerIndices = new ColumnHeaderIndices();
            headerIndices.Admin1 = (int) udProvince.Value - 1;
            headerIndices.Admin2 = (int) udMunicipality.Value - 1;
            headerIndices.Admin3 = (int) udBarangay.Value - 1;

            geoCoder.SetInputColumns(headerIndices);
        }

        #endregion Methods
    }
}