using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.EGraphics.Beam
{
    public enum eDiagramStyle
    {
        /// <summary>
        /// Draws the digram curve only.
        /// </summary>
        Draw,
        /// <summary>
        /// Fills the diagram curve and fills the diagram.
        /// </summary>
        Fill,
        /// <summary>
        /// Draw the diagram curve and splice lines.
        /// </summary>
        Slice,
    }
}
