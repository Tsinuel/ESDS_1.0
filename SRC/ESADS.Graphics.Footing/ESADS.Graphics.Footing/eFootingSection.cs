using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS;
using ESADS.EGraphics;
using ESADS.Mechanics.Design.Footing;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ESADS.EGraphics.Footing
{
    public class eFootingSection:eFDrawing
    {
        private eFootingBar bar;
        private eDrawingStage dwgStage;
        private bool drawDowels;
        private eFootingColumnConnection connection;
        private double dowelsBent;
        public double DowelsBentLength
        {
            get
            {
                return dowelsBent;
            }
            set
            {
                dowelsBent = value;
            }
        }

        public eFootingColumnConnection Connection
        {
            get
            {
                return connection;
            }
            set
            {
                connection = value;
            }
        }

        public bool DrawDowels
        {
            get
            {
                return drawDowels;
            }
            set
            {
                drawDowels = value;
            }
        }

        public eFootingSection(eLayers layers, eDFooting footing, eLengthUnits lengthUnit, eForceUints forceUnit)
        {
            this.layers = layers;
            this.f = footing;
            this.dwgs = new List<eIDrawing>();
            this.forceUnit = forceUnit;
            this.lengthUnit = lengthUnit;
            this.bar = new eFootingBar(f.LBar, layers[1], eBarAlignment.Horizontal, eBarLoaction.Section);
            this.connection = eFootingColumnConnection.Fixed;
        }

        private void AddLeanConcrete()
        {
            double D;
            if (dwgStage == eDrawingStage.ModelingStage && !f.IsDepthGiven)
                D = f.Length / 4;
            else
                D = f.Depth;
            PointF[] pts = new PointF[4];
            pts[0] = new PointF(0, (float)(f.Length / 2 + D));
            pts[1] = new PointF((float)f.Length, (float)(f.Length / 2 + D));
            pts[2] = new PointF((float)f.Length + 50, (float)(f.Length / 2 + D + 50));
            pts[3] = new PointF(-50, (float)(f.Length / 2 + D + 50));
            ePolygone pg = layers[0].AddPolyGon(pts, eDrawType.Fill);
            pg.HatchStyle = HatchStyle.SolidDiamond;
            pg.FillColor = new eColor(Color.DarkGray, eChangeBy.ByObject);
            dwgs.Add(pg);
        }

        protected override void AddBars()
        {
            bar.AddDrawings();
            bar.Move(f.Cover, f.Length / 2 + f.Depth - f.Cover );
            for (int i = 0; i < f.BBar.Number; i++)
            {
                dwgs.Add(layers[1].AddCircle(f.Cover + f.BBar.Diameter / 2 + i * f.BBar.DisplaySpacing,
                f.Length / 2 + f.Depth - f.Cover - f.BBar.Diameter / 2, f.BBar.Diameter / 2, eDrawType.Fill));
            }
            AddBarLegend();
        }
        private void AddDowles()
        {
            double cd = f.ColumnType == eColumnType.Rectangular ? f.ColumnLength : f.ColumnDiameter;
            if (connection == eFootingColumnConnection.Pin)
            {
                dwgs.Add(layers[1].AddVerLine(f.Length / 2 - cd / 2 + f.Cover, 0, f.Length / 2 + f.Depth - f.Cover - f.BBar.Diameter));
                dwgs.Add(layers[1].AddVerLine(f.Length / 2 + cd / 2 - f.Cover, 0, f.Length / 2 + f.Depth - f.Cover - f.BBar.Diameter));
                dwgs.Add(layers[1].AddHorLine(f.Length / 2 - cd / 2 + f.Cover, f.Length / 2 - cd / 2 + f.Cover - dowelsBent, f.Length / 2 + f.Depth - f.Cover - f.BBar.Diameter));
                dwgs.Add(layers[1].AddHorLine(f.Length / 2 + cd / 2 - f.Cover, f.Length / 2 + cd / 2 - f.Cover + dowelsBent, f.Length / 2 + f.Depth - f.Cover - f.BBar.Diameter));
            }
            else
            {
                dwgs.Add(layers[1].AddVerLine(f.Length / 2 - cd / 2 + f.Cover, 0, f.Length / 2 + f.Depth - f.Cover - f.BBar.Diameter));
                dwgs.Add(layers[1].AddVerLine(f.Length / 2 + cd / 2 - f.Cover, 0, f.Length / 2 + f.Depth - 1.5 * f.Cover - f.BBar.Diameter));
                dwgs.Add(layers[1].AddHorLine(f.Length / 2 - cd / 2 + f.Cover, f.Length / 2 - cd / 2 + f.Cover + dowelsBent, f.Length / 2 + f.Depth - f.Cover - f.BBar.Diameter));
                dwgs.Add(layers[1].AddHorLine(f.Length / 2 + cd / 2 - f.Cover, f.Length / 2 + cd / 2 - f.Cover - dowelsBent, f.Length / 2 + f.Depth - 1.5 * f.Cover - f.BBar.Diameter));
            }
        }

        
        protected override void AddColumn()
        {
            double cd = f.ColumnType == eColumnType.Rectangular ? f.ColumnLength : f.ColumnDiameter;
            dwgs.Add(layers[0].AddVerLine(f.Length / 2 - cd / 2, f.Length / 2, 0));
            dwgs.Add(layers[0].AddVerLine(f.Length / 2 + cd / 2, f.Length / 2, 0));
        }

        protected override void AddFootingExterior()
        {
            double D;
            if (dwgStage == eDrawingStage.ModelingStage && !f.IsDepthGiven)
                D = f.Length / 4;
            else
                D = f.Depth;
            double cd = f.ColumnType == eColumnType.Rectangular ? f.ColumnLength : f.ColumnDiameter;
            dwgs.Add(layers[0].AddHorLine(0, f.Length / 2 - cd / 2, f.Length / 2));
            dwgs.Add(layers[0].AddVerLine(0, f.Length / 2, D + f.Length / 2));
            dwgs.Add(layers[0].AddHorLine(0, f.Length, D + f.Length / 2));
            dwgs.Add(layers[0].AddVerLine(f.Length, f.Length / 2, D + f.Length / 2));
            dwgs.Add(layers[0].AddHorLine(f.Length, f.Length / 2 + cd / 2, f.Length / 2));
            dwgs.Add(layers[2].AddBreakLine(new PointF((float)(f.Length / 2 - cd / 2), 0), new PointF((float)(f.Length / 2 + cd / 2), 0)));
            (dwgs[dwgs.Count - 1] as eBreakLine).Extention = 0.5f * (float)cd;
            dwgs.Add(layers[3].AddText("Section A-A", f.Length / 2, 1.2 * f.Length / 2 + D));
            //(dwgs[dwgs.Count - 1] as eText).SetFontHeight(f.Length / 10);
        }

        protected override void AddDimension()
        {
            double D;
            if (dwgStage == eDrawingStage.ModelingStage && !f.IsDepthGiven)
            {
                D = f.Length / 4;
            }
            else
                D = f.Depth;
            string txt = dwgStage == eDrawingStage.ModelingStage && !f.IsDepthGiven ? "D" : Math.Round(eUtility.ConvertFrom(f.Depth, lengthUnit), precision).ToString();

            dwgs.Add(layers[2].AddVerDim(0, f.Length / 2, f.Length / 2 + D, txt, (float)(D * dimF)));
            (dwgs[dwgs.Count - 1] as eDimension).ArrowSize = 2 * arrowsiz * (float)f.Depth;
            (dwgs[dwgs.Count - 1] as eDimension).TextStyle = new eTextStyle(new Font("Arial", (float)D / 6f), eChangeBy.ByObject);
        }

        public override void DrawModelingStage()
        {
            dwgStage = eDrawingStage.ModelingStage;
            AddFootingExterior();
            AddDimension();
            AddColumn();
            AddLoads();
            CreateContainingRectangle();
        }

        public override void DrawDetailingStage()
        {
            dwgStage = eDrawingStage.DetailingStage;
            AddFootingExterior();
            AddDimension();
            AddColumn();
            AddBars();
            AddDowles();
            //AddLeanConcrete();
            CreateContainingRectangle();
        }

        protected override void CreateContainingRectangle()
        {
            double D;
            if (dwgStage == eDrawingStage.ModelingStage && !f.IsDepthGiven)
                D = f.Length / 4;
            else
                D = f.Depth;
            double cd = f.ColumnType == eColumnType.Rectangular ? f.ColumnLength : f.ColumnDiameter;
            if (dwgStage == eDrawingStage.ModelingStage)
                contRect = layers[0].AddRectangle(-D * dimF, -cd, f.Length + D * dimF, f.Length / 2 + D + cd);
            else
                contRect = layers[0].AddRectangle(-D * dimF, 0, f.Length + D * dimF, f.Length / 2 + D);
            contRect.Color = new eColor(Color.Transparent, eChangeBy.ByObject);
            dwgs.Add(contRect);
        }

        private void AddBarLegend()
        {
            try
            {
                dwgs.Add(layers[1].AddText(f.BBar.Name, f.Cover + f.BBar.Diameter / 2 + (f.BBar.Number - 2) * f.BBar.DisplaySpacing + layers[3].TextStyle.Height / 2,
                                            f.Length / 2 - f.BBar.EffD * 0.5 - layers[3].TextStyle.Height / 2));
                eLine l1 = layers[3].AddVerLine(f.Cover + f.BBar.Diameter / 2 + (f.BBar.Number - 2) * f.BBar.DisplaySpacing,
                              f.Length / 2 + f.Depth - f.Cover - f.BBar.Diameter / 2, f.Length / 2 - f.BBar.EffD * 0.5);
                l1.LineWeight = new eLineWeight(1.0f);
                dwgs.Add(l1);
                eLine l2 = layers[3].AddLine(f.Cover + f.BBar.Diameter / 2 + (f.BBar.Number - 3) * f.BBar.DisplaySpacing, f.Length / 2 + f.Depth - f.Cover - f.BBar.Diameter / 2,
                                             f.Cover + f.BBar.Diameter / 2 + (f.BBar.Number - 2) * f.BBar.DisplaySpacing, f.Length / 2 - f.BBar.EffD * 0.5);

                l2.LineWeight = new eLineWeight(1.0f);
                dwgs.Add(l2);
                eCircle c = layers[3].AddCircle(f.Cover + f.BBar.Diameter / 2 + (f.BBar.Number - 2) * f.BBar.DisplaySpacing + layers[3].TextStyle.Height / 2,
                                            f.Length / 2 - f.BBar.EffD * 0.5 - layers[3].TextStyle.Height / 2, layers[3].TextStyle.Height / 2);
                c.LineWeight = new eLineWeight(1.0f);
                dwgs.Add(c);
            }
            catch { }
        }

        protected override void AddLoads()
        {
            double cd = f.ColumnType == eColumnType.Rectangular ? f.ColumnLength : f.ColumnDiameter;
            dwgs.Add(layers[3].AddLeader(eLeaderType.Straight, "", false, new PointF((float)f.Length / 2, -(float)cd), new PointF((float)f.Length / 2, (float)-cd / 4)));
            string txt = Math.Round(eUtility.ConvertFrom(f.P, forceUnit), precision).ToString() + forceUnit.ToString();
            (dwgs[dwgs.Count - 1] as eLeader).ArrowAndDotSize = 70;
            (dwgs[dwgs.Count - 1] as eLeader).SuppressDot = true;
            dwgs.Add(layers[3].AddText(txt, f.Length / 2, -layers[3].TextStyle.Height - cd));
        }

        public void Move(double xoffset, double yoffset)
        {
            foreach (eIDrawing d in dwgs)
                d.Pan((float)xoffset, (float)yoffset);
            bar.Move(xoffset, yoffset);
        }

        public void RemoveDiagram()
        {
            for (int i = 0; i < dwgs.Count; i++)
            {
                for (int j = 0; j < layers.Count; j++)
                {
                    layers[j].Remove(dwgs[i]);
                }
            }
            layers[0].Remove(contRect);
            contRect = null;
            dwgs = new List<eIDrawing>();
            bar.RemoveDiagram();
        }

    }
}
