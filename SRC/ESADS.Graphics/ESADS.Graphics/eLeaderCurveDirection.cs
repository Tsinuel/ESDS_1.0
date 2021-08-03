using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Defines the direction of fall of the curve when watching in the direction from the start to the end for an arc type leader.
    /// </summary>
    public enum eLeaderCurveDirection
    {
        /// <summary>
        /// When the arc of a leader curves convex to the right hand side when observing from the start to the end of the leader.
        /// </summary>
        ToTheRight,     
        /// <summary>
        /// When the arc of a leader curves convex to the left hand side when observing from the start to the end of the leader.
        /// </summary>
        ToTheLeft,

    }
}
