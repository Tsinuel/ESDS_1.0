using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code
{
    /// <summary>
    /// The classification of concrete based on their physical property like weight, density etc.
    /// </summary>
    public enum eConcreteType
    {
        /// <summary>
        /// Concrete with relatively lighter in weight.
        /// </summary>
        LightWeight,
        /// <summary>
        /// Concrete applied for most constructions
        /// </summary>
        NormalWeight,
        /// <summary>
        /// Concrete with a relatively heavier made with heavier aggregate.
        /// </summary>
        HeavyWeight,
        /// <summary>
        /// Concrete used in Reinforced and prestressed construction.
        /// </summary>
        ReinforcedAndPrestressed,
        /// <summary>
        /// Fresh concrete before hardening.
        /// </summary>
        Unhardened,
        /// <summary>
        /// Concrete type defined by user specifying the unit weight.
        /// </summary>
        UserDefined,
    }
}
