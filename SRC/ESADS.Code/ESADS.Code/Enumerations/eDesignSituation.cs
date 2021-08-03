using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code
{
    /// <summary>
    /// Those sets of physical conditions representing a certain time interval for which the design will demonstrate that relevant limit states are not exceeded. [Section 3.3.1 of EBCS -2, 1995], [Section 1.1.3.2 of EBCS-1, 1995]
    /// </summary>
    /// <remarks>
    /// See Section 3.3.1 of EBCS-2, 1995
    /// See also Section 1.1.3.2 of EBCS -1, 1995 for definition
    /// </remarks>
    public enum eDesignSituation
    {
        /// <summary>
        /// Design situation involving exceptional conditions of the structure or its exposure, e.g. fire, explosion, impact or local failure. [Sec. 1.1.3.2 of EBCS-1, 1995]
        /// </summary>
        Accidental,
        /// <summary>
        /// Applying both persistent and transient conditions. Persistent means Design situation which is relevant during a period of the same order as the design working life of the structure. Generally it refers to conditions of normal use. [Sec 1.1.3.2 of EBCS -1, 1995]. Transient means Design situation which is relevant during a period much shorter that the design working life of the structure and which as a high probability of occurrence. It refers to temporary conditions of the structure, of use, or exposure, e.g. during construction or repair. [Sec 1.1.3.2 of EBCS-1, 1995].
        /// </summary>
        PersistentAndTransient,
    }
}
