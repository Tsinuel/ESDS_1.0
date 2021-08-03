using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code
{
    /// <summary>
    /// Set of materials defined in the building code.
    /// </summary>
    public enum eMaterialType
    {
        /// <summary>
        /// Is a material made from a mixture of aggregates and cement and moulded into a shape.
        /// </summary>
        Concrete,
        /// <summary>
        /// Is the material composed into RC to impart tensile strength capacity for concrete
        /// </summary>
        ReinforcingSteel,
    }
}
