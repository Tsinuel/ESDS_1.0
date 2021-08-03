using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS;
using ESADS.Code;
using ESADS.Mechanics.Design;
using ESADS.Mechanics.Design.Beam;

namespace ESADS.Mechanics.Analysis.Beam
{
    /// <summary>
    /// Represents 2D Structural anaysis element for beam joint.
    /// </summary>
    public class eAMember
    {
        #region Fields
        /// <summary>
        /// Holds a value for pupblic property 'Forces'.
        /// </summary>
        private double[] forces;
        /// <summary>
        /// Holds a value for pupblic property 'Length'.
        /// </summary>
        private double length;
        /// <summary>
        /// Holds a value for pupblic property 'MSM'.
        /// </summary>
        private double[,] mSM;
        /// <summary>
        /// Holds a value for pupblic property 'NEJoint'.
        /// </summary>
        private eJoint nEJoint;
        /// <summary>
        /// Holds a value for pupblic property 'FEJoint'.
        /// </summary>
        private eJoint fEJoint;
        /// <summary>
        /// Holds a value for pupblic property 'Displacements'.
        /// </summary>
        private double[] displacements;
        /// <summary>
        /// Holds a value for pupblic property 'Loads'.
        /// </summary>
        private List<eLoad> loads;
        private double[] fEForces;
        private eRectangularLoad selfWeight;
        /// <summary>
        /// Holds the value of the 'Beam' property.
        /// </summary>
        private eABeam beam;
        /// <summary>
        /// Holds the design joint connected to this joint.
        /// </summary>
        private eDMember member_Design;
        private double start;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the near end joint of the joint.
        /// </summary>
        public eJoint NEJoint
        {
            get
            {
                return nEJoint;
            }
        }

        /// <summary>
        /// Gets the far end joint of the joint.
        /// </summary>
        public eJoint FEJoint
        {
            get
            {
                return fEJoint;
            }
        }
        /// <summary>
        /// Gets the EI value of the member.
        /// </summary>
        public double EI
        {
            get
            {
                if (this.beam.UseConstantEI)
                    return 1.0;
                else if (member_Design.Section.UseNominal_EI)
                    return member_Design.Section.Nominal_EI;
                else
                    return member_Design.Section.GetMomentOfInertia() * member_Design.Beam.Concrete.E;
            }
        }
        /// <summary>
        /// Gets the memeber stiffnes matrix of the joint.
        /// </summary>
        public double[,] MSM
        {
            get
            {
                return mSM;
            }
        }

        /// <summary>
        /// Gets or sets the load in this joint.
        /// </summary>
        public List<eLoad> Loads
        {
            get
            {
                return loads;
            }
            set
            {
                loads = value;
            }
        }

        /// <summary>
        /// Gets or sets the length of this joint
        /// </summary>
        public double Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
            }
        }

        /// <summary>
        /// Gets the Member force.
        /// </summary>
        public double[] Forces
        {
            get
            {
                return forces;
            }
        }

        /// <summary>
        /// Gets the displcements of this joint.
        /// </summary>
        public double[] Displacements
        {
            get
            {
                return displacements;
            }
            set
            {
                displacements = value;
            }
        }

        /// <summary>
        /// Gets the beam in which the joint resides.
        /// </summary>
        public eABeam Beam
        {
            get
            {
                return beam;
            }
        }

        /// <summary>
        /// Gets or sets the design component of a specific joint
        /// </summary>
        public eDMember Member_Design
        {
            get
            {
                return member_Design;
            }
            set
            {
                member_Design = value;
            }
        }

        public double Start
        {
            get
            {
                return this.start;
            }
        }

        public double End
        {
            get
            {
                return this.start + this.length;
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Analysis.eAMember class from the given basic parameters.
        /// </summary>
        /// <param name="nEJoint">Near end joint of the joint.</param>
        /// <param name="fEJoint">Far end joint of the joint.</param>
        /// <param name="length">Length of the joint.</param>
        /// <param name="EI">Flexural stiffness of the joint.</param>
        public eAMember(eJoint nEJoint, eJoint fEJoint, double length, eABeam beam)
        {
            this.beam = beam;
            this.nEJoint = nEJoint;
            this.fEJoint = fEJoint;
            this.length = length;
            this.loads = new List<eLoad>();
            this.start = 0.0;

            foreach (var m in beam.Members)
                this.start += m.length;

            this.member_Design = new eDMember(this.beam.Beam_Design, this);

            this.FillSpanType();

            this.nEJoint.TypeChanged += new eJointChangedEventHandler(Joint_TypeChanged);
            this.fEJoint.TypeChanged += new eJointChangedEventHandler(Joint_TypeChanged);
            this.nEJoint.SupportWidthChanged += new eJointChangedEventHandler(Joint_SupportWidthChanged);
            this.fEJoint.SupportWidthChanged += new eJointChangedEventHandler(Joint_SupportWidthChanged);
        }
        #endregion

        #region Methods

        private void Joint_SupportWidthChanged(object sender, EventArgs e)
        {
            switch (member_Design.SpanType)
            {
                case eSpanType.SimplySupported:
                    {
                        double clrDist = Length - nEJoint.SupportWidth / 2.0 - fEJoint.SupportWidth / 2.0;
                        if (Member_Design.SupportSxn_Left != null && Member_Design.SupportSxn_Right != null)
                            this.Member_Design.EffectiveSpanLength = Math.Min(clrDist + Member_Design.SupportSxn_Left.EffectiveDepth + Member_Design.SupportSxn_Right.EffectiveDepth, Length);
                        else
                            this.Member_Design.EffectiveSpanLength = Length;
                        break;
                    }
                case eSpanType.Cantilever:
                    {
                        if (beam.Members.Count == 1)
                        {
                            if (nEJoint.Type == eJointType.Fixed)
                            {
                                member_Design.EffectiveSpanLength = Length - nEJoint.SupportWidth / 2.0;
                            }
                            else
                            {
                                member_Design.EffectiveSpanLength = Length - fEJoint.SupportWidth / 2.0;
                            }
                        }
                        else
                        {
                            member_Design.EffectiveSpanLength = Length;
                        }
                        break;
                    }
                case eSpanType.EndSpan:
                    {
                        member_Design.EffectiveSpanLength = Length;
                        break;
                    }
                case eSpanType.InteriorSpan:
                    {
                        this.Member_Design.EffectiveSpanLength = Length;
                        break;
                    }
            }
        }

        private void FillSpanType()
        {
            if (beam.Members.Count == 0)
            {
                if ((nEJoint.Type == eJointType.Pin || nEJoint.Type == eJointType.Roller) &&
                    (fEJoint.Type == eJointType.Pin || fEJoint.Type == eJointType.Roller))
                {
                    member_Design.SetSpanType(eSpanType.SimplySupported);
                }
                else if (((nEJoint.Type == eJointType.Roller || nEJoint.Type == eJointType.Pin) && fEJoint.Type == eJointType.Fixed) ||
                    (fEJoint.Type == eJointType.Roller || fEJoint.Type == eJointType.Pin) && nEJoint.Type == eJointType.Fixed)
                {
                    member_Design.SetSpanType(eSpanType.EndSpan);
                }
                else if (((nEJoint.Type == eJointType.Free || nEJoint.Type == eJointType.Continious) && fEJoint.Type == eJointType.Fixed) ||
                    (fEJoint.Type == eJointType.Free || fEJoint.Type == eJointType.Continious) && nEJoint.Type == eJointType.Fixed)
                {
                    member_Design.SetSpanType(eSpanType.Cantilever);
                }
                else if ((nEJoint.Type == eJointType.Fixed && fEJoint.Type == eJointType.Fixed) ||
                    (fEJoint.Type == eJointType.Fixed && nEJoint.Type == eJointType.Fixed))
                {
                    member_Design.SetSpanType(eSpanType.InteriorSpan);
                }
                else
                {
                    beam.CanBeDesigned = false;
                }
            }
            else
            {
                if ((nEJoint.Type == eJointType.Pin || nEJoint.Type == eJointType.Roller) &&
                    (fEJoint.Type == eJointType.Pin || fEJoint.Type == eJointType.Roller))
                {
                    member_Design.SetSpanType(eSpanType.EndSpan);
                }
                else if ((nEJoint.Type == eJointType.Pin || nEJoint.Type == eJointType.Roller) && fEJoint.Type == eJointType.Free)
                {
                    member_Design.SetSpanType(eSpanType.Cantilever);
                }
                else if ((nEJoint.Type == eJointType.Pin || nEJoint.Type == eJointType.Roller) && fEJoint.Type == eJointType.Fixed)
                {
                    member_Design.SetSpanType(eSpanType.InteriorSpan);
                }
                else
                {
                    beam.CanBeDesigned = false;
                }

                if (beam.CanBeDesigned)
                {
                    if (beam.Members.Count == 1)
                    {
                        if (beam.Members[0].nEJoint.Type == eJointType.Fixed)
                        {
                            beam.Members[0].Member_Design.SetSpanType(eSpanType.InteriorSpan);
                        }
                        else if (beam.Members[0].nEJoint.Type == eJointType.Free)
                        {
                            beam.Members[0].Member_Design.SetSpanType(eSpanType.Cantilever);
                        }
                        else if (beam.Members[0].nEJoint.Type == eJointType.Pin || beam.Members[0].NEJoint.Type == eJointType.Roller)
                        {
                            beam.Members[0].Member_Design.SetSpanType(eSpanType.EndSpan);
                        }
                        else
                        {
                            beam.CanBeDesigned = false;
                        }
                    }
                    else
                    {
                        if ((beam.Members[beam.Members.Count - 1].NEJoint.Type == eJointType.Pin || beam.Members[beam.Members.Count - 1].NEJoint.Type == eJointType.Roller) &&
                            (beam.Members[beam.Members.Count - 1].FEJoint.Type == eJointType.Pin || beam.Members[beam.Members.Count - 1].FEJoint.Type == eJointType.Roller))
                        {
                            beam.Members[beam.Members.Count - 1].Member_Design.SetSpanType(eSpanType.InteriorSpan);
                        }
                        else
                        {
                            beam.CanBeDesigned = false;
                        }
                    }
                }
            }
        }

        private void Joint_TypeChanged(object sender, EventArgs e)
        {
            if (beam.Members.Count == 1)
            {
                if ((nEJoint.Type == eJointType.Pin || nEJoint.Type == eJointType.Roller) &&
                    (fEJoint.Type == eJointType.Pin || fEJoint.Type == eJointType.Roller))
                {
                    member_Design.SetSpanType(eSpanType.SimplySupported);
                }
                else if (((nEJoint.Type == eJointType.Roller || nEJoint.Type == eJointType.Pin) && fEJoint.Type == eJointType.Fixed) ||
                    (fEJoint.Type == eJointType.Roller || fEJoint.Type == eJointType.Pin) && nEJoint.Type == eJointType.Fixed)
                {
                    member_Design.SetSpanType(eSpanType.EndSpan);
                }
                else if (((nEJoint.Type == eJointType.Free || nEJoint.Type == eJointType.Continious) && fEJoint.Type == eJointType.Fixed) ||
                    (fEJoint.Type == eJointType.Free || fEJoint.Type == eJointType.Continious) && nEJoint.Type == eJointType.Fixed)
                {
                    member_Design.SetSpanType(eSpanType.Cantilever);
                }
                else if (((nEJoint.Type == eJointType.Fixed || nEJoint.Type == eJointType.VerticalRoller) && fEJoint.Type == eJointType.Fixed) ||
                    (fEJoint.Type == eJointType.Fixed || fEJoint.Type == eJointType.VerticalRoller) && nEJoint.Type == eJointType.Fixed)
                {
                    member_Design.SetSpanType(eSpanType.InteriorSpan);
                }
                else
                {
                    beam.CanBeDesigned = false;
                }
            }
            else if (beam.Members.IndexOf(this) == 0 || beam.Members.IndexOf(this) == beam.Members.Count - 1)
            {
                if ((nEJoint.Type == eJointType.Pin || nEJoint.Type == eJointType.Roller) &&
                    (fEJoint.Type == eJointType.Pin || fEJoint.Type == eJointType.Roller))
                {
                    member_Design.SetSpanType(eSpanType.EndSpan);
                }
                else if (nEJoint.Type == eJointType.Free && (fEJoint.Type == eJointType.Pin || fEJoint.Type == eJointType.Roller) ||
                    ((nEJoint.Type == eJointType.Pin || nEJoint.Type == eJointType.Roller) && fEJoint.Type == eJointType.Free))
                {
                    member_Design.SetSpanType(eSpanType.Cantilever);
                }
                else if (nEJoint.Type == eJointType.Fixed && (fEJoint.Type == eJointType.Pin || fEJoint.Type == eJointType.Roller) ||
                    ((nEJoint.Type == eJointType.Pin || nEJoint.Type == eJointType.Roller) && fEJoint.Type == eJointType.Fixed))
                {
                    member_Design.SetSpanType(eSpanType.InteriorSpan);
                }
                else
                {
                    beam.CanBeDesigned = false;
                }
            }
            else
            {
                if ((nEJoint.Type == eJointType.Pin || nEJoint.Type == eJointType.Roller) &&
                    (fEJoint.Type == eJointType.Pin || fEJoint.Type == eJointType.Roller))
                {
                    member_Design.SetSpanType(eSpanType.InteriorSpan);
                }
                else
                {
                    beam.CanBeDesigned = false;
                }
            }
        }

        /// <summary>
        /// Fills the joint stractural stiffness matrix.
        /// </summary>
        public void FillMSM()
        {
            mSM = new double[4, 4] { { 12 * EI / Math.Pow(length, 3), 6 * EI/ Math.Pow(length, 2), -12 * EI / Math.Pow(length, 3), 6 * EI / Math.Pow(length, 2) }, 
                                     { 6 * EI / Math.Pow(length, 2), 4 * EI / length, -6 * EI/ Math.Pow(length, 2), 2 * EI /length }, 
                                     {-12 * EI/ Math.Pow(length, 3), -6 * EI/ Math.Pow(length, 2), 12 * EI / Math.Pow(length, 3), -6 * EI / Math.Pow(length, 2) }, 
                                     { 6 * EI / Math.Pow(length, 2), 2 * EI / length, -6 * EI / Math.Pow(length, 2), 4 * EI / length } };
        }

        /// <summary>
        /// Returns Fixed end force for the joint for the given loading condition.
        /// </summary>
        public void FillFixedEndForces()
        {
            fEForces = new double[4];

            for (int j = 0; j < loads.Count; j++)
            {
                loads[j].FillFixedEndForces();
                for (int i = 0; i < 4; i++)
                    fEForces[i] += loads[j].FixedEndForces[i];
            }
            FillFEF_forJoints();
        }

        /// <summary>
        /// Fills the fixed end forces into the joints so considering those in unconstrained directions as load and those in constrained directions as reactions.
        /// </summary>
        private void FillFEF_forJoints()
        {
            switch (nEJoint.Type)
            {
                case eJointType.Continious:
                    nEJoint.FE_Forces[0] += -fEForces[0] + nEJoint.Loads[0];
                    nEJoint.FE_Forces[1] += -fEForces[1] + nEJoint.Loads[1];
                    break;
                case eJointType.Free:
                    nEJoint.FE_Forces[0] += -fEForces[0] + nEJoint.Loads[0];
                    nEJoint.FE_Forces[1] += -fEForces[1] + nEJoint.Loads[1];
                    break;
                case eJointType.Hinge:
                    nEJoint.FE_Forces[0] += -fEForces[0] + nEJoint.Loads[0];
                    nEJoint.FE_Forces[2] += -fEForces[1] + nEJoint.Loads[2];
                    break;
                case eJointType.Pin:
                    nEJoint.FE_Forces[0] += -fEForces[1] + nEJoint.Loads[0];
                    break;
                case eJointType.Roller:
                    nEJoint.FE_Forces[0] += -fEForces[1] + nEJoint.Loads[0];
                    break;
                case eJointType.VerticalGuidedRoller:
                    nEJoint.FE_Forces[2] += -fEForces[0] + nEJoint.Loads[2];
                    nEJoint.FE_Forces[0] += -fEForces[1] + nEJoint.Loads[0];
                    break;
                case eJointType.VerticalRoller:
                    nEJoint.FE_Forces[0] += -fEForces[0] + nEJoint.Loads[0];
                    break;
            }
            switch (fEJoint.Type)
            {
                case eJointType.Continious:
                    fEJoint.FE_Forces[0] += -fEForces[2] + fEJoint.Loads[0];
                    fEJoint.FE_Forces[1] += -fEForces[3] + fEJoint.Loads[1];
                    break;
                case eJointType.Free:
                    fEJoint.FE_Forces[0] += -fEForces[2] + fEJoint.Loads[0];
                    fEJoint.FE_Forces[1] += -fEForces[3] + fEJoint.Loads[1];
                    break;
                case eJointType.Hinge:
                    fEJoint.FE_Forces[0] += -fEForces[2] + fEJoint.Loads[0];
                    fEJoint.FE_Forces[1] += -fEForces[3] + fEJoint.Loads[1];
                    break;
                case eJointType.Pin:
                    fEJoint.FE_Forces[0] += -fEForces[3] + fEJoint.Loads[0];
                    break;
                case eJointType.Roller:
                    fEJoint.FE_Forces[0] += -fEForces[3] + fEJoint.Loads[0];
                    break;
                case eJointType.VerticalGuidedRoller:
                    fEJoint.FE_Forces[1] += -fEForces[2] + fEJoint.Loads[1];
                    fEJoint.FE_Forces[0] += -fEForces[3] + fEJoint.Loads[0];
                    break;
                case eJointType.VerticalRoller:
                    fEJoint.FE_Forces[0] += -fEForces[2] + fEJoint.Loads[0];
                    break;
            }
        }

        /// <summary>
        /// Fills the joint end froces.
        /// </summary>
        public void FillForces()
        {
            forces = new double[4];
            FillMemberDisplacements();
            forces = eMath.Superpose(eMath.Multiply(mSM, displacements), fEForces);
            FillReactions();
        }

        /// <summary>
        /// Returns the Shear at the specified location in the joint relative to the joint coordinate.
        /// </summary>
        /// <param name="x">The location of the section from near end of the joint.</param>
        /// <param name="sectionType">Value indication weather the section is imediately from the left or right.</param>
        /// <returns></returns>
        public double GetShearAt(double x, eSectionAt sectionType = eSectionAt.FromLeft)
        {
            if (sectionType == eSectionAt.FromRight)
                x += 0.00001;
            double load = 0;

            foreach (eLoad l in loads)
            {
                if (l.LoadType != eLoadType.ConcentratedMoment)
                    load += l.ConcentrateAt(x);
            }

            return forces[0] - load;
        }

        /// <summary>
        /// Returns the Moment at the specified location in the joint relative to the joint coordinate.
        /// </summary>
        /// <param name="x">The location if the section from near end of the joint.</param>
        /// <param name="sectionType">Value indication weather the section is imediately from the left or right.</param>
        /// <returns></returns>
        public double GetMomentAt(double x, eSectionAt sectionType = eSectionAt.FromLeft)
        {
            if (sectionType == eSectionAt.FromRight)
                x += 0.0000000000000001;
            double loadMom = 0;

            foreach (eLoad l in loads)
                loadMom += l.GetCentroidAt(x) * l.ConcentrateAt(x);

            return -loadMom + x * forces[0] - forces[1];
        }

        /// <summary>
        /// Returns the maximum shear in the joint and fills its dependecies in out parameters.
        /// </summary>
        /// <param name="MaxNegative">Parameter to be filled with maximum negative shear.</param>
        /// <param name="X_maxPositive">Parameter to be filled with the location of maximum posetive shear.</param>
        /// <param name="X_maxNegative">Parameter to be filled with the location of maximum negative shear.</param>
        /// <returns></returns>
        public double GetMaxShear(out double MaxNegative, out double X_maxPositive, out double X_maxNegative)
        {
            double maxPos = 0;
            MaxNegative = 0;
            X_maxNegative = X_maxPositive = 0;

            double[] intervals = GetSectionsInterval();

            for (int i = 0; i < intervals.Length - 1; i++)
            {
                double x1, x2, x3;
                double y1, y2, y3;
                double tempMaxNeg, tempMaxPos;
                double tempXneg, tempXpos;

                x1 = intervals[i];
                x3 = intervals[i + 1];
                x2 = 0.5 * (x1 + x3);

                y1 = GetShearAt(x1, eSectionAt.FromRight);
                y2 = GetShearAt(x2);
                y3 = GetShearAt(x3);

                tempMaxPos = eMath.GetMaxOf(x1, y1, x2, y2, x3, y3, out tempMaxNeg, out tempXneg, out tempXpos);

                if (maxPos < tempMaxPos)
                {
                    maxPos = tempMaxPos;
                    X_maxPositive = tempXpos;
                }
                if (MaxNegative > tempMaxNeg)
                {
                    MaxNegative = tempMaxNeg;
                    X_maxNegative = tempXneg;
                }
            }

            return maxPos;
        }

        /// <summary>
        /// Returns the maximum moment in the joint and fills its dependecies in out parameters.
        /// </summary>
        /// <param name="MaxNegative">Parameter to be filled with maximum negative moment.</param>
        /// <param name="X_maxPositive">Parameter to be filled with the location of maximum posetive moment.</param>
        /// <param name="X_maxNegative">Parameter to be filled with the location of maximum negative moment.</param>
        /// <returns></returns>
        public double GetMaxMoment(out double MaxNegative, out double X_maxPositive, out double X_maxNegative)
        {
            double maxPos = 0;
            MaxNegative = 0;
            X_maxNegative = X_maxPositive = 0;

            double[] intervals = GetSectionsInterval();

            for (int i = 0; i < intervals.Length - 1; i++)
            {
                double x1, x2, x3, x4;
                double y1, y2, y3, y4;
                double tempMaxNeg, tempMaxPos;
                double tempXneg, tempXpos;

                x1 = intervals[i];
                x4 = intervals[i + 1];
                x2 = x1 + (1d / 3d) * (x4 - x1);
                x3 = x1 + (2d / 3d) * (x4 - x1);

                y1 = GetMomentAt(x1, eSectionAt.FromRight);
                y2 = GetMomentAt(x2);
                y3 = GetMomentAt(x3);
                y4 = GetMomentAt(x4);

                tempMaxPos = eMath.GetMaxOf(x1, y1, x2, y2, x3, y3, x4, y4, out tempMaxNeg, out tempXneg, out tempXpos);

                if (maxPos < tempMaxPos)
                {
                    maxPos = tempMaxPos;
                    X_maxPositive = tempXpos;
                }
                if (MaxNegative > tempMaxNeg)
                {
                    MaxNegative = tempMaxNeg;
                    X_maxNegative = tempXneg;
                }
            }

            return maxPos;

        }

        /// <summary>
        /// Returns the intervals of all possible section in the joint.
        /// </summary>
        /// <returns>The retured array contains the distance of each section starting and ending point relativeto the near end of the joint.</returns>
        public double[] GetSectionsInterval()
        {
            List<double> intervals = new List<double>();

            if (loads.Count > 0)
            {
                //merges all the intervals due to each load.
                for (int i = 0; i < this.loads.Count; i++)
                {
                    double[] subIntervals = loads[i].GetSectionsInterval();

                    for (int j = 0; j < subIntervals.Length; j++)
                    {
                        if (!intervals.Contains(subIntervals[j]))
                            intervals.Add(subIntervals[j]);
                    }
                }
            }
            else
            {
                intervals.Add(0);
                intervals.Add(this.length);
            }
            //Sorts the interval
            intervals.Sort();

            return (double[])intervals.ToArray();
        }

        /// <summary>
        /// Fills displacement for the joint.
        /// </summary>
        private void FillMemberDisplacements()
        {
            displacements = new double[4];
            switch (nEJoint.Type)
            {
                case eJointType.VerticalGuidedRoller:
                    displacements[0] += nEJoint.FinalDisps[2];
                    displacements[1] += nEJoint.FinalDisps[0];
                    break;
                case eJointType.Hinge:
                    displacements[0] += nEJoint.FinalDisps[0];
                    displacements[1] += nEJoint.FinalDisps[2];
                    break;
                default:
                    displacements[0] += nEJoint.FinalDisps[0];
                    displacements[1] += nEJoint.FinalDisps[1];
                    break;
            }
            switch (fEJoint.Type)
            {
                case eJointType.VerticalGuidedRoller:
                    displacements[2] += fEJoint.FinalDisps[1];
                    displacements[3] += fEJoint.FinalDisps[0];
                    break;
                case eJointType.Hinge:
                    displacements[2] += fEJoint.FinalDisps[0];
                    displacements[3] += fEJoint.FinalDisps[1];
                    break;
                default:
                    displacements[2] += fEJoint.FinalDisps[0];
                    displacements[3] += fEJoint.FinalDisps[1];
                    break;
            }
        }

        /// <summary>
        /// Adds Concentrated force load to the joint.
        /// </summary>
        /// <param name="Magnitude">Magnitude of the load.</param>
        /// <param name="Start">The starting distance of the load from the near end.</param>
        public void AddConcForce(double Magnitude, double Start, eActionType ActionType)
        {
            if (Start == 0)
                nEJoint.AddLoad(new eConcentratedForce(Magnitude, nEJoint, ActionType));
            if (Start == this.length)
                fEJoint.AddLoad(new eConcentratedForce(Magnitude, fEJoint, ActionType));
            else
                loads.Add(new eConcentratedForce(Magnitude, this, Start, ActionType));
        }

        /// <summary>
        /// Adds Concentrated moment load to the joint.
        /// </summary>
        /// <param name="Magnitude">Magnitude of the load.</param>
        /// <param name="Start">The distance between the starting point of the load and the near end.</param>
        public void AddConcMoment(double Magnitude, double Start, eActionType ActionType)
        {
            if (Start == 0)
                nEJoint.AddLoad(new eConcentratedMoment(Magnitude, nEJoint, ActionType));
            if (Start == this.length)
                fEJoint.AddLoad(new eConcentratedMoment(Magnitude, fEJoint, ActionType));
            else
                loads.Add(new eConcentratedMoment(Magnitude, this, Start, ActionType));

        }

        /// <summary>
        /// Adds uniformly distributed rectangular load to the joint.
        /// </summary>
        /// <param name="Magnitude">Magnitude of the load.</param>
        /// <param name="Start">The distance between the starting point of the load and the near end.</param>
        /// <param name="End">The distance between the ending point of the load and the far end.</param>
        /// <returns></returns>
        public eRectangularLoad AddRectLoad(double Magnitude, double Start, double End, eActionType ActionType)
        {
            loads.Add(new eRectangularLoad(Magnitude, Start, End, this, ActionType));
            return (eRectangularLoad)loads[loads.Count - 1];
        }

        /// <summary>
        /// Adds uniformly distributed rectangular load to the joint.
        /// </summary>
        /// <param name="Magnitude">Magnitude of the load.</param>
        /// <param name="Start">The distance between the starting point of the load and the near end.</param>
        /// <param name="End">The distance between the ending point of the load and the far end.</param>
        /// <param name="Orientation">The alignment of the load.</param>
        /// <returns></returns>
        public eTriangularLoad AddTriangularLoad(double Magnitude, double Start, double End, eActionType ActionType, eTriangularLoadOrientation Orientation = eTriangularLoadOrientation.LeftToRight)
        {
            loads.Add(new eTriangularLoad(Magnitude, Start, End, this, Orientation, ActionType));
            return (eTriangularLoad)loads[loads.Count - 1];
        }

        /// <summary>
        /// Gets the distance from the start of the joint where the shear force is zero within a given interval.
        /// </summary>
        /// <param name="X_min">The left boundary of the interval</param>
        /// <param name="X_max">The right boundary of the interval.</param>
        public double[] GetShearZero(double X_min, double X_max)
        {
            double x1, x2, x3, x4;
            double y1, y2, y3, y4;

            x1 = X_min;
            x4 = X_max;

            x2 = x1 + (1.0 / 3.0) * (X_max - X_min);
            x3 = x1 + (2.0 / 3.0) * (X_max - X_min);

            y1 = GetShearAt(x1, eSectionAt.FromRight);
            y2 = GetShearAt(x2);
            y3 = GetShearAt(x3);
            y4 = GetShearAt(x4);

            return eMath.GetZeroOf(x1, y1, x2, y2, x3, y3, x4, y4, X_min, X_max);
        }

        /// <summary>
        /// Gets the distance from the start of the joint where the moment is zero within a given interval.
        /// </summary>
        /// <param name="X_min">The left boundary of the interval.</param>
        /// <param name="X_max">The right boundary of the interval.</param>
        public double[] GetMomentZero(double X_min, double X_max)
        {
            double x1, x2, x3, x4;
            double y1, y2, y3, y4;

            x1 = X_min;
            x4 = X_max;

            x2 = x1 + (1.0 / 3.0) * (X_max - X_min);
            x3 = x1 + (2.0 / 3.0) * (X_max - X_min);

            y1 = Math.Round(GetMomentAt(x1, eSectionAt.FromRight), 8);
            y2 = Math.Round(GetMomentAt(x2), 8);
            y3 = Math.Round(GetMomentAt(x3), 8);
            y4 = Math.Round(GetMomentAt(x4), 8);

            return eMath.GetZeroOf(x1, y1, x2, y2, x3, y3, x4, y4, X_min, X_max);
        }

        /// <summary>
        /// Returns the absolute maximum Bending moment on this joint.
        /// </summary>
        /// <returns></returns>
        public double GetMaxMoment()
        {
            double negM;
            double posX;
            double negX;
            return Math.Max(GetMaxMoment(out negM, out posX, out negX), Math.Abs(negM));
        }

        /// <summary>
        /// Gets the maximum moment between two interval points assuming that the load doesn't vary in actionType between the intervals.
        /// </summary>
        public double GetMaxMoment(double x1, double x2, out double maxNegative, out double XatMaxNeg, out double XatMaxPos)
        {
            if (x1 < 0 || x1 > this.length || x2 < 0 || x2 > this.length || x2 < x1)
                throw new Exception("Length out of bounds.");

            double y1, y2, y3, y4;
            double x3, x4;

            x3 = x1 + (x2 - x1) / 3.0;
            x4 = x3 + (x2 - x1) / 3.0;

            y1 = Math.Round(GetMomentAt(x1, eSectionAt.FromRight), 8);
            y2 = Math.Round(GetMomentAt(x2), 8);
            y3 = Math.Round(GetMomentAt(x3), 8);
            y4 = Math.Round(GetMomentAt(x4), 8);

            return eMath.GetMaxOf(x1, y1, x3, y3, x2, y2, out maxNegative, out XatMaxNeg, out XatMaxPos);
        }

        /// <summary>
        /// Returns the Maximum absolute bending moment in the specified range.
        /// </summary>
        /// <param name="x1">The left bound from the end of the joint.</param>
        /// <param name="x2">The right bound from the end of the joint.</param>
        /// <returns></returns>
        public double GetMaxMoment(double x1, double x2)
        {
            double MaxNegative, XatMaxNeg, XatMaxPos;
            return Math.Max(GetMaxMoment(x1, x2, out MaxNegative, out XatMaxNeg, out XatMaxPos), Math.Abs(MaxNegative));
        }

        /// <summary>
        /// Returns the absolute maximum shear on this joint.
        /// </summary>
        /// <returns></returns>
        public double GetMaxShear()
        {
            double negV;
            double posX;
            double negX;
            return Math.Max(GetMaxShear(out negV, out posX, out negX), Math.Abs(negV));
        }

        /// <summary>
        /// Gets the maximum shear in a given interval.
        /// </summary>
        public double GetMaxShear(double x1, double x2, out double MaxNegative, out double XatMaxNeg, out double XatMaxPos)
        {
            if (x1 < 0 || x1 > this.length || x2 < 0 || x2 > this.length || x2 < x1)
                throw new Exception("Length out of bounds.");

            double y1, y2, y3, y4;
            double x3, x4 = x2;

            x2 = x1 + (x4 - x1) / 3.0;
            x3 = x2 + (x4 - x1) / 3.0;

            y1 = GetShearAt(x1, eSectionAt.FromRight);
            y2 = GetShearAt(x2);
            y3 = GetShearAt(x3);
            y4 = GetShearAt(x4);

            return eMath.GetMaxOf(x1, y1, x2, y2, x3, y3, x4, y4, out MaxNegative, out XatMaxNeg, out XatMaxPos);
        }

        /// <summary>
        /// Returns the Maximum absolute shear in the specified range.
        /// </summary>
        /// <param name="x1">The left bound from the end of the joint.</param>
        /// <param name="x2">The right bound from the end of the joint.</param>
        /// <returns></returns>
        public double GetMaxShear(double x1, double x2)
        {
            double MaxNegative, XatMaxNeg, XatMaxPos;
            return Math.Max(GetMaxShear(x1, x2, out MaxNegative, out XatMaxNeg, out XatMaxPos), Math.Abs(MaxNegative));
        }

        /// <summary>
        /// Fills the reaction of this joint to the adjusent restrained supports directions.
        /// </summary>
        public void FillReactions()
        {
            switch (nEJoint.Type)
            {
                case eJointType.Fixed:
                    nEJoint.Reactions[0] += this.forces[0] + nEJoint.Loads[0];
                    nEJoint.Reactions[1] += this.forces[1] + nEJoint.Loads[1];
                    break;
                case eJointType.Pin:
                    nEJoint.Reactions[0] += this.forces[0] + nEJoint.Loads[0];
                    break;
                case eJointType.VerticalRoller:
                    nEJoint.Reactions[0] += this.forces[1] + nEJoint.Loads[0];
                    break;
                case eJointType.Roller:
                    nEJoint.Reactions[0] += this.forces[0] + nEJoint.Loads[0];
                    break;
            }
            switch (fEJoint.Type)
            {
                case eJointType.Fixed:
                    fEJoint.Reactions[0] += this.forces[2] + fEJoint.Loads[0];
                    fEJoint.Reactions[1] += this.forces[3] + fEJoint.Loads[1];
                    break;
                case eJointType.Pin:
                    fEJoint.Reactions[0] += this.forces[2] + fEJoint.Loads[0];
                    break;
                case eJointType.VerticalRoller:
                    fEJoint.Reactions[0] += this.forces[3] + fEJoint.Loads[0];
                    break;
                case eJointType.Roller:
                    fEJoint.Reactions[0] += this.forces[2] + fEJoint.Loads[0];
                    break;
            }
        }

        /// <summary>
        /// Gets the extreme moment between the given coordinated measured from the left side of the joint. It retains the sign of the mement.
        /// </summary>
        /// <param name="X1">The first coordinate of the interval measured from the left side of the joint.</param>
        /// <param name="X2">The end of the interval measured fromt the left side of the joint.</param>
        public double GetExtremeMoment(double X1, double X2)
        {
            double MaxNegative, X_maxPositive, X_maxNegative;

            double max = GetMaxMoment(X1, X2, out MaxNegative, out X_maxNegative, out X_maxPositive);

            if (max > Math.Abs(MaxNegative))
                return max;
            else
                return MaxNegative;
        }

        /// <summary>
        /// Gets the extreme moment between the given coordinated measured from the left side of the joint. It retains the sign of the mement.
        /// </summary>
        /// <param name="X1">The first coordinate of the interval measured from the left side of the joint.</param>
        /// <param name="X2">The end of the interval measured fromt the left side of the joint.</param>
        /// <param name="X_max">The x coordinate where the maximum moment occured.</param>
        public double GetExtremeMoment(double X1, double X2, out double X_max)
        {
            double MaxNegative, X_maxPositive, X_maxNegative;

            double max = GetMaxMoment(X1, X2, out MaxNegative, out X_maxNegative, out X_maxPositive);

            if (max > Math.Abs(MaxNegative))
            {
                X_max = X_maxPositive;
                return max;
            }
            else
            {
                X_max = X_maxNegative;
                return MaxNegative;
            }
        }

        /// <summary>
        /// Adds the selfweight of the joint to its load list.
        /// </summary>
        internal void AddSelfWeight()
        {
            if (this.selfWeight != null)
                this.loads.Remove(this.selfWeight);
            double mag = this.member_Design.Width * this.member_Design.Depth * this.member_Design.Concrete.UnitWeight;
            this.selfWeight = new eRectangularLoad(mag, 0.0, 0.0, this, eActionType.Permanent);
            this.loads.Add(selfWeight);
        }

        private void RefreshSelfWeight()
        {
            if (selfWeight != null)
                selfWeight.UnfactoredMagnitude = this.member_Design.Width * this.member_Design.Depth * this.member_Design.Concrete.UnitWeight;
        }

        /// <summary>
        /// Removes the selfweight of the joint from its load list.
        /// </summary>
        internal void RemoveSelfWieght()
        {
            if (this.selfWeight != null)
            {
                this.loads.Remove(selfWeight);
                this.selfWeight = null;
            }
        }

        /// <summary>
        /// Gets the extreme shear force at a given section within an interval.
        /// </summary>
        /// <param name="X1">The first coordinate of the interval measured from the left side of the joint.</param>
        /// <param name="X2">The end of the interval measured fromt the left side of the joint.</param>
        public double GetExtremeShear(double X1, double X2)
        {
            double MaxNegative, X_maxPositive, X_maxNegative;

            double max = GetMaxShear(X1, X2, out MaxNegative, out X_maxNegative, out X_maxPositive);

            if (max > Math.Abs(MaxNegative))
                return max;
            else
                return MaxNegative;
        }

        /// <summary>
        /// Gets the location where the magnitude of the shear is equal to a certain value within an interval.
        /// </summary>
        public double[] GetXofShear(double Shear, double X_min, double X_max)
        {
            double x1, x2, x3, x4;
            double y1, y2, y3, y4;

            x1 = X_min;
            x4 = X_max;

            x2 = x1 + (1.0 / 3.0) * (X_max - X_min);
            x3 = x1 + (2.0 / 3.0) * (X_max - X_min);

            y1 = GetShearAt(x1, eSectionAt.FromRight) - Shear;
            y2 = GetShearAt(x2) - Shear;
            y3 = GetShearAt(x3) - Shear;
            y4 = GetShearAt(x4) - Shear;

            double[] zeros = eMath.GetZeroOf(x1, y1, x2, y2, x3, y3, x4, y4, X_min, X_max);

            y1 = GetShearAt(x1, eSectionAt.FromRight) + Shear;
            y2 = GetShearAt(x2) + Shear;
            y3 = GetShearAt(x3) + Shear;
            y4 = GetShearAt(x4) + Shear;

            double[] zeros2 = eMath.GetZeroOf(x1, y1, x2, y2, x3, y3, x4, y4, X_min, X_max);

            double[] result = new double[zeros.Length + zeros2.Length];

            for (int i = 0; i < zeros.Length; i++)
            {
                result[i] = zeros[i];
            }
            for (int i = 0, j = zeros.Length; i < zeros2.Length; i++, j++)
            {
                result[j] = zeros2[i];
            }

            return result;
        }
        #endregion
    }
}
