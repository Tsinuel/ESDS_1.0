using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code
{
    /// <summary>
    /// Specifies the available grades of steel based of EBCS
    /// </summary>
    public enum eSteelGrade
    {
        /// <summary>
        /// Represents the grade of steel with characterstic strength of 300MPa
        /// </summary>
        S300 = 300,
        /// <summary>
        /// Represents the grade of steel with characterstic strength of 400MPa
        /// </summary>
        S400 = 400,
        /// <summary>
        /// Represents the grade of steel with characterstic strength of 460MPa
        /// </summary>
        S460 = 460,
        /// <summary>
        /// User defined steel grade.
        /// </summary>
        Custom,
    }
}
