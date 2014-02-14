namespace GeoLocationTool
{
    using System;
    using System.Windows.Forms;

    internal class ErrorHandler
    {
        public static void Process(string message, Exception exception)
        {
            // Display the error details
            MessageBox.Show(
                   String.Format("Error: {0} Details: {1}", message, exception.Message),                  
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
        }
    }
}