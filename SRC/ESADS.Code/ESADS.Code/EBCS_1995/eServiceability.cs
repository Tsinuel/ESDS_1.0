using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code.EBCS_1995
{
    /// <summary>
    /// Contains the provision given in Chapter 5-Serviceability Limit States of EBCS-2-1995
    /// </summary>
    public static class eServiceability
    {
        /// <summary>
        /// Gets the constant given in Table 5.1 of EBCS -2 1995
        /// </summary>
        /// <param name="TypeOfStructure">The type of structure, defined in eStructureType, under consideration.</param>
        /// <param name="TypeOfSpan">The type of span, defined in eSpanType, whose coefficient is to be determined.</param>
        public static double Get_β_a(eStructureType TypeOfStructure, eSpanType TypeOfSpan)
        {
            switch (TypeOfStructure)
            {
                case eStructureType.Beam:
                    {
                        if (TypeOfSpan == eSpanType.SimplySupported)
                            return 20;
                        else if (TypeOfSpan == eSpanType.EndSpan)
                            return 24;
                        else if (TypeOfSpan == eSpanType.InteriorSpan)
                            return 28;
                        else
                            return 10;
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }
        }

        /// <summary>
        /// Gets the minimum depth requirement stated in Sec 5.2.3 of EBCS-2-1995
        /// </summary>
        /// <param name="SteelGrade">The grade of steel to be used</param>
        /// <param name="EffectiveSpan">The effective span in meter and for two way slabs it is the shorter span.</param>
        /// <param name="TypeOfSpan">Is one of the types of span defined by the eTypeOfSpan enumeration</param>
        /// <param name="TypeOfStructure">Is one of the types of structures defined by the eTypeOfStructure enumeration</param>
        public static double GetMinEffDepth(eSteelGrade SteelGrade, double EffectiveSpan, eSpanType TypeOfSpan, 
            eStructureType TypeOfStructure)
        {
            double f_yk = eMaterial.Get_f_yk(SteelGrade);
            return (0.4 + 0.6 * f_yk / 400) * EffectiveSpan / Get_β_a(TypeOfStructure, TypeOfSpan);
        }
    }
}
