using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS
{
    /// <summary>
    /// Specifies different unit systems used to measure force.
    /// </summary>
    public enum eForceUints
    {
        /// <summary>
        /// Represents Metric unit system, KiloNewton.
        /// </summary>
        KN,
        /// <summary>
        /// Represents Metric unit system, Newton.
        /// </summary>
        N,
        /// <summary>
        /// Represents Imperial unit system, Pound.
        /// </summary>
        lb,
        /// <summary>
        /// Represents Imperial unit system, Kilo Pounds
        /// </summary>
        Kip
    };
}
