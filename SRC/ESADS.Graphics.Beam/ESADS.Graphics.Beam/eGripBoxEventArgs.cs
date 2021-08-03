using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ESADS.EGraphics.Beam
{
    public class eGripBoxEventArgs : EventArgs
    {
        /// <summary>
        /// Value of the SuppressChanges
        /// </summary>
        private bool suppressChanges;
        /// <summary>
        /// value of the 'location'
        /// </summary>
        private PointF location;
    
        public eGripBoxEventArgs()
            : base()
        {

        }

        public eGripBoxEventArgs(bool suppressChanges)
        {
            this.suppressChanges = suppressChanges;
        }

        public eGripBoxEventArgs(PointF location)
        {
            this.location = location;
        }

        public eGripBoxEventArgs(bool suppressChanges, PointF location)
        {
            this.suppressChanges = suppressChanges;
            this.location = location;
        }

        /// <summary>
        /// Gets or sets the value whether to apply the changes when the grip box is being turned off.
        /// </summary>
        public bool SuppressChanges
        {
            get
            {
                return suppressChanges;
            }
            set
            {
                suppressChanges = value;
            }
        }

        /// <summary>
        /// Gets the location of the grip box when the event occured.
        /// </summary>
        public PointF Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

    }
}
