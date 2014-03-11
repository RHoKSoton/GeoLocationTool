// FormLoadLocationData.cs

namespace GeoLocationTool.UI
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using MultiLevelGeoCoder;
    using MultiLevelGeoCoder.Logic;

    /// <summary>
    /// Form to display the  gazetter location data and enable the user to 
    /// select the relevant columns
    /// </summary>
    public partial class FormLoadLocationData : Form
    {
        #region Fields

        //private readonly IColumnsMappingProvider columnsMapping;
        private readonly IGeoCoder geoCoder = new GeoCoder(Program.Connection);

        private FormLoadData formLoadData;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FormLoadLocationData"/> class.
        /// Reads the gazetteer file path from the args, used in testing. 
        /// </summary>
        /// <param name="args">The arguments containing the gazetteer filepath.</param>
        public FormLoadLocationData(string[] args = null)
        {
            InitializeComponent();

            //columnsMapping = new ColumnsMappingProvider(Program.Connection);
            if (args != null && args.Length >= 1)
            {
                string path = args[0];

                if (!String.IsNullOrWhiteSpace(path))
                {
                    LoadFile(path);
                    DisplayColumnNameLists();
                }
            }
        }

        #endregion Constructors

        #region Methods

        private bool AreColumnNamesSelected()
        {
            // levels 1,2 and 3 must be selected
            bool selected = cboLevel1Codes.SelectedIndex >= 0;
            selected = selected && cboLevel2Codes.SelectedIndex > 0;
            selected = selected && cboLevel3Codes.SelectedIndex > 0;
            selected = selected && cboLevel1Names.SelectedIndex > 0;
            selected = selected && cboLevel2Names.SelectedIndex > 0;
            selected = selected && cboLevel3Names.SelectedIndex > 0;

            return selected;
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                var path = SelectFile();

                if (!String.IsNullOrWhiteSpace(path))
                {
                    LoadFile(path);
                    DisplayColumnNameLists();
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
                    if (!AreColumnNamesSelected())
                    {
                        UiHelper.DisplayMessage(
                            "Please select the columns that contain the code and name data.",
                            "Missing Data");
                    }
                    else
                    {
                        SetColumnNames();
                        SaveColumnMappings();
                        LoadNextScreen();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Error loading input form", ex);
            }
        }

        private void DisplayColumnNameLists()
        {
            cboLevel1Codes.DataSource = geoCoder.AllGazetteerColumnNames();
            cboLevel2Codes.DataSource = geoCoder.AllGazetteerColumnNames();
            cboLevel3Codes.DataSource = geoCoder.AllGazetteerColumnNames();

            cboLevel1Names.DataSource = geoCoder.AllGazetteerColumnNames();
            cboLevel2Names.DataSource = geoCoder.AllGazetteerColumnNames();
            cboLevel3Names.DataSource = geoCoder.AllGazetteerColumnNames();

            cboLevel1AltNames.DataSource = geoCoder.AllGazetteerColumnNames();
            cboLevel2AltNames.DataSource = geoCoder.AllGazetteerColumnNames();
            cboLevel3AltNames.DataSource = geoCoder.AllGazetteerColumnNames();

            // todo set the defaults to those stored in the database
            SetDefaultNames();
        }

        private void DisplaySavedColumnHeaderIndices(string path)
        {
            //// todo move  code away from the UI, get these via the geoCoder
            //var locationColumnMapping = columnsMapping.GetLocationColumnsMapping(path);
            //if (locationColumnMapping != null)
            //{
            //    udCode1.Value = locationColumnMapping.Level1Code + 1;
            //    udName1.Value = locationColumnMapping.Level1Name + 1;
            //    udAltName1.Value = locationColumnMapping.Level1AltName + 1;
            //    udCode2.Value = locationColumnMapping.Level2Code + 1;
            //    udName2.Value = locationColumnMapping.Level2Name + 1;
            //    udAltName2.Value = locationColumnMapping.Level2AltName + 1;
            //    udCode3.Value = locationColumnMapping.Level3Code + 1;
            //    udName3.Value = locationColumnMapping.Level3Name + 1;
            //    udAltName3.Value = locationColumnMapping.Level3AltName + 1;
            //}
        }

        private void FormLoadDataClosing(object sender, CancelEventArgs e)
        {
            // close this form too if the main form has closed
            Close();
        }

        private void FormLoadLocationData_Load(object sender, EventArgs e)
        {
            SetGridDefaults();
        }

        private void LoadFile(string path)
        {
            geoCoder.LoadGazetter(path);
            dataGridView1.DataSource = geoCoder.GazetteerData;
            dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
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
            //// todo move  code away from the UI, save via the geoCoder
            //int loc1Code = (int) udCode1.Value - 1;
            //int loc1Name = (int) udName1.Value - 1;
            //int loc1AltName = (int) udAltName1.Value - 1;
            //int loc2Code = (int) udCode2.Value - 1;
            //int loc2Name = (int) udName2.Value - 1;
            //int loc2AltName = (int) udAltName2.Value - 1;
            //int loc3Code = (int) udCode3.Value - 1;
            //int loc3Name = (int) udName3.Value - 1;
            //int loc3AltName = (int) udAltName3.Value - 1;

            //columnsMapping.SaveLocationColumnsMapping(
            //    new LocationColumnsMapping
            //    {
            //        FileName = txtFileName.Text,
            //        Level1Code = loc1Code,
            //        Level1Name = loc1Name,
            //        Level1AltName = loc1AltName,
            //        Level2Code = loc2Code,
            //        Level2Name = loc2Name,
            //        Level2AltName = loc2AltName,
            //        Level3Code = loc3Code,
            //        Level3Name = loc3Name,
            //        Level3AltName = loc3AltName,
            //    }
            //    );
        }

        private string SelectFile()
        {
            const string filter = "csv files (*.csv)|*.csv";
            txtFileName.Clear();
            var path = UiHelper.GetFileName(filter).Trim();
            txtFileName.Text = path;
            return path;
        }

        private void SetColumnNames()
        {
            GazetteerColumnNames columnNames = new GazetteerColumnNames();
            columnNames.Level1Code = cboLevel1Codes.SelectedValue as string;
            columnNames.Level1Name = cboLevel1Names.SelectedValue as string;
            columnNames.Level2AltNames = cboLevel1AltNames.SelectedValue as string;

            columnNames.Level2Code = cboLevel2Codes.SelectedValue as string;
            columnNames.Level2Name = cboLevel2Names.SelectedValue as string;
            columnNames.Level2AltNames = cboLevel2AltNames.SelectedValue as string;

            columnNames.Level3Code = cboLevel3Codes.SelectedValue as string;
            columnNames.Level3Name = cboLevel3Names.SelectedValue as string;
            columnNames.Level3AltNames = cboLevel3AltNames.SelectedValue as string;
            geoCoder.SetGazetteerColumns(columnNames);
        }

        private void SetDefaultNames()
        {
            // set defaults if they exist in the input sheet
            GazetteerColumnNames defaultColumnNames =
                geoCoder.DefaultGazetteerColumnNames();
            cboLevel1Codes.SelectedIndex =
                cboLevel1Codes.FindStringExact(defaultColumnNames.Level1Code);
            cboLevel2Codes.SelectedIndex =
                cboLevel2Codes.FindStringExact(defaultColumnNames.Level2Code);
            cboLevel3Codes.SelectedIndex =
                cboLevel3Codes.FindStringExact(defaultColumnNames.Level3Code);

            cboLevel1Names.SelectedIndex =
                cboLevel1Names.FindStringExact(defaultColumnNames.Level1Name);
            cboLevel2Names.SelectedIndex =
                cboLevel2Names.FindStringExact(defaultColumnNames.Level2Name);
            cboLevel3Names.SelectedIndex =
                cboLevel3Names.FindStringExact(defaultColumnNames.Level3Name);

            cboLevel1AltNames.SelectedIndex =
                cboLevel1AltNames.FindStringExact(defaultColumnNames.Level1AltNames);
            cboLevel2AltNames.SelectedIndex =
                cboLevel2AltNames.FindStringExact(defaultColumnNames.Level2AltNames);
           cboLevel3AltNames.SelectedIndex =
                cboLevel3AltNames.FindStringExact(defaultColumnNames.Level3AltNames);
        }

        private void SetGridDefaults()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.ReadOnly = true;
        }

        #endregion Methods
    }
}