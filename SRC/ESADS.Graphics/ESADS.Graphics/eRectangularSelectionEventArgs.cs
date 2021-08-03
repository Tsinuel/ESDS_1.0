using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Contains the information needed from the ending of a rectangular selection.
    /// </summary>
    public class eRectangularSelectionEventArgs :EventArgs
    {
        /// <summary>
        /// Value of the property, 'Region'
        /// </summary>
        private Region region;
        /// <summary>
        /// Value of the property, 'IsPositive'.
        /// </summary>
        private bool isPositive;
        /// <summary>
        /// Value of the property, 'SuppressEvent'.
        /// </summary>
        private bool suppressEvent;

        /// <param name="region">The region of the selection rectangle.</param>
        /// <param name="isPositive">Value if the rectangle selects all the objects it touches. If the value is false, it selects all the objects it touches.</param>
        public eRectangularSelectionEventArgs(Region region, bool isPositive)
        {
            this.region = region;
            this.isPositive = isPositive;
            this.suppressEvent = false;
        }
        /// <summary>
        /// Gets the region of the selection rectangle.
        /// </summary>
        public Region Region
        {
            get
            {
                return region;
            }
        }

        /// <summary>
        /// Gets if the selection is of positive type.
        /// </summary>
        public bool IsPositive
        {
            get
            {
                return isPositive;
            }
        }

        /// <summary>
        /// Gets or sets the value whether to apply the event or to avoid it.
        /// </summary>
        public bool SuppressEvent
        {
            get
            {
                return suppressEvent;
            }
            set
            {
                suppressEvent = value;
            }
        }
    }
}
