using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Represents a circular drawing object.
    /// </summary>
    [Serializable]
    public class eCircle : eIDrawing
    {
        #region Fields
        /// <summary>
        /// Hold a value for  property 'ZoomDashPatern'.
        /// </summary>
        private bool zoomDashPatern;
        /// <summary>
        /// Accesses the public property 'Radiu'.
        /// </summary>
        private float radius;
        /// <summary>
        /// Accesses the public property 'location'.
        /// </summary>
        private PointF location;
        /// <summary>
        /// Accesses the public property 'Layer'.
        /// </summary>
        private eLayer layer;
        /// <summary>
        /// Accesses the public property 'NegFillColor'.
        /// </summary>
        private eColor fillColor;
        /// <summary>
        /// Accesses the public property 'DrawType'.
        /// </summary>
        private eDrawType drawType;
        /// <summary>
        /// Holds a value for public property 'HatchStyle'.
        /// </summary>
        private HatchStyle hatchStyle;
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
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the value indicating wheter the dash patern can be zoomed or not.
        /// </summary>
        public bool ZoomDashPatern
        {
            get { return zoomDashPatern; }
            set { zoomDashPatern = value; }
        }
        /// <summary>
        /// Gets or sets the radiu of the circle.
        /// </summary>
        public float Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
            }
        }

        /// <summary>
        /// Gets or sets the start of the center of the circle.
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
        /// Gets the layer of the circle.
        /// </summary>
        public eLayer Layer
        {
            get
            {
                return layer;
            }
        }

        /// <summary>
        /// Gets the drawing startPoint of the circle. i.e Draw or Fill.
        /// </summary>
        public eDrawType DrawType
        {
            get
            {
                return drawType;
            }
            set
            {
                drawType = value;
            }
        }

        /// <summary>
        /// Gets or sets the fill color of the circle.
        /// </summary>
        public eColor FillColor
        {
            get
            {
                return fillColor;
            }
            set
            {
                fillColor.SetColor(value);
            }
        }

        /// <summary>
        /// Gets or sets the hatch style of the circle.
        /// </summary>
        public HatchStyle HatchStyle
        {
            get { return hatchStyle; }
            set { hatchStyle = value; }
        }

        /// <summary>
        /// Gets or sets the color of the Circle.
        /// </summary>
        public eColor Color
        {
            get { return color; }
            set
            {
                color.ChangeBy = eChangeBy.ByObject;
                color.SetColor(value.Value);
            }
        }

        /// <summary>
        /// Gets or sets the line type of the Circle.
        /// </summary>
        public eLineType LineType
        {
            get { return lineType; }
            set
            {
                lineType.ChangeBy = eChangeBy.ByObject;
                lineType.SetLineType(value);
            }
        }

        /// <summary>
        /// Gets or sets the line weight of the Circle.
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

        #region Constructors

        /// <summary>
        /// Creaters an instance of ESADS.EGraphics.eCircle class from all the necessary parameters.
        /// </summary>
        /// <param name="location">The coordinate of the center of the circle.</param>
        /// <param name="radius">radius of the circle.</param>
        /// <param name="drawType">The draw startPoint of the circle,i.e. Draw or fill.</param>
        ///<param name="hatchStyle">The hatch style if the circle is needed to be hatched.</param>
        /// <param name="layer">layer of the circle.</param>
        public eCircle(PointF location, float radius, eDrawType drawType,HatchStyle hatchStyle, eLayer layer)
        {
            this.location = location;
            this.fillColor = layer.Color;
            this.color = layer.Color;
            this.lineType = layer.LineType;
            this.lineWeigth = layer.LineWeight;
            this.drawType = drawType;
            this.hatchStyle = hatchStyle;
            this.layer = layer;
            this.radius = radius;
            this.zoomDashPatern = true;
            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified); 
        }
    
        /// <summary>
        /// Creaters an instance of ESADS.EGraphics.eCircle class from all the necessary parameters.
        /// </summary>
        /// <param name="location">The coordinate of the center of the circle.</param>
        /// <param name="radius">radius of the circle.</param>
        /// <param name="drawType">The draw startPoint of the circle,i.e. Draw or fill.</param>
        /// <param name="layer">layer of the circle.</param>
        public eCircle(PointF location, float radius, eDrawType drawType,eLayer layer)
        {
            this.location = location;
            this.radius = radius;
            this.drawType = drawType;
            this.layer = layer;
            this.fillColor = layer.Color;
            this.color = layer.Color;
            this.lineType = layer.LineType;
            this.lineWeigth = layer.LineWeight;
            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified);
            this.zoomDashPatern = true;
        }
       
        /// <summary>
        /// Creaters an instance of ESADS.EGraphics.eCircle class from all the necessary parameters for drawing mode.
        /// </summary>
        /// <param name="location">The coordinate of the center of the circle.</param>
        /// <param name="radius">radius of the circle.</param>
        /// <param name="layer">layer of the circle.</param>
        public eCircle(PointF location, float radius, eLayer layer)
        {
            this.location = location;
            this.radius = radius;
            this.fillColor = layer.Color;
            this.drawType  = eDrawType.Draw;
            this.layer = layer;
            this.fillColor = layer.Color;
            this.color = layer.Color;
            this.lineType = layer.LineType;
            this.lineWeigth = layer.LineWeight;
            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified);
            this.zoomDashPatern = true;
        }

       
        #endregion

        #region Methods
        /// <summary>
        /// Zooms the circle on which it is called.
        /// </summary>
        /// <param name="ZoomCenter">The zoom center from which zooming is done.</param>
        /// <param name="ZoomFactor">The factor by which the drawing is elarged.</param>
        public void Zoom( PointF ZoomCenter, float ZoomFactor)
        {
            this.location.X = ZoomFactor * (this.location.X - ZoomCenter.X) + ZoomCenter.X;
            this.location.Y = ZoomFactor * (this.location.Y - ZoomCenter.Y) + ZoomCenter.Y;
            radius *= ZoomFactor;
            if (this.layer.LineType != eLineTypes.Continuous&&zoomDashPatern)
                this.lineType.Scale(ZoomFactor);
            
        }

        /// <summary>
        /// Pans or moves the circle by the specifeid offesets in both axis.
        /// </summary>
        /// <param name="XOffset">The distance moved in x-direction</param>
        /// <param name="YOffset">The distance moved in y-direction</param>
        public void Pan(float XOffset, float YOffset)
        {
            this.location.X += XOffset;
            this.location.Y += YOffset;
        }

        /// <summary>
        /// Draws the circle on which it is called.
        /// </summary>
        /// <param name="g">The graphic object on which drawing is done.</param>
        public void Draw(Graphics g)
        {
            Pen p = new Pen(this.color.Value, this.lineWeigth);
            if (this.lineType != eLineTypes.Continuous)
                p.DashPattern = lineType.DashPatern;
            switch (drawType)
            {
                case eDrawType.Draw:
                    g.DrawEllipse(p, new RectangleF(location.X - radius, location.Y - radius, 2 * radius, 2 * radius));
                    break;
                case eDrawType.Fill:
                    g.FillEllipse(new SolidBrush(this.fillColor), new RectangleF(location.X - radius, location.Y - radius, 2 * radius, 2 * radius));
                    break;
                case eDrawType.FillAndDraw:
                    g.FillEllipse(new SolidBrush(this.fillColor), new RectangleF(location.X - radius, location.Y - radius, 2 * radius, 2 * radius));
                    g.DrawEllipse(p, new RectangleF(location.X - radius, location.Y - radius, 2 * radius, 2 * radius));
                    break;
                case eDrawType.Hatch:
                    g.FillEllipse(new HatchBrush(hatchStyle, fillColor), new RectangleF(location.X - radius, location.Y - radius, 2 * radius, 2 * radius));
                    break;
                case eDrawType.HatchAndDraw:
                    g.FillEllipse(new HatchBrush(hatchStyle, fillColor,System.Drawing.Color.Transparent), new RectangleF(location.X - radius, location.Y - radius, 2 * radius, 2 * radius));
                    g.DrawEllipse(p, new RectangleF(location.X - radius, location.Y - radius, 2 * radius, 2 * radius));
                    break;
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
        /// <summary>
        /// Creates a region for this cirle with the specified thickness.
        /// </summary>
        /// <param name="thickness">The thickness of the region.</param>
        /// <returns></returns>
        public Region GetRegion(float thickness)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(location.X - (radius + thickness / 2), location.Y - (radius + thickness / 2), 2 * (radius + thickness / 2), 2 * (radius + thickness / 2));
            Region bigReg = new Region(gp);
            gp = new GraphicsPath();
            gp.AddEllipse(location.X - (radius - thickness / 2), location.Y - (radius - thickness / 2), 2 * (radius - thickness / 2), 2 * (radius - thickness / 2));
            Region smalReg = new Region(gp);
            bigReg.Xor(smalReg);
            return bigReg;
        }

        #endregion
    }
}
