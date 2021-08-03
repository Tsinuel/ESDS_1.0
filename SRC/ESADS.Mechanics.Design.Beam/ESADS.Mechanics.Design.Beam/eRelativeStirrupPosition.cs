using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Design.Beam
{
    /// <summary>
    /// Specifies the orientation of of stirrups holding the logtudinal barType1.
    /// </summary>
    public enum eRelativeStirrupPosition
    {
        /// <summary>
        /// Specifies Orientation with bottom stirrup and top longtudinal shearBar.
        /// </summary>
        StirrupAtBottom,
        /// <summary>
        /// Specifies Orientation with top stirrup and bottom longtudinal shearBar.
        /// </summary>
        StirrupAtTop
    };
}
