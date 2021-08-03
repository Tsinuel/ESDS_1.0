using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Analysis.Beam
{
    /// <summary>
    /// Describes the actionType of load dealt by an instance of load interms of kind or shape of the load.
    /// </summary>
    public enum eLoadType
    {
        /// <summary>
        /// Represents a single force applied at a point on the structure.
        /// </summary>
        ConcentratedForce,
        /// <summary>
        /// Represents a single moment curling along an axis passing horzontally perpendicular through the joint.
        /// </summary>
        ConcentratedMoment,
        /// <summary>
        /// Represents a force load distributed uniformly over a distance.
        /// </summary>
        UniformlyDistributed,
        /// <summary>
        /// Represents a force load zero at a point and increases linearly to a value at other point.
        /// </summary>
        Triangular,
    }
}
