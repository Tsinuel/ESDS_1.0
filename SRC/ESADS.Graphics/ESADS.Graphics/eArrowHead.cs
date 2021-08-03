using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Represents the small triangular shape put to indicate the end of an arrow.
    /// </summary>
    [Serializable]
    public class eArrowHead : eIDrawing
    {
        #region Fields
 
        /// <summary>
        /// Holds the value of the 'location' property.
        /// </summary>
        private PointF location;
        /// <summary>
        /// Holds the value of the 'Rotation' property.
        /// </summary>
        private float rotation;
        /// <summary>
        /// Holds the value of the 'Width' property.
        /// </summary>
        private float width;
        /// <summary>
        /// Holds the value of the 'Height' property.
        /// </summary>
        private float height;
        /// <summary>
        /// The three points of the arrow head corner.
        /// </summary>
        private PointF[] points;
        /// <summary>
        /// Holds the value of the 'Layer' property.
        /// </summary>
        private eLayer layer;
        /// <summary>
        /// Holds a value for property 'Color'.
        /// </summary>
        private eColor color;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates new instance of the eArrowHead to serve as a triangular arrowhead. The arrow points to the left with a rotation of 0 degree.
        /// </summary>
        /// <param name="location">The start of the head of the arrow head.</param>
        /// <param name="rotation">The angle, measured in degrees by which the rotation of the body of the arrow head taking the start as the center. The angle is measured from the positive x-axis, counterclockwise positive.</param>
        /// <param name="width">The base width of the arrow head.</param>
        /// <param name="height">The height of the arrow, i.e. the distance from the arrow tip to the mid point of the base.</param>
        /// <param name="layer">The layer on which to draw the arrow head.</param>
        public eArrowHead(PointF location, float rotation, float width, float height, eLayer layer)
        {
            this.location = location;
            this.rotation = rotation;
            this.width = width;
            this.height = height;
            this.layer = layer;
            this.color = layer.Color;
            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified);
            SetCorners();
        }
        /// <summary>
        /// Creates new instance of the eArrowHead to serve as a triangular arrowhead. The arrow points to the left with a rotation of 0 degree.
        /// </summary>
        /// <param name="location">The start of the head of the arrow head.</param>
        /// <param name="rotation">The angle, measured in degrees by which the rotation of the body of the arrow head taking the start as the center. The angle is measured from the positive x-axis, counterclockwise positive.</param>
        /// <param name="size">The size of the arrow.</param>
        /// <param name="layer">The layer on which to draw the arrow head.</param>
        public eArrowHead(System.Drawing.PointF location, float rotation, float size, ESADS.EGraphics.eLayer layer)
        {
            this.location = location;
            this.rotation = rotation;
            this.width = 0.83f * size;
            this.height = 2.5f * size;
            this.layer = layer;
            this.color = layer.Color;
            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified);
            SetCorners();
        }

   
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the start of the head of the arrow head.
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
                SetCorners();
            }
        }

        /// <summary>
        /// Gets or sets the angle, measured in degrees by which the rotation of the body of the arrow head taking the start as the center. The angle is measured from the positive x-axis, counter clockwise positive.
        /// </summary>
        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
                SetCorners();
            }
        }

        /// <summary>
        /// Gets or sets the base width of the arrow head.
        /// </summary>
        public float Width
        {
            get
            {
                return width;
            }
            set
            {
                if (value > 0)
                    width = value;
                else
                    throw new eGraphicsException("An arrow head base width cannot be zero or negative.");
                SetCorners();
            }
        }

        /// <summary>
        /// Gets or sets the height of the arrow, i.e. the distance from the arrow tip to the mid point of the base.
        /// </summary>
        public float Height
        {
            get
            {
                return height;
            }
            set
            {
                if (value > 0)
                    height = value;
                else
                    throw new eGraphicsException("An arrow head height cannot be zero or negative.");
                SetCorners();
            }
        }

        /// <summary>
        /// Gets or setst the layer on which the arrow head is to be drawn on.
        /// </summary>
        public eLayer Layer
        {
            get
            {
                return layer;
            }
            set
            {
                layer = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the ArrowHead.
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
        #endregion

        #region Method
        /// <summary>
        /// Zooms the arrow head with respect to the zoom center give.
        /// </summary>
        /// <param name="ZoomCenter">The point from which the arrow head is to be zoomed.</param>
        /// <param name="ZoomFactor">The scale factor to zoom the arrowhead with.</param>
        public void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].X = ZoomFactor * (points[i].X - ZoomCenter.X) + ZoomCenter.X;
                points[i].Y = ZoomFactor * (points[i].Y - ZoomCenter.Y) + ZoomCenter.Y;
            }

            location.X = ZoomFactor * (location.X - ZoomCenter.X) + ZoomCenter.X;
            location.Y = ZoomFactor * (location.Y - ZoomCenter.Y) + ZoomCenter.Y;

            width *= ZoomFactor;
            height *= ZoomFactor;
        }

        /// <summary>
        /// Pans the arrow head with the given x and y offset.
        /// </summary>
        /// <param name="Xoffset">Amount of offset in x direction.</param>
        /// <param name="Yoffset">Amount of offset in y direction.</param>
        public void Pan(float Xoffset, float Yoffset)
        {
            for (int i = 0; i < this.points.Length; i++)
            {
                this.points[i].X += Xoffset;
                this.points[i].Y += Yoffset;
            }

            location.X += Xoffset;
            location.Y += Yoffset;
        }

        /// <summary>
        /// Sets the corner points of an arrow head based on the current height, width, start and rotation.
        /// </summary>
        private void SetCorners()
        {
            PointF p1 = new PointF();
            PointF p2 = new PointF();
            float theta = rotation * (float)Math.PI / 180.0f;

            p1.X = (float)(location.X + height * Math.Cos(theta) + (width / 2.0) * Math.Sin(theta));
            p1.Y = (float)(location.Y + height * Math.Sin(theta) - (width / 2.0) * Math.Cos(theta));
            p2.X = (float)(location.X + height * Math.Cos(theta) - (width / 2.0) * Math.Sin(theta));
            p2.Y = (float)(location.Y + height * Math.Sin(theta) + (width / 2.0) * Math.Cos(theta));

            this.points = new PointF[] { location, p1, p2 };
        }

        /// <summary>
        /// Draws the arrow head on the given graphics object.
        /// </summary>
        /// <param name="g">The graphics object on which to draw the arrow head.</param>
        public void Draw(Graphics g)
        {
            SolidBrush b = new SolidBrush(this.color);
            g.FillPolygon(b, this.points);
        }
        /// <summary>
        /// Event handler for ESADS.EGraphics.eLayer.Modifeid event;
        /// </summary>
        /// <param name="sender">The layer sending this event.</param>
        /// <param name="e">Event argument containing all the necessary information related to the event.</param>
        private void layer_Modified(eLayer sender, eLayerModifiedEventArgs e)
        {
            if (this.color.ChangeBy == eChangeBy.ByLayer)
                this.color.SetColor(e.Color);
        }
        #endregion

        /// <summary>
        /// Gets the region enbounded by the arrow head.
        /// </summary>
        public Region GetRegion()
        {
            GraphicsPath p = new GraphicsPath();
            p.AddLines(points);
            return new Region(p);
        }
    }
}
