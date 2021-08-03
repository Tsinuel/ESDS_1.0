using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Code.EBCS_1995
{
    /// <summary>
    /// Contains the provisions given in Chapter 3, Basis of Design of EBCS-2, 1995
    /// </summary>
    public static class eBasisOfDesign
    {
        /// <summary>
        /// Is the maximum compressive strain of concrete in bending(simple or compound) [Sec. 4.2.1.1 of EBCS-2-1995]
        /// </summary>
        public const double ε_c_max_bending = 0.0035;
        /// <summary>
        /// Is the maximum steel tensile strain.
        /// </summary>
        public const double ε_s_max = 0.01;
        /// <summary>
        /// Is the maximum compressive strain of concrete in axial compression
        /// </summary>
        public const double ε_c_max_axial = 0.002;
        /// <summary>
        /// Gets the ultimate limit state partial safety factors for the specified 
        /// material based on the classwork and design situation.[Sec. 3.5.3.1 of EBCS-2-1995]
        /// </summary>
        /// <param name="Material">The type of material defined in eMaterialType 
        /// enumeration whose partial safety factor is to be determined.</param>
        /// <param name="DesignSituation">The design situation defined in 
        /// eDesignSituation enumeration in which the material is going to be used</param>
        /// <param name="ClassWork">The class of work defined in eClassWork 
        /// enumeration that expresses the quality of work of the design.</param>
        /// <returns>The ultimate limit state partial safety factor of the selected material.</returns>
        public static double GetMaterialPartialSafetyFactor(eMaterialType Material, eDesignSituation DesignSituation, 
            eClassWork ClassWork)
        {
            if (Material == eMaterialType.Concrete)
            {
                if (DesignSituation == eDesignSituation.Accidental)
                {
                    if (ClassWork == eClassWork.ClassI)
                    {
                        return 1.3;
                    }
                    else
                    {
                        return 1.45;
                    }
                }
                else
                {
                    if (ClassWork == eClassWork.ClassI)
                    {
                        return 1.5;
                    }
                    else
                    {
                        return 1.65;
                    }
                }
            }
            else
            {
                if (DesignSituation == eDesignSituation.Accidental)
                {
                    if (ClassWork == eClassWork.ClassI)
                    {
                        return 1.00;
                    }
                    else
                    {
                        return 1.1;
                    }
                }
                else
                {
                    if (ClassWork == eClassWork.ClassI)
                    {
                        return 1.15;
                    }
                    else
                    {
                        return 1.2;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the partial safety factors for the specified material based on the type of 
        /// limit state, classwork and design situation.[Sec. 3.5.3 of EBCS-2-1995]
        /// </summary>
        /// <param name="TypeOfDesign">The type of design to be conducted with the safety factor returned.</param>
        /// <param name="Material">The type of material defined in eMaterialType enumeration whose partial safety factor 
        /// is to be determined.</param>
        /// <param name="DesignSituation">The design situation defined in eDesignSituation enumeration in which the 
        /// material is going to be used</param>
        /// <param name="ClassWork">The class of work defined in eClassWork enumeration that expresses the quality of
        /// work of the design.</param>
        /// <returns>The partial safety factor of the selected material</returns>
        public static double GetMaterialPartialSafetyFactor(ESADS.Code.eLimitStateType TypeOfDesign,
            eMaterialType Material, eDesignSituation DesignSituation, eClassWork ClassWork)
        {
            if (TypeOfDesign == eLimitStateType.SLS)
            {
                return 1.0;
            }
            else
            {
                return GetMaterialPartialSafetyFactor(Material, DesignSituation, ClassWork);
            }
        }

        /// <summary>
        /// Gets the partial safety factor for the specified material taking the design situation as Persistent and Transient and the limit state type as Ultimate limit state.
        /// </summary>
        /// <param name="Material">
        /// The type of material defined in eMaterialType enumeration whose partial safety factor 
        /// is to be determined.
        /// </param>
        /// <param name="ClassWork">
        /// The class of work defined in eClassWork enumeration that expresses the quality of
        /// work of the design.
        /// </param>
        public static double GetMaterialPartialSafetyFactor(ESADS.Code.eMaterialType Material, ESADS.Code.eClassWork ClassWork)
        {
            return GetMaterialPartialSafetyFactor(Material, eDesignSituation.PersistentAndTransient, ClassWork);
        }

        /// <summary>
        /// Gets the compressive strength of concrete based on the class of work according to Sec. 3.5.4.1 if EBCS-2-1995
        /// </summary>
        /// <param name="ConcreteGrade">The grade of concrete, defined in eConcreteGrade whose design strength is going to be 
        /// computed.</param>
        /// <param name="ClassWork">The class of the work defined in eClassWork enumeration</param>
        /// <param name="DesignSituation">The design situation, defined in eDesignSituation, in which the concrete is going to
        /// be used.</param>
        /// <returns>The design compressive strength of concrete</returns>
        public static double Get_f_cd(eConcreteGrade ConcreteGrade, eClassWork ClassWork, 
            ESADS.Code.eDesignSituation DesignSituation)
        {
            return Get_f_cd(ConcreteGrade, ClassWork, DesignSituation, eConcreteTestSpecimenType.Cylinder);
        }

        /// <summary>
        /// Gets the compressive strength of concrete based on the class of work according to Sec. 3.5.4.1 if EBCS-2-1995
        /// </summary>
        /// <param name="ConcreteGrade">The grade of concrete, defined in eConcreteGrade whose design strength is going to 
        /// be computed.</param>
        /// <param name="ClassWork">The class of the work defined in eClassWork enumeration</param>
        public static double Get_f_cd(eConcreteGrade ConcreteGrade, eClassWork ClassWork)
        {
            return Get_f_cd(ConcreteGrade, ClassWork, eDesignSituation.PersistentAndTransient);
        }

        /// <summary>
        /// Gets the compressive strength of concrete [Sec. 3.5.4 of EBCS -2-1995]
        /// </summary>
        /// <param name="ConcreteGrade">The grade of concrete, defined in eConcreteGrade whose design strength is going to be 
        /// computed.</param>
        /// <param name="ClassWork">The class of work, defined in eClassWork, in which the concrete is going to be used.</param>
        /// <param name="DesignSituation">The design situation, defined in eDesignSituation, in which the concrete is going to 
        /// be used.</param>
        /// <param name="TypeOfTestSpecimen">The type of concrete test specimen, defined in eConcreteTestSpecimenType, used to 
        /// test the characterstic strength of the concrete.</param>
        public static double Get_f_cd(eConcreteGrade ConcreteGrade, eClassWork ClassWork, 
            eDesignSituation DesignSituation, eConcreteTestSpecimenType TypeOfTestSpecimen)
        {
            double f_ck = eMaterial.Get_f_ck(ConcreteGrade,TypeOfTestSpecimen);
            double FS = GetMaterialPartialSafetyFactor(eMaterialType.Concrete, DesignSituation, ClassWork);
            return 0.85 * f_ck / FS;
        }

        /// <summary>
        /// Gets the desing compressive strength for given characterstic compressive strength and class work of the constraction.
        /// </summary>
        /// <param name="CharactersticCompressiveConcStrgth">Characterstic compressive strength of the concrete.</param>
        /// <param name="ClassWork">Class work of the constraction.</param>
        /// <returns></returns>
        public static double Get_f_cd(double CharactersticCompressiveConcStrgth, eClassWork ClassWork)
        {
            double FS = GetMaterialPartialSafetyFactor(eMaterialType.Concrete, eDesignSituation.PersistentAndTransient, ClassWork);
            return 0.85 * CharactersticCompressiveConcStrgth / FS;
        }

        /// <summary>
        /// Gets the compressive stress of concrete depending on its grade and the amount of strain. Returns zero if out of limits.
        /// It considers cylinderical concrete test specimen with Persistent and Transient design situation.
        /// [Fig. 4.2 of EBCS-2-1995]
        /// </summary>
        /// <param name="ConcreteGrade">The grade of concrete, difined in eConcreteGrade, whose compressive stress is desired.</param>
        /// <param name="AmountOfStrain">The amount of compressive strain of concrete expressed in positive number.</param>
        /// <param name="ClassWork">The class of work, defined in eClassWork, in which the concrete is going to be used</param>
        /// <returns>The design compressive strength of concrete as positive value. If the strain is greater than the maximum 
        /// value specified by the code, is zero or negative(which means the concrete is in tension), zero is returned as there 
        /// is no compressive stress in all these cases.</returns>
        public static double Get_f_c(ESADS.Code.eConcreteGrade ConcreteGrade, double AmountOfStrain, eClassWork ClassWork)
        {
            double f_cd = Get_f_cd(ConcreteGrade, ClassWork);
            return Get_f_c(AmountOfStrain, f_cd);
        }

        /// <summary>
        /// Gets the compressive stress of concrete depending on its grade and the amount of strain. Returns zero if out of limits.
        /// It considers the concrete test specimen as cylinderical.
        /// [Fig. 4.2 of EBCS-2-1995]
        /// </summary>
        /// <param name="ConcreteGrade">The grade of concrete defined in eConcreteGrade, whose stress is to be computed.</param>
        /// <param name="AmountOfStrain">The amount of compressive strain of concrete expressed in positive number.</param>
        /// <param name="ClassWork">The class of work, defined in eClassWork, in which the concrete is going to be used</param>
        /// <param name="DesignSituation">The design situation, defined in eDesignSituation, in which the concrete is used.</param>
        /// <returns>The design compressive strength of concrete as positive value. If the strain is greater than the maximum 
        /// value specified by the code, is zero or negative(which means the concrete is in tension), zero is returned as there is
        /// no compressive stress in all these cases.</returns>
        public static double Get_f_c(eConcreteGrade ConcreteGrade, double AmountOfStrain, ESADS.Code.eClassWork ClassWork, 
            ESADS.Code.eDesignSituation DesignSituation)
        {
            double f_cd = Get_f_cd(ConcreteGrade, ClassWork, DesignSituation);
            return Get_f_c(AmountOfStrain, f_cd);
        }

        /// <summary>
        /// Gets the compressive stress of concrete depending on its grade and the amount of strain. Returns zero if out of limits.
        /// [Fig. 4.2 of EBCS-2-1995]
        /// </summary>
        /// <param name="ConcreteGrade">The grade of concrete defined in eConcreteGrade, whose stress is to be computed.</param>
        /// <param name="AmountOfStrain">The amount of compressive strain of concrete expressed in positive number.</param>
        /// <param name="ClassWork">The class of work, defined in eClassWork, in which the concrete is going to be used</param>
        /// <param name="DesignSituation">The design situation, defined in eDesignSituation, in which the concrete is used.</param>
        /// <param name="TypeOfTestSpecimen">The type of concrete test specimen, defined in eConcreteTestSpecimenType, to test the
        /// concrete characterstic strength.</param>
        /// <returns>The design compressive strength of concrete as positive value. If the strain is greater than the maximum 
        /// value specified by the code, is zero or negative(which means the concrete is in tension), zero is returned as there 
        /// is no compressive stress in all these cases.</returns>
        public static double Get_f_c(ESADS.Code.eConcreteGrade ConcreteGrade, double AmountOfStrain,
            ESADS.Code.eClassWork ClassWork, eDesignSituation DesignSituation, eConcreteTestSpecimenType TypeOfTestSpecimen)
        {
            double f_cd = Get_f_cd(ConcreteGrade, ClassWork, DesignSituation, TypeOfTestSpecimen);
            return Get_f_c(AmountOfStrain, f_cd);
        }

        /// <summary>
        /// Gets the compressive stress of concrete depending on its grade and the amount of strain. Returns zero if out of limits.
        /// </summary>
        /// <param name="AmountOfStrain">The amount of compressive strain of concrete expressed in positive number.</param>
        /// <param name="f_cd">The design compressive strength of concrete whose stress is to be computed.</param>
        /// <returns>
        /// The design compressive strength of concrete as positive value. If the strain is greater than the maximum 
        /// value specified by the code, is zero or negative(which means the concrete is in tension), zero is returned as there 
        /// is no compressive stress in all these cases.
        /// </returns>
        public static double Get_f_c(double AmountOfStrain, double f_cd)
        {
            return 1000 * AmountOfStrain * f_cd * (1 - 250 * AmountOfStrain);
        }

        /// <summary>
        /// Gets the design tensile strength of concrete as per Sec. 3.5.4 of EBCS-2-1995
        /// </summary>
        /// <param name="Grade">The grade of concrete, defined in eConcreteGrade, whose  design tensile strength is to be computed.</param>
        /// <param name="ClassWork">The class of work, defined in eClassWork, in which the concrete concerned is found in.</param>
        public static double Get_f_ctd(eConcreteGrade Grade, eClassWork ClassWork)
        {
            return Get_f_ctd(Grade, ClassWork, eConcreteTestSpecimenType.Cylinder);
        }

        /// <summary>
        /// Gets the design tensile strength of concrete as per Sec. 3.5.4 of EBCS-2-1995
        /// </summary>
        /// <param name="Grade">The grade of concrete, defined in eConcreteGrade, whose  design tensile strength is to be computed.</param>
        /// <param name="ClassWork">The class of work, defined in eClassWork, in which the concrete concerned is found in.</param>
        /// <param name="TypeOfTestSpecimen">The type of concrete test specimen, defined in eConcreteTestSpecimenType, used for determination of the characterstic strength of the concrete</param>
        public static double Get_f_ctd(eConcreteGrade Grade, eClassWork ClassWork, eConcreteTestSpecimenType TypeOfTestSpecimen)
        {
            return Get_f_ctd(Grade, ClassWork, TypeOfTestSpecimen, eDesignSituation.PersistentAndTransient);
        }

        /// <summary>
        /// Gets the design tensile strength of concrete as per Sec. 3.5.4 of EBCS-2-1995
        /// </summary>
        /// <param name="Grade">The grade of concrete, defined in eConcreteGrade, whose  design tensile strength is to be computed.</param>
        /// <param name="ClassWork">The class of work, defined in eClassWork, in which the concrete concerned is found in.</param>
        /// <param name="TypeOfTestSpecimen">The type of concrete test specimen, defined in eConcreteTestSpecimenType, used for determination of the characterstic strength of the concrete</param>
        /// <param name="DesignSituation">The design situation, defined in eDesignSituation, in which the concrete concerned is found in.</param>
        public static double Get_f_ctd(eConcreteGrade Grade, eClassWork ClassWork, eConcreteTestSpecimenType TypeOfTestSpecimen, eDesignSituation DesignSituation)
        {
            double f_ctk = eMaterial.Get_f_ctk(Grade, TypeOfTestSpecimen);
            double FS = GetMaterialPartialSafetyFactor(eMaterialType.Concrete, DesignSituation, ClassWork);
            return f_ctk / FS;
        }

        /// <summary>
        /// Returns the Design tesile strength of a concrete for a given characterstic concrete tensile strength and class work of constraction.
        /// </summary>
        /// <param name="CharactersticConcreteTensileStrength">Characterstic concrete tensile strength.</param>
        /// <param name="ClassWork">Class work of the constraction.</param>
        /// <returns></returns>
        public static double Get_f_ctd(double CharactersticConcreteTensileStrength, eClassWork ClassWork)
        {
            double FS = GetMaterialPartialSafetyFactor(eMaterialType.Concrete, eDesignSituation.PersistentAndTransient, ClassWork);
            return CharactersticConcreteTensileStrength / FS;
        }

        /// <summary>
        /// Gets the design strength of steel
        /// </summary>
        /// <param name="Grade">The grade of steel, defined in eSteelGrade, whose design strength is to be calculated</param>
        /// <param name="ClassWork">The class of work, defined in eClassWork, in which the steel concerned is to be used.</param>
        /// <param name="DesignSituation">The design situation, defined in eDesignSituation, in which the concerned steel is found.</param>
        public static double Get_f_yd(eSteelGrade Grade, eClassWork ClassWork, eDesignSituation DesignSituation)
        {
            double f_yk = eMaterial.Get_f_yk(Grade);
            double FS = GetMaterialPartialSafetyFactor(eMaterialType.ReinforcingSteel, DesignSituation, ClassWork);
            return f_yk / FS;
        }

        /// <summary>
        /// Gets the design strength of steel
        /// </summary>
        /// <param name="Grade">The grade of steel, defined in eSteelGrade, whose design strength is to be calculated</param>
        /// <param name="ClassWork">The class of work, defined in eClassWork, in which the steel concerned is to be used.</param>
        public static double Get_f_yd(eSteelGrade Grade, eClassWork ClassWork)
        {
            return Get_f_yd(Grade, ClassWork,eDesignSituation.PersistentAndTransient);
        }

        /// <summary>
        /// Returns the design yeild strength of a steel reinforcement for a given characterstic yeild strength and classwork.
        /// </summary>
        /// <param name="CharactersticYeildStrength">The characterstic strength of the steel.</param>
        /// <param name="ClassWork">The class work of the construction.</param>
        /// <returns></returns>
        public static double Get_f_yd(double CharactersticYeildStrength, eClassWork ClassWork)
        {
            return CharactersticYeildStrength / GetMaterialPartialSafetyFactor(eMaterialType.ReinforcingSteel, eDesignSituation.PersistentAndTransient, ClassWork);
        }

        /// <summary>
        /// Gets the limit state partial safety factor for the specified action type [Table 3.3 of EBCS-2-1995]
        /// </summary>
        /// <param name="ActionType">The type of action, defined in eActionType, whose partial safety factor is to be determined.</param>
        /// <param name="ActionCondition">The condition of aciton, defined in eActionCondition</param>
        /// <param name="DesignSituation">The design situation, defined in eDesignSituation</param>
        public static double GetActionPartialSafetyFactor(ESADS.Code.eActionType ActionType, ESADS.Code.eActionCondition ActionCondition, eDesignSituation DesignSituation)
        {
            switch (ActionType)
            {
                case eActionType.Permanent:
                    {
                        if(ActionCondition == eActionCondition.Favourable)
                        {
                            if(DesignSituation == eDesignSituation.Accidental)
                            {
                                return 1.0;
                            }
                            else
                            {
                                return 1.0;
                            }
                        }
                        else
                        {
                            if(DesignSituation == eDesignSituation.Accidental)
                            {
                                return 1.0;
                            }
                            else
                            {
                                return 1.3;
                            }
                        }
                    }
                case eActionType.Variable:
                    {
                        if(ActionCondition == eActionCondition.Favourable)
                        {
                            if(DesignSituation == eDesignSituation.Accidental)
                            {
                                throw new Exception("The only action defined for Accidental Design situation is Permanent Action.");
                            }
                            else
                            {
                             return 0.0;   
                            }
                        }
                        else
                        {
                            if(DesignSituation == eDesignSituation.Accidental)
                            {
                                throw new Exception("The only action defined for Accidental Design situation is Permanent Action.");
                            }
                            else
                            {
                                return 1.6;
                            }
                        }
                    }
                default:
                    {
                        throw new Exception("Action type other than Accidental and Permanent was used which is out of the scope.");
                    }
            }
        }

        /// <summary>
        /// Gets the limit state partial safety factor for the specified action type [Table 3.3 of EBCS-2-1995]
        /// </summary>
        /// <param name="ActionType">The type of action, defined in eActionType, whose partial safety factor is to be determined.</param>
        public static double GetActionPartialSafetyFactor(eActionType ActionType)
        {
            return GetActionPartialSafetyFactor(ActionType, eActionCondition.Unfavourable, eDesignSituation.PersistentAndTransient); 
        }

        /// <summary>
        /// Gets the limit state partial safety factor for the specified action type [Table 3.3 of EBCS-2-1995]
        /// </summary>
        /// <param name="ActionType">The type of action, defined in eActionType, whose partial safety factor is to be determined.</param>
        /// <param name="DesignSituation">The design situation, defined in eDesignSituation</param>
        public static double GetActionPartialSafetyFactor(ESADS.Code.eActionType ActionType, eDesignSituation DesignSituation)
        {
            return GetActionPartialSafetyFactor(ActionType, eActionCondition.Unfavourable, DesignSituation);
        }

        /// <summary>
        /// Gets the limit state partial safety factor for the specified action type [Table 3.3 of EBCS-2-1995]
        /// </summary>
        /// <param name="ActionType">The type of action, defined in eActionType, whose partial safety factor is to be determined.</param>
        /// <param name="ActionCondition">The type of action condition, defined in eActionCondition</param>
        public static double GetActionPartialSafetyFactor(ESADS.Code.eActionType ActionType, eActionCondition ActionCondition)
        {
            return GetActionPartialSafetyFactor(ActionType, ActionCondition, eDesignSituation.PersistentAndTransient);
        }
    }
}

