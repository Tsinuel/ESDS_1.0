using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Contains all the new informations needed in connection with any graphic change of a beam_Analysis member.
    /// </summary>
    public class eMemberGraphicsEventArgs : EventArgs
    {
        /// <summary>
        /// Holds the value of the location property.
        /// </summary>
        private PointF location;
        /// <summary>
        /// Holds the value of the End property.
        /// </summary>
        private PointF end;
        /// <summary>
        /// Holds the value of 'Length' property.
        /// </summary>
        private double length;

        /// <summary>
        /// Creates a new event argument for Member_Analysis Resize event.
        /// </summary>
        /// <param name="location">The future location of the member.</param>
        /// <param name="end">The future end point of the member.</param>
        /// <param name="length">Future length of the member.</param>
        public eMemberGraphicsEventArgs(PointF location, PointF end, double length)
        {
            this.location = location;
            this.end = end;
            this.length = length;
        }

        public eMemberGraphicsEventArgs(PointF location, PointF end)
        {
            this.location = location;
            this.end = end;
        }
        /// <summary>
        /// Gets the location that resized member will have after the completion of the resize.
        /// </summary>
        public PointF Location
        {
            get
            {
                return location;
            }
        }

        /// <summary>
        /// Gets the end point that the resized member will have.
        /// </summary>
        public PointF End
        {
            get
            {
                return end;
            }
        }

        /// <summary>
        /// Gets the future length of the member
        /// </summary>
        public double Length
        {
            get
            {
                return length;
            }
        }
    }
}
