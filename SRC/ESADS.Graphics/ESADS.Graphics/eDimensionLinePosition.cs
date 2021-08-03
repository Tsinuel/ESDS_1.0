using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Determines the relative position of the membDimension and the object to be measured.
    /// </summary>
    public enum eDimensionLinePosition
    {
        /// <summary>
        /// Puts the membDimension line to the left or above the object to be measured depanding on the startPoint of membDimension.
        /// </summary>
        LeftOrAbove,
        /// <summary>
        /// Puts the membDimension line to the right or at the bottom of the object to be measured.
        /// </summary>
        RightOrBottom,
    }
}
