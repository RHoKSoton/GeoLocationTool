// FormLoadLocationData.cs

namespace GeoLocationTool.UI
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using DataAccess;
    using Logic;
    using GeoLocationTool.Model;

    /// <summary>
    /// Form to display the location data and enable the user to select the relevant columns
    /// </summary>
    public partial class FormLoadLocationData : Form
    {
        #region Fields

        private DataTable locationDataTable;
        private readonly IColumnsMappingProvider columnsMapping;

        #endregion Fields

        #region Constructors

        public FormLoadLocationData(string[] args = null)
        {
            InitializeComponent();
            columnsMapping = new ColumnsMappingProvider(Program.Connection);
            if (args != null && args.Length >= 1)
            {
                string locationPath = args[0];
                txtLocationFileName.Text = locationPath;
                locationDataTable = InputFile.ReadCsvFile(locationPath, true);
                dataGridView1.DataSource = locationDataTable;
                FormatGrid();
                
                var locationColumnMapping = columnsMapping.GetLocationColumnsMapping(locationPath);
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
                    const bool isFirstRowHeader = true;
                    locationDataTable =
                        InputFile.ReadCsvFile(path, isFirstRowHeader);
                    dataGridView1.DataSource = locationDataTable;
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
                if (locationDataTable == null)
                {
                    UiHelper.DisplayMessage("Please load the location data.", "Missing Data");
                }
                else if (ColumnIndicesValid())
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

                    // create a new list of LocationDataRecords
                    List<Gadm> locationCodeList = new List<Gadm>();
                    foreach (DataRow row in locationDataTable.Rows)
                    {
                        Gadm gadm = new Gadm();
                        gadm.NAME_1 = row[loc1Name].ToString();
                        gadm.ID_1 = row[loc1Code].ToString();
                        //gadm.VarName1 = row[loc1AltName].ToString();

                        gadm.NAME_2 = row[loc2Name].ToString();
                        gadm.ID_2 = row[loc2Code].ToString();
                        //gadm.VarName1 = row[loc1AltName].ToString();

                        gadm.NAME_3 = row[loc3Name].ToString();
                        gadm.ID_3 = row[loc3Code].ToString();
                        //gadm.VarName1 = row[loc1AltName].ToString();
                        locationCodeList.Add(gadm);
                    }

                    LocationData locationData =
                        new LocationData(locationCodeList);

                    // Load next screen
                    FormLoadData formLoadData = new FormLoadData(locationData);
                    formLoadData.Show();
                }
                else
                {
                    UiHelper.DisplayMessage(
                        "Please select column numbers for the Code and Name columns.",
                        "Missing Data");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Error loading input form", ex);
            }
        }

        private bool ColumnIndicesValid()
        {
            // code column indices must be greater than 0
            bool valid = (udCode1.Value > 0);
            valid = valid & (udCode1.Value > 0);
            valid = valid & (udCode2.Value > 0);
            valid = valid & (udCode3.Value > 0);

            // name column indices must be greater than 0
            valid = valid & (udName1.Value > 0);
            valid = valid & (udName2.Value > 0);
            valid = valid & (udName2.Value > 0);

            return valid;
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

        #endregion Methods
    }
}