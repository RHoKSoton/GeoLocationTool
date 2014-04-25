using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoLocationTool
{
    using MultiLevelGeoCoder.DataAccess;
    using System.Data.Common;
    using UI;

    static class Program
    {
        public static DbConnection Connection { get; private set; }
        private const string DB_LOCATION = @"GeoLocationTool.sdf";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Connection = DBHelper.GetDbConnection(DB_LOCATION);
            Connection.InitializeDB();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLoadGazetteer(args));
            Connection.Close();
        }
    }
}
