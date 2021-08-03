using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code.EBCS_1995
{
    /// <summary>
    /// Contains the provisions given in Chapter 4-Ultimate Limit States of EBCS-2-1995 concerning flexure.
    /// </summary>
    public static class eFlexure
    {
        /// <summary>
        /// A_General coefficient to estimate the actual parabolic stress distribution with rectangular one.
        /// </summary>
        public const double StressApprxFactor = 0.8;

        /// <summary>
        /// Gets the maximum x/d value[Sec. 3.7.9 of EBCS 2, 1995]
        /// </summary>
        /// <param name="PercentageRedistribution">The moment after redistribution. For example 0.9 for 10% redistribution</param>
        public static double Get_k_x_max(double PercentageRedistribution)
        {
            return (PercentageRedistribution - 0.44) / 1.25;
        }

        /// <summary>
        /// Gets the maximum x/d value taking the 0% redistribution. [Sec. 3.7.9 of EBCS 2, 1995]
        /// </summary>
        public static double Get_k_x_max()
        {
            return Get_k_x_max(1);
        }
    }
}
