// IncompleteLocationException.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;

    public class IncompleteLocationException : Exception
    {
        #region Constructors

        public IncompleteLocationException(string message)
            : base(message)
        {
        }

        #endregion Constructors
    }
}