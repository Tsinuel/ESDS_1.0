using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Represents any two dimensional closed shape.
    /// </summary>
    [Serializable]
    public class ePolygone : eIDrawing
    {
        #region Fields

        private string id;
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
        /// The corner points of the polygon.
        /// </summary>
        private PointF[] points;
        /// <summary>
        /// Holds the value of the 'location' property.
        /// </summary>
        private PointF location;
        /// <summary>
        /// Holds the value of the 'NegFillColor' property.
        /// </summary>
        private eColor fillColor;
        /// <summary>
        /// Holds the value of the 'Layer' property.
        /// </summary>
        private eLayer layer;
        /// <summary>
        /// Holds the value of the 'HatchStyle' property.
        /// </summary>
        private HatchStyle hatchStyle;
        /// <summary>
        /// Holds the value of the 'DrawType' property.
        /// </summary>
        private eDrawType drawType;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a polygone formed from a number of points. 
        /// </summary>
        /// <param name="points">The corner points of the polygone.</param>
        /// <param name="drawType">The way to drawType the polygon.</param>
        /// <param name="layer">The layer on which to draw the polygone.</param>
        public ePolygone(PointF[] points, eDrawType drawType, eLayer layer)
        {
            if (points == null || points.Length < 3)
                throw new eGraphicsException("At least three points have to be obtained to drawType a polygon.");
            this.layer = layer;
            this.color = layer.Color;
            this.drawType = drawType;
            this.points = (PointF[])points.Clone();
            this.location = points[0];
            this.lineType = layer.LineType;
            this.fillColor = layer.Color;
            this.color = layer.Color;
            this.lineWeigth = layer.LineWeight;
            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified);
        }

        /// <summary>
        /// Creates a polygone inscribed in a circle having and having a number of sides.
        /// </summary>
        /// <param name="center">The center of the inscribing circle</param>
        /// <param name="radius">The radius of the inscribing circle.</param>
        /// <param name="numberOfSides">The number of sides of the regular polygon.</param>
        /// <param name="drawType">The way to draw the polygon, to fill it or just draw it.</param>
        /// <param name="layer">The layer on which to draw the layer.</param>
        /// <param name="color">The draw color of the polygone.</param>
        /// <param name="fillColor">The color to fill the polygone.</param>
        /// <param name="hatchStyle">The hatch style of the polygone.</param>
        /// <param name="lineType">The line startPoint with which to draw the polygone.</param>
        /// <param name="lineThickness">The thickness of the line.</param>
        public ePolygone(PointF center, float radius, int numberOfSides, eDrawType drawType, eLayer layer, Color color, Color fillColor, HatchStyle hatchStyle, eLineTypes lineType, 
            float lineThickness)
        {
            this.location = center;
            this.points = new PointF[0];
            this.layer = layer;
            this.lineType = layer.LineType;
            this.fillColor = layer.Color;
            this.color = layer.Color;
            this.lineWeigth = layer.LineWeight;
            SetInscribingCircle(center, radius, numberOfSides);

            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified);
        }

        /// <summary>
        /// Creates a polygone inscribed in a circle having and having a number of sides.
        /// </summary>
        /// <param name="center">The center of the inscribing circle</param>
        /// <param name="radius">The radius of the inscribing circle.</param>
        /// <param name="numberOfSides">The number of sides of the regular polygon.</param>
        /// <param name="drawType">The way to draw the polygon, to fill it or just draw it.</param>
        /// <param name="layer">The layer on which to draw the layer.</param>
        public ePolygone(PointF center, float radius, int numberOfSides, eDrawType drawType, eLayer layer)
        {
            this.location = center;
            this.layer = layer;
            this.drawType = drawType;
            this.lineType = layer.LineType;
            this.fillColor = layer.Color;
            this.color = layer.Color;
            this.lineWeigth = layer.LineWeight;
            SetInscribingCircle(center, radius, numberOfSides);
            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified);
        }
        #endregion

        #region Properties
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// Gets the point in this polygon.
        /// </summary>
        public PointF[] Points
        {
            get { return points; }
        }
        /// <summary>
        /// Gets or setsThe center of the polygone
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
        /// Gets or sets the fill color of the polygone.
        /// </summary>
        public eColor FillColor
        {
            get
            {
                return fillColor;
            }
            set
            {
                fillColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the layer on which the polygone is drawn.
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
        /// Gets or sets the way to hatch the area when the drawing is hatched.
        /// </summary>
        public HatchStyle HatchStyle
        {
            get
            {
                return this.hatchStyle;
            }
            set
            {
                this.hatchStyle = value;
            }
        }

        /// <summary>
        /// Gets or sets the way to draw the polygone.
        /// </summary>
        public eDrawType DrawType
        {
            get
            {
                return this.drawType;
            }
            set
            {
                this.drawType = value;
            }
        }
        /// <summary>
        /// Gets or sets the color of the Dimension.
        /// </summary>
        public eColor Color
        {
            get { return color; }
            set
            {
                color.SetColor(value);
            }
        }

        /// <summary>
        /// Gets or sets the line type of the Dimension.
        /// </summary>
        public eLineType LineType
        {
            get { return lineType; }
            set
            {
                lineType.SetLineType(value);
            }
        }

        /// <summary>
        /// Gets or sets the line weight of the Dimension.
        /// </summary>
        public eLineWeight LineWeight
        {
            get { return lineWeigth; }
            set
            {
                lineWeigth.SetLineWeight(value);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Zooms the polygone with a given factor about an origin.
        /// </summary>
        /// <param name="ZoomCenter">The center of scaling.</param>
        /// <param name="ZoomFactor">The scale factor to scale the polygone with.</param>
        public void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].X = ZoomFactor * (points[i].X - ZoomCenter.X) + ZoomCenter.X;
                points[i].Y = ZoomFactor * (points[i].Y - ZoomCenter.Y) + ZoomCenter.Y;
            }
            lineType.Scale(ZoomFactor);
            
        }

        /// <summary>
        /// Pans the polygone with a given x and y offset.
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
        }

        /// <summary>
        /// Draw the polygone given a graphics object.
        /// </summary>
        /// <param name="g">The graphics object on which to draw the polygone.</param>
        public void Draw(Graphics g)
        {
            if (this.drawType == eDrawType.Fill)
                goto B; //Goes directly to fill
            if (this.drawType == eDrawType.Hatch)
                goto C; //Goes directly to hatch
            //Draw
            Pen pen = new Pen(this.color, this.lineWeigth);
            if (lineType != eLineTypes.Continuous)
                pen.DashPattern = this.lineType.DashPatern;
            g.DrawPolygon(pen, this.points);

            if (this.drawType == eDrawType.Draw)
                return;
            if (this.drawType == eDrawType.HatchAndDraw)
                goto C;
        B: //Fill
            SolidBrush b = new SolidBrush(this.fillColor);
            g.FillPolygon(b, this.points);
            return; //Since it has been filled it cannot be hatched.
        C: //Hatch
            HatchBrush h = new HatchBrush(this.hatchStyle, this.color, this.fillColor);
            g.FillPolygon(h, this.points);

        }

        /// <summary>
        /// Fills the corner points of the polygone.
        /// </summary>
        /// <param name="Points">The new corner points of the polygone.</param>
        public void SetPoints(PointF[] Points)
        {
            if (Points == null || Points.Length < 3)
                throw new eGraphicsException("No polygon can be drawn with less than three points.");

            this.points = (PointF[])Points.Clone();

            this.location = points[0];
        }

        /// <summary>
        /// Sets the points to drawType a regular polygon inscribed in a given circle.
        /// </summary>
        /// <param name="Center">The center point of the inscribing circle.</param>
        /// <param name="Radius">the radius of the inscribing circle.</param>
        /// <param name="NumberOfSides">The number of sides of the regular polygone.</param>
        public void SetInscribingCircle(PointF Center, float Radius, int NumberOfSides)
        {
            if (NumberOfSides < 3)
                throw new eGraphicsException("No polygon can have sides less than three.");
            if (Radius < 0)
                throw new eGraphicsException("No circle can have a radius of zero or negative value.");

            this.location = Center;

            this.points = new PointF[NumberOfSides];

            float theta = 0, dtheta, x, y;

            dtheta = (float)(Math.PI * 2) / NumberOfSides;

            for (int i = 0; i < NumberOfSides; i++)
            {
                x = (Radius * (float)Math.Cos(theta)) + Center.X;
                y = (Radius * (float)Math.Sin(theta)) + Center.Y;

                points[i] = new PointF(x, y);

                theta += dtheta;
            }
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
            if (this.fillColor.ChangeBy == eChangeBy.ByLayer)
                this.fillColor.SetColor(e.Color);
            if (this.lineWeigth.ChangeBy == eChangeBy.ByLayer)
                this.lineWeigth.SetLineWeight(e.LineWeight);
            if (this.lineType.ChangeBy == eChangeBy.ByLayer)
                this.lineType.SetLineType(e.LineType);
        }
        #endregion
    }
}
