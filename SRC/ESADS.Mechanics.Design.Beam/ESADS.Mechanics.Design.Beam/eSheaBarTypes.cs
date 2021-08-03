using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Design.Beam
{
    public enum eShearBarTypes
    {
        /// <summary>
        /// Shear shearBar that inclose all shearBar in the beam.
        /// </summary>
        EnclosingStirrup,
        /// <summary>
        /// Inner stirrups that added to hold middle bars of rows of longitudinal bars above the bottom row.
        /// </summary>
        InnerStirrup,
    }
}
