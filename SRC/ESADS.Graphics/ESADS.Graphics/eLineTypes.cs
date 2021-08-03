using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Specifies the way a line is drawn.
    /// </summary>
    public enum eLineTypes
    {
        /// <summary>
        /// Solid continuous line without any gap.
        /// </summary>
        Continuous,
        /// <summary>
        /// One small dash followed by relatively longer dash.
        /// </summary>
        Center,
        /// <summary>
        /// Sequential dots.
        /// </summary>
        DotDot,
        /// <summary>
        /// Sequential dashs.
        /// </summary>
        Dashed,
        /// <summary>
        /// One dot followed by a dash.
        /// </summary>
        DashDot,
    }
}
