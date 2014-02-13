// FormManualMatch.cs

namespace GeoLocationTool
{
    using System;
    using System.Data;
    using System.Windows.Forms;

    /// <summary>
    /// Form to enable the manual matching/selection of fuzzy match suggestions
    /// </summary>
    public partial class FormManualMatch : Form
    {
        #region Fields

        private const string BaracayCodeColumn = "BaracayCode";
        private const string MunicipalityCodeColumn = "MunicipalityCode";
        private const string ProvinceCodeColumn = "ProvinceCode";

        private readonly int columnIndexLevel1;
        private readonly int columnIndexLevel2;
        private readonly int columnIndexLevel3;
        private readonly DataTable dt;
        private readonly FuzzyMatch fuzzyMatch;
        private readonly GeoLocationData geoLocationData;

        private int selectedRowIndex;

        #endregion Fields

        #region Constructors

        public FormManualMatch(
            DataTable dt,
            int columnIndexLevel1,
            int columnIndexLevel2,
            int columnIndexLevel3,
            GeoLocationData geoLocationData)
        {
            InitializeComponent();
            this.dt = dt;
            this.columnIndexLevel1 = columnIndexLevel1;
            this.columnIndexLevel2 = columnIndexLevel2;
            this.columnIndexLevel3 = columnIndexLevel3;
            this.geoLocationData = geoLocationData;
            fuzzyMatch = new FuzzyMatch(geoLocationData);
        }

        #endregion Constructors

        #region Methods

        private void btnMainScreen_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex < (dataGridView1.RowCount - 1))
            {
                dataGridView1.Rows[++selectedRowIndex].Selected = true;
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex > 0)
            {
                dataGridView1.Rows[--selectedRowIndex].Selected = true;
            }
        }

        private void btnUseManual_Click(object sender, EventArgs e)
        {
            string province = cboProvince.SelectedValue.ToString();
            string municipality = cboMunicipality.SelectedValue.ToString();
            string barangay = cboBarangay.SelectedValue.ToString();
            UpdateRow(province, municipality, barangay);
        }

        private void btnUseOriginal_Click(object sender, EventArgs e)
        {
            string province = txtProvince.Text;
            string municipality = txtMunicipality.Text;
            string barangay = txtBarangay.Text;
            UpdateRow(province, municipality, barangay);
        }

        private void btnUseSuggestion_Click(object sender, EventArgs e)
        {
            string province = cboProvinceSuggestion.SelectedValue.ToString();
            string municipality = cboMunicipalitySuggestion.SelectedValue.ToString();
            string barangay = cboBarangaySuggestion.SelectedValue.ToString();
            UpdateRow(province, municipality, barangay);
        }

        private void cboMunicipalitySuggestion_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            DisplayBarangaySuggestions();
        }

        private void cboMunicipality_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayBarangayList();
        }

        private void cboProvinceSuggestion_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            DisplayMunicipalitySuggestions();
        }

        private void cboProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayMunicipalityList();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                txtSelectedIndex.Text = selectedRowIndex.ToString();
                DisplaySelectedRecord();
                DisplayProvinceSuggestions();
            }
        }

        private void DisplayBarangayList()
        {
            // based on selected level 1 and 2
            cboBarangay.DataSource = geoLocationData.Level3LocationNames(
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
            cboMunicipality.DataSource = geoLocationData.Level2LocationNames(
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
            cboProvince.DataSource = geoLocationData.Level1LocationNames();
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
                dataGridView1.Rows[selectedRowIndex].Cells[columnIndexLevel1].Value as
                    string;
            txtMunicipality.Text =
                dataGridView1.Rows[selectedRowIndex].Cells[columnIndexLevel2].Value as
                    string;
            txtBarangay.Text =
                dataGridView1.Rows[selectedRowIndex].Cells[columnIndexLevel3].Value as
                    string;
        }

        private void DisplayUnmatchedRecords()
        {
            // only show those records where a location code is null
            EnumerableRowCollection<DataRow> query = from record in dt.AsEnumerable()
                where record.Field<String>(ProvinceCodeColumn) == null ||
                      record.Field<string>(MunicipalityCodeColumn) == null ||
                      record.Field<string>(BaracayCodeColumn) == null
                select record;

            DataView unmatched = query.AsDataView();
            dataGridView1.DataSource = unmatched;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void FormManualMatch_Shown(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Cursor = Cursors.WaitCursor;
                dataGridView1.Rows[0].Selected = true;
                DisplaySelectedRecord();
                DisplayProvinceSuggestions();
                Cursor = Cursors.Default;
            }
        }

        private void FormSuggestions_Load(object sender, EventArgs e)
        {
            DisplayUnmatchedRecords();
            DisplayProvinceList();
            // txtRowCount.DataBindings.Add("Text", dt.Rows, "Count");
        }

        private void UpdateRow(string province, string municipality, string barangay)
        {
            //display new text
            dataGridView1.Rows[selectedRowIndex].Cells[columnIndexLevel1].Value = province;
            dataGridView1.Rows[selectedRowIndex].Cells[columnIndexLevel2].Value =
                municipality;
            dataGridView1.Rows[selectedRowIndex].Cells[columnIndexLevel3].Value = barangay;

            //get codes
            Location location = new Location(province, barangay, municipality);
            geoLocationData.GetLocationCodes(location);

            //display codes
            dataGridView1.Rows[selectedRowIndex].Cells[ProvinceCodeColumn].Value =
                location.ProvinceCode;
            dataGridView1.Rows[selectedRowIndex].Cells[MunicipalityCodeColumn].Value =
                location.MunicipalityCode;
            dataGridView1.Rows[selectedRowIndex].Cells[BaracayCodeColumn].Value =
                location.BarangayCode;

            DisplayUnmatchedRecords();
        }

        #endregion Methods
    }
}