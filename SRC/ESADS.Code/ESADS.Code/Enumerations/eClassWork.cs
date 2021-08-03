using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code
{
    /// <summary>
    /// Concrete works are classification as either Class I or II depending on the quality of workmanship and
    /// the competence of the supervisors directing the works.[Sec.1.2.1 of EBCS-2-1995]
    /// </summary>
    public enum eClassWork
    {
        /// <summary>
        /// Works carried out under the direction of appropriately qualified supervisors ensuring the 
        /// attainment of level of quality control envisaged in Chapter 9 of EBCS-2-1995.[Sec. 1.2.1 of EBCS-2-1995]
        /// </summary>
        ClassI,
        /// <summary>
        /// Works with a lower level of quality control which are permissible only for single story buildings.
        /// </summary>
        ClassII,
    }
}
