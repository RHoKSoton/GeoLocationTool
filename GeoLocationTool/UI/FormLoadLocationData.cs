// FormLoadLocationData.cs

namespace GeoLocationTool.UI
{
    using System;
    using System.ComponentModel;
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

        private FormLoadData formLoadData;
        private readonly IGeoCoder geoCoder = new GeoCoder();

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
                geoCoder.LoadGazetter(path);
                dataGridView1.DataSource = geoCoder.GazetteerData;
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
                    geoCoder.LoadGazetter(path);
                    dataGridView1.DataSource = geoCoder.GazetteerData;
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
                if (!geoCoder.IsGazetteerInitialised())
                {
                    UiHelper.DisplayMessage(
                        "Please load the gazetteer data.",
                        "Missing Data");
                }
                else
                {
                    SetColumnHeaders(); 
                    if (!ColumnHeadersSet())
                    {
                        UiHelper.DisplayMessage(
                            "Please select column numbers for the Code and Name columns.",
                            "Missing Data");
                    }
                    SaveColumnMappings();
                    LoadNextScreen();                   
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Error loading input form", ex);
            }
        }

        private bool ColumnHeadersSet()
        {
            const bool areSet = true;
            //todo check that collumns have been selected

            return areSet;
        }
     
        private void DisplaySavedColumnHeaderIndices(string path)
        {
            var locationColumnMapping = columnsMapping.GetLocationColumnsMapping(path);
            if (locationColumnMapping != null)
            {
                udCode1.Value = locationColumnMapping.Level1Code + 1;
                udName1.Value = locationColumnMapping.Level1Name + 1;
                udAltName1.Value = locationColumnMapping.Level1AltName + 1;
                udCode2.Value = locationColumnMapping.Level2Code + 1;
                udName2.Value = locationColumnMapping.Level2Name + 1;
                udAltName2.Value = locationColumnMapping.Level2AltName + 1;
                udCode3.Value = locationColumnMapping.Level3Code + 1;
                udName3.Value = locationColumnMapping.Level3Name + 1;
                udAltName3.Value = locationColumnMapping.Level3AltName + 1;
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

        private void FormLoadDataClosing(object sender, CancelEventArgs e)
        {
            // close this form too if the main form has closed
            Close();
        }

        private void LoadNextScreen()
        {
            if (formLoadData == null)
            {
                formLoadData = new FormLoadData(geoCoder);
                formLoadData.Closing += FormLoadDataClosing;
            }
                 
            formLoadData.Show(this);
            Hide();
        }

        private void SaveColumnMappings()
        {
            // todo move  code away from the UI, save via the geoCoder
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
                    Level1Code = loc1Code,
                    Level1Name = loc1Name,
                    Level1AltName = loc1AltName,
                    Level2Code = loc2Code,
                    Level2Name = loc2Name,
                    Level2AltName = loc2AltName,
                    Level3Code = loc3Code,
                    Level3Name = loc3Name,
                    Level3AltName = loc3AltName,
                }
                );
        }

        private void SetColumnHeaders()
        {
            // todo use column names instead of indices
            GazetteerColumnHeaders  headers = new GazetteerColumnHeaders();
            headers.Admin1Code = (int)udCode1.Value - 1;
            headers.Admin1Name = (int)udName1.Value - 1;
            headers.Admin1AltName = (int)udAltName1.Value - 1;

            headers.Admin2Code = (int)udCode2.Value - 1;
            headers.Admin2Name = (int)udName2.Value - 1;
            headers.Admin2AltName = (int)udAltName2.Value - 1;

            headers.Admin3Code = (int)udCode3.Value - 1;
            headers.Admin3Name = (int)udName3.Value - 1;
            headers.Admin3AltName = (int)udAltName3.Value - 1;
            geoCoder.SetGazetteerColumns(headers);
        
        }

        #endregion Methods
    }
}