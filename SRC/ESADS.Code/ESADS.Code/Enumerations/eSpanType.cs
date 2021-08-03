using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code
{
    /// <summary>
    /// The type of span of beam or slab depending on the type of connection to neighbours.
    /// </summary>
    public enum eSpanType
    {
        /// <summary>
        /// There is no moment continuity in all direction. 
        /// </summary>
        SimplySupported,
        /// <summary>
        /// There is moment continuity in one direction and no one in the other.
        /// </summary>
        EndSpan,
        /// <summary>
        /// There is moment continuity in every direction.
        /// </summary>
        InteriorSpan,
        /// <summary>
        /// There is moment continuity in one direction and no connection(free end) in the other.
        /// </summary>
        Cantilever,
    }
}
