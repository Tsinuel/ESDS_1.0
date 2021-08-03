using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Design.Beam
{
    /// <summary>
    /// Represents barType1 used in the reinforced structures.
    /// </summary>
    public interface eIBar
    {
        /// <summary>
        /// Gets or sets the  the diameter of the shearBar.
        /// </summary>
        double Diameter
        {
            get;
            set;
         
        }

        /// <summary>
        /// Gets or sests the name of the shearBar.
        /// </summary>
        string Name
        {
            get;
            set;
        }
    }
}
