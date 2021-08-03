using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Code.EBCS_1995;
using ESADS.Code;

namespace ESADS.Mechanics.Design
{
    /// <summary>
    /// Contains information related to eConcrete class
    /// </summary>
    [Serializable]
    public class eConcrete
    {
        #region Private Fields

        /// <summary>
        /// Holds the value of the characterstic compressive strength property.
        /// </summary>
        private double charactersticCompressiveStrength;
        /// <summary>
        /// Holds the value of the characterstic tensile strength property.
        /// </summary>
        private double charactersticTensileStrength;
        /// <summary>
        /// Access the public property 'fcd'.
        /// </summary>
        private double desnCompStrength;
        /// <summary>
        /// Accesses the public property 'fctd'.
        /// </summary>
        private double desnTensStrength;
        /// <summary>
        /// Holds the value of the Concrete grade property.
        /// </summary>
        private eConcreteGrade grade;
        /// <summary>
        /// Holds the value of the modulus of elasticity property of the material.
        /// </summary>
        private double modulusOfElasticity;
        /// <summary>
        /// Holds the value of the unit weight property of the c.
        /// </summary>
        private double unitWeight;
        /// <summary>
        /// Holds the value of the c type property.
        /// </summary>
        private eConcreteType concreteType;
        /// <summary>
        /// Holds a value for 'MaxAggtSize'.
        /// </summary>
        private double maxAggtSize;
        /// <summary>
        /// Holds the value of 'Name'.
        /// </summary>
        private string name;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates new instance of c from its properties directly instead of grade.
        /// </summary>
        /// <param name="name">The name of the concrete material</param>
        /// <param name="charactersticCompressiveStrength">The characterstic compressive strength of the c.</param>
        /// <param name="charactersticTensileStrength">The characterstic tensile strength of the c.</param>
        /// <param name="modulusOfElasticity">The modulus of elasticity of the c.</param>
        /// <param name="unitWeight">The unit weight of the c.</param>
        public eConcrete(string name, double charactersticCompressiveStrength, double charactersticTensileStrength, double modulusOfElasticity, double unitWeight)
        {
            this.name = name;
            this.charactersticCompressiveStrength = charactersticCompressiveStrength;
            this.charactersticTensileStrength = charactersticTensileStrength;
            this.modulusOfElasticity = modulusOfElasticity;
            this.unitWeight = unitWeight;
            this.concreteType = eConcreteType.NormalWeight;
            this.grade = eConcreteGrade.Custom;
            this.desnCompStrength = eBasisOfDesign.Get_f_cd(charactersticCompressiveStrength, eClassWork.ClassI);
            this.desnTensStrength = eBasisOfDesign.Get_f_ctd(charactersticTensileStrength, eClassWork.ClassI);
            this.maxAggtSize = 20;
        }
        /// <summary>
        /// Creates new instance of c from its properties directly instead of grade.
        /// </summary>
        /// <param name="charactersticCompressiveStrength">The characterstic compressive strength of the c.</param>
        /// <param name="charactersticTensileStrength">The characterstic tensile strength of the c.</param>
        /// <param name="modulusOfElasticity">The modulus of elasticity of the c.</param>
        /// <param name="unitWeight">The unit weight of the c.</param>
        public eConcrete(double charactersticCompressiveStrength, double charactersticTensileStrength, double modulusOfElasticity, double unitWeight)
        {
            this.charactersticCompressiveStrength = charactersticCompressiveStrength;
            this.charactersticTensileStrength = charactersticTensileStrength;
            this.modulusOfElasticity = modulusOfElasticity;
            this.unitWeight = unitWeight;
            this.concreteType = eConcreteType.NormalWeight;
            this.grade = eConcreteGrade.Custom;
            this.desnCompStrength = eBasisOfDesign.Get_f_cd(charactersticCompressiveStrength, eClassWork.ClassI);
            this.desnTensStrength = eBasisOfDesign.Get_f_ctd(charactersticTensileStrength, eClassWork.ClassI);
            this.maxAggtSize = 20;
            this.name = grade.ToString();
        }

        /// <summary>
        /// Creates new instance of c from its name, grade and c type
        /// </summary>
        /// <param name="grade">The grade of the c.</param>
        /// <param name="type">The type of the c defined by the eConcreteType enumeration.</param>
        public eConcrete(eConcreteGrade grade, eConcreteType type)
        {
            this.charactersticCompressiveStrength = eMaterial.Get_f_ck(grade);
            this.charactersticTensileStrength = eMaterial.Get_f_ctk(grade);
            this.modulusOfElasticity = eMaterial.Get_ConcModOfElasticity(grade);
            this.unitWeight = eMaterial.Get_ConcUnitWeight(type);
            this.concreteType = type;
            this.grade = grade;
            this.desnCompStrength = eBasisOfDesign.Get_f_cd(grade, eClassWork.ClassI);
            this.desnTensStrength = eBasisOfDesign.Get_f_ctd(grade, eClassWork.ClassI);
            this.maxAggtSize = 20;
            this.name = grade.ToString();
        }

        /// <summary>
        /// Creates new instance of c from its name, grade considering the c as the normal weight.
        /// </summary>
        /// <param name="grade">The grade of the c.</param>
        public eConcrete(eConcreteGrade grade)
        {
            // if the grade is custom it throws an exption since this constractor accepts only predefined grade values.
            if (grade == eConcreteGrade.Custom)
            {
                throw new eIllegalGradeAssignmentException("Custom grade cannot be used as predefined grade type.");
            }
            this.charactersticCompressiveStrength = eMaterial.Get_f_ck(grade);
            this.charactersticTensileStrength = eMaterial.Get_f_ctk(grade);
            this.modulusOfElasticity = eMaterial.Get_ConcModOfElasticity(grade);
            this.unitWeight = eMaterial.Get_ConcUnitWeight(eConcreteType.NormalWeight);
            this.grade = grade;
            this.concreteType = eConcreteType.NormalWeight;
            this.desnCompStrength = eBasisOfDesign.Get_f_cd(grade, eClassWork.ClassI);
            this.desnTensStrength = eBasisOfDesign.Get_f_ctd(grade, eClassWork.ClassI);
            this.maxAggtSize = 20;
            this.name = grade.ToString();
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the concrete material.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Gets or sets the characterstic compressive strength of the material. When this is set, it automatically chages the grade property to custom.
        /// </summary>
        public double fck
        {
            get
            {
                return charactersticCompressiveStrength;
            }
        }

        /// <summary>
        /// Gets or sets the characterstic tensile strength of the material. When this is set, it automatically chages the grade property to custom.
        /// </summary>
        public double fctk
        {
            get
            {
                return charactersticTensileStrength;
            }
        }

        /// <summary>
        /// Gets or sets the maximum size of aggregate used in the concrete mix.
        /// </summary>
        public double MaxAgrtSize
        {
            get { return maxAggtSize; }
            set { maxAggtSize = value; }
        }

        /// <summary>
        /// Gets the design yeild strength of the c.
        /// </summary>
        public double fcd
        {
            get { return desnCompStrength; }
        }

        /// <summary>
        /// Gets the the design tesile strength of the c.
        /// </summary>
        public double fctd
        {
            get { return desnTensStrength; }
        }

        /// <summary>
        /// Gets or sets the modulus of elasticity of the c material.
        /// </summary>
        public double E
        {
            get
            {
                return modulusOfElasticity;
            }
        }

        /// <summary>
        /// Gets the unit weight of the c unit weight.
        /// </summary>
        public double UnitWeight
        {
            get
            {
                return unitWeight;
            }
        }

        /// <summary>
        /// Gets the grade of the c. If the properties are user defined, the grade is custom.
        /// </summary>
        public eConcreteGrade Grade
        {
            get
            {
                return grade;
            }
        }

        /// <summary>
        /// Gets the c type of the material.
        /// </summary>
        public eConcreteType ConcreteType
        {
            get
            {
                return concreteType;
            }
        }
        /// <summary>
        /// Get the maximum bending stress in concrete.
        /// </summary>
        public double εb
        {
            get { return eBasisOfDesign.ε_c_max_bending; }
        }
        /// <summary>
        /// Get the maximum axial compresion stress in concrete.
        /// </summary>
        public double εc
        {
            get { return eBasisOfDesign.ε_c_max_axial; }
        }
        #endregion

        #region Methods

        /// <summary>
        ///Sets the class work of the constraction using this c material.
        /// </summary>
        /// <param name="ClassWork">The class work in which the design is taking place.</param>
        public void SetClassWork(eClassWork ClassWork)
        {
            if (grade == eConcreteGrade.Custom)
            {
                desnCompStrength = eBasisOfDesign.Get_f_cd(charactersticCompressiveStrength, ClassWork);
                desnTensStrength = eBasisOfDesign.Get_f_ctd(charactersticCompressiveStrength, ClassWork);
            }
            else
            {
                desnCompStrength = eBasisOfDesign.Get_f_cd(grade, ClassWork);
                desnTensStrength = eBasisOfDesign.Get_f_ctd(grade, ClassWork);
            }
        }

        public override bool Equals(object obj)
        {
            try
            {
                eConcrete conc = (eConcrete)obj;

                if (conc.charactersticCompressiveStrength == this.charactersticCompressiveStrength && conc.charactersticTensileStrength == this.charactersticTensileStrength &&
                    conc.concreteType == this.concreteType && conc.grade == this.grade && conc.modulusOfElasticity == this.modulusOfElasticity && conc.unitWeight == this.unitWeight)
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }

        public static bool operator ==(eConcrete left, eConcrete right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(eConcrete left, eConcrete right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Returns the name of the concrete material.
        /// </summary>
        public override string ToString()
        {
            return this.name;
        }
        #endregion

    }
}
