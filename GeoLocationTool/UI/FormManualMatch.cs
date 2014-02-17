// FormManualMatch.cs

namespace GeoLocationTool.UI
{
    using System;
    using System.Windows.Forms;
    using Logic;

    /// <summary>
    /// Form to enable the manual matching/selection of fuzzy match suggestions
    /// </summary>
    public partial class FormManualMatch : Form
    {
        #region Fields

        private readonly FuzzyMatch fuzzyMatch;
        private readonly InputData inputData;
        private readonly LocationData locationData;

        private int selectedRowIndex;

        #endregion Fields

        #region Constructors

        public FormManualMatch(InputData inputData, LocationData locationData)
        {
            InitializeComponent();
            this.inputData = inputData;
            this.locationData = locationData;
            fuzzyMatch = new FuzzyMatch(locationData);
        }

        #endregion Constructors

        #region Methods

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
            cboBarangay.DataSource = locationData.Level3LocationNames(
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
            cboMunicipality.DataSource = locationData.Level2LocationNames(
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
            cboProvince.DataSource = locationData.Level1LocationNames();
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
            txtProvince.Text =
                dataGridView1.Rows[selectedRowIndex].Cells[inputData.ColumnIndexLoc1]
                    .Value as
                    string;
            txtMunicipality.Text =
                dataGridView1.Rows[selectedRowIndex].Cells[inputData.ColumnIndexLoc2]
                    .Value as
                    string;
            txtBarangay.Text =
                dataGridView1.Rows[selectedRowIndex].Cells[inputData.ColumnIndexLoc3]
                    .Value as
                    string;
        }

        private void DisplayUnmatchedRecords()
        {
            dataGridView1.DataSource = inputData.GetUnmatchedRecords();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void FormManualMatch_Load(object sender, EventArgs e)
        {
            try
            {
                DisplayUnmatchedRecords();
                DisplayProvinceList();
                // txtRowCount.DataBindings.Add("Text", dt.Rows, "Count");
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

        private void UpdateRow(string level1, string level2, string level3)
        {
            //display new name values
            dataGridView1.Rows[selectedRowIndex].Cells[inputData.ColumnIndexLoc1].Value =
                level1;
            dataGridView1.Rows[selectedRowIndex].Cells[inputData.ColumnIndexLoc2].Value =
                level2;
            dataGridView1.Rows[selectedRowIndex].Cells[inputData.ColumnIndexLoc3].Value =
                level3;

            //get codes using new names
            Location location = new Location(level1, level3, level2);
            locationData.GetLocationCodes(location);

            //display codes
            dataGridView1.Rows[selectedRowIndex].Cells[InputData.Level1CodeColumnName]
                .Value =
                location.ProvinceCode;
            dataGridView1.Rows[selectedRowIndex].Cells[InputData.Level2CodeColumnName]
                .Value =
                location.MunicipalityCode;
            dataGridView1.Rows[selectedRowIndex].Cells[InputData.Level3CodeColumnName]
                .Value =
                location.BarangayCode;

            DisplayUnmatchedRecords();
        }

        #endregion Methods
    }
}