using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Design.Beam
{
    /// <summary>
    /// Exception thrown when the actual steel ratios are one bellow the absolute minimum and one above the absolute maximum steel ratio or when both are above the absolute maximum steel ratio.
    /// </summary>
    class eImposibleSteelRatioException:Exception
    {
          /// <summary>
        /// Initializes an instance of ESADS_Mechanics.eImposibleSteelRatioException class.
        /// </summary>
        public eImposibleSteelRatioException()
            : base()
        {
        }
        /// <summary>
        /// Initializes an instance of ESADS_Mechanics.eImposibleSteelRatioException class for specified exception message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        ///<param name="steelRatio1">The smallest reinforcement ratio that is calculated from mechanics requirment.</param>
        ///<param name="steelRatio2">The largest reinforcement ratio that is calculated from mechanics requirment.</param>
        ///<param name="steelRatioMax">The largest reinforcement ratio that is provided in the code.</param>
        public eImposibleSteelRatioException(string message ,double steelRatio1, double steelRatio2,double steelRatioMax)
            : base(message)
        {
            this.SteelRatio1 = steelRatio1;
            this.SteelRatio2 = steelRatio2;
            this.MaxSteelRatio = steelRatioMax;
        }

        /// <summary>
        /// Initializes an instance of ESADS_Mechanics.eImposibleSteelRatioException class with specified error message and 
        ///  a reference to the inner exception that is the cause of this exception.
        /// </summary>
        ///  <param name="message">The error message that explains the reason for the exception.</param>
        ///  <param name="innerException"> The exception that is the cause of the current exception, or a null reference.</param>
      
        public eImposibleSteelRatioException(string message, Exception innerException)
            : base(message,innerException)
        {
        }

        /// <summary>
        /// Initializes an instance of ESADS_Mechanics.eImposibleSteelRatioException class with serialize data.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context"> The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        public eImposibleSteelRatioException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

        public double SteelRatio1
        {
            get;
            set;
        }
        public double SteelRatio2
        {
            get;
            set;
        }
        public double MaxSteelRatio
        {
            get;
            set;
        }

        public override string Message
        {
            get
            {
                return base.Message + "\n Maximum Steel Ratio: " + MaxSteelRatio.ToString() + "\n Steel Ratio 1: " + SteelRatio1.ToString() + "\n Steel Ratio 2: " + SteelRatio2.ToString();
            }
        }
    }
}
