using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Code;
using ESADS.Code.EBCS_1995;
using ESADS.Mechanics.Analysis;
using ESADS.Mechanics.Analysis.Beam;

namespace ESADS.Mechanics.Design.Beam
{
    public class eDBeam : eIDModel
    {
        #region Fields
        /// <summary>
        /// Holds the value of the 'Beam_Analysis' property.
        /// </summary>
        private eABeam beam_Analysis;
        /// <summary>
        /// Holds all the members for the whole beam.
        /// </summary>
        private List<eDMember> members;
        /// <summary>
        /// Holds the value of the 'ShearSections' property.
        /// </summary>
        private List<eDShearSection> shearSections;
        /// <summary>
        /// Holds the value of the 'NumberOfShearSections'.
        /// </summary>
        private int numOfShearSxns;
        /// <summary>
        /// Holds the value of the 'NumOfNegativeSxns' property.
        /// </summary>
        private int numOfNegativeSxns;
        /// <summary>
        /// Holds the value of the 'NumOfPositiveSxns' property.
        /// </summary>
        private int numOfPositiveSxns;
        /// <summary>
        /// positive flexure sections
        /// </summary>
        private List<eDFlexureSection> posSections;
        /// <summary>
        /// negative flexure sections
        /// </summary>
        private List<eDFlexureSection> negSections;
        /// <summary>
        /// Holds the value of 'MainBar1'
        /// </summary>
        private eReinforcement mainBar1;
        /// <summary>
        /// Holds the value of 'MainBar2'
        /// </summary>
        private eReinforcement mainBar2;
        /// <summary>
        /// Holds the value of 'StirrupBar'
        /// </summary>
        private eReinforcement stirrupBar;
        /// <summary>
        /// Holds the value of 'BasicBar'.
        /// </summary>
        private eReinforcement basicBar;
        /// <summary>
        /// Holds the value of 'ExposureType'
        /// </summary>
        private eExposureType exposureType;
        /// <summary>
        /// Holds the value of 'ReinfCover'
        /// </summary>
        private double reinfCover;
        /// <summary>
        /// Holds the value of 'StirrupPosn'
        /// </summary>
        private eRelativeStirrupPosition stirrupPosin;
        /// <summary>
        /// Holds the value of 'MaxAggSize'
        /// </summary>
        private double maxAggSize;
        /// <summary>
        /// Holds the value of 'ClassWork'.
        /// </summary>
        private eClassWork classWork;
        /// <summary>
        /// Holds the value of 'Concrete'
        /// </summary>
        private eConcrete concrete;
        /// <summary>
        /// Holds the value of 'Steel'.
        /// </summary>
        private eSteel steel;
        /// <summary>
        /// Holds the value of 'UseTwoBars'.
        /// </summary>
        private bool useTwoBars;
        /// <summary>
        /// Holds the value of 'LongitudinalBars'.
        /// </summary>
        private List<eLongtBar> longitudinalBars;
        /// <summary>
        /// Holds the value of 'SupportCompBarCuttingLength'.
        /// </summary>
        private double supportCompBarCuttingLength;
        /// <summary>
        /// Holds the value of 'SupportTensBarCuttingLength'.
        /// </summary>
        private double supportTensBarCuttingLength;
        /// <summary>
        /// Holds the value of 'DefaultSection'.
        /// </summary>
        private eBeamSection defaultSection;
        private List<eBeamSection> definedSections;
        /// <summary>
        /// Holds the value of 'StirrupHookLength'.
        /// </summary>
        private double stirrupHookLength;
        private bool useCornerBars;
        private double preceision;
        #endregion

        #region Constructors

        #endregion

        #region Properties
        /// <summary>
        /// Creates new instance of the ESADS.Mechnincs.Design.eDBeam class instance
        /// </summary>
        /// <param name="beam">The analysis component of the beam to be designed.</param>
        /// <param name="defaultSection">The default cross-section of the members.</param>
        public eDBeam(eABeam beam, eBeamSection defaultSection)
        {
            this.beam_Analysis = beam;
            this.defaultSection = defaultSection;
            this.numOfPositiveSxns = 1;
            this.numOfNegativeSxns = 1;

            this.classWork = eClassWork.ClassI;
            this.basicBar = eReinforcement.Φ16;
            this.exposureType = eExposureType.Moderate;
            this.mainBar1 = eReinforcement.Φ16;
            this.mainBar2 = eReinforcement.Φ20;
            this.maxAggSize = eUtility.Convert(20, eLengthUnits.mm, eUtility.SLU);
            this.reinfCover = eUtility.Convert(25, eLengthUnits.mm, eUtility.SLU);
            this.stirrupBar = eReinforcement.Φ8;
            this.stirrupPosin = eRelativeStirrupPosition.StirrupAtBottom;
            this.concrete = new eConcrete(eConcreteGrade.C25);
            this.steel = new eSteel(eSteelGrade.S300);

            this.members = new List<eDMember>();

            this.definedSections = new List<eBeamSection>();
            this.definedSections.Add(defaultSection);
            this.defaultSection.Used = true;

            this.supportTensBarCuttingLength = 0.3;
            this.supportCompBarCuttingLength = 0.3;
            this.stirrupHookLength = eXBar.GetDiam(stirrupBar) * 5;
            this.useCornerBars = true;

            //foreach (var v in beam_Analysis.Members)
            //{
            //    this.members.Add(v.Member_Design);
            //}
        }

        /// <summary>
        /// Gets the analysis component of the continuous beam
        /// </summary>
        public eABeam Beam_Analysis
        {
            get
            {
                return beam_Analysis;
            }
        }

        /// <summary>
        /// Gets the members holding the design properties needed by the sections.
        /// </summary>
        public List<eDMember> Members
        {
            get
            {
                return members;
            }
        }

        /// <summary>
        /// Gets the flexure design sections.
        /// </summary>
        public List<eDShearSection> ShearSections
        {
            get
            {
                return shearSections;
            }
        }

        /// <summary>
        /// Gets or sets the number of shear sections in the beam.
        /// </summary>
        public int NumOfShearSxns
        {
            get
            {
                return numOfShearSxns;
            }
            set
            {
                if (value <= 0)
                    throw new Exception("The number of shear sections cannot be zero or negative.");
                numOfShearSxns = value;
            }
        }

        /// <summary>
        /// Gets or sets the total number of negative moment sections in the beam.
        /// </summary>
        public int NumOfNegativeSxns
        {
            get
            {
                return numOfNegativeSxns;
            }
            set
            {
                if (value <= 0)
                    throw new Exception("The number of flexure sections cannot be zero or negative.");
                numOfNegativeSxns = value;
            }
        }

        /// <summary>
        /// Gets or sets the total sections designed with different positive moment.
        /// </summary>
        public int NumOfPositiveSxns
        {
            get
            {
                return this.numOfPositiveSxns;
            }
            set
            {
                if (value > 0)
                    this.numOfPositiveSxns = value;
                else
                    throw new Exception("Number of sections cannot be zero or negative.");
            }
        }

        /// <summary>
        /// Gets the collection of flexure sections with negative moment.
        /// </summary>
        public List<eDFlexureSection> NegativeFlexSxns
        {
            get
            {
                return negSections;
            }
        }

        /// <summary>
        /// Gets the collection of flexure sections with positive moment.
        /// </summary>
        public List<eDFlexureSection> PositiveFlexSxns
        {
            get
            {
                return posSections;
            }
        }

        /// <summary>
        /// Gets or sets one of the user shearBar choices.
        /// </summary>
        public eReinforcement MainBar1
        {
            get
            {
                return mainBar1;
            }
            set
            {
                mainBar1 = value;
            }
        }

        /// <summary>
        /// Gets or sets second user shearBar choices.
        /// </summary>
        public eReinforcement MainBar2
        {
            get
            {
                return mainBar2;
            }
            set
            {
                mainBar2 = value;
            }
        }

        /// <summary>
        /// Gets or sets the user chosen reinforcement shearBar for stirrup.
        /// </summary>
        public eReinforcement StirupBar
        {
            get
            {
                return stirrupBar;
            }
            set
            {
                stirrupBar = value;
            }
        }

        /// <summary>
        /// Gets or sets longitudinal shearBar type to be provided at corners to hold stirrups in place.
        /// </summary>
        public eReinforcement BasicBar
        {
            get
            {
                return basicBar;
            }
            set
            {
                basicBar = value;
            }
        }

        /// <summary>
        /// Gets or sets the user chosen exposure type of the beam to be used to determine the concrete cover for the reinforcements.
        /// </summary>
        public eExposureType ExposureType
        {
            get
            {
                return exposureType;
            }
            set
            {
                exposureType = value;
            }
        }

        /// <summary>
        /// Gets or sets the reinforcement cover. It throws exception if the dimension is less than the minimum value required by EBCS for the exposure type of the beam.
        /// </summary>
        public double Cover
        {
            get
            {
                return reinfCover;
            }
            set
            {
                reinfCover = value;
            }
        }

        /// <summary>
        /// Gets or sets the relative position of the stirup to the longitudinal shearBar.
        /// </summary>
        public eRelativeStirrupPosition StirrupPosn
        {
            get
            {
                return stirrupPosin;
            }
            set
            {
                stirrupPosin = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum aggregate size of the concrete mix used.
        /// </summary>
        public double MaxAggSize
        {
            get
            {
                return maxAggSize;
            }
            set
            {
                maxAggSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the class work of the beam
        /// </summary>
        public eClassWork ClassWork
        {
            get
            {
                return classWork;
            }
            set
            {
                classWork = value;
            }
        }

        /// <summary>
        /// Gets or sets the concrete material used to design the whole beam.
        /// </summary>
        public eConcrete Concrete
        {
            get
            {
                return concrete;
            }
            set
            {
                concrete = value;
            }
        }

        /// <summary>
        /// Gets or sets the steel material used to design the whole beam.
        /// </summary>
        public eSteel Steel
        {
            get
            {
                return steel;
            }
            set
            {
                steel = value;
            }
        }

        /// <summary>
        /// Gets or sets the value whether to use two shearBar per section.
        /// </summary>
        public bool UseTwoBars
        {
            get
            {
                return useTwoBars;
            }
            set
            {
                useTwoBars = value;
            }
        }

        /// <summary>
        /// Gets the collection of longitudinal bars throughout the beam.
        /// </summary>
        public List<eLongtBar> LongitudinalBars
        {
            get
            {
                return longitudinalBars;
            }
        }

        /// <summary>
        /// Gets or sets the ratio the length of compression bars at supports to the length of that member.
        /// </summary>
        public double SupportCompBarCuttingLength
        {
            get
            {
                return supportCompBarCuttingLength;
            }
            set
            {
                supportCompBarCuttingLength = value;
            }
        }

        /// <summary>
        /// Gets or sets the ratio the length of tensile bars at supports to the length of that member.
        /// </summary>
        public double SupportTensBarCuttingLength
        {
            get
            {
                return supportTensBarCuttingLength;
            }
            set
            {
                supportTensBarCuttingLength = value;
            }
        }

        /// <summary>
        /// Gets or sets the section assigned to members by default if no section is provided.
        /// </summary>
        public eBeamSection DefaultSection
        {
            get
            {
                return this.defaultSection;
            }
            set
            {
                this.defaultSection = value;
            }
        }

        /// <summary>
        /// Gets or sets the defined sections of the beam
        /// </summary>
        public List<eBeamSection> DefinedSections
        {
            get
            {
                return this.definedSections;
            }
            set
            {
                this.definedSections = value;
            }
        }

        /// <summary>
        /// Gets or sets the hook length of the shear bars.
        /// </summary>
        public double StirrupHookLength
        {
            get
            {
                return this.stirrupHookLength;
            }
            set
            {
                if (value > 0)
                    this.stirrupHookLength = value;
                else
                    throw new Exception("The value of stirrup hook length cannot be zero or negative.");
            }
        }

        /// <summary>
        /// Gets or sets the value whether to use minimum of two bars for the combinations.
        /// </summary>
        public bool UseCornerBars
        {
            get
            {
                return this.useCornerBars;
            }
            set
            {
                this.useCornerBars = value;
            }
        }

        /// <summary>
        /// Gets or sets the preceision of bar length and spacing calculations.
        /// </summary>
        public double BarPreceision
        {
            get
            {
                return this.preceision;
            }
            set
            {
                if (value != 0)
                    this.preceision = value;
                else
                    throw new Exception("Preceision value cannot be zero!");
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Runs the design of the whole beam.
        /// </summary>
        public void Design()
        {
            CreateFlexureSections();
            
            foreach (eDFlexureSection s in posSections)
            {
                s.Design();
            }
            foreach (eDFlexureSection s in negSections)
            {
                s.Design();
            }

            NameSections();
            CreateShearSections();

            foreach (eDShearSection s in shearSections)
            {
                s.Design();
            }
            NameSections(false);

            CreateLongitudinalBars();
        }

        private void NameSections(bool flexure = true)
        {
            if (flexure)
            {
                char name = 'A';

                for (int i = 0; i < posSections.Count; i++)
                {
                    if (posSections[i].Name == "")
                        posSections[i].Name = (name++).ToString();
                    for (int j = i + 1; j < posSections.Count; j++)
                    {
                        if (posSections[i].IsSimilar(posSections[j]))
                            posSections[j].Name = posSections[i].Name;
                    }
                }

                for (int i = 0; i < negSections.Count; i++)
                {
                    if (negSections[i].Name == "")
                        negSections[i].Name = (name++).ToString();
                    for (int j = i + 1; j < negSections.Count; j++)
                    {
                        if (negSections[i].IsSimilar(negSections[j]))
                            negSections[j].Name = negSections[i].Name;
                    }
                }
            }
            else
            {
                int name = 1;

                for (int i = 0; i < shearSections.Count; i++)
                {
                    if (shearSections[i].Name == "")
                        shearSections[i].Name = (name++).ToString();
                    for (int j = i + 1; j < shearSections.Count; j++)
                    {
                        if (shearSections[i].IsSimilar(shearSections[j]))
                            shearSections[j].Name = shearSections[i].Name;
                    }
                    shearSections[i].FillBar();
                }
            }
        }

        /// <summary>
        /// Creates all the necessary shear sections making them ready to be designed.
        /// </summary>
        private void CreateShearSections()
        {
            this.shearSections = new List<eDShearSection>();

            double x = 0, xl, xr, V_l, V_r, Vc;
            double[] x_zero, x_c;
            List<double[]> allPoints;

            foreach (var memb in beam_Analysis.Members)
            {
                xl = memb.NEJoint.SupportWidth / 2.0 + GetFlexureSectionAt(x).EffectiveDepth;
                xr = memb.Length - (memb.NEJoint.SupportWidth / 2.0 + GetFlexureSectionAt(x + memb.Length, true).EffectiveDepth);

                x_zero = memb.GetShearZero(xl, xr);

                V_l = memb.GetShearAt(xl);
                V_r = memb.GetShearAt(xr);
                Vc = eDShearSection.GetConcShearCapacity(GetFlexureSectionAt(x + memb.Length / 2.0), concrete.fctd);
                x_c = memb.GetXofShear(Vc, xl, xr);

                xl = memb.NEJoint.SupportWidth / 2.0;
                xr = memb.Length - memb.NEJoint.SupportWidth / 2.0;

                allPoints = MixPoints(x_zero, x_c, Vc, V_l, V_r, xl, xr);

                for (int i = 0; i < allPoints.Count - 1; i++)
                {
                    shearSections.Add(new eDShearSection(this, "", memb.Member_Design.Width, memb.Member_Design.Depth, Math.Max(Math.Abs(allPoints[i][1]),
                        Math.Abs(allPoints[i + 1][1]))));
                    shearSections[shearSections.Count - 1].Intervals.Add(new double[] { allPoints[i][0] + x, allPoints[i + 1][0] + x });
                    shearSections[shearSections.Count - 1].FlexureSection = GetFlexureSectionAt(Math.Abs(allPoints[i][1]) > Math.Abs(allPoints[i + 1][1]) ? allPoints[i][0] : allPoints[i + 1][0]);
                }

                x += memb.Length;
            }

            MergeAdjacentSections();
        }

        /// <summary>
        /// Merges adjacent shear sections with equal design shear so that they may be designed together as one.
        /// </summary>
        private void MergeAdjacentSections()
        {
            for (int i = 0; i < shearSections.Count-1; i++)
            {
                if (shearSections[i].Intervals[0][1] == shearSections[i + 1].Intervals[0][0] && shearSections[i].Shear == shearSections[i + 1].Shear)
                {
                    shearSections[i].Intervals[0][1] = shearSections[i + 1].Intervals[0][1];
                    shearSections.RemoveAt(i + 1);
                    i--;
                }
            }
        }

        /// <summary>
        /// Mixes all critical shear points in a member to create a sorted array of pair of numbers(coordinate and shear).
        /// </summary>
        /// <param name="x_zero">points on the member where the shaer is zero. Measured from the start of the member.</param>
        /// <param name="x_c">points where the shear is equal to the concrete shear capacity of the member.</param>
        /// <param name="Vc">Concrete shear capacity of the member assuming the flexure section at the middle of the member.</param>
        /// <param name="V_Left">The shear force at d distance from the face of the left support.</param>
        /// <param name="V_Right">The shear force at d distance form the face of the right support.</param>
        /// <param name="x_V_left">The distance from the center of the left support to a point d distance far from the face of the left support.</param>
        /// <param name="x_V_right">The distance form the center of the right support to a point, d distance from the face of the right support.</param>
        private List<double[]> MixPoints(double[] x_zero, double[] x_c, double Vc, double V_Left, double V_Right, double x_V_left, double x_V_right)
        {
            List<double[]> x_all = new List<double[]>();

            x_all.Add(new double[] { x_V_left, Math.Abs(V_Left) });
            foreach (double x in x_c)
            {
                if (x > x_V_left && x < x_V_right)
                    x_all.Add(new double[] { x, Vc });
            }

            foreach (double x in x_zero)
            {
                if (x > x_V_left && x < x_V_right)
                    x_all.Add(new double[] { x, 0.0 });
            }

            x_all.Add(new double[] { x_V_right, Math.Abs(V_Right) });

            for (int i = 0; i < x_all.Count; i++)
            {
                for (int j = i + 1; j < x_all.Count; j++)
                {
                    if (x_all[i][0] > x_all[j][0])
                    {
                        double[] temp = x_all[i];
                        x_all[i] = x_all[j];
                        x_all[j] = temp;
                    }
                }
            }

            return x_all;
        }

        /// <summary>
        /// Creates all the necessary moment sections for the whole beam.
        /// </summary>
        private void CreateFlexureSections()
        {
            posSections = new List<eDFlexureSection>();
            negSections = new List<eDFlexureSection>();

            double startCoord = 0.0;
            double moment, x_mom_max;
            List<double> intrvl = new List<double>();

            foreach (eAMember m in beam_Analysis.Members)
            {
                double[] zeros = m.GetMomentZero(0, m.Length);
                eDFlexureSection membSxn = new eDFlexureSection(this, m.Member_Design.Width, m.Member_Design.Depth);

                if (zeros == null || zeros.Length == 0)
                {
                    intrvl.Add(startCoord);
                    intrvl.Add(startCoord + m.Length);
                    moment = m.GetExtremeMoment(0, m.Length);
                    moment = m.GetExtremeMoment(0, m.Length, out x_mom_max);
                    if (moment < 0)
                    {
                        this.negSections.Add(new eDFlexureSection(this, m.Member_Design.Width, m.Member_Design.Depth, moment));
                        this.negSections[this.negSections.Count - 1].Intervals.Add(new double[] { intrvl[intrvl.Count - 2], intrvl[intrvl.Count - 1] });
                        this.negSections[this.negSections.Count - 1].Member = m.Member_Design;
                        this.negSections[this.negSections.Count - 1].Location = startCoord + x_mom_max;

                        if (intrvl[intrvl.Count - 2] - startCoord == 0)
                            m.Member_Design.SupportSxn_Left = this.negSections[this.negSections.Count - 1];
                        if (intrvl[intrvl.Count - 1] - startCoord == m.Length)
                            m.Member_Design.SupportSxn_Right = this.negSections[this.negSections.Count - 1];
                    }
                    else
                    {
                        this.posSections.Add(new eDFlexureSection(this, m.Member_Design.Width, m.Member_Design.Depth, moment));
                        this.posSections[this.posSections.Count - 1].Intervals.Add(new double[] { intrvl[intrvl.Count - 2], intrvl[intrvl.Count - 1] });
                        this.posSections[this.posSections.Count - 1].Member = m.Member_Design;
                        this.posSections[this.posSections.Count - 1].Location = startCoord + x_mom_max;
                        m.Member_Design.SpanSxn = this.posSections[this.posSections.Count - 1];
                    }
                }
                else
                {
                    intrvl.Add(startCoord);

                    for (int i = 0; i <= zeros.Length; i++)
                    {
                        if (i == 0)
                        {
                            intrvl.Add(startCoord + zeros[i]);
                            moment = m.GetExtremeMoment(0, zeros[i], out x_mom_max);
                        }
                        else if (i == zeros.Length)
                        {
                            intrvl.Add(startCoord + m.Length);
                            moment = m.GetExtremeMoment(zeros[i - 1], m.Length, out x_mom_max);
                        }
                        else
                        {
                            intrvl.Add(startCoord + zeros[i]);
                            moment = m.GetExtremeMoment(zeros[i - 1], zeros[i], out x_mom_max);
                        }

                        if (moment < 0)
                        {
                            this.negSections.Add(new eDFlexureSection(this, m.Member_Design.Width, m.Member_Design.Depth, moment));
                            this.negSections[this.negSections.Count - 1].Intervals.Add(new double[] { intrvl[intrvl.Count - 2], intrvl[intrvl.Count - 1] });
                            this.negSections[this.negSections.Count - 1].Member = m.Member_Design;
                            this.negSections[this.negSections.Count - 1].Location = startCoord + x_mom_max;

                            if (intrvl[intrvl.Count - 2] - startCoord == 0)
                                m.Member_Design.SupportSxn_Left = this.negSections[this.negSections.Count - 1];
                            if (intrvl[intrvl.Count - 1] - startCoord == m.Length)
                                m.Member_Design.SupportSxn_Right = this.negSections[this.negSections.Count - 1];
                        }
                        else
                        {
                            this.posSections.Add(new eDFlexureSection(this, m.Member_Design.Width, m.Member_Design.Depth, moment));
                            this.posSections[this.posSections.Count - 1].Intervals.Add(new double[] { intrvl[intrvl.Count - 2], intrvl[intrvl.Count - 1] });
                            this.posSections[this.posSections.Count - 1].Member = m.Member_Design;
                            this.posSections[this.posSections.Count - 1].Location = x_mom_max + startCoord;
                            m.Member_Design.SpanSxn = this.posSections[this.posSections.Count - 1];
                        }
                    }
                }

                startCoord += m.Length;
            }

            SortSections();
            //EliminateSimilar();
            //ReduceNumber();
            //AssociateSections();
        }

        /// <summary>
        /// Bonds negative sections to positive section from which bars extend.
        /// </summary>
        private void AssociateSections()
        {
            foreach (var n in negSections)
            {
                foreach (var p in posSections)
                {
                    if ((Math.Round(n.Intervals[0][0], 6) == Math.Round(p.Intervals[0][1], 6)) || (Math.Round(n.Intervals[0][1], 6) == Math.Round(p.Intervals[0][0], 6)))
                    {
                        n.ExtendFrom = p;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Reduces the number of flexure sections to that chosen by the user.
        /// </summary>
        private void ReduceNumber()
        {
            if (this.numOfNegativeSxns > negSections.Count)
                this.numOfNegativeSxns = negSections.Count;
            if (this.numOfPositiveSxns > posSections.Count)
                this.numOfPositiveSxns = posSections.Count;

            while (numOfNegativeSxns < negSections.Count)
            {
                int indx = 0;
                double minDiff = negSections[negSections.Count - 1].Moment - negSections[0].Moment;

                for (int i = 0; i < negSections.Count - 1; i++)
                {
                    if ((negSections[i + 1].Moment - negSections[i].Moment) > minDiff)
                    {
                        minDiff = negSections[i + 1].Moment - negSections[i].Moment;
                        indx = i;
                    }
                }

                negSections[indx + 1].Intervals.AddRange(negSections[indx].Intervals);
                negSections.RemoveAt(indx);
            }

            while (numOfPositiveSxns < posSections.Count)
            {
                int indx = 0;
                double minDiff = posSections[posSections.Count - 1].Moment - posSections[0].Moment;

                for (int i = 0; i < posSections.Count - 1; i++)
                {
                    if ((posSections[i + 1].Moment - posSections[i].Moment) < minDiff)
                    {
                        minDiff = posSections[i + 1].Moment - posSections[i].Moment;
                        indx = i;
                    }
                }

                posSections[indx + 1].Intervals.AddRange(posSections[indx].Intervals);
                posSections.RemoveAt(indx);
            }
        }

        /// <summary>
        /// Elliminates similar flexure sections both from the negative and positive flexure sections.
        /// </summary>
        private void EliminateSimilar()
        {
            for (int i = 0; i < posSections.Count; i++)
            {
                for (int j = i + 1; j < posSections.Count; j++)
                {
                    if (posSections[i].Equals(posSections[j]))
                    {
                        posSections[i].Intervals.AddRange(posSections[j].Intervals);
                        posSections.RemoveAt(j);
                        j--;
                    }
                }
            }

            for (int i = 0; i < negSections.Count; i++)
            {
                for (int j = i + 1; j < negSections.Count; j++)
                {
                    if (negSections[i].Equals(negSections[j]))
                    {
                        negSections[i].Intervals.AddRange(negSections[j].Intervals);
                        negSections.RemoveAt(j);
                        j--;
                    }
                }
            }
        }

        /// <summary>
        /// Sorts both positive and negative flexure section in increasing order of design moment.
        /// </summary>
        private void SortSections()
        {
            eDFlexureSection temp;

            for (int i = 0; i < posSections.Count; i++)
            {
                for (int j = i + 1; j < posSections.Count; j++)
                {
                    if (posSections[i].Moment > posSections[j].Moment)
                    {
                        temp = posSections[i];
                        posSections[i] = posSections[j];
                        posSections[j] = temp;
                    }
                }
            }

            for (int i = 0; i < negSections.Count; i++)
            {
                for (int j = i + 1; j < negSections.Count; j++)
                {
                    if (Math.Abs(negSections[i].Moment) > Math.Abs(negSections[j].Moment))
                    {
                        temp = negSections[i];
                        negSections[i] = negSections[j];
                        negSections[j] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the flexure section used to design the moment at a given X_Coordinates coordinate.
        /// </summary>
        /// <param name="X">The distance from the left end of  the beam.</param>
        public eDFlexureSection GetFlexureSectionAt(double X, bool toTheLeft = false)
        {
            if (toTheLeft)
            {
                foreach (var v in negSections)
                {
                    foreach (var vi in v.Intervals)
                    {
                        if (vi[0] < X && X <= vi[1])
                            return v;
                    }
                }

                foreach (var v in posSections)
                {
                    foreach (var vi in v.Intervals)
                    {
                        if (vi[0] < X && X <= vi[1])
                            return v;
                    }
                }
            }
            else
            {
                foreach (var v in negSections)
                {
                    foreach (var vi in v.Intervals)
                    {
                        if (vi[0] <= X && X < vi[1])
                            return v;
                    }
                }

                foreach (var v in posSections)
                {
                    foreach (var vi in v.Intervals)
                    {
                        if (vi[0] <= X && X < vi[1])
                            return v;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Creates all longitudinal bars for a completed design.
        /// </summary>
        public void CreateLongitudinalBars()
        {
            if (this.members.Count == 0)
                foreach (var memb in this.beam_Analysis.Members)
                    this.members.Add(memb.Member_Design);

            this.longitudinalBars = new List<eLongtBar>();

            foreach (var memb in this.members)
            {
                memb.FillLongtBars();
                memb.MergeLongtBars(longitudinalBars);
            }

            eDMember m1;
            eDMember m2;

            for (int i = 0; i < members.Count - 1; i++)
            {
                m1 = members[i];
                m2 = members[i + 1];

                Merge(m1.Bar9, m2.Bar1);
                Merge(m1.Bar10, m2.Bar2);
                Merge(m1.Bar3, m2.Bar3);
                Merge(m1.Bar4, m2.Bar4);
                Merge(m1.Bar11, m2.Bar5);
                Merge(m1.Bar12, m2.Bar6);
                Merge(m1.Bar7, m2.Bar7);
                Merge(m1.Bar8, m2.Bar8);

                RefineBars(m1, m2);
            }

            SortBars();
        }

        private void RefineBars(eDMember m1, eDMember m2)
        {
            if (m2.Bar1 != null && m2.Bar1.Number == 0)
                m2.Bar1 = null;
            if (m2.Bar2 != null && m2.Bar2.Number == 0)
                m2.Bar2 = null;
            if (m2.Bar3 != null && m2.Bar3.Number == 0)
                m2.Bar3 = null;
            if (m2.Bar4 != null && m2.Bar4.Number == 0)
                m2.Bar4 = null;
            if (m2.Bar5 != null && m2.Bar5.Number == 0)
                m2.Bar5 = null;
            if (m2.Bar7 != null && m2.Bar7.Number == 0)
                m2.Bar7 = null;
            if (m2.Bar8 != null && m2.Bar8.Number == 0)
                m2.Bar8 = null;
            if (m2.Bar12 != null && m2.Bar12.Number == 0)
                m2.Bar12 = null;
        }

        private void SortBars()
        {
            if (longitudinalBars.Count == 0)
                return;
            eLongtBar temp;

            //Sorting by level
            for (int i = 0; i < longitudinalBars.Count; i++)
            {
                for (int j = i + 1; j < longitudinalBars.Count; j++)
                {
                    if (longitudinalBars[i].Level > longitudinalBars[j].Level)
                    {
                        temp = longitudinalBars[i];
                        longitudinalBars[i] = longitudinalBars[j];
                        longitudinalBars[j] = temp;
                    }
                }
            }

            //filling sub levels
            for (int i = 0; i < longitudinalBars.Count; )
            {
                int j, sl = 0;
                for (j = i; j < longitudinalBars.Count && longitudinalBars[i].Level == longitudinalBars[j].Level; j++)
                {
                    if (longitudinalBars[j].SubLevel == 0)
                        longitudinalBars[j].SubLevel = ++sl;

                    for (int k = j + 1; k < longitudinalBars.Count && longitudinalBars[i].Level == longitudinalBars[k].Level; k++)
                        if (longitudinalBars[j].Start > longitudinalBars[k].End || longitudinalBars[k].Start > longitudinalBars[j].End && longitudinalBars[k].SubLevel == 0)
                            longitudinalBars[k].SubLevel = longitudinalBars[j].SubLevel;
                }
                i = j;
            }

            //Shifting the level values due to the addition of the sub levels.
            int shift = 0, max_sl = 0, lvl;
            for (int i = 0; i < longitudinalBars.Count; )
            {
                int j;
                lvl = longitudinalBars[i].Level;
                for (j = i; j < longitudinalBars.Count && lvl == longitudinalBars[j].Level; j++)
                {
                    max_sl = Math.Max(max_sl, longitudinalBars[j].SubLevel - 1);
                    longitudinalBars[j].Level += shift + longitudinalBars[j].SubLevel - 1;
                }
                i = j;
                shift += max_sl;
                max_sl = 0;
            }

            //Sorting by level
            for (int i = 0; i < longitudinalBars.Count; i++)
            {
                for (int j = i + 1; j < longitudinalBars.Count; j++)
                {
                    if (longitudinalBars[i].Level > longitudinalBars[j].Level)
                    {
                        temp = longitudinalBars[i];
                        longitudinalBars[i] = longitudinalBars[j];
                        longitudinalBars[j] = temp;
                    }
                }
            }

            //keeps the continuity of the level values.
            lvl = longitudinalBars[0].Level;
            longitudinalBars[0].Level = 1;
            for (int i = 1; i < longitudinalBars.Count; i++)
            {
                if (lvl == longitudinalBars[i].Level)
                {
                    lvl = longitudinalBars[i].Level;
                    longitudinalBars[i].Level = longitudinalBars[i - 1].Level;
                }
                else
                {
                    lvl = longitudinalBars[i].Level;
                    longitudinalBars[i].Level = longitudinalBars[i - 1].Level + 1;
                }
            }

            //Sort by length 
            for (int i = 0; i < longitudinalBars.Count; i++)
            {
                for (int j = i + 1; j < longitudinalBars.Count; j++)
                {
                    if (longitudinalBars[i].IsTop && longitudinalBars[j].IsTop && longitudinalBars[i].Length < longitudinalBars[j].Length)
                    {
                        lvl = longitudinalBars[i].Level;
                        longitudinalBars[i].Level = longitudinalBars[j].Level;
                        longitudinalBars[j].Level = lvl;

                        temp = longitudinalBars[i];
                        longitudinalBars[i] = longitudinalBars[j];
                        longitudinalBars[j] = temp;
                    }
                    else if (!longitudinalBars[i].IsTop && !longitudinalBars[j].IsTop && longitudinalBars[i].Length > longitudinalBars[j].Length)
                    {
                        lvl = longitudinalBars[i].Level;
                        longitudinalBars[i].Level = longitudinalBars[j].Level;
                        longitudinalBars[j].Level = lvl;

                        temp = longitudinalBars[i];
                        longitudinalBars[i] = longitudinalBars[j];
                        longitudinalBars[j] = temp;
                    }
                }
            }

        }

        private void Merge(eLongtBar left, eLongtBar right)
        {
            if (left == null || right == null)
                return;

            if (left.Number == right.Number)
            {
                right.Start = left.Start;
                longitudinalBars.Remove(left);
                left.Number = 0;
            }
            else if(left.Number > right.Number)
            {
                left.Number -= right.Number;
                right.Start = left.Start;
            }
            else
            {
                right.Number -= left.Number;
                left.End = right.End;
            }
        }
        #endregion
    }
}