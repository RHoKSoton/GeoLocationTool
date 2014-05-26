// GazetteerRecord.cs

namespace MultiLevelGeoCoder.Logic
{
    /// <summary>
    /// Holds a record from the Gazetteer data file
    /// </summary>
    internal class GazetteerRecord
    {
        private string name1;
        private string name2;
        private string name3;
        private string id3;
        private string id2;
        private string id1;
        private string altName3;
        private string altName2;
        private string altName1;

        #region Properties

        public string AltName1
        {
            get { return altName1; }
            set { altName1 = value != null ? value.Trim() : null; }
        }

        public string AltName2
        {
            get { return altName2; }
            set { altName2 = value != null ? value.Trim() : null; }
        }

        public string AltName3
        {
            get { return altName3; }
            set { altName3 = value != null ? value.Trim() : null; }
        }

        public string Id1
        {
            get { return id1; }
            set { id1 = value != null ? value.Trim() : null; }
        }

        public string Id2
        {
            get { return id2; }
            set { id2 = value != null ? value.Trim() : null; }
        }

        public string Id3
        {
            get { return id3; }
            set { id3 = value != null ? value.Trim() : null; }
        }

        public string Name1
        {
            get { return name1; }
            set { name1 = value != null ? value.Trim() : null; }
        }

        public string Name2
        {
            get { return name2; }
            set { name2 = value != null ? value.Trim() : null; }
        }

        public string Name3
        {
            get { return name3; }
            set { name3 = value != null ? value.Trim() : null; }
        }

        #endregion Properties
    }
}