﻿// FormManualMatch.cs

namespace GeoLocationTool.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        private readonly IGeoCoder geoCoder;
        private readonly INearMatchesProvider nearMatches;

        private int selectedRowIndex;

        #endregion Fields

        #region Constructors

        public FormManualMatch(IGeoCoder geoCoder)
        {
            InitializeComponent();
            this.geoCoder = geoCoder;
            fuzzyMatch = geoCoder.FuzzyMatcher();
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

        private void AddAltNames(
            string level1,
            string level2,
            string level3,
            string originalLevel1,
            string originalLevel2,
            string originalLevel3)
        {
            ClearAltNames();
            //display alt names only if different to the original
            if (!string.Equals(originalLevel1, level1, StringComparison.OrdinalIgnoreCase))
            {
                dataGridView1.Rows[selectedRowIndex].Cells[InputData.Used1ColumnName]
                    .Value
                    =
                    level1;
            }

            if (
                !string.Equals(
                    originalLevel2,
                    level2,
                    StringComparison.OrdinalIgnoreCase))
            {
                dataGridView1.Rows[selectedRowIndex].Cells[InputData.Used2ColumnName]
                    .Value
                    =
                    level2;
            }

            if (
                !string.Equals(
                    originalLevel3,
                    level3,
                    StringComparison.OrdinalIgnoreCase))
            {
                dataGridView1.Rows[selectedRowIndex].Cells[InputData.Used3ColumnName]
                    .Value
                    =
                    level3;
            }
        }

        private void AddCodes(CodedLocation codedLocation)
        {
            dataGridView1.Rows[selectedRowIndex].Cells[InputData.Level1CodeColumnName]
                .Value =
                codedLocation.GeoCode1.Code;
            dataGridView1.Rows[selectedRowIndex].Cells[InputData.Level2CodeColumnName]
                .Value =
                codedLocation.GeoCode2.Code;
            dataGridView1.Rows[selectedRowIndex].Cells[InputData.Level3CodeColumnName]
                .Value =
                codedLocation.GeoCode3.Code;
        }

        private void AddUsedNames(CodedLocation codedLocation)
        {
            ClearAltNames();
            //display alt names only if different to the original
            if (!string.Equals(
                codedLocation.GeoCode1.Name,
                codedLocation.Name1,
                StringComparison.OrdinalIgnoreCase))
            {
                dataGridView1.Rows[selectedRowIndex].Cells[InputData.Used1ColumnName]
                    .Value
                    = codedLocation.GeoCode1.Name;
            }

            if (
                !string.Equals(
                    codedLocation.GeoCode2.Name,
                    codedLocation.Name2,
                    StringComparison.OrdinalIgnoreCase))
            {
                dataGridView1.Rows[selectedRowIndex].Cells[InputData.Used2ColumnName]
                    .Value
                    = codedLocation.GeoCode2.Name;
            }

            if (
                !string.Equals(
                    codedLocation.GeoCode3.Name,
                    codedLocation.Name3,
                    StringComparison.OrdinalIgnoreCase))
            {
                dataGridView1.Rows[selectedRowIndex].Cells[InputData.Used3ColumnName]
                    .Value
                    = codedLocation.GeoCode3.Name;
            }
        }

        private void btnMainScreen_Click(object sender, EventArgs e)
        {
            Close();
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

        private void ClearAltNames()
        {
            dataGridView1.Rows[selectedRowIndex].Cells[InputData.Used1ColumnName].Value =
                null;
            dataGridView1.Rows[selectedRowIndex].Cells[InputData.Used2ColumnName].Value =
                null;
            dataGridView1.Rows[selectedRowIndex].Cells[InputData.Used3ColumnName].Value =
                null;
        }

        private IEnumerable<FuzzyMatchResult> ConcatWithDistinct(
            IEnumerable<FuzzyMatchResult> a,
            IEnumerable<FuzzyMatchResult> b)
        {
            var set = new HashSet<string>(a.Select(x => x.Location.ToLower()));
            var list = a.ToList();
            foreach (var fuzzyMatch in b)
            {
                if (!set.Contains(fuzzyMatch.Location.ToLower()))
                {
                    list.Add(fuzzyMatch);
                }
            }
            return list;
        }

        private void dataGridView1_RowsAdded(
            object sender,
            DataGridViewRowsAddedEventArgs e)
        {
            txtRowCount.Text = dataGridView1.RowCount.ToString();
        }

        private void dataGridView1_RowsRemoved(
            object sender,
            DataGridViewRowsRemovedEventArgs e)
        {
            txtRowCount.Text = dataGridView1.RowCount.ToString();
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
            cboBarangay.DataSource = geoCoder.Level3LocationNames(
                cboProvince.SelectedValue.ToString(),
                cboMunicipality.SelectedValue.ToString());
        }

        private void DisplayBarangaySuggestions()
        {
            //based on the suggested level 1 and 2 and the original level3
            string level1 = cboProvinceSuggestion.SelectedValue.ToString();
            string level2 = cboMunicipalitySuggestion.SelectedValue.ToString();
            string level3 = txtBarangay.Text.Trim();
            var barangayNearMatches =
                nearMatches.GetActualMatches(level3, level1, level2)
                    .Select(x => new FuzzyMatchResult(x.Level3, x.Weight));

            cboBarangaySuggestion.DisplayMember = "DisplayText";
            // todo:  after testing don't display the coeficient
            cboBarangaySuggestion.ValueMember = "Location";

            cboBarangaySuggestion.DataSource =
                ConcatWithDistinct(
                    barangayNearMatches,
                    fuzzyMatch.GetLevel3Suggestions(level1, level2, level3)).ToList();
        }

        private void DisplayMunicipalityList()
        {
            // based on selected level 1
            cboMunicipality.DataSource = geoCoder.Level2LocationNames(
                cboProvince.SelectedValue.ToString());
        }

        private void DisplayMunicipalitySuggestions()
        {
            //based on the suggested level 1 and the original level2
            string level1 = cboProvinceSuggestion.SelectedValue.ToString();
            string level2 = txtMunicipality.Text.Trim();
            var municipalityNearMatches =
                nearMatches.GetActualMatches(level2, level1)
                    .Select(x => new FuzzyMatchResult(x.Level2, x.Weight));

            cboMunicipalitySuggestion.DisplayMember = "DisplayText";
            // todo:  after testing don't display the coeficient
            cboMunicipalitySuggestion.ValueMember = "Location";

            cboMunicipalitySuggestion.DataSource =
                ConcatWithDistinct(
                    municipalityNearMatches,
                    fuzzyMatch.GetLevel2Suggestions(level1, level2)).ToList();
        }

        private void DisplayProvinceList()
        {
            cboProvince.DataSource = geoCoder.Level1LocationNames();
        }

        private void DisplayProvinceSuggestions()
        {
            string level1 = txtProvince.Text;
            var provinceNearMatches =
                nearMatches.GetActualMatches(level1)
                    .Select(x => new FuzzyMatchResult(x.Level1, x.Weight));
            cboProvinceSuggestion.DisplayMember = "DisplayText";
            // todo:  after testing don't display the coeficient
            cboProvinceSuggestion.ValueMember = "Location";

            cboProvinceSuggestion.DataSource =
                ConcatWithDistinct(
                    provinceNearMatches,
                    fuzzyMatch.GetLevel1Suggestions(level1))
                    .ToList();
        }

        private void DisplaySelectedRecord()
        {
            InputColumnNames columnNames = geoCoder.InputColumnNames();
            txtProvince.Text =
                dataGridView1.Rows[selectedRowIndex].Cells[
                    columnNames.Level1]
                    .Value as
                    string;
            txtMunicipality.Text =
                dataGridView1.Rows[selectedRowIndex].Cells[
                    columnNames.Level2]
                    .Value as
                    string;
            txtBarangay.Text =
                dataGridView1.Rows[selectedRowIndex].Cells[
                    columnNames.Level3]
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
                SetDefaults();
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
            // todo call via the geoCoder
            nearMatches.SaveMatch(txtProvince.Text, province);
            nearMatches.SaveMatch(txtMunicipality.Text, province, municipality);
            nearMatches.SaveMatch(txtBarangay.Text, province, municipality, barangay);

            // geoCoder.SaveNearMatch(nearMatch);
        }

        private void SetDefaults()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.ReadOnly = true;
            btnUseSuggestion.Select();
        }

        private void UpdateRow(string level1, string level2, string level3)
        {
            string originalLevel1 = txtProvince.Text;
            string originalLevel2 = txtMunicipality.Text;
            string originalLevel3 = txtBarangay.Text;

            Location location = new Location(
                level1,
                level2,
                level3);

            Location location2 = new Location(
                originalLevel1,
                originalLevel2,
                originalLevel3);

            //DisplayAltNames(
            //    level1,
            //    level2,
            //    level3,
            //    originalLevel1,
            //    originalLevel2,
            //    originalLevel3);

            CodedLocation codedLocation = geoCoder.GetGeoCodes(location2);

            AddCodes(codedLocation);
            AddUsedNames(codedLocation);
            DisplayUnmatchedRecords();
        }

        #endregion Methods
    }
}