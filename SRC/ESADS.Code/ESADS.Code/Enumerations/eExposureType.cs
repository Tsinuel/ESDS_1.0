using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code
{
    /// <summary>
    /// Enumerates the varies Enviromental exposure conditions for Deciding the minimum reinforcement Cover
    /// </summary>
    public enum eExposureType
    {
        /// <summary>
        /// Represents Exposures in Dry Enviroment: Interior of a building Normal Habituation office 
        /// [EBCS _1995  7.1.3 Concrete Cover to Reinforcement,Table 7.2 Minimum Cover Requirements for Concrete Members]
        /// </summary>
        Mild = 15,
        /// <summary>
        /// Represents Exposures in Humid environment:Interior components (e.g. laundries); exterior components;components 
        /// in non-aggressive soil and/or in water[EBCS1995 Table 7.2 Minimum Cover Requirements for Concrete Members]
        /// </summary>
        Moderate = 25,
        /// <summary>
        /// Represents Exposures Sever Enviromental Condition[EBCS1995 Table 7.2 Minimum Cover Requirements for Concrete Members]]
        /// </summary>
        Severe = 50,
    }
}
