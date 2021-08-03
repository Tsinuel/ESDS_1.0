using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Represents ESADS line type which contains information related to line type.
    /// </summary>
    public struct eLineType
    {
        /// <summary>
        /// Holds a value for the line type represented by this structure.
        /// </summary>
        private eLineTypes lineType;
        /// <summary>
        /// Holds a value for property 'ChangeLineTypeBy'.
        /// </summary>
        private eChangeBy changeBy;
        /// <summary>
        /// Holds a value for public property 'DashPatern'.
        /// </summary>
        private float[] dashPatern;

        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eLineType structure from the given basic parameters.
        /// </summary>
        /// <param name="lineType">The enumeration representing the line type.</param>
        /// <param name="changeBy">Represents the way by which the line type changes.</param>
        public eLineType(eLineTypes lineType,eChangeBy changeBy)
        {
            this.lineType = lineType;
            this.changeBy = changeBy;
            this.dashPatern = null;
            this.dashPatern = GetDashPatern(lineType);
        }
        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eLineType structure from the given basic parameters.
        /// </summary>
        /// <param name="lineType">The enumeration representing the line type.</param>
        /// <param name="scale">The scale of the dash pattern</param>
        public eLineType(eLineTypes lineType, float scale = 1)
        {
            this.lineType = lineType;
            this.changeBy = eChangeBy.ByLayer;
            this.dashPatern = null;
            this.dashPatern = GetDashPatern(lineType);
            this.Scale(scale);
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
        /// Gets the type chosen.
        /// </summary>
        public eLineTypes Type
        {
            get
            {
                return this.lineType;
            }
        }

        /// <summary>
        /// Convers the ESADS.EGraphics.eLineType struct to ESADS.EGraphics.eLineTypes enumeration.
        /// </summary>
        /// <param name="lineType"></param>
        /// <returns></returns>
        public static implicit operator eLineTypes(eLineType lineType)
        {
            return lineType.lineType;
        }

        /// <summary>
        /// Sets the type of the line for this Color. 
        /// </summary>
        /// <param name="LineType">Type of line.</param>
        public void SetLineType(eLineTypes LineType)
        {
            float zoomState = 1;
            this.changeBy = eChangeBy.ByObject;
            if (dashPatern != null)
                zoomState = this.dashPatern[1] / GetDashPatern(lineType)[1];
            this.lineType = LineType;
            this.dashPatern = GetDashPatern(this.lineType);
            if (dashPatern != null)
                Scale(zoomState);
        }

        /// <summary>
        /// Gets the DashPatern for this line type.
        /// </summary>
        public float[] DashPatern
        {
            get { return dashPatern; }
        }

        /// <summary>
        /// Scales the dash patern of this line type by the specified scale factor.
        /// </summary>
        /// <param name="factor">The scale factor.</param>
        public void Scale(float factor)
        {
            if (dashPatern == null)
                return;
            for (int i = 0; i < dashPatern.Length; i++)
            {
                if (!(((lineType == eLineTypes.DotDot) && (i == 0)) || ((lineType == eLineTypes.DashDot) && (i == 2))))
                    dashPatern[i] *= factor;
            }
        }

        /// <summary>
        /// Returns the dash patern used for different line types.
        /// </summary>
        /// <param name="lineType">Line startPoint for which dash patern is going to be generated</param>
        /// <returns></returns>
        private  float[] GetDashPatern(eLineTypes lineType)
        {
            switch (lineType)
            {
                case eLineTypes.Continuous:
                    return null;
                case eLineTypes.Dashed:
                    return new float[2] { 10, 10 };
                case eLineTypes.Center:
                    return new float[4] { 20, 4, 4, 4 };
                case eLineTypes.DashDot:
                    return new float[4] { 10, 10, 1, 10 };
                case eLineTypes.DotDot:
                    return new float[2] { 1, 10 };
                default:
                    return null;
            }
        }
    }
}
