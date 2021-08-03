using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code.EBCS_1995
{
    /// <summary>
    /// Contains the provisions given in Chapter 4-Ultimate Limit States of EBCS-2-1995 concerning punching.
    /// </summary>
    public static class ePunching
    {

        /// <summary>
        /// Returns the Ultimate shear resistance capacity of the concrete.
        /// </summary>
        /// <param name="u">Perimeter of punching shear.</param>
        /// <param name="fctd">Design tensile stregth.</param>
        /// <param name="d">Avarage effective depth.</param>
        /// <param name="px"> Geometric reinforcment ratio in x-direction.</param>
        /// <param name="py"> Geometric reinforcment ratio in y-direction.</param>
        /// <returns></returns>
        public static double GetVrd(double u, double fctd, double d, double px, double py)
        {
            double pe = Math.Sqrt(px + py) <= 0.015 ? Math.Sqrt(px + py) : 0.015;
            double k1 = (1 + 50 * pe) <= 2 ? (1 + 50 * pe) : 2;
            double k2 = 1.6 - d / 1000 >= 1 ? 1.6 - d / 1000 : 1;
            return 0.25 * fctd * k1 * k2 * u * d;
        }
    }
}
