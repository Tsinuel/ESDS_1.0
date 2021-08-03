using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Code;
using ESADS.Code.EBCS_1995;

namespace ESADS.Mechanics.Design
{
    /// <summary>
    /// Represents a beam cross section that is to be design for flexure.
    /// </summary>
    public class eDFlexurSection:eDSection
    {
        #region Feilds
        /// <summary>
        /// Specifies the tolerance to which depth revision is significantly not important.
        /// </summary>
        private const double depthTolerance = 5;
        /// <summary>
        /// Accesses the public property 'BarConstraints'.
        /// </summary>
        private double[] barConstraints;
        /// <summary>
        /// Accesses the public property 'CompCombination'.
        /// </summary>
        private eCombination compCombination;
        /// <summary>
        /// Accesses the public property 'EffectiveSLSdepth'.
        /// </summary>
        private double effectiveSLSdepth;
        /// <summary>
        /// Accesses the public property 'EffectiveSpanLength'.
        /// </summary>
        private double effectiveSpanLength;
        /// <summary>
        /// Accesses the public property 'MaxAggrSize'.
        /// </summary>
        private double maxAggrSize;
        /// <summary>
        /// Accesses the public property 'SpanType'.
        /// </summary>
        private eSpanType spanType;
        /// <summary>
        /// Accesses the public property 'StirrupPosition'.
        /// </summary>
        private eRelativeStirrupPosition stirrupPosition;
        /// <summary>
        /// Accesses the public property 'TensCombination'.
        /// </summary>
        private eCombination tensCombination;
        /// <summary>
        /// Hold a value for property 'ConcreteCover'.
        /// </summary>
        private double concreteCover;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the concrete cover used for the reinforcements.
        /// </summary>
        public double ConcreteCover
        {
            get { return concreteCover; }
            set { concreteCover = value; }
        }

        /// <summary>
        /// Gets compresion bar combination.
        /// </summary>
        public eCombination CompresionComb
        {
            get { return compCombination; }
        }

        /// <summary>
        /// Gets tensile bar combination.
        /// </summary>
        public eCombination TensileComb
        {
            get { return tensCombination; }
        }

        /// <summary>
        /// Gets the relative position of stirrups.
        /// </summary>
        public eRelativeStirrupPosition StirrupPosition
        {
            get { return stirrupPosition; }
            set { stirrupPosition = value; }
        }

        /// <summary>
        /// Gets the effective servisability limit state depth.
        /// </summary>
        public double EffSLS_Depth
        {
            get { return effectiveSLSdepth; }
        }

        /// <summary>
        /// Gets or sets the effective span length.
        /// </summary>
        public double EffSpanLength
        {
            get { return effectiveSpanLength; }
            set { effectiveSpanLength = value; }
        }

        /// <summary>
        /// Gets or sets the maximum agregate size.
        /// </summary>
        public double MaxAggSize
        {
            get { return maxAggrSize; }
            set { maxAggrSize = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Design.eDFlexurSection class for a given basic parameters.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="width">Width of the section.</param>
        /// <param name="depth">Depth of the section.</param>
        public eDFlexurSection(string sectionName, double width, double depth)
            : base(sectionName, width, depth)
        {
        }

        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Design.eDFlexurSection class for a given basic parameters.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="width">Width of the section.</param>
        /// <param name="depth">Depth of the section.</param>
        /// <param name="moment">The moment used to design this cross section.</param>
        public eDFlexurSection(string sectionName, double width, double depth,double moment)
            : base(sectionName, width, depth,moment)
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// Sets properties related bar preferences of the beam.
        /// </summary>
        /// <param name="BarConstraints">Main bar constraints.</param>
        /// <param name="StirrupPosition">Relative location of the stirrup.</param>
        /// <param name="StirrupDiam">Diameter of the main stirrup.</param>
        public void SetBarPreferences(double[] BarConstraints, eRelativeStirrupPosition StirrupPosition, double StirrupDiam)
        {
            this.barConstraints = BarConstraints;
            this.stirrupPosition = StirrupPosition;
            this.stirrupD = StirrupDiam;
        }

        /// <summary>
        /// Sets the properties related to design condition.
        /// </summary>
        /// <param name="classWork">Class work of the design.</param>
        /// <param name="exposureType">Exposure type of the beam.</param>
        /// <param name="spanType">Span type of the beam.</param>
        /// <param name="effectiveSpanLength">Effective length of the span at which the beam section is taken.</param>
        /// <param name="maxAggrSize">Maximum aggregate size used in the design of the beam.</param>
        /// <param name="concreteCover">Concrete cover used in the design of the beam.</param>
        public void SetDesignConditions(eClassWork classWork, eSpanType spanType, double effectiveSpanLength, double maxAggrSize, double concreteCover)
        {
            this.classwork = classWork;
            this.spanType = spanType;
            this.effectiveSpanLength = effectiveSpanLength;
            this.maxAggrSize = maxAggrSize;
            this.concreteCover = concreteCover;
        }

        /// <summary>
        /// Returns the depth of the nutral axis from moment for section to be designed.
        /// </summary>
        /// <param name="Moment">Magnitude of moment used for singly reinforced part.</param>
        /// <returns></returns>
        private double GetNutralAxisDepth(double Moment)
        {
            // implementation of the beam nutral axis formula x = pmd.
            return GetSteelRation(Moment) * s.fyd / (0.8 * c.fcd) * d;
        }

        /// <summary>
        /// Gets the location of the nutral axis for already designed cross section.
        /// </summary>
        /// <returns></returns>
        private double GetNutralAxisDepth()
        {
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
        }

        /// <summary>
        /// Checks if the given area satisfies the minimum requirments.
        /// </summary>
        /// <param name="As">Area of steel to be checked for minimum requirments.</param>
        private void CheckMinAreaOfSteel(ref double As)
        {
            if (As < eDetailing.Get_ρ_min(eStructureType.Beam, s.Grade) * b * d)
                As = eDetailing.Get_ρ_min(eStructureType.Beam, s.Grade) * b * d;
        }

        /// <summary>
        /// Fills bar combinations for Compresive and Tensile area of s.
        /// </summary>
        /// <param name="As">Tensile area of steel</param>
        /// <param name="AsPrim">Compressive area of steel.</param>
        /// <param name="X">Depth of nutrual axis.</param>
        private void FillCombinations(double As, double AsPrim, double X)
        {
            tensCombination = eBarManager.GetCombinations(As, barConstraints, GetMaxAreaOfSteel(), b, D, X, eDetailing.GetMinSpacing(barConstraints[0], maxAggrSize), concreteCover, stirrupD, stirrupPosition, new eDesignFailurLink(OnDesignFailur));
            compCombination = eBarManager.GetCombinations(AsPrim, barConstraints, double.PositiveInfinity, b, D, D - X, eDetailing.GetMinSpacing(barConstraints[0], maxAggrSize), concreteCover, stirrupD, stirrupPosition, new eDesignFailurLink(OnDesignFailur));
        }

        /// <summary>
        /// Returns the maximum area of steel.
        /// </summary>
        /// <returns></returns>
        private double GetMaxAreaOfSteel()
        {
            // implementation of the formula AsMax = pmax*b*d.
            return (eFlexure.Get_k_x_max() * 0.8 * c.fcd / s.fyd) * (b * d);
        }

        /// <summary>
        /// Designs the beam on which it is called.
        /// </summary>
        public override void  Design()
        {
            double M1, M2, As1, As2, AsPrim, X, dcPrim;
            effectiveSLSdepth = eServiceability.GetMinEffDepth(s.Grade, effectiveSpanLength, spanType, eStructureType.Beam);
            dcPrim = barConstraints[barConstraints.Length - 1] / 2 + stirrupD + concreteCover;
            d = D - dcPrim;
            do
            {
                M1 = GetMomentCapacity(GetReductionFactor()) > F ? F : GetMomentCapacity(GetReductionFactor());
                M2 = F - M1;
                As1 = GetAreaOfSteel(M1);
                As2 = GetAreaOfSteel(M2, dcPrim);
                X = GetNutralAxisDepth(M1);
                AsPrim = GetAreaOfSteel(M2, dcPrim, X);
                CheckMinAreaOfSteel(ref AsPrim);
                FillCombinations(As1 + As2, AsPrim, X);
                if (d < effectiveSLSdepth)
                {
                    OnDesignFailur(new eDesignFailedEventArgs(D, effectiveSLSdepth + (D - d), eFailureTypes.SLS));
                    return;
                }
                if (tensCombination.Area == 0 || compCombination.Area == 0)
                    return;
                designCompleted = CheckDepthTolerance(d, tensCombination.EffectiveDepth) && CheckDepthTolerance(D - compCombination.EffectiveDepth, dcPrim);
                dcPrim = D - compCombination.EffectiveDepth;
                d = tensCombination.EffectiveDepth;
            }
            while (!designCompleted);
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
        /// Returns the maxismum possible steel ratio to make a beam section dectile.
        /// </summary>
        /// <returns></returns>
        private double GetMaxSteelRatio()
        {
            return eFlexure.Get_k_x_max() * 0.8 * c.fcd / s.fyd;
        }

        /// <summary>
        /// Returns the ultimate limit state depth required.
        /// </summary>
        /// <param name="ReductionFactor">The reduction factor by which steel ratio is going to be reduced.</param>
        /// <returns></returns>
        private double GetULSdepth(double ReductionFactor)
        {
            //returns the the depth required for ultimate limit state for a given reduction factor.
            // Uses the Ultimated Limit stated depth formula.
            return Math.Pow(F / (0.8 * ReductionFactor * eFlexure.Get_k_x_max() * b * c.fcd * (1 - 0.4 * ReductionFactor * eFlexure.Get_k_x_max())), 0.5);
        }

        /// <summary>
        /// Returns the steel ration for a given moment capacity and effective depth.
        /// </summary>
        /// <param name="MomentCapacity">The moment capacity of the section if it is singly reinforced.</param>
        /// <returns></returns>
        private double GetSteelRation(double MomentCapacity)
        {

            double minSteelRatio = Math.Round(eDetailing.Get_ρ_min(eStructureType.Beam, s.Grade), 9);
            double maxSteelRatio = Math.Round(GetMaxSteelRatio(), 9);
            double steelRatio1 = Math.Round((1 - Math.Pow((1 - 2 * MomentCapacity / (c.fcd * b * Math.Pow(d, 2))), 0.5)) / (s.fyd / c.fcd), 9);
            double steelRatio2 = Math.Round((1 + Math.Pow((1 - 2 * MomentCapacity / (c.fcd * b * Math.Pow(d, 2))), 0.5)) / (s.fyd / c.fcd), 9);

            if ((steelRatio1 < minSteelRatio) && (steelRatio2 < maxSteelRatio))
            {
                return steelRatio2;
            }
            if ((steelRatio1 > minSteelRatio) && (steelRatio1 <= maxSteelRatio))
            {
                return steelRatio1;
            }
            if ((steelRatio1 < minSteelRatio) && (steelRatio2 > maxSteelRatio))
            {
                throw new eImposibleSteelRatioException("The actual steel ratios are one bellow the absolute minimum and the other above the absolute maximum steel ratio.", steelRatio1, steelRatio2, maxSteelRatio);
            }
            if (steelRatio1 > maxSteelRatio)
            {
                throw new eImposibleSteelRatioException("The actual steel ratios are above absolute maximum steel ratio.", steelRatio1, steelRatio2, maxSteelRatio);
            }
            else
            {
                return minSteelRatio;
            }

        }

        /// <summary>
        /// Returns the singly reinforced moment capacity.
        /// </summary>
        /// <param name="ReductionFactor">Reducton factor to reduce steel ratio.</param>
        private double GetMomentCapacity(double ReductionFactor)
        {
            double kx = eFlexure.Get_k_x_max() * ReductionFactor;
            //Returns the Moment capacity applying the given reduction factor.
            return 0.8 * kx * b * Math.Pow(d, 2) * c.fcd * (1 - 0.4 * kx);
        }

        /// <summary>
        /// Returns the area of steel required for a given moment capacity.
        /// </summary>
        /// <param name="Moment">The moment for which the steel is going to be calcualated.</param>
        /// <returns></returns>
        private double GetAreaOfSteel(double Moment)
        {
            return GetSteelRation(Moment) * b * d;
        }

        /// <summary>
        /// Returns the secondary tensile area of steel that makes couple with top compressive steel.
        /// </summary>
        /// <param name="SecondaryMoment">Secondary moment caried by the coupled tensile and compresive areas of steel.</param>
        /// <param name="dcPrim">The distance between top compresion fiber and the centroid of compresive area of steel.</param>
        /// <returns></returns>
        private double GetAreaOfSteel(double SecondaryMoment, double dcPrim)
        {
            //Returns the area of steel required for tesile region which will couple with the top compression steel.
            //It is stressed to its yeild strain.
            return SecondaryMoment / (s.fyd * (d - dcPrim));
        }

        /// <summary>
        /// Returns the area of compressive steel required to resist secondary moment coupling with bottom tensile reinforcement.
        /// </summary>
        /// <param name="SecondaryMoment">Secondary moment caried by the coupled tensile and compresive areas of steel.</param>
        /// <param name="dcPrim">The distance between top compresion fiber and the centroid of compresive area of steel.</param>
        /// <param name="X">Depth of nutral axis.</param>
        /// <returns></returns>
        private double GetAreaOfSteel(double SecondaryMoment, double dcPrim, double X)
        {
            //returns the area of steel required in compression region which will couple with the bottom compression steel.
            //It calculates the area assuming the stress level in the bars.
            return SecondaryMoment / (GetCompSteelStress(dcPrim, X) * (d - dcPrim));
        }

        /// <summary>
        /// Returns the stress in compresion area of steel.
        /// </summary>
        private double GetCompSteelStress(double dcPrim, double NutralAxisDepth)
        {
            //calculates the stress in the top compresion steel.
            double stress = s.E * eBasisOfDesign.ε_c_max_bending * (NutralAxisDepth - dcPrim) / NutralAxisDepth;

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
        /// Returns the string containing all bars useded in the design and their diameters.
        /// </summary>
        /// <returns></returns>
        private string GetConstraintsBarArray()
        {
            string selectedBars = "";
            for (int i = 0; i < barConstraints.Length; i++)
            {
                selectedBars += "φ" + barConstraints[i].ToString() + "," + " ";
            }
            selectedBars.Remove(selectedBars.Length - 1);
            return selectedBars;
        }

        /// <summary>
        /// Returns the ultimate moment capacity of this beam section and fill the location of the nutral axis in out parameter.
        /// </summary>
        /// <param name="X">Parameter ot be filled by the locaion of the nuntral axis.</param>
        /// <returns></returns>
        public override double Analyse()
        {
            double As1, As2, AsPrim, fsPrim,X;

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
                    fsPrim = (X - (D - compCombination.EffectiveDepth)) * eBasisOfDesign.ε_c_max_bending * s.E / X;
                }
                return 0.8 * b * X * c.fcd * (d - 0.4 * X) + fsPrim * AsPrim * (d - (D - compCombination.EffectiveDepth));
            }
            else
                return 0.8 * b * X * c.fcd * (d - 0.4 * X) + s.fyd * AsPrim * (d - (D - compCombination.EffectiveDepth));
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
                double X , kx, n, p1, p2, dc;
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
        /// Returns the reduction factor for reinforcement to limit the area of steel not to be above maximum allowable area.
        /// </summary>
        /// <returns></returns>
        public double GetReductionFactor()
        {
            return (GetMaxAreaOfSteel() - barConstraints[0]) / GetMaxAreaOfSteel();
        }

        #endregion
    }
}
