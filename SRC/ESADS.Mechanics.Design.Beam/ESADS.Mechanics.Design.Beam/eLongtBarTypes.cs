using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Design.Beam
{
    public enum eLongtBarTypes
    {
        /// <summary>
        /// Left support tensile top bar of bar type1.
        /// </summary>
        Bar1 = 3,
        /// <summary>
        /// Left support tensile top bar of bar type2.
        /// </summary>
        Bar2 = 4,
        /// <summary>
        /// Span compressive top bar of bar type1.
        /// </summary>
        Bar3 = 1,
        /// <summary>
        /// Span compressive top bar of bar type2.
        /// </summary>
        Bar4 = 2,
        /// <summary>
        /// Left support compression bottom bar of bar type1.
        /// </summary>
        Bar5 = 5,
        /// <summary>
        /// Left support compression bottom bar of bartype2.
        /// </summary>
        Bar6 = 6,
        /// <summary>
        /// Span tensile bottom bar of bar type1.
        /// </summary>
        Bar7 = 7,
        /// <summary>
        /// Span tensile bottom bar of bar type2.
        /// </summary>
        Bar8 = 8,
        /// <summary>
        /// Right support tensile top bar of bar type1.
        /// </summary>
        Bar9 = 3,
        /// <summary>
        /// Right support tensile top bar of bar type2.
        /// </summary>
        Bar10 = 4,
        /// <summary>
        /// Right support compressive bottom bar of bar type1.
        /// </summary>
        Bar11 = 5,
        /// <summary>
        /// Right support compressive bottom bar of bar type2.
        /// </summary>
        Bar12 = 6,
    }
}
