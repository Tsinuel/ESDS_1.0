using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Design.Beam
{
    /// <summary>
    /// Represents the type of the section which is identified based on the location of the section.
    /// </summary>
    public enum eSectionType
    {
        /// <summary>
        /// Represents the section at supportSxn_Right.
        /// </summary>
        Support,
        /// <summary>
        /// Represents the section at span.
        /// </summary>
        Span,
    }
}
