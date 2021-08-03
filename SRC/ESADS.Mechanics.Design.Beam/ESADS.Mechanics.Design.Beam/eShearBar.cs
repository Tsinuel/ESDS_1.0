using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Code;
using ESADS.Code.EBCS_1995;

namespace ESADS.Mechanics.Design.Beam
{
    public struct eShearBar : eIBar
    {
        #region Fields
        /// <summary>
        /// Hold a value for property 'Diameter'.
        /// </summary>
        private double diameter;
        /// <summary>
        /// Hold a value for property 'Lengths'.
        /// </summary>
        private double[] lengths;
        /// <summary>
        /// Hold a value for property 'Name'.
        /// </summary>
        private string name;
        /// <summary>
        /// Holds a value for property 'BarType'.
        /// </summary>
        private eShearBarTypes barType;
        private eDShearSection section;
        private bool isTop;
        #endregion

        #region Properties
        
        /// <summary>
        /// Gets or sets the diamter of the shearBar.
        /// </summary>
        public double Diameter
        {
            get
            {
                return eXBar.GetDiam(section.FlexureSection.Beam.StirupBar);
            }
            set
            {
                
            }
        }

        /// <summary>
        /// Gets the spacing of the shearBar.
        /// </summary>
        public double Spacing
        {
            get { return this.section.BarSpacing; }
        }

        /// <summary>
        /// Gets the total length of the shearBar.
        /// </summary>
        public double Length
        {
            get
            {
                if (this.barType == eShearBarTypes.EnclosingStirrup)
                    return lengths.Sum() * 2;
                else
                    return lengths[0] * 2 + lengths[1];
            }
        }

        /// <summary>
        /// Gets the lengths of each segment. For enclosing type it has three lengths, i.e. hook length, width and depth in this order. For inner stirrups it has two numbers, viz.
        /// hook length and width.
        /// </summary>
        public double[] Lengths
        {
            get { return (double[])this.lengths.Clone(); }
        }

        /// <summary>
        /// Gets the name or the mark of the shearBar.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets the type of the shear shearBar used.
        /// </summary>
        public eShearBarTypes BarType
        {
            get { return barType; }
        }

        public bool IsTop
        {
            get
            {
                return this.isTop;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of ESAD.Mechanics.Design.eShearBar structure for a given name and shear section.
        /// </summary>
        /// <param name="name">Name of the shearBar.</param>
        /// <param name="section">The section which designed the bar.</param>
        public eShearBar(eShearBarTypes barType, eDShearSection section)
        {
            this = new eShearBar();
            this.section = section;
            this.barType = barType;
            this.name = "st-" + section.Name;
            this.diameter = eXBar.GetDiam(section.Beam.StirupBar);
            this.lengths = new double[] { 0 };
            FillDetails();
        }

        /// <summary>
        /// Creates an instance of ESAD.Mechanics.Design.eShearBar structure for a given name and shear section.
        /// </summary>
        /// <param name="name">Name of the shearBar.</param>
        /// <param name="section">The section which designed the bar.</param>
        public eShearBar(eShearBarTypes barType, eDShearSection section, bool isTop, string name)
        {
            this = new eShearBar();
            this.section = section;
            this.barType = barType;
            this.name =name;
            this.isTop = isTop;
            this.diameter = eXBar.GetDiam(section.Beam.StirupBar);
            this.lengths = new double[] { 0 };
            FillDetails();
        }

        #endregion

        #region Mehods
        /// <summary>
        /// Fills all the necessary details for this shearBar.
        /// </summary>
        private void FillDetails()
        {
            if (barType == eShearBarTypes.EnclosingStirrup)
            {
                lengths = new double[3];

                lengths[0] = section.Beam.StirrupHookLength; //the length of the hook.
                lengths[1] = section.Width - 2 * (section.Beam.Cover + eXBar.GetDiam(section.Beam.StirupBar) / 2.0); //the two horizontal lengths.
                lengths[2] = section.Depth - 2 * (section.Beam.Cover + eXBar.GetDiam(section.Beam.StirupBar) / 2.0); //the two vertical lengths.
            }
            else
            {
                lengths = new double[2];

                lengths[0] = section.Beam.StirrupHookLength;
                lengths[1] = section.Width - 2 * (section.Beam.Cover + eXBar.GetDiam(section.Beam.StirupBar) / 2.0);
            }

        }
        #endregion
    }
}
