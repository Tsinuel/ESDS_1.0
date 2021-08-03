using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Defines the way to measure the distance between two poiints.
    /// </summary>
    public enum eDimensionType
    {
        /// <summary>
        /// Represents a membDimension that measures the vertical difference between two points.(difference in y-coordinates.)
        /// </summary>
        LinearVertical,
        /// <summary>
        /// Represents a membDimension that measures the horizontal difference between two points.(difference in x-coordinates.)
        /// </summary>
        LinearHorizontal,
        /// <summary>
        /// Represents a membDimension that measures the length between two points .
        /// </summary>
        Aligned,
    }
}
