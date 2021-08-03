using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using ESADS;
using ESADS.EGraphics;
using ESADS.Code;
using ESADS.Mechanics.Design.Footing;
using System.Windows.Forms;
namespace ESADS.EGraphics.Footing
{
    public class eFootingBar
    {
        private eFBar bar;
        private eBarAlignment alignment;
        private PointF location;
        private eLayer layer;
        private List<eIDrawing> dwgs;
        private eBarLoaction barLoc;
        private eLengthUnits lengthUnit;
        private eRectangle contRect;
        private int precision;
        public eFootingBar(eFBar bar, eLayer layer, eBarAlignment alignment,eBarLoaction barLoc)
        {
            this.bar = bar;
            this.layer = layer;
            this.dwgs = new List<eIDrawing>();
            this.alignment = alignment;
            this.barLoc = barLoc;
            this.precision = 2;
            this.lengthUnit = eLengthUnits.mm;
        }

        public eRectangle ContRect
        {
            get { return contRect; }
        }

        public eBarLoaction BarLocation
        {
            get { return barLoc; }
            set { barLoc = value; }
        }
        public eFBar Bar
        {
            get
            {
                return bar;
            }
            set
            {
                bar = value;
            }
        }

        public eBarAlignment Alignment
        {
            get
            {
                return alignment;
            }
        }

        public eLayer Layer
        {
            get { return layer; }
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

        public eLengthUnits LengthUnit
        {
            get
            {
                return lengthUnit;
            }
            set
            {
                lengthUnit = value;
            }
        }

        private void AddText()
        {
            Label l = new Label();
            string txt;
            Graphics g = l.CreateGraphics();
            SizeF s = new SizeF();
            if (barLoc == eBarLoaction.Alone && alignment == eBarAlignment.Horizontal)
            {
                txt = bar.Name + " " + "#" + bar.Number.ToString() + " Φ" + bar.Diameter.ToString() + " C/C = " +
                Math.Round(eUtility.ConvertFrom(bar.ProvSpacing, lengthUnit), precision).ToString() + " L = " + Math.Round(eUtility.ConvertFrom(bar.Length, lengthUnit), precision).ToString();
                s = g.MeasureString(txt, layer.TextStyle);
                dwgs.Add(layer.AddText(txt, new PointF((float)bar.Legths[1] / 2, -s.Height / 2)));
                if (bar.AnchorType != eAnchorageType.Straight)
                {
                    g.MeasureString(bar.Legths[1].ToString(), layer.TextStyle);
                    dwgs.Add(layer.AddText(bar.Legths[0].ToString(), new PointF(-s.Height / 2, -(float)bar.Legths[0] / 2), 90));
                    dwgs.Add(layer.AddText(bar.Legths[0].ToString(), new PointF(s.Height / 2 + (float)bar.Legths[1], -(float)bar.Legths[0] / 2), 90));
                }
                s = g.MeasureString(bar.Legths[1].ToString(), layer.TextStyle);
                dwgs.Add(layer.AddText(bar.Legths[1].ToString(), new PointF((float)bar.Legths[1] / 2, s.Height / 2)));
            }
            else if (barLoc == eBarLoaction.Plan)
            {
                s = g.MeasureString(bar.Name, layer.TextStyle);
                if (alignment == eBarAlignment.Horizontal)
                    dwgs.Add(layer.AddText(bar.Name, new PointF((float)bar.Legths[1] / 2, -s.Height / 2)));
                else
                    dwgs.Add(layer.AddText(bar.Name, new PointF(s.Width / 2, (float)bar.Legths[1] / 2)));
            }
        }

        private void AddLines()
        {
            if (alignment == eBarAlignment.Vertical)
            {
                if (bar.AnchorType != eAnchorageType.Straight)
                {
                    dwgs.Add(layer.AddLine(new PointF(0,0), new PointF((float)bar.Legths[0], 0)));
                    dwgs.Add(layer.AddLine(new PointF(0, (float)bar.Legths[1]), new PointF((float)bar.Legths[0], (float)bar.Legths[1])));
                }
                dwgs.Add(layer.AddLine(new PointF(0,0), new PointF(0, (float)bar.Legths[1])));
            }
            else
            {
                if (bar.AnchorType != eAnchorageType.Straight)
                {
                    dwgs.Add(layer.AddLine(new PointF(0, 0), new PointF(0, -(float)bar.Legths[0])));
                    dwgs.Add(layer.AddLine(new PointF((float)bar.Legths[1], 0), new PointF((float)bar.Legths[1], -(float)bar.Legths[0])));
                }
                dwgs.Add(layer.AddLine(new PointF(0,0), new PointF((float)bar.Legths[1], 0)));
            }
        }

        internal void Move(double xOffset, double yOffset)
        {
            foreach (eIDrawing d in dwgs)
                d.Pan((float)xOffset, (float)yOffset);
        }

        private void CreteContainingRectangle()
        {
            if (alignment == eBarAlignment.Horizontal)
            {
                if (bar.AnchorType != eAnchorageType.Straight)
                {
                    contRect = layer.AddRectangle(0, 0, bar.Length, 1f);
                }
                else
                {
                    contRect = layer.AddRectangle(0, -bar.Legths[0], bar.Legths[1], bar.Legths[0]);
                }
            }
            else
            {
                if (bar.AnchorType != eAnchorageType.Straight)
                {
                    contRect = layer.AddRectangle(0, 0, bar.Legths[0], bar.Legths[1]);
                }
                else
                {
                    contRect = layer.AddRectangle(0, 0, 1, bar.Legths[1]);
                }
            }
            contRect.Color = new eColor(Color.Transparent, eChangeBy.ByObject);
            dwgs.Add(contRect);
        }
        public void AddDrawings()
        {
            AddLines();
            AddText();
            CreteContainingRectangle();
        }

        public void RemoveDiagram()
        {
            for (int i = 0; i < dwgs.Count; i++)
            {
                layer.Remove(dwgs[i]);
            }
            contRect = null;
            dwgs = new List<eIDrawing>();
        }

        public void RemoveDwgs()
        {
            this.dwgs = new List<eIDrawing>();
        }
        
    }
}
