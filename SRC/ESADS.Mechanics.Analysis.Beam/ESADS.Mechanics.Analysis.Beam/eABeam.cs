using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Mechanics.Design;
using ESADS.Mechanics.Design.Beam;

namespace ESADS.Mechanics.Analysis.Beam
{
    /// <summary>
    /// Represents a one dimensional(X-axis) structure facing loads in only two directions(Moment and shear).
    /// </summary>
    public class eABeam
    {
        #region Fields
        /// <summary>
        /// Holds the structure stiffness matrix of the whole beam.
        /// </summary>
        private double[,] SSM;
        /// <summary>
        /// Holds the known force matrix.
        /// </summary>
        private double[] QK;
        /// <summary>
        /// Known displacement matrix.
        /// </summary>
        private double[] DK;
        /// <summary>
        /// Holds the value of the 'Joints' property.
        /// </summary>
        private List<eJoint> joints;
        /// <summary>
        /// Holds the value of the 'Members' property.
        /// </summary>
        private List<eAMember> members;
        /// <summary>
        /// Holds the calculated force values for the whole beam.
        /// </summary>
        private double[] QU;
        /// <summary>
        /// Calculated displacement values for the joints.
        /// </summary>
        private double[] DU;
        /// <summary>
        /// Part of the structure stiffness matrix at the top left.
        /// </summary>
        private double[,] K11;
        /// <summary>
        /// Part of the structure stiffness matrix at the top right.
        /// </summary>
        private double[,] K12;
        /// <summary>
        /// Part of the structure stiffness matrix at the bottom left.
        /// </summary>
        private double[,] K21;
        /// <summary>
        /// Part of the structure stiffness matrix at the bottom right.
        /// </summary>
        private double[,] K22;
        /// <summary>
        /// Holds a value for property 'AnalysisCompleted'.
        /// </summary>
        private bool analysisCompleted;
        /// <summary>
        /// Holds the value of the 'ConsiderSelfWeight' property.
        /// </summary>
        private bool considerSelfWeight;
        /// <summary>
        /// Holds the value of 'Beam_Design'
        /// </summary>
        private eDBeam beam_Design;
        /// <summary>
        /// Holds the value of 'CanBeAnalysed'.
        /// </summary>
        private bool canBeAnalysed;
        /// <summary>
        /// Holds the value of 'CanBeDesigned'.
        /// </summary>
        private bool canBeDesigned;
        private eLoadCombination loadCombination;
        private bool useConstantEI;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates new instace of a continuous beam for structural analysis.
        /// </summary>
        public eABeam()
        {
            this.beam_Design = new eDBeam(this, new eBeamSection());
            this.joints = new List<eJoint>();
            this.members = new List<eAMember>();
            this.analysisCompleted = false;
            this.considerSelfWeight = true;
            this.loadCombination = new eLoadCombination();
            this.canBeAnalysed = true;
            this.canBeDesigned = true;
            this.useConstantEI = false;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the value indicating whether the analysis is completed or not.
        /// </summary>
        public bool AnaysisCompleted
        {
            get { return analysisCompleted; }
            set
            {
                analysisCompleted = value;
                foreach (eJoint j in joints)
                    j.ResetMatrices();
            }
        }

        /// <summary>
        /// Gets all the joints involved in the beam.
        /// </summary>
        public List<eJoint> Joints
        {
            get
            {
                return joints;
            }
        }

        /// <summary>
        /// Gets all the beam members involved in the beam
        /// </summary>
        public List<eAMember> Members
        {
            get
            {
                return members;
            }
        }

        /// <summary>
        /// Gets the length of the beam.
        /// </summary>
        public double Length
        {
            get
            {
                double sum = 0;
                foreach (eAMember m in members)
                    sum += m.Length;
                return sum;
            }
        }

        /// <summary>
        /// Gets or sets the value whehter to consider the self weight of the beam or not.
        /// </summary>
        public bool ConsiderSelfWeight
        {
            get
            {
                return this.considerSelfWeight;
            }
            set
            {
                this.considerSelfWeight = value;
                if (considerSelfWeight)
                    this.AddSelfWeight();
                else
                    this.RemoveSelfWeight();
            }
        }

        /// <summary>
        /// Gets the load combination currently applied in the beam.
        /// </summary>
        public eLoadCombination LoadCombination
        {
            get
            {
                return loadCombination;
            }
        }

        /// <summary>
        /// Gets the design object of the beam.
        /// </summary>
        public eDBeam Beam_Design
        {
            get
            {
                return beam_Design;
            }
        }

        /// <summary>
        /// Gets or sets the value if the beam can be designed.
        /// </summary>
        public bool CanBeDesigned
        {
            get
            {
                return canBeDesigned;
            }
            set
            {
                canBeDesigned = value;
            }
        }

        /// <summary>
        /// Gets or sets the value if the beam can be analysed.
        /// </summary>
        public bool CanBeAnalysed
        {
            get
            {
                return canBeAnalysed;
            }
            set
            {
                canBeAnalysed = value;
            }
        }
        #endregion

        /// <summary>
        /// Gets or sets the value whether to use a constant EI value or not.
        /// </summary>
        public bool UseConstantEI
        {
            get
            {
                return this.useConstantEI;
            }
            set
            {
                this.useConstantEI = value;
            }
        }

        #region Methods
        /// <summary>
        /// Runs the analysis of the whole beam.
        /// </summary>
        public void Analyze()
        {
            if (!this.canBeAnalysed)
                throw new Exception("The beam cannot be analysed!");

            if (considerSelfWeight)
                AddSelfWeight();

            foreach (eAMember m in members)
                m.FillFixedEndForces();

            foreach (eAMember m in members)
                m.FillMSM();

            Fill_SSM_Index();

            Fill_DK();
            Fill_QK();
            Fill_SSM();
            PartitionSSM();
            
            DU = eMath.Solve(K11, eMath.Superpose(QK, eMath.Multiply(K12, DK), false));
            QU = eMath.Superpose(eMath.Multiply(K21, DU), eMath.Multiply(K22, DK));

            FillBackResults();

            foreach (eAMember m in members)
                m.FillForces();
            this.analysisCompleted = true;
        }

        /// <summary>
        /// Fills the structure stiffness matrix from all the joint stiffness'  of the beam.
        /// </summary>
        private void Fill_SSM()
        {
            int Nz, Ny, Fz, Fy;

            foreach (eAMember memb in members)
            {
                if (memb.NEJoint.Type == eJointType.Hinge)
                { 
                    Ny = memb.NEJoint.SSMIndex[0];
                    Nz = memb.NEJoint.SSMIndex[2];                   
                }
                else if (memb.NEJoint.Type == eJointType.VerticalGuidedRoller)
                {
                    Ny = memb.NEJoint.SSMIndex[2];
                    Nz = memb.NEJoint.SSMIndex[0];
                }
                else
                {
                    Ny = memb.NEJoint.SSMIndex[0];
                    Nz = memb.NEJoint.SSMIndex[1];
                }
                if (memb.FEJoint.Type == eJointType.Hinge)
                { 
                    Fy = memb.FEJoint.SSMIndex[0];
                    Fz = memb.FEJoint.SSMIndex[1];                  
                }
                else if (memb.FEJoint.Type == eJointType.VerticalGuidedRoller)
                {
                    Fy = memb.FEJoint.SSMIndex[1];
                    Fz = memb.FEJoint.SSMIndex[0];
                }
                else
                {
                    Fy = memb.FEJoint.SSMIndex[0];
                    Fz = memb.FEJoint.SSMIndex[1];
                }

                SSM[Ny, Ny] += memb.MSM[0, 0];
                SSM[Ny, Nz] += memb.MSM[0, 1];
                SSM[Ny, Fy] += memb.MSM[0, 2];
                SSM[Ny, Fz] += memb.MSM[0, 3];

                SSM[Nz, Ny] += memb.MSM[1, 0];
                SSM[Nz, Nz] += memb.MSM[1, 1];
                SSM[Nz, Fy] += memb.MSM[1, 2];
                SSM[Nz, Fz] += memb.MSM[1, 3];

                SSM[Fy, Ny] += memb.MSM[2, 0];
                SSM[Fy, Nz] += memb.MSM[2, 1];
                SSM[Fy, Fy] += memb.MSM[2, 2];
                SSM[Fy, Fz] += memb.MSM[2, 3];

                SSM[Fz, Ny] += memb.MSM[3, 0];
                SSM[Fz, Nz] += memb.MSM[3, 1];
                SSM[Fz, Fy] += memb.MSM[3, 2];
                SSM[Fz, Fz] += memb.MSM[3, 3];
            }
        }

        /// <summary>
        /// Returns the maximum moment from all the members with the specified sign.
        /// </summary>
        /// <param name="maxNegM">Parameter to be filled with the maximum negative moment.</param>
        /// <param name="maxPosM_X">Parameter to be fills with the location of maximum posetive moment.</param>
        /// <param name="maxNegM_X">Parameter to be fills with the location of maximum negative moment.</param>
        public double GetMaxMoment(out double maxNegM, out double maxPosM_X, out double maxNegM_X)
        {
            double maxPosM = 0;//Represets the maximum positive moment.                    
            double temNegM = 0;//Represets temporary  negative moment at each iteration.
            double temPosM_X = 0;//Represets the location of temporary posetive moment at each iteration.
            double temNegM_X = 0;//Represets the location of temporary negative moment at each iteration.
            maxNegM = maxPosM_X = maxNegM_X = 0;
            double totLength = 0;
            for (int i = 0; i < members.Count; i++)
            {
                double temPosM = members[i].GetMaxMoment(out temNegM,out temPosM_X,out temNegM_X);
                if (temPosM > maxPosM)
                {
                    maxPosM = temPosM;
                    maxPosM_X = totLength+ temPosM_X;
                }
                if (temNegM < maxNegM)
                {
                    maxNegM = temNegM;
                    maxNegM_X = totLength + temNegM_X;
                }
                totLength += members[i].Length;
            }
            return maxPosM;
        }

        /// <summary>
        /// Returns the maximum shear from all the members within the beam.
        /// </summary>
        /// <param name="maxNegV">Parameter to be fills with the maximum negative shear.</param>
        /// <param name="maxPosV_X">Parameter to be fills with the location of maximum posetive shear.</param>
        /// <param name="maxNegV_X">Parameter to be fills with the location of maximum negative shear.</param>
        public double GetMaxShear(out double maxNegV, out double maxPosV_X, out double maxNegV_X)
        {
            double maxPosV = 0;//Represets the maximum posetive moment.                    
            double temNegV = 0;//Represets temporary  negative moment at each iteration.
            double temPosV_X = 0;//Represets the location of temporary posetive moment at each iteration.
            double temNegV_X = 0;//Represets the location of temporary negative moment at each iteration.
            maxNegV = maxPosV_X = maxNegV_X = 0;
            double temPosV;
            double totLength = 0;
            for (int i = 0; i < members.Count; i++)
            {
                temPosV = members[i].GetMaxShear(out temNegV, out temPosV_X, out temNegV_X);
                if (temPosV > maxPosV)
                {
                    maxPosV = temPosV;
                    maxPosV_X = totLength + temPosV_X;
                }
                if (temNegV < maxNegV)
                {
                    maxNegV = temNegV;
                    maxNegV_X = totLength + temNegV_X;
                }
                totLength += members[i].Length;
            }
            return maxPosV;
        }

        /// <summary>
        /// Fills the known force matrix for the whole beam.
        /// </summary>
        private void Fill_QK()
        {
            foreach (eJoint joint in joints)
            {
                switch (joint.Type)
                {
                    case eJointType.Continious:
                        QK[joint.SSMIndex[0]] = joint.Loads[0] + joint.FE_Forces[0];
                        QK[joint.SSMIndex[1]] = joint.Loads[1] + joint.FE_Forces[1];
                        break;
                    case eJointType.Free:
                        QK[joint.SSMIndex[0]] = joint.Loads[0] + joint.FE_Forces[0];
                        QK[joint.SSMIndex[1]] = joint.Loads[1] + joint.FE_Forces[1];
                        break;
                    case eJointType.Hinge:
                        QK[joint.SSMIndex[0]] = joint.Loads[0] + joint.FE_Forces[0];
                        QK[joint.SSMIndex[1]] = joint.Loads[1] + joint.FE_Forces[1];
                        QK[joint.SSMIndex[2]] = joint.Loads[2] + joint.FE_Forces[2];
                        break;
                    case eJointType.Pin:
                        QK[joint.SSMIndex[1]] = joint.Loads[1] + joint.FE_Forces[0];
                        break;
                    case eJointType.Roller:
                        QK[joint.SSMIndex[1]] = joint.Loads[1] + joint.FE_Forces[0];
                        break;
                    case eJointType.VerticalGuidedRoller:
                        QK[joint.SSMIndex[0]] = joint.Loads[0] + joint.FE_Forces[0];
                        QK[joint.SSMIndex[1]] = joint.Loads[1] + joint.FE_Forces[1];
                        QK[joint.SSMIndex[2]] = joint.Loads[2] + joint.FE_Forces[2];
                        break;
                    case eJointType.VerticalRoller:
                        QK[joint.SSMIndex[0]] = joint.Loads[1] + joint.FE_Forces[0];
                        break;
                }
            }
        }

        /// <summary>
        /// Fills the known displacement matrix for the whole beam.
        /// </summary>
        private void Fill_DK()
        {
            int diff = QK.Length; //Is the difference between the index in DK and SSM matrices of a known displacement.

            foreach (eJoint joint in joints)
            {
                switch (joint.Type)
                {
                    case eJointType.Fixed:
                        DK[joint.SSMIndex[0] - diff] = joint.InitialDisps[0];
                        DK[joint.SSMIndex[1] - diff] = joint.InitialDisps[1];
                        break;
                    case eJointType.Pin:
                        DK[joint.SSMIndex[0] - diff] = joint.InitialDisps[0];
                        break;
                    case eJointType.Roller:
                        DK[joint.SSMIndex[0] - diff] = joint.InitialDisps[0];
                        break;
                    case eJointType.VerticalRoller:
                        DK[joint.SSMIndex[1] - diff] = joint.InitialDisps[1];
                        break;
                }
            }
        }

        /// <summary>
        /// Fills the possible displacement directions for joints that indicates their location in the structure stiffness matrix.
        /// </summary>
        private void Fill_SSM_Index()
        {
            int count = 0;

            foreach (eJoint joint in this.joints)
            {
                switch (joint.Type)
                {
                    case eJointType.Continious:
                        joint.SSMIndex[0] = count++;
                        joint.SSMIndex[1] = count++;
                        break;
                    case eJointType.Free:
                        joint.SSMIndex[0] = count++;
                        joint.SSMIndex[1] = count++;
                        break;
                    case eJointType.Hinge:
                        joint.SSMIndex[0] = count++;
                        joint.SSMIndex[1] = count++;
                        joint.SSMIndex[2] = count++;
                        break;
                    case eJointType.Pin:
                        joint.SSMIndex[1] = count++;
                        break;
                    case eJointType.Roller:
                        joint.SSMIndex[1] = count++;
                        break;
                    case eJointType.VerticalGuidedRoller:
                        joint.SSMIndex[0] = count++;
                        joint.SSMIndex[1] = count++;
                        joint.SSMIndex[2] = count++;
                        break;
                    case eJointType.VerticalRoller:
                        joint.SSMIndex[0] = count++;
                        break;
                }
            }

            QK = new double[count]; // The dimension is the same as the number of unconstrained displacement directions.
            DU = new double[QK.Length];

            foreach (eJoint joint in this.joints)
            {
                switch (joint.Type)
                {
                    case eJointType.Fixed:
                        joint.SSMIndex[0] = count++;
                        joint.SSMIndex[1] = count++;
                        break;
                    case eJointType.Pin:
                        joint.SSMIndex[0] = count++;
                        break;
                    case eJointType.Roller:
                        joint.SSMIndex[0] = count++;
                        break;
                    case eJointType.VerticalRoller:
                        joint.SSMIndex[1] = count++;
                        break;
                }
            }

            SSM = new double[count, count]; //Initializes the structure stiffness matrix with the number last assigned to joint.
            DK = new double[SSM.GetLength(0) - QK.Length];
            QU = new double[DK.Length];
        }

        /// <summary>
        /// Divides the structure stiffness matrix into four so that it can be applied directly for the analysis.
        /// </summary>
        private void PartitionSSM()
        {
            K11 = new double[QK.Length, QK.Length];
            K22 = new double[DK.Length, DK.Length];
            K12 = new double[K11.GetLength(0), K22.GetLength(1)];
            K21 = new double[K22.GetLength(0), K11.GetLength(1)];

            int diff = QK.Length;

            for (int i = 0; i < SSM.GetLength(0); i++)
            {
                for (int j = 0; j < SSM.GetLength(1); j++)
                {
                    if (i < QK.Length)
                    {
                        if (j < QK.Length) //K11
                        {
                            K11[i, j] = SSM[i, j];
                        }
                        else //K12
                        {
                            K12[i, j - diff] = SSM[i, j];
                        }
                    }
                    else
                    {
                        if (j < QK.Length) //K21
                        {
                            K21[i - diff, j] = SSM[i, j];
                        }
                        else //K22
                        {
                            K22[i - diff, j - diff] = SSM[i, j];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Fills the forces and displacements calculated from the matrices to the corresponding members and joints.
        /// </summary>
        private void FillBackResults()
        {
            int diff = QK.Length;
            //Fill DU and QU
            foreach (eJoint joint in joints)
            {
                switch (joint.Type)
                {
                    case eJointType.Continious:
                        joint.FinalDisps[0] = DU[joint.SSMIndex[0]];
                        joint.FinalDisps[1] = DU[joint.SSMIndex[1]];
                        break;
                    case eJointType.Free:
                        joint.FinalDisps[0] = DU[joint.SSMIndex[0]];
                        joint.FinalDisps[1] = DU[joint.SSMIndex[1]];
                        break;
                    case eJointType.Hinge:
                        joint.FinalDisps[0] = DU[joint.SSMIndex[0]];
                        joint.FinalDisps[1] = DU[joint.SSMIndex[1]];
                        joint.FinalDisps[2] = DU[joint.SSMIndex[2]];
                        break;
                    case eJointType.Pin:
                        joint.FinalDisps[1] = DU[joint.SSMIndex[1]];
                        break;
                    case eJointType.Roller:
                        joint.FinalDisps[1] = DU[joint.SSMIndex[1]];
                        break;
                    case eJointType.VerticalGuidedRoller:
                        joint.FinalDisps[0] = DU[joint.SSMIndex[0]];
                        joint.FinalDisps[1] = DU[joint.SSMIndex[1]];
                        joint.FinalDisps[2] = DU[joint.SSMIndex[2]];
                        break;
                    case eJointType.VerticalRoller:
                        joint.FinalDisps[0] = DU[joint.SSMIndex[0]];
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the internal moment at the specified point measured from the far left point of the beam.
        /// </summary>
        /// <param name="X">The distance from the far left end of the beam to the point where the moment is to be found.</param>
        /// <param name="sectionAt">Value indicating wheater the section imediatly from the left or from the right.</param>
        public double GetMomentAt(double X,eSectionAt sectionAt = eSectionAt.FromLeft)
        {
            if (X < 0)
                throw new Exception("A distance cannot be negative value.");
            if (sectionAt == eSectionAt.FromRight)
                X += 0.00000001;
            double totX = 0;
            foreach (eAMember m in this.members)
            {
                if (X <= m.Length + totX)
                    return m.GetMomentAt(X - totX);
                totX += m.Length;
            }
            throw new Exception("The distance given is out of the beam length.");
        }

        /// <summary>
        /// Returns the shear force at the specified section.
        /// </summary>
        /// <param name="X">The distance from the far left end of the beam to the point where the shear is to be found. </param>
        /// <param name="sectionAt">Value indicating wheater the section imediatly from the left or from the right.</param>
        /// <returns></returns>
        public double GetShearAt(double X,eSectionAt sectionAt = eSectionAt.FromLeft)
        {  
            if (sectionAt == eSectionAt.FromRight)
                X += 0.00001;
            double cumLength = 0;//Cumulative lenght of the members at each iteration.     
    
            for (int i = 0; i < members.Count; i++)
            {
                if (X <= cumLength + members[i].Length)
                {
                        return members[i].GetShearAt(X - cumLength);
                }
                cumLength += members[i].Length;
            }
            throw new Exception("The specified section is out of the beam.");
        }

        /// <summary>
        /// Adds a joint to the joint collections in this beam.
        /// </summary>
        /// <param name="joint">Member to be added.</param>
        public void AddMember(eAMember member)
        {
            members.Add(member);
            this.beam_Design.Members.Add(this.members[this.members.Count - 1].Member_Design);
        }

        /// <summary>
        /// Adds a joint to the joint collections in this beam.
        /// </summary>
        /// <param name="joint">Joint to be added.</param>
        public void AddJoint(eJoint joint)
        {
            joints.Add(joint);
        }

        /// <summary>
        /// Adds joint to the joint.
        /// </summary>
        /// <param name="Type">actionType of the joint to be added.</param>
        /// <returns></returns>
        public eJoint AddJoint(eJointType JointType)
        {
            this.joints.Add(new eJoint(JointType, this));
            return joints[joints.Count - 1];
        }

        /// <summary>
        /// Returns the absolute maximum bending moment in the beam.
        /// </summary>
        /// <returns></returns>
        public double GetMaxMoment()
        {
            double temMax;
            temMax = members[0].GetMaxMoment();
            foreach (eAMember m in this.members)
                temMax = Math.Max(m.GetMaxMoment(), temMax);
            return temMax;
        }

        /// <summary>
        /// Returns the absolute maximum shear in the beam.
        /// </summary>
        /// <returns></returns>
        public double GetMaxShear()
        {
            double temMax;
            temMax = members[0].GetMaxShear();
            foreach (eAMember m in this.members)
                temMax = Math.Max(m.GetMaxShear(), temMax);
            return temMax;
        }

        /// <summary>
        /// Returns all continious intervals in the beam.
        /// </summary>
        /// <returns></returns>
        public double[] GetSectionIntervals()
        {
            List<double> intervals = new List<double>();
            double[] subIntervals;
            double X = 0;
            foreach (eAMember m in members)
            {
                subIntervals = m.GetSectionsInterval();
                for (int i = 0; i < subIntervals.Length-1; i++)
                {
                    intervals.Add(subIntervals[i] + X);
                }
                X += m.Length;
            }
            intervals.Add(X);
            return intervals.ToArray() as double[];
        }

        /// <summary>
        /// Checks the stability of  the beam.
        /// </summary>
        /// <returns>Returns true if it is stable and false if it is unstable.</returns>
        private bool IsStable()
        {
            int n = CountContiniouMembers();
            int r = CountReactions();

            if (r < 3 * n)
                return false;
            else return true;
        }

        /// <summary>
        /// Returns the total number of reaction in the beam including x-axis reactions
        /// </summary>
        /// <returns></returns>
        private int CountReactions()
        {
            int count = 0;
            for (int i = 0; i < joints.Count; i++)
            {
                switch (joints[i].Type)
                {
                    case eJointType.Fixed:
                        count += 3;
                        break;
                    case eJointType.Pin:
                        count += 2;
                        break;
                    case eJointType.Roller:
                        count += 2;
                        break;
                    case eJointType.VerticalRoller:
                        count += 2;
                        break;
                }
            }
            return count;
        }

        /// <summary>
        /// Returns the number of continious members which does not have hhinge or vertical guided roller.
        /// </summary>
        /// <returns></returns>
        private int CountContiniouMembers()
        {
            int count = 1;

            for (int i = 1; i < joints.Count - 1; i++)
            {
                if (joints[i].Type == eJointType.Hinge || joints[i].Type == eJointType.VerticalGuidedRoller)
                    count++;
            }
            return count;
        }

        private bool CheckConcurency(eJoint start, eJoint end)
        {
            //    if (joints.IndexOf(start) == joints.IndexOf(end) + 1)
            //    {
            //        if ((start.Type == eJointType.Roller || start.Type == eJointType.Pin || start.Type == eJointType.Hinge || start.Type == eJointType.Free || start.Type == eJointType.Continious)
            //            && end.Type == eJointType.VerticalRoller)
            //            return false;
            //        else if ((end.Type == eJointType.Roller || end.Type == eJointType.Pin || end.Type == eJointType.Hinge || end.Type == eJointType.Free || end.Type == eJointType.Continious)
            //            && start.Type == eJointType.VerticalRoller)
            //            return false;
            //        else if(start.Type == eJointType.VerticalRoller && end.Type == eJointType.VerticalRoller
            //    }
            //    else
            //    {
            //
            //    }
            //}

            for (int i = joints.IndexOf(start); i <= joints.IndexOf(end); i++)
            {
                //comparing x.
                if (joints[i].ConstrainedAxis == eConstrainedAxis.X_Y)
                {
                    if (joints[i + 1].ConstrainedAxis != eConstrainedAxis.X_Y || joints[i].ConstrainedAxis == eConstrainedAxis.Y)
                        ;
                }
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Orders all the members to add their self weight to their load list.
        /// </summary>
        private void AddSelfWeight()
        {
            foreach (eAMember memb in this.members)
            {
                memb.AddSelfWeight();
            }
        }

        /// <summary>
        /// Orders all the members to remove their self weight from their load list.
        /// </summary>
        private void RemoveSelfWeight()
        {
            foreach (eAMember memb in this.members)
            {
                memb.RemoveSelfWieght();
            }
        }
        #endregion
    }
}
