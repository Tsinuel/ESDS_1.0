using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Code.EBCS_1995;
using ESADS.Code;

namespace ESADS.Mechanics.Design
{
    /// <summary>
    /// Contains information related to Reinforcement eSteel
    /// </summary>
    [Serializable]
    public class eSteel
    {
        #region Fields

        /// <summary>
        /// feild used to access public property 'DesnCharStrgth'
        /// </summary>
        private double charYeildStrgth;
        /// <summary>
        /// feild used to access public property 'DesnMaxStrain'
        /// </summary>
        private double desnMaxStrain;
        /// <summary>
        /// feild used to access public property 'DesnYeildStrgth'
        /// </summary>
        private double desnYeildStrgth;
        /// <summary>
        /// feild used to access public property 'Grade'
        /// </summary>
        private eSteelGrade grade;
        /// <summary>
        /// feild used to access public property 'ClassWork'
        /// </summary>
        private double modulOfElast;
        /// <summary>
        /// field used to access public property 'DesnYeildStrain'
        /// </summary>
        private double desnYeildStrain;
        /// <summary>
        /// Access the public property 'UnitWeight'.
        /// </summary>
        private double unitWeight;
        /// <summary>
        /// Holds the value of 'Name'.
        /// </summary>
        private string name;

        #endregion

        #region Constructor

        /// <summary>
        /// Intializes an instance of ESADS_Mechanics.eSteel class from a given grade of S.
        /// </summary>
        /// <param name="grade">Specifies the grade of S used</param>
        public eSteel(eSteelGrade grade)
        {
            if (grade == eSteelGrade.Custom)
            {
                throw new eIllegalGradeAssignmentException("Custom grade cannot be used as predefined grade type.");
            }
            this.grade = grade;
            this.modulOfElast = eMaterial.GetSteelModulusOfElasticity();
            this.charYeildStrgth = eMaterial.Get_f_yk(grade);
            this.desnMaxStrain = eBasisOfDesign.ε_s_max;
            this.desnYeildStrgth = eBasisOfDesign.Get_f_yd(grade, eClassWork.ClassI);
            this.desnYeildStrain = desnYeildStrgth / modulOfElast;
            this.unitWeight = eMaterial.GetSteelUnitWeight();
            this.name = grade.ToString();
        }

        /// <summary>
        /// Creates an instance of ESADS_Mechanics.eSteel class from a given custom grade,modules of elasticity and characterstic yeild strength.
        /// </summary>
        /// <param name="modulesOfElasticity">Modulus of elasticity for the newly defined S material.</param>
        /// <param name="charYeildStrength">characterstic yeild strength of the newly defined S material.</param>
        /// <param name="unitWeight">Unit wheight of the newly defined S material.</param>
        public eSteel(double modulesOfElasticity, double charYeildStrength, double unitWeight)
        {
            this.grade = eSteelGrade.Custom;
            this.modulOfElast = modulesOfElasticity;
            this.charYeildStrgth = charYeildStrength;
            this.desnMaxStrain = eBasisOfDesign.ε_s_max;
            this.desnYeildStrgth = eBasisOfDesign.Get_f_yd(charYeildStrength, eClassWork.ClassI);
            this.desnYeildStrain = desnYeildStrgth / modulesOfElasticity;
            this.unitWeight = unitWeight;
            this.name = grade.ToString();
        }
        /// <summary>
        /// Creates an instance of ESADS_Mechanics.eSteel class from a given custom grade,modules of elasticity and characterstic yeild strength.
        /// </summary>
        /// <param name="name">The name of the steel material.</param>
        /// <param name="modulesOfElasticity">Modulus of elasticity for the newly defined steel material.</param>
        /// <param name="charYeildStrength">characterstic yeild strength of the newly defined steel material.</param>
        /// <param name="unitWeight">Unit wheight of the newly defined steel material.</param>
        public eSteel(string name, double modulesOfElasticity, double charYeildStrength, double unitWeight)
        {
            this.name = name;
            this.grade = eSteelGrade.Custom;
            this.modulOfElast = modulesOfElasticity;
            this.charYeildStrgth = charYeildStrength;
            this.desnMaxStrain = eBasisOfDesign.ε_s_max;
            this.desnYeildStrgth = eBasisOfDesign.Get_f_yd(charYeildStrength, eClassWork.ClassI);
            this.desnYeildStrain = desnYeildStrgth / modulesOfElasticity;
            this.unitWeight = unitWeight;
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets the grade of S.
        /// </summary>
        public eSteelGrade Grade
        {
            get
            {
                return grade;
            }
        }

        /// <summary>
        /// Gets the Desing Yeild Strength of Steel Class
        /// </summary>
        public double fyd
        {
            get { return desnYeildStrgth; }
        }

        /// <summary>
        /// Gets the characterstic yeild strength of S material.
        /// </summary>
        public double fyk
        {
            get
            {
                return charYeildStrgth;
            }
        }

        /// <summary>
        /// Gets the unit weight of S.
        /// </summary>
        public double UnitWeight
        {
            get { return unitWeight; }
        }
        /// <summary>
        /// Gets the modulus of elasticity of Steel materail.
        /// </summary>
        public double E
        {
            get
            {
                return modulOfElast;
            }
        }

        /// <summary>
        /// Gets the design maximum strain of Steel material.
        /// </summary>
        public double εsMax
        {
            get
            {
                return desnMaxStrain;
            }
        }

        /// <summary>
        /// Gets the design yeild strain of Steel material.
        /// </summary>
        public double εs
        {
            get
            {
                return desnYeildStrain;
            }
        }

        /// <summary>
        /// Gets the name of the steel.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the classWork of the constraction using this material.
        /// </summary>
        /// <param name="ClassWork">ClassWork of the constraction.</param>
        public void SetClassWork(eClassWork ClassWork)
        {
            //If the grade is custom it will calculate the design yeild strength using 
            //its defined characterstic yeild strength and class work.
            if (grade == eSteelGrade.Custom)
            {
                desnYeildStrgth = eBasisOfDesign.Get_f_yd(charYeildStrgth, ClassWork);
            }
            else //if the garde is not custom it will calculate using its grade and  class work
            {
                desnYeildStrgth = eBasisOfDesign.Get_f_yd(grade, ClassWork);
            }

            //Calculates the the design yeild strain whenever the class work is changed.
            desnYeildStrain = desnYeildStrgth / modulOfElast;
        }

        public override bool Equals(object obj)
        {
            try
            {
                eSteel stl = (eSteel)obj;

                if (stl.charYeildStrgth == this.charYeildStrgth && stl.grade == this.grade && stl.modulOfElast == this.modulOfElast && this.unitWeight == stl.unitWeight)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool operator ==(eSteel left, eSteel right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(eSteel left, eSteel right)
        {
            return left.Equals(right);
        }   
        
        /// <summary>
        /// Returns the name of the steel.
        /// </summary>
        public override string ToString()
        {
            return this.name;
        }
        #endregion
    }
}
