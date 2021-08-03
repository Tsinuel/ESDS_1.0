using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using ESADS.Code;
using ESADS.Code.EBCS_1995;

namespace ESADS.Mechanics.Design.Beam
{
    /// <summary>
    /// Represents a beam cross section which is going to be design for flexure.
    /// </summary>
    public class eDFlexureSection : eDSection
    {

        #region Feilds
        /// <summary>
        /// Specifies the tolerance to which depth revision is significantly not important.
        /// </summary>
        private double depthTolerance = 5;
        /// <summary>
        /// Accesses the public property 'CompCombination'.
        /// </summary>
        private eCombination compCombination;
        /// <summary>
        /// Accesses the public property 'EffectiveSLSdepth'.
        /// </summary>
        private double effectiveSLSdepth;
        /// <summary>
        /// Accesses the public property 'SpanType'.
        /// </summary>
        private eSpanType spanType;
        /// <summary>
        /// Accesses the public property 'TensCombination'.
        /// </summary>
        private eCombination tensCombination;
        /// <summary>
        /// Holds a value for property 'SectionType'.
        /// </summary>
        private eSectionType sectionType;
        /// <summary>
        /// Holds the value of the 'Moment'.
        /// </summary>
        private double moment;
        /// <summary>
        /// Holds the value of 'ExtendFrom'
        /// </summary>
        private eDFlexureSection extendFrom;
        /// <summary>
        /// Holds the value of 'Member'.
        /// </summary>
        private eDMember member;
        /// <summary>
        /// Holds the value of 'IsOverReinforced'.
        /// </summary>
        private bool isOverReinforced;
        /// <summary>
        /// Holds the value of 'SectionFailed'.
        /// </summary>
        private bool failed;
        private double As;
        private double AsPrim;
        /// <summary>
        /// Holds the value of 'Location'.
        /// </summary>
        private double location;
        /// <summary>
        /// Holds the value of 'NeutralAxisDepth'.
        /// </summary>
        private double X;
        private string failureNote;
        #endregion

        #region Properties

        /// <summary>
        /// Gets the depth tolerance to be used in the design.
        /// </summary>
        public double DepthTolerance
        {
            get { return depthTolerance; }
        }

        /// <summary>
        /// Gets or sets the type of the section.
        /// </summary>
        public eSectionType SectionType
        {
            get { return sectionType; }
            set { sectionType = value; }
        }

        /// <summary>
        /// Gets compresion shearBar combination.
        /// </summary>
        public eCombination CompresionComb
        {
            get { return compCombination; }
        }

        /// <summary>
        /// Gets tensile shearBar combination.
        /// </summary>
        public eCombination TensileComb
        {
            get { return tensCombination; }
        }

        /// <summary>
        /// Gets the effective servisability limit state depth.
        /// </summary>
        public double EffSLS_Depth
        {
            get { return effectiveSLSdepth; }
        }

        /// <summary>
        /// Gets or sets the design flexural moment of the section
        /// </summary>
        internal double Moment
        {
            get
            {
                return moment;
            }
            set
            {
                moment = value;
            }
        }

        /// <summary>
        /// Gets the value if the flexure section designed with negative moment.
        /// </summary>
        public bool IsNegative
        {
            get
            {
                return this.moment < 0.0;
            }
        }

        /// <summary>
        /// Gets the smaller shearBar diameter
        /// </summary>
        private double minBarDiam
        {
            get
            {
                return Math.Min(eXBar.GetDiam(beam.MainBar1), eXBar.GetDiam(beam.MainBar2));
            }
        }

        /// <summary>
        /// Gets the larger shearBar diameter.
        /// </summary>
        private double maxBarDiam
        {
            get
            {
                return Math.Max(eXBar.GetDiam(beam.MainBar1), eXBar.GetDiam(beam.MainBar2));
            }
        }

        /// <summary>
        /// Holds the value of the moment
        /// </summary>
        private double M
        {
            get
            {
                return this.moment;
            }
        }

        /// <summary>
        /// Gets or sets the section from which compression shearBar extend.
        /// </summary>
        public eDFlexureSection ExtendFrom
        {
            get
            {
                return extendFrom;
            }
            set
            {
                if (value != null)
                    if (!value.IsNegative)
                        extendFrom = value;
                    else
                        throw new Exception("Cannot extend from negative section to positive.");
            }
        }

        /// <summary>
        /// Gets or sets the member bearing the flexure section.
        /// </summary>
        public eDMember Member
        {
            get
            {
                return member;
            }
            set
            {
                member = value;
            }
        }

        /// <summary>
        /// Gets the governing effective depth of the section from ULS and SLS depths.
        /// </summary>
        public new double EffectiveDepth
        {
            get
            {
                return Math.Max(d, EffSLS_Depth);
            }
        }

        /// <summary>
        /// Gets the diameter of the larger preferred shearBar.
        /// </summary>
        public double Bar1
        {
            get
            {
                if (!beam.UseTwoBars)
                    return eXBar.GetDiam(beam.MainBar1);
                else
                    return Math.Max(eXBar.GetDiam(beam.MainBar1), eXBar.GetDiam(beam.MainBar2));
            }
        }

        /// <summary>
        /// Gets the diameter of the smaller preferred shearBar. It will have a value of 0 if one shearBar is used for the design.
        /// </summary>
        public double Bar2
        {
            get
            {
                if (!beam.UseTwoBars)
                    return eXBar.GetDiam(beam.MainBar2);
                else
                    return Math.Min(eXBar.GetDiam(beam.MainBar1), eXBar.GetDiam(beam.MainBar2));
            }
        }

        /// <summary>
        /// Gets the value whether the section is overrinforced or not.
        /// </summary>
        public bool IsOverReninforced
        {
            get
            {
                return isOverReinforced;
            }
            internal set
            {
                this.isOverReinforced = value;
            }
        }

        /// <summary>
        /// Gets the value if the section has failed in ultimate limit state with the provided gross depth.
        /// </summary>
        public bool Failed
        {
            get
            {
                return failed;
            }
        }

        /// <summary>
        /// Gets or sets the exact location of the maximum moment. Coordinate measured from the start of the beam.
        /// </summary>
        internal double Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        /// <summary>
        /// Gets the neutral axis depth of the section.
        /// </summary>
        public double NeutralAxisDepth
        {
            get
            {
                return X;
            }
        }

        /// <summary>
        /// Gets the reason for failure if the section has failed.
        /// </summary>
        public string FailureNote
        {
            get
            {
                return this.failureNote;
            }
        }

        /// <summary>
        /// Gets the number of bar type 1 in tension steel of the section.
        /// </summary>
        public int NumOfTensBar1
        {
            get
            {
                return this.tensCombination.NumOfBar1;
            }
        }

        /// <summary>
        /// Gets the number of bar type 2 in tension steel of the section.
        /// </summary>
        public int NumOfTensBar2
        {
            get
            {
                return this.tensCombination.NumOfBar2;
            }
        }

        /// <summary>
        /// Gets the number of bar type 1 in compression steel of the section.
        /// </summary>
        public int NumOfCompBar1
        {
            get
            {
                return this.compCombination.NumOfBar1;
            }
        }

        /// <summary>
        /// Gets the number of bar type 2 in compression steel of the section.
        /// </summary>
        public int NumOfCompBar2
        {
            get
            {
                return this.compCombination.NumOfBar2;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Design.eDFlexureSection class for a given basic parameters.
        /// </summary>
        /// <param name="beam">The beam bearing the section</param>
        /// <param name="width">Width of the section.</param>
        /// <param name="depth">Depth of the section.</param>
        public eDFlexureSection(eDBeam beam, double width, double depth)
            : base(beam, width, depth)
        {
            this.tensCombination = new eCombination(this, false);
            this.compCombination = new eCombination(this, true);

            this.isOverReinforced = false;
            this.failed = false;
        }

        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Design.eDFlexureSection class for a given basic parameters.
        /// </summary>
        /// <param name="beam">The beam bearing the section</param>
        /// <param name="width">Width of the section.</param>
        /// <param name="depth">Depth of the section.</param>
        public eDFlexureSection(eDBeam beam, double width, double depth, double moment)
            : base(beam, width, depth)
        {
            this.tensCombination = new eCombination(this, false);
            this.compCombination = new eCombination(this, true);
            this.moment = moment;

            this.failed = false;
            this.isOverReinforced = false;
        }

        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Design.eDFlexureSection class for a given basic parameters.
        /// </summary>
        /// <param name="beam">The beam bearing the section</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="width">Width of the section.</param>
        /// <param name="depth">Depth of the section.</param>
        public eDFlexureSection(eDBeam beam, string sectionName, double width, double depth)
            : base(beam, sectionName, width, depth)
        {
            this.tensCombination = new eCombination(this, false);
            this.compCombination = new eCombination(this, true);

            this.failed = false;
            this.isOverReinforced = false;
        }

        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Design.eDFlexureSection class for a given basic parameters.
        /// </summary>
        /// <param name="beam">The beam bearing the section</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="width">Width of the section.</param>
        /// <param name="depth">Depth of the section.</param>
        /// <param name="moment">The moment used to design this cross section.</param>
        public eDFlexureSection(eDBeam beam, string sectionName, double width, double depth, double moment)
            : base(beam, sectionName, width, depth)
        {
            this.tensCombination = new eCombination(this, false);
            this.compCombination = new eCombination(this, true);

            this.moment = moment;

            this.failed = false;
            this.isOverReinforced = false;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Gets the location of the nutral axis for already designed cross section.
        /// </summary>
        /// <returns></returns>
        public double GetNutralAxisDepth()
        {
            double As, AsPrim, fsPrim, fs, X, X_prev = 0.0;

            AsPrim = compCombination.Area;
            As = tensCombination.Area;

            X = (s.fyd * As - s.fyd * AsPrim) / (0.8 * b * c.fcd);
            if (X == 0)
                goto A;
            do
            {
                X_prev = X;

                if (AsPrim > 0.0)
                    fsPrim = (X - (Depth - compCombination.EffectiveDepth)) * eBasisOfDesign.ε_c_max_bending * s.E / X;
                else
                    fsPrim = 0.0;

                fs = (d - X) * eBasisOfDesign.ε_c_max_bending * s.E / X;

                if (fs > s.fyd)
                    fs = s.fyd;
                if (fsPrim > s.fyd)
                    fsPrim = s.fyd;

                X = (fs * As - fsPrim * AsPrim) / (0.8 * b * c.fcd);

            } while (Math.Round(X, 8) != Math.Round(X_prev, 8));
        A:
            return X;
        }
        /*
        backup for GetNutral axis depth
        double As1, As2, AsPrim, fsPrim, X;

            As2 = AsPrim = compCombination.Area;
            As1 = tensCombination.Area - As2;
            X = As1 * s.fyd / (0.8 * b * c.fcd);
            fsPrim = (X - (d - compCombination.EffectiveDepth)) * eBasisOfDesign.ε_c_max_bending * s.E / X;

            if (fsPrim < s.fyd)
            {
                double a1, b1, c1, x1, x2;
                while (Math.Round((As1 + As2) * s.fyd, 0) != Math.Round(0.8 * X * b * c.fcd + AsPrim * (X - (D - compCombination.EffectiveDepth)) * eBasisOfDesign.ε_c_max_bending * s.E / X, 0))
                {
                    a1 = 0.8 * b * c.fcd;
                    b1 = AsPrim * eBasisOfDesign.ε_c_max_bending * s.E - (As1 + As2) * s.fyd;
                    c1 = AsPrim * (D - compCombination.EffectiveDepth) * eBasisOfDesign.ε_c_max_bending * s.E;
                    x1 = 0.5 * (-b1 + Math.Pow(b1 * b1 - 4 * a1 * c1, 0.5));
                    x2 = 0.5 * (-b1 - Math.Pow(b1 * b1 - 4 * a1 * c1, 0.5));
                    X = x1 > 0 && x1 < d ? x1 : x2;
                }
            }
            return X;
        */

        /// <summary>
        /// Checks if the given area satisfies the minimum requirments.
        /// </summary>
        /// <param name="As">Area of steel to be checked for minimum requirments.</param>
        private void CheckAreaLimits(ref double As, double AsPrim)
        {
            if (As > eDetailing.Get_ρ_max() * b * d || AsPrim > eDetailing.Get_ρ_max() * b * d)
                this.isOverReinforced = true;
            else
                As = Math.Max(As, eDetailing.Get_ρ_min(eStructureType.Beam, s.Grade) * b * d);
        }

        /// <summary>
        /// Returns the maximum area of steel.
        /// </summary>
        /// <returns></returns>
        private double GetMaxAreaOfSteel()
        {
            // implementation of the formula AsMax = pmax*b*d.
            return eDetailing.Get_ρ_max() * b * d;
           
        }

        /// <summary>
        /// Designs the beam on which it is called.
        /// </summary>
        public override void Design()
        {
            List<double> d_store = new List<double>();
            d_store.AddRange(new double[] { 0, 0, 0, 0, 0 });

            double M1, M2, As1, As2, dcPrim;
            effectiveSLSdepth = eServiceability.GetMinEffDepth(s.Grade, member.EffectiveSpanLength, member.SpanType, eStructureType.Beam);
            dcPrim = Math.Max(eXBar.GetDiam(beam.MainBar1), eXBar.GetDiam(beam.MainBar2)) / 2 + eXBar.GetDiam(beam.StirupBar) + beam.Cover;
            d = D - dcPrim;

            d_store.Add(d);
            d_store.RemoveAt(0);

            do
            {
                M1 = (Math.Min(GetMomentCapacity(), Math.Abs(M))) * (Math.Abs(M) / M);
                M2 = M - M1;
                As1 = GetSteelRatio(M1) * b * d;
                if (failed)
                    return;
                As2 = M2 / (s.fyd * (d - dcPrim));

                this.As = As1 + As2;

                X = GetSteelRatio(M1) * s.fyd / (0.8 * c.fcd) * d;
                this.AsPrim = Math.Abs(M2) / (GetCompSteelStress(dcPrim, X) * (d - dcPrim));

                CheckAreaLimits(ref As, AsPrim);

                if (this.isOverReinforced)
                {
                    this.failureNote = "Reinforcement limit Exceeded";
                    return;
                }

                tensCombination.FillCombinations(As, GetMaxAreaOfSteel(), d);
                compCombination.FillCombinations(AsPrim, GetMaxAreaOfSteel(), d);

                X = GetNutralAxisDepth();
                if (d < effectiveSLSdepth)
                {
                    this.failed = true;
                    this.failureNote = "Depth failed by SLS";
                    return;
                }
                if (!tensCombination.CheckConjustion(X) && !compCombination.CheckConjustion(D - X))
                {
                    this.failed = true;
                    this.failureNote = "Depth failed by Conjestion";
                    return;
                }
                if (tensCombination.Area == 0)
                    return;

                foreach (double di in d_store)
                {
                    if (tensCombination.EffectiveDepth == di)
                    {
                        this.depthTolerance += 2;
                        break;
                    }
                }

                designCompleted = CheckDepthTolerance(d, tensCombination.EffectiveDepth) && CheckDepthTolerance(D - compCombination.EffectiveDepth, dcPrim);
                dcPrim = D - compCombination.EffectiveDepth;

                d_store.Add(d);
                d_store.RemoveAt(0);

                d = tensCombination.EffectiveDepth;
            }
            while (!designCompleted);

            this.depthTolerance = 5;
        }

        /// <summary>
        /// Checks if a given NewDepth is within specified tolerance of a given OldDepth.
        /// </summary>
        /// <param name="OldDepth">The previous depth.</param>
        /// <param name="NewDepth">The new depth.</param>
        /// <returns></returns>
        private bool CheckDepthTolerance(double OldDepth, double NewDepth)
        {
            //checks if the new depth is within the specified tolerance relative to the old depth.
            if ((Math.Round(NewDepth, 3) <= Math.Round(OldDepth + depthTolerance, 3)) && (Math.Round(NewDepth, 3) >= Math.Round(OldDepth, 3)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the steel ration for a given moment capacity and effective depth.
        /// </summary>
        /// <param name="Md">The moment capacity of the section if it is singly reinforced.</param>
        /// <returns></returns>
        private double GetSteelRatio(double Md)
        {
            double minSteelRatio = Math.Round(eDetailing.Get_ρ_min(eStructureType.Beam, s.Grade), 9);
            double maxSteelRatio = Math.Round(eFlexure.Get_k_x_max() * 0.8 * c.fcd / s.fyd, 9);

            double m = s.fyd / (0.8 * c.fcd);
            double c1 = 2.5 / m;
            double c2 = 0.32 * Math.Pow(m, 2) * c.fcd;

            //double steelRatio1 = 0.5 * (c1 + Math.Sqrt(Math.Pow(c1, 2) - (4 * Math.Abs(Md)) / (b * Math.Pow(d, 2) * c2)));
            double steelRatio2 = 0.5 * (c1 - Math.Sqrt(Math.Pow(c1, 2) - (4 * Math.Abs(Md)) / (b * Math.Pow(d, 2) * c2)));

            if (steelRatio2 < maxSteelRatio)
                return Math.Min(minSteelRatio, steelRatio2);
            else
            {
                this.failed = true;
                this.failureNote = "ρ max exceeded!";
                return maxSteelRatio;
            }

            //if ((steelRatio1 >= minSteelRatio) && (steelRatio1 <= maxSteelRatio) && (steelRatio2 >= minSteelRatio) && (steelRatio2 <= maxSteelRatio))
            //{
            //    return Math.Min(steelRatio1, steelRatio2);
            //}
            //else if ((steelRatio1 >= minSteelRatio) && (steelRatio1 <= maxSteelRatio))
            //{
            //    return steelRatio1;
            //}
            //else if ((steelRatio2 >= minSteelRatio) && (steelRatio2 <= maxSteelRatio))
            //{
            //    return steelRatio2;
            //}
            //else if ((steelRatio1 <= minSteelRatio) || steelRatio2 <= minSteelRatio)
            //{
            //    return minSteelRatio;
            //}
            //else 
            //{
            //    throw new eImposibleSteelRatioException("The actual steel ratios are one bellow the absolute minimum and the other above the absolute maximum steel ratio.", steelRatio1, steelRatio2, maxSteelRatio);
            //}
        }

        /// <summary>
        /// Returns the singly reinforced moment capacity.
        /// </summary>
        private double GetMomentCapacity()
        {
            double kx = eFlexure.Get_k_x_max() ;
            //Returns the Moment capacity applying the given reduction factor.
            return 0.8 * kx * b * Math.Pow(d, 2) * c.fcd * (1 - 0.4 * kx);
        }

        /// <summary>
        /// Returns the stress in compresion area of steel.
        /// </summary>
        private double GetCompSteelStress(double dcPrim, double NeutralAxisDepth)
        {
            //calculates the stress in the top compresion steel.
            double stress = s.E * eBasisOfDesign.ε_c_max_bending * (NeutralAxisDepth - dcPrim) / NeutralAxisDepth;

            if (stress > s.fyd)
            {
                //If the stress is above the yeild strength it returns the yeild strength.
                return s.fyd;
            }
            else
            {
                //else if it is below it return the calculated stress.
                return stress;
            }
        }

        /// <summary>
        /// Returns the cracked moment of area for this beam section.
        /// </summary>
        /// <param name="slabBeam">True if the beam is embeded in the slab and false other wise.</param>
        /// <returns></returns>
        public double GetCracked_I(bool slabBeam = false)
        {

            if (slabBeam)
                return b * Math.Pow(D, 3) / 12;
            else
            {
                double X, kx, n, p1, p2, dc;
                X = GetNutralAxisDepth();
                n = s.E / c.E;
                p1 = tensCombination.Area / (b * d);
                p2 = compCombination.Area / (b * d);
                dc = D - compCombination.EffectiveDepth;
                kx = -n * (p1 + p2) + Math.Pow((Math.Pow(n, 2) * Math.Pow(p1 + p2, 2) + 2 * n * (p2 * dc / d + p1)), 2);
                return 1 / 3 * Math.Pow(kx, 3) + p2 * n * Math.Pow(kx - dc / d, 2) + p1 * n * Math.Pow(1 - kx, 2);
            }
        }

        /// <summary>
        /// Returns the effective width of the beam on which logtudinal shearBar are going to be distributed.
        /// </summary>
        /// <returns></returns>
        public double GetEffWidth()
        {
            return b - 2 * (beam.Cover + stirrupD);
        }

        /// <summary>
        /// Tests an object is the same as this flexure section.
        /// </summary>
        public override bool Equals(object obj)
        {
            try
            {
                eDFlexureSection op1 = obj as eDFlexureSection;
                eDFlexureSection op2 = this;

                if (op1.beam.ClassWork != op2.beam.ClassWork)
                    return false;
                if (op1.b != op2.b)
                    return false;
                if (!op1.c.Equals(op2.c))
                    return false;
                if (op1.beam.Cover != op2.beam.Cover)
                    return false;
                if (op1.D != op2.D)
                    return false;
                if (Math.Round(op1.moment, 3) != Math.Round(op2.moment, 3))
                    return false;
                if (!op1.s.Equals(op2.s))
                    return false;
                if (op1.beam.StirrupPosn != op2.beam.StirrupPosn)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the steel ratio of the section after the completion of the design of the section.
        /// </summary>
        public double GetSteelRatio()
        {
            return tensCombination.Area / (Width * d);
        }

        internal bool Contains(eReinforcement bar, bool inTensileCombination = true)
        {
            if (inTensileCombination && tensCombination.Rows != null)
            {
                foreach (eCombination.eRow r in tensCombination.Rows)
                {
                    foreach (eXBar b in r.Bars)
                    {
                        if (b.Diameter == eXBar.GetDiam(bar))
                            return true;
                    }
                }
                return false;
            }
            else if (compCombination.Rows != null)
            {
                foreach (eCombination.eRow r in this.compCombination.Rows)
                {
                    foreach (eXBar b in r.Bars)
                    {
                        if (b.Diameter == eXBar.GetDiam(bar))
                            return true;
                    }
                }
                return false;
            }
            else
                return false;
        }

        internal int Count(double diameter, bool inTesileCombintion = true)
        {
            int count = 0;

            if (inTesileCombintion)
            {
                foreach (eCombination.eRow r in tensCombination.Rows)
                {
                    foreach (eXBar b in r.Bars)
                    {
                        if (b.Diameter == diameter)
                            count++;
                    }
                }
            }
            else
            {
                foreach (eCombination.eRow r in this.compCombination.Rows)
                {
                    foreach (eXBar b in r.Bars)
                    {
                        if (b.Diameter == diameter)
                            count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Gets the area of steel calculated theorotically.
        /// </summary>
        /// <param name="tensile">Value indicating if the concerned area of steel is that of tensile or not.</param>
        internal double GetAs_calc(bool tensile = true)
        {
            if (tensile)
                return As;
            else
                return AsPrim;
        }

        /// <summary>
        /// Gets the area of steel actually used in the section.
        /// </summary>
        /// <param name="tensile">Value indicating if the concerned area of steel is that of tensile or not.</param>
        internal double GetAs_used(bool tensile = true)
        {
            if (tensile)
                return tensCombination.Area;
            else
                return compCombination.Area;
        }

        public override string ToString()
        {
            if (this.failed || this.isOverReinforced)
                return this.failureNote;
            if (this.IsNegative)
                return "Top:" + tensCombination.ToString() + " Bottom:" + compCombination.ToString();
            else
                return "Top:" + compCombination.ToString() + " Bottom:" + tensCombination.ToString();
        }

        /// <summary>
        /// Fills all the longitudinal bars that represent the section.
        /// </summary>
        internal void FillLongitudinalBars()
        {
            this.tensCombination.FillLongtBars();
            this.compCombination.FillLongtBars();
        }

        /// <summary>
        /// Returns true if the given section has exactly the same number of bars in the top and bottom.
        /// </summary>
        /// <param name="sec">The section to compare with.</param>
        /// <returns>True if the given section has the same number of bars.</returns>
        internal override bool IsSimilar(eDSection section)
        {
            eDFlexureSection sec = section as eDFlexureSection;

            if (sec == null)
                return false;
            if (this.failed || sec.failed)
                return false;
            if (this.isOverReinforced || sec.isOverReinforced)
                return false;
            if (this.IsNegative != sec.IsNegative)
                return false;
            if (this.Depth != sec.Depth)
                return false;
            if (this.Width != sec.Width)
                return false;
            if (this.NumOfCompBar1 != sec.NumOfCompBar1)
                return false;
            if (this.NumOfCompBar2 != sec.NumOfCompBar2)
                return false;
            if (this.NumOfTensBar1 != sec.NumOfTensBar1)
                return false;
            if (this.NumOfTensBar2 != sec.NumOfTensBar2)
                return false;

            return true;
        }
        #endregion
    }
}
