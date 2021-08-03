using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Code;
using ESADS.Code.EBCS_1995;

namespace ESADS.Mechanics.Design.Beam
{
    /// <summary>
    /// Represents a beam cross section that is to be design for shear.
    /// </summary>
    public class eDShearSection : eDSection
    {
        #region Fields
        /// <summary>
        /// Holds the value of 'NumberOfBars'.
        /// </summary>
        private int numberOfBars;
        /// <summary>
        /// Hold a value for property 'Bar'.
        /// </summary>
        private List<eShearBar> shearBars;
        /// <summary>
        /// Holds the value of the 'Shear'
        /// </summary>
        private double shear;
        /// <summary>
        /// Holds the value of 'FlexureSection'
        /// </summary>
        private eDFlexureSection flexureSection;
        /// <summary>
        /// Holds the value of 'AreaOfSteel'.
        /// </summary>
        private double areaOfSteel;
        /// <summary>
        /// Holds the value of 'BarSpacing'.
        /// </summary>
        private double barSpacing;
        /// <summary>
        /// Holds the value of 'FailedInDiagonalCompression'.
        /// </summary>
        private bool failedInDiagComp;
        /// <summary>
        /// Holds the value of 'BarConjested'.
        /// </summary>
        private bool barConjested;
        /// <summary>
        /// The concrete shear capacity of the section.
        /// </summary>
        private double V_c;
        /// <summary>
        /// Holds the value of 'Preceision'.
        /// </summary>
        private double preceision;
        /// <summary>
        /// Holds the value of 'TransverseSpacingExceeded'.
        /// </summary>
        private bool trensverseSpacingExceeded;
        private string failureNote;
        #endregion

        #region Properties

        /// <summary>
        /// Gets shear bar used in this shear design section.
        /// </summary>
        public List<eShearBar> ShearBars
        {
            get { return shearBars; }
        }

        /// <summary>
        /// Gets the failure note associated with the failure if the section has failed.
        /// </summary>
        public string FailureNote
        {
            get
            {
                return this.failureNote;
            }
        }

        /// <summary>
        /// Gets the longitudinal spacing of shear reinforcements.
        /// </summary>
        public double BarSpacing
        {
            get { return barSpacing; }
        }

        /// <summary>
        /// Gets the shear of the section
        /// </summary>
        private double V
        {
            get
            {
                return this.shear;
            }
        }

        /// <summary>
        /// Gets or sets the Shear force to design the shear section.
        /// </summary>
        public double Shear
        {
            get
            {
                return this.shear;
            }
            set
            {
                this.shear = value;
            }
        }

        /// <summary>
        /// Gets or sets the flexure section at the shear design section
        /// </summary>
        public eDFlexureSection FlexureSection
        {
            get
            {
                return flexureSection;
            }
            set
            {
                flexureSection = value;
                this.V_c = GetConcShearCapacity(value, value.Beam.Concrete.fctd);
            }
        }

        /// <summary>
        /// Gets the area of shear reinforcement in mm^2 per mm.
        /// </summary>
        public double AreaOfSteel
        {
            get
            {
                return areaOfSteel;
            }
        }

        /// <summary>
        /// Gets the total number of shearBar distributed over all intervals.
        /// </summary>
        public int NumberOfBars
        {
            get
            {
                return numberOfBars;
            }
        }

        /// <summary>
        /// Gets the value if the shear section has failedInDiagComp in diagonal compression of concrete.
        /// </summary>
        public bool FailedInDiagonalCompression
        {
            get
            {
                return failedInDiagComp;
            }
        }

        /// <summary>
        /// Gets the value if the shear bars are conjested (spacing below the minimum required).
        /// </summary>
        public bool BarConjested
        {
            get
            {
                return barConjested;
            }
        }

        /// <summary>
        /// Gets or sets the value of the precesion of stirrup bar spacing.
        /// </summary>
        public double Preceision
        {
            get
            {
                return preceision;
            }
            set
            {
                preceision = value;
            }
        }

        /// <summary>
        /// Gets the value if the maximum spacing of shear reinforcement legs permitted by the code has been exceeded.
        /// </summary>
        public bool TransverseSpacingExceeded
        {
            get
            {
                return this.trensverseSpacingExceeded;
            }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Design.eDShearSection class for a given basic parameters.
        /// </summary>
        /// <param name="beam">The beam bearing the section</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="width">Width of the section.</param>
        /// <param name="depth">Depth of the section.</param>
        public eDShearSection(eDBeam beam, string sectionName, double width, double depth)
            : base(beam, sectionName, width, depth)
        {
            this.barConjested = false;
            this.designCompleted = false;
            this.failedInDiagComp = false;
            this.trensverseSpacingExceeded = false;

            this.preceision = eUtility.Convert(10, eLengthUnits.mm, eUtility.SLU);
        }

        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Design.eDShearSection class for a given basic parameters.
        /// </summary>
        /// <param name="beam">The beam bearing the section</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="width">Width of the section.</param>
        /// <param name="depth">Depth of the section.</param>
        /// <param name="shear">The shear force used to design this cross section.</param>
        public eDShearSection(eDBeam beam, string sectionName, double width, double depth, double shear)
            : base(beam, sectionName, width, depth)
        {
            this.barConjested = false;
            this.designCompleted = false;
            this.failedInDiagComp = false;
            this.trensverseSpacingExceeded = false;

            this.shear = shear;
            this.preceision = eUtility.Convert(10, eLengthUnits.mm, eUtility.SLU);
        }

        #endregion

        #region Methods
        /// <summary>
        /// Designs the section for shear on which it is called.
        /// </summary>
        public override void Design()
        {
            CheckDiagonalCompression();

            if (failedInDiagComp)
                return;

            FillBarSpacing(Math.Abs(this.shear) - V_c);

            CheckSpacing();
        }

        public void FillBar()
        {
            shearBars = new List<eShearBar>();

            this.shearBars.Add(new eShearBar(eShearBarTypes.EnclosingStirrup, this));

            char name = 'a';

            for (int i = 1; i < flexureSection.CompresionComb.Rows.GetLength(0); i++)
            {
                if (flexureSection.CompresionComb.Rows[i].NumOfBar1 + flexureSection.CompresionComb.Rows[i].NumOfBar2 > 2)
                    this.shearBars.Add(new eShearBar(eShearBarTypes.InnerStirrup, this, !flexureSection.IsNegative, "st-" + this.name + (name++).ToString()));
            }

            for (int i = 1; i < flexureSection.TensileComb.Rows.GetLength(0); i++)
            {
                if (flexureSection.TensileComb.Rows[i].NumOfBar1 + flexureSection.TensileComb.Rows[i].NumOfBar2 > 2)
                    this.shearBars.Add(new eShearBar(eShearBarTypes.InnerStirrup, this, flexureSection.IsNegative, "st-" + this.name + (name++).ToString()));
            }
        }

        /// <summary>
        /// Fills shear reinforcement bar spacing for the shear force to be carried by the reinforcement.
        /// </summary>
        /// <param name="V_s">The portion of the shear force to be carried by the steel.</param>
        private void FillBarSpacing(double V_s)
        {
            V_s = V_s < 0 ? 0.0 : V_s;

            double A_v = 2 * eXBar.GetArea(eXBar.GetDiam(beam.StirupBar));

            this.barSpacing = (int)(((A_v * beam.Steel.fyd * flexureSection.EffectiveDepth) / V_s) / preceision) * preceision;

            this.barSpacing = Math.Abs(this.barSpacing);
        }

        /// <summary>
        /// Gets the shear capacity of the concrete without reinforcement.
        /// </summary>
        /// <param name="flexSection">The flexure section designed at which the capacity is to be computed.</param>
        /// <param name="fctd">The design tensile capacity of the concrete.</param>
        internal static double GetConcShearCapacity(eDFlexureSection flexSection, double fctd)
        {
            double k1 = Math.Min(2.0, 1 + 50 * flexSection.GetSteelRatio());
            double k2 = Math.Max(1.0, 1.6 - eUtility.Convert(flexSection.EffectiveDepth, eUtility.SLU, eLengthUnits.m));

            return 0.25 * fctd * k1 * k2 * flexSection.Width * flexSection.EffectiveDepth;
        }

        /// <summary>
        /// Checks if the section's diagonal compression capacity of the concrete.
        /// </summary>
        public void CheckDiagonalCompression()
        {
            if (eShear.GetVRd(beam.Concrete.fcd, Width, flexureSection.EffectiveDepth) < Math.Abs(shear))
            {
                this.failureNote = "Failed in Diagonal Compression";
                this.failedInDiagComp = true;
            }
            else
                this.failedInDiagComp = false;
        }

        /// <summary>
        /// Checks if the spacing of the reinforcement is within the limits.
        /// </summary>
        public void CheckSpacing()
        {
            if (eDetailing.GetMinSpacing(eXBar.GetDiam(beam.StirupBar), beam.MaxAggSize) > this.barSpacing - eXBar.GetDiam(beam.StirupBar) * 2)
            {
                this.failureNote = "Reinforcement Conjested!";
                this.barConjested = true;
                return;
            }

            if (Math.Abs(this.shear) <= this.GetSectionCapacity() * 2 / 3)
                this.barSpacing = Math.Min(this.barSpacing, (int)(Math.Min(0.5 * flexureSection.EffectiveDepth, eUtility.Convert(300, eLengthUnits.mm, eUtility.SLU)) / preceision) * preceision);
            else
                this.barSpacing = Math.Min(this.barSpacing, (int)(Math.Min(0.3 * flexureSection.EffectiveDepth, eUtility.Convert(200, eLengthUnits.mm, eUtility.SLU)) / preceision) * preceision);

            double rho = 0.4 / eUtility.Convert(beam.Steel.fyk, eUtility.SLU, eUtility.SFU, eLengthUnits.mm, eForceUints.N);

            this.barSpacing = Math.Min(this.barSpacing, (int)((2.0 * eXBar.GetArea(eXBar.GetDiam(beam.StirupBar)) / (rho * this.Width)) / preceision) * preceision);
        }

        /// <summary>
        /// Gets the section's ultimate shear capacity.
        /// </summary>
        public double GetSectionCapacity()
        {
            double A_v = eXBar.GetArea(eXBar.GetDiam(beam.StirupBar)) * 2.0;
            double V_s = (A_v * beam.Steel.fyd * flexureSection.EffectiveDepth) / this.barSpacing;
            double V_rd = eShear.GetVRd(beam.Concrete.fcd, Width, flexureSection.EffectiveDepth);

            return Math.Min(V_rd, V_c + V_s);
        }

        internal override bool IsSimilar(eDSection section)
        {
            eDShearSection sec = section as eDShearSection;

            if (sec == null)
                return false;
            if (this.b != sec.b)
                return false;
            if (this.d != sec.d)
                return false;
            if (this.barConjested || sec.barConjested)
                return false;
            if (this.failedInDiagComp || sec.failedInDiagComp)
                return false;
            if (this.trensverseSpacingExceeded || this.trensverseSpacingExceeded)
                return false;
            if (this.barSpacing != sec.barSpacing)
                return false;

            return true;
        }
        #endregion
    }
}
