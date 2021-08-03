using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ESADS.EGraphics
{
    /// <summary>
    ///Represents Rectangular graphics drawing.
    /// </summary>
    [Serializable]
    public class eRectangle : eIDrawing
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
        /// Holds the value of the 'Width' property.
        /// </summary>
        private float width;
        /// <summary>
        /// Holds the value of the 'Height' property.
        /// </summary>
        private float height;
        /// <summary>
        /// Holds the value of the 'location' property.
        /// </summary>
        private PointF location;
        /// <summary>
        /// Holds the value of the 'Draw startPoint' property.
        /// </summary>
        private eDrawType drawType;
        /// <summary>
        /// Holds the value of the 'NegFillColor' property.
        /// </summary>
        private eColor fillColor;
        /// <summary>
        /// Holds the value of the 'Layer' property.
        /// </summary>
        private eLayer layer;  
        /// <summary>
        /// Accesses the public property 'HatchStyle'.
        /// </summary>
        private HatchStyle hatchStyle;
        /// <summary>
        /// Holds the value of the 'Rotation' property.
        /// </summary>
        private float rotation;

        #endregion        

        #region Constructor
        /// <summary>
        /// Creates new instance of ESADS.EGraphics.eRectangle class from all basic parameters.
        /// </summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="location">The start of the top left corner of the rectangle.</param>
        /// <param name="drawType">The drawType startPoint of the rectangle.</param>
        /// <param name="hatchStyle">The hatch Style of the rectangle.</param>
        /// <param name="layer">The layer on which the drawing is done.</param>
        public eRectangle(PointF location, float width, float height,eDrawType drawType,HatchStyle hatchStyle,eLayer layer)
        {
            this.width = width;
            this.height = height;
            this.location = location;
            this.drawType = drawType;
            this.fillColor = layer.Color;
            this.color = layer.Color;
            this.lineWeigth = layer.LineWeight;
            this.lineType = layer.LineType;
            this.hatchStyle = hatchStyle;
            this.layer = layer;
            this.layer.Modified+=new eLayerModifiedEventHandler(layer_Modified);
        }
        /// <summary>
        ///Creates new instance of ESADS.EGraphics.eRectangle class for draw mode.
        /// </summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="location">The start of the top left corner of the rectangle.</param>
        /// <param name="layer">The layer on which the drawing is done.</param>
        public eRectangle(PointF location,float width, float height,eLayer layer)
        {
            this.width = width;
            this.height = height;
            this.location = location;
            this.drawType  = eDrawType.Draw;
            this.layer = layer;
            this.lineType = layer.LineType;
            this.color = layer.Color;
            this.lineWeigth = layer.LineWeight;
            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified);
        }
        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eRectangle class for fill mode.
        /// </summary>
        /// <param name="location">The the top left corner coordinate of the rectangle.</param>
        /// <param name="width">Width of the rectanle.</param>
        /// <param name="height">Height of the rectangle.</param>
        /// <param name="drawType">The drawing mode.</param>
        /// <param name="layer">The layer on which the drawing is done.</param>
        public eRectangle(PointF location,float width, float height,eDrawType drawType,eLayer layer)
        {
            this.width = width;
            this.height = height;
            this.location = location;
            this.drawType = drawType;
            this.fillColor = layer.Color;
            this.layer = layer;
            this.lineType = layer.LineType;
            this.color = layer.Color;
            this.lineWeigth = layer.LineWeight;
            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified);

        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the way the drawing is drawn.
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
        /// Gets or sets the color to fill the rectangle with.
        /// </summary>
        public eColor FillColor
        {
            get
            {
                return this.fillColor;
            }
            set
            {
                this.fillColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the start of the top left corner of the rectangle.
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
        /// Gets or sets the width of the rectangle.
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
                    throw new eGraphicsException("Rectangle can't have zero or negative width");
            }
        }

        /// <summary>
        /// Gets or sets the height of the rectangle.
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
                    throw new eGraphicsException("Rectangle can't have zero or negative height");
            }
        }    
        
        /// <summary>
        /// Gets the layer in which the rectangle is located.
        /// </summary>
        public eLayer Layer
        {
            get
            {
                return layer;
            }
        }     

        /// <summary>
        /// Gets or sets the coordinate of the top right corner.
        /// </summary>
        public PointF TopRight
        {
            get
            {
                return new PointF(location.X + width, location.Y);
            }
        }

        /// <summary>
        /// Gets or sets the coordinate of the bottom left corner.
        /// </summary>
        public PointF BottomLeft
        {
            get
            {
                return new PointF(location.X, location.Y + height);
            }
        }

        /// <summary>
        /// Gets or sets the coordinate of the bottom right.
        /// </summary>
        public PointF BottomRight
        {
            get
            {
                return new PointF(location.X + width, location.Y + height);
            }
        }

        /// <summary>
        /// Gets or sets the hatch style of the rctangle.
        /// </summary>
        public HatchStyle HatchStyle
        {
            get{return hatchStyle;}
            set{hatchStyle = value;}
        }

        /// <summary>
        /// Gets or sets the clockwise rotation angle of rectangle relative to its center.
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
        /// Zooms the rectangle on which it is called.
        /// </summary>
        /// <param name="ZoomCenter">The zoom center for the rcatangle</param>
        /// <param name="ZoomFactor">The zoom factor by which the rectagle is going to be  enlarged.</param>
        public void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            this.location.X = (this.location.X - ZoomCenter.X) * ZoomFactor + ZoomCenter.X;
            this.location.Y = (this.location.Y - ZoomCenter.Y) * ZoomFactor + ZoomCenter.Y;
            this.lineType.Scale(ZoomFactor);
            this.width *= ZoomFactor;
            this.height *= ZoomFactor;
        }

        /// <summary>
        /// Pans or moves the rectangle on which it is called.
        /// </summary>
        /// <param name="Xoffset">The distance in x-direction by which the drawing is going to be moved.</param>
        /// <param name="Yoffset">The distance in y-direction by which the drawing is going to be moved.</param>
        public void Pan(float Xoffset, float Yoffset)
        {
            this.location.X += Xoffset;
            this.location.Y += Yoffset;
        }     
        
        /// <summary>
        /// Draws the rectangle object on which it is called.
        /// </summary>
        /// <param name="g">Graphic object on which the drawing are going to be drawn.</param>
        public void Draw(Graphics g)
        {
            Matrix m = new Matrix();
            m.RotateAt(this.rotation, new PointF(this.location.X + this.width / 2.0f, this.location.Y + this.height / 2.0f));
            g.Transform = m;
            Pen p = new Pen(this.color, this.LineWeight);
            if (this.lineType != eLineTypes.Continuous)
                p.DashPattern = this.lineType.DashPatern;
            switch (drawType)
            {
                case eDrawType.Draw:
                    g.DrawRectangle(p, location.X, location.Y, width, height);
                    break;
                case eDrawType.Fill:
                    g.FillRectangle(new SolidBrush(fillColor), location.X, location.Y, width, height);
                    break;
                case eDrawType.FillAndDraw:
                    g.FillRectangle(new SolidBrush(fillColor), location.X, location.Y, width, height);
                    g.DrawRectangle(p, location.X, location.Y, width, height);
                    break;
                case eDrawType.Hatch:
                    g.FillRectangle(new HatchBrush(hatchStyle,this.color), location.X, location.Y, width, height);
                    break;
                case eDrawType.HatchAndDraw:
                    g.FillRectangle(new HatchBrush(hatchStyle, fillColor, System.Drawing.Color.Transparent), location.X, location.Y, width, height);
                    g.DrawRectangle(p, location.X, location.Y, width, height);                  
                    break;
            }
            g.ResetTransform();
        }

        /// <summary>
        /// Checks if a point is contained in this rectangle.
        /// </summary>
        /// <param name="p">The point for which the existance is going to be checked. </param>
        /// <returns></returns>
        public bool Contains(PointF p)
        {
            if ((p.X > location.X) && (p.Y > location.Y) && (p.X < TopRight.X) && (p.Y < BottomLeft.Y))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Event handler for ESADS.EGraphics.eLayer.Modifeid event;
        /// </summary>
        /// <param name="sender">The layer sending this event.</param>
        /// <param name="e">Event argument containing all the necessary information related to the event.</param>
        void layer_Modified(eLayer sender, eLayerModifiedEventArgs e)
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
