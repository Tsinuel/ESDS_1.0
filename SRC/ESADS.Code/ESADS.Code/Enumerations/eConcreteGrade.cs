using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code
{
    /// <summary>
    /// Used to specify different types of grade of concrete interms of its characterstic compressive cube strength.
    /// </summary>
    /// <remarks>Allowable Grades in Class I and Class II Work are different, therefore there must be 
    /// FindDesnCompStrength condition to handle this. This enumertion totaly include the domains of both ClassI Works</remarks>
    public enum eConcreteGrade
    {
        /// <summary>
        /// Concrete having characterstic compressive cube strength of 5MPa.
        /// </summary>
        C5 = 5,
        /// <summary>
        /// Concrete having characterstic compressive cube strength of 15MPa.
        /// </summary>
        C15 = 15,
        /// <summary>
        /// Concrete having characterstic compressive cube strength of 20MPa.
        /// </summary>
        C20 = 20,
        /// <summary>
        /// Concrete having characterstic compressive cube strength of 25MPa.
        /// </summary>
        C25 = 25,
        /// <summary>
        /// Concrete having characterstic compressive cube strength of 30MPa.
        /// </summary>
        C30 = 30,
        /// <summary>
        /// Concrete having characterstic compressive cube strength of 40MPa.
        /// </summary>
        C40 = 40,
        /// <summary>
        /// Concrete having characterstic compressive cube strength of 50MPa.
        /// </summary>
        C50 = 50,
        /// <summary>
        /// Concrete having characterstic compressive cube strength of 60MPa.
        /// </summary>
        C60 = 60,
        /// <summary>
        /// User defined concrete grade.
        /// </summary>
        Custom,
    }
}
