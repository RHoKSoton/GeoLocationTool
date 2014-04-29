// UiHelper.cs

namespace GeoLocationTool.UI
{
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
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = filter;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = dialog.FileName;
                    if (!File.Exists(fileName))
                    {
                        MessageBox.Show(
                            "File does not exist:\r\n" + fileName,
                            "No File",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                    }
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