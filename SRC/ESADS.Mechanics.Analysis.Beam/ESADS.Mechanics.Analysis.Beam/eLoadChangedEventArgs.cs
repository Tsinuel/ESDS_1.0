using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Analysis.Beam
{
    /// <summary>
    /// Event argument containing information related ot ESADS.Mechanics.eLoad.Changed event.
    /// </summary>
    public class eLoadChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Value of IsDimensionChanged property.
        /// </summary>
        private bool isDimensionChanged;
        /// <summary>
        /// Value of IsMagnitudeChanged property.
        /// </summary>
        private bool isMagnitudeChanged;
        /// <summary>
        /// Value of IsOrientationChanged property.
        /// </summary>
        private bool isOrientationChanged;

        /// <summary>
        /// Creates new event argument when both the Magnitude and the  dimension have changed.
        /// </summary>
        /// <param name="isDimChanged">value if indicating if the dimension has changed.</param>
        /// <param name="isMagChanged">value if the Magnitude has changed.</param>
        public eLoadChangedEventArgs(bool isDimChanged, bool isMagChanged)
        {
            this.isMagnitudeChanged = isMagChanged;
            this.isDimensionChanged = isDimChanged;
        }

        /// <summary>
        /// Creates new event argument when the orientation of a triangular load has changed.
        /// </summary>
        /// <param name="isOrientationChanged">Value if orientation changed.</param>
        public eLoadChangedEventArgs(bool isOrientationChanged)
        {
            this.isDimensionChanged = false;
            this.isMagnitudeChanged = false;
            this.isOrientationChanged = isOrientationChanged;
        }
        /// <summary>
        /// Gets or sets the value if the dimension of the load relative to the joint has changed.
        /// </summary>
        public bool IsDimensionChanged
        {
            get
            {
                return this.isDimensionChanged;
            }
            set
            {
                this.isDimensionChanged = value;
            }
        }

        /// <summary>
        /// Gets or sets the value if the Magnitude of the load has changed.
        /// </summary>
        public bool IsMagnitudeChanged
        {
            get
            {
                return this.isMagnitudeChanged;
            }
            set
            {
                this.isMagnitudeChanged = value;
            }
        }

        /// <summary>
        /// Gets or sets the value if the orientation of a triangular load has changed.
        /// </summary>
        public bool IsOrientationChanged
        {
            get
            {
                return isOrientationChanged;
            }
            set
            {
                isOrientationChanged = value;
            }
        }
    }
}
