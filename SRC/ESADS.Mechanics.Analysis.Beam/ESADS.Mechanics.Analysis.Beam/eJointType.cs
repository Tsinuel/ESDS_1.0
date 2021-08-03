using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Analysis.Beam
{
    /// <summary>
    /// Represents different 2D joint types.
    /// </summary>
    public enum eJointType
    {
        /// <summary>
        /// Represents pin support actionType.
        /// </summary>
        Pin,
        /// <summary>
        /// Represents Roller support actionType.
        /// </summary>
        Roller,
        /// <summary>
        /// Represents Hinge support actionType.
        /// </summary>
        Hinge,
        /// <summary>
        /// Represents Free support actionType.
        /// </summary>
        Free,
        /// <summary>
        /// Represents Continious support actionType.
        /// </summary>
        Continious,
        /// <summary>
        /// Represents Vertically Guided Roller joint actionType.
        /// </summary>
        VerticalGuidedRoller,
        /// <summary>
        /// Represents a fixed support joint actionType.
        /// </summary>
        Fixed,
        /// <summary>
        /// Represents a vertical roller support joint actionType.
        /// </summary>
        VerticalRoller,
    }
}
