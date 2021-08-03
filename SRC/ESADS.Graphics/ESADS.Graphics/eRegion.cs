using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Contains all necessary methods and data used to create and modify graphic region.
    /// </summary>
    public class eRegion:eIDrawing
    {

        private PointF location;
        private eColor color;
        private eLayer layer;
        private Region region;
        private GraphicsPath path;
        private eDrawType drawType;
        private eColor fillColor;
        private HatchStyle hatchStyle;

        public eRegion(GraphicsPath path, eLayer layer, Color color, eDrawType drawType = eDrawType.Fill)
        {
            this.path = path;
            this.drawType = drawType;
            this.location = path.PathPoints[0];
            this.layer = layer;
            this.fillColor = layer.Color;
            this.color = new eColor(color);
            this.region = new Region(path);
        }

        public eRegion(GraphicsPath path, eLayer layer)
        {
            this.path = path;
            this.drawType = eDrawType.Fill;
            this.location = path.PathPoints[0];
            this.layer = layer;
            this.fillColor = layer.Color;
            this.color = layer.Color;
            this.region = new Region(path);
           
        }

        public Region Region
        {
            get { return new Region(path); }
        }

        public GraphicsPath Path
        {
            get { return path; }
            set
            {
                path = value;
                region = new Region(path);
                location = path.PathPoints[0];
            }
        }
        public HatchStyle HatchStyle
        {
            get { return hatchStyle; }
            set { hatchStyle = value; }
        }
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

        public eLayer Layer
        {
            get {return layer; }
        }

        public eColor Color
        {
            get
            {
                return color;
            }
            set
            {
                color = new eColor(value.Value, eChangeBy.ByObject);
            }
        }
        public eColor FillColor
        {
            get
            {
                return fillColor;
            }
            set
            {
                fillColor = new eColor(value.Value, eChangeBy.ByObject);
            }
        }
        public eDrawType DrawType
        {
            get { return drawType; }
            set { drawType = value; }
        }

        public void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            this.location.X = ZoomFactor * (this.location.X - ZoomCenter.X) + ZoomCenter.X;
            this.location.Y = ZoomFactor * (this.location.Y - ZoomCenter.Y) + ZoomCenter.Y;
            for (int i = 0; i < path.PathPoints.Length; i++)
            {
                path.PathPoints[i].X = ZoomFactor * (path.PathPoints[i].X - ZoomCenter.X) + ZoomCenter.X;
                path.PathPoints[i].Y = ZoomFactor * (path.PathPoints[i].Y - ZoomCenter.Y) + ZoomCenter.Y;
            }
        }

        public void Pan(float Xoffset, float Yoffset)
        {
            this.location.X += Xoffset;
            this.location.Y += Yoffset;
            for (int i = 0; i < path.PathPoints.Length; i++)
            {
                path.PathPoints[i].X += Xoffset;
                path.PathPoints[i].Y += Yoffset;
            }
        }
        
        public void Draw(Graphics g)
        {

            if (drawType == eDrawType.Draw || drawType == eDrawType.HatchAndDraw || drawType == eDrawType.FillAndDraw)
                g.DrawPath(new Pen(color.Value), path);
            if (drawType == eDrawType.Fill || drawType == eDrawType.FillAndDraw)
                g.FillRegion(new SolidBrush(fillColor.Value), region);
            if (drawType == eDrawType.Hatch || drawType == eDrawType.HatchAndDraw)
                g.FillRegion(new HatchBrush(hatchStyle, fillColor.Value), region);
        }
    }
}
