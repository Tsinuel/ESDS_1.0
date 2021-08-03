using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Represents ESADS spline curve.
    /// </summary>
    public class eCurve : eIDrawing
    {
        #region Fields
        /// <summary>
        /// Holds  a value for property 'Color'.
        /// </summary>
        private eColor color;
        /// <summary>
        /// Holds  a value for property 'Layer'.
        /// </summary>
        private eLayer layer;
        /// <summary>
        /// Holds  a value for property 'location'.
        /// </summary>
        private PointF location;
        /// <summary>
        /// Holds  a value for property 'LineType'.
        /// </summary>
        private eLineType lineType;
        /// <summary>
        /// Holds  a value for property 'LineWeight'.
        /// </summary>
        private eLineWeight lineWeight;
        /// <summary>
        /// Holds  a value for property 'Points'.
        /// </summary>
        private PointF[] points;

        #endregion

        #region Porperties
        /// <summary>
        /// Gets or sets the location of the spline.
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

        /// <summary>
        /// Gets the layerof the spline.
        /// </summary>
        public eLayer Layer
        {
            get { return layer; }
        }

        /// <summary>
        /// Gets or sets the color of the spline.
        /// </summary>
        public eColor Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        /// <summary>
        /// Gets or sets the line type of the spline.
        /// </summary>
        public eLineType LineType
        {
            get
            {
                return lineType;
            }
            set
            {
                lineType = value;
            }
        }

        /// <summary>
        /// Gets or sets the line weight of the spline.
        /// </summary>
        public eLineWeight LineWeight
        {
            get
            {
                return lineWeight;
            }
            set
            {
                lineWeight = value;
            }
        }

        /// <summary>
        /// Gets or sets the point which are connected by the spline.
        /// </summary>
        public PointF[] Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eSpline class from the given basic parameters.
        /// </summary>
        /// <param name="points">  Array of System.Drawing.Point structures that represent the points that determine.</param>
        /// <param name="layer">Layer on which the spline is drawn.</param>
        public eCurve(PointF[] points, eLayer layer)
        {
            this.points = points;
            this.location = points[0];
            this.layer = layer;
            this.color = layer.Color;
            this.lineType = layer.LineType;
            this.lineWeight = layer.LineWeight;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Zooms the spline from the specified zoom center by the given zoom factor.
        /// </summary>
        /// <param name="ZoomCenter">The zoom center from which zooming is done.</param>
        /// <param name="ZoomFactor">The factor by which the drawing is elarged.</param>
        public void Zoom(System.Drawing.PointF ZoomCenter, float ZoomFactor)
        {
            this.location.X = ZoomFactor * (this.location.X - ZoomCenter.X) + ZoomCenter.X;
            this.location.Y = ZoomFactor * (this.location.Y - ZoomCenter.Y) + ZoomCenter.Y;           
            for (int i = 0; i < points.Length; i++)
            {
                this.points[i].X = ZoomFactor * (points[i].X - ZoomCenter.X) + ZoomCenter.X;
                this.points[i].Y = ZoomFactor * (this.points[i].Y - ZoomCenter.Y) + ZoomCenter.Y;
            } 
            if (this.layer.LineType != eLineTypes.Continuous)
                this.lineType.Scale(ZoomFactor);
        }

        /// <summary>
        /// Pans or moves the spline by the specifeid offesets in both axis.
        /// </summary>
        /// <param name="XOffset">The distance moved in x-direction</param>
        /// <param name="YOffset">The distance moved in y-direction</param>
        public void Pan(float Xoffset, float Yoffset)
        {
            this.location.X += Xoffset;
            this.location.Y += Yoffset;
            for (int i = 0; i < points.Length; i++)
            {
                this.points[i].X += Xoffset;
                this.points[i].Y += Yoffset;
            }
        }

        /// <summary>
        /// Draws this spline.
        /// </summary>
        /// <param name="g">The graphics object on which the drawing is done.</param>
        public void Draw(Graphics g)
        {
            Pen p = new Pen(this.color, this.lineWeight);
            if (this.lineType != eLineTypes.Continuous)
                p.DashPattern = this.lineType.DashPatern;
            g.DrawCurve(p, this.points,0.2f);
        }
        #endregion

    }
}
