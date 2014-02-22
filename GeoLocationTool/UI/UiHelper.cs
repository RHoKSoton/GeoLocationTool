// UiHelper.cs

namespace GeoLocationTool.UI
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// UI helper methods used by multiple forms.
    /// </summary>
    public class UiHelper
    {
        #region Methods

        /// <summary>
        /// Gets the name of the file from the user.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The file name and path.</returns>
        public static string GetFileName(string filter)
        {
            string fileName = string.Empty;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog1.Filter = filter;

            // todo check the filter index values
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                if (!File.Exists(fileName))
                {
                    MessageBox.Show(
                        "File does not exist:\r\n" + fileName,
                        "No File",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                }
            }
            return fileName;
        }


        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        public static void DisplayMessage(string message, string caption)
        {
            MessageBox.Show(message, caption);
        }

        #endregion Methods
    }
}