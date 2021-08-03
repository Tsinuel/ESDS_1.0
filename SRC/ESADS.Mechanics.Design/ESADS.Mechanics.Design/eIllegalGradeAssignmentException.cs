using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Design
{
    /// <summary>
    /// Exeption thrown when using user defined grades as predefined grade to calculate other properies of material specified in the code.
    /// </summary>
    class eIllegalGradeAssignmentException : Exception
    {
        /// <summary>
        /// Initializes an instance of ESADS_Mechanics.eIllegalAssignmentException class.
        /// </summary>
        public eIllegalGradeAssignmentException()
            : base()
        {
        }

        /// <summary>
        /// Initializes an instance of ESADS_Mechanics.eIllegalAssignmentException class for specified exception message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        
        public eIllegalGradeAssignmentException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes an instance of ESADS_Mechanics.eIllegalAssignmentException class with specified error message and 
        ///  a reference to the inner exception that is the cause ofthis exception.
        /// </summary>
        ///  <param name="message">The error message that explains the reason for the exception.</param>
        ///  <param name="innerException"> The exception that is the cause of the current exception, or a null reference.</param>
      
        public eIllegalGradeAssignmentException(string message, Exception innerException)
            : base(message,innerException)
        {
        }

        /// <summary>
        /// Initializes an instance of ESADS_Mechanics.eIllegalAssignmentException class with serialize data.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context"> The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        public eIllegalGradeAssignmentException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
