using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ESADS;
using ESADS.Code;
using ESADS.Code.EBCS_1995;

namespace ESADS.Mechanics.Design.Beam
{
    public class eLongtBar : eIBar
    {
        #region Feilds
        /// <summary>
        /// Hold a value for property 'Diameter'.
        /// </summary>
        private double diameter;
        /// <summary>
        /// Hold a value for property 'Lengths'.
        /// </summary>
        private double[] lengths;
        /// <summary>
        /// Hold a value for property 'Name'.
        /// </summary>
        private string name;
        /// <summary>
        /// Holds the value of 'IsInCompression'.
        /// </summary>
        private bool isInCompression;
        /// <summary>
        /// Holds the value of 'Section'.
        /// </summary>
        private eDFlexureSection section;
        /// <summary>
        /// Holds the value of 'Number'.
        /// </summary>
        private int number;
        /// <summary>
        /// Holds the value of 'IsBent'.
        /// </summary>
        private bool isBent;
        private double length;
        private List<PointF> Points;
        /// <summary>
        /// Holds the value of 'BentOnTheLeft'.
        /// </summary>
        private bool bentOnTheLeft;
        /// <summary>
        /// Holds the value of 'BentOnTheRight'.
        /// </summary>
        private bool bentOnTheRight;
        /// <summary>
        /// Holds the value of 'Start'.
        /// </summary>
        private double start;
        /// <summary>
        /// Holds the value of 'End'.
        /// </summary>
        private double end;
        /// <summary>
        /// Holds the value of 'IsInSpan'.
        /// </summary>
        private bool isInSpan;
        private bool isOnLeft;
        private bool isTop;
        private int level;
        private eLongtBarTypes type;
        private int subLevel;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the diamter of the Bar.
        /// </summary>
        public double Diameter
        {
            get
            {
                return diameter;
            }
            set
            {
                diameter = value;
            }
        }

        /// <summary>
        /// Gets the area of the single Bar.
        /// </summary>
        public double Area
        {
            get { return eXBar.GetArea(diameter); }
        }

        /// <summary>
        /// Gets the total length of the shearBar.
        /// </summary>
        public double Length
        {
            get { return length; }
        }

        /// <summary>
        /// Gets the lengths of each segment.
        /// </summary>
        public double[] Lengths
        {
            get { return lengths; }
        }

        /// <summary>
        /// Gets the name or the mark of the shearBar.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Gets the value if the bar is found at the top of the section
        /// </summary>
        public bool IsTop
        {
            get
            {
                return this.isInCompression && this.isInSpan || !this.isInCompression && !this.isInSpan;
            }
        }

        /// <summary>
        /// Gets the value if the bar is compression bar.
        /// </summary>
        public bool IsInCompression
        {
            get
            {
                return this.isInCompression;
            }
        }

        /// <summary>
        /// Gets the flexure section  which bears this bar.
        /// </summary>
        public eDFlexureSection Section
        {
            get
            {
                return this.section;
            }
        }

        /// <summary>
        /// Gets the number of this bar.
        /// </summary>
        public int Number
        {
            get
            {
                return this.number;
            }
            set
            {
                if (value > 0)
                    this.number = value;
                else
                    this.number = 0;
            }
        }

        /// <summary>
        /// Gets the value if the bar is a bent bar or not.
        /// </summary>
        internal bool IsBent
        {
            get
            {
                return isBent;
            }
        }

        /// <summary>
        /// Gets or sets the value if the bar is bent on its left end.
        /// </summary>
        internal bool BentOnTheLeft
        {
            get
            {
                return bentOnTheLeft;
            }
            set
            {
                bentOnTheLeft = value;
            }
        }

        /// <summary>
        /// Gets or sets the value if the bar is bent on its right end.
        /// </summary>
        internal bool BentOnTheRight
        {
            get
            {
                return bentOnTheRight;
            }
            set
            {
                bentOnTheRight = value;
            }
        }

        /// <summary>
        /// Gets the value if the bar is founded in the span section of a member.
        /// </summary>
        public bool IsInSpan
        {
            get
            {
                return this.isInSpan;
            }
        }

        /// <summary>
        /// Gets or sets the start point of the reinforcement as distance from the left most point of the whole beam.
        /// </summary>
        public double Start
        {
            get
            {
                return this.start;
            }
            set
            {
                this.length += (this.start - value);
                this.start = value;
            }
        }

        /// <summary>
        /// Gets or sets the end point of the reinforcement as a distance from the left most end of the whole beam.
        /// </summary>
        public double End
        {
            get
            {
                return this.end;
            }
            set
            {
                this.length += (value - this.end);
                this.end = value;
            }
        }

        /// <summary>
        /// Gets or sets the level of the bar in the detailing of the beam.
        /// </summary>
        public int Level
        {
            get
            {
                return this.level;
            }
            set
            {
                if (value > 0)
                    this.level = value;
                else
                    throw new Exception("The level value of a longitudinal bar cannot be zero or negative!");
            }
        }

        /// <summary>
        /// Gets or sets the longitudinal bar type.
        /// </summary>
        public eLongtBarTypes Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        /// <summary>
        /// Gets or sets the drawing level of the bar if there is/are other bars of the same longitudinal bar type.
        /// </summary>
        public int SubLevel
        {
            get
            {
                return this.subLevel;
            }
            set
            {
                this.subLevel = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates new instance of a longitudinal bar structure.
        /// </summary>
        /// <param name="diameter">The diameter of the longitudinal bar.</param>
        /// <param name="section">The flexure section in which the longitudinal bar is primarily found.</param>
        /// <param name="number">The number of such flexural bars represented by this longitudinal bars.</param>
        /// <param name="isInCompression">Value indicating whether the bar is in compression or not.</param>
        /// <param name="isInSpan">Value indicating if the bar is basically found in the span section of  a member.</param>
        /// <param name="isOnLeft">Value indicating if the bar is found on the left support section of the member it is found in.</param>
        public eLongtBar(double diameter, eDFlexureSection section, int number, bool isInCompression, bool isInSpan, bool isOnLeft)
        {
            this.diameter = diameter;
            this.section = section;
            this.isInCompression = isInCompression;
            this.isInSpan = isInSpan;
            this.isOnLeft = isOnLeft;
            this.isBent = false;
            this.number = number;

            FillType();
            this.level = (int)this.type;
            this.subLevel = 0;

            FillLength();
        }

        private void FillType()
        {
            if (this.isInSpan)
                if (this.isInCompression)
                    if (diameter == section.Bar1)
                        this.type = eLongtBarTypes.Bar3;
                    else
                        this.type = eLongtBarTypes.Bar4;
                else
                    if (diameter == section.Bar1)
                        this.type = eLongtBarTypes.Bar7;
                    else
                        this.type = eLongtBarTypes.Bar8;
            else if (this.isOnLeft)
                if (this.isInCompression)
                    if (diameter == section.Bar1)
                        this.type = eLongtBarTypes.Bar5;
                    else
                        this.type = eLongtBarTypes.Bar6;
                else
                    if (diameter == section.Bar1)
                        this.type = eLongtBarTypes.Bar1;
                    else
                        this.type = eLongtBarTypes.Bar2;
            else
                if (this.isInCompression)
                    if (diameter == section.Bar1)
                        this.type = eLongtBarTypes.Bar11;
                    else
                        this.type = eLongtBarTypes.Bar12;
                else
                    if (diameter == section.Bar1)
                        this.type = eLongtBarTypes.Bar9;
                    else
                        this.type = eLongtBarTypes.Bar10;

        }

        private void FillLength()
        {
            double l;
            double fbd = diameter == 6.0 ? 2 * section.Beam.Concrete.fctd : section.Beam.Concrete.fctd;
            double lb = (diameter * section.Beam.Steel.fyd) / (4 * fbd);

            double lb_min, lb_net;

            if (this.isInCompression)
            {
                lb_min = Math.Max(0.6 * lb, Math.Max(10 * diameter, eUtility.Convert(200, eLengthUnits.mm, eUtility.SLU)));
                lb_net = Math.Max(lb * section.GetAs_calc(false) / section.GetAs_used(false), lb_min);
            }
            else
            {
                lb_min = Math.Max(0.3 * lb, Math.Max(10 * diameter, eUtility.Convert(200, eLengthUnits.mm, eUtility.SLU)));
                lb_net = Math.Max(lb * section.GetAs_calc() / section.GetAs_used(), lb_min);
            }

            if (this.isInSpan)
                l = section.Member.EffectiveSpanLength;
            else if (this.isInCompression)
                l = section.Beam.SupportCompBarCuttingLength * section.Member.Member_Analysis.Length;
            else
                l = section.Beam.SupportTensBarCuttingLength * section.Member.Member_Analysis.Length;

            this.length = l < lb_net ? lb_net : l;

            if (this.isInSpan)
            {
                start = section.Member.Member_Analysis.Start;
                end = section.Member.Member_Analysis.End;
            }
            else
            {
                if (isOnLeft)
                {
                    start = section.Member.Member_Analysis.Start;
                    if (section.Member.SpanSxn == null)
                        end = Math.Max(section.Member.Member_Analysis.Start + l, section.Member.Member_Analysis.End);
                    else
                        end = section.Member.Member_Analysis.Start + l;
                }
                else
                {
                    if (section.Member.SpanSxn == null)
                        start = Math.Min(section.Member.Member_Analysis.End - l, section.Member.Member_Analysis.Start);
                    else
                        start = section.Member.Member_Analysis.End - l;
                    end = section.Member.Member_Analysis.End;
                }
            }
        }
        #endregion

        #region Mehods
        /// <summary>
        /// Fills the length limits for the bar.
        /// </summary>
        private void FillLengthLimits()
        {

        }

        /// <summary>
        /// Fills the anchorage lengths for the bar.
        /// </summary>
        private void FillAnchorageLengths()
        {
            CheckRequiredAnchorageLength();
        }

        private void CheckRequiredAnchorageLength()
        {
            double fbd = diameter == 6.0 ? 2 * section.Beam.Concrete.fctd : section.Beam.Concrete.fctd;
            double lb = (diameter * section.Beam.Steel.fyd) / (4 * fbd);

            double lb_min, lb_net;

            if (this.isInCompression)
            {
                lb_min = Math.Max(0.6 * lb, Math.Max(10 * diameter, eUtility.Convert(200, eLengthUnits.mm, eUtility.SLU)));
                lb_net = Math.Max(lb * section.GetAs_calc(false) / section.GetAs_used(false), lb_min);
            }
            else
            {
                lb_min = Math.Max(0.3 * lb, Math.Max(10 * diameter, eUtility.Convert(200, eLengthUnits.mm, eUtility.SLU)));
                lb_net = Math.Max(lb * section.GetAs_calc() / section.GetAs_used(), lb_min);
            }

            double length_left = section.Location - section.Intervals[0][0];
            double length_right = section.Intervals[0][1] - section.Location;
            double leftLimit = (-section.Beam.Beam_Analysis.Members[0].NEJoint.SupportWidth / 2.0) + section.Beam.Cover + diameter / 2.0;

            if (length_left < lb_net)
            {
                this.length = lb_net;
                if (length_left < leftLimit)
                    this.isBent = true;
            }

        }

        public override string ToString()
        {
            return start.ToString() + " to " + end.ToString() + "Level:" + this.level.ToString() + " Sublevel:" + this.subLevel.ToString();
        }
        #endregion
    }
}
