using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using ESADS.Mechanics.Design.Column;
using ESADS.GUI;
using System.Windows.Forms;

namespace ESADS.EGraphics.Column
{
    /// <summary>
    /// Represents column moment axial load interation diagram.
    /// </summary>
    public class eInteractionDiagram:eIDrawing
    {
        #region Fields
        private double r;
        private eLayer layer;
        private eColor color;
        private PointF location;
        private eLengthUnits lengthUnit;
        private eForceUints forceUnit;
        private eDColumn column;
        private float size;
        private Region region;
        private Color provColor;
        private Color calcColor;
        private int precision;
        private List<eIDrawing> dwgs;
        private Timer t;
        private eModelForm dwgForm;
        float zoomState = 1;
        #endregion

        #region Constructor
        public eInteractionDiagram(eDColumn column, eLayer layer, eModelForm dwgForm)
        {
            this.column = column;
            this.layer = layer;
            dwgForm.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(dwgForm_MouseDoubleClick);
            location = new PointF();
            lengthUnit = eLengthUnits.m;
            forceUnit = eForceUints.KN;
            calcColor = System.Drawing.Color.Yellow;
            provColor = System.Drawing.Color.Red;
            dwgs = new List<eIDrawing>();
            precision = 2;
            size = 300;
            t = new Timer();
            t.Interval = 1000;
            t.Tick += new EventHandler(t_Tick);
            if (column.Action == eColumnAction.Biaxial)
                r = (column as eBiaxial).R;
            this.dwgForm = dwgForm;
            AddDrawings();
        }

        #endregion 

        #region Properties
       
        public double R
        {
            get
            {
                return r;
            }
            set
            {
                if (column.GetType() == typeof(eBiaxial))
                    r = value;
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

        public eForceUints ForceUnit
        {
            get
            {
                return forceUnit;
            }
            set
            {
                forceUnit = value;
            }
        }

        public eDColumn Column
        {
            get
            {
                return column;
            }
            set
            {
                column = value;
            }
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
            get { return layer; }
        }

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

        #endregion

        #region Methods
        /// <summary>
        /// Zoom the given drawing by the specifeid scale factor for a given zoom center.
        /// </summary>
        /// <param name="ZoomCenter">Zoom center from which the zooming is done.</param>
        /// <param name="ZoomFactor">Zoom factor by which the drawing is elarged.</param>
        public void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            foreach (eIDrawing d in dwgs)
                d.Zoom(ZoomCenter, ZoomFactor);
            AddRegion((dwgs[0] as eCurve).Points.ToList());
            this.location.X = ZoomFactor * (this.location.X - ZoomCenter.X) + ZoomCenter.X;
            this.location.Y = ZoomFactor * (this.location.Y - ZoomCenter.Y) + ZoomCenter.Y;
            zoomState *= ZoomFactor;
        }

        /// <summary>
        /// Pans this drawing object a given distance in both direction.
        /// </summary>
        /// <param name="Xoffset">X distance by which the drawing is going to be shifted.</param>
        /// <param name="Yoffset">Y distance by which the drawing is going to be shifted.</param>
        public void Pan(float Xoffset, float Yoffset)
        {
            foreach (eIDrawing d in dwgs)
                d.Pan(Xoffset, Yoffset);
            AddRegion((dwgs[0] as eCurve).Points.ToList());
            this.location.X += Xoffset;
            this.location.Y += Yoffset;
        }

        /// <summary>
        /// Draws this drawing object on the given graphics object.
        /// </summary>
        /// <param name="g">Graphics object on which the drawing is caried out.</param>
        public void Draw(Graphics g)
        {
            foreach (eIDrawing d in dwgs)
            d.Draw(g);
            System.Drawing.Color c = System.Drawing.Color.FromArgb(50, calcColor);       
            c = System.Drawing.Color.FromArgb(250, provColor);
        }

        private void AddDrawings()
        {
            List<double> points;
            int n = 10;
            double Pmax, Mmax;
            points = column.GetInteractionDiag(n, r, eAnalysisReinf.AsProvided);
            Pmax = points[points.Count - 2];
            Mmax = points.Max();
            AddCurve(size / Pmax, Pmax, size / Mmax, points, provColor);
            AddRegion(((eCurve)dwgs[0]).Points.ToList());
            AddLables(Pmax, points[1]);
            points = column.GetInteractionDiag(n, r);
            AddCurve(size / Pmax, Pmax, size / Mmax, points, calcColor);
            AddAxis();          
            AddLagend();
            AddAxisMarks();
            AddMdPd(size / Mmax, size / Pmax, Pmax);

            layer.AddText("Mx = " + Math.Round(eUtility.ConvertFrom(column.Mx, eLengthUnits.m, eForceUints.KN), precision).ToString() + "KNm", size / 2, -1.5f * layer.TextStyle.Height);
            layer.AddText("P = " + Math.Round(eUtility.ConvertFrom(column.P, eForceUints.KN), precision).ToString() + "KN", size / 2, -2 * 1.5f * layer.TextStyle.Height);
            if (column.Action == eColumnAction.Biaxial)
            {
                layer.AddText(column.Action.ToString() + ": Mx/My = " + (column as eBiaxial).R.ToString(), size / 2, -3 * 1.5f * layer.TextStyle.Height);
                layer.AddText("My = " + Math.Round(eUtility.ConvertFrom((column as eBiaxial).My, eLengthUnits.m, eForceUints.KN), precision).ToString() + "KNm", size / 2, 0);
            }
            else
                layer.AddText(column.Action.ToString(), size / 2, -3 * 1.5f * layer.TextStyle.Height);
            t.Start();
        }

        /// <summary>
        /// Redarws the interaction diagram again.
        /// </summary>
        public void Redraw()
        {
            dwgs = new List<eIDrawing>();  
            AddDrawings();
        }

        private void AddCurve(double Pfactro, double Pmax, double Mfactor, List<double> points, Color c)
        {
            PointF[] pts = new PointF[points.Count / 2];
         
            for (int i = 0; i < pts.Length; i++)
            {
                pts[i] = new PointF((float)(points[2 * i + 1] * Mfactor), (float)(Pfactro * (Pmax - points[2 * i])));
            }
            eCurve cc = new eCurve(pts, layer);
            cc.Color = new eColor(c, eChangeBy.ByObject);
            dwgs.Add(cc);
        }

        private float Convert(params double []  d)
        {
            double ans = 1;
            for (int i = 0; i < d.Length; i++)
                ans *= ans;
            return (float)ans;
        }

        private void AddAxis()
        {
            dwgs.Add(new eLeader(eLeaderType.Straight, this.Layer, null, false, new PointF(0, size), new PointF(0, -size / 5)));
            dwgs.Add(new eLeader(eLeaderType.Straight, Layer, null, false, new PointF(0, size), new PointF(size + size / 5, size)));
            dwgs.Add(new eText("P(KN)", new PointF(-Layer.TextStyle.Height * 2, -size / 10), Layer));
            dwgs.Add(new eText(column.Action == eColumnAction.Uniaxial ? "M(KN)" : "Mx(KN)", new PointF(size + size / 4.5f, size + Layer.TextStyle.Height), Layer));
        }

        private void AddRegion(List<PointF> pts)
        {
            GraphicsPath gp = new GraphicsPath();
            pts.Add(new PointF(pts[pts.Count - 1].X, pts[pts.Count - 1].Y));
            pts.Add(new PointF(pts[pts.Count - 2].X, pts[0].Y));
            pts.Add(pts[0]);
            gp.AddPolygon((PointF[])pts.ToArray());
            region = new Region(gp);
        }

        private void AddLables(double maxP, double flexurM)
        {
            string txt;
            System.Windows.Forms.Label l = new System.Windows.Forms.Label();
            Graphics g = l.CreateGraphics();
            txt = Math.Round(eUtility.Convert(maxP, eUtility.SFU, eForceUints.KN), precision).ToString();
            SizeF s = g.MeasureString(txt, layer.TextStyle);
            dwgs.Add(new eText(txt, new PointF(-s.Width / 2, 0),layer));
            txt = Math.Round(eUtility.Convert(flexurM, eUtility.SLU, lengthUnit, eUtility.SFU, forceUnit), precision).ToString();
            s = g.MeasureString(txt, layer.TextStyle);
            dwgs.Add(new eText(txt, new PointF((dwgs[0] as eCurve).Points[0].X, size + s.Height / 2),layer));
            dwgs.Add(new eText(txt, new PointF((dwgs[0] as eCurve).Points[0].X, size + s.Height / 2), layer));
        }

        private void AddLagend()
        {
            System.Windows.Forms.Label l = new System.Windows.Forms.Label();
            Graphics g = l.CreateGraphics();
            dwgs.Add(new eRectangle(new PointF(size + size / 5, 0), 150, 100, layer));
            dwgs.Add(new eLine(new PointF(size + size / 5 + 10, 55), new PointF(size + size / 5 + 50, 55), layer));
            dwgs[dwgs.Count - 1].Color = new eColor(provColor);
            dwgs.Add(new eText("As,provided", new PointF(size + size / 5 + 50 + g.MeasureString("As,provided", layer.TextStyle).Width / 2, 55), layer));
            dwgs.Add(new eLine(new PointF(size + size / 5 + 10, 85), new PointF(size + size / 5 + 50, 85), layer));
            dwgs[dwgs.Count - 1].Color = new eColor(calcColor);
            dwgs.Add(new eText("As,calculated", new PointF(size + size / 5 + 50 + g.MeasureString("As,calculated", layer.TextStyle).Width / 2, 85), layer));
            eText t = new eText("Legend", new PointF(size + size / 5 + 75, 20), layer);
            t.Zoom(t.Location, 1.5f);
            dwgs.Add(t);
            dwgs.Add(new eLine(new PointF(size + size / 5, 40), new PointF(size + size / 5 + 150, 40), layer));
        }

        private void AddAxisMarks()
        {
            eCurve c = dwgs[0] as eCurve;
            dwgs.Add(new eLine(new PointF(-3, c.Points[c.Points.Length - 1].Y), new PointF(3, c.Points[c.Points.Length - 1].Y), layer));
            dwgs.Add(new eLine(new PointF(c.Points[0].X, size - 3), new PointF(c.Points[0].X, size + 3), layer));
        }

        private PointF GetMaxPoint()
        {
            PointF [] pts = (dwgs[1] as eCurve).Points;
            float Xmax = pts[0].X;
            int n = 0;
            for (int i = 0; i < pts.Length; i++)
            {
                if (pts[i].X > Xmax)
                {
                    Xmax = pts[i].X;
                    n = i;
                }
            }
            return pts[n];
        }

        private void AddMdPd(double mfactor, double pfactor, double Pmax)
        {

                eCircle c = new eCircle(new PointF((float)(column.Mx * mfactor), (float)(pfactor * (Pmax - column.P))), 4, eDrawType.Fill, layer);
                c.FillColor = new eColor(System.Drawing.Color.Red);
                dwgs.Add(c);
        }

        void t_Tick(object sender, EventArgs e)
        {
            eCircle c = (dwgs[dwgs.Count - 1] as eCircle);
            if (c.FillColor.Value == System.Drawing.Color.Red)
                c.FillColor = new eColor(System.Drawing.Color.Transparent);
            else
                c.FillColor = new eColor(System.Drawing.Color.Red);
            dwgForm.Invalidate(new Rectangle((int)(c.Location.X - c.Radius - 2), (int)(c.Location.Y - c.Radius - 2), 2 * (int)c.Radius + 4, 2 * (int)c.Radius + 4));
        }

        private void dwgForm_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (region.IsVisible(e.Location))
            {
                eColumnOutputDialog dlg = new eColumnOutputDialog(lengthUnit, forceUnit, this.column);
                dlg.ShowDialog();
            }
        }

        public void ReleasHandler()
        {
            dwgForm.MouseDoubleClick -= dwgForm_MouseDoubleClick;
        }
        #endregion
    }
}
