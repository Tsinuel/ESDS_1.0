using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Analysis.Beam
{
    /// <summary>
    /// Represents a concentrated Force.
    /// </summary>
    public class eConcentratedForce : eLoad
    {
        #region Constructor
        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Analysis ePointForce class from the given basic parameters for joint load.
        /// </summary>
        /// <param name="Magnitude">Magnitude of the load</param>
        /// <param name="joint">The joint on which the load found.</param>
        /// <param name="start">The distance between far end and the end point of the load.</param>
        /// <param name="actionType">The action type defined in EBCS.</param>
        public eConcentratedForce(double magnitude, eAMember member, double start, ESADS.Code.eActionType actionType)
            : base(eLoadType.ConcentratedForce,magnitude, member, start, actionType)
        {
        }

        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Analysis ePointForce class for joint load.
        /// </summary>
        /// <param name="Magnitude">Magnitude of the load</param>
        /// <param name="actionType">The actionType type defined in EBCS.</param>
        public eConcentratedForce(double magnitude, eJoint joint, ESADS.Code.eActionType actionType)
            : base(eLoadType.ConcentratedForce, magnitude, joint, actionType)
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// Returns the distance of the centroid of the load from the specified location.
        /// </summary>
        /// <param name="location">The distance of the section from the near end.</param>
        /// <returns></returns>
        public override double GetCentroidAt(double location)
        {
            if (location > start)
                return location - start;
            else
                return 0;
        }

        /// <summary>
        /// Fills the fixed end forces for this joint.
        /// </summary>
        public override void FillFixedEndForces()
        {
            fixedEndForces = new double[4];
            double P = Magnitude;
            double L = member.Length;
            double a = start;
            double b = L - a;

            fixedEndForces[1] = P * a * Math.Pow(b,  2) / Math.Pow(L, 2);
            fixedEndForces[3] = -P * b * Math.Pow(a, 2) / Math.Pow(L, 2);

            fixedEndForces[0] = (P * b + fixedEndForces[1] + fixedEndForces[3]) / L;
            fixedEndForces[2] = P - fixedEndForces[0];
        }
        #endregion
    }
}
