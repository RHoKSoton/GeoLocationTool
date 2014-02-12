// FormManualMatch.cs

namespace GeoLocationTool
{
    using System;
    using System.Data;
    using System.Linq;
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
        private int selectedRowIndex;

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
            DisplayManualLists();
           // txtRowCount.DataBindings.Add("Text", dt.Rows, "Count");
        }

        private void DisplayManualLists()
        {
            cboProvince.DataSource = geoLocationData.Level1List();
            cboMunicipality.DataSource = geoLocationData.Level2List();
            cboBarangay.DataSource = geoLocationData.Level3List();
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
                dataGridView1.Rows[selectedRowIndex].Cells[columnIndexLevel1].Value as string;
            txtMunicipality.Text =
                dataGridView1.Rows[selectedRowIndex].Cells[columnIndexLevel2].Value as string;
            txtBarangay.Text =
                dataGridView1.Rows[selectedRowIndex].Cells[columnIndexLevel3].Value as string;
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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                txtSelectedIndex.Text = selectedRowIndex.ToString();
                DisplaySelectedRecord();
                DisplayFuzzyMatches();               
            }
        }

        private void FormManualMatch_Shown(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Cursor = Cursors.WaitCursor;
                dataGridView1.Rows[0].Selected = true;
                DisplaySelectedRecord();
                DisplayFuzzyMatches();
                Cursor = Cursors.Default;
            }
        }

        private void btnUseOriginal_Click(object sender, EventArgs e)
        {
            string province = txtProvince.Text;
            string municipality = txtMunicipality.Text;
            string barangay = txtBarangay.Text;
            UpdateRow(province, municipality, barangay);
        }

        private void UpdateRow(string province, string municipality, string barangay)
        {
            //display new text
            dataGridView1.Rows[selectedRowIndex].Cells[columnIndexLevel1].Value = province;
            dataGridView1.Rows[selectedRowIndex].Cells[columnIndexLevel2].Value = municipality;
            dataGridView1.Rows[selectedRowIndex].Cells[columnIndexLevel3].Value = barangay;
          
            //get codes
            Location location = new Location(province,barangay,municipality);
            geoLocationData.GetLocationCodes(location);

            //display codes
            dataGridView1.Rows[selectedRowIndex].Cells[ProvinceCodeColumn].Value = location.ProvinceCode;
            dataGridView1.Rows[selectedRowIndex].Cells[MunicipalityCodeColumn].Value = location.MunicipalityCode;
            dataGridView1.Rows[selectedRowIndex].Cells[BaracayCodeColumn].Value = location.BarangayCode;

            DisplayUnmatchedRecords();

        }

        private void btnUseManual_Click(object sender, EventArgs e)
        {
            string province = cboProvince.SelectedValue.ToString();
            string municipality = cboMunicipality.SelectedValue.ToString();
            string barangay = cboBarangay.SelectedValue.ToString();
            UpdateRow(province, municipality, barangay);
        }

        private void btnUseSuggestion_Click(object sender, EventArgs e)
        {
            string province = cboProvinceSuggestion.SelectedValue.ToString();
            string municipality = cboMunicipalitySuggestion.SelectedValue.ToString();
            string barangay = cboBarangaySuggestion.SelectedValue.ToString();
            UpdateRow(province, municipality, barangay);
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



        //private void AddMatchedColumn()
        //{
        //    if (!dt.Columns.Contains(MatchedColumn))
        //    {
        //        DataGridViewCheckBoxColumn matched = new DataGridViewCheckBoxColumn();
        //        matched.HeaderText = MatchedColumn;
        //        matched.Name = MatchedColumn;
        //        dataGridView1.Columns.Add(matched);
        //    }
        //}

        //private void UpdateMatchedColumn()
        //{
        //    foreach (DataGridViewRow row in dataGridView1.Rows)
        //    {
        //        if (HasValue(row, ProvinceCodeColumn) &&
        //            HasValue(row, MunicipalityCodeColumn) &&
        //            HasValue(row, BaracayCodeColumn))
        //        {
        //            row.Cells[MatchedColumn].Value = true;
        //        }
        //        else
        //        {
        //            row.Cells[MatchedColumn].Value = false;
        //        }
        //    }
        //}
    }
}