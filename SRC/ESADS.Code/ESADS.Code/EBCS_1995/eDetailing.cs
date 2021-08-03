using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code.EBCS_1995
{
    /// <summary>
    /// Contains the provisions given in Chapter 7 - Detailing Provision of EBCS-2-1995
    /// </summary>
    public static class eDetailing
    {
        /// <summary>
        /// Returns array of bar diameters between the specified bar diameters
        /// </summary>
        /// <param name="diam1">The smallest bar diameter.</param>
        /// <param name="diam2">The largest bar diameter.</param>
        /// <returns></returns>
        public static double[] GetBarsBetwee(double diam1, double diam2)
        {
            List<double> allBars = new List<double>() { 6, 8, 10, 12, 14, 16, 20, 24, 28, 32 };
            List<double> subBars = new List<double>();
            for (int i = allBars.IndexOf(diam1); i <= allBars.IndexOf(diam2); i++)
            {
                subBars.Add(allBars[i]);
            }
            return (double[])subBars.ToArray();
        }
        /// <summary>
        /// Gets the minimum geometrical ratio of reinforcement for flexure.
        /// </summary>
        /// <param name="TypeOfStructure">The type of structure, defined in eStructureType, under consideration</param>
        /// <param name="SteelGrade">The steel grade, defined in eSteelGrade, used in the specified structure at the particular position whose minimum reinforcement ratio is to be computed.</param>
        public static double Get_ρ_min(eStructureType TypeOfStructure, eSteelGrade SteelGrade)
        {
            double f_yk = eMaterial.Get_f_yk(SteelGrade);
            switch (TypeOfStructure)
            {
                case eStructureType.Beam:
                    {
                        return 0.6 / eMaterial.Get_f_yk(SteelGrade);
                    }
                case eStructureType.Column:
                    return 0.008;
                default:
                    return 0.5 / eMaterial.Get_f_yk(SteelGrade);
            }
        }

        /// <summary>
        /// Returns the maximum bar spacing in slabs for a given depth of slab.
        /// </summary>
        /// <param name="depth">Depth of slab.</param>
        /// <returns></returns>
        public static double GetMaxBarSpacingForSlab(double depth)
        {
            return Math.Min(2 * depth, 350);
        }
        /// <summary>
        /// Returns the maximum allowable area of steel for beam.
        /// </summary>
        /// <returns></returns>
        public static double Get_ρ_max()
        {
            return 0.04;
        }

        /// <summary>
        /// Gets the minimum spacing of longitudinal reinforcements in beam[Sec. 7.1.4.3 of EBCS-2, 1995]
        /// </summary>
        /// <param name="BiggestBarDia">The maximum size of bar used</param>
        /// <param name="MaxAggSize">The maximum aggregate size of the concrete used</param>
        public static double GetMinSpacing(double BiggestBarDia, double MaxAggSize)
        {
            return Math.Max(BiggestBarDia, Math.Max(20.0, MaxAggSize + 5));
        }

        /// <summary>
        /// Returns the minimum concrete cover used for a given exposure type as per [EBCS-1995,EBCS-2,Sec 7.1.3.]. 
        /// </summary>
        /// <param name="ExposureType">Type of exposure for the beam.</param>
        /// <returns></returns>
        public static double GetMinConcreteCover(eExposureType ExposureType)
        {
            switch (ExposureType)
            {
                case eExposureType.Mild:
                    return eUtility.Convert(15, eLengthUnits.mm, eUtility.SLU);
                case eExposureType.Moderate:
                    return eUtility.Convert(25, eLengthUnits.mm, eUtility.SLU);
                case eExposureType.Severe:
                    return eUtility.Convert(50, eLengthUnits.mm, eUtility.SLU);
                default :
                    return 25;
            }
        }

        /// <summary>
        /// Gets the design bond strength of a bond according to EBCS-2-Section 7.1.5.1
        /// </summary>
        /// <param name="f_ctd">The design tensile strength of the concrete used.</param>
        /// <param name="BarIsDeformed">Indicates if the bar used is deformed or not.</param>
        /// <param name="GoodBondCondition">Value indicating if the bond condition is good. Good bond conditions are defined in EBCS-2-1992, Sec. 7.1.5.1(2). Otherwise the value is false.</param>
        /// <returns>The design bond strength in the same unit as that of the design tensile strength of the concrete given.</returns>
        private static double GetDesignBondStrength(double f_ctd, bool BarIsDeformed, bool GoodBondCondition)
        {
            if (GoodBondCondition)
            {
                if (BarIsDeformed)
                {
                    return 2.0 * f_ctd;
                }
                else
                {
                    return f_ctd;
                }
            }
            else  //0.7 times that for good bond condition.
            {
                if (BarIsDeformed)
                {
                    return 1.4 * f_ctd;  
                }
                else
                {
                    return 0.7 * f_ctd;
                }
            }
        }

        /// <summary>
        /// Gets the embedment length required to develop the full  design strength of a straight reinforcing  bar..[EBCS-2, 1995 Sec.7.1.6.1]
        /// </summary>
        /// <param name="Diameter">The diameter of the reinforcement.</param>
        /// <param name="f_yd">The design strength of the reinforcement steel.</param>
        /// <param name="f_bd">The design bond strength in the same unit as f_yd.</param>
        /// <returns>The basic anchorage length in the same length unit as that of the given bar diameter.</returns>
        private static double GetBasicAnchorageLength(double Diameter, double f_yd, double f_bd)
        {
            return Diameter * f_yd / (4 * f_bd);
        }

        /// <summary>
        /// Gets the minimum anchorage lenght. [EBCS-2, 1995 Sec 7.1.6.2]
        /// </summary>
        /// <param name="Diameter">The diameter of the renforcement.</param>
        /// <param name="l_b">The basic anchorage length.</param>
        /// <param name="InTension">Indicates if the reinforcement is loaded in tension. If the value is false it is implied that the bar is loaded in compression.</param>
        private static double GetMinAnchorageLength(double Diameter, double l_b, bool InTension)
        {
            if (InTension)
            {
                return Math.Max(0.3 * l_b, Math.Max(10.0 * Diameter, eUtility.Convert(200, eLengthUnits.mm, eUtility.SLU)));
            }
            else
            {
                return Math.Max(0.6 * l_b, Math.Max(10.0 * Diameter, eUtility.Convert(200, eLengthUnits.mm, eUtility.SLU)));
            }
        }

        /// <summary>
        /// Gets the required anchorage length a reinforcement. [EBCS-2, 1995 Sec 7.1.6.2]
        /// </summary>
        /// <param name="A_s_calc">The theoretical area of reinforcement required by the design.</param>
        /// <param name="A_s_used">The actual area of steel used after the design.</param>
        /// <param name="l_b">The basic anchorage length.</param>
        /// <param name="l_b_min">The minimum anchorage length.</param>
        /// <param name="anchorType">Value indicating if the bar is straight used in compression or tension. If the value is false it is implied that the anchorage is in tension with standard hooks of EBCS-2, 1995, Fig.7.2.</param>
        public static double GetReqAnchorageLength(double A_s_calc, double A_s_used, double l_b, double l_b_min, eAnchorageType anchorType)
        {
            double a = anchorType == eAnchorageType.Straight ? 1.0 : 0.7;
            return Math.Max(a * l_b * A_s_calc / A_s_used, l_b_min);
        }

        /// <summary>
        /// Gets the required anchorage length a reinforcement from the basic inputs required. [EBCS-2, 1995 Sec 7.1.6.2]
        /// </summary>
        /// <param name="A_s_calc">The theoretical area of reinforcement required by the design.</param>
        /// <param name="A_s_used">The actual area of steel used after the design.</param>
        /// <param name="Diameter">The diameter of the reinforcement.</param>
        /// <param name="f_yd">The design strength of the reinforcement steel.</param>
        /// <param name="f_ctd">The design tensile strength of the concrete used.</param>
        /// <param name="BarIsDeformed">Indicates if the bar used is deformed or not.</param>
        /// <param name="GoodBondCondition">Value indicating if the bond condition is good. Good bond conditions are defined in EBCS-2-1992, Sec. 7.1.5.1(2). Otherwise the value is false.</param>
        /// <param name="InTension">Indicates if the reinforcement is loaded in tension. If the value is false it is implied that the bar is loaded in compression.</param>
        /// <param name="IsStraightBar">Value indicating if the bar is straight used in compression or tension. If the value is false it is implied that the anchorage is in tension with standard hooks of EBCS-2, 1995, Fig.7.2.</param>
        public static double GetReqAnchorageLength(double A_s_calc, double A_s_used, double Diameter, double f_yd, double f_ctd, bool BarIsDeformed = true, bool GoodBondCondition = true, bool InTension = true, eAnchorageType anchorType = eAnchorageType.Straight)
        {
            double f_bd = GetDesignBondStrength(f_ctd, BarIsDeformed, GoodBondCondition);
            double l_b = GetBasicAnchorageLength(Diameter, f_yd, f_bd);
            double l_b_min = GetMinAnchorageLength(Diameter, l_b, InTension);
            return GetReqAnchorageLength(A_s_calc, A_s_used, l_b, l_b_min, anchorType);
        }

        /// <summary>
        /// Gets the length of lap for reinforcement.
        /// </summary>
        public static double GetLapLength()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the minimum lap length allowed.
        /// </summary>
        /// <param name="a">Distance between two adjacent laps.</param>
        /// <param name="b">Distance to the nearest surface.</param>
        /// <param name="PercentReinf">Percentage of reinforcement lapped within the required lap length.</param>
        /// <param name="IsStraightBar">Value indicating if the bar is straight used in compression or tension. If the value is false it is implied that the anchorage is in tension with standard hooks of EBCS-2, 1995, Fig.7.2.</param>
        /// <param name="l_b">The basic anchorage length.</param>
        /// <param name="Diameter">The reinforcement diameter.</param>
        private static double GetMinLapLength(double a, double b, double PercentReinf, bool IsStraightBar, double l_b, double Diameter)
        {
            double aa = IsStraightBar ? 1.0 : 0.7;
            double a1 = Get_a1(a, b, PercentReinf, Diameter);

            return Math.Max(0.3 * aa * a1 * l_b, Math.Max(15.0 * Diameter, eUtility.Convert(200, eLengthUnits.mm, eUtility.SLU)));
        }

        /// <summary>
        /// Gets the a1 value for lab length calculation according to Table 7.3 of EBCS-2, 1995
        /// </summary>
        /// <param name="a">Distance between two adjacent laps.</param>
        /// <param name="b">Distance to the nearest surface.</param>
        /// <param name="PercentReinf">Percentage of reinforcement lapped within the required lap length. Value should be between 0 and 100 inclusive.</param>
        /// <param name="Diameter">The diameter of the reinforcement.</param>
        private static double Get_a1(double a, double b, double PercentReinf, double Diameter)
        {
            bool case1 = true;
            if (a > 10.0 * Diameter && b > 5.0 * Diameter)
                case1 = false;

            if (0 <= PercentReinf && PercentReinf < 20.0)
            {
                if (case1)
                    return eUtility.Interpolate(0, 0, 20, 1.2, PercentReinf);
                else
                    return eUtility.Interpolate(0, 0, 20, 1.0, PercentReinf);
            }
            else if (PercentReinf < 25.0)
            {
                if (case1)
                    return eUtility.Interpolate(20, 1.2, 25, 1.4, PercentReinf);
                else
                    return eUtility.Interpolate(20, 1.0, 25, 1.1, PercentReinf);
            }
            else if (PercentReinf < 33.0)
            {
                if (case1)
                    return eUtility.Interpolate(25, 1.4, 33, 1.6, PercentReinf);
                else
                    return eUtility.Interpolate(25, 1.1, 33, 1.2, PercentReinf);
            }
            else if (PercentReinf < 50.0)
            {
                if (case1)
                    return eUtility.Interpolate(33, 1.6, 50, 1.8, PercentReinf);
                else
                    return eUtility.Interpolate(33, 1.2, 50, 1.3, PercentReinf);
            }
            else if (PercentReinf <= 100.0)
            {
                if (case1)
                    return eUtility.Interpolate(50, 1.8, 100, 2.0, PercentReinf);
                else
                    return eUtility.Interpolate(50, 1.3, 100, 1.4, PercentReinf);
            }
            else
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Returns the maximum shear reinforcement spacing in the longtudinal direction of beam.
        /// </summary>
        /// <param name="d">Effective depth of the cross section.</param>
        /// <param name="Vsd">Design shear force at that cross section.</param>
        /// <param name="VRd">The diagonal compresive strength of the concrete.</param>
        /// <returns></returns>
        public static double GetMaxLongtudinalShearBarSpacing(double d, double Vsd, double VRd)
        {
            if (Vsd <= 2 * VRd / 3d)
                return 0.5 * d < 300 ? 0.5 * d : 300;
            else
                return 0.3 * d < 200 ? 0.3 * d : 200;
        }

        /// <summary>
        /// Returns the maximum shear reinforcement spacing in transversal direction of beam
        /// </summary>
        /// <param name="d">Effective depth of the cross section.</param>
        /// <returns></returns>
        public static double GetMaxTransversalShearBarSpacing(double d)
        {
            return Math.Min(d, 800);
        }

        /// <summary>
        /// Gets the minimum geometrical ratio of reinforcement for shear.
        /// </summary>
        ///<param name="fyk"> The characterstic yeild strength of the shear reinforcement.</param>
        public static double Get_ρ_min(double fyk)
        {
            return 0.4 / fyk;
        }

        /// <summary>
        /// Return the length of standard hook for reinforcemtns.
        /// </summary>
        /// <param name="diameter">diameter of the bar.</param>
        /// <returns></returns>
        public static double GetHookLength(double diameter)
        {
            return 5 * diameter;
        }
    }
}
