using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using ESADS.Mechanics.Design.Column;
using ESADS.GUI;

namespace ESADS.EGraphics.Column
{
    public class eGColumn:eIGModel
    {
        #region Fields
        private eLayers layers;
        private eDColumn column;
        private eColumnDrawingStage drawingStage;
        private eLengthUnits lengthUnit;
        private eForceUints forceUnit;
        private int precision;
        private eModelForm modelForm;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or set the precision to display numerica values.
        /// </summary>
        public int Precision
        {
            get { return precision; }
            set { precision = value; }
        }

        /// <summary>
        /// Length unit used in the design.
        /// </summary>
        public eLengthUnits LengthUnit
        {
            get { return lengthUnit; }
            set
            { 
                lengthUnit = value;
                AddDimensions();
            }
        }

        /// <summary>
        /// Force unit used in the design.
        /// </summary>
        public eForceUints ForceUnit
        {
            get { return forceUnit; }
            set { forceUnit = value; }
        }

        /// <summary>
        /// Stage of drawing in the column i.e. Modeling and Detailing.
        /// </summary>
        public eColumnDrawingStage DrawingStage
        {
            get { return drawingStage; }
            set { drawingStage = value; }
        }

        /// <summary>
        /// The Mechanics component of the column being designed.
        /// </summary>
        public eDColumn Column
        {
            get { return column; }
            set { column = value; }
        }

        /// <summary>
        /// Layers involved in the graphic display of the column.
        /// </summary>
        public eLayers Layers
        {
            get { return layers; }
            set { layers = value; }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates un instance of ESADS.EGraphics.eGColumn class from the given basic parameters.
        /// </summary>
        /// <param name="mForm">Model form.</param>
        /// <param name="column">Mechanics component of the column being designed.</param>
        public eGColumn(eModelForm mForm, eDColumn column)
        {
            this.modelForm = mForm;
            layers = new eLayers(mForm);
            this.column = column;
            this.lengthUnit = modelForm.Document.LengthUnit;
            this.forceUnit = modelForm.Document.ForceUnit;
            this.precision = 2;
            InitializeComponents();
        }
        #endregion

        #region Methods
        private void InitializeComponents()
        {
            layers.Add("Column", Color.Red, eLineTypes.Continuous, 2.0f);
            layers.Add("Bars", Color.Magenta, eLineTypes.Continuous, 2.0f);
            layers.Add("Dimension", Color.Cyan, eLineTypes.Continuous, 1.0f, new Font("Arail", (float)(column.MinDim / 15)));
            layers.Add("Text", Color.White, eLineTypes.Continuous, 3.0f, new Font("Arail", (float)(column.MinDim / 10)));
            layers.Add("Diagram", Color.White, eLineTypes.Continuous, 2.0f);
            Draw(eColumnDrawingStage.Modeling);
        }

        private void AddDrawings()
        {
            layers.Zoom(new PointF(), 1 / layers["Text"].ZoomFactor);
            layers["Column"].AddRectangle(new PointF(), (float)column.Width, (float)column.Depth);
            AddDimensions();
            if (drawingStage == eColumnDrawingStage.Modeling)
                AddModelingDrawings();
            else if (drawingStage == eColumnDrawingStage.Design)
                AddDesignDrawings();
            else
                AddDetailingDrawings();
            ArrangeDrawings();
        }

        private void AddDesignDrawings()
        {
            AddModelingDrawings();
            double Arae;
            if (column.TypeOfDetail == eDetailType.Type1)
                Arae = column.AsTotal / 2;
            else if (column.TypeOfDetail == eDetailType.Type2)
                Arae = column.AsTotal / 4;
            else
                Arae = column.AsTotal / column.NumberOfBars;

            layers["Text"].AddText("A = " + Math.Round(column.GetUnitArea(), precision).ToString(), new PointF((float)column.Width / 2, (float)(column.Depth + column.Depth / 5)));
            AddDiagram();
        }

        private void AddModelingDrawings()
        {
            AddLoad();
            double div = Math.Min(column.Depth, column.Depth) / 10;
            double tick = column.Width / 60;
            if (column.TypeOfDetail == eDetailType.Type1)
            {
                layers["Bars"].AddRectangle(new PointF((float)(div / 2), (float)(div / 2)), (float)(column.Width - div), (float)(tick), DrawType: eDrawType.FillAndDraw);
                layers["Bars"].AddRectangle(new PointF((float)(div / 2), (float)(column.Depth - div / 2 - tick )), (float)(column.Width - div), (float)(tick), DrawType: eDrawType.FillAndDraw);
                layers["Text"].AddText("A", new PointF((float)column.Width / 2, (float)(div / 2 + tick + layers["Text"].TextStyle.Height)));
                layers["Text"].AddText("A", new PointF((float)column.Width / 2, (float)(column.Depth - (div / 2 + tick + layers["Text"].TextStyle.Height))));
            }
            else if (column.TypeOfDetail == eDetailType.Type2)
            {
                layers["Bars"].AddRectangle(new PointF((float)(div / 2), (float)(div / 2)), (float)(column.Width - div), (float)(tick), DrawType: eDrawType.FillAndDraw);
                layers["Bars"].AddRectangle(new PointF((float)(div / 2), (float)(column.Depth - div / 2 - tick / 2)), (float)(column.Width - div + tick / 2), (float)(tick), DrawType: eDrawType.FillAndDraw);
                layers["Bars"].AddRectangle(new PointF((float)(div / 2), (float)(div / 2)), (float)(tick), (float)(column.Depth - div), DrawType: eDrawType.FillAndDraw);
                layers["Bars"].AddRectangle(new PointF((float)(column.Width - div / 2 - tick / 2), (float)(div / 2)), (float)(tick), (float)(column.Depth - div), DrawType: eDrawType.FillAndDraw);
                layers["Text"].AddText("A", new PointF((float)column.Width / 2, (float)(div / 2 + tick + layers["Text"].TextStyle.Height)));
                layers["Text"].AddText("A", new PointF((float)column.Width / 2, (float)(column.Depth - (div / 2 + tick + layers["Text"].TextStyle.Height))));
                layers["Text"].AddText("A", new PointF((float)(div / 2 + tick + layers["Text"].TextStyle.Height), (float)column.Depth / 2));
                layers["Text"].AddText("A", new PointF((float)(column.Width - (div / 2 + tick + layers["Text"].TextStyle.Height)), (float)column.Depth / 2));
            }
            else
                AddMainBars(0.05 * column.Depth);
        }

        private void AddDetailingDrawings()
        {
            System.Windows.Forms.Label l = new System.Windows.Forms.Label();
            Graphics g = l.CreateGraphics();

            string txt = column.NumberOfBars.ToString() + "Φ" + column.BarDiam.ToString();
            layers["Bars"].AddRectangle(new PointF((float)(column.Cover + column.StirrumDiam), (float)(column.Cover + column.StirrumDiam)),
                  (float)(column.Width - 2 * (column.Cover + column.StirrumDiam)), (float)(column.Depth - 2 * (column.Cover + column.StirrumDiam)));

            layers["Text"].AddText(txt, new PointF((float)(column.MinDim / 4 + column.Width + g.MeasureString(txt, layers["Text"].TextStyle).Width / 2), (float)column.Depth / 2));

            float[,] locns = column.GetReinforcements();
            for (int i = 0; i < column.NumberOfBars; i++)
            {
                eLine ll = layers["Text"].AddLine(new PointF(locns[i, 0], locns[i, 1]), new PointF((float)(column.MinDim / 4 + column.Width), (float)column.Depth / 2));
                ll.Color = new eColor(Color.DarkGray);
                ll.LineWeight = new eLineWeight(1.0f);
            }
            AddMainBars(column.BarDiam);
            AddDiagram();
            //txt = "Acalc = " + Math.Round(column.AsTotal, precision).ToString();
            //SizeF s = g.MeasureString(txt, layers["Text"].TextStyle);
            //layers["Text"].AddText(txt, new PointF((float)(column.MinDim / 4 + column.Width + s.Width / 2), (float)column.Depth / 2 + s.Height));
            //txt = "Aprov = " + Math.Round(column.AstProvided, precision).ToString();
            //s = g.MeasureString(txt, layers["Text"].TextStyle);
            //layers["Text"].AddText(txt, new PointF((float)(column.MinDim / 4 + column.Width + s.Width / 2), (float)column.Depth / 2 + 2 * s.Height));
            l.Dispose();
            g.Dispose();
        }

        private void AddDimensions()
        {
            layers["Dimension"].AddDim(new PointF(0, 0), new PointF(0, (float)column.Depth), Math.Round(eUtility.Convert(column.Depth, eUtility.SLU,lengthUnit), precision).ToString(), 
                                       eDimensionType.LinearVertical, eDimensionLinePosition.LeftOrAbove, (float)(column.MinDim / 8)).ArrowSize = 10;

            layers["Dimension"].AddDim(new PointF(0, (float)column.Depth), new PointF((float)column.Width, (float)column.Depth), Math.Round(eUtility.Convert(column.Width, eUtility.SLU, lengthUnit), precision).ToString(),
                                       eDimensionType.LinearHorizontal, eDimensionLinePosition.RightOrBottom, (float)(column.MinDim / 8)).ArrowSize = 10;        
        }

        private void Draw(eColumnDrawingStage drawingStage)
        {
            this.drawingStage = drawingStage;
            layers.ResetLayers();
            AddDrawings();
            modelForm.Invalidate();
        }

        private void AddMainBars(double diam)
        {
            float[,] locns = column.GetReinforcements();
            for (int i = 0; i < column.NumberOfBars; i++)
            {
                layers["Bars"].AddCircle(new PointF(locns[i, 0], locns[i, 1]), (float)(diam / 2), eDrawType.FillAndDraw);
            }
        }

        private void AddDiagram()
        {
            layers["Diagram"].AddDiagram(column, modelForm);
            layers["Diagram"].Zoom(new PointF(), (float)(column.Depth * 2 / 300));
            layers["Diagram"].Pan((float)column.Width * 2, -(float)column.Depth / 2);
        }

        private void ArrangeDrawings()
        {
            layers.Zoom(new PointF(), (float)(modelForm.ClientRectangle.Height / (column.Depth * 3)));
            layers.Pan(modelForm.ClientRectangle.Width / 2 - (float)column.Width, modelForm.ClientRectangle.Height / 2 - (float)column.Depth / 4);
        }

        /// <summary>
        /// Design the comlumn and display the detailing in the model form.
        /// </summary>
        public void Design()
        {
            try
            {
                column.Design();
                if (!column.DesingAndDetail)
                    Draw(eColumnDrawingStage.Design);
                else
                    Draw(eColumnDrawingStage.Detailing);
                return;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Modify Your Input", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Asterisk);
                Draw(eColumnDrawingStage.Modeling);
            }
        }

        /// <summary>
        /// Desplaye the medeling stage drawing in the model form.
        /// </summary>
        public void DisplayModel()
        {
            Draw(eColumnDrawingStage.Modeling);
        }


        public void AddLoad()
        {
            string txt = eUtility.ConvertFrom(column.Mx, eLengthUnits.m, eForceUints.KN).ToString() + "KNm";
            layers["Text"].AddText(txt, column.Width + column.Depth / 3, column.Depth / 2 + layers["Text"].TextStyle.Height);
            eLeader l;
            if (column.Action == eColumnAction.Biaxial)
            {
                l = layers["Text"].AddLeader(eLeaderType.Straight, "", false, new PointF((float)column.Width / 2, 0), new PointF((float)column.Width / 2, -(float)column.Depth / 3));
                l.SuppressDot = true;
                l.ArrowAndDotSize = 10;
                l = layers["Text"].AddLeader(eLeaderType.Straight, "", false, new PointF((float)column.Width / 2, 0), new PointF((float)column.Width / 2, -0.75f * (float)column.Depth / 3));
                l.SuppressDot = true;
                l.ArrowAndDotSize = 10;
                txt = eUtility.ConvertFrom((column as eBiaxial).My, eLengthUnits.m, eForceUints.KN).ToString() + "KNm";
                layers["Text"].AddText(txt, column.Width / 2, -column.Depth / 3 - layers["Text"].TextStyle.Height);
            }
            l = layers["Text"].AddLeader(eLeaderType.Straight, "", false, new PointF((float)column.Width, (float)column.Depth / 2), new PointF((float)(column.Width + column.Depth / 3), (float)column.Depth / 2));
            l.SuppressDot = true;
            l.ArrowAndDotSize = 10;
            l = layers["Text"].AddLeader(eLeaderType.Straight, "", false, new PointF((float)column.Width, (float)column.Depth / 2), new PointF((float)(column.Width + 0.75f * column.Depth / 3), (float)column.Depth / 2));
            l.SuppressDot = true;
            l.ArrowAndDotSize = 10;
        }

        public void ReleasHandlers()
        {
            layers.ReleaseHandlers();
            for (int i = 0; i < layers["Diagram"].DwgObjects.Count; i++)
            {
                if (layers["Diagram"].DwgObjects[i] is eInteractionDiagram)
                {
                    (layers["Diagram"].DwgObjects[i] as eInteractionDiagram).ReleasHandler();
                    break;
                }

            }
        }
         
        #endregion
    }
}
