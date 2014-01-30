// FormManualMatch.cs

namespace GeoLocationTool
{
    using System;
    using System.Data;
    using System.Windows.Forms;

    /// <summary>
    /// Form to enable the manual matching/selection of fuzzy match posibilities - in progress 
    /// </summary>
    public partial class FormManualMatch : Form
    {
        private readonly DataTable dt;
        private readonly int columnIndexLevel1;
        private readonly int columnIndexLevel2;
        private readonly int columnIndexLevel3;
        private readonly GeoLocationData geoLocationData;
        private const string BaracayCodeColumn = "BaracayCode";
        private const string MunicipalityCodeColumn = "MunicipalityCode";
        private const string ProvinceCodeColumn = "ProvinceCode";
        private readonly FuzzyMatch fuzzyMatch;

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

        private void FormSuggestions_Load(object sender, EventArgs e)
        {
            DisplayUnmatchedRecords();
        }

        private void DisplayFuzzyMatches()
        {
            DisplayProvinceFuzzyMatches();
            DisplayMunicipalityFuzzyMatches();
            DisplayBarangayFuzzyMatches();
        }

        private void DisplaySelectedRecord()
        {
            txtProvince.Text =
                dataGridView1.SelectedRows[0].Cells[columnIndexLevel1].Value as string;
            txtMunicipality.Text =
                dataGridView1.SelectedRows[0].Cells[columnIndexLevel2].Value as string;
            txtBarangay.Text =
                dataGridView1.SelectedRows[0].Cells[columnIndexLevel3].Value as string;
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
            dataGridView1.Rows[0].Selected = true;
        }

        private void DisplayBarangayFuzzyMatches()
        {
            cboBarangaySuggestion.DataSource =
                fuzzyMatch.GetBarangaySuggestions(txtBarangay.Text);
            cboBarangaySuggestion.DisplayMember = "DisplayText";
            // temp only, change to location after testing
            cboBarangaySuggestion.ValueMember = "Location";
        }

        private void DisplayMunicipalityFuzzyMatches()
        {
            cboMunicipalitySuggestion.DataSource =
                fuzzyMatch.GetMunicipalitySuggestions(txtMunicipality.Text);
            cboMunicipalitySuggestion.DisplayMember = "DisplayText";
            // temp only, change to location after testing
            cboMunicipalitySuggestion.ValueMember = "Location";
        }

        private void DisplayProvinceFuzzyMatches()
        {
            cboProvinceSuggestion.DataSource =
                fuzzyMatch.GetProvinceSuggestions(txtProvince.Text);
            cboProvinceSuggestion.DisplayMember = "DisplayText";
            // temp only, change to location after testing
            cboProvinceSuggestion.ValueMember = "Location";
        }

        private void btnMainScreen_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                Cursor = Cursors.WaitCursor;
                DisplaySelectedRecord();
                DisplayFuzzyMatches();
                Cursor = Cursors.Default;
            }
        }

        private void FormManualMatch_Shown(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            DisplaySelectedRecord();
            DisplayFuzzyMatches();
            Cursor = Cursors.Default;
        }
    }
}