using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS
{
    /// <summary>
    /// Provideds data for event which is raised when unit system used in the measurment is changed.
    /// </summary>
    public class eUnitChangedEventArgs:EventArgs
    {
        /// <summary>
        /// Value of the 'ForceUnit' property.
        /// </summary>
        private eForceUints forceUnit;
        /// <summary>
        /// Value of the 'LengthUnit' property.
        /// </summary>
        private eLengthUnits lengthUnit;

        /// <summary>
        /// Creates a new event argument with the new force and length units.
        /// </summary>
        /// <param name="newForceUnit">The new force unit.</param>
        /// <param name="newLengthUnit">The new length unit.</param>
        public eUnitChangedEventArgs(eForceUints newForceUnit, eLengthUnits newLengthUnit)
        {
            this.forceUnit = newForceUnit;
            this.lengthUnit = newLengthUnit;
        }
        /// <summary>
        /// Gets the newly changed length unit.
        /// </summary>
        public eLengthUnits LengthUnit
        {
            get
            {
                return lengthUnit;
            }
        }

        /// <summary>
        /// Gets the newly changed force unit.
        /// </summary>
        public eForceUints ForceUnit
        {
            get
            {
                return forceUnit;
            }
        }
    }
}
