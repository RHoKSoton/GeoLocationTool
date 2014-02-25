// FormManualMatch.cs

namespace GeoLocationTool.UI
{
    using System;
    using System.Windows.Forms;
    using MultiLevelGeoCoder;
    using MultiLevelGeoCoder.DataAccess;
    using MultiLevelGeoCoder.Logic;

    /// <summary>
    /// Form to enable the manual matching/selection of fuzzy match suggestions
    /// </summary>
    public partial class FormManualMatch : Form
    {
        #region Fields

        private readonly FuzzyMatch fuzzyMatch;
        private readonly GeoCoder geoCoder;
        private readonly INearMatchesProvider nearMatches;

        private int selectedRowIndex;

        #endregion Fields

        #region Constructors

        //public FormManualMatch(InputData inputData, LocationData locationData)
        //{
        //    InitializeComponent();
        //    this.inputData = inputData;
        //    this.locationData = locationData;
        //    fuzzyMatch = new FuzzyMatch(locationData);
        //    nearMatches = new NearMatchesProvider(Program.Connection);
        //}
        public FormManualMatch(GeoCoder geoCoder)
        {
            InitializeComponent();
            this.geoCoder = geoCoder;
            fuzzyMatch = new FuzzyMatch(geoCoder.Gazetteer);
            nearMatches = new NearMatchesProvider(Program.Connection);
        }

        #endregion Constructors

        #region Methods

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

        private void btnMainScreen_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedRowIndex < (dataGridView1.RowCount - 1))
                {
                    dataGridView1.Rows[++selectedRowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Navigation error - Next.", ex);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedRowIndex > 0)
                {
                    dataGridView1.Rows[--selectedRowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Navigation error - Prev.", ex);
            }
        }

        private void btnUseManual_Click(object sender, EventArgs e)
        {
            try
            {
                string province = cboProvince.SelectedValue.ToString();
                string municipality = cboMunicipality.SelectedValue.ToString();
                string barangay = cboBarangay.SelectedValue.ToString();
                SaveNearMatch(province, municipality, barangay);
                UpdateRow(province, municipality, barangay);
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Error applying manual selection to the data.", ex);
            }
        }

        private void btnUseOriginal_Click(object sender, EventArgs e)
        {
            try
            {
                string province = txtProvince.Text;
                string municipality = txtMunicipality.Text;
                string barangay = txtBarangay.Text;
                UpdateRow(province, municipality, barangay);
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Error applying original selection to the data.", ex);
            }
        }

        private void btnUseSuggestion_Click(object sender, EventArgs e)
        {
            try
            {
                string province = cboProvinceSuggestion.SelectedValue.ToString();
                string municipality = cboMunicipalitySuggestion.SelectedValue.ToString();
                string barangay = cboBarangaySuggestion.SelectedValue.ToString();
                SaveNearMatch(province, municipality, barangay);
                UpdateRow(province, municipality, barangay);
            }
            catch (Exception ex)
            {
                ErrorHandler.Process(
                    "Error applying suggested selection to the data.",
                    ex);
            }
        }

        private void cboMunicipalitySuggestion_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            try
            {
                DisplayBarangaySuggestions();
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Error displaying level 3 suggestion list.", ex);
            }
        }

        private void cboMunicipality_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DisplayBarangayList();
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Error displaying level 3 manual list.", ex);
            }
        }

        private void cboProvinceSuggestion_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            try
            {
                DisplayMunicipalitySuggestions();
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Error displaying level 3 suggestion list.", ex);
            }
        }

        private void cboProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DisplayMunicipalityList();
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Error displaying level 2 manual list.", ex);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                    txtSelectedIndex.Text = selectedRowIndex.ToString();
                    DisplaySelectedRecord();
                    DisplayProvinceSuggestions();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Grid navigation error.", ex);
            }
        }

        private void DisplayBarangayList()
        {
            // based on selected level 1 and 2
            cboBarangay.DataSource = geoCoder.Gazetteer.Level3LocationNames(
                cboProvince.SelectedValue.ToString(),
                cboMunicipality.SelectedValue.ToString());
        }

        private void DisplayBarangaySuggestions()
        {
            //based on the suggested level 1 and 2 and the original level3
            string level1 = cboProvinceSuggestion.SelectedValue.ToString();
            string level2 = cboMunicipalitySuggestion.SelectedValue.ToString();
            string level3 = txtBarangay.Text.Trim();

            cboBarangaySuggestion.DisplayMember = "DisplayText";
            // temp only, change to location after testing
            cboBarangaySuggestion.ValueMember = "Location";

            cboBarangaySuggestion.DataSource =
                fuzzyMatch.GetLevel3Suggestions(level1, level2, level3);
        }

        private void DisplayMunicipalityList()
        {
            // based on selected level 1
            cboMunicipality.DataSource = geoCoder.Gazetteer.Level2LocationNames(
                cboProvince.SelectedValue.ToString());
        }

        private void DisplayMunicipalitySuggestions()
        {
            //based on the suggested level 1 and the original level2
            string level1 = cboProvinceSuggestion.SelectedValue.ToString();
            string level2 = txtMunicipality.Text.Trim();

            cboMunicipalitySuggestion.DisplayMember = "DisplayText";
            // temp only, change to location after testing
            cboMunicipalitySuggestion.ValueMember = "Location";

            cboMunicipalitySuggestion.DataSource =
                fuzzyMatch.GetLevel2Suggestions(level1, level2);
        }

        private void DisplayProvinceList()
        {
            cboProvince.DataSource = geoCoder.Gazetteer.Level1LocationNames();
        }

        private void DisplayProvinceSuggestions()
        {
            string level1 = txtProvince.Text;
            cboProvinceSuggestion.DisplayMember = "DisplayText";
            // temp only, change to location after testing
            cboProvinceSuggestion.ValueMember = "Location";

            cboProvinceSuggestion.DataSource =
                fuzzyMatch.GetLevel1Suggestions(level1);
        }

        private void DisplaySelectedRecord()
        {
            ColumnHeaderIndices indices = geoCoder.InputColumnIndices();
            txtProvince.Text =
                dataGridView1.Rows[selectedRowIndex].Cells[
                    indices.Admin1]
                    .Value as
                    string;
            txtMunicipality.Text =
                dataGridView1.Rows[selectedRowIndex].Cells[
                    indices.Admin2]
                    .Value as
                    string;
            txtBarangay.Text =
                dataGridView1.Rows[selectedRowIndex].Cells[
                    indices.Admin3]
                    .Value as
                    string;
        }

        private void DisplayUnmatchedRecords()
        {
            dataGridView1.DataSource = geoCoder.UnmatchedRecords();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void FormManualMatch_Load(object sender, EventArgs e)
        {
            try
            {
                SetGridDefaults();
                DisplayUnmatchedRecords();
                DisplayProvinceList();
            }
            catch (Exception ex)
            {
                ErrorHandler.Process(
                    "An error occurred during Manual Match screen load.",
                    ex);
            }
        }

        private void FormManualMatch_Shown(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    Cursor = Cursors.WaitCursor;
                    dataGridView1.Rows[0].Selected = true;
                    DisplaySelectedRecord();
                    DisplayProvinceSuggestions();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Process(
                    "An error occurred during Manual Match screen show.",
                    ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SaveNearMatch(string province, string municipality, string barangay)
        {
            nearMatches.SaveMatch(txtProvince.Text, province);
            nearMatches.SaveMatch(txtMunicipality.Text, province, municipality);
            nearMatches.SaveMatch(txtBarangay.Text, province, municipality, barangay);
        }

        private void SetGridDefaults()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
        }

        private void UpdateRow(string level1, string level2, string level3)
        {
            //display new name values
            dataGridView1.Rows[selectedRowIndex].Cells[InputData.Loc1ColumnName].Value =
                level1;
            dataGridView1.Rows[selectedRowIndex].Cells[InputData.Loc2ColumnName].Value =
                level2;
            dataGridView1.Rows[selectedRowIndex].Cells[InputData.Loc3ColumnName].Value =
                level3;

            //get codes using new names
            Location location = new Location(level1, level3, level2);
            geoCoder.Gazetteer.GetLocationCodes(location);

            //display codes
            dataGridView1.Rows[selectedRowIndex].Cells[InputData.Loc1CodeColumnName]
                .Value =
                location.ProvinceCode;
            dataGridView1.Rows[selectedRowIndex].Cells[InputData.Loc2CodeColumnName]
                .Value =
                location.MunicipalityCode;
            dataGridView1.Rows[selectedRowIndex].Cells[InputData.Loc3CodeColumnName]
                .Value =
                location.BarangayCode;

            DisplayUnmatchedRecords();
        }

        #endregion Methods
    }
}