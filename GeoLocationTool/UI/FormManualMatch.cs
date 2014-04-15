﻿// FormManualMatch.cs

namespace GeoLocationTool.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using MultiLevelGeoCoder;
    using MultiLevelGeoCoder.Logic;

    /// <summary>
    /// Form to enable the manual matching/selection of fuzzy match suggestions
    /// </summary>
    public partial class FormManualMatch : Form
    {
        #region Fields

        private readonly FuzzyMatch fuzzyMatch;
        private readonly IGeoCoder geoCoder;
        private bool matchInProgress;
        private int selectedRowIndex;

        #endregion Fields

        #region Constructors

        public FormManualMatch(IGeoCoder geoCoder)
        {
            InitializeComponent();
            this.geoCoder = geoCoder;
            fuzzyMatch = geoCoder.FuzzyMatch();
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
            // add the codes to the input data
            InputColumnNames columnNames = geoCoder.CodeColumnNames();
            if (codedLocation.GeoCode1 != null)
            {
                dataGridView1.Rows[selectedRowIndex].Cells[columnNames.Level1]
                    .Value =
                    codedLocation.GeoCode1.Code;
            }

            if (codedLocation.GeoCode2 != null)
            {
                dataGridView1.Rows[selectedRowIndex].Cells[columnNames.Level2]
                    .Value =
                    codedLocation.GeoCode2.Code;
            }

            if (codedLocation.GeoCode3 != null)
            {
                dataGridView1.Rows[selectedRowIndex].Cells[columnNames.Level3]
                    .Value =
                    codedLocation.GeoCode3.Code;
            }

            // add the names used to generate those codes as information for the user.
            AddUsedMatchNames(codedLocation);
        }

        private void AddUsedMatchNames(CodedLocation codedLocation)
        {
            InputColumnNames columnNames = geoCoder.MatchColumnNames();
            // add the actual name used to get the code if different to that on the input
            if (codedLocation.IsName1Different())
            {
                dataGridView1.Rows[selectedRowIndex].Cells[columnNames.Level1]
                    .Value
                    = codedLocation.GeoCode1.Name;
            }

            if (codedLocation.IsName2Different())
            {
                dataGridView1.Rows[selectedRowIndex].Cells[columnNames.Level2]
                    .Value
                    = codedLocation.GeoCode2.Name;
            }

            if (codedLocation.IsName3Different())
            {
                dataGridView1.Rows[selectedRowIndex].Cells[columnNames.Level3]
                    .Value
                    = codedLocation.GeoCode3.Name;
            }
        }

        private void btnApplyAll_Click(object sender, EventArgs e)
        {
            try
            {
                matchInProgress = true;
                Cursor = Cursors.WaitCursor;
                geoCoder.CodeAll();
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
                matchInProgress = false;
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[0].Selected = true;
                }
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
                if (dataGridView1.RowCount > 0)
                {
                    string level1 = cboLevel1Manual.SelectedValue as string;
                    string level2 = cboLevel2Manual.SelectedValue as string;
                    string level3 = cboLevel3Manual.SelectedValue as string;
                    SaveSelectedMatch(level1, level2, level3);
                    UpdateRow();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Error applying manual selection to the data.", ex);
            }
        }

        private void btnUseSuggestion_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.RowCount > 0)
                {
                    string level1 = cboLevel1Suggestion.SelectedValue as string;
                    string level2 = cboLevel2Suggestion.SelectedValue as string;
                    string level3 = cboLevel3Suggestion.SelectedValue as string;
                    SaveSelectedMatch(level1, level2, level3);
                    UpdateRow();
                }
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

        private void chkUnmatchedOnly_CheckedChanged(object sender, EventArgs e)
        {
            DisplayRecords();
        }

        private void ClearCodes()
        {
            InputColumnNames columnNames = geoCoder.CodeColumnNames();
            dataGridView1.Rows[selectedRowIndex].Cells[columnNames.Level1]
                .Value = null;
            dataGridView1.Rows[selectedRowIndex].Cells[columnNames.Level2]
                .Value = null;
            dataGridView1.Rows[selectedRowIndex].Cells[columnNames.Level3]
                .Value = null;
        }

        private void ClearExistingCodes()
        {
            ClearCodes();
            ClearUsedNames();
        }

        private void ClearUsedNames()
        {
            InputColumnNames columnNames = geoCoder.MatchColumnNames();
            dataGridView1.Rows[selectedRowIndex].Cells[columnNames.Level1].Value =
                null;
            dataGridView1.Rows[selectedRowIndex].Cells[columnNames.Level2].Value =
                null;
            dataGridView1.Rows[selectedRowIndex].Cells[columnNames.Level3].Value =
                null;
        }

        private IEnumerable<MatchResult> ConcatWithDistinct(
            IEnumerable<MatchResult> a,
            IEnumerable<MatchResult> b)
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
                if (!matchInProgress && dataGridView1.SelectedRows.Count > 0)
                {
                    selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                    txtSelectedIndex.Text = selectedRowIndex.ToString();
                    DisplaySelectedRecord();
                    DisplayLevel1Suggestions();
                    DisplayLevel1List();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Grid navigation error.", ex);
            }
        }

        private void DisplayAllRecords()
        {
            dataGridView1.DataSource = geoCoder.InputData;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void DisplayLevel1List()
        {
            if (string.IsNullOrEmpty(Level1Text()))
            {
                cboLevel1Manual.DataSource = null;
            }
            else
            {
                cboLevel1Manual.DataSource = geoCoder.Level1LocationNames();
            }
        }

        private string Level1Text()
        {
            return txtLevel1Original.Text.Trim();
        }

        private void DisplayLevel1Suggestions()
        {
            if (string.IsNullOrEmpty(Level1Text()))
            {
                // empty list
                cboLevel1Suggestion.DataSource = null;
            }
            else
            {
                // get any saved matched name
                IEnumerable<MatchResult> savedMatch = geoCoder.GetSavedMatchLevel1(
                    Level1Text());

                // add it to the top of the list
                cboLevel1Suggestion.DisplayMember = "DisplayText";
                // todo:  after testing don't display the coeficient
                cboLevel1Suggestion.ValueMember = "Location";

                cboLevel1Suggestion.DataSource =
                    ConcatWithDistinct(
                        savedMatch,
                        fuzzyMatch.GetLevel1Suggestions(Level1Text()))
                        .ToList();
            }
        }

        private void DisplayLevel2List()
        {
            if (string.IsNullOrEmpty(Level2Text()))
            {
                // empty list
                cboLevel2Manual.DataSource = null;
                cboLevel3Manual.DataSource = null;
            }
            else
            {
                // display lists based on selected level 1
                if (cboLevel1Manual.SelectedValue != null)
                {
                    cboLevel2Manual.DataSource = geoCoder.Level2LocationNames(
                        cboLevel1Manual.SelectedValue.ToString());
                }
            }
        }

        private string Level2Text()
        {
            return txtLevel2Original.Text.Trim();
        }

        private void DisplayLevel2Suggestions()
        {
            if (string.IsNullOrEmpty(Level2Text()))
            {
                // empty list
                cboLevel2Suggestion.DataSource = null;
                cboLevel3Suggestion.DataSource = null;
            }
            else
            {
                //based on the suggested level 1 and the original level2
                if (cboLevel1Suggestion.SelectedValue != null)
                {
                    string level1 = cboLevel1Suggestion.SelectedValue.ToString();

                    // get any saved matched name
                    IEnumerable<MatchResult> savedMatch =
                        geoCoder.GetSavedMatchLevel2(Level2Text(), level1);

                    // Add it to the top of the suggestions list
                    cboLevel2Suggestion.DataSource =
                        ConcatWithDistinct(
                            savedMatch,
                            fuzzyMatch.GetLevel2Suggestions(level1, Level2Text())).ToList();

                    cboLevel2Suggestion.DisplayMember = "DisplayText";
                    // todo:  after testing don't display the coeficient
                    cboLevel2Suggestion.ValueMember = "Location";
                }
            }
        }

        private void DisplayLevel3List()
        {
            if (string.IsNullOrEmpty(Level3Text()))
            {
                // empty list
                cboLevel3Manual.DataSource = null;
            }
            else
            {
                if (cboLevel1Manual.SelectedValue != null &&
                    cboLevel2Manual.SelectedValue != null)
                {
                    // display listbased on selected level 1 and 2
                    cboLevel3Manual.DataSource = geoCoder.Level3LocationNames(
                        cboLevel1Manual.SelectedValue.ToString(),
                        cboLevel2Manual.SelectedValue.ToString());
                }
            }
        }

        private void DisplayLevel3Suggestions()
        {
            if (string.IsNullOrEmpty(Level3Text()))
            {
                // empty list
                cboLevel3Suggestion.DataSource = null;
            }
            else
            {
                if (cboLevel1Suggestion.SelectedValue != null &&
                    cboLevel2Suggestion.SelectedValue != null)
                {
                    // display list based on the suggested level 1 and 2 and the original level3
                    string level1 = cboLevel1Suggestion.SelectedValue.ToString();
                    string level2 = cboLevel2Suggestion.SelectedValue.ToString();
                    string level3 = txtLevel3Original.Text.Trim();

                    // get any saved matched name
                    IEnumerable<MatchResult> savedMatch =
                        geoCoder.GetSavedMatchLevel3(level3, level1, level2);

                    // Add it to the top of the suggestions list
                    cboLevel3Suggestion.DataSource =
                        ConcatWithDistinct(
                            savedMatch,
                            fuzzyMatch.GetLevel3Suggestions(level1, level2, level3))
                            .ToList();

                    cboLevel3Suggestion.DisplayMember = "DisplayText";
                    // todo:  after testing don't display the coeficient
                    cboLevel3Suggestion.ValueMember = "Location";
                }
            }
        }

        private string Level3Text()
        {
            return txtLevel3Original.Text.Trim();
        }

        private void DisplayRecords()
        {
            if (chkUnmatchedOnly.Checked)
            {
                DisplayUnmatchedRecords();
            }
            else
            {
                DisplayAllRecords();
            }
        }

        private void DisplaySelectedRecord()
        {
            InputColumnNames columnNames = geoCoder.InputColumnNames();
            txtLevel1Original.Text =
                dataGridView1.Rows[selectedRowIndex].Cells[
                    columnNames.Level1]
                    .Value as
                    string;

            // level 2 is optional
            if (!string.IsNullOrEmpty(columnNames.Level2))
            {
                txtLevel2Original.Text =
                    dataGridView1.Rows[selectedRowIndex].Cells[
                        columnNames.Level2]
                        .Value as
                        string;
            }

            // level 3 is optional
            if (!string.IsNullOrEmpty(columnNames.Level3))
            {
                txtLevel3Original.Text =
                    dataGridView1.Rows[selectedRowIndex].Cells[
                        columnNames.Level3]
                        .Value as
                        string;
            }
        }

        private void DisplayUnmatchedRecords()
        {
            dataGridView1.DataSource = geoCoder.UncodedRecords();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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

                    // select the first row and display the suggestions for it
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

        private void SaveOutputFile()
        {
            geoCoder.SaveOutputFile();
        }

        private void SaveSelectedMatch(string level1, string level2, string level3)
        {
            var originalLevel1 = txtLevel1Original.Text;
            var originalLevel2 = txtLevel2Original.Text;
            var originalLevel3 = txtLevel3Original.Text;

            Location inputLocation = new Location(
                originalLevel1,
                originalLevel2,
                originalLevel3);
            Location gazetteerLocation = new Location(level1, level2, level3);

            geoCoder.SaveMatch(inputLocation, gazetteerLocation);
        }

        private void SetDefaults()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.ReadOnly = true;
            chkUnmatchedOnly.Checked = true;
            btnUseSuggestion.Select();
        }

        private void UpdateRow()
        {
            string originalLevel1 = txtLevel1Original.Text;
            string originalLevel2 = txtLevel2Original.Text;
            string originalLevel3 = txtLevel3Original.Text;

            Location location2 = new Location(
                originalLevel1,
                originalLevel2,
                originalLevel3);

            CodedLocation codedLocation = geoCoder.GetCodes(location2);
            ClearExistingCodes();
            AddCodes(codedLocation);
            DisplayRecords();
            SaveOutputFile();
        }

        #endregion Methods
    }
}