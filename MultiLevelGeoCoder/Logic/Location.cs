﻿// Location.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;
    using System.Text;

    /// <summary>
    /// Simple structure holding location names
    /// </summary>
    public class Location
    {
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

        public string Name1 { get; set; }

        public string Name2 { get; set; }

        public string Name3 { get; set; }

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
            StringBuilder message = new StringBuilder();

            // a location must have a level 1
            if (string.IsNullOrEmpty(Name1))
            {
                message.Append("Level 1 missing");

                // name if present must have value at all higher levels
                if (!string.IsNullOrEmpty(Name3))
                {
                    if (string.IsNullOrEmpty(Name2))
                    {
                        message.Append("Level 2 missing");
                    }
                }
            }
            if (message.Length > 0)
            {
                throw new IncompleteLocationException(message.ToString());
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