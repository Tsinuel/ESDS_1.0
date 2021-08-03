using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.EGraphics.Beam
{
    public enum eJointOrientation
    {
        /// <summary>
        /// Represents a joint connected to a member on its left side.
        /// </summary>
        LeftSideConnected,
        /// <summary>
        /// Represents a joint connected to a member on its right side.
        /// </summary>
        RightSideConnected,
        /// <summary>
        /// Represents a default joint type which connected in both sides.
        /// </summary>
        Default,
    }
}
