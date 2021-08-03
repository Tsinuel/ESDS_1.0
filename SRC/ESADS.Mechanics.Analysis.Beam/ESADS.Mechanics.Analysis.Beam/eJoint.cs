using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Analysis.Beam
{
    /// <summary>
    /// Represents a joint for matrix stractural analysis of a beam.
    /// </summary>
    public class eJoint
    {
        #region Fields

        /// <summary>
        /// Holds a value for public property 'FinalDisp'.
        /// </summary>
        private double[] finalDisp;
        /// <summary>
        /// Holds a value fro public property 'Forces'.
        /// </summary>
        private double [] loads;
        /// <summary>
        /// Holds a value fro public property 'SSMIndex'.
        /// </summary>
        private int[] sSMIndex;
        /// <summary>
        /// Holds a value fro public property 'Type'.
        /// </summary>
        private eJointType type;
        /// <summary>
        /// Holds a value for property 'Reactions'.
        /// </summary>
        private double[] reactions;
        /// <summary>
        /// Holds a value for property 'InitialDisp'.
        /// </summary>
        private double[] initialDisp;
        /// <summary>
        /// Holds a value for public property 'FE_Forces'.
        /// </summary>
        private double[] fE_Forces;
        /// <summary>
        /// Holds the value of the 'SupportWidth' property.
        /// </summary>
        private double supportWidth;

        #endregion

        #region Properties

        /// <summary>
        /// Retursn the constrained axis in the beam.
        /// </summary>
        public eConstrainedAxis ConstrainedAxis
        {
            get
            {
                switch (type)
                {
                    case eJointType.Fixed:
                        return eConstrainedAxis.X_Y_Z;
                    case eJointType.Pin:
                        return eConstrainedAxis.X_Y;
                    case eJointType.VerticalRoller:
                        return eConstrainedAxis.X_Z;
                    case eJointType.Roller:
                        return eConstrainedAxis.Y;
                    case eJointType.VerticalGuidedRoller:
                        return eConstrainedAxis.X_Z_X;
                    default:
                        return eConstrainedAxis.None;
                }
            }
        }

        /// <summary>
        /// Gets or sets the displacements of the joint
        /// </summary>
        public double[] FinalDisps
        {
            get
            {
                return finalDisp;
            }
            set
            {
                finalDisp = value;
            }
        }

        /// <summary>
        /// Gets or sets the  support actionType of the joint
        /// </summary>
        public eJointType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                ResetMatrices();
                OnTypeChanged();
            }
        }

        /// <summary>
        /// Gets or sets the fixed end forces at the joint with their conresponding order in the displacement direction.
        /// </summary>
        internal double[] FE_Forces
        {
            get
            {
                return fE_Forces;
            }
            set
            {
                fE_Forces = value;
            }
        }

        /// <summary>
        /// Gets the Stractural stiffness matrix index in their coresponding displacement direction.
        /// </summary>
        public int[] SSMIndex
        {
            get
            {
                return sSMIndex;
            }
            set
            {
                sSMIndex = value;
            }
        }

        /// <summary>
        /// Gets or sets the reaction at the joint.
        /// </summary>
        public double[] Reactions
        {
            get { return reactions; }
            internal set { reactions = value; }
        }

        /// <summary>
        /// Gets or sets the initial Displacement due to support settlement.
        /// </summary>
        public double[] InitialDisps
        {
            get { return initialDisp; }
            set { initialDisp = value; }
        }

        /// <summary>
        /// Gets or set the loads add at the joint.
        /// </summary>
        public double[] Loads
        {
            get { return loads; }
            set { loads = value; }
        }

        /// <summary>
        /// Gets or sets the width of the support.
        /// </summary>
        public double SupportWidth
        {
            get
            {
                return supportWidth;
            }
            set
            {
                supportWidth = value;
                OnSupportWidthChanged();
            }
        }

        /// <summary>
        /// Gets the beam bearing the joint.
        /// </summary>
        public eABeam Beam
        {
            get
            {
                return this.beam;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Analysis.eJoint class from the given support condition.
        /// </summary>
        /// <param name="type">The support actionType of the joint.</param>
        public eJoint(eJointType jointType, eABeam beam)
        {
            this.beam = beam;
            this.type = jointType;
            this.supportWidth = eUtility.Convert(300.0, eLengthUnits.mm, eUtility.SLU);
            this.loadObjs = new List<eLoad>();
            ResetMatrices();

            if (type == eJointType.Hinge || type == eJointType.VerticalGuidedRoller)
            {
                this.loads = new double[3];
                this.initialDisp = new double[3];
            }
            else
            {
                this.loads = new double[2];
                this.initialDisp = new double[2];
            }
        }

        internal void ResetMatrices()
        {
            if (type == eJointType.Hinge || type == eJointType.VerticalGuidedRoller)
            {
                this.finalDisp = new double[3];
                this.sSMIndex = new int[3];
            }
            else
            {
                this.finalDisp = new double[2];
                this.sSMIndex = new int[2];
            }

            if (type == eJointType.Pin || type == eJointType.VerticalRoller || type == eJointType.Roller)
            {
                reactions = new double[1];
                fE_Forces = new double[1];

            }
            else if (type == eJointType.Fixed)
            {
                reactions = new double[2];
                fE_Forces = null;
            }
            else if (type == eJointType.VerticalGuidedRoller || type == eJointType.Hinge)
            {
                reactions = null;
                fE_Forces = new double[3];
            }
            else
            {
                fE_Forces = new double[2];
                reactions = null;
            }
        }

        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Analysis.eJoint class from the given support condition and displacement.
        /// </summary>
        /// <param name="type">The support actionType of the joint.</param>
        /// <param name="displacements">The corresponding initial displacements at the joint or support setlments.</param>
        public eJoint(eJointType jointType, eABeam beam, double[] displacements)
        {
            this.beam = beam;
            this.type = jointType;
            this.supportWidth = eUtility.Convert(300.0, eLengthUnits.mm, eUtility.SLU);
            this.initialDisp = displacements;
            this.loadObjs = new List<eLoad>();
            ResetMatrices();

            if (type == eJointType.Hinge || type == eJointType.VerticalGuidedRoller)
            {
                this.loads = new double[3];
                this.initialDisp = new double[3];
            }
            else
            {
                this.loads = new double[2];
                this.initialDisp = new double[2];
            }
        }

        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Analysis.eJoint class from the given support condition and displacement and joint forces.
        /// </summary>
        /// <param name="type">The support actionType of the joint.</param>
        /// <param name="displacements">The corresponding initial displacements at the joint or support setlments.</param>
        /// <param name="loads"> The foces that act on the joint.</param>
        public eJoint(eJointType jointType, eABeam beam, double[] displacements, double[] loads)
        {
            this.beam = beam;
            this.type = jointType;
            this.initialDisp = displacements;
            this.supportWidth = eUtility.Convert(300.0, eLengthUnits.mm, eUtility.SLU);
            this.loads = loads;
            this.loadObjs = new List<eLoad>();
            ResetMatrices();

            if (type == eJointType.Hinge || type == eJointType.VerticalGuidedRoller)
            {
                this.loads = new double[3];
                this.initialDisp = new double[3];
            }
            else
            {
                this.loads = new double[2];
                this.initialDisp = new double[2];
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Adds a concetrated force or moment for VerticalGuidedRoller and Hindges joints.
        /// </summary>
        /// <param name="Load">The load to be added.</param>
        /// <param name="OnRightSide">Indicates wheither the load is added on the right side or on the left side of the joint.
        /// This parameter used in case of vertical guided roller for force and internal hidge for mement. </param>
        public void AddLoad(eLoad Load, bool OnRightSide = true)
        {
            if (Load.LoadType == eLoadType.ConcentratedForce)
            {
                if (type == eJointType.VerticalGuidedRoller)
                {
                    if (OnRightSide)
                        loads[2] += Load.Magnitude;
                    else
                        loads[1] += Load.Magnitude;
                }
                else
                    loads[0] += Load.Magnitude;
                loadObjs.Add(Load);
            }
            else if (Load.LoadType == eLoadType.ConcentratedMoment)
            {
                if (type == eJointType.Hinge)
                {
                    if (OnRightSide)
                        loads[2] += Load.Magnitude;
                    else
                        loads[1] += Load.Magnitude;
                }
                else if (type == eJointType.VerticalGuidedRoller)
                    loads[0] += Load.Magnitude;
                else
                    loads[1] += Load.Magnitude;
                loadObjs.Add(Load);
            }

            else
                throw new Exception("Only concentrated loads are allowed on joint.");
        }

        /// <summary>
        /// Adds a concetrated force or moment for joints other than VerticalGuidedRoller and Hindges.
        /// </summary>
        /// <param name="Load">The load to be added.</param>
        public void AddLoad(eLoad Load)
        {
            if (Load.LoadType == eLoadType.ConcentratedForce)
                loads[0] += Load.Magnitude;
            else if (Load.LoadType == eLoadType.ConcentratedMoment)
                loads[1] += Load.Magnitude;
            else
                throw new Exception("Only concentrated loads are allowed on joint.");
            this.loadObjs.Add(Load);
        }

        public void RemoveLoad(eLoad Load)
        {
            if (this.type == eJointType.VerticalGuidedRoller || this.type == eJointType.Hinge)
                throw new NotImplementedException("Veritical guided roller and hinge loads have not yet been handled.");

            if (!loadObjs.Contains(Load))
                return;

            if (Load.LoadType == eLoadType.ConcentratedForce)
                loads[0] -= Load.Magnitude;
            else if (Load.LoadType == eLoadType.ConcentratedMoment)
                loads[1] -= Load.Magnitude;
            else
                throw new Exception("Only concentrated loads are allowed on joint.");
            this.loadObjs.Remove(Load);
        }

        public void RemoveAllLoads()
        {
            if (this.type == eJointType.VerticalGuidedRoller || this.type == eJointType.Hinge)
                throw new NotImplementedException("Veritical guided roller and hinge loads have not yet been handled.");

            this.loads = new double[2];
            this.loadObjs = new List<eLoad>();
        }

        #endregion

        #region Events
        /// <summary>
        /// Occurs when the type of the joint is changed.
        /// </summary>
        public event eJointChangedEventHandler TypeChanged;

        /// <summary>
        /// Fires the 'TypeChanged' event.
        /// </summary>
        public void OnTypeChanged()
        {
            if (TypeChanged != null)
            {
                this.TypeChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Occurs when the support width of the joint is changed.
        /// </summary>
        public event eJointChangedEventHandler SupportWidthChanged;
        private eABeam beam;
        private List<eLoad> loadObjs;

        /// <summary>
        /// Fires the 'SupportWidthChanged' event.
        /// </summary>
        public void OnSupportWidthChanged()
        {
            if (this.SupportWidthChanged != null)
            {
                this.SupportWidthChanged(this, new EventArgs());
            }
        }
        #endregion

    }
}
