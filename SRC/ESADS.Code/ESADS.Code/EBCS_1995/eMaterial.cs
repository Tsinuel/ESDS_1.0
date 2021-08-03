using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code.EBCS_1995
{
    /// <summary>
    /// Contains provisions given in Chapter 2-Data on concrete and steel of EBCS-2-1995.
    /// </summary>
    public static class eMaterial
    {
        /// <summary>
        /// Gets the characterstic cylinder compressive concrete strength [Sec. 2.3 of EBCS-2-1995]
        /// </summary>
        /// <param name="Grade">The grade of concrete, defined in eConcreteGrade, whose characterstic compressive strength is to be computed.</param>
        /// <param name="TypeOfTestSpecimen">The type of concrete test specimen, defined in eConcreteTestSpecimenType, used to determine the characterstic compressive strength of the concrete.</param>
        public static double Get_f_ck(eConcreteGrade Grade, eConcreteTestSpecimenType TypeOfTestSpecimen)
        {
            if (TypeOfTestSpecimen == eConcreteTestSpecimenType.Cube)
            {
                return (eUtility.Convert((((int)Grade / 1.05) * 1000000), eForceUints.N, eUtility.SFU) / (Math.Pow(eUtility.Convert(1, eLengthUnits.m, eUtility.SLU), 2)));
            }
            else
            {
                return (eUtility.Convert((((int)Grade / 1.25) * 1000000), eForceUints.N, eUtility.SFU) / (Math.Pow(eUtility.Convert(1, eLengthUnits.m, eUtility.SLU), 2)));
            }
        }

        /// <summary>
        /// Gets the characterstic compressive strength of concrete based on the grade and type of test specimen [Sec. 2.3 of EBCS-2-1995]
        /// </summary>
        /// <param name="Grade">The grade of concrete, defined in eConcreteGrade, whose characterstic compressive strength is to be computed.</param>
        public static double Get_f_ck(eConcreteGrade Grade)
        {
            return Get_f_ck(Grade, eConcreteTestSpecimenType.Cylinder);
        }

        /// <summary>
        /// Gets the characterstic concrete tensile strength[Sec. 2.4 of EBCS-2-1995]
        /// </summary>
        /// <param name="Grade">The grade of concrete, defined in eConcreteGrade, whose characterstic tensile strength is to be computed.</param>
        public static double Get_f_ctk(eConcreteGrade Grade)
        {
            return Get_f_ctk(Grade, eConcreteTestSpecimenType.Cylinder);
        }

        /// <summary>
        /// Gets the characterstic concrete tensile strength[Sec. 2.4 of EBCS-2-1995]
        /// </summary>
        /// <param name="Grade">The grade of concrete, defined in eConcreteGrade, whose characterstic tensile strength is to be computed.</param>
        /// <param name="TypeOfTestSpecimen">The type of concrete test specimen, defined in eConcreteTestSpecimenType, used to determine the characterstic tensile strength of the concrete.</param>
        public static double Get_f_ctk(eConcreteGrade Grade, eConcreteTestSpecimenType TypeOfTestSpecimen)
        {
            double f_ck = Get_f_ck(Grade, TypeOfTestSpecimen);
            double f_ctm = 0.3 * Math.Pow(f_ck, 0.66666666666666666666667);
            return 0.7 * f_ctm;
        }

        /// <summary>
        /// Gets the characterstic steel strength
        /// </summary>
        /// <param name="Grade">The grade of steel, defined in eSteelGrade, whose characterstic  strength is to be determined.</param>
        public static double Get_f_yk(eSteelGrade Grade)
        {
            return (double)Grade;
        }

        /// <summary>
        /// Gets the modulus of elasticity of the selected material.
        /// </summary>
        /// <param name="ConcreteGrade">The grade of concrete, defined in eConcreteGrade, whose modulus of elasticity is to be computed.</param>
        /// <returns>The modulus of elasticity of concrete</returns>
        public static double Get_ConcModOfElasticity(eConcreteGrade ConcreteGrade)
        {
            double f_ck = Get_f_ck(ConcreteGrade);
            double res = (9.5 * Math.Pow((f_ck + 8), 0.3333333333333333333333333333));
            return res * 1000;
        }

        /// <summary>
        /// Gets the unit weight of concrete based on its type[Table 2.1 of EBCS-1-1995]
        /// </summary>
        /// <param name="TypeOfConcrete">The type of concrete, defined in eConcreteType, whose unit weight is to be computed.</param>
        /// <returns>The unit weight of the specified concrete in Kg/mm^3</returns>
        public static double Get_ConcUnitWeight(eConcreteType TypeOfConcrete)
        {
            switch (TypeOfConcrete)
            {
                case eConcreteType.NormalWeight:
                    {
                        return (eUtility.Convert(24, eForceUints.KN, eUtility.SFU) / Math.Pow(eUtility.Convert(1, eLengthUnits.m, eUtility.SLU), 3));
                    }
                case eConcreteType.HeavyWeight:
                    {
                        return (eUtility.Convert(28, eForceUints.KN, eUtility.SFU) / Math.Pow(eUtility.Convert(1, eLengthUnits.m, eUtility.SLU), 3));
                    }
                case eConcreteType.ReinforcedAndPrestressed:
                    {
                        return (eUtility.Convert(25, eForceUints.KN, eUtility.SFU) / Math.Pow(eUtility.Convert(1, eLengthUnits.m, eUtility.SLU), 3));
                    }
                case eConcreteType.Unhardened:
                    {
                        return (eUtility.Convert(25, eForceUints.KN, eUtility.SFU) / Math.Pow(eUtility.Convert(1, eLengthUnits.m, eUtility.SLU), 3));
                    }
                default:
                    {
                        throw new Exception();
                    }
            }
        }

        /// <summary>
        /// Gets the Unit weight of Steel in system units. [Sec. 2.8.1  of EBCS-2-1995]
        /// </summary>
        /// <returns></returns>
        public static double GetSteelUnitWeight()
        {
            return  (eUtility.Convert((7850 * 9.81), eForceUints.N, eUtility.SFU) / (Math.Pow(eUtility.Convert(1, eLengthUnits.m, eUtility.SLU), 3)));
        }
        /// <summary>
        /// Gets the Modulus of elasticity of steel in system units [Sec.2.9.4.1 of EBCS-2-1995]
        /// </summary>
        /// <returns></returns>
        public static double GetSteelModulusOfElasticity()
        {
            return (eUtility.Convert(200000000, eForceUints.KN, eUtility.SFU) / (Math.Pow(eUtility.Convert(1, eLengthUnits.m, eUtility.SLU), 2)));
        }
    }
}
