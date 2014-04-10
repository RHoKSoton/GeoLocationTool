// NameInGazetteerException.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;

    /// <summary>
    /// Exception thrown when a match name is already in the gazetteer.
    /// </summary>
    internal class NameInGazetteerException : Exception
    {
        #region Constructors

        public NameInGazetteerException(string message)
            : base(message)
        {
        }

        #endregion Constructors
    }
}