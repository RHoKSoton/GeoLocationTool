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
        private readonly INearMatchesProvider matches;

        private int selectedRowIndex;

        #endregion Fields

        #region Constructors

        public FormManualMatch(IGeoCoder geoCoder)
        {
            InitializeComponent();
            this.geoCoder = geoCoder;
            fuzzyMatch = geoCoder.FuzzyMatcher();
            matches = new NearMatchesProvider(Program.Connection);
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
            ClearUsedNames();
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

        private void btnApplyAll_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                geoCoder.MatchAll();
            }
            catch (Exception ex)
            {
                ErrorHandler.Process(
                    "Error refreshing the data.",
                    ex);
            }
            finally
            {
                Cursor = Cursors.Default;
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
                string level1 = cboLevel1Manual.SelectedValue.ToString();
                string level2 = cboLevel2Manual.SelectedValue.ToString();
                string level3 = cboLevel3Manual.SelectedValue.ToString();
                SaveNearMatch(level1, level2, level3);
                UpdateRow(level1, level2, level3);
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
                string level1 = txtLevel1Original.Text;
                string level2 = txtLevel2Original.Text;
                string level3 = txtLevel3Original.Text;
                UpdateRow(level1, level2, level3);
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
                string level1 = cboLevel1Suggestion.SelectedValue.ToString();
                string level2 = cboLevel2Suggestion.SelectedValue.ToString();
                string level3 = cboLevel3Suggestion.SelectedValue.ToString();
                SaveNearMatch(level1, level2, level3);
                UpdateRow(level1, level2, level3);
            }
            catch (Exception ex)
            {
                ErrorHandler.Process(
                    "Error applying suggested selection to the data.",
                    ex);
            }
        }

        private void cboLevel1Manual_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DisplayLevel2List();
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Error displaying level 2 manual list.", ex);
            }
        }

        private void cboLevel1Suggestion_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            try
            {
                DisplayLevel2Suggestions();
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Error displaying level 2 suggestion list.", ex);
            }
        }

        private void cboLevel2Manual_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DisplayLevel3List();
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Error displaying level 3 manual list.", ex);
            }
        }

        private void cboLevel2Suggestion_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            try
            {
                DisplayLevel3Suggestions();
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Error displaying level 3 suggestion list.", ex);
            }
        }

        private void ClearUsedNames()
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
                    DisplayLevel1Suggestions();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Grid navigation error.", ex);
            }
        }

        private void DisplayLevel1List()
        {
            cboLevel1Manual.DataSource = geoCoder.Level1LocationNames();
        }

        private void DisplayLevel1Suggestions()
        {
            string level1 = txtLevel1Original.Text;
            var provinceNearMatches =
                matches.GetActualMatches(level1)
                    .Select(x => new FuzzyMatchResult(x.Level1, x.Weight));
            cboLevel1Suggestion.DisplayMember = "DisplayText";
            // todo:  after testing don't display the coeficient
            cboLevel1Suggestion.ValueMember = "Location";

            cboLevel1Suggestion.DataSource =
                ConcatWithDistinct(
                    provinceNearMatches,
                    fuzzyMatch.GetLevel1Suggestions(level1))
                    .ToList();
        }

        private void DisplayLevel2List()
        {
            // based on selected level 1
            cboLevel2Manual.DataSource = geoCoder.Level2LocationNames(
                cboLevel1Manual.SelectedValue.ToString());
        }

        private void DisplayLevel2Suggestions()
        {
            //based on the suggested level 1 and the original level2
            string level1 = cboLevel1Suggestion.SelectedValue.ToString();
            string level2 = txtLevel2Original.Text.Trim();
            var municipalityNearMatches =
                matches.GetActualMatches(level2, level1)
                    .Select(x => new FuzzyMatchResult(x.Level2, x.Weight));

            cboLevel2Suggestion.DisplayMember = "DisplayText";
            // todo:  after testing don't display the coeficient
            cboLevel2Suggestion.ValueMember = "Location";

            cboLevel2Suggestion.DataSource =
                ConcatWithDistinct(
                    municipalityNearMatches,
                    fuzzyMatch.GetLevel2Suggestions(level1, level2)).ToList();
        }

        private void DisplayLevel3List()
        {
            // based on selected level 1 and 2
            cboLevel3Manual.DataSource = geoCoder.Level3LocationNames(
                cboLevel1Manual.SelectedValue.ToString(),
                cboLevel2Manual.SelectedValue.ToString());
        }

        private void DisplayLevel3Suggestions()
        {
            //based on the suggested level 1 and 2 and the original level3
            string level1 = cboLevel1Suggestion.SelectedValue.ToString();
            string level2 = cboLevel2Suggestion.SelectedValue.ToString();
            string level3 = txtLevel3Original.Text.Trim();
            var level3Matches =
                matches.GetActualMatches(level3, level1, level2)
                    .Select(x => new FuzzyMatchResult(x.Level3, x.Weight));

            cboLevel3Suggestion.DisplayMember = "DisplayText";
            // todo:  after testing don't display the coeficient
            cboLevel3Suggestion.ValueMember = "Location";

            cboLevel3Suggestion.DataSource =
                ConcatWithDistinct(
                    level3Matches,
                    fuzzyMatch.GetLevel3Suggestions(level1, level2, level3)).ToList();
        }

        private void DisplaySelectedRecord()
        {
            InputColumnNames columnNames = geoCoder.InputColumnNames();
            txtLevel1Original.Text =
                dataGridView1.Rows[selectedRowIndex].Cells[
                    columnNames.Level1]
                    .Value as
                    string;
            txtLevel2Original.Text =
                dataGridView1.Rows[selectedRowIndex].Cells[
                    columnNames.Level2]
                    .Value as
                    string;
            txtLevel3Original.Text =
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
                DisplayLevel1List();
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
                    DisplayLevel1Suggestions();
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

        private void SaveNearMatch(string level1, string level2, string level3)
        {
            // todo call via the geoCoder
            matches.SaveMatch(txtLevel1Original.Text, level1);
            matches.SaveMatch(txtLevel2Original.Text, level1, level2);
            matches.SaveMatch(txtLevel3Original.Text, level1, level2, level3);

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
            string originalLevel1 = txtLevel1Original.Text;
            string originalLevel2 = txtLevel2Original.Text;
            string originalLevel3 = txtLevel3Original.Text;

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