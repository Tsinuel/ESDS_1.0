using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code
{
    /// <summary>
    /// Defines the types of test specimen to test the compressive strength of concrete.
    /// </summary>
    public enum eConcreteTestSpecimenType
    {
        /// <summary>
        /// 200mm sized cubical test specimen
        /// </summary>
        Cube,
        /// <summary>
        /// 150mm diameter and 300mm high cylinderical test specimen
        /// </summary>
        Cylinder,
    }
}
