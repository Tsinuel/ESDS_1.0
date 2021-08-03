using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Represents the way by which drawing objects change their properties.
    /// </summary>
    public enum eChangeBy
    {
        /// <summary>
        ///Represents condition when the drawing object properties change when the layer property change.
        /// </summary>
        ByLayer,
        /// <summary>
        ///Represents condition when the drawing object properties change when the object  property change.
        /// </summary>
        ByObject,
    }
}
