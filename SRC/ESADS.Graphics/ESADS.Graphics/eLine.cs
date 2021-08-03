using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Represents a line drawing object.
    /// </summary>
    [Serializable]
    public class eLine : eIDrawing
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
        /// Holds the value of the 'TopLeft' property.
        /// </summary>
        private PointF location;
        /// <summary>
        /// Holds the value of the 'BottomRight' property.
        /// </summary>
        private PointF end;
        /// <summary>
        /// Accesses the public property 'Layer'.
        /// </summary>
        private eLayer layer;
        /// <summary>
        /// Hold the value for property 'ID'.
        /// </summary>
        private string id;
        private bool zoomDashPattern;
        /// <summary>
        /// Value of the 'Visible' property
        /// </summary>
        private bool visible = true;

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the ID of this object.
        /// </summary>
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// Gets or sets the start point of the lineType.
        /// </summary>
        public PointF Location
        {
            get
            {
                return location;
            }
            set
            {
                end.X += value.X - location.X;
                end.Y += value.Y - location.Y;
                location = value;
            }
        }

        /// <summary>
        /// Gets or sets the end point of the lineType.
        /// </summary>
        public PointF End
        {
            get
            {
                return end;
            }
            set
            {
                end = value;
            }
        }

        /// <summary>
        /// Gets the length of the lineType
        /// </summary>
        public float Length
        {
            get
            {
                return (float)Math.Sqrt(Math.Pow(location.X - end.X, 2) + Math.Pow(location.Y - end.Y, 2));
            }
        }

        /// <summary>
        /// Gets the layer of this line.
        /// </summary>
        public eLayer Layer
        {
            get { return layer; }
        }

        /// <summary>
        /// Gets or sets the color of the Line.
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
        /// Gets or sets the line type of the Line.
        /// </summary>
        public eLineType LineType
        {
            get { return lineType; }
            set
            {
                lineType.ChangeBy = eChangeBy.ByObject;
                lineType = value;
            }
        }

        /// <summary>
        /// Gets or sets the line weight of the Line.
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

        /// <summary>
        /// Gets or sets the value whether to zoom the dash patterns like a drawing.
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

        /// <summary>
        /// Gets or sets the value if the line is visible.
        /// </summary>
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eLine class given start point, end point and layer.
        /// </summary>
        /// <param name="start">TopLeft point or start of the lineType.</param>
        /// <param name="end">BottomRight point of the lineType.</param>
        /// <param name="layer">Layer of the lineType.</param>
        public eLine(PointF start, PointF end,eLayer layer,eChangeBy changeBy = eChangeBy.ByLayer)
        {
            this.location = start;
            this.end = end;
            this.lineType = layer.LineType;
            this.lineWeigth = layer.LineWeight;
            this.color = layer.Color;
            this.id = "";
            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified);
        }

        
        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eLine class given start point, end point and layer.
        /// </summary>
        /// <param name="start">TopLeft point or start of the lineType.</param>
        /// <param name="end">BottomRight point of the lineType.</param>
        /// <param name="layer">Layer of the lineType.</param>
        public eLine(PointF start, PointF end, eLayer layer)
        {
            this.location = start;
            this.end = end;
            this.lineType = layer.LineType;
            this.lineWeigth = layer.LineWeight;
            this.color = layer.Color;
            this.layer = layer;
            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified);
        }

        #endregion

        #region Methods
        /// <summary>
        /// Zooms the the lineType on which it is called for specified ZoomCenter and ZoomFactor.
        /// </summary>
        /// <param name="ZoomCenter">The zoom center from which the zooming is done.</param>
        /// <param name="ZoomFactor">The zoom factor by which the lineType is elarged.</param>
        public void Zoom(PointF ZoomCenter,float ZoomFactor)
        {
            this.location.X = ZoomFactor * (this.location.X - ZoomCenter.X)+ZoomCenter.X;
            this.location.Y = ZoomFactor * (this.location.Y - ZoomCenter.Y)+ZoomCenter.Y;
            this.end.X = ZoomFactor * (this.end.X - ZoomCenter.X)+ZoomCenter.X;
            this.end.Y = ZoomFactor * (this.end.Y - ZoomCenter.Y)+ZoomCenter.Y;
            if (this.layer.LineType != eLineTypes.Continuous && zoomDashPattern)
                this.lineType.Scale(ZoomFactor);
        }

        /// <summary>
        /// Pans the line by the spcified offsets in x and y axis.
        /// </summary>
        /// <param name="XOffset">The x-distance by which the drawing is going to be moved.</param>
        /// <param name="YOffset">The y-distance by which the drawing is going to be moved.</param>
        public void Pan(float XOffset, float YOffset)
        {
            this.location.X += XOffset;
            this.location.Y += YOffset;
            this.end.X += XOffset;
            this.end.Y += YOffset;
        }

        /// <summary>
        /// Draws the line on which it is called.
        /// </summary>
        /// <param name="g">Graphic object on which the drawing is going to be done.</param>
        public void Draw(Graphics g)
        {
            if (!visible)
                return;
            Pen p = new Pen(this.color, this.lineWeigth);
            if (this.lineType != eLineTypes.Continuous)
                p.DashPattern = this.lineType.DashPatern;
            g.DrawLine(p, location, end);
        }

        /// <summary>
        /// Returns the distance between two points.
        /// </summary>
        /// <param name="p1">TopLeft point.</param>
        /// <param name="p2">BottomRight point.</param>
        /// <returns></returns>
        public static float GetLength(PointF p1, PointF p2)
        {
            return (float)Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2));
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
            if (this.lineWeigth.ChangeBy == eChangeBy.ByLayer)
                this.lineWeigth.SetLineWeight(e.LineWeight);
            if (this.lineType.ChangeBy == eChangeBy.ByLayer)
                this.lineType.SetLineType(e.LineType);
        }

        /// <summary>
        /// Offset this line by the given offset distances in both axises.
        /// </summary>
        /// <param name="xOffSet">The x-Offset distance.</param>
        /// <param name="yOffset">The y-Offset distance.</param>
        /// <returns></returns>
        public eLine OffSet(float xOffSet, float yOffset)
        {
            return new eLine (new PointF(location.X + xOffSet, location.Y + yOffset), new PointF(end.X + xOffSet, end.Y + yOffset),this.layer);
        }

        /// <summary>
        /// Returns the region of this line for the specified region thickness.
        /// </summary>
        /// <param name="thickness">Thickness of the region.</param>
        /// <returns></returns>
        public Region GetRegion(float thickness)
        {
            GraphicsPath gp = new GraphicsPath();
            double teta;
            float Dx, Dy;
            PointF p1, p2, p3, p4;
            try
            {
                teta = Math.Atan((end.Y - location.Y) / (end.X - location.X));
            }
            catch(DivideByZeroException)
            {
                teta = (Math.PI / 2) * Math.Sign(end.Y - location.Y);
            }
            Dx = (float)(thickness * Math.Sqrt(2) / 2 * Math.Cos(Math.PI / 4 + teta));
            Dy = (float)(thickness * Math.Sqrt(2) / 2 * Math.Sin(Math.PI / 4 + teta));
            p1 = new PointF(end.X + Dx, end.Y + Dy);
            p3 = new PointF(location.X - Dx, location.Y - Dy);           
            Dx = (float)(thickness * Math.Sqrt(2) / 2 * Math.Sin(Math.PI / 4 + teta));
            Dy = (float)(thickness * Math.Sqrt(2) / 2 * Math.Cos(Math.PI / 4 + teta));
            p4 = new PointF(end.X + Dx, end.Y - Dy);
            p2 = new PointF(location.X - Dx, location.Y + Dy); 
            gp.AddLine(p1, p2);
            gp.AddLine(p2, p3);
            gp.AddLine(p3, p4);
            gp.AddLine(p4, p1);
            return new Region(gp);
        }
        #endregion
    }
}
