using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Mechanics.Design;

namespace ESADS.EGraphics
{
    public interface eIGModel
    {
        /// <summary>
        /// Gets layers collection in the component.
        /// </summary>
        eLayers Layers
        {
            get;
        }

        /// <summary>
        /// Design the component on which it is called.
        /// </summary>
        void Design();
    }
}
