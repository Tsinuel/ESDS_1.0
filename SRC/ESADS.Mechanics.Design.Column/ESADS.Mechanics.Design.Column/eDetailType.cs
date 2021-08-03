using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Design.Column
{
    public enum eDetailType
    {
        /// <summary>
        /// Type of rectangularly reinforced column with uniformly distributed reinforcement at the top and bottom of the section.
        /// </summary>
        Type1,
        /// <summary>
        /// Type of rectangularly reinforced column with uniformly distributed reinforcement around all the sides.
        /// </summary>
        Type2,
        /// <summary>
        /// Type of rectangularly reinforced column with number of bars in all sides defined.
        /// </summary>
        Type3,
        Type4,
    }
}
