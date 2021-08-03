using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Design.Beam
{
    /// <summary>
    /// Enumerates all possible types of beam failurs.
    /// </summary>
    public enum eFailureTypes
    {
        /// <summary>
        /// Represents failur by congestion.
        /// </summary>
        AllCombinationsCongested,
        /// <summary>
        /// Represents failur in which all combinations are not symetrical.
        /// </summary>
        NoSymetricCombination,
        /// <summary>
        /// Represents failur due to servisability limit state .
        /// </summary>
        SLS,
        /// <summary>
        /// Represents failur due to shear ultimate limit state.
        /// </summary>
        Shear,
        /// <summary>
        /// Represents failur when calculated area of steel is above maximum area of steel allowed.
        /// </summary>
        OverReiforced
    }
}
