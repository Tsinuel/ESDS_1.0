using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Code;
using ESADS.Code.EBCS_1995;

namespace ESADS.Mechanics.Analysis
{
    public class eLoadCombination
    {
        private double permanentLoadFactor;
        private double variableLoadFactor;
        private string name;

        /// <summary>
        /// Creates a new Load Combination object given all the load factors  and the name.
        /// </summary>
        /// <param name="name">The name of the combination.</param>
        /// <param name="permanentLoadFactor">The partial safety factor for permanent load.</param>
        /// <param name="variableLoadFactor">The partial safety factor for variable load.</param>
        public eLoadCombination(string name, double permanentLoadFactor, double variableLoadFactor)
        {
            this.name = name;
            this.permanentLoadFactor = permanentLoadFactor;
            this.variableLoadFactor = variableLoadFactor;
        }

        /// <summary>
        /// Creates a new load combination object with the minimum load factors given in the code.
        /// </summary>
        public eLoadCombination()
        {
            this.name = "DefaultCombo";
            this.permanentLoadFactor = eBasisOfDesign.GetActionPartialSafetyFactor(eActionType.Permanent);
            this.variableLoadFactor = eBasisOfDesign.GetActionPartialSafetyFactor(eActionType.Variable);
        }
        /// <summary>
        /// Gets or sets the name of the load combination.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Gets the safety factor for the permanent load in the combination.
        /// </summary>
        public double PermanentLoadFactor
        {
            get
            {
                return permanentLoadFactor;
            }
            set
            {
                permanentLoadFactor = value;
            }
        }

        /// <summary>
        /// Gets the safety  factor for the variable load in the combination.
        /// </summary>
        public double VariableLoadFactor
        {
            get
            {
                return variableLoadFactor;
            }
            set
            {
                variableLoadFactor = value;
            }
        }

        /// <summary>
        /// Gets the factored load given the unfactored load and action type.
        /// </summary>
        /// <param name="ActionType">The type of action of the load.</param>
        /// <param name="UnfactoredLoad">The unfactored magnitud of the load.</param>
        public double GetFactored(ESADS.Code.eActionType ActionType, double UnfactoredLoad)
        {
            switch (ActionType)
            {
                case Code.eActionType.Permanent:
                    return permanentLoadFactor * UnfactoredLoad;
                case Code.eActionType.Variable:
                    return variableLoadFactor * UnfactoredLoad;
                default:
                    throw new Exception("Action type not in use in the current version of ESADS");
            }
        }
    }
}
