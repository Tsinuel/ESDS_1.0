using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Represents ESADS Text styles.
    /// </summary>
    public struct eTextStyle
    {
        /// <summary>
        /// Represents the font represented by this text_left style.
        /// </summary>
        private Font font;
        /// <summary>
        /// Holds a value for property 'ChangeBy'.
        /// </summary>
        private eChangeBy changeBy;
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
        /// Creates an instance of ESADS.EGraphics.eTextStyle structure from the given basic parameters.
        /// </summary>
        /// <param name="font">Font used in the text_left style.</param>
        /// <param name="changeBy">The way how the text_left style change.</param>
        public eTextStyle(Font font, eChangeBy changeBy)
        {
            this.font = font;
            this.changeBy = changeBy;
        }
        /// <summary>
        /// Convers the ESADS.EGraphics.eColor struct to System.Drawing.eColor.
        /// </summary>
        /// <param name="textStyle"></param>
        /// <returns></returns>
        public static implicit operator Font (eTextStyle textStyle)
        {
            return textStyle.font;
        }
        /// <summary>
        /// Sets Font for this text_left style.
        /// </summary>
        /// <param name="font">Font type.</param>
        public void SetFont(Font font)
        {
            this.font = font;
            this.changeBy = eChangeBy.ByObject;
        }
        /// <summary>
        /// Returns the size of the string measured when it is drawn in drawing media.
        /// </summary>
        /// <param name="Text">The text_left to be measured.</param>
        /// <returns></returns>
        public SizeF GetSizeOf(string Text)
        {
            Label l = new Label();
            Graphics g = l.CreateGraphics();            
            return  g.MeasureString(Text, this.font);
        }
        /// <summary>
        /// Gets or set the height of the text_left.
        /// </summary>
        public float Height
        {
            get { return font.Size; }
            set { font = new Font(font.Name, value); }
        }
        /// <summary>
        /// Gets or set the font of the text_left style.
        /// </summary>
        public Font Value
        {
            get { return font; }
            set { font = value; }
        }

        public void SetHeight(double height)
        {
            font = new Font(font.Name, (float)height);
        }
    }
}
