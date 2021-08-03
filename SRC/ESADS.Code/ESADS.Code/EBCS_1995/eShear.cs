using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code.EBCS_1995
{
    /// <summary>
    /// Contains the provisions given in Chapter 4-Ultimate Limit States of EBCS-2-1995 concerning shear.
    /// </summary>
    public static class eShear
    {
        /// <summary>
        /// Returns the ultimate shear capacity of the section to prevent diagonal compresion failure.Refer EBCS-2-1995,section 4.5.2(1).
        /// </summary>
        /// <param name="fcd">The design compresive strength.</param>
        /// <param name="width">The width of the section.</param>
        /// <param name="d">Effective depth of the section.</param>
        public static double GetVRd(double fcd,double width,double d)
        {
            return 0.25 * fcd * width * d;
        }

        /// <summary>
        /// Returns the shear force caried by the concrete for section with out significan axial force.Refer EBCS-2-1995,section 4.5.3.1(1).
        /// </summary>
        /// <param name="fctd">The design tensile strength of the concrete.</param>
        /// <param name="width">Width of the section.</param>
        /// <param name="d">Effective depth of the section.</param>
        /// <param name="As">Area of tensile reinforcements.</param>
        /// <param name="curtailed">Value indicating whether the 50% tensile reinforcement is curtailed or not. True if curtailed and false otherwise.</param>
        /// <returns></returns>
        public static double GetVc(double fctd, double width, double d,double As,bool curtailed = false)
        {
            double k1, k2, p;
            p = As / (width * d); //Reinforcement ration.
            k1 = (1 + 50 * p) < 2 ? 1 + 50 * p : 2;
            k2 = (1.6 - d / 1000) < 1 || curtailed ? 1 : 1.6 - d / 1000;
            return 0.25 * fctd * k1 * k2 * width * d;
        }

        /// <summary>
        /// Returns the spacing of vertical shear reinforcements for the given appleid shear.Refer EBCS-2-1995,section 4.5.3.1(1).
        /// </summary>
        /// <param name="fyd">Design yeild strength of the shear reinforcement.</param>
        /// <param name="Av">Area of  shear reinforcement within a spacing. </param>
        /// <param name="d">Effective depth of the section.</param>
        /// <param name="V">Shear force to be caried by the shear reinforcement.</param>
        /// <returns></returns>
        public static double GetSpacing(double fyd,double Av,double d,double V)
        {
            return Av * fyd * d / V;
        }
    }
}
