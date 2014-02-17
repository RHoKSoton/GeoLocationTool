// ErrorHandler.cs

namespace GeoLocationTool.UI
{
    using System;
    using System.Windows.Forms;


    /// <summary>
    /// Handle exceptions
    /// </summary>
    internal class ErrorHandler
    {
        #region Methods

        public static void Process(string message, Exception exception)
        {
            // Display the error details
            MessageBox.Show(
                String.Format("Error: {0} Details: {1}", message, exception.Message),
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        #endregion Methods
    }
}