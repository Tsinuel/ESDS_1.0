using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Code;
using ESADS.Code.EBCS_1995;

namespace ESADS.Mechanics.Analysis.Beam
{
    /// <summary>
    /// Represents a base class for all types of load that act on a structure.
    /// </summary>
    public abstract class eLoad
    {
        #region Feilds
        /// <summary>
        /// Holds a value for public property 'Magnitude'.
        /// </summary>
        private double magnitude;
        /// <summary>
        ///Contains the lenght of the joint on which the load is found.
        /// </summary>
        protected eAMember member;
        /// <summary>
        /// Holds a value for public property 'Start'.
        /// </summary>
        protected double start;
        /// <summary>
        /// Holds a value for public property 'FE_Forces.
        /// </summary>
        protected double[] fixedEndForces;
        /// <summary>
        /// Holds a value for public property 'LoadType'.
        /// </summary>
        protected eLoadType loadType;
        #endregion
        /// <summary>
        /// Holds the value of the 'ActionType'.
        /// </summary>
        protected eActionType actionType;
        /// <summary>
        /// The current load combination applied in the structure in which the load is applied.
        /// </summary>
        private eLoadCombination loadCombination;

        #region Properties
        /// <summary>
        /// Gets or sets the factored Magnitude of the load.
        /// </summary>
        public double Magnitude
        {
            get
            {
                return loadCombination.GetFactored(this.actionType, this.magnitude);
            }
        }

        /// <summary>
        /// Gets or sets the starting location of the load distribution.
        /// </summary>
        public double Start
        {
            get { return start; }
            set 
            {
                start = value;
                OnChanged(new eLoadChangedEventArgs(true, false));
            }
        }

        /// <summary>
        /// Gets the joint on which the load found.
        /// </summary>
        public eAMember Member
        {
            get { return member; }
        }

        /// <summary>
        /// Gets the fixed end forces on the joint due to this load only.
        /// </summary>
        public double[] FixedEndForces
        {
            get
            {
                return fixedEndForces;
            }
        }

        /// <summary>
        /// Gets the load actionType of this load.
        /// </summary>
        public eLoadType LoadType
        {
            get { return loadType; }
        }

        #endregion

        /// <summary>
        /// Gets or sets the actionType of the load defined in EBCS.
        /// </summary>
        public eActionType ActionType
        {
            get { return actionType; }
            set { actionType = value; }
        }

        /// <summary>
        /// Gets or sets the unfactored load Magnitude.
        /// </summary>
        public double UnfactoredMagnitude
        {
            get
            {
                return this.magnitude;
            }
            set
            {
                this.magnitude = value; 
                OnChanged(new eLoadChangedEventArgs(false, true));
            }
        }

        #region Constructor

        /// <summary>
        /// Fills the base class parameters for class drived from this class.
        /// </summary>
        /// <param name="loadType"> Value representing the actionType of the load.</param>
        /// <param name="unfactoredMagnitude">The Magnitude of the load.</param>
        /// <param name="member">The member on which the load is found.</param>
        /// <param name="joint">The joint on which the load found.</param>
        /// <param name="start">The distance between the near end and the starting point of the load.</param>
        /// <param name="actionType">The type of the actionType(load) defined in EBCS.</param>
        protected eLoad(eLoadType loadType, double unfactoredMagnitude, eAMember member, double start, eActionType actionType)
        {
            this.UnfactoredMagnitude = unfactoredMagnitude;
            this.member = member;
            this.start = start;
            this.loadType = loadType;
            this.actionType = actionType;
            this.loadCombination = member.Beam.LoadCombination;
        }

        /// <summary>
        /// Fills the base class parameters for class drived from this class.
        /// </summary>
        /// <param name="loadType"> Value representing the actionType of the load.</param>
        /// <param name="unfactoredMagnitude">The Magnitude of the load.</param>
        /// <param name="joint">The joint on which the load found.</param>
        /// <param name="actionType">The type of the actionType(load) defined in EBCS.</param>
        protected eLoad(eLoadType loadType, double unfactoredMagnitude, eJoint joint, eActionType actionType)
        {
            this.UnfactoredMagnitude = unfactoredMagnitude;
            this.loadType = loadType;
            this.actionType = actionType;
            this.loadCombination = joint.Beam.LoadCombination;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the concentrated load for this load when a section is taken at defined point in the joint.
        /// </summary>
        /// <param name="location">The distance of the section cut from the near end.</param>
        /// <returns></returns>
        public virtual double ConcentrateAt(double location)
        {
            if (location > start)
                return Magnitude;
            else
                return 0;
        }

        /// <summary>
        /// Returns the distance of the centroid of the load from the specified location.
        /// </summary>
        /// <param name="location">The distance of the section from the near end.</param>
        /// <returns></returns>
        public abstract double GetCentroidAt(double location);

        /// <summary>
        /// Fills the fixed end forces based on the load.
        /// </summary>
        public abstract void FillFixedEndForces();

        /// <summary>
        /// Returns the intervals of all possible section in the joint due to this load only.
        /// </summary>
        /// <returns>The retured array contains the distance of each section starting and section ending point from the near end.</returns>
        public virtual double[] GetSectionsInterval()
        {
            double[] intervals;
            if ((start > 0) && (start < member.Length))
            {
                intervals = new double[3];
                intervals[0] = 0;
                intervals[1] = start;
                intervals[2] = member.Length;
                return intervals;
            }
            else
            {
                intervals = new double[2];
                intervals[0] = 0;
                intervals[1] = member.Length;
                return intervals;
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// Fires the Changed event.
        /// </summary>
        /// <param name="e">The event argument .</param>
        protected void OnChanged(eLoadChangedEventArgs e)
        {
            if (this.Changed != null)
            {
                this.Changed(this, e);
            }
        }

        /// <summary>
        /// Occurs when the location or distribution of the load relative to the joint is changed.
        /// </summary>
        public event eLoadChangedEventHandler Changed;
        #endregion
    }
}
