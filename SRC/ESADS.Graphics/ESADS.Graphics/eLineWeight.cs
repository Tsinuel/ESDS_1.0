using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Represents ESADS line weights which contains information related to line wheight of a line.
    /// </summary>
    public struct eLineWeight
    { /// <summary>
        /// Holds a value for public property 'ChangeLineWeightBy'.
        /// </summary>
        private eChangeBy changeBy;
        /// <summary>
        /// Holds a value for public property 'LineWeight'.
        /// </summary>
        private float lineWeight;
        /// <summary>
        /// Gets or sets the way by which the line weight of a drawing object change.
        /// </summary>
        public eChangeBy ChangeBy
        {
            get { return changeBy; }
            set { changeBy = value; }
        }

        /// <summary>
        /// Gets the line weight value.
        /// </summary>
        public float LineWeight
        {
            get
            {
                return this.lineWeight;
            }
        }
        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eLineWeight structure from the given parameters.
        /// </summary>
        /// <param name="thickness">Thickness of the line.</param>
        /// <param name="changeBy">Value indicating the way how the lineweight change.</param>
        public eLineWeight(float thickness, eChangeBy changeBy)
        {
            this.lineWeight = thickness;
            this.changeBy = changeBy;
        }

        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eLineWeight structure from the given parameters.
        /// </summary>
        /// <param name="thickness">Thickness of the line.</param>
        public eLineWeight(float thickness)
        {
            this.lineWeight = thickness;
            this.changeBy = eChangeBy.ByLayer;
        }
        /// <summary>
        /// Convers the ESADS.EGraphics.eLineType struct to Syste.Float structure.
        /// </summary>
        public static implicit operator float(eLineWeight LineWeight)
        {
            return LineWeight.lineWeight;
        }
        /// <summary>
        /// Sets the line weight for this Color.
        /// </summary>
        /// <param name="thickness">Thickness of the line.</param>
        public void SetLineWeight(float thickness)
        {
            this.lineWeight = thickness;
            this.changeBy = eChangeBy.ByObject;
        }
    }
}
