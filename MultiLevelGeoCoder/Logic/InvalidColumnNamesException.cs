// InvalidColumnNamesException.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;

    /// <summary>
    /// Represents errors that occur during column names selection
    /// </summary>
    public class InvalidColumnNamesException : Exception
    {
        #region Constructors

        public InvalidColumnNamesException(string message)
            : base(message)
        {
        }

        #endregion Constructors
    }
}