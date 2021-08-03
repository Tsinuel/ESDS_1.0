using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ESADS.GUI.Controls;
using ESADS.GUI;
using ESADS.Mechanics.Analysis;
using ESADS.Mechanics.Analysis.Beam;

namespace ESADS.EGraphics.Beam
{

    /// <summary>
    /// Represents the graphic elements of a triangularly distributed force load on a member.
    /// </summary>
    public class eGTriangularLoad : eGLoad
    {
        /// <summary>
        /// Holds the value of the 'Load' property.
        /// </summary>
        private eTriangularLoad load;
        /// <summary>
        /// List of all the necessary drawing objects for the member drawing.
        /// </summary>
        private List<eIDrawing> dwgObjects;
        /// <summary>
        /// The text_left that represents the magnitude of the load_Tri.
        /// </summary>
        private eText text;
        /// <summary>
        /// The grip box at the left end of the load_Tri dwg.
        /// </summary>
        private eGripBox grip_Left;
        /// <summary>
        /// The grip box at the right end of the load_Tri dwg.
        /// </summary>
        private eGripBox grip_Right;
        /// <summary>
        /// The grip box used to move the whole load_Tri.
        /// </summary>
        private eGripBox grip_middle;
        /// <summary>
        /// The diagonal line at the top of the load_Tri.
        /// </summary>
        private eLine topLine;
        /// <summary>
        /// The membDimension to indicate the length of the load_Tri distribution dinamically.
        /// </summary>
        //private eDimension dim_middle;
        /// <summary>
        /// The point at the end of the load_Tri at its intersection with the member.
        /// </summary>
        private PointF endPt;

        ///<summary>
        ///Creates new drawing object to represent a triangular member on a member.
        /// </summary>
        /// <param name="member">The mechanics object which the drawing is going to represent.</param>
        /// <param name="location">The far left point of intersection of the member with the member on which it is loaded on.</param>
        /// <param name="layer">The layer on which the member is drawn.</param>
        /// <param name="dwgForm">The form on which the member is shown to the user.</param>
        public eGTriangularLoad(eTriangularLoad load, eLayer layer, eGMember membDWG, ESADS.GUI.eModelForm dwgForm)
            : base(membDWG, layer, dwgForm)
        {
            this.member.Beam.Changed += new EventHandler(Beam_Changed);
            this.load = load;
            this.location = new PointF((float)(membDWG.Start.X + (load.Start * (membDWG.End.X - membDWG.Start.X) / load.Member.Length)), membDWG.Start.Y);
            this.endPt = new PointF((float)(membDWG.Start.X + ((load.Member.Length - load.End) * (membDWG.End.X - membDWG.Start.X) / (load.Member.Length))), location.Y);

            GenerateDwgObjects();

            InitializeComponents(membDWG, dwgForm);
        }

        private void Beam_Changed(object sender, EventArgs e)
        {
            GenerateDwgObjects();
        }
    
        public override PointF Location
        {
            get
            {
                return location;
            }
            set
            {
            }
        }
        public override eColor Color
        {
            get
            {
                return text.Color;
            }
            set
            {
                text.Color = value;
                topLine.Color = value;

                foreach (eIDrawing dwg in dwgObjects)
                    dwg.Color = value;
            }
        }
        /// <summary>
        /// Gets the mechanics object associated with this member drawing.
        /// </summary>
        public eTriangularLoad Load
        {
            get
            {
                return load;
            }
        }

        public override void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            base.Zoom(ZoomCenter, ZoomFactor);

            foreach (eIDrawing dwg in dwgObjects)
            {
                dwg.Zoom(ZoomCenter, ZoomFactor);
            }

            endPt.X = ZoomFactor * (endPt.X - ZoomCenter.X) + ZoomCenter.X;
            endPt.Y = ZoomFactor * (endPt.Y - ZoomCenter.Y) + ZoomCenter.Y;

            topLine.Zoom(ZoomCenter, ZoomFactor);
            text.Zoom(ZoomCenter, ZoomFactor);

            grip_Left.Zoom(ZoomCenter, ZoomFactor);
            grip_middle.Zoom(ZoomCenter, ZoomFactor);
            grip_Right.Zoom(ZoomCenter, ZoomFactor);

            grip_Left.Location = location;
            grip_Right.Location = endPt;
            grip_middle.Location = new PointF((endPt.X + location.X) / 2.0f, location.Y);

            CreateRegion();
        }

        public override void Pan(float Xoffset, float Yoffset)
        {
            base.Pan(Xoffset, Yoffset);

            foreach (eIDrawing dwg in dwgObjects)
            {
                dwg.Pan(Xoffset, Yoffset);
            }

            endPt.X += Xoffset;
            endPt.Y += Yoffset;

            topLine.Pan(Xoffset, Yoffset);
            text.Pan(Xoffset, Yoffset);

            grip_Left.Pan(Xoffset, Yoffset);
            grip_middle.Pan(Xoffset, Yoffset);
            grip_Right.Pan(Xoffset, Yoffset);

            grip_Left.Location = location;
            grip_Right.Location = endPt;
            grip_middle.Location = new PointF((endPt.X + location.X) / 2.0f, location.Y);

            CreateRegion();
        }

        public override void Draw(Graphics g)
        {
            if (!visible)
                return;
            foreach (eIDrawing dwg in dwgObjects)
            {
                dwg.Draw(g);
            }

            topLine.Draw(g);
            text.Draw(g);

            grip_Left.Draw(g);
            grip_middle.Draw(g);
            grip_Right.Draw(g);
        }

        /// <summary>
        /// Generates all the necessary drawing objects used in the member drawing.
        /// </summary>
        protected override void GenerateDwgObjects()
        {
            dwgObjects = new List<eIDrawing>();

            float h = (float)((member.Beam.Extent_V * layer.ZoomFactor * 0.1) +
                Math.Abs(load.UnfactoredMagnitude) * member.Beam.Extent_V * layer.ZoomFactor / member.Beam.MaxMagnitude);
            float L = endPt.X - location.X;
            int n = 0;
            PointF p1 = new PointF();
            PointF p2 = new PointF();

            if (L % (h / 2.0f) == 0)
                n = (int)(L / (h / 2.0f));
            else
                n = (int)(L / (h / 2.0f)) + 1;

            n += n == 1 ? 1 : 0;

            n = n > 30 ? 30 : n;

            float s = L / (float)n;

            p1.X = location.X + L;
            p1.Y = location.Y;

            bool upward = load.UnfactoredMagnitude >= 0;
            int factr = 1;
            float angle;

            if (!upward)
            {
                factr = 1;
                angle = 90;
            }
            else
            {
                factr = -1;
                angle = 270;
            }


            if (load.Orientation == eTriangularLoadOrientation.LeftToRight)
            {
                p2.X = p1.X;
                p2.Y = p1.Y + factr * h;

                topLine = new eLine(location, p2, layer);

                for (int i = 1; i <= n; i++)
                {
                    float hi = h * i * s / L;
                    PointF pi1 = new PointF(location.X + i * s, location.Y);
                    PointF pi2 = new PointF(pi1.X, pi1.Y + factr * hi);

                    dwgObjects.Add(new eLine(pi1, pi2, layer));

                    float arr_h = 0.3f * h > hi ? hi : 0.3f * h;
                    float arr_w = (1.0f / 3.0f) * arr_h;
                    dwgObjects.Add(new eArrowHead(pi1, angle, arr_w, arr_h, layer));
                }

                if (n == 0)
                {
                    dwgObjects.Add(new eArrowHead(location, angle, size * 5 * zoomFactor, layer));
                }
            }
            else
            {
                p2.X = location.X;
                p2.Y = location.Y + factr * h;

                topLine = new eLine(p1, p2, layer);

                for (int i = 1; i <= n; i++)
                {
                    float hi = h * i * s / L;
                    PointF pi1 = new PointF(location.X + (n - i) * s, location.Y);
                    PointF pi2 = new PointF(pi1.X, pi1.Y + factr * hi);

                    dwgObjects.Add(new eLine(pi1, pi2, layer));

                    float arr_h = 0.3f * h > hi ? hi : 0.3f * h;
                    float arr_w = (1.0f / 3.0f) * arr_h;
                    dwgObjects.Add(new eArrowHead(pi1, angle, arr_w, arr_h, layer));
                }

                if (n == 0)
                {
                    dwgObjects.Add(new eArrowHead(location, angle, size * 5 * zoomFactor, layer));
                }
            }

            text = new eText(Math.Round(Math.Abs(eUtility.Convert(load.UnfactoredMagnitude, eUtility.SFU,
                this.forceUnit) / eUtility.Convert(1, eUtility.SLU, this.lengthUnit)), 3).ToString(), new PointF(), layer);
            text.Location = new PointF(p2.X, p2.Y + factr * text.Height / 2.0f);

            if (h > 0.0f && !float.IsInfinity(h) && !float.IsNaN(h))
                text.Height = (float)(0.6 * member.Beam.Extent_V * layer.ZoomFactor);

            CreateRegion();
        }

        protected override void CreateRegion()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLines(new PointF[] { location, endPt, topLine.End });

            this.region = new Region(path);
            region.Union(new RectangleF(text.Location.X - text.Width / 2.0f, text.Location.Y - text.Height / 2.0f, text.Width, text.Height));

            this.region.Exclude(new RectangleF(location.X - 5, location.Y - 5, 10, 10));
            this.region.Exclude(new RectangleF(endPt.X - 5, endPt.Y - 5, 10, 10));
            this.region.Exclude(new RectangleF((location.X + endPt.X) / 2.0f - 5, location.Y - 5, 10, 10));
        }

        private void InitializeComponents(eGMember membDWG, ESADS.GUI.eModelForm dwgForm)
        {
            this.visible = true;
            base.InitializeComponents(membDWG, dwgForm);

            grip_Left = new eGripBox(location, layer, membDWG.Start.X, endPt.X, dwgForm);
            grip_Right = new eGripBox(new PointF(topLine.End.X, location.Y), layer, location.X, membDWG.End.X, dwgForm);
            float x = (location.X + endPt.X) / 2.0f;
            grip_middle = new eGripBox(new PointF(x, grip_Right.Location.Y), layer, x - (grip_Left.Location.X - membDWG.Start.X), x + (membDWG.End.X - endPt.X), dwgForm);

            grip_Left.TurnedOn += new eGripBoxEventHandler(grip_Left_TurnedOn);
            grip_Left.TurnedOff += new eGripBoxEventHandler(grip_Left_TurnedOff);
            grip_Left.Move += new eGripBoxEventHandler(grip_Left_Move);

            grip_Right.TurnedOn += new eGripBoxEventHandler(grip_Right_TurnedOn);
            grip_Right.TurnedOff += new eGripBoxEventHandler(grip_Right_TurnedOff);
            grip_Right.Move += new eGripBoxEventHandler(grip_Right_Move);

            grip_middle.TurnedOn += new eGripBoxEventHandler(grip_Middle_TurnedOn);
            grip_middle.TurnedOff += new eGripBoxEventHandler(grip_Middle_TurnedOff);
            grip_middle.Move += new eGripBoxEventHandler(grip_Middle_Move);

            eLayer lay = new eLayer("", System.Drawing.Color.FromArgb(100, 250, 250, 250));
            Font f = new Font("Arial", 7);
            lay.TextStyle = new eTextStyle(f, eChangeBy.ByObject);

            //this.dim_middle = new eDimension(location, endPt, "", eDimensionType.LinearHorizontal, lay, eDimensionLinePosition.RightOrBottom, size * 15.0f);
            //this.dim_middle.Visible = false;

            if (load.UnfactoredMagnitude < 0)
            {
                //dim_Left.DimensionLinePosition = eDimensionLinePosition.LeftOrAbove;
                //dim_middle.DimensionLinePosition = eDimensionLinePosition.LeftOrAbove;
                //dim_Right.DimensionLinePosition = eDimensionLinePosition.LeftOrAbove;
            }
        }

        protected override void memberDWG_LocationChanged(object sender, eMemberGraphicsEventArgs e)
        {
            location = new PointF(e.Location.X + location.X - grip_Left.Min_X, e.Location.Y);
            endPt = new PointF(e.End.X - grip_Right.Max_X + endPt.X, e.End.Y);

            grip_Left.Location = location;
            grip_Right.Location = endPt;
            grip_middle.Location = new PointF((location.X + endPt.X) / 2.0f, location.Y);

            grip_Left.Min_X = e.Location.X;
            grip_Left.Max_X = endPt.X;

            grip_Right.Min_X = location.X;
            grip_Right.Max_X = e.End.X;

            grip_middle.Min_X = grip_middle.Location.X - (location.X - e.Location.X);
            grip_middle.Max_X = grip_middle.Location.X + (e.End.X - endPt.X);

            GenerateDwgObjects();
        }

        private void grip_Middle_Move(object Sender, eGripBoxEventArgs e)
        {
            PointF loc = location;
            location = new PointF(e.Location.X - (endPt.X - loc.X) / 2.0f, loc.Y);
            endPt = new PointF(e.Location.X + (endPt.X - loc.X) / 2.0f, loc.Y);

            grip_Left.Location = location;
            grip_Right.Location = endPt;
            grip_Left.Max_X = grip_Right.Location.X;
            grip_Right.Min_X = grip_Left.Location.X;
            GenerateDwgObjects();

            //dim_Left.Text = Math.Round((location.X - grip_Left.Min_X) * eUtility.Convert(load.Member.Length, eUtility.SLU, this.lengthUnit) / (grip_Right.Max_X - grip_Left.Min_X), 3).ToString();
            //dim_Right.Text = Math.Round((grip_Right.Max_X - endPt.X) * eUtility.Convert(load.Member.Length, eUtility.SLU, this.lengthUnit) / (grip_Right.Max_X - grip_Left.Min_X), 3).ToString();

            //dim_Left.End = dim_middle.Start = location;
            //dim_middle.End = dim_Right.Start = endPt;
        }

        private void grip_Middle_TurnedOff(object Sender, eGripBoxEventArgs e)
        {
            if (e.SuppressChanges)
            {
                //dim_Left.Visible = false;
                //dim_middle.Visible = false;
                //dim_Right.Visible = false;
            }
            else
            {
                //dim_Left.Visible = false;
                //dim_middle.Visible = false;
                //dim_Right.Visible = false;

                load.Start = (location.X - grip_Left.Min_X) * load.Member.Length / (grip_Right.Max_X - grip_Left.Min_X);
                load.End = (grip_Right.Max_X - endPt.X) * load.Member.Length / (grip_Right.Max_X - grip_Left.Min_X);
            }

            OnMoveEnd();
        }

        private void grip_Middle_TurnedOn(object Sender, eGripBoxEventArgs e)
        {
            //dim_Left.Visible = true;
            //dim_Left.Text = Math.Round(eUtility.Convert(load.Start, eUtility.SLU, this.lengthUnit), 3).ToString();
            //dim_Left.Start = new PointF(grip_Left.Min_X, location.Y);
            //dim_Left.End = location;

            //dim_middle.Visible = true;
            //dim_middle.Text = Math.Round(eUtility.Convert((load.Member.Length - load.Start - load.End),eUtility.SLU, this.lengthUnit), 3).ToString();
            //dim_middle.Start = location;
            //dim_middle.End = endPt;

            //dim_Right.Visible = true;
            //dim_Right.Text = Math.Round(eUtility.Convert(load.End, eUtility.SLU, this.lengthUnit), 3).ToString();
            //dim_Right.Start = endPt;
            //dim_Right.End = new PointF(grip_Right.Max_X, location.Y);

            OnMoveStart();
        }

        private void grip_Right_Move(object Sender, eGripBoxEventArgs e)
        {
            endPt = e.Location;
            GenerateDwgObjects();
            grip_Left.Max_X = e.Location.X;
            grip_middle.Location = new PointF((location.X + endPt.X) / 2.0f, location.Y);
            grip_middle.Max_X = grip_middle.Location.X + (grip_Right.Max_X - endPt.X);
            grip_middle.Min_X = grip_middle.Location.X - (location.X - grip_Left.Min_X);

            //dim_middle.Text = Math.Round((endPt.X - location.X) * eUtility.Convert(load.Member.Length, eUtility.SLU, this.lengthUnit) / (grip_Right.Max_X - grip_Left.Min_X), 3).ToString();
            //dim_Right.Text = Math.Round((grip_Right.Max_X - endPt.X) * eUtility.Convert(load.Member.Length, eUtility.SLU, this.lengthUnit) / (grip_Right.Max_X - grip_Left.Min_X), 3).ToString();
            //dim_middle.End = dim_Right.Start = endPt;

        }

        private void grip_Right_TurnedOff(object Sender, eGripBoxEventArgs e)
        {
            if (e.SuppressChanges)
            {
                //dim_Left.Visible = false;
                //dim_middle.Visible = false;
                //dim_Right.Visible = false;
            }
            else
            {
                if (textBox.Visible)
                {
                    textBox.Visible = false;
                    textBox.Parent.Focus();

                    double l =  eUtility.Convert(textBox.DoubleValue, this.lengthUnit, eUtility.SLU);

                    if (l < 0)
                        load.End = 0;
                    else if (l > (load.Member.Length - load.Start))
                        load.End = load.Member.Length - load.Start;
                    else
                        load.End = l;

                    endPt.X = grip_Right.Max_X - (float)(load.End * (grip_Right.Max_X - grip_Left.Min_X) / load.Member.Length);

                    grip_Left.Location = location;
                    grip_Right.Location = endPt;
                    grip_middle.Location = new PointF((location.X + endPt.X) / 2.0f, location.Y);

                    grip_Left.Max_X = endPt.X;
                    grip_Right.Min_X = location.X;
                    grip_middle.Min_X = grip_middle.Location.X - location.X + grip_Left.Min_X;
                    grip_middle.Max_X = grip_middle.Location.X + grip_Right.Max_X - endPt.X;

                    GenerateDwgObjects();

                    //dim_Left.Visible = false;
                    //dim_middle.Visible = false;
                    //dim_Right.Visible = false;
                }
                else
                {
                    //dim_Left.Visible = false;
                    //dim_middle.Visible = false;
                    //dim_Right.Visible = false;

                    load.End = (grip_Right.Max_X - endPt.X) * load.Member.Length / (grip_Right.Max_X - grip_Left.Min_X);
                }
            }

            OnMoveEnd();
        }

        private void grip_Right_TurnedOn(object Sender, eGripBoxEventArgs e)
        {
            //dim_Left.Visible = true;
            //dim_Left.Text = Math.Round(eUtility.Convert(load.Start, eUtility.SLU, this.lengthUnit), 3).ToString();
            //dim_Left.Start = new PointF(grip_Left.Min_X, location.Y);
            //dim_Left.End = location;

            //dim_middle.Visible = true;
            //dim_middle.Text = Math.Round(eUtility.Convert((load.Member.Length - load.Start - load.End), eUtility.SLU, this.lengthUnit), 3).ToString();
            //dim_middle.Start = location;
            //dim_middle.End = endPt;

            //dim_Right.Visible = true;
            //dim_Right.Text = Math.Round(eUtility.Convert(load.End, eUtility.SLU, this.lengthUnit), 3).ToString();
            //dim_Right.Start = endPt;
            //dim_Right.End = new PointF(grip_Right.Max_X, location.Y);

            OnMoveStart();
        }

        private void grip_Left_Move(object Sender, eGripBoxEventArgs e)
        {
            location = e.Location;
            GenerateDwgObjects();
            grip_Right.Min_X = e.Location.X;
            grip_middle.Location = new PointF((location.X + endPt.X) / 2.0f, location.Y);
            grip_middle.Max_X = grip_middle.Location.X + (grip_Right.Max_X - endPt.X);
            grip_middle.Min_X = grip_middle.Location.X - (location.X - grip_Left.Min_X);

            //dim_middle.Text = Math.Round((endPt.X - location.X) * eUtility.Convert(load.Member.Length, eUtility.SLU, this.lengthUnit) / (grip_Right.Max_X - grip_Left.Min_X), 3).ToString();
            //dim_Left.Text = Math.Round((location.X - grip_Left.Min_X) * eUtility.Convert(load.Member.Length, eUtility.SLU, this.lengthUnit) / (grip_Right.Max_X - grip_Left.Min_X), 3).ToString();
            //dim_middle.Start = dim_Left.End = location;

        }

        private void grip_Left_TurnedOff(object Sender, eGripBoxEventArgs e)
        {
            if (e.SuppressChanges)
            {
                //dim_Left.Visible = false;
                //dim_middle.Visible = false;
                //dim_Right.Visible = false;
            }
            else
            {
                if (textBox.Visible)
                {
                    textBox.Visible = false;
                    textBox.Parent.Focus();

                    double l = eUtility.Convert(textBox.DoubleValue, this.lengthUnit, eUtility.SLU);

                    if (l < 0)
                        load.Start = 0;
                    else if (l > (load.Member.Length - load.End))
                        load.Start = load.Member.Length - load.End;
                    else
                        load.Start = l;

                    location.X = grip_Left.Min_X + (float)(load.Start * (grip_Right.Max_X - grip_Left.Min_X) / load.Member.Length);

                    grip_Left.Location = location;
                    grip_Right.Location = endPt;
                    grip_middle.Location = new PointF((location.X + endPt.X) / 2.0f, location.Y);

                    grip_Left.Max_X = endPt.X;
                    grip_Right.Min_X = location.X;
                    grip_middle.Min_X = grip_middle.Location.X - location.X + grip_Left.Min_X;
                    grip_middle.Max_X = grip_middle.Location.X + grip_Right.Max_X - endPt.X;

                    GenerateDwgObjects();

                    //dim_Left.Visible = false;
                    //dim_middle.Visible = false;
                    //dim_Right.Visible = false;
                }
                else
                {
                    //dim_Left.Visible = false;
                    //dim_middle.Visible = false;
                    //dim_Right.Visible = false;

                    load.Start = (location.X - grip_Left.Min_X) * load.Member.Length / (grip_Right.Max_X - grip_Left.Min_X);
                }
            }

            OnMoveEnd();
        }

        private void grip_Left_TurnedOn(object Sender, eGripBoxEventArgs e)
        {
            //dim_Left.Visible = true;
            //dim_Left.Text = Math.Round(eUtility.Convert(load.Start, eUtility.SLU, this.lengthUnit), 3).ToString();
            //dim_Left.Start = new PointF(grip_Left.Min_X, location.Y);
            //dim_Left.End = location;

            //dim_middle.Visible = true;
            //dim_middle.Text = Math.Round(eUtility.Convert((load.Member.Length - load.Start - load.End), eUtility.SLU, this.lengthUnit), 3).ToString();
            //dim_middle.Start = location;
            //dim_middle.End = endPt;

            //dim_Right.Visible = true;
            //dim_Right.Text = Math.Round(eUtility.Convert(load.End, eUtility.SLU, this.lengthUnit), 3).ToString();
            //dim_Right.Start = endPt;
            //dim_Right.End = new PointF(grip_Right.Max_X, location.Y);

            OnMoveStart();
        }

        protected override void dwgForm_KeyDown(object sender, KeyEventArgs e)
        {
            base.dwgForm_KeyDown(sender, e);

            if (e.KeyCode == Keys.Enter)
            {
                if (textBox.Visible && !grip_Left.On && !grip_middle.On && !grip_Right.On)
                {
                    textBox.Visible = false;
                    textBox.Parent.Focus();
                    double mag = eUtility.Convert(textBox.DoubleValue, this.forceUnit, eUtility.SFU) / eUtility.Convert(1, this.lengthUnit, eUtility.SLU);
                    if (mag != load.UnfactoredMagnitude)
                    {
                        load.UnfactoredMagnitude = mag;
                        GenerateDwgObjects();
                        if (load.UnfactoredMagnitude >= 0)
                        {
                            //dim_Left.DimensionLinePosition = eDimensionLinePosition.RightOrBottom;
                            //dim_middle.DimensionLinePosition = eDimensionLinePosition.RightOrBottom;
                            //dim_Right.DimensionLinePosition = eDimensionLinePosition.RightOrBottom;
                        }
                        else
                        {
                            //dim_Left.DimensionLinePosition = eDimensionLinePosition.LeftOrAbove;
                            //dim_middle.DimensionLinePosition = eDimensionLinePosition.LeftOrAbove;
                            //dim_Right.DimensionLinePosition = eDimensionLinePosition.LeftOrAbove;
                        }
                        OnMagnitudeChanged();
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                location.X = (float)(grip_Left.Min_X + (load.Start * (grip_Right.Max_X - grip_Left.Min_X) / load.Member.Length));
                endPt.X = (float)(grip_Right.Max_X - (load.End * (grip_Right.Max_X - grip_Left.Min_X) / load.Member.Length));

                grip_Left.Location = location;
                grip_Right.Location = endPt;
                grip_middle.Location = new PointF((endPt.X + location.X) / 2.0f, location.Y);

                GenerateDwgObjects();

                textBox.Visible = false;
                textBox.Parent.Focus();
                textBox.Text = "";

                if (isSelected)
                {
                    isSelected = false;
                    ChangeSelect();
                }
                (sender as Form).Invalidate();
            }
            else if (NumericKeyPressed(e) && !textBox.Visible)
            {
                (sender as eModelForm).EditingText = true;

                if (grip_Left.On)
                {
                    textBox.Visible = true;
                    textBox.Text = "";
                    textBox.Focus();
                    //textBox.Location = new Point((int)(dim_Left.TextObjet.Location.X - dim_Left.TextObjet.Width / 2.0f), (int)(dim_Left.TextObjet.Location.Y - dim_Left.TextObjet.Height / 2.0f));
                    //textBox.Font = dim_Left.TextObjet.TextStyle;
                    textBox.Location = new Point((int)((this.grip_Left.Min_X + grip_Left.Location.X) / 2.0), (int)(grip_Left.Location.Y));
                }
                else if (grip_Right.On)
                {
                    textBox.Visible = true;
                    textBox.Text = "";
                    textBox.Focus();
                    //textBox.Location = new Point((int)(dim_Right.TextObjet.Location.X - dim_Right.TextObjet.Width / 2.0f), (int)(dim_Right.TextObjet.Location.Y - dim_Right.TextObjet.Height / 2.0f));
                    //textBox.Font = dim_Right.TextObjet.TextStyle;
                    textBox.Location = new Point((int)((this.grip_Right.Max_X + grip_Right.Location.X) / 2.0), (int)(grip_Right.Location.Y));
                }
            }
        }

        protected override void dwgForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!grip_Left.On && !grip_Right.On &&
                (new Region(new RectangleF(text.Location.X - text.Width / 2.0f, text.Location.Y - text.Height / 2.0f, text.Width, text.Height)).IsVisible(e.Location)))
            {
                if ((sender as eModelForm).Locked)
                {
                    MessageBox.Show("The model cannot be edited while it is locked. Unlock it to continue.", "Model locked", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if ((sender as eModelForm).EditingText)
                    return;
                else
                    (sender as eModelForm).EditingText = true;
                (sender as eModelForm).ObjFoundBelowClickPt = true;

                textBox.Visible = true;
                textBox.Font = text.TextStyle;
                textBox.DoubleValue = eUtility.Convert(load.UnfactoredMagnitude, eUtility.SFU, this.forceUnit) / eUtility.Convert(1, eUtility.SLU, this.lengthUnit);
                textBox.Location = new Point((int)(text.Location.X - text.Width / 2.0f), (int)(text.Location.Y - text.Height / 2.0f));
            }
        }

        protected override void ChangeHighLight()
        {
            if (isHighlighted)
            {
                topLine.LineWeight = new eLineWeight(3.0f);
                foreach (eIDrawing dwg in dwgObjects)
                {
                    if (dwg.GetType() == typeof(eLine))
                        (dwg as eLine).LineWeight = new eLineWeight(3.0f);
                }

                if (isSelected)
                {
                    topLine.LineType = new eLineType(eLineTypes.Dashed, 1.0f / 6.0f);
                    foreach (eIDrawing dwg in dwgObjects)
                    {
                        if (dwg.GetType() == typeof(eLine))
                            (dwg as eLine).LineType = new eLineType(eLineTypes.Dashed, 1.0f / 6.0f);
                    }
                }
            }
            else
            {
                topLine.LineWeight = new eLineWeight(1.0f);
                foreach (eIDrawing dwg in dwgObjects)
                {
                    if (dwg.GetType() == typeof(eLine))
                        (dwg as eLine).LineWeight = new eLineWeight(1.0f);
                }

                if (isSelected)
                {
                    topLine.LineType = new eLineType(eLineTypes.Dashed, 0.5f);
                    foreach (eIDrawing dwg in dwgObjects)
                        if (dwg.GetType() == typeof(eLine))
                            (dwg as eLine).LineType = new eLineType(eLineTypes.Dashed, 0.5f);
                }
            }

            
        }

        protected override void ChangeSelect()
        {
            if (isSelected)
            {
                grip_Left.Visible = true;
                grip_Right.Visible = true;
                grip_middle.Visible = true;

                if (isHighlighted)
                {
                    topLine.LineType = new eLineType(eLineTypes.Dashed, 1 / 6.0f);
                    foreach (eIDrawing dwg in dwgObjects)
                    {
                        if (dwg.GetType() == typeof(eLine))
                        {
                            (dwg as eLine).LineType = new eLineType(eLineTypes.Dashed, 1 / 6.0f);
                        }
                    }
                }
                else
                {
                    topLine.LineType = new eLineType(eLineTypes.Dashed, 0.5f);
                    foreach (eIDrawing dwg in dwgObjects)
                    {
                        if (dwg.GetType() == typeof(eLine))
                        {
                            (dwg as eLine).LineType = new eLineType(eLineTypes.Dashed,0.5f);
                        }
                    }
                }
            }
            else
            {
                grip_Left.Visible = false;
                grip_Right.Visible = false;
                grip_middle.Visible = false;

                topLine.LineType = new eLineType(eLineTypes.Continuous);
                foreach (eIDrawing dwg in dwgObjects)
                {
                    if (dwg.GetType() == typeof(eLine))
                    {
                        (dwg as eLine).LineType = new eLineType(eLineTypes.Continuous);
                    }
                }
            }
        }

        protected override void member_Resize(object sender, eMemberGraphicsEventArgs e)
        {
            if (load.Member.Length == e.Length)
                return;
            load.Start = e.Length * load.Start / load.Member.Length;
            load.End = e.Length * load.End / load.Member.Length;

            location.X = e.Location.X + (float)(load.Start * (e.End.X - e.Location.X) / e.Length);
            endPt.X = e.End.X - (float)(load.End * (e.End.X - e.Location.X) / e.Length);

            location.X = e.Location.X + (float)(load.Start * (e.End.X - e.Location.X) / load.Member.Length);
            endPt.X = e.End.X - (float)(load.End * (e.End.X - e.Location.X) / load.Member.Length);

            grip_Left.Location = location;
            grip_Right.Location = endPt;
            grip_middle.Location = new PointF((location.X + endPt.X) / 2.0f, location.Y);

            grip_Left.Min_X = e.Location.X;
            grip_Left.Max_X = endPt.X;
            grip_middle.Min_X = grip_middle.Location.X - (location.X - e.Location.X);
            grip_middle.Max_X = grip_middle.Location.X + (location.X - e.Location.X);
            grip_Right.Min_X = location.X;
            grip_Right.Max_X = e.End.X;

            GenerateDwgObjects();
        }

        internal override void ReleaseHandlers(eModelForm dwgForm)
        {
            base.ReleaseHandlers(dwgForm);

            grip_Left.ReleaseHandlers(dwgForm);
            grip_middle.ReleaseHandlers(dwgForm);
            grip_Right.ReleaseHandlers(dwgForm);

            this.member.Beam.Changed -= new EventHandler(Beam_Changed);
        }

        public override float MaxNegOffset
        {
            get 
            {
                if (load.UnfactoredMagnitude >= 0)
                    return 0.0f;
                else
                {
                    return Math.Abs(topLine.Location.Y - topLine.End.Y) + text.Height;
                }
            }
        }

        public override float MaxPosOffset
        {
            get
            {
                if (load.UnfactoredMagnitude < 0)
                    return 0.0f;
                else
                {
                    return Math.Abs(topLine.Location.Y - topLine.End.Y) + text.Height;
                }
            }
        }
    }
}
