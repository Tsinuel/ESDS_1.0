using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Specifies the startPoint of leader lines.
    /// </summary>
    public enum eLeaderType
    {
        /// <summary>
        /// Bizeir startPoint line based on the points provided.
        /// </summary>
        Spline,
        /// <summary>
        /// Set of straight lines running from the start to the end.
        /// </summary>
        Straight,
        /// <summary>
        /// The line is formed from an elliptical arc.
        /// </summary>
        Arc,
    }
}
