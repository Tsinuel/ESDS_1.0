using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Mechanics.Analysis;
using ESADS.Mechanics.Analysis.Beam;
using ESADS.Code;

namespace ESADS.Mechanics.Design.Beam
{
    /// <summary>
    /// Represents a member in continious beam structure used for design purpose.
    /// </summary>
    public class eDMember
    {
        #region Feilds
        /// <summary>
        /// Holds a value for property 'Member_Analysis'.
        /// </summary>
        private eAMember member_Analysis;
        /// <summary>
        /// Holds the value of the 'ShearSection' property.
        /// </summary>
        private eDShearSection shearSection;
        /// <summary>
        /// Holds the value of the 'NumOfShearSxns' property.
        /// </summary>
        private int numOfShearSxns;
        /// <summary>
        /// Holds the value of 'Beam'.
        /// </summary>
        private eDBeam beam;
        /// <summary>
        /// Holds the value of 'SpanType'.
        /// </summary>
        private eSpanType spanType;
        /// <summary>
        /// Holds the value of 'EffectiveSpanLength'.
        /// </summary>
        private double effectiveSpan;
        /// <summary>
        /// Holds the value of 'SupportSxn_Left'.
        /// </summary>
        private eDFlexureSection supportSxn_Left;
        /// <summary>
        /// Holds the value of 'SupportSxn_Right'.
        /// </summary>
        private eDFlexureSection supportSxn_Right;
        /// <summary>
        /// Holds the value of 'SpanSxn'.
        /// </summary>
        private eDFlexureSection spanSxn;
        /// <summary>
        /// Holds the value of 'Section'.
        /// </summary>
        private eBeamSection section;
        private eLongtBar bar1;
        private eLongtBar bar2;
        private eLongtBar bar3;
        private eLongtBar bar4;
        private eLongtBar bar5;
        private eLongtBar bar6;
        private eLongtBar bar7;
        private eLongtBar bar8;
        private eLongtBar bar9;
        private eLongtBar bar10;
        private eLongtBar bar11;
        private eLongtBar bar12;
        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new design component of a continuous beam from the beam to be designed and the analysis component of the member.
        /// </summary>
        /// <param name="beam">The design beam object bearing this member.</param>
        /// <param name="member_Analysis">The analysis member attached to this design member.</param>
        public eDMember(eDBeam beam, eAMember member_Analysis)
        {
            this.member_Analysis = member_Analysis;
            this.section = beam.DefaultSection;
            this.beam = beam;
        }

        /// <summary>
        /// Creates a new design component of a member from the beam, analysis component of the member and the cross-section of the member.
        /// </summary>
        /// <param name="beam">The design beam object bearing this member.</param>
        /// <param name="member_Analysis">The analysis member attached to this design member.</param>
        /// <param name="section">Represents the cross-section of the member.</param>
        public eDMember(eDBeam beam, eAMember member_Analysis, eBeamSection section)
        {
            this.member_Analysis = member_Analysis;
            this.section = section;
            this.beam = beam;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets the concrete material used for this member and the whole beam.
        /// </summary>
        public eConcrete Concrete
        {
            get
            {
                return beam.Concrete;
            }
        }

        /// <summary>
        /// Gets or the steel material used fo this member and for the whole beam.
        /// </summary>
        public eSteel Steel
        {
            get
            {
                return beam.Steel;
            }
        }

        /// <summary>
        /// Gets or set the depth of this member.
        /// </summary>
        public double Depth
        {
            get
            {
                return this.section.Depth;
            }
            set
            {
                section.Depth = value;
            }
        }

        /// <summary>
        /// Gets or set the width of this member.
        /// </summary>
        public double Width
        {
            get
            {
                return this.section.Width;
            }
            set
            {
                this.section.Width = value;
            }
        }

        /// <summary>
        /// Gets or sets the member used in the anaysis.
        /// </summary>
        public eAMember Member_Analysis
        {
            get
            {
                return member_Analysis;
            }
            set
            {
                member_Analysis = value;
            }
        }

        /// <summary>
        /// Gets the representative shear section of the member that holds the concrete shear capacity.
        /// </summary>
        public eDShearSection ShearSection
        {
            get
            {
                return shearSection;
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of shear sections taken wihtin the member.
        /// </summary>
        public int NumOfShearSxns
        {
            get
            {
                return numOfShearSxns;
            }
            set
            {
                if (value > 0)
                    numOfShearSxns = value;
            }
        }

        /// <summary>
        /// Gets the beam bearing this member.
        /// </summary>
        public eDBeam Beam
        {
            get
            {
                return this.beam;
            }
        }

        /// <summary>
        /// Gets the span type of the member.
        /// </summary>
        public eSpanType SpanType
        {
            get
            {
                return spanType;
            }
        }

        /// <summary>
        /// Gets or sets the effective span length of the member.
        /// </summary>
        public double EffectiveSpanLength
        {
            get
            {
                return this.member_Analysis.Length;
            }
            set
            {
                this.effectiveSpan = value;
            }
        }

        /// <summary>
        /// Gets or sets the flexure section of the member at the left end of the member.
        /// </summary>
        public eDFlexureSection SupportSxn_Left
        {
            get
            {
                return supportSxn_Left;
            }
            set
            {
                supportSxn_Left = value;
                supportSxn_Left.ExtendFrom = spanSxn;
            }
        }

        /// <summary>
        /// Gets or sets the flexure section of the member at the right end of the member.
        /// </summary>
        public eDFlexureSection SupportSxn_Right
        {
            get
            {
                return supportSxn_Right;
            }
            set
            {
                supportSxn_Right = value;
                supportSxn_Right.ExtendFrom = spanSxn;
            }
        }

        /// <summary>
        /// Gets or sets the flexure section of the member at the span.
        /// </summary>
        public eDFlexureSection SpanSxn
        {
            get
            {
                return spanSxn;
            }
            set
            {
                spanSxn = value;
                if (supportSxn_Left != null)
                    supportSxn_Left.ExtendFrom = spanSxn;
                if (supportSxn_Right != null)
                    supportSxn_Right.ExtendFrom = spanSxn;
            }
        }

        /// <summary>
        /// Gets or sets the section of the member.
        /// </summary>
        public eBeamSection Section
        {
            get
            {
                return this.section;
            }
            set
            {
                this.section = value;
            }
        }

        /// <summary>
        /// Gets or sets the longitudinal bar of bartype1 in the tension bars of left support.
        /// </summary>
        public eLongtBar Bar1
        {
            get
            {
                return this.bar1;
            }
            set
            {
                this.bar1 = value;
            }
        }

        /// <summary>
        /// Gets or sets the longitudinal bar of bartype2 in the tension bars of the left support.
        /// </summary>
        public eLongtBar Bar2
        {
            get
            {
                return this.bar2;
            }
            set
            {
                this.bar2 = value;
            }
        }

        /// <summary>
        /// Gets or sets the longitudinal bar of bartype1 in the compression bars of the span section.
        /// </summary>
        public eLongtBar Bar3
        {
            get
            {
                return this.bar3;
            }
            set
            {
                this.bar3 = value;
            }
        }

        /// <summary>
        /// Gets or sets the longitudinal bar of bartype2 in the compression bars of the span section.
        /// </summary>
        public eLongtBar Bar4
        {
            get
            {
                return this.bar4;
            }
            set
            {
                this.bar4 = value;
            }
        }

        /// <summary>
        /// Gets or sets the longitudinal bar of bartype1 in the compression bars of the left support.
        /// </summary>
        public eLongtBar Bar5
        {
            get
            {
                return this.bar5;
            }
            set
            {
                this.bar5 = value;
            }
        }

        /// <summary>
        /// Gets or sets the longitudinal bar of bartype2 in the compression bars of the left support.
        /// </summary>
        public eLongtBar Bar6
        {
            get
            {
                return this.bar6;
            }
            set
            {
                this.bar6 = value;
            }
        }

        /// <summary>
        /// Gets or sets the longitudianl bar of bartype1 in the tension bars of the span section.
        /// </summary>
        public eLongtBar Bar7
        {
            get
            {
                return this.bar7;
            }
            set
            {
                this.bar7 = value;
            }
        }

        /// <summary>
        /// Gets or sets the longitudinal bar of bartype2 in the tension bars of the span section.
        /// </summary>
        public eLongtBar Bar8
        {
            get
            {
                return this.bar8;
            }
            set
            {
                this.bar8 = value;
            }
        }

        /// <summary>
        /// Gets or sets the longitudinal bar of bartype1 in the tension bars of the right support section.
        /// </summary>
        public eLongtBar Bar9
        {
            get
            {
                return this.bar9;
            }
            set
            {
                this.bar9 = value;
            }
        }

        /// <summary>
        /// Gets or sets the longitudinal bar of bartype2 in the tension bars of the right support section.
        /// </summary>
        public eLongtBar Bar10
        {
            get
            {
                return this.bar10;
            }
            set
            {
                this.bar10 = value;
            }
        }

        /// <summary>
        /// Gets or sets the longitudinal bar of bartype1 in the compression bars of the right support section.
        /// </summary>
        public eLongtBar Bar11
        {
            get
            {
                return this.bar11;
            }
            set
            {
                this.bar11 = value;
            }
        }

        /// <summary>
        /// Gets or sets the longitudinal bar of bartype2 in the compression bar of the right support section.
        /// </summary>
        public eLongtBar Bar12
        {
            get
            {
                return this.bar12;
            }
            set
            {
                this.bar12 = value;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Sets the span type of member
        /// </summary>
        /// <param name="type">The span type to assign.</param>
        public void SetSpanType(eSpanType type)
        {
            this.spanType = type;
        }

        /// <summary>
        /// Fills the longitudinal bars.
        /// </summary>
        internal void FillLongtBars()
        {
            if (this.supportSxn_Left != null)
                this.supportSxn_Left.FillLongitudinalBars();
            if (this.supportSxn_Right != null)
                this.supportSxn_Right.FillLongitudinalBars();
            if (this.spanSxn != null)
                this.spanSxn.FillLongitudinalBars();
        }

        internal void MergeLongtBars(List<eLongtBar> mainBarsCollection)
        {
            int num1 = 0, num2 = 0;

            if (supportSxn_Left != null)
            {
                if (supportSxn_Left.TensileComb.LongtBar1 != null)
                    bar1 = new eLongtBar(supportSxn_Left.Bar1, supportSxn_Left, supportSxn_Left.TensileComb.LongtBar1.Number, false, false, true);
                if (supportSxn_Left.TensileComb.LongtBar2 != null)
                    bar2 = new eLongtBar(supportSxn_Left.Bar2, supportSxn_Left, supportSxn_Left.TensileComb.LongtBar2.Number, false, false, true);
                if (supportSxn_Left.CompresionComb.LongtBar1 != null)
                    bar5 = new eLongtBar(supportSxn_Left.Bar1, supportSxn_Left, supportSxn_Left.CompresionComb.LongtBar1.Number, true, false, true);
                if (supportSxn_Left.CompresionComb.LongtBar2 != null)
                    bar6 = new eLongtBar(supportSxn_Left.Bar2, supportSxn_Left, supportSxn_Left.CompresionComb.LongtBar2.Number, true, false, true);
            }

            if (spanSxn != null)
            {
                if (spanSxn.CompresionComb.LongtBar1 != null)
                    bar3 = new eLongtBar(spanSxn.Bar1, spanSxn, spanSxn.CompresionComb.LongtBar1.Number, true, true, false);
                if (spanSxn.CompresionComb.LongtBar2 != null)
                    bar4 = new eLongtBar(spanSxn.Bar2, spanSxn, spanSxn.CompresionComb.LongtBar2.Number, true, true, false);
                if (spanSxn.TensileComb.LongtBar1 != null)
                    bar7 = new eLongtBar(spanSxn.Bar1, spanSxn, spanSxn.TensileComb.LongtBar1.Number, false, true, false);
                if (spanSxn.TensileComb.LongtBar2 != null)
                    bar8 = new eLongtBar(spanSxn.Bar2, spanSxn, spanSxn.TensileComb.LongtBar2.Number, false, true, false);
            }

            if (supportSxn_Right != null)
            {
                if (supportSxn_Right.TensileComb.LongtBar1 != null)
                    bar9 = new eLongtBar(supportSxn_Right.Bar1, supportSxn_Right, supportSxn_Right.TensileComb.LongtBar1.Number, false, false, false);
                if (supportSxn_Right.TensileComb.LongtBar2 != null)
                    bar10 = new eLongtBar(supportSxn_Right.Bar2, supportSxn_Right, supportSxn_Right.TensileComb.LongtBar2.Number, false, false, false);
                if (supportSxn_Right.CompresionComb.LongtBar1 != null)
                    bar11 = new eLongtBar(supportSxn_Right.Bar1, supportSxn_Right, supportSxn_Right.CompresionComb.LongtBar1.Number, true, false, false);
                if (supportSxn_Right.CompresionComb.LongtBar2 != null)
                    bar12 = new eLongtBar(supportSxn_Right.Bar2, supportSxn_Right, supportSxn_Right.CompresionComb.LongtBar2.Number, true, false, false);
            }

            if (bar1 != null)
                num1 = bar1.Number;
            if (bar2 != null)
                num2 = bar2.Number;
            if (bar1 != null && bar3 != null)
                bar1.Number -= bar3.Number;
            if (bar2 != null && bar4 != null)
                bar2.Number -= bar4.Number;

            if (bar1 != null && bar3 != null && bar4 != null && num1 >= bar3.Number && num2 < bar4.Number)
                bar1.Number -= NumberOf((bar4.Number - num2) * bar4.Area, bar1.Area);
            else if (bar2 != null && bar3 != null && bar4 != null && num2 >= bar4.Number)
                bar2.Number -= NumberOf((bar3.Number - num1) * bar3.Area, bar2.Area);

            if (bar5 != null)
                num1 = bar5.Number;
            if (bar6 != null)
                num2 = bar6.Number;
            if (bar5 != null && bar7 != null)
                bar5.Number -= bar7.Number;
            if (bar6 != null && bar8 != null)
                bar6.Number -= bar8.Number;

            if (bar5 != null && bar7 != null && bar8 != null && num1 >= bar7.Number && num2 < bar8.Number)
                bar5.Number -= NumberOf((bar8.Number - num2) * bar8.Area, bar5.Area);
            else if (bar4 != null && bar6 != null && bar7 != null && num2 >= bar4.Number)
                bar6.Number -= NumberOf((bar7.Number - num1) * bar7.Area, bar6.Area);

            if (bar9 != null)
                num1 = bar9.Number;
            if (bar10 != null)
                num2 = bar10.Number;
            if (bar9 != null && bar3 != null)
                bar9.Number -= bar3.Number;
            if (bar10 != null && bar4 != null)
                bar10.Number -= bar4.Number;

            if (bar9 != null && bar3 != null && bar4 != null && num1 >= bar3.Number && num2 < bar4.Number)
                bar9.Number -= NumberOf((bar4.Number - num2) * bar4.Area, bar9.Area);
            else if (bar10 != null && bar3 != null && bar4 != null && num2 >= bar4.Number)
                bar10.Number -= NumberOf((bar3.Number - num1) * bar3.Area, bar10.Area);

            if (bar11 != null)
                num1 = bar11.Number;
            if (bar12 != null)
                num2 = bar12.Number;
            if (bar11 != null && bar7 != null)
                bar11.Number -= bar7.Number;
            if (bar12 != null && bar8 != null)
                bar12.Number -= bar8.Number;

            if (bar11 != null && bar7 != null && bar8 != null && num1 >= bar7.Number && num2 < bar8.Number)
                bar11.Number -= NumberOf((bar8.Number - num2) * bar8.Area, bar11.Area);
            else if (bar12 != null && bar7 != null && bar4 != null && num2 >= bar4.Number)
                bar12.Number -= NumberOf((bar7.Number - num1) * bar7.Area, bar12.Area);

            if (bar1 != null && bar1.Number > 0)
                mainBarsCollection.Add(bar1);
            else
                bar1 = null;

            if (bar2 != null && bar2.Number > 0)
                mainBarsCollection.Add(bar2);
            else
                bar2 = null;

            if (bar3 != null && bar3.Number > 0)
                mainBarsCollection.Add(bar3);
            else
                bar3 = null;

            if (bar4 != null && bar4.Number > 0)
                mainBarsCollection.Add(bar4);
            else
                bar4 = null;

            if (bar5 != null && bar5.Number > 0)
                mainBarsCollection.Add(bar5);
            else
                bar5 = null;

            if (bar6 != null && bar6.Number > 0)
                mainBarsCollection.Add(bar6);
            else
                bar6 = null;

            if (bar7 != null && bar7.Number > 0)
                mainBarsCollection.Add(bar7);
            else
                bar7 = null;

            if (bar8 != null && bar8.Number > 0)
                mainBarsCollection.Add(bar8);
            else
                bar8 = null;

            if (bar9 != null && bar9.Number > 0)
                mainBarsCollection.Add(bar9);
            else
                bar9 = null;

            if (bar10 != null && bar10.Number > 0)
                mainBarsCollection.Add(bar10);
            else
                bar10 = null;

            if (bar11 != null && bar11.Number > 0)
                mainBarsCollection.Add(bar11);
            else
                bar11 = null;

            if (bar12 != null && bar12.Number > 0)
                mainBarsCollection.Add(bar12);
            else
                bar12 = null;
        }

        private double AreaOf(int num, double a)
        {
            return num * a;
        }

        private int NumberOf(double A, double a, bool roundUp = false)
        {
            if (A <= 0)
                return 0;
            if (roundUp)
            {
                if (A % a == 0)
                    return (int)(A / a);
                else
                    return (int)(A / a) + 1;
            }
            else
            {
                return (int)(A / a);
            }
        }
        #endregion
    }
}
