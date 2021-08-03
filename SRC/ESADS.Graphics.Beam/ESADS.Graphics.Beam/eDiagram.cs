using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using ESADS.Mechanics.Analysis;
using ESADS.Mechanics.Analysis.Beam;
using System.Windows.Forms;
using ESADS.GUI;

namespace ESADS.EGraphics.Beam
{
    /// <summary>
    /// Represents graphs drawn for structures like BMD,SFD and Deflection diagram.
    /// </summary>
    public class eDiagram : eIDrawing
    {
        #region Fields

        /// <summary>
        /// Hols a value for property 'Beam_Analysis'.
        /// </summary>
        private eGBeam beam;
        /// <summary>
        /// Hols a value for property 'Interval'.
        /// </summary>
        private float interval;
        /// <summary>
        /// Hols a value for property 'Layer'.
        /// </summary>
        private eLayer layer;
        /// <summary>
        /// Hols a value for property 'LineType'.
        /// </summary>
        private eLineType lineType;
        /// <summary>
        /// Hols a value for property 'location'.
        /// </summary>
        private PointF location;
        /// <summary>
        /// Hols a value for property 'NegativeColor'.
        /// </summary>
        private eColor negColor;
        /// <summary>
        /// Hols a value for property 'PosetiveColor'.
        /// </summary>
        private eColor posColor;
        /// <summary>
        /// Hols a value for property 'ScaleFactor'.
        /// </summary>
        private double scaleFactor;
        /// <summary>
        /// Hols a value for property 'DiagramType'.
        /// </summary>
        private eDiagramType diagramType;
        /// <summary>
        /// Hols a value for property 'DiagramStyle'.
        /// </summary>
        private eDiagramStyle diagramStyle;
        /// <summary>
        /// Hols a value for property 'LineWeight'.
        /// </summary>
        private eLineWeight lineWeight;
        /// <summary>
        /// Hols a value for property 'Oreintation'.
        /// </summary>
        private eDiagramOreintation oreintation;
        /// <summary>
        /// Holds a value for public property 'LengthUnit'.
        /// </summary>
        private eLengthUnits lengthUnit
        {
            get
            {
                return beam.Document.LengthUnit;
            }
        }
        /// <summary>
        /// Holds a value for public property 'LengthUnit'.
        /// </summary>
        private eForceUints forceUnit
        {
            get
            {
                return beam.Document.ForceUnit;
            }
        }
        /// <summary>
        /// Holds the function that is used to draw the diagram.
        /// </summary>
        private eDiagramFunction function;
        /// <summary>
        /// Contains all the drawing used in the diagram.
        /// </summary>
        private List<eIDrawing> dwgs;
        /// <summary>
        /// Represents point on the beam_Analysis axis which show critical points in the diagram.
        /// </summary>
        private List<double> axisMarks;
        /// <summary>
        /// Holds a value for property 'ShowLables'.
        /// </summary>
        private bool showLables;
        /// <summary>
        /// Holds a objs_texts writen on the diagram.
        /// </summary>
        private List<eText> lables;
        /// <summary>
        /// Hold value for property 'TextStyle'.
        /// </summary>
        private eTextStyle textStyle;
        /// <summary>
        /// Hold value for property 'AxisColor'.
        /// </summary>
        private eColor axisColor;
        /// <summary>
        /// Hold a value for property 'Precision'.
        /// </summary>
        private int precision;
        /// <summary>
        /// Hold a value for property 'Arranged'.
        /// </summary>
        private bool arranged;
        /// <summary>
        /// contains the region of this diagram area.
        /// </summary>
        private Region region;
        /// <summary>
        /// Holds a value for how mach the drawing is enlarged from its original state.
        /// </summary>
        private float zoomState;
        /// <summary>
        /// Used to display orinate of the diagram at different absisa.
        /// </summary>
        private Label tracelable;
        /// <summary>
        /// Hold  value for public property 'Trace'.
        /// </summary>
        private bool trace;
        /// <summary>
        /// Hold the most recent location of the mouse.
        /// </summary>
        private Point prevLocation;
        /// <summary>
        /// 
        /// </summary>
        private PointF[] axisMarkPoints;

        #endregion

        #region Properties
        /// <summary>
        /// Gets or set the value indicating wheter the this diagram is arranged in the displaye window to satisfy some user requirments.
        /// </summary>
        public bool Arranged
        {
            get { return arranged; }
            set { arranged = value; }
        }

        /// <summary>
        /// Gets or sets the color of  the axis.
        /// </summary>
        public eColor AxisColor
        {
            get { return axisColor; }
            set { 
                axisColor = value;
                for (int i = 0; i < dwgs.Count; i++)
                {
                    if (dwgs[i].GetType() == typeof(eText) && (dwgs[i] as eText).ID == "A")
                        dwgs[i].Color = axisColor;
                    if (dwgs[i].GetType() == typeof(eLine) && (dwgs[i] as eLine).ID == "A")
                        dwgs[i].Color = axisColor;
                }
            }
        }

        /// <summary>
        /// Gets the coordinate of the starting point of the structure.
        /// </summary>
        public PointF Location
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// Gets or sets the layer on which this diagram found.
        /// </summary>
        public eLayer Layer
        {
            get { return this.layer; }
        }

        /// <summary>
        /// Gets the Beam_Analysis for which the the diagram is drawn.
        /// </summary>
        public eGBeam Beam
        {
            get
            {
                return beam;
            }
        }

        /// <summary>
        /// Gets or sets the Color of the negative part of the diagram.
        /// </summary>
        public eColor NegativeColor
        {
            get
            {
                return negColor;
            }
            set
            {
                for (int i = 0; i < dwgs.Count; i++)
                {
                    if (dwgs[i].GetType() == typeof(eText))
                    {
                        if (((dwgs[i] as eText).ID != "A") && (dwgs[i].Color == negColor))
                            dwgs[i].Color = value;
                    }
                    else if (dwgs[i].GetType() == typeof(ePolygone))
                    {
                        if ((dwgs[i] as ePolygone).FillColor.Value == System.Drawing.Color.FromArgb(100, negColor))
                            (dwgs[i] as ePolygone).FillColor = new eColor(System.Drawing.Color.FromArgb(100,value));
                    }
                    else if (dwgs[i].Color == negColor)
                        dwgs[i].Color = value;
                }
                negColor = value; 
            }
        }

        /// <summary>
        /// Gets or sets the Color the posetive part of the diagram.
        /// </summary>
        public eColor Color
        {
            get { return posColor; }
            set {
                for (int i = 0; i < dwgs.Count; i++)
                {
                    if (dwgs[i].GetType() == typeof(eText))
                    {
                        if (((dwgs[i] as eText).ID != "A") && (dwgs[i].Color == posColor))
                            dwgs[i].Color = value;
                    }
                    else if (dwgs[i].GetType() == typeof(ePolygone))
                    {
                        if ((dwgs[i] as ePolygone).FillColor.Value == System.Drawing.Color.FromArgb(100,posColor))
                            (dwgs[i] as ePolygone).FillColor = new eColor(System.Drawing.Color.FromArgb(100, value));
                    }
                    else if (dwgs[i].Color == posColor)
                        dwgs[i].Color = value;
                }
                posColor = value; 
            }
        }
      
        /// <summary>
        /// Gets or sets the line type used to draw the diagrams.
        /// </summary>
        public eLineType LineType
        {
            get
            {
                return lineType;
            }
            set
            {
                for (int i = 0; i < dwgs.Count - 4; i++)
                {
                    if (dwgs[i].GetType() == typeof(eLine))
                        (dwgs[i] as eLine).LineType = value;
                }
                lineType = value;
            }
        }

        /// <summary>
        /// Gets or sets the scale factor to multiply the ordinate of the diagram.
        /// </summary>
        public double ScaleFactor
        {
            get
            {
                return scaleFactor;
            }

            set
            {
                scaleFactor = value;
                if (dwgs.Count != 0)
                {
                    ReDrawDiagram();
                }
            }
        }

        /// <summary>
        /// Gets or sets the sub-divisions to draw the diagram.
        /// </summary>
        public float Interval
        {
            get
            {
                return interval;
            }
            set
            {
                interval = value;
                if (dwgs.Count != 0)
                {
                    ReDrawDiagram();
                }
            }
        }

        /// <summary>
        /// Gets or sets the type o the diagram.
        /// </summary>
        public eDiagramType DiagramType
        {
            get
            {
                return diagramType;
            }
        }

        /// <summary>
        /// Gets or sets the drawing style of the diagram.
        /// </summary>
        public eDiagramStyle DiagramStyle
        {
            get
            {
                return diagramStyle;
            }
            set
            {
                diagramStyle = value;
            }
        }

        /// <summary>
        /// Gets or sets the line weight used to draw the diagram
        /// </summary>
        public eLineWeight LineWeight
        {
            get
            {
                return lineWeight;
            }
            set
            {
                for (int i = 0; i < dwgs.Count - 4; i++)
                {
                    if (dwgs[i].GetType() == typeof(eLine))
                        (dwgs[i] as eLine).LineWeight = value;
                }
                lineWeight = value;
            }
        }

        /// <summary>
        /// Gets or sets the oreintation of the diagram.
        /// </summary>
        public eDiagramOreintation Oreintation
        {
            get { return oreintation; }
            set 
            {
                oreintation = value;
                if (dwgs.Count != 0)
                    ReDrawDiagram();
            }
        }

        /// <summary>
        /// Gets or sets the value indicating whether the lables should be shown or not.
        /// </summary>
        public bool ShowLables
        {
            get { return showLables; }
            set
            {
                showLables = value;
            }
        }

        /// <summary>
        /// Gets or set the text style of lables that apear on the diagram.
        /// </summary>
        public eTextStyle TextStyle
        {
            get { return textStyle; }
            set
            {
                textStyle = value;
                for (int i = 0; i < dwgs.Count - 4; i++)
                {
                    if (dwgs[i].GetType() == typeof(eText))
                        (dwgs[i] as eText).TextStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the precision after decimal palce for values in the diagram
        /// </summary>
        public int Precision
        {
            get { return precision; }
            set
            {
                precision = value;
                ReDrawDiagram();
            }
        }

        /// <summary>
        /// Gets or sets the value indicatin wheter to trace on the diagram or not.
        /// </summary>
        public bool Trace
        {
            get { return trace; }
            set { trace = value; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eDiagrams class from the given basic componets.
        /// </summary>
        /// <param name="beam_Analysis">The graphic beam_Analysis to initialize the layer_ShearBars diagram.</param>
        /// <param name="diagramType"></param>
        /// <param name="layer"></param>
        /// <param name="dwgForm"></param>
        public eDiagram(eGBeam beam, eDiagramType diagramType, eLayer layer, eModelForm dwgForm)
        {
            this.beam = beam;
            this.layer = layer;
            this.layer.LayerOn = true;
            this.lineWeight = layer.LineWeight;
            this.lineType = layer.LineType;
            this.diagramType = diagramType;
            this.textStyle = layer.TextStyle;
            this.tracelable = new Label();
            this.tracelable.BackColor = System.Drawing.Color.Beige;
            this.tracelable.ForeColor = dwgForm.BackColor;
            this.tracelable.Text = "";
            this.tracelable.AutoSize = true;
            this.tracelable.Visible = false;
            this.location = new PointF();
            dwgForm.MouseMove += new MouseEventHandler(dwgForm_MouseMove);
            dwgForm.MouseClick += new MouseEventHandler(dwgForm_MouseClick);
            dwgForm.MouseDoubleClick += new MouseEventHandler(dwgForm_MouseDoubleClick);
            dwgForm.UnitChanged += new eUnitChangedEventHandler(dwgForm_UnitChanged);
            dwgForm.Controls.Add(tracelable);   
            InitializeComponets();        
        }

        private void dwgForm_UnitChanged(object sender, eUnitChangedEventArgs e)
        {
            
            this.ReDrawDiagram();
        }

      
        #endregion

        #region Methods

        /// <summary>
        /// Initailizes basic componets with their default value.
        /// </summary>
        private void InitializeComponets()
        {
            this.oreintation = eDiagramOreintation.PosY_Upward;
            this.dwgs = new List<eIDrawing>();
            this.posColor = new eColor(System.Drawing.Color.Yellow, eChangeBy.ByObject);
            this.negColor = new eColor( System.Drawing.Color.Red, eChangeBy.ByObject);
            this.showLables = true;
            this.precision = 2;
            this.interval = 100;
            this.axisColor = new eColor(System.Drawing.Color.White);        
            this.lables = new List<eText>();
            this.axisMarks = new List<double>();
            this.axisMarkPoints = new PointF[0];
            if (this.diagramType == eDiagramType.SFD)
               this.function = beam.Beam_Analysis.GetShearAt;
            else if (this.diagramType == eDiagramType.BMD)
                 this.function = beam.Beam_Analysis.GetMomentAt;
            this.arranged = true;
            this.zoomState = 1;
            this.region = new Region(new Rectangle(0, 0, 0, 0));
            this.trace = true;
        }

        /// <summary>
        /// Zooms the diagram on which it is called.
        /// </summary>
        /// <param name="ZoomCenter">The zoom center from which zooming is done.</param>
        /// <param name="ZoomFactor">The factor by which the drawing is elarged.</param>
        public void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            this.region = new Region(new Rectangle(0, 0, 0, 0));
            foreach (eIDrawing d in dwgs)
            {
                d.Zoom(ZoomCenter, ZoomFactor);
                if (d.GetType() == typeof(ePolygone))
                {
                    GraphicsPath gp = new GraphicsPath();
                    gp.AddPolygon((d as ePolygone).Points);
                    region.Union(gp);
                }
            }
            this.location.X = ZoomFactor * (this.location.X - ZoomCenter.X) + ZoomCenter.X;
            this.location.Y = ZoomFactor * (this.location.Y - ZoomCenter.Y) + ZoomCenter.Y;
            for (int i = 0; i < axisMarkPoints.Length; i++)
            {
                this.axisMarkPoints[i].X = ZoomFactor * (this.axisMarkPoints[i].X - ZoomCenter.X) + ZoomCenter.X;
                this.axisMarkPoints[i].Y = ZoomFactor * (this.axisMarkPoints[i].Y - ZoomCenter.Y) + ZoomCenter.Y;
            }
            this.tracelable.Location = new Point(prevLocation.X,(int) this.location.Y);
            this.zoomState *= ZoomFactor;
        }

        /// <summary>
        /// Pans or moves the diagram by the specifeid offesets in both axis.
        /// </summary>
        /// <param name="XOffset">The distance moved in x-direction</param>
        /// <param name="YOffset">The distance moved in y-direction</param>
        public void Pan(float XOffset, float YOffset)
        {
            this.region = new Region(new Rectangle(0, 0, 0, 0));

            foreach (eIDrawing d in dwgs)
            {
                d.Pan(XOffset, YOffset);
                if (d.GetType() == typeof(ePolygone))
                {
                    GraphicsPath gp = new GraphicsPath();
                    gp.AddPolygon((d as ePolygone).Points);
                    region.Union(gp);
                }
            }
            this.location.X += XOffset;
            this.location.Y += YOffset;
            for (int i = 0; i < axisMarkPoints.Length; i++)
            {
                this.axisMarkPoints[i].X += XOffset;
                this.axisMarkPoints[i].Y += YOffset;
            }
        }
      
        /// <summary>
        /// Draws the diagram on which it is called.
        /// </summary>
        /// <param name="g">The graphic object on which drawing is done.</param>
        public void Draw(Graphics g)
        {
            for (int i = 0; i < dwgs.Count; i++)
            {
                if (dwgs[i].GetType() == typeof(ePolygone))
                {
                    if (diagramStyle == eDiagramStyle.Fill)
                        dwgs[i].Draw(g);
                }
                else if (dwgs[i].GetType() == typeof(eLine))
                {
                    if (diagramStyle == eDiagramStyle.Slice)
                        dwgs[i].Draw(g);
                    else if ((dwgs[i] as eLine).ID != "Slice")
                        dwgs[i].Draw(g);
                }
                else if (dwgs[i].GetType() == typeof(eText))
                {
                    if (showLables || (dwgs[i] as eText).ID == "T")
                        dwgs[i].Draw(g);
                }
            }
            DrawAxisMarks(g);
        }

        /// <summary>
        /// Handles the mouse Double click event of the Form on which this diagram found.
        /// </summary>
        /// <param name="sender">Object sending this event.</param>
        /// <param name="e">Event argument containing information realated to the event.</param>
        private void dwgForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(region.IsVisible(e.Location))
            MessageBox.Show("Stress Diagram dialog will be displayed.");
        }

        /// <summary>
        /// Handles the mouse Double click event of the Form on which this diagram found.
        /// </summary>
        /// <param name="sender">Object sending this event.</param>
        /// <param name="e">Event argument containing information realated to the event.</param>
        private void dwgForm_MouseClick(object sender, MouseEventArgs e)
        {
        }

        /// <summary>
        /// Handles the mouse Double click event of the Form on which this diagram found.
        /// </summary>
        /// <param name="sender">Object sending this event.</param>
        /// <param name="e">Event argument containing information realated to the event.</param>
        private void dwgForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!layer.LayerOn)
                return;

            if (region.IsVisible(e.Location) && trace)
            {
                float x = (float)e.Location.X - this.location.X;
                x /= this.zoomState;
                if (x < 0 || x > this.beam.Beam_Analysis.Length)
                    return;
                tracelable.Visible = true;
                if (diagramType == eDiagramType.SFD)
                    tracelable.Text = "X = " + Math.Round(eUtility.Convert(x, eUtility.SLU, lengthUnit), precision).ToString() + "\n" + "V = " + Math.Round(eUtility.Convert(function(x), eUtility.SFU, forceUnit), precision).ToString();
                else
                    tracelable.Text = "X = " + Math.Round(eUtility.Convert(x, eUtility.SLU, lengthUnit), precision).ToString() + "\n" + "M = " + Math.Round(eUtility.Convert(function(x), eUtility.SLU, lengthUnit, eUtility.SFU, forceUnit), precision).ToString();

                if (function(x) > 0)
                {
                    if (oreintation == eDiagramOreintation.PosY_Upward)
                        tracelable.Location = F(new Point(e.X, (int)this.location.Y));
                    else
                        tracelable.Location = new Point(e.X, (int)this.location.Y - tracelable.Height);
                }
                else
                {
                    if (oreintation == eDiagramOreintation.PosY_Upward)
                        tracelable.Location = F(new Point(e.X, (int)this.location.Y - tracelable.Height));
                    else
                        tracelable.Location = new Point(e.X, (int)this.location.Y);
                }
            }
            else
                tracelable.Visible = false;
            prevLocation = e.Location;
        }

        /// <summary>
        /// Returns a point to be drawn on the graphic media based on the oreintation of the diagram.
        /// </summary>
        /// <param name="p">Point in absolute coordinated system.</param>
        /// <returns></returns>
        private PointF F(PointF p)
        {
            if (oreintation == eDiagramOreintation.PosY_Upward)
                return new PointF(p.X, -p.Y);
            else
                return p;
        }

        /// <summary>
        /// Adds all the necessary drawings for the diagram.
        /// </summary>
        internal void AddDrawings()
        {
            double posF, negF, posX, negX;
            PointF[] points = GetPoints();
            for (int i = 0; i < points.Length - 1; i++)
            {
                eLine line = new eLine(F(points[i]), F(points[i + 1]), this.layer);
                line.Color = GetColor(points[i], points[i + 1]);
                line.LineType = this.lineType;
                line.LineWeight = this.lineWeight;
                dwgs.Add(line);
            }
            AddPolygons(points);
            AddSlices();
            RemovRepeated(axisMarks);
            AddLables();

            eLine xAxis = new eLine(new PointF(0 ,0), new PointF((float)beam.Beam_Analysis.Length + 100,0), this.layer);
            eText xtext = new eText("X", new PointF(xAxis.End.X + textStyle.GetSizeOf("   X").Width / 2, 0), this.layer);
            xtext.ID = "A";
            xtext.Color = posColor;
            xAxis.ID = "A";
            xtext.ID = "T";
            xAxis.Color = axisColor;
         
            this.dwgs.Add(xAxis);
            this.dwgs.Add(xtext);

            eText yText;
            if (diagramType == eDiagramType.SFD)
            {
                posF = this.beam.Beam_Analysis.GetMaxShear(out negF, out posX, out negX);
                yText = new eText("V(x)", F(new PointF(0, (float)(scaleFactor * posF) + 100 + textStyle.GetSizeOf("V(x)").Height / 2)), this.layer);
                yText.Color = posColor;
            }
            else
            {
                posF = this.beam.Beam_Analysis.GetMaxMoment(out negF, out posX, out negX);
                yText = new eText("M(x)", F(new PointF(0, (float)(scaleFactor * posF) + 100 + textStyle.GetSizeOf("M(x)").Height/2)), this.layer);
                yText.Color = posColor;
            }

            eLine yAxis = new eLine(F(new PointF(0, (float)(scaleFactor * posF) + 100)), F(new PointF(0, (float)(scaleFactor * negF) - 100)), this.layer);
            yAxis.Color = axisColor;
            yText.ID = "T";
            yAxis.ID = "A";
            this.dwgs.Add(yAxis);
            this.dwgs.Add(yText);
            AddAxisPoints();
        }

        /// <summary>
        /// Returns the color of the line connecting two point base on their stress value.
        /// </summary>
        ///<param name="start">The start point of the line.</param>
        ///<param name="end">The end point of the line.</param>
        private eColor GetColor(PointF start, PointF end)
        {
            if ((start.Y + end.Y) / 2 > 0)
                return posColor;
            else if ((start.Y + end.Y) / 2 < 0)
                return negColor;
            else
                return new eColor(System.Drawing.Color.Transparent);
        }

        /// <summary>
        /// Draws the axis marks on the beam_Analysis axis.
        /// </summary>
        /// <param name="g">The graphic object on which the drawing is done.</param>
        private void DrawAxisMarks(Graphics g)
        {
            if (showLables)
            {
                for (int i = 0; i < axisMarkPoints.Length; i++)
                {
                    g.DrawLine(new Pen(axisColor, lineWeight), axisMarkPoints[i].X, axisMarkPoints[i].Y - 5f, axisMarkPoints[i].X, axisMarkPoints[i].Y + 5f);
                }
            }
        }

        /// <summary>
        /// Fills the diagram for the given member points.
        /// </summary>
        /// <param name="points">Points used to draw the diagram for the member.</param>
        private void AddPolygons(PointF[] points)
        {
            region = new Region(new Rectangle(0, 0, 0, 0));
           
            PointF[] polyPoints = new PointF[5];
            ePolygone p;
            for (int i = 0; i < points.Length-1; i++)
            {
                polyPoints[0] = F(new PointF(points[i].X, 0));
                polyPoints[1] = F(points[i]);
                polyPoints[2] = F(points[i + 1]);
                polyPoints[3] = F(new PointF(points[i + 1].X, 0));
                polyPoints[4] = F(new PointF(points[i].X, 0));
                GraphicsPath gp = new GraphicsPath();
                gp.AddPolygon(polyPoints);
                region.Union(gp);
                p = new ePolygone(polyPoints, eDrawType.Fill, this.layer);
                p.FillColor = new eColor(System.Drawing.Color.FromArgb(100, GetColor(points[i], points[i + 1])), eChangeBy.ByObject);
                this.dwgs.Add(p);
            }
        }

        /// <summary>
        /// Adds the vertical slices in the drawing for eDiagramStyle.Slice.
        /// </summary>
        private void AddSlices()
        {
            double X = 0;
            while (X < beam.Beam_Analysis.Length)
            {
                eLine line = new eLine(F(new PointF((float)X, (float)(scaleFactor * function(X)))),F( new PointF((float)X, 0)), this.layer);
                line.Color = GetColor(F(line.Location), F(line.End));
                line.LineType = this.lineType;
                line.LineWeight = this.lineWeight;
                line.ID = "Slice";
                this.dwgs.Add(line);
                X += interval;
            }
        }

        /// <summary>
        /// Redraws the digram when ever a property is chaned.
        /// </summary>
        private void ReDrawDiagram()
        {

            this.location = new PointF(0, 0);
            this.axisMarks = new List<double>();
            this.dwgs = new List<eIDrawing>();
            this.lables = new List<eText>();
            this.region = new Region();
            this.axisMarkPoints = new PointF[0];
            this.zoomState = 1;
            this.AddDrawings();
            this.ArrageDiagram();
        }

        /// <summary>
        /// Adds lables to the Diagram.
        /// </summary>
        private void AddLables()
        {
            eText yText;
            eText xText;
            PointF px, py;
            string txt;
            for (int i = 0; i < axisMarks.Count; i++)
            {
                if (function(axisMarks[i]) > 0)
                {
                    px = new PointF((float)axisMarks[i], -textStyle.GetSizeOf("A").Height / 2);
                    if (Math.Round(function(axisMarks[i]),precision) != 0)
                    {
                        double r = Math.Round(function(axisMarks[i]));
                        py = new PointF((float)axisMarks[i], (float)(scaleFactor * function(axisMarks[i])) + textStyle.GetSizeOf("A").Height / 2);
                        if (diagramType == eDiagramType.SFD)
                            txt = Math.Round(eUtility.Convert(function(axisMarks[i]), eUtility.SFU, this.forceUnit), precision).ToString();
                        else
                            txt = Math.Round(eUtility.Convert(function(axisMarks[i]), eUtility.SLU, this.lengthUnit, eUtility.SFU, this.forceUnit), precision).ToString();
                        yText = new eText(txt, F(py), this.layer);
                        yText.Color = posColor;
                        this.dwgs.Add(yText);
                    }
                }
                else
                {
                    px = new PointF((float)axisMarks[i], textStyle.GetSizeOf("A").Height / 2);
                    if (Math.Round(function(axisMarks[i])) != 0)
                    {
                        double y = Math.Round(function(axisMarks[i]));
                        py = new PointF((float)axisMarks[i], (float)(scaleFactor * function(axisMarks[i])) - textStyle.GetSizeOf("A").Height / 2);
                        if (diagramType == eDiagramType.SFD)
                            txt = Math.Round(eUtility.Convert(function(axisMarks[i]), eUtility.SFU, this.forceUnit), precision).ToString();
                        else
                            txt = Math.Round(eUtility.Convert(function(axisMarks[i]), eUtility.SLU, this.lengthUnit, eUtility.SFU, this.forceUnit), precision).ToString();
                        yText = new eText(txt, F(py), this.layer);
                        yText.Color = negColor;
                        this.dwgs.Add(yText);
                    }
                }
                if (axisMarks[i] != 0)
                {
                    xText = new eText(Math.Round(eUtility.Convert(axisMarks[i], eUtility.SLU, this.lengthUnit), precision).ToString(), F(px), this.layer);
                    xText.Color = axisColor;
                    xText.ID = "A";
                    this.dwgs.Add(xText);
                }
                try
                {
                    if (Math.Round(function(axisMarks[i])) != Math.Round(function(axisMarks[i], eSectionAt.FromRight)))
                    {
                        if (function(axisMarks[i],eSectionAt.FromRight) > 0)
                        {
                            if (Math.Round(function(axisMarks[i],eSectionAt.FromRight)) != 0)
                            {
                                py = new PointF((float)axisMarks[i], (float)(scaleFactor * function(axisMarks[i],eSectionAt.FromRight)) + textStyle.GetSizeOf("A").Height / 2);
                                if (diagramType == eDiagramType.SFD)
                                    txt = Math.Round(eUtility.Convert(function(axisMarks[i],eSectionAt.FromRight), eUtility.SFU, this.forceUnit), precision).ToString();
                                else
                                    txt = Math.Round(eUtility.Convert(function(axisMarks[i],eSectionAt.FromRight), eUtility.SLU, this.lengthUnit, eUtility.SFU, this.forceUnit), precision).ToString();
                                yText = new eText(txt, F(py), this.layer);
                                yText.ID = "Variable";
                                yText.Color = posColor;
                                this.dwgs.Add(yText);
                            }
                        }
                        else
                        {
                            if (Math.Round(function(axisMarks[i],eSectionAt.FromRight)) != 0)
                            {
                                py = new PointF((float)axisMarks[i], (float)(scaleFactor * function(axisMarks[i], eSectionAt.FromRight)) - textStyle.GetSizeOf("A").Height / 2);
                                if (diagramType == eDiagramType.SFD)
                                    txt = Math.Round(eUtility.Convert(function(axisMarks[i],eSectionAt.FromRight), eUtility.SFU, this.forceUnit), precision).ToString();
                                else
                                    txt = Math.Round(eUtility.Convert(function(axisMarks[i],eSectionAt.FromRight), eUtility.SLU, this.lengthUnit, eUtility.SFU, this.forceUnit), precision).ToString();
                                yText = new eText(txt, F(py), this.layer);
                                yText.Color = negColor;
                                yText.ID = "Variable";
                                this.dwgs.Add(yText);
                            }
                        }
                    }
                }
                catch { };
            }
        }

        /// <summary>
        /// Returns all point necessary to draw a diagram.
        /// </summary>
        /// <returns></returns>
        private PointF[] GetPoints()
        {
            List<PointF> points = new List<PointF>();            
            double X = 0;
            AddPoint(new PointF(0, 0),points);
            while (X < this.beam.Beam_Analysis.Length )
            {
                points.Add(new PointF((float)X, (float)(scaleFactor * function(X))));
                X += interval;
            }
            AddSectionIntervalPoints(points);
            AddMaxPoints(points);
            AddZeroPoints(points);
            points.Add(new PointF((float)this.beam.Beam_Analysis.Length, 0));
            return points.ToArray() as PointF[];
        }

        /// <summary>
        /// Add section points of section interval in the points array.
        /// </summary>
        /// <param name="points">List of wher section interval are going to be added.</param>
        private void AddSectionIntervalPoints(List<PointF>points)
        {
            double[] intervals = this.beam.Beam_Analysis.GetSectionIntervals(); 
            axisMarks.AddRange(intervals);
            for (int i = 1; i < intervals.Length - 1; i++)
            {
                AddPoint(new PointF((float)intervals[i], (float)(scaleFactor * function(intervals[i]))), points);
                PointF p = new PointF((float)intervals[i], (float)(scaleFactor * function(intervals[i], eSectionAt.FromRight)));
                AddPoint(p, points);
                if ((function(intervals[i]) > 0 && function(intervals[i], eSectionAt.FromRight) < 0) || (function(intervals[i]) < 0 && function(intervals[i], eSectionAt.FromRight) > 0))
                    points.Insert(points.IndexOf(p), new PointF((float)intervals[i], 0));
            }
            AddPoint(new PointF((float)intervals[intervals.Length - 1], (float)(scaleFactor * function(intervals[intervals.Length - 1]))), points);
        }

        /// <summary>
        /// Adds point of maxismum ordinate in the given continious intervals.
        /// </summary>
        /// <param name="points">Lis of point in which point of max ordinates are going to be added.</param>
        private void AddMaxPoints(List<PointF> points)
        {
            double negF, posX, negX, X = 0;
            foreach (eAMember m in this.beam.Beam_Analysis.Members)
            {
                m.GetMaxMoment(out negF, out posX, out negX);
                if (posX > 0 && posX < m.Length)
                {
                    AddPoint(new PointF((float)(X + posX), (float)(scaleFactor * function(X + posX))), points);
                    if (!axisMarks.Contains(X + posX))
                        axisMarks.Add(X + posX);
                }
                if (negX > 0 && negX < m.Length)
                {
                    AddPoint(new PointF((float)(X + negX), (float)(scaleFactor * function(X + negX))), points);
                    if (!axisMarks.Contains(X +negX))
                        axisMarks.Add(X + negX);
                }                               
                X += m.Length;
            }
        }

        /// <summary>
        /// Adds point of zero ordinated.
        /// </summary>
        /// <param name="points">Lis of points to add point of zero ordinate.</param>
        private void AddZeroPoints(List<PointF> points)
        {
            double[] Xc;
            double[] Intervals;
            double X = 0;
            foreach (eAMember m in this.beam.Beam_Analysis.Members)
            {
                Intervals = m.GetSectionsInterval();
                for (int i = 0; i < Intervals.Length - 1; i++)
                {
                    if (diagramType == eDiagramType.SFD)
                        Xc = m.GetShearZero(Intervals[i], Intervals[i + 1]);
                    else
                        Xc = m.GetMomentZero(Intervals[i], Intervals[i + 1]);
                    if (Xc != null)
                    {
                        for (int j = 0; j < Xc.Length; j++)
                        {
                            AddPoint(new PointF((float)(X + Xc[j]), (float)(scaleFactor * function(X + Xc[j]))), points);
                            if (!axisMarks.Contains(X + Xc[j]))
                                axisMarks.Add(X + Xc[j]);
                        }
                    }
                }
                X += m.Length;
            }
        }

        /// <summary>
        /// Adds a point to the given collection int its sorted order ckecking if it is not repeated.
        /// </summary>
        /// <param name="p">The point ot be added.</param>
        /// <param name="points">Listo to point to add the new point.</param>
        private void AddPoint(PointF p, List<PointF> points)
        {
            if (!points.Contains(p))
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].X > p.X)
                    {
                        points.Insert(i, p);
                        return;
                    }                   
                }
                points.Add(p);
            }
        }

        /// <summary>
        /// Arranges the diagram.
        /// </summary>
        internal void ArrageDiagram()
        {
            double posF, posX, negF, negX, tempF;
            float yOffset;

            if (diagramType == eDiagramType.SFD)
            {
                beam.Layers["SFD"].RemoveObjects();
                beam.Layers["SFD"].DwgObjects.Add(this);
                float factor = beam.Layers.Scale;                
                AddDrawings();
                posF = beam.Beam_Analysis.GetMaxShear(out negF, out posX, out negX);
                this.Zoom(this.location, factor);
                if (oreintation == eDiagramOreintation.PosY_Upward)
                    this.Pan(beam.Joints[0].Location.X, beam.Joints[0].Location.Y + (float)(posF * scaleFactor * factor) + beam.MaxTotalNegOffset + 100 * factor + textStyle.GetSizeOf("V(x)").Height * factor);
                else
                    this.Pan(beam.Joints[0].Location.X, beam.Joints[0].Location.Y + (float)(Math.Abs(negF) * scaleFactor * factor) + beam.MaxTotalNegOffset + 100 * factor);
            }
            else
            {
                beam.Layers["BMD"].RemoveObjects();
                beam.Layers["BMD"].DwgObjects.Add(this);
                float factor = beam.Layers.Scale;    
                AddDrawings();
                posF = beam.Beam_Analysis.GetMaxMoment(out negF, out posX, out negX);
                if (beam.SFD == null)
                {
                    this.Zoom(this.location, factor);
                    if (oreintation == eDiagramOreintation.PosY_Upward)
                        this.Pan(beam.Joints[0].Location.X, (float)(posF * scaleFactor * factor) + beam.MaxTotalNegOffset + 100 * factor + textStyle.GetSizeOf("M(x)").Height * factor);
                    else
                        this.Pan(beam.Joints[0].Location.X, (float)(Math.Abs(negF) * scaleFactor * factor) + beam.MaxTotalNegOffset + 100 * factor);
                }
                else
                {
                    posF = beam.Beam_Analysis.GetMaxMoment(out negF, out posX, out negX);
                    tempF = beam.Beam_Analysis.GetMaxShear(out negF, out posX, out negX);
                    if (oreintation == eDiagramOreintation.PosY_Upward)
                    {
                        if (beam.SFD.oreintation == eDiagramOreintation.PosY_Upward)
                            yOffset = beam.SFD.Location.Y + (float)(Math.Abs(negF) * beam.SFD.ScaleFactor * factor) + 400 * factor + (float)(posF * this.ScaleFactor * factor);
                        else
                            yOffset = beam.SFD.Location.Y + (float)(tempF * beam.SFD.ScaleFactor * factor) + 400 * factor + (float)(posF * this.ScaleFactor * factor);
                    }
                    else
                    { 
                        posF = beam.Beam_Analysis.GetMaxMoment(out negF, out posX, out negX);
                        if (beam.SFD.oreintation == eDiagramOreintation.PosY_Upward)
                           
                            yOffset = beam.SFD.Location.Y + (float)(Math.Abs(negF) * beam.SFD.ScaleFactor * factor) + 400 * factor + (float)(Math.Abs(negF) * this.ScaleFactor * factor);
                        else
                            yOffset = beam.SFD.Location.Y + (float)(tempF * beam.SFD.ScaleFactor * factor) + 400 * factor + (float)(Math.Abs(negF) * this.ScaleFactor * factor);
                    }
                    this.Zoom(this.location, factor);
                    this.Pan(this.beam.SFD.location.X, yOffset);
                }              
            }           
        }

        /// <summary>
        /// Inverst the y-ordinate axis based on the the oreintation of the diagram.
        /// </summary>
        /// <param name="p">The point to be inverted.</param>
        /// <returns></returns>
        private Point F(Point p)
        {
            if (oreintation == eDiagramOreintation.PosY_Downward)
                return new Point(p.X, -p.Y);
            else return p;
        }

        /// <summary>
        /// Adds axis mark points.
        /// </summary>
        public void AddAxisPoints()
        {
            axisMarkPoints = new PointF[axisMarks.Count];
            for (int i = 0; i < axisMarks.Count; i++)
            {
                axisMarkPoints[i] = new PointF((float)axisMarks[i], 0);
            }
        }

        /// <summary>
        /// Removes the repeated values from the list.
        /// </summary>
        /// <param name="array">The list containign the values.</param>
        private void RemovRepeated(List<double> array)
        {
            for (int i = 0; i < array.Count; i++)
            {
                for (int j = i + 1; j < array.Count; j++)
                {
                    if (Math.Round(array[i]) == Math.Round(array[j]))
                    {
                        array.RemoveAt(j);
                        j--;
                    }
                }
            }
        }

        /// <summary>
        /// Releases all the event handlers of the drawing Form added for this diagram.
        /// </summary>
        /// <param name="dwgForm"></param>
        internal void ReleaseHandlers(eModelForm dwgForm)
        {
            dwgForm.MouseMove -= new MouseEventHandler(dwgForm_MouseMove);
            dwgForm.MouseClick -= new MouseEventHandler(dwgForm_MouseClick);
            dwgForm.MouseDoubleClick -= new MouseEventHandler(dwgForm_MouseDoubleClick);
            dwgForm.UnitChanged -= new eUnitChangedEventHandler(dwgForm_UnitChanged);
            dwgForm.Controls.Remove(tracelable);
        }

        /// <summary>
        /// Returns the minimum point in the diagram.
        /// </summary>
        /// <returns></returns>
        internal  PointF GetMinPoint()
        {
            for( int i = 0 ;i<dwgs.Count;i++)
            {
                if (dwgs[i].GetType() == typeof(eLine) && (dwgs[i] as eLine).ID == "A")
                {
                    if ((dwgs[i] as eLine).End.Y > (dwgs[i] as eLine).Location.Y)
                        return (dwgs[i] as eLine).End;
                    else
                        return (dwgs[i] as eLine).Location;
                }
            }
            return new PointF();
        }

        #endregion
     
    }
}
