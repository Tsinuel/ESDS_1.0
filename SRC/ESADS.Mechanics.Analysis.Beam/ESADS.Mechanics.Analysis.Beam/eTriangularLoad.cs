using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Analysis.Beam
{
    /// <summary>
    /// Represents uniformly varing loads;
    /// </summary>
    public class eTriangularLoad : eLoad
    {
        #region Feilds
        /// <summary>
        /// Holds a value for public property 'Orientation'.
        /// </summary>
        private eTriangularLoadOrientation orientation;
        /// <summary>
        /// Holds a value for public property 'End'.
        /// </summary>
        private double end;
       
        #endregion


        #region Properties
        /// <summary>
        /// Gets or sets the orientation of the load.
        /// </summary>
        /// 
        public eTriangularLoadOrientation Orientation
        {
            get { return orientation; }
            set 
            {
                orientation = value;
                OnChanged(new eLoadChangedEventArgs(true));
            }
        }

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
        /// Creates an instance of ESADS.Mechanics.Analysis eTriangularLoad class from the given basic parameters.
        /// </summary>
        /// <param name="Magnitude">Magnitude of the load</param>
        /// <param name="start">The distance between near end and the start point of the load.</param>
        /// <param name="end">The distance between far end and the end point of the load.</param>
        /// <param name="joint">The joint on which the load found.</param>
        /// <param name="orientation">The alignment of the load.</param>
        /// <param name="actionType">The action type defined in EBCS.</param>
        public eTriangularLoad(double magnitude, double start, double end, eAMember member, eTriangularLoadOrientation orientation, ESADS.Code.eActionType actionType)
            : base(eLoadType.Triangular,magnitude, member, start, actionType)
        {
            this.end = end;
            this.orientation = orientation;
        }

        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Analysis eTriangularLoad class from the given basic parameters for LeftToRight oreintation.
        /// </summary>
        /// <param name="Magnitude">Magnitude of the load</param>
        /// <param name="start">The distance between near end and the start point of the load.</param>
        /// <param name="end">The distance between far end and the end point of the load.</param>
        /// <param name="joint">The joint on which the load found.</param>
        /// <param name="actionType">The action type defined in EBCS.</param>
        public eTriangularLoad(double magnitude, double start, double end, eAMember member, ESADS.Code.eActionType actionType)
            : base(eLoadType.Triangular,magnitude, member, start, actionType)
        {
            this.end = end;
            this.orientation  = eTriangularLoadOrientation.LeftToRight;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the concentrated load for this load.
        /// </summary>
        /// <param name="x">The distance of the section cut from the near end.</param>
        /// <returns></returns>
        public override double ConcentrateAt(double x)
        {
            double a = start;
            double b = end;
            double w = Magnitude;
            double L = member.Length;
            double y;
            switch (orientation)
            {
                case eTriangularLoadOrientation.LeftToRight:
                    {
                        if (x <= start)
                        {
                            return 0;
                        }
                        else if ((x > a) && (x <=L - b))
                        {
                            y = w * (x - a) / (L - a - b);
                            return 0.5 * y * (x - a);
                        }
                        else
                        {
                            return 0.5 * w * (L - a - b);
                        }
                    }
                case eTriangularLoadOrientation.RightToLeft:
                    {
                        if (x <= a)
                        {
                            return 0;
                        }
                        else if ((x > a) && (x <=L - b))
                        {
                            y = w * (L - b - x) / (L - b - a);
                            return 0.5 * (y + w) * (x - a);
                        }
                        else
                        {
                            return 0.5* w * (L - a - b);
                        }
                    }
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Returns the distance of the centroid of the load from the specified location.
        /// </summary>
        /// <param name="x">The distance of the section from the near end.</param>
        /// <returns></returns>
        public override double GetCentroidAt(double x)
        {
            double a = start;
            double b = end;
            double w = Magnitude;
            double L = member.Length;
            double y;
            switch (orientation)
            {
                case eTriangularLoadOrientation.LeftToRight:
                    {
                        if (x <= a)
                        {
                            return 0;
                        }
                        else if ((x > a) && (x <= L - b))
                        {
                            return (x - a) / 3;
                        }
                        else
                        {
                            return (3 * x - 2 * L - a + 2 * b) / 3;
                        }
                    }
                case eTriangularLoadOrientation.RightToLeft:
                    {
                        if (x <= a)
                        {
                            return 0;
                        }
                        else if ((x > a) && (x <= L - b))
                        {
                            y = w * (L - b - x) / (L - b - a);
                            return ((x - a) / 3) * (2 * w + y) / (w + y);
                        }
                        else
                        {
                            return (3 * x - L - 2 * a + b) / 3;
                        }
                    }
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Returns the intervals of all possible section in the joint due to this load only.
        /// </summary>
        /// <returns>The retured array contains the distance of each section starting and section ending point from the near end.</returns>
        public override double[] GetSectionsInterval()
        {
            double[] intervals;
            if ((start > 0) && (end > 0))
            {
                intervals = new double[4];
                intervals[0] = 0;
                intervals[1] = start;
                intervals[2] = member.Length - end;
                intervals[3] = member.Length;
                return intervals;
            }
            else if ((start == 0) && (end == 0))
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

        /// <summary>
        /// Fills the fixed end forces of a joint on which this load found. Fixed end forces added to the joint end are only due to this load.
        /// </summary>
        public override void FillFixedEndForces()
        {
            double a = start;
            double b = end;
            double w = Magnitude;
            double L = member.Length;
            fixedEndForces = new double [4];

            if (orientation == eTriangularLoadOrientation.LeftToRight)
            {
                fixedEndForces[0] = w * Math.Pow(L - a, 3) * ((3 * L + 2 * a) * (1 + b / (L - a) + Math.Pow(b, 2) / Math.Pow(L - a, 2)) - Math.Pow(b, 3) / Math.Pow(L - a, 2) * (2 + (15 * L - 8 * b) / (L - a))) / (20 * Math.Pow(L, 3));
                fixedEndForces[1] = w * Math.Pow(L - a, 3) * ((2 * L + 3 * a) * (1 + b / (L - a) + Math.Pow(b, 2) / Math.Pow(L - a, 2)) - 3 * Math.Pow(b, 3) / Math.Pow(L - a, 2) * (1 + (5 * L - 4 * b) / (L - a))) / (60 * Math.Pow(L, 2));
                fixedEndForces[2] = (w / 2) * (L - a - b) - fixedEndForces[0];
                fixedEndForces[3] = -w * (L - a - b) * (L - a + 2 * b) / 6 + fixedEndForces[0] * L - fixedEndForces[1];
            }
            else
            {
                fixedEndForces[0] = w * Math.Pow(L - a, 3) * ((7 * L + 8 * a) - (b * (3 * L + 2 * a) / (L - a)) * (1 + b / (L - a) + Math.Pow(b, 2) / Math.Pow(L - a, 2)) + 2 * Math.Pow(b, 4) / Math.Pow(L - a, 3)) / (20 * Math.Pow(L, 3));
                fixedEndForces[1] = w * Math.Pow(L - a, 3) * (3 *( L + 4 * a) - (b * (2 * L + 3 * a) / (L - a)) * (1 + b / (L - a) + Math.Pow(b, 2) / Math.Pow(L - a, 2)) + 3 * Math.Pow(b, 4) / Math.Pow(L - a, 3)) / (60 * Math.Pow(L, 2));
                fixedEndForces[2] = (w / 2) * (L - a - b) - fixedEndForces[0];
                fixedEndForces[3] = w * (L - a - b) * (-2 * L + 2 * a - b) / 6 + fixedEndForces[0] * L - fixedEndForces[1];
            }
        }
        #endregion

    }
}
