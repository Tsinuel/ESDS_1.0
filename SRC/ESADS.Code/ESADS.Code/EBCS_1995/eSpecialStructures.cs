using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code.EBCS_1995
{
   /// <summary>
    /// Contains provision related to special structures which is specified in EBCS-2 chapter 6.
    /// </summary>
    public static class eSpecialStructures
    {
        #region Footing
        /// <summary>
        /// Returns the minimum depth of footing for different footing types.
        /// </summary>
        /// <param name="footingType">The type of footing: whether it is constructed on soil or piles.</param>
        /// <returns></returns>
        public static double GetMinFootingDepth(eFootingType footingType)
        {
            if (footingType == eFootingType.FootingOnSoil)
            {
                return 150;
            }
            else
            {
                return 300;
            }
        }

        #endregion
    }
}
