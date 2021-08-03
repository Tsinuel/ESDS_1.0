using System;

namespace ESADS.EGraphics
{
    /// <summary>
    /// An exception that occurs within the Graphics namespace.
    /// </summary>
    public class eGraphicsException : Exception
    {
        /// <summary>
        /// Creates a new exception that occurs in the Graphics namespace.
        /// </summary>
        public eGraphicsException()
            : base()
        { }

        /// <summary>
        /// Creates a new exception that occurs in the Graphics namespace with a message attached to it.
        /// </summary>
        /// <param name="message">The message that explains the details of the exception.</param>
        public eGraphicsException(string message)
            : base(message)
        { }
    }
}
