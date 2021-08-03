using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;
using System.Drawing;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Represents ESADS color types.
    /// </summary>
    public struct eColor
    {
        /// <summary>
        /// Holds a value for property 'ChangeBy'.
        /// </summary>
        private eChangeBy changeBy;
        /// <summary>
        /// Contains the clor represented in this structure.
        /// </summary>
        private Color color;
        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eColor structure from given basic parameters.
        /// </summary>
        /// <param name="color">Required color.</param>
        /// <param name="changeBy">Indicates the way how the color change.</param>
        public eColor(Color color, eChangeBy changeBy)
        {
            this.color = color;
            this.changeBy = changeBy;
        }
        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eColor structure from given basic parameters.
        /// </summary>
        /// <param name="color">Required color.</param>
        public eColor(Color color)
        {
            this.color = color;
            this.changeBy = eChangeBy.ByObject;
        }
        /// <summary>
        /// Gets or set the value indicating how this color change.
        /// </summary>
        public eChangeBy ChangeBy
        {
            get
            {
                return changeBy;
            }
            set
            {
                changeBy = value;
            }
        }

        /// <summary>
        /// Sets the color from the ARGB values.
        /// </summary>
        /// <param name="Alpha">The alpha value ranging from 0-255.</param>
        /// <param name="Red">The red value ranging from 0-255.</param>
        /// <param name="Green">The green value ranging from 0-255.</param>
        /// <param name="Blue">The blue value ranging from 0-255.</param>
        public void FromArgb(int Alpha ,int Red, int Green, int Blue)
        {
            this.color = Color.FromArgb(Alpha, Red, Green, Blue);
        }
        /// <summary>
        /// Sets the color from the ARGB values.
        /// </summary>
        /// <param name="Red">The red value ranging from 0-255.</param>
        /// <param name="Green">The green value ranging from 0-255.</param>
        /// <param name="Blue">The blue value ranging from 0-255.</param>
        public void FromArgb(int Red, int Green, int Blue)
        {
            this.color = Color.FromArgb( Red, Green, Blue);
        }
        /// <summary>
        /// Sets the color from predefied colors.
        /// </summary>
        /// <param name="Color">The color to set the original value.</param>
        public void SetColor(Color Color)
        {
            this.color = Color;
            this.changeBy = eChangeBy.ByObject;
        }
        /// <summary>
        /// Convers the ESADS.EGraphics.eColor struct to System.Drawing.eColor.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static implicit operator Color (eColor c)
        {
            return c.color;
        }
        public Color Value
        {
            get { return color; }
            set { color = value; }
        }
        public static bool operator ==(eColor c1,eColor c2)
        {
            if (c1.color == c2.color)
                return true;
            else
             return false;
        }

        public static bool operator !=(eColor c1, eColor c2)
        {
            if (c1.color == c2.color)
                return false;
            else
                return true;
        }
    }
}
