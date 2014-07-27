// InputColumnNames.cs

namespace MultiLevelGeoCoder.Logic
{
    /// <summary>
    /// Holds the selected input column headers.
    /// </summary>
    public class InputColumnHeaders
    {
        #region Properties

        public string Level1 { get; set; }

        public string Level2 { get; set; }

        public string Level3 { get; set; }

        #endregion Properties

        #region Methods

        public void Validitate()
        {
            // if a level is set then all lower levels must be set

            string errorMessage = string.Empty;

            if (! string.IsNullOrEmpty(Level3))
            {
                if (string.IsNullOrEmpty(Level2))
                {
                    errorMessage = "Level 2 not set";
                }
                if (string.IsNullOrEmpty(Level1))
                {
                    errorMessage = "Level 1 not set";
                }
            }

            if (!string.IsNullOrEmpty(Level2))
            {
                if (string.IsNullOrEmpty(Level1))
                {
                    errorMessage = "Level 1 not set";
                }
            }

            if (errorMessage.Length > 0)
            {
                throw new InvalidColumnNamesException(errorMessage);
            }
        }

        #endregion Methods
    }
}