// FormLoadLocationData.cs

namespace GeoLocationTool.UI
{
    using System;
    using System.Windows.Forms;
    using MultiLevelGeoCoder;
    using MultiLevelGeoCoder.DataAccess;
    using MultiLevelGeoCoder.Logic;
    using MultiLevelGeoCoder.Model;

    /// <summary>
    /// Form to display the  gazetter location data and enable the user to 
    /// select the relevant columns
    /// </summary>
    public partial class FormLoadLocationData : Form
    {
        #region Fields

        private readonly IColumnsMappingProvider columnsMapping;

        private GazetteerData gazetteerData;

        #endregion Fields

        #region Constructors

        public FormLoadLocationData(string[] args = null)
        {
            InitializeComponent();
            columnsMapping = new ColumnsMappingProvider(Program.Connection);
            if (args != null && args.Length >= 1)
            {
                string path = args[0];
                txtLocationFileName.Text = path;
                gazetteerData = GeoCoder.GetGazetteerFile(path);
                dataGridView1.DataSource = gazetteerData.Data;
                FormatGrid();

                DisplaySavedColumnHeaderIndices(path);
            }
        }

        #endregion Constructors

        #region Methods

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                const string filter = "csv files (*.csv)|*.csv";
                txtLocationFileName.Clear();
                txtLocationFileName.Text = UiHelper.GetFileName(filter);
                var path = txtLocationFileName.Text.Trim();
                if (!String.IsNullOrWhiteSpace(path))
                {
                    gazetteerData = GeoCoder.GetGazetteerFile(path);
                    dataGridView1.DataSource = gazetteerData.Data;
                    FormatGrid();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Could not read file.", ex);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (gazetteerData == null)
                {
                    UiHelper.DisplayMessage(
                        "Please load the gazetteer data.",
                        "Missing Data");
                }
                else
                {
                    SetColumnIndices();

                    if (gazetteerData.ColumnIndicesValid())
                    {
                        SaveColumnMappings();

                        LocationCodes gazetteer =
                            new LocationCodes(gazetteerData.LocationList);

                        // Load next screen
                        FormLoadData formLoadData = new FormLoadData(gazetteer);
                        formLoadData.Show();
                    }
                    else
                    {
                        UiHelper.DisplayMessage(
                            "Please select column numbers for the Code and Name columns.",
                            "Missing Data");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Error loading input form", ex);
            }
        }

        private void DisplaySavedColumnHeaderIndices(string path)
        {
            var locationColumnMapping = columnsMapping.GetLocationColumnsMapping(path);
            if (locationColumnMapping != null)
            {
                udCode1.Value = locationColumnMapping.Location1Code + 1;
                udName1.Value = locationColumnMapping.Location1Name + 1;
                udAltName1.Value = locationColumnMapping.Location1AltName + 1;
                udCode2.Value = locationColumnMapping.Location2Code + 1;
                udName2.Value = locationColumnMapping.Location2Name + 1;
                udAltName2.Value = locationColumnMapping.Location2AltName + 1;
                udCode3.Value = locationColumnMapping.Location3Code + 1;
                udName3.Value = locationColumnMapping.Location3Name + 1;
                udAltName3.Value = locationColumnMapping.Location3AltName + 1;
            }
        }

        private void FormatGrid()
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderText = string.Format(
                    "{0} [{1}]",
                    column.HeaderText,
                    column.Index + 1);
                column.MinimumWidth = column.HeaderText.Length;
            }
            dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void SaveColumnMappings()
        {
            // todo move  code away from the UI
            int loc1Code = (int) udCode1.Value - 1;
            int loc1Name = (int) udName1.Value - 1;
            int loc1AltName = (int) udAltName1.Value - 1;
            int loc2Code = (int) udCode2.Value - 1;
            int loc2Name = (int) udName2.Value - 1;
            int loc2AltName = (int) udAltName2.Value - 1;
            int loc3Code = (int) udCode3.Value - 1;
            int loc3Name = (int) udName3.Value - 1;
            int loc3AltName = (int) udAltName3.Value - 1;

            columnsMapping.SaveLocationColumnsMapping(
                new LocationColumnsMapping
                {
                    FileName = txtLocationFileName.Text,
                    Location1Code = loc1Code,
                    Location1Name = loc1Name,
                    Location1AltName = loc1AltName,
                    Location2Code = loc2Code,
                    Location2Name = loc2Name,
                    Location2AltName = loc2AltName,
                    Location3Code = loc3Code,
                    Location3Name = loc3Name,
                    Location3AltName = loc3AltName,
                }
                );
        }

        private void SetColumnIndices()
        {
            gazetteerData.Admin1Code = (int) udCode1.Value - 1;
            gazetteerData.Admin1Name = (int) udName1.Value - 1;
            gazetteerData.Admin1AltName = (int) udAltName1.Value - 1;

            gazetteerData.Admin2Code = (int) udCode2.Value - 1;
            gazetteerData.Admin2Name = (int) udName2.Value - 1;
            gazetteerData.Admin2AltName = (int) udAltName2.Value - 1;

            gazetteerData.Admin3Code = (int) udCode3.Value - 1;
            gazetteerData.Admin3Name = (int) udName3.Value - 1;
            gazetteerData.Admin3AltName = (int) udAltName3.Value - 1;
        }

        #endregion Methods
    }
}