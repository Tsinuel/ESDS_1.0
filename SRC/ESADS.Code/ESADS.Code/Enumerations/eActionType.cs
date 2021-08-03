using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code
{
    /// <summary>
    /// Action is the Force(load) applied to the structure(direct action) or an imposed or constrained deformation 
    /// or an imposed acceleration[Sec. 1.1.3 of EBCS-1-1995].
    /// </summary>
    public enum eActionType
    {
        /// <summary>
        /// Action which is likely to act throughout a given design situation and for which the variation in magnitude 
        /// with time is negligible in relation to the mean value, or for which the variation is always in the same 
        /// direction (monotonic) until the action attains a certain limit value.
        /// </summary>
        Permanent,
        /// <summary>
        /// Action, which is unlikely to act throughout a given design situation or for which the variation in magnitude
        /// with time is neither negligible in relation to the mean value nor monotonic.
        /// </summary>
        Variable,
    }
}
