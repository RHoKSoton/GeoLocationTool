// FormManualMatch.cs
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

        private const string LeaveBlankText = "No Match - Leave blank";

        private readonly IFuzzyMatch fuzzyMatch;
        private readonly IGeoCoder geoCoder;
        private readonly DataGridView parentGrid;

        private bool matchInProgress;
        private int selectedRowIndex;

        #endregion Fields

        #region Constructors

        public FormManualMatch(IGeoCoder geoCoder, DataGridView parentGrid)
        {
            InitializeComponent();
            this.geoCoder = geoCoder;
            this.parentGrid = parentGrid;
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
            InputColumnNames columnNames = geoCoder.LocationCodeColumnNames();
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
            SelectNextRow();
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
                Cursor = Cursors.WaitCursor;
                matchInProgress = true;

                // remember the current row
                int selectedIndex = selectedRowIndex;

                // disconnect the data grids until the coding is complete
                dataGridView1.DataSource = null;
                parentGrid.DataSource = null;

                geoCoder.AddAllLocationCodes();

                // todo refactor the disconnection and reconnection of the grids to make more robust
                // reconect the data grids
                DisplayRecords();
                parentGrid.DataSource = geoCoder.InputData;
                FormatGrid(parentGrid);

                // highlight the remembered row
                HighlightCurrentRow(selectedIndex);
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
                    string level1 = SelectedValue(cboLevel1Manual);
                    string level2 = SelectedValue(cboLevel2Manual);
                    string level3 = SelectedValue(cboLevel3Manual);
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
                    string level1 = SelectedValue(cboLevel1Suggestion);
                    string level2 = SelectedValue(cboLevel2Suggestion);
                    string level3 = SelectedValue(cboLevel3Suggestion);
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
            InputColumnNames columnNames = geoCoder.LocationCodeColumnNames();
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
            // remember the current row
            int selectedIndex = selectedRowIndex;
            dataGridView1.DataSource = geoCoder.InputData;
            FormatGrid(dataGridView1);

            // highlight the remembered row
            HighlightCurrentRow(selectedIndex);
        }

        private void DisplayLevel1List()
        {
            if (string.IsNullOrEmpty(Level1Original()))
            {
                cboLevel1Manual.DataSource = null;
            }
            else
            {
                cboLevel1Manual.DataSource = geoCoder.Level1LocationNames();
            }
        }

        private void DisplayLevel1Suggestions()
        {
            if (string.IsNullOrEmpty(Level1Original()))
            {
                // empty list
                cboLevel1Suggestion.DataSource = null;
            }
            else
            {
                // get any saved matched name
                IEnumerable<MatchResult> savedMatch = geoCoder.GetSavedMatchLevel1(
                    Level1Original());

                //get suggestions
                var suggestions = fuzzyMatch.Level1Suggestions(Level1Original());

                // format
                const bool addBlank = false;
                List<KeyValuePair<string, string>> suggestionList =
                    FormatSuggestionList(savedMatch, suggestions, addBlank);

                cboLevel1Suggestion.DataSource = suggestionList;
                cboLevel1Suggestion.DisplayMember = "Value";
                cboLevel1Suggestion.ValueMember = "Key";
            }
        }

        private void DisplayLevel2List()
        {
            if (string.IsNullOrEmpty(Level2Original()))
            {
                // display an empty list
                cboLevel2Manual.DataSource = null;
            }
            else
            {
                // display lists based on selected level 1
                var level1 = SelectedValue(cboLevel1Manual);
                if (level1 == null)
                {
                    // display an empty list
                    cboLevel2Manual.DataSource = null;
                }
                else
                {
                    var list = geoCoder.Level2LocationNames(
                        SelectedValue(cboLevel1Manual));

                    // add a leave blank option to the bottom of the list
                    list.Add(LeaveBlankText);

                    cboLevel2Manual.DataSource = list;
                }
            }
        }

        private void DisplayLevel2Suggestions()
        {
            if (string.IsNullOrEmpty(Level2Original()))
            {
                // display an empty list
                cboLevel2Suggestion.DataSource = null;
            }
            else
            {
                //based on the suggested level 1 and the original level2
                string level1 = SelectedValue(cboLevel1Suggestion);
                string level2 = Level2Original();

                if (level1 == null)
                {
                    // display an empty list
                    cboLevel2Suggestion.DataSource = null;
                }
                else
                {
                    // get any saved matched name
                    IEnumerable<MatchResult> savedMatch =
                        geoCoder.GetSavedMatchLevel2(level2, level1);

                    // get suggestions
                    List<MatchResult> suggestions = fuzzyMatch.Level2Suggestions(
                        level1,
                        level2);

                    // format
                    const bool addBlank = true;
                    List<KeyValuePair<string, string>> suggestionList =
                        FormatSuggestionList(savedMatch, suggestions, addBlank);

                    cboLevel2Suggestion.DataSource = suggestionList;
                    cboLevel2Suggestion.DisplayMember = "Value";
                    cboLevel2Suggestion.ValueMember = "Key";
                }
            }
        }

        private void DisplayLevel3List()
        {
            if (string.IsNullOrEmpty(Level3Original()))
            {
                // display an empty list
                cboLevel3Manual.DataSource = null;
            }
            else
            {
                // display list based on selected level 1 and 2
                string level1 = SelectedValue(cboLevel1Manual);
                string level2 = SelectedValue(cboLevel2Manual);

                if (level2 == null)
                {
                    // display an empty list
                    cboLevel3Manual.DataSource = null;
                }
                else
                {
                    cboLevel3Manual.DataSource = geoCoder.Level3LocationNames(
                        level1,
                        level2);
                }
            }
        }

        private void DisplayLevel3Suggestions()
        {
            if (string.IsNullOrEmpty(Level3Original()))
            {
                // display an empty list
                cboLevel3Suggestion.DataSource = null;
            }
            else
            {
                //based on the suggested level 1 and 2 and the original level3
                string level1 = SelectedValue(cboLevel1Suggestion);
                string level2 = SelectedValue(cboLevel2Suggestion);
                string level3 = Level3Original();

                if (level2 == null)
                {
                    // display an empty list
                    cboLevel3Suggestion.DataSource = null;
                }
                else
                {
                    // get any saved matched name
                    IEnumerable<MatchResult> savedMatch =
                        geoCoder.GetSavedMatchLevel3(level3, level1, level2);

                    // get suggestions
                    List<MatchResult> suggestions = fuzzyMatch.Level3Suggestions(
                        level1,
                        level2,
                        level3);

                    // format
                    const bool addBlank = true;
                    List<KeyValuePair<string, string>> suggestionList =
                        FormatSuggestionList(savedMatch, suggestions, addBlank);

                    cboLevel3Suggestion.DataSource = suggestionList;
                    cboLevel3Suggestion.DisplayMember = "Value";
                    cboLevel3Suggestion.ValueMember = "Key";
                }
            }
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
            InputColumnNames columnNames = geoCoder.InputLocationColumnNames();
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
            // remember the current row
            int selectedIndex = selectedRowIndex;
            dataGridView1.DataSource = geoCoder.UncodedRecords();
            FormatGrid(dataGridView1);

            // highlight the remembered row
            HighlightCurrentRow(selectedIndex);
        }

        private void FormatGrid(DataGridView grid)
        {
            grid.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.AllCells;
            DataGridViewColumnCollection columnCollection = grid.Columns;
            DataGridViewColumn lastVisibleColumn =
                columnCollection.GetLastColumn(
                    DataGridViewElementStates.Visible,
                    DataGridViewElementStates.None);
            if (lastVisibleColumn != null)
                lastVisibleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private List<KeyValuePair<string, string>> FormatSuggestionList(
            IEnumerable<MatchResult> savedMatch,
            IEnumerable<MatchResult> suggestions,
            bool addBlank)
        {
            // Add the saved match to the top of the suggestions list
            var list = ConcatWithDistinct(
                savedMatch,
                suggestions).ToList();

            // format the list
            List<KeyValuePair<string, string>> formattedList =
                list.Select(
                    x =>
                        new KeyValuePair<string, string>
                            (
                            x.Location,
                            string.Format("{0}:   {1:P2}", x.Location, x.Coefficient)
                            )).ToList();

            if (addBlank)
            {
                // add a leave blank option to the bottom of the list
                formattedList.Add(new KeyValuePair<string, string>(null, LeaveBlankText));
            }
            return formattedList;
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

        private void HighlightCurrentRow(int index)
        {
            if (dataGridView1.RowCount >0)
            {
                if (index > dataGridView1.RowCount - 1)
                {
                    // highlight last
                    index = dataGridView1.RowCount - 1;
                }

                if (index < 0)
                {
                    // highlight first
                    index = 0;
                }
                dataGridView1.CurrentCell = dataGridView1.Rows[index].Cells[0];
                dataGridView1.Rows[index].Selected = true;
            }
        }

        private string Level1Original()
        {
            return txtLevel1Original.Text.Trim();
        }

        private string Level2Original()
        {
            return txtLevel2Original.Text.Trim();
        }

        private string Level3Original()
        {
            return txtLevel3Original.Text.Trim();
        }

        private void SaveSelectedMatch(string level1, string level2, string level3)
        {
            Location inputLocation = new Location(
                Level1Original(),
                Level2Original(),
                Level3Original());

            Location gazetteerLocation = new Location(level1, level2, level3);

            geoCoder.SaveMatch(inputLocation, gazetteerLocation);
        }

        private string SelectedValue(ComboBox comboBox)
        {
            string level = null;
            if (comboBox.SelectedValue != null)
            {
                level = comboBox.SelectedValue.ToString().Trim();

                // set to null if leave blank
                if (string.Equals(
                    level,
                    LeaveBlankText,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    level = null;
                }
            }

            return level;
        }

        private void SelectNextRow()
        {
            // move to next row if all rows are displayed, if only unmatched are
            // showing then the current row is removed so we dont need to explicitly move to the next
            if (!chkUnmatchedOnly.Checked)
            {
                dataGridView1.Rows[selectedRowIndex + 1].Selected = true;
            }
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

            CodedLocation codedLocation = geoCoder.AddLocationCodes(location2);
            ClearExistingCodes();
            AddCodes(codedLocation);
            DisplayRecords();
        }

        #endregion Methods
    }
}