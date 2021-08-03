using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Code;
using ESADS.Code.EBCS_1995;

namespace ESADS.Mechanics.Design.Beam
{
    /// <summary>
    /// Represents a base class for sections to be designed.
    /// </summary>
    public abstract class eDSection
    {
        #region Feilds
        /// <summary>
        /// Hold a value for public property 'Name'.
        /// </summary>
        protected string name;
        /// <summary>
        /// Holds the value of porperty 'Depth'.
        /// </summary>
        protected double D;
        /// <summary>
        /// Holds the value of porperty 'EffectiveDepht'.
        /// </summary>
        protected double d;
        /// <summary>
        /// Holds the value of porperty 'Width'.
        /// </summary>
        protected double b;
        /// <summary>
        /// Hold value for property 'DesignCompleted'.
        /// </summary>
        protected bool designCompleted;
        /// <summary>
        /// Holds the value of the 'Intervals' property.
        /// </summary>
        protected List<double[]> intervals;
        /// <summary>
        /// Holds the value of the 'Beam'.
        /// </summary>
        protected eDBeam beam;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets the concrete used for this section and the whole beam.
        /// </summary>
        protected eConcrete c
        {
            get
            {
                return beam.Concrete;
            }
        }

        /// <summary>
        /// Gets the steel used for this section and the whole beam.
        /// </summary>
        protected eSteel s
        {
            get
            {
                return beam.Steel;
            }
        }

        /// <summary>
        /// Gets or sets the width of the section.
        /// </summary>
        public double Width
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
            }
        }

        /// <summary>
        /// Gets or sets the depth of the section.
        /// </summary>
        public double Depth
        {
            get
            {
                return D;
            }
            set
            {
                D = value;
            }
        }
      
        /// <summary>
        /// Gets the effective depth of the section.
        /// </summary>
        public double EffectiveDepth
        {
            get
            {
                return d;
            }
        }

        /// <summary>
        /// Gets the value indicating whether the design is completed or not.
        /// </summary>
        public bool DesignCompleted
        {
            get { return designCompleted; }
        }

        /// <summary>
        /// Gets or sets the coordinate intervals of the beam in which the section is defined. The first dimension is for the number of intervals while the second is for the start and end of the interval.
        /// </summary>
        /// <summary>
        /// Gets or sets the ordered pairs representing the start and end coordinate of the length overwhich the section applies.
        /// </summary>
        public List<double[]> Intervals
        {
            get
            {
                return intervals;
            }
            set
            {
                intervals = value;
            }
        }

        /// <summary>
        /// Gets the parent beam of the section
        /// </summary>
        public eDBeam Beam
        {
            get
            {
                return this.beam;
            }
        }

        /// <summary>
        /// Gets the diameter of the stirrup used.
        /// </summary>
        protected double stirrupD
        {
            get
            {
                return eXBar.GetDiam(beam.StirupBar);
            }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Design.eDSection class for a given basic parameters.
        /// </summary>
        /// <param name="beam">The beam bearing the section</param>
        /// <param name="width">Width of the section.</param>
        /// <param name="depth">Depth of the section.</param>
        protected eDSection(eDBeam beam, double width, double depth)
        {
            this.beam = beam;
            this.b = width;
            this.D = depth;
            this.name = "";

            this.intervals = new List<double[]>();
        }

        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Design.eDSection class for a given basic parameters.
        /// </summary>
        /// <param name="beam">The beam bearing the section</param>
        /// <param name="name">Name of the section.</param>
        /// <param name="width">Width of the section.</param>
        /// <param name="depth">Depth of the section.</param>
        protected eDSection(eDBeam beam, string name, double width, double depth)
        {
            this.beam = beam;
            this.b = width;
            this.D = depth;

            if (name == null)
                this.name = "";
            else
                this.name = name;


            this.intervals = new List<double[]>();
        }

        #endregion

        #region Method
       

        /// <summary>
        /// Designs this section.
        /// </summary>
        public abstract void Design();

        internal abstract bool IsSimilar(eDSection section);
        #endregion

    }
}
