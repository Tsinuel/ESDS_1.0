using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code
{
    public static class eActionsOnStructure
    {
        /// <summary>
        /// Returns the imposed load on floors of building with different floor functional category.
        /// </summary>
        /// <param name="category">Functional category of the floor.</param>
        /// <returns></returns>
        public static double GetImposedLoad(eLoadCategories category)
        {
            switch(category)
            {
                case eLoadCategories.A_Balcolnies:
                    return 2;
                case eLoadCategories.A_General:
                    return 3;
                case eLoadCategories.A_Stair:
                    return 4;
                case eLoadCategories.B:
                    return 3;
                case eLoadCategories.C1:
                    return 3;
                case eLoadCategories.C2:
                    return 4;
                case eLoadCategories.C3:
                    return 5;
                case eLoadCategories.C4:
                    return 5;
                case eLoadCategories.C5:
                    return 5;
                case eLoadCategories.D1:
                    return 5;
                case eLoadCategories.D2:
                    return 5;
                case eLoadCategories.E:
                    return 6;
                default :
                    return 0;
            }
        }
    }
}
