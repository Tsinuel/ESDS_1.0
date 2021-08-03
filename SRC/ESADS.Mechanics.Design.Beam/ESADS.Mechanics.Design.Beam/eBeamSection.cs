using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Design.Beam
{
    public class eBeamSection
    {
        /// <summary>
        /// Holds the value of 'Depth'.
        /// </summary>
        private double depth;
        /// <summary>
        /// Holds the value of 'Width'.
        /// </summary>
        private double width;
        /// <summary>
        /// Holds the value of 'Name'.
        /// </summary>
        private string name;
        /// <summary>
        /// Holds the value of 'Used'.
        /// </summary>
        private bool used;
        private bool useNominal_EI;
        private double nominal_EI;

        /// <summary>
        /// Creates a new instance of a beam section object.
        /// </summary>
        /// <param name="name">The name of the section.</param>
        /// <param name="depth">The depth of the section.</param>
        /// <param name="width">The width of the section.</param>
        public eBeamSection(string name, double depth, double width)
        {
            this.name = name;
            this.depth = depth;
            this.width = width;
            this.used = false;
        }

        /// <summary>
        /// Creates the default beam cross-section 
        /// </summary>
        public eBeamSection()
        {
            this.name = "B 30x40";
            this.depth = eUtility.Convert(400, eLengthUnits.mm, eUtility.SLU);
            this.width = eUtility.Convert(300, eLengthUnits.mm, eUtility.SLU);
            this.used = false;
        }

        /// <summary>
        /// Gets or sets the depth of the section.
        /// </summary>
        public double Depth
        {
            get
            {
                return this.depth;
            }
            set
            {
                if (value >= 0)
                    this.depth = value;
                else
                    throw new Exception("The depth of a beam cannot be negative");
            }
        }

        /// <summary>
        /// Gets or sets the width of the section.
        /// </summary>
        public double Width
        {
            get
            {
                return this.width;
            }
            set
            {
                if (value >= 0)
                    this.width = value;
                else
                    throw new Exception("The width of a beam section cannot be negative");
            }
        }

        /// <summary>
        /// Gets or sets the name of the section.
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
        /// Gets or sets the value indicating if the section has been used.
        /// </summary>
        public bool Used
        {
            get
            {
                return this.used;
            }
            set
            {
                this.used = value;
            }
        }

        /// <summary>
        /// Gets or sets a nominal value of EI if assigned by user.
        /// </summary>
        public double Nominal_EI
        {
            get
            {
                if (this.useNominal_EI)
                    return this.nominal_EI;
                else
                    throw new Exception("The section is not a type to use Nominal EI value.");
            }
            set
            {
                this.nominal_EI = value;
            }
        }

        /// <summary>
        /// Gets or sets whether to use a nominal value of EI.
        /// </summary>
        public bool UseNominal_EI
        {
            get
            {
                return this.useNominal_EI;
            }
            set
            {
                this.useNominal_EI = value;
            }
        }

        /// <summary>
        /// Gets the area of the section.
        /// </summary>
        public double GetArea()
        {
            return this.depth * this.width;
        }

        /// <summary>
        /// Gets the moment of inertia of the section.
        /// </summary>
        public double GetMomentOfInertia()
        {
            return this.width * Math.Pow(this.depth, 3) / 12;
        }

        public override string ToString()
        {
            return this.name;
        }

        public override bool Equals(object obj)
        {
            try
            {
                eBeamSection other = (eBeamSection)obj;

                if (this.depth != other.depth)
                    return false;
                if (this.width != other.width)
                    return false;
                if (this.name != other.name)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
