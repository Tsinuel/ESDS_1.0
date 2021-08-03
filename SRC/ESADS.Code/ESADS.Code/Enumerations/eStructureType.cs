using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code
{
    /// <summary>
    /// Defines the type of structure(Organized combination of connected parts designed to provide some measure of rigidity.
    /// [Sec. 1.1.3 of EBCS-1-1995])
    /// </summary>
    public enum eStructureType
    {
        /// <summary>
        /// A_General structural component mainly working in bending through the agency of vertical forces
        /// and that transmits to the bearing points the loads that are applied to it.
        /// </summary>
        Beam,
        /// <summary>
        /// A_General structural component mainly working in axial force and bending and transmits load vertically.
        /// </summary>
        Column,
        /// <summary>
        /// An element generally parallelepipedic of small thickness in comparison with its surface, used as
        /// inspection cover, in pavement, cover of box culvert, deck of bridge, etc.
        /// </summary>
        Slab,
        /// <summary>
        /// A_General substructure used to transfer superstructure load from column to the underlying soil.
        /// </summary>
        Footing,
    }
}
