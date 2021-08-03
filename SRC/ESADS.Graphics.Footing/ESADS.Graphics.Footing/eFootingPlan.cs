using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using ESADS.EGraphics;
using ESADS.Mechanics.Design.Footing;
namespace ESADS.EGraphics.Footing
{
    public class eFootingPlan:eFDrawing
    {
        private eFootingBar LBar;
        private eFootingBar BBar;

        public eFootingPlan(eLayers layers, eDFooting footing ,eLengthUnits lengthUnit, eForceUints forceUnit)
        {
            this.layers = layers;
            this.f = footing;
            this.dwgs = new List<eIDrawing>();
            this.lengthUnit = lengthUnit;
            this.forceUnit = forceUnit;
            this.LBar = new eFootingBar(f.LBar, layers[1],  eBarAlignment.Horizontal, eBarLoaction.Plan);
            this.BBar = new eFootingBar(f.BBar, layers[1], eBarAlignment.Vertical, eBarLoaction.Plan);
        }

        private void AddSectionLine()
        {
            int extnDiv = 5;
            dwgs.Add(layers[4].AddLine(new PointF((float)-f.Length / extnDiv, (float)(f.Width / 2 - f.Width / 8)),
                new PointF((float)(f.Length + f.Length / extnDiv), (float)(f.Width / 2 - f.Width / 8))));
            dwgs.Add(layers[4].AddLeader(eLeaderType.Straight, "", false, new PointF((float)-f.Length / extnDiv, (float)(f.Width / 2 - f.Width / 8)),
                new PointF((float)-f.Length / extnDiv, (float)(f.Width / 2 - f.Width / 8 - f.Length / extnDiv))));
            (dwgs[dwgs.Count - 1] as eLeader).ArrowAndDotSize = 100;
            (dwgs[dwgs.Count - 1] as eLeader).SuppressDot = true;
            dwgs.Add(layers[4].AddLeader(eLeaderType.Straight, "", false, new PointF((float)(f.Length + f.Length / extnDiv), (float)(f.Width / 2 - f.Width / 8)),
                    new PointF((float)(f.Length + f.Length / extnDiv), (float)(f.Width / 2 - f.Width / 8 - f.Length / extnDiv))));
            (dwgs[dwgs.Count - 1] as eLeader).ArrowAndDotSize = 100;
            (dwgs[dwgs.Count - 1] as eLeader).SuppressDot = true;
            dwgs.Add(layers[4].AddText("A", new PointF((float)-f.Length / extnDiv, (float)(f.Width / 2 - f.Width / 8 - f.Length / extnDiv - layers[4].TextStyle.Height))));
            dwgs.Add(layers[4].AddText("A", new PointF((float)(f.Length + f.Length / extnDiv), (float)(f.Width / 2 - f.Width / 8 - f.Length / extnDiv - layers[4].TextStyle.Height))));
        }

        protected override void AddLoads()
        {
            SizeF s;
            string txt = "My = " + Math.Round(eUtility.ConvertFrom(f.MB, lengthUnit, forceUnit), precision).ToString()+forceUnit.ToString()+lengthUnit.ToString();
            System.Windows.Forms.Label l = new System.Windows.Forms.Label();
            s = l.CreateGraphics().MeasureString(txt, layers[3].TextStyle);
            dwgs.Add(layers[3].AddLeader(eLeaderType.Straight, "", false, new PointF((float)f.Length / 2, 0), new PointF((float)f.Length / 2, -(float)f.Width / 4)));
            (dwgs[dwgs.Count - 1] as eLeader).ArrowAndDotSize = 70;
            (dwgs[dwgs.Count - 1] as eLeader).SuppressDot = true;
            dwgs.Add(layers[3].AddLeader(eLeaderType.Straight, "", false, new PointF((float)f.Length / 2, 0), new PointF((float)f.Length / 2, -(float)(0.75 * f.Width / 4))));
            (dwgs[dwgs.Count - 1] as eLeader).ArrowAndDotSize = 70;
            (dwgs[dwgs.Count - 1] as eLeader).SuppressDot = true;
            dwgs.Add(layers[3].AddText(txt, new PointF((float)f.Length / 2 + 1.1f * s.Width / 2, -(float)(0.75 * f.Width / 4))));

            txt = "Mx = " + Math.Round(eUtility.ConvertFrom(f.ML, lengthUnit, forceUnit), precision).ToString() + forceUnit.ToString() + lengthUnit.ToString();
            s = l.CreateGraphics().MeasureString(txt, layers[3].TextStyle);
            dwgs.Add(layers[3].AddLeader(eLeaderType.Straight, "", false, new PointF((float)f.Length, (float)f.Width / 2), new PointF((float)(f.Length + f.Length / 4), (float)f.Width / 2)));
            (dwgs[dwgs.Count - 1] as eLeader).ArrowAndDotSize = 70;
            (dwgs[dwgs.Count - 1] as eLeader).SuppressDot = true;

            dwgs.Add(layers[3].AddLeader(eLeaderType.Straight, "", false, new PointF((float)f.Length, (float)f.Width / 2), new PointF((float)( f.Length + 0.75f*f.Length / 4), (float)(f.Width / 2))));
            (dwgs[dwgs.Count - 1] as eLeader).ArrowAndDotSize = 70;
            (dwgs[dwgs.Count - 1] as eLeader).SuppressDot = true;
            dwgs.Add(layers[3].AddText(txt, new PointF((float)(f.Length + 1.1f * s.Width / 2), (float)f.Width / 2 + s.Height)));
        }

        protected override void AddColumn()
        {
            if (f.ColumnType == eColumnType.Rectangular)
            {
                dwgs.Add(layers[0].AddRectangle(new PointF((float)(f.Length / 2 - f.ColumnLength / 2), (float)(f.Width / 2 - f.ColumnWidth / 2)), (float)f.ColumnLength, (float)f.ColumnWidth));
                (dwgs[dwgs.Count - 1] as eRectangle).DrawType = eDrawType.HatchAndDraw;
                (dwgs[dwgs.Count - 1] as eRectangle).FillColor = new eColor(Color.DarkGray, eChangeBy.ByObject);
                (dwgs[dwgs.Count - 1] as eRectangle).HatchStyle = HatchStyle.DiagonalCross;
            }
            else
            {
                dwgs.Add(layers[0].AddCircle(new PointF((float)(f.Length / 2), (float)(f.Length / 2)), (float)f.ColumnDiameter / 2));
                (dwgs[dwgs.Count - 1] as eCircle).DrawType = eDrawType.HatchAndDraw;
                (dwgs[dwgs.Count - 1] as eCircle).FillColor = new eColor(Color.DarkGray, eChangeBy.ByObject);
                (dwgs[dwgs.Count - 1] as eCircle).HatchStyle = HatchStyle.DiagonalCross;
            }
        }

        protected override void AddFootingExterior()
        {
            dwgs.Add(layers[0].AddRectangle(new PointF(), (float)f.Length, (float)f.Width));          
        }

        protected override void AddDimension()
        {
            string txt = Math.Round(eUtility.ConvertFrom(f.Length, lengthUnit), precision).ToString();
            dwgs.Add(layers[2].AddDim(new PointF(0,(float)f.Width), new PointF((float)f.Length, (float)f.Width), txt, eDimensionType.LinearHorizontal, eDimensionLinePosition.RightOrBottom, (float)f.Width * dimF));
            (dwgs[dwgs.Count - 1] as eDimension).ArrowSize = arrowsiz * (float)f.Length;

            txt = Math.Round(eUtility.ConvertFrom(f.Width, lengthUnit), precision).ToString();
            dwgs.Add(layers[2].AddDim(new PointF(), new PointF(0, (float)f.Width), txt, eDimensionType.LinearVertical, eDimensionLinePosition.LeftOrAbove, (float)f.Width * dimF));
            (dwgs[dwgs.Count - 1] as eDimension).ArrowSize = arrowsiz * (float)f.Width;

            if (f.ColumnType == eColumnType.Rectangular)
            {
                txt = Math.Round(eUtility.ConvertFrom(f.ColumnLength, lengthUnit), precision).ToString();
                dwgs.Add(layers[2].AddHorDim(f.Length / 2 - f.ColumnLength / 2, f.Length / 2 + f.ColumnLength / 2,
                f.Width / 2 + f.ColumnWidth / 2, txt, (float)f.ColumnWidth * dimF, eDimensionLinePosition.RightOrBottom));
                (dwgs[dwgs.Count - 1] as eDimension).ArrowSize = 2*arrowsiz * (float)f.ColumnLength;
                (dwgs[dwgs.Count - 1] as eDimension).TextStyle = new eTextStyle(new Font("Arial", (float)f.Length / 40f), eChangeBy.ByObject);

                txt = Math.Round(eUtility.ConvertFrom(f.ColumnWidth, lengthUnit), precision).ToString();
                dwgs.Add(layers[2].AddVerDim(f.Length / 2 - f.ColumnLength / 2, f.Width / 2 - f.ColumnWidth / 2,
                f.Width / 2 + f.ColumnWidth / 2, txt, (float)f.ColumnWidth * dimF, eDimensionLinePosition.LeftOrAbove));
                (dwgs[dwgs.Count - 1] as eDimension).ArrowSize = 2*arrowsiz * (float)f.ColumnLength;
                (dwgs[dwgs.Count - 1] as eDimension).TextStyle = new eTextStyle(new Font("Arial", (float)f.Length / 40f), eChangeBy.ByObject);
            }
            else
            {
                txt = Math.Round(eUtility.ConvertFrom(f.ColumnDiameter, lengthUnit), precision).ToString();
                dwgs.Add(layers[2].AddDim(new PointF((float)(f.Length / 2 - f.ColumnDiameter / 2), (float)(f.Width / 2 - f.ColumnDiameter / 2)),
                    new PointF((float)(f.Length / 2 + f.ColumnDiameter / 2), (float)(f.Width / 2 - f.ColumnDiameter / 2)), txt, eDimensionType.LinearVertical, eDimensionLinePosition.LeftOrAbove, (float)f.ColumnDiameter * dimF));
                (dwgs[dwgs.Count - 1] as eDimension).ArrowSize = arrowsiz * (float)f.ColumnDiameter;
            }
        }

        public override void DrawModelingStage()
        {
            dwgStage = eDrawingStage.ModelingStage;
            AddFootingExterior();
            AddDimension();
            AddColumn();
            AddSectionLine();
            AddLoads();
            CreateContainingRectangle();
        }

        public override void DrawDetailingStage()
        {
            dwgStage = eDrawingStage.DetailingStage;
            AddFootingExterior();
            AddDimension();
            AddColumn();
            AddSectionLine();
            AddBars();
            CreateContainingRectangle(); 
        }

        protected override void AddBars()
        {
            double r1 = 0.2, r2 = 0.1;
            LBar.AddDrawings();
            BBar.AddDrawings();
            LBar.Move(f.Cover, f.Width * (1 - r1));
            BBar.Move(f.Length * r1, f.Cover);

            eDimension d1 = (layers[2].AddHorDistSymbol(f.Cover, f.Length - 2 * f.Cover, r2 * f.Width));
            eDimension d2 = layers[2].AddVerDistSymbol(f.Length * (1 - r2), f.Cover, f.Width - 2 * f.Cover);
            d1.Color = new eColor(Color.DarkGray);
            d2.Color = new eColor(Color.DarkGray);
            eCircle c1 = layers[2].AddCircle(r1 * f.Length, r2 * f.Width, f.Width / 80, eDrawType.Fill);
            eCircle c2 = layers[2].AddCircle((1 - r2) * f.Length, (1 - r1) * f.Width, f.Width / 80, eDrawType.Fill);
            c1.FillColor = new eColor(Color.DarkGray);
            c2.FillColor = new eColor(Color.DarkGray);
            dwgs.Add(d1);
            dwgs.Add(d2);
            dwgs.Add(c1);
            dwgs.Add(c2);
        }

        protected override void CreateContainingRectangle()
        {
            if (dwgStage == eDrawingStage.ModelingStage)
                contRect = layers[0].AddRectangle(f.Width * dimF, -f.Width / 4, f.Length + f.Width * dimF + f.Length / 4, f.Width + f.Width / 4 + f.Width * dimF);
            else
                contRect = layers[0].AddRectangle(f.Width * dimF, -f.Width * dimF, f.Width * dimF + f.Length, f.Width + f.Width * dimF);
            contRect.Color = new eColor(Color.Transparent, eChangeBy.ByObject);
            dwgs.Add(contRect);
        }

        public void Move(double xoffset, double yoffset)
        {
            foreach (eIDrawing d in dwgs)
                d.Pan((float)xoffset, (float)yoffset);
            LBar.Move(xoffset, yoffset);
            BBar.Move(xoffset, yoffset);
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
            LBar.RemoveDiagram();
            BBar.RemoveDiagram();
        }
    }
}
