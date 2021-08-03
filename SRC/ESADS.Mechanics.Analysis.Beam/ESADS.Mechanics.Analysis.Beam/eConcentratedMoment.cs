using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Analysis.Beam
{
    /// <summary>
    /// Represents e concetrated moment loads.
    /// </summary>
    public class eConcentratedMoment:eLoad
    {

        #region Constructor
        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Analysis ePointMoment class from the given basic parameters for joint load.
        /// </summary>
        /// <param name="Magnitude">Magnitude of the load</param>
        /// <param name="joint">The joint on which the load found.</param>
        /// <param name="start">The distance between near end and the starting point of the load.</param>
        /// <param name="actionType">The actionType type defined in EBCS.</param>
        public eConcentratedMoment(double magnitude, eAMember member, double start, ESADS.Code.eActionType actionType)
            : base(eLoadType.ConcentratedMoment, magnitude, member, start, actionType)
        {
        }
        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Analysis ePointMoment class for joint load.
        /// </summary>
        /// <param name="Magnitude">Magnitude of the load</param>
        /// <param name="actionType">The actionType type defined in EBCS</param>
        public eConcentratedMoment(double magnitude, eJoint joint, ESADS.Code.eActionType actionType)
            : base(eLoadType.ConcentratedMoment, magnitude, joint, actionType)
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// Fills the fixed end forces of a joint on which this load found. Fixed end forces added to the joint end are only due to this load.
        /// </summary>
        public override void FillFixedEndForces()
        {
            fixedEndForces = new double[4];
            double M = Magnitude;
            double L = member.Length;
            double a = start;
            double b = L - a;
            fixedEndForces[1] = M * b * (2 * a - b) / Math.Pow(L, 2);
            fixedEndForces[3] = M * a * (2 * b - a) / Math.Pow(L, 2);
            fixedEndForces[0] = (M + fixedEndForces[1] + fixedEndForces[3]) / L;
            fixedEndForces[2] = -fixedEndForces[0];
        }

        /// <summary>
        /// Returns the distance of the centroid of the moment from the specified location.
        /// </summary>
        /// <param name="location">The distance of the section from the near end.</param>
        /// <returns></returns>
        public override double GetCentroidAt(double location)
        {
            if (location > start)
                return 1;
            else
                return 0;
        }

        #endregion
    }
}
