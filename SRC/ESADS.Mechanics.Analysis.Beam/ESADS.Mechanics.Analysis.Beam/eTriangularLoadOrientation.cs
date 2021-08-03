using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Analysis.Beam
{
    /// <summary>
    /// Represents different oreintaions of triangular load.
    /// </summary>
    public enum eTriangularLoadOrientation
    {
        /// <summary>
        /// Represents an alignment with load intensity icreasing direction being  from left to right.
        /// </summary>
        LeftToRight,
        /// <summary>
        /// Represents an alignment with load intensity icreasing direction being  from right to left.
        /// </summary>
        RightToLeft,
    }
}
