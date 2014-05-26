// Location.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Simple structure holding location names
    /// </summary>
    public class Location
    {
        private string name1;
        private string name2;
        private string name3;

        #region Constructors

        public Location(string name1, string name2 = "", string name3 = "")
        {
            Name1 = name1;
            Name2 = name2;
            Name3 = name3;
        }

        public Location()
        {
        }

        #endregion Constructors

        #region Properties

        public string Name1
        {
            get { return name1; }
            set { name1 = value.Trim(); }
        }

        public string Name2
        {
            get { return name2; }
            set { name2 = value.Trim(); }
        }

        public string Name3
        {
            get { return name3; }
            set { name3 = value.Trim(); }
        }

        #endregion Properties

        #region Methods

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Location) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Name1 != null ? Name1.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Name2 != null ? Name2.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Name3 != null ? Name3.GetHashCode() : 0);
                return hashCode;
            }
        }

        public void Validate()
        {
            // name if present must have value at all higher levels
            List<int> missing = new List<int>();

            if (!string.IsNullOrEmpty(Name3))
            {
                if (string.IsNullOrEmpty(Name2))
                {
                    missing.Add(2);
                }
                if (string.IsNullOrEmpty(Name1))
                {
                    missing.Add(1);
                }
            }

            if (!string.IsNullOrEmpty(Name2))
            {
                if (string.IsNullOrEmpty(Name1))
                {
                    missing.Add(1);
                }
            }

            if (missing.Count > 0)
            {
                missing.Sort();
                string message = "Incomplete location. Missing level(s): " +
                                 string.Join(", ", missing.Distinct());
                throw new IncompleteLocationException(message);
            }
        }

        protected bool Equals(Location other)
        {
            return
                string.Equals(
                    Name1,
                    other.Name1,
                    StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(
                    Name2,
                    other.Name2,
                    StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(
                    Name3,
                    other.Name3,
                    StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion Methods
    }
}