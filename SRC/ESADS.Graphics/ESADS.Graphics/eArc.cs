using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Represents portion of a circle or an ellipse, may be tilted at an angle.
    /// </summary>
    [Serializable]
    public class eArc : eIDrawing
    {
        #region Fields
        /// <summary>
        /// Holds a value for property 'Color'.
        /// </summary>
        private eColor color;
        /// <summary>
        /// Holds a value for property 'LineType'.
        /// </summary>
        private eLineType lineType;
        /// <summary>
        /// Holds a value for property 'LineWeigth'.
        /// </summary>
        private eLineWeight lineWeigth;
        /// <summary>
        /// Holds the value of the 'Layer' property.
        /// </summary>
        private eLayer layer;
        /// <summary>
        /// Holds the value of the 'startAngle' property.
        /// </summary>
        private float startAngle;
        /// <summary>
        /// Holds the value of the 'sweepAngle' property.
        /// </summary>
        private float sweepAngle;
        /// <summary>
        /// The rectangle that bears the ellipse
        /// </summary>
        private RectangleF rectangle;
        /// <summary>
        /// The clockwise angle by which the arc is to be tited.
        /// </summary>
        private float rotation;
        /// <summary>
        /// Holds the value of the 'RotationCenter' property.
        /// </summary>
        private PointF rotationCenter;
        /// <summary>
        /// the start point of the arc.
        /// </summary>
        private PointF start;
        /// <summary>
        /// the end point of the arc.
        /// </summary>
        private PointF end;
        /// <summary>
        /// Holds the value of the 'Curvature' property.
        /// </summary>
        private float cuvature;
        #endregion
        /// <summary>
        /// value of 'ZoomDashPattern'
        /// </summary>
        private bool zoomDashPattern;

        #region Constructors
        /// <summary>
        /// Creates a circular arc given its center, radius, start angle and sweep angle.
        /// </summary>
        /// <param name="center">The center point of the arc.</param>
        /// <param name="radius">The radius of the arc.</param>
        /// <param name="startAngle">The clockwise angle measured from the positive x-axis.</param>
        /// <param name="sweepAngle">The clockwise angle measured form the start point.</param>
        /// <param name="layer">The layer on which to draw the arc on.</param>
        public eArc(PointF center, float radius, float startAngle, float sweepAngle, eLayer layer)
        {
            this.startAngle = startAngle;
            this.sweepAngle = sweepAngle;
            this.rectangle = new RectangleF(center.X - radius, center.Y - radius, radius * 2, radius * 2);
            this.rotation = 0.0f;
            this.rotationCenter = this.Location;
            this.layer = layer;
            this.color = layer.Color;
            this.lineWeigth = layer.LineWeight;

            float x1 = (float)(center.X + radius * Math.Cos(-startAngle * Math.PI / 180.0));
            float y1 = (float)(center.Y - radius * Math.Sin(-startAngle * Math.PI / 180.0));
            float x2 = (float)(center.X + radius * Math.Cos(-(startAngle + sweepAngle) * Math.PI / 180.0));
            float y2 = (float)(center.Y - radius * Math.Sin(-(startAngle + sweepAngle) * Math.PI / 180.0));

            this.start = new PointF(x1, y1);
            this.end = new PointF(x2, y2);

        }

        /// <summary>
        /// Creates an arc that is extracted from an ellipse rotated at an angle.
        /// </summary>
        /// <param name="IncludingRectangle">The rectangle that forms the ellipse from which the arc is cut.</param>
        /// <param name="StartAngle">The clockwise angle measured from the positive x-axis to the start point of the arc.</param>
        /// <param name="SweepAngle">The clockwise angle measured from the start point of the arc to the end point.</param>
        /// <param name="Rotation">The clockwise angle to rotate the whole arc about the center of the ellipse.</param>
        /// <param name="rotationCenter">The center about which to rotate the including rectangle to draw the arc.</param>
        /// <param name="layer">The layer on which to draw the arc.</param>
        /// <param name="lineType">The line style of the line to draw the arc.</param>
        public eArc(RectangleF IncludingRectangle, float StartAngle, float SweepAngle, float Rotation, PointF rotationCenter, eLayer layer, eLineTypes lineType)
        {
            this.rectangle = IncludingRectangle;
            this.startAngle = StartAngle;
            this.sweepAngle = SweepAngle;
            this.rotation = Rotation;
            this.layer = layer;
            this.rotationCenter = rotationCenter;
        }

        /// <summary>
        /// Creates an arc given its start, end and curvature. The arc drawn is always a quarter of an ellipse.
        /// </summary>
        /// <param name="start">The start point of the arc.</param>
        /// <param name="end">The end point of the arc.</param>
        /// <param name="cuvature">The amount of curvature of the arc measured with a scale from 0 for straight to 90 highly curved at the tip.</param>
        /// <param name="layer">The layer on which to draw the arc.</param>
        public eArc(PointF start, PointF end, float cuvature, eLayer layer)
        {
            this.start = start;
            this.end = end;
            this.cuvature = cuvature;

            Refresh();
            this.layer = layer;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the layer on which the arc is drawn.
        /// </summary>
        public eLayer Layer
        {
            get
            { return this.layer; }
            set
            { layer = value; }
        }

        /// <summary>
        /// Gets or sets the center start of the rectangle inscribing the full ellipse.
        /// </summary>
        public PointF Location
        {
            get
            {
                PointF loc = this.rectangle.Location;
                loc.X += this.rectangle.Width / 2;
                loc.Y += this.rectangle.Height / 2;
                return loc;
            }
            set 
            {
                rectangle.X = value.X - rectangle.Width / 2.0f;
                rectangle.Y = value.Y - rectangle.Height / 2.0f;
            }
        }

        /// <summary>
        /// Gets or sets the rotation center if the arc is to be rotated.
        /// </summary>
        public PointF RotationCenter
        {
            get
            { return rotationCenter; }
            set
            { rotationCenter = value; }
        }

        /// <summary>
        /// Gets or sets a clockwise angle to rotate the arc with about the rotation center.
        /// </summary>
        public float Rotation
        {
            get
            { return rotation; }
            set
            { rotation = value; }
        }

        /// <summary>
        /// Gets or sets the start point of the arc. When it is set, it changes a circular arc to an elliptical one.
        /// </summary>
        public PointF Start
        {
            get
            { return start; }
            set
            {
                start = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the end point of the arc.
        /// </summary>
        public PointF End
        {
            get
            { return this.end; }
            set
            {
                this.end = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the curvature value of the arc.
        /// </summary>
        public float Curvature
        {
            get
            { return this.cuvature; }
            set
            {
                this.cuvature = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the color of the arc
        /// </summary>
        public eColor Color
        {
            get { return color; }
            set 
            {
                color.ChangeBy = eChangeBy.ByObject;
                color.SetColor(value);
            }
        }

        /// <summary>
        /// Gets or sets the line type of the arc.
        /// </summary>
        public eLineType LineType
        {
            get { return lineType; }
            set
            {
                lineType.ChangeBy = eChangeBy.ByObject;
                lineType =value;
            }
        }

        /// <summary>
        /// Gets or sets the line weight of the arc.
        /// </summary>
        public eLineWeight LineWeight
        {
            get { return lineWeigth; }
            set 
            {
                lineWeigth.ChangeBy = eChangeBy.ByObject;
                lineWeigth.SetLineWeight(value);
            }
        }
        #endregion

        /// <summary>
        /// Gets the radius of the arc for a circle type arc. For other types it returns zero.
        /// </summary>
        public float Radius
        {
            get
            {
                return rectangle.Height / 2.0f;
            }
        }

        /// <summary>
        /// Gets or set the value whether to zoom the dashes together with the dwg.
        /// </summary>
        public bool ZoomDashPattern
        {
            get
            {
                return zoomDashPattern;
            }
            set
            {
                zoomDashPattern = value;
            }
        }

        #region Methods
        /// <summary>
        /// Recalculates all the necessary components based on the most recent changes.
        /// </summary>
        private void Refresh()
        {
            double alpha = (Math.Abs(this.cuvature) % 90.0) * Math.PI / 180.0;
            if (alpha == 0.0)
                alpha = Math.PI / 180.0;
            if (alpha == Math.PI / 2.0)
                alpha = Math.PI / 2.0 - Math.PI / 180.0;
            double r = Math.Sqrt(Math.Pow(end.X - start.X, 2.0) + Math.Pow(end.Y - start.Y, 2.0));
            double h = 2.0 * r * Math.Cos(alpha);
            double b = 2.0 * r * Math.Sin(alpha);
            double beta = Math.Atan2(end.Y - start.Y, end.X - start.X);
            double theta = (beta + alpha - Math.PI / 2.0) * -1.0;

            PointF loc = new PointF(start.X, start.Y - (float)h / 2.0f);

            this.rectangle = new RectangleF(loc, new SizeF((float)b, (float)h));
            this.rotation = (float)(theta * -180.0 / Math.PI);
            this.rotationCenter = start;
            this.startAngle = 180;
            this.sweepAngle = -90;
        }

        /// <summary>
        /// Zooms the arc given the zoom center and zoom factor.
        /// </summary>
        /// <param name="ZoomCenter">The zoom origin from which all relative coordinates are scaled.</param>
        /// <param name="ZoomFactor">The zoom factor by which to scale the relative coordinates.</param>
        public void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            PointF loc = this.rectangle.Location;
            loc.X = ZoomFactor * (this.rectangle.Location.X - ZoomCenter.X) + ZoomCenter.X;
            loc.Y = ZoomFactor * (this.rectangle.Location.Y - ZoomCenter.Y) + ZoomCenter.Y;

            this.rectangle.Location = loc;
            this.rectangle.Width *= ZoomFactor;
            this.rectangle.Height *= ZoomFactor;

            rotationCenter.X = (rotationCenter.X - ZoomCenter.X) * ZoomFactor + ZoomCenter.X;
            rotationCenter.Y = (rotationCenter.Y - ZoomCenter.Y) * ZoomFactor + ZoomCenter.Y;

            if (this.layer.LineType != eLineTypes.Continuous && zoomDashPattern)
                this.lineType.Scale(ZoomFactor);
        }

        /// <summary>
        /// Pans the arc with the given x and y offsets.
        /// </summary>
        /// <param name="Xoffset">Amount of offset in x direction.</param>
        /// <param name="Yoffset">Amount of offset in y direction.</param>
        public void Pan(float Xoffset, float Yoffset)
        {
            PointF loc = this.rectangle.Location;
            loc.X += Xoffset;
            loc.Y += Yoffset;

            rotationCenter.X += Xoffset;
            rotationCenter.Y += Yoffset;
            this.rectangle.Location = loc;
        }

        /// <summary>
        /// Draws the arc on the given graphics object.
        /// </summary>
        /// <param name="g">The graphics object on which to draw the arc.</param>
        public void Draw(Graphics g)
        {
            Pen p = new Pen(this.color,this.LineWeight);
            if (this.lineType != eLineTypes.Continuous)
                p.DashPattern = this.lineType.DashPatern;
            Matrix m = new Matrix();
            m.RotateAt(this.rotation, this.rotationCenter);
            g.Transform = m;
            g.DrawArc(p, this.rectangle, this.startAngle, this.sweepAngle);
            g.ResetTransform();
        }

        /// <summary>
        /// Sets the arc to a circular arc taking the radius, center, start angle and sweep angle.
        /// </summary>
        /// <param name="Center">The center point of the arc.</param>
        /// <param name="Radius">The radius of the circle from the arc was cut from.</param>
        /// <param name="StartAngle">The clockwise angle measured from the positive x-axis from which the arc starts.</param>
        /// <param name="SweepAngle">The clockwise angle subtended by the arc.</param>
        public void SetCircularArc(PointF Center, float Radius, float StartAngle, float SweepAngle)
        {
            this.startAngle = StartAngle;
            this.sweepAngle = SweepAngle;
            this.rectangle = new RectangleF(Center.X - Radius, Center.Y - Radius, Radius * 2, Radius * 2);
            this.rotation = 0.0f;
        }

        /// <summary>
        /// Sets the arc to an elliptical arc tilted by an angle from the including rectangle, start angle and sweep angle.
        /// </summary>
        /// <param name="IncludingRectangle">The rectangle that forms the ellipse from which the arc is cut.</param>
        /// <param name="StartAngle">The clockwise angle measured from the positive x-axis.</param>
        /// <param name="SweepAngle">The clockwise angle subtended by the arc.</param>
        /// <param name="Rotation">The clockwise angle to rotate the whole arc about the center of the ellipse.</param>
        public void SetEllipticalArc(RectangleF IncludingRectangle, float StartAngle, float SweepAngle, float Rotation)
        {
            this.rectangle = IncludingRectangle;
            this.startAngle = StartAngle;
            this.sweepAngle = SweepAngle;
            this.rotation = Rotation;
        }
        #endregion

        /// <summary>
        /// Gets the region enclosing the arc with a distance given.
        /// </summary>
        /// <param name="offset">The offset from the arc to the region boundry.</param>
        public Region GetRegion(float offset)
        {
            GraphicsPath p = new GraphicsPath();
            p.AddArc(new RectangleF(this.rectangle.X - offset, this.rectangle.Y - offset, this.rectangle.Width + 2 * offset, this.rectangle.Height + 2 * offset), this.startAngle, this.sweepAngle);
            try
            {
                p.AddArc(new RectangleF(this.rectangle.X + offset, this.rectangle.Y + offset, this.rectangle.Width - 2 * offset, this.rectangle.Height - 2 * offset), this.startAngle, this.sweepAngle);
            }
            catch { }
            return new Region(p);
        }
    }
}
