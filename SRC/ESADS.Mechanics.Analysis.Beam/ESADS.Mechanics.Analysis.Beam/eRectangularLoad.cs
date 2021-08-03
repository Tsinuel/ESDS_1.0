using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Analysis.Beam
{
    /// <summary>
    /// Represents a uniformly distributed load.
    /// </summary>
    public class eRectangularLoad : eLoad
    {
        #region Feilds
        /// <summary>
        /// Holds a value for public property 'End'.
        /// </summary>
        private double end;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the distance between the end of the load and the far end.
        /// </summary>
        public double End
        {
            get { return end; }
            set 
            {
                end = value;
                OnChanged(new eLoadChangedEventArgs(true, false));
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Analysis eRectangularLoad class from the given basic parameters.
        /// </summary>
        /// <param name="Magnitude">Magnitude of the load</param>
        /// <param name="start">The distance between near end and the start point of the load.</param>
        /// <param name="end">The distance between far end and the ending point of the load.</param>
        /// <param name="joint">The joint on which the load found.</param>
        /// <param name="actionType">The action type defined in EBCS.</param>
        public eRectangularLoad(double magnitude, double start, double end, eAMember member, ESADS.Code.eActionType actionType)
            : base(eLoadType.UniformlyDistributed,magnitude, member, start, actionType)
        {
            this.end = end;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the concentrated load for this load.
        /// </summary>
        /// <param name="location">The distance of the section cut from the near end.</param>
        /// <returns></returns>
        public override double ConcentrateAt(double location)
        {
            if (location <= start)
            {
                return 0;
            }
            else if ((location > start) && (location <= member.Length - end))
            {
                return Magnitude * (location - start);
            }
            else
            {
                return Magnitude * (member.Length - start - end);
            }
        }

        /// <summary>
        /// Returns the distance of the centroid of the load from the specified location.
        /// </summary>
        /// <param name="location">The distance of the section from the near end.</param>
        /// <returns></returns>
        public override double GetCentroidAt(double location)
        {
            if (location <= start)
            {
                return 0;
            }
            else if ((location > start) && (location <= member.Length - end))
            {
                return (location - start) / 2;
            }
            else
            {
                return location - (member.Length + start - end) / 2;
            }
        }

        /// <summary>
        /// Fills the fixed end forces of a joint on which this load found. Fixed end forces added to the joint end are only due to this load.
        /// </summary>
        public override void FillFixedEndForces()
        {
            fixedEndForces = new double[4];
            double w = Magnitude;
            double L = member.Length;
            double a = start + (L - start - end) / 2;
            double b = end + (L - start - end) / 2;
            double m = (L - start - end) / 2;
            double c = 2 * m;
         
            fixedEndForces[1] = (2*w * m / Math.Pow(L, 2)) * ((L - b) * Math.Pow(b, 2) + (L / 3 - b) * Math.Pow(m, 2));
            fixedEndForces[3] = -(2 * w * m / Math.Pow(L, 2)) * ((L - a) * Math.Pow(a, 2) + (L / 3 - a) * Math.Pow(m, 2));

            fixedEndForces[0] = (2 * m * w * b + fixedEndForces[1] + fixedEndForces[3]) / L;
            fixedEndForces[2] = 2 * m * w - fixedEndForces[0];
        }

        /// <summary>
        /// Returns the intervals of all possible section in the joint due to this load only.
        /// </summary>
        /// <returns>The retured array contains the distance of each section starting and section ending point from the near end.</returns>
        public override double[] GetSectionsInterval()
        {
            double[] intervals;
            if ((start > 0) && (end >0 ))
            {
                intervals = new double[4];
                intervals[0] = 0;
                intervals[1] = start;
                intervals[2] = member.Length - end;
                intervals[3] = member.Length;
                return intervals;
            }
            else if((start == 0)&&( end == 0))
            {
                intervals = new double[2];
                intervals[0] = 0;
                intervals[1] = member.Length;
                return intervals;
            }
            else if ((start > 0) && (end == 0))
            {
                 intervals = new double[3];
                intervals[0] = 0;
                intervals[1] = start;
                intervals[2] = member.Length;
                return intervals;
            }
            else
            {              
                intervals = new double[3];
                intervals[0] = 0;
                intervals[1] = member.Length - end;
                intervals[2] = member.Length;
                return intervals;
            }
        }
        #endregion
    }
}
