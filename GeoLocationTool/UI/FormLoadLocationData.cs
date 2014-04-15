// FormLoadLocationData.cs

namespace GeoLocationTool.UI
{
    using System;
    using System.Collections.Generic;
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
            try
            {
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
            catch (Exception ex)
            {
                ErrorHandler.Process("Form load error.", ex);
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

            //optional columns so display a blank row at the top
            IList<string> columnNames1 = geoCoder.AllGazetteerColumnNames();
            columnNames1.Insert(0, string.Empty);
            cboLevel1AltNames.DataSource = columnNames1;

            IList<string> columnNames2 = geoCoder.AllGazetteerColumnNames();
            columnNames2.Insert(0, string.Empty);
            cboLevel2AltNames.DataSource = columnNames2;

            IList<string> columnNames3 = geoCoder.AllGazetteerColumnNames();
            columnNames3.Insert(0, string.Empty);
            cboLevel3AltNames.DataSource = columnNames3;

            SetDefaultNames();
        }

        private void formLoadData_Closed(object sender, EventArgs e)
        {
            // quit the application if the input form has closed
            Application.Exit();
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
                DataGridViewAutoSizeColumnsMode.AllCells;
            txtFileName.Text = path;
        }

        private void LoadNextScreen()
        {
            if (formLoadData == null)
            {
                formLoadData = new FormLoadData(geoCoder);
                formLoadData.Closed += formLoadData_Closed;
            }

            formLoadData.Show(this);
            Hide();
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
            columnNames.Level1AltName = cboLevel1AltNames.SelectedValue as string;

            columnNames.Level2Code = cboLevel2Codes.SelectedValue as string;
            columnNames.Level2Name = cboLevel2Names.SelectedValue as string;
            columnNames.Level2AltName = cboLevel2AltNames.SelectedValue as string;

            columnNames.Level3Code = cboLevel3Codes.SelectedValue as string;
            columnNames.Level3Name = cboLevel3Names.SelectedValue as string;
            columnNames.Level3AltName = cboLevel3AltNames.SelectedValue as string;
            geoCoder.SetGazetteerColumns(columnNames);
        }

        private void SetDefaultNames()
        {
            // set default column names if they exist in the spread sheet
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
                cboLevel1AltNames.FindStringExact(defaultColumnNames.Level1AltName);
            cboLevel2AltNames.SelectedIndex =
                cboLevel2AltNames.FindStringExact(defaultColumnNames.Level2AltName);
            cboLevel3AltNames.SelectedIndex =
                cboLevel3AltNames.FindStringExact(defaultColumnNames.Level3AltName);
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