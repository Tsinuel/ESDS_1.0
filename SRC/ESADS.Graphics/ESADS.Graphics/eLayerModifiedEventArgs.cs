using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Contais all information related the changed layer.
    /// </summary>
    public class eLayerModifiedEventArgs:EventArgs
    {
        #region Fields
        /// <summary>
        /// Holds a value for public property 'Color'.
        /// </summary>
        private eColor color;
        /// <summary>
        /// Holds a value for public property 'Color'.
        /// </summary>
        private eLineTypes lineType;
        /// <summary>
        /// Holds a value for public property 'LineWeght'.
        /// </summary>
        private eLineWeight lineWeight;
        /// <summary>
        /// Holds a value for public property 'Font'.
        /// </summary>
        private eTextStyle  textStyle;
        #endregion
        /// <summary>
        /// Value of 'PinPoint'
        /// </summary>
        private PointF pinPoint;
        /// <summary>
        /// Holds the value of the 'ZoomFactor'.
        /// </summary>
        private float zoomFactor;

        #region Constructor

        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eLayerChangedEventArgs class from all the necessary parameters.
        /// </summary>
        /// <param name="color">Color of the layer under going the change.</param>
        /// <param name="lineType">Color of the layer under going the change.</param>
        /// <param name="lineWeight">LineWeight of the layer under going the change.</param>
        /// <param name="textStyle">Font of the layer under going the change.</param>
        public eLayerModifiedEventArgs(eColor color, eLineType lineType, eLineWeight lineWeight, eTextStyle textStyle)
        {
            this.color = color;
            this.lineType = lineType;
            this.lineWeight = lineWeight;
            this.textStyle = textStyle;
        }
        #endregion

        /// <summary>
        /// Creates an event argument for 'Layer.TurnedOn' event.
        /// </summary>
        /// <param name="pinPoint">The pin point of the layer.</param>
        /// <param name="MasterZoomFactor">The zoom factor of the layer.</param>
        public eLayerModifiedEventArgs(PointF pinPoint, float zoomFactor)
        {
            this.pinPoint = pinPoint;
            this.zoomFactor = zoomFactor;
        }

        #region Properties
        /// <summary>
        /// Gets the color of the layer which  undergo the change.
        /// </summary>
        public eColor Color
        {
            get
            {
                return color;
            }

        }

        /// <summary>
        /// Gets the line weight of the layer which  undergo the change.
        /// </summary>
        public eLineTypes LineType
        {
            get
            {
                return lineType;
            }

        }

        /// <summary>
        /// Gets the line weightof the layer which  undergo the change.
        /// </summary>
        public eLineWeight LineWeight
        {
            get
            {
                return lineWeight;
            }
        }

        /// <summary>
        /// Gets the TextStyle of the layer which  undergo the change.
        /// </summary>
        public eTextStyle TextStyle
        {
            get
            {
                return textStyle;
            }
        }
        #endregion

        /// <summary>
        /// Gets the pin point of the layer that is being turned on.
        /// </summary>
        public PointF PinPoint
        {
            get
            {
                return pinPoint;
            }
        }

        /// <summary>
        /// Gets the zoom factor that the layer has been zoomed by through its history.
        /// </summary>
        public float ZoomFactor
        {
            get
            {
                return zoomFactor;
            }
        }
    }
}
