using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Code;
using ESADS;

namespace ESADS.Mechanics.Design.Beam
{
    /// <summary>
    /// Contains information related to reinforcement shearBar.
    /// </summary>
    [Serializable]
    public struct eXBar : eIBar
    {
        #region Feilds
        /// <summary>
        /// Accesses public property 'Diameter'.
        /// </summary>
        private double diameter;
        /// <summary>
        /// Accesses public property 'Area'.
        /// </summary>
        private double area;
        /// <summary>
        /// Accesses public property 'XCoord'.
        /// </summary>
        private double xCoord;
        /// <summary>
        /// Accesses public property 'YCoord'.
        /// </summary>
        private double yCoord;
        /// <summary>
        /// Accesses the 'Name' property.
        /// </summary>
        private string name;
        /// <summary>
        /// Holds a value for property 'Row'.
        /// </summary>
        private int row;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of eBar class given Diameter,X-Coordinate and Y-Coordinate.
        /// </summary>
        /// <param name="diameter">Diameter of the shearBar.</param>
        /// <param name="xCoord">X-Coordinate of the shearBar.</param>
        /// <param name="yCoord">Y-Coordinate of the shearBar.</param>
        public eXBar(double diameter, double xCoord, double yCoord)
        {
            this.diameter = diameter;
            this.xCoord = xCoord;
            this.yCoord = yCoord;
            this.name = "";
            this.area = 0;
            this.row = 0;
            this.area = GetArea(diameter);
        }

        public eXBar(double diameter, double xCoord)
        {
            
            this.diameter = diameter;
            this.xCoord = xCoord;
            this.yCoord = 0;
            this.name = "";
            this.area = 0;
            this.row = 0;
            this.area = GetArea(diameter);
        }

        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Design.eXBar struct from given shearBar diameter.
        /// </summary>
        /// <param name="diameter">Diameter of the shearBar.</param>
        public eXBar(double diameter)
        {
            this.diameter = diameter;
            this.xCoord = 0;
            this.yCoord = 0;
            this.name = "";
            this.row = 0;
            this.area = 0;
            this.area = GetArea(diameter);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the area of a shearBar for the given diameter.
        /// </summary>
        /// <param name="diameter">Diameter of the shearBar.</param>
        /// <returns></returns>
        public static double GetArea(double diameter)
        {
            return (Math.PI / 4) * Math.Pow(diameter, 2);
        }

        /// <summary>
        /// Fills the detail of shearBar.
        /// </summary>
        public void FillDetails()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the diameter of the given shearBar.
        /// </summary>
        /// <param name="Bar">The shearBar whose diamter is to be determined.</param>
        public static double GetDiam(eReinforcement Bar)
        {
            return eUtility.Convert((int)Bar, eLengthUnits.mm, eUtility.SLU);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the Diameter of a Bar.
        /// </summary>
        public double Diameter
        {
            get { return diameter; }
            set { diameter = value; }
        }

        /// <summary>
        /// Gets the area of a shearBar.
        /// </summary>
        public double Area
        {
            get { return area; }
        }

        /// <summary>
        /// Gets the X-Coordinate of a shearBar.
        /// </summary>
        public double XCoord
        {
            get { return xCoord; }
            set { xCoord = value; }
        }

        /// <summary>
        /// Gets the Y- Coordinate of a shearBar.
        /// </summary>
        public double YCoord
        {
            get { return yCoord; }
            set { yCoord = value; }
        }

        /// <summary>
        /// Gets or sets the name of the shearBar.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the total length of the shearBar.
        /// </summary>
        public double Length
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets or sets the lengths of each segments.
        /// </summary>
        public double[] Lengths
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets or sets the row of this shearBar counted from the bottom row.
        /// </summary>
        public int Row
        {
            get { return row; }
            set { row = value; }
        }
        #endregion
    }
}
