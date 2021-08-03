using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ESADS.Mechanics.Analysis;
using ESADS.Mechanics.Analysis.Beam;
using ESADS.GUI.Controls;
using ESADS.GUI;

namespace ESADS.EGraphics.Beam
{
    /// <summary>
    /// Represents the graphic elements of a uniformly distributed force load on a member.
    /// </summary>
    public class eGRectangularLoad : eGLoad
    {
        #region Fields

        /// <summary>
        /// List of all drawing components needed to draw the member.
        /// </summary>
        private List<eIDrawing> dwgObjects;
        /// <summary>
        /// holds the value of the the 'Load' property.
        /// </summary>
        private eRectangularLoad load;
        /// <summary>
        /// The text_left that represents the magnitude of the load_Tri.
        /// </summary>
        private eText text;
        /// <summary>
        /// The grip box on the left side end of the load_Tri.
        /// </summary>
        private eGripBox grip_Left;
        /// <summary>
        /// The grip box on the right side end of the load_Tri.
        /// </summary>
        private eGripBox grip_Right;
        /// <summary>
        /// The line that is drawn at the top of the vertical lines.
        /// </summary>
        private eLine topLine;
        /// <summary>
        /// The end point of the load_Tri on the member.
        /// </summary>
        private PointF endPt;
        /// <summary>
        /// The grip box used to move the whole load_Tri.
        /// </summary>
        private eGripBox grip_middle;
        /// <summary>
        /// Used to show the length of the load_Tri distribution length.
        /// </summary>
        //private eDimension dim_middle;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a drawing object that represents a rectangular distributed member.
        /// </summary>
        /// <param name="member">The mechanics object representing the member.</param>
        /// <param name="location">The location of the member. It is the far left point of the member and member intersection.</param>
        /// <param name="load">The uniformly distributed laod represented by the drawing.</param>
        /// <param name="layer">The layer on which the member is to be drawn.</param>
        /// <param name="membDWG">The member drawing on which the load is place.</param>
        /// <param name="dwgForm">The form on which the member is going to be shown.</param>
        public eGRectangularLoad(eRectangularLoad load, eLayer layer, eGMember membDWG, ESADS.GUI.eModelForm dwgForm)
            : base(membDWG, layer, dwgForm)
        {
            this.member.Beam.Changed += new EventHandler(Beam_Changed);
            this.load = load;
            this.location = new PointF((float)(membDWG.Start.X + (load.Start * (membDWG.End.X - membDWG.Start.X) / load.Member.Length)), membDWG.Start.Y);
            this.endPt = new PointF((float)(membDWG.Start.X + ((load.Member.Length - load.End) * (membDWG.End.X - membDWG.Start.X) / (load.Member.Length))), location.Y);

            GenerateDwgObjects();

            InitializeComponents(membDWG, dwgForm);

            member.Beam.OnChanged();
        }

        private void Beam_Changed(object sender, EventArgs e)
        {
            GenerateDwgObjects();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the color of the load dwg.
        /// </summary>
        public override eColor Color
        {
            get { return text.Color; }
            set
            {
                text.Color = value;
                topLine.Color = value;

                foreach (eIDrawing dwg in dwgObjects)
                    dwg.Color = value;
            }
        }
        /// <summary>
        /// Gets or sets the left most point of intersection of the load with the member.
        /// </summary>
        public override PointF Location
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
        /// Gets the mechanics object connected to this member drawing.
        /// </summary>
        public eRectangularLoad Load
        {
            get
            {
                return load;
            }
        }

        public override float MaxNegOffset
        {
            get
            {
                if (load.UnfactoredMagnitude >= 0)
                    return 0.0f;
                else
                    return topLine.Location.Y - location.Y + text.Height;
            }
        }

        public override float MaxPosOffset
        {
            get
            {
                if (load.UnfactoredMagnitude < 0)
                    return 0.0f;
                else
                    return topLine.Location.Y - location.Y + text.Height;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Zooms the rectangular load dwg.
        /// </summary>
        /// <param name="ZoomCenter">Scaling center.</param>
        /// <param name="ZoomFactor">Scale factor.</param>
        public override void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            base.Zoom(ZoomCenter, ZoomFactor);

            foreach (eIDrawing dwg in dwgObjects)
            {
                dwg.Zoom(ZoomCenter, ZoomFactor);
            }
            topLine.Zoom(ZoomCenter, ZoomFactor);
            text.Zoom(ZoomCenter, ZoomFactor);

            endPt.X = ZoomFactor * (endPt.X - ZoomCenter.X) + ZoomCenter.X;
            endPt.Y = ZoomFactor * (endPt.Y - ZoomCenter.Y) + ZoomCenter.Y;

            grip_Left.Zoom(ZoomCenter, ZoomFactor);
            grip_Right.Zoom(ZoomCenter, ZoomFactor);
            grip_middle.Zoom(ZoomCenter, ZoomFactor);

            grip_Left.Location = location;
            grip_Right.Location = endPt;
            grip_middle.Location = new PointF((location.X + endPt.X) / 2.0f, location.Y);

            CreateRegion();
        }

        /// <summary>
        /// Pans the rectangular load dwg.
        /// </summary>
        /// <param name="Xoffset">Amount of offset in X.</param>
        /// <param name="Yoffset">Amount of offset in Y.</param>
        public override void Pan(float Xoffset, float Yoffset)
        {
            base.Pan(Xoffset, Yoffset);

            foreach (eIDrawing dwg in dwgObjects)
            {
                dwg.Pan(Xoffset, Yoffset);
            }
            topLine.Pan(Xoffset, Yoffset);
            text.Pan(Xoffset, Yoffset);

            endPt.X += Xoffset;
            endPt.Y += Yoffset;

            grip_Left.Pan(Xoffset, Yoffset);
            grip_Right.Pan(Xoffset, Yoffset);
            grip_middle.Pan(Xoffset, Yoffset);

            grip_Left.Location = location;
            grip_Right.Location = endPt;
            grip_middle.Location = new PointF((location.X + endPt.X) / 2.0f, location.Y);

            CreateRegion();
        }

        /// <summary>
        /// Draws the uniformly distributed load using a graphics object provided.
        /// </summary>
        /// <param name="g">Graphics object used to draw the load.</param>
        public override void Draw(System.Drawing.Graphics g)
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
            grip_Right.Draw(g);
            grip_middle.Draw(g);
        }

        protected override void GenerateDwgObjects()
        {
            dwgObjects = new List<eIDrawing>();

            float h = (float)((member.Beam.Extent_V * layer.ZoomFactor * 0.1) +
                Math.Abs(load.UnfactoredMagnitude) * member.Beam.Extent_V * layer.ZoomFactor / member.Beam.MaxMagnitude);
            float L = endPt.X - location.X;
            float angle;
            int n = 0, sign;
            float s = h / 2.0f;
            float L_i;

            if (L % s == 0)
                n = (int)(L / s);
            else
                n = (int)(L / s) + 1;

            n = n <= 1 ? 2 : n; //minimum two spacings

            n = n > 30 ? 30 : n;

            s = L / (float)n;

            n++;

            if (load.UnfactoredMagnitude >= 0)
            {
                sign = -1;
                angle = 270;
            }
            else
            {
                sign = 1;
                angle = 90;
            }

            topLine = new eLine(new PointF(location.X, location.Y + sign * h), new PointF(endPt.X, endPt.Y + sign * h), layer);

            PointF p1 = location;
            PointF p2 = new PointF(location.X, location.Y + sign * h);

            float arr_h = 0.3f * h;
            float arr_w = (1.0f / 3.0f) * arr_h;

            for (int i = 0; i < n; i++)
            {
                dwgObjects.Add(new eLine(p1, p2, layer));
                dwgObjects.Add(new eArrowHead(p1, angle, arr_w, arr_h, layer));

                p1.X += s;
                p2.X += s;
            }

            double mag = eUtility.Convert(load.UnfactoredMagnitude, eUtility.SFU, forceUnit) / eUtility.Convert(1, eUtility.SLU, this.lengthUnit);
            text = new eText(Math.Round(Math.Abs(mag), 3).ToString(), new PointF(), layer);
            if (h > 0.0f && !float.IsInfinity(h) && !float.IsNaN(h))
                text.Height = (float)(0.6 * member.Beam.Extent_V * layer.ZoomFactor);

            text.Location = new PointF((location.X + endPt.X) / 2.0f, location.Y + sign * (h + text.Height / 2.0f));

            CreateRegion();
        }

        protected override void CreateRegion()
        {
            this.region = new Region(new RectangleF(location.X, Math.Min(location.Y, topLine.Location.Y), endPt.X - location.X, Math.Abs(topLine.Location.Y - location.Y)));
            this.region.Union(new RectangleF(text.Location.X - text.Width / 2.0f, text.Location.Y - text.Height / 2.0f, text.Width, text.Height));

            this.region.Exclude(new RectangleF(location.X - 5, location.Y - 5, 10, 10));
            this.region.Exclude(new RectangleF(endPt.X - 5, endPt.Y - 5, 10, 10));
            this.region.Exclude(new RectangleF((location.X + endPt.X) / 2.0f - 5, location.Y - 5, 10, 10));
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
                            (dwg as eLine).LineType = new eLineType(eLineTypes.Dashed, 0.5f);
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

            grip_middle.TurnedOn += new eGripBoxEventHandler(grip_middle_TurnedOn);
            grip_middle.TurnedOff += new eGripBoxEventHandler(grip_middle_TurnedOff);
            grip_middle.Move += new eGripBoxEventHandler(grip_middle_Move);

            eLayer lay = new eLayer("", System.Drawing.Color.FromArgb(100, 250, 250, 250));
            Font f = new Font("Arial", 7);
            lay.TextStyle = new eTextStyle(f, eChangeBy.ByObject);
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

        private void grip_middle_TurnedOn(object Sender, eGripBoxEventArgs e)
        {
            OnMoveStart();
        }

        private void grip_middle_Move(object Sender, eGripBoxEventArgs e)
        {
            PointF loc = location;
            location = new PointF(e.Location.X - (endPt.X - loc.X) / 2.0f, loc.Y);
            endPt = new PointF(e.Location.X + (endPt.X - loc.X) / 2.0f, loc.Y);

            grip_Left.Location = location;
            grip_Right.Location = endPt;
            GenerateDwgObjects();
        }

        private void grip_middle_TurnedOff(object Sender, eGripBoxEventArgs e)
        {
            if (!e.SuppressChanges)
            {
                grip_Left.Max_X = grip_Right.Location.X;
                grip_Right.Min_X = grip_Left.Location.X;

                load.Start = (location.X - grip_Left.Min_X) * load.Member.Length / (grip_Right.Max_X - grip_Left.Min_X);
                load.End = (grip_Right.Max_X - endPt.X) * load.Member.Length / (grip_Right.Max_X - grip_Left.Min_X);
            }

            OnMoveEnd();
        }

        private void grip_Right_TurnedOn(object Sender, eGripBoxEventArgs e)
        {
            OnMoveStart();
        }

        private void grip_Right_Move(object Sender, eGripBoxEventArgs e)
        {
            endPt = e.Location;
            GenerateDwgObjects();

            grip_middle.Location = new PointF((location.X + endPt.X) / 2.0f, location.Y);
        }

        private void grip_Right_TurnedOff(object Sender, eGripBoxEventArgs e)
        {
            if (!e.SuppressChanges)
            {
                if (textBox.Visible)
                {
                    textBox.Visible = false;
                    textBox.Parent.Focus();

                    double l = eUtility.Convert(textBox.DoubleValue, this.lengthUnit, eUtility.SLU);

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
                }
                else
                {
                    grip_Left.Max_X = e.Location.X;
                    grip_middle.Max_X = grip_middle.Location.X + (grip_Right.Max_X - endPt.X);
                    grip_middle.Min_X = grip_middle.Location.X - (location.X - grip_Left.Min_X);

                    load.End = (grip_Right.Max_X - endPt.X) * load.Member.Length / (grip_Right.Max_X - grip_Left.Min_X);
                }
            }

            OnMoveEnd();
        }

        private void grip_Left_TurnedOn(object Sender, eGripBoxEventArgs e)
        {
            OnMoveStart();
        }

        private void grip_Left_Move(object Sender, eGripBoxEventArgs e)
        {
            location = e.Location;
            GenerateDwgObjects();
            grip_middle.Location = new PointF((location.X + endPt.X) / 2.0f, location.Y);
        }

        private void grip_Left_TurnedOff(object Sender, eGripBoxEventArgs e)
        {
            if (!e.SuppressChanges)
            {
                if (textBox.Visible)
                {
                    textBox.Visible = false;
                    textBox.Parent.Focus();

                    double l = eUtility.Convert(textBox.DoubleValue, this.lengthUnit, eUtility.SLU);

                    if (l < 0)
                        load.Start = 0;
                    else if (l> (load.Member.Length - load.End))
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
                }
                else
                {
                    grip_Right.Min_X = e.Location.X;
                    grip_middle.Max_X = grip_middle.Location.X + (grip_Right.Max_X - endPt.X);
                    grip_middle.Min_X = grip_middle.Location.X - (location.X - grip_Left.Min_X);

                    load.Start = (location.X - grip_Left.Min_X) * load.Member.Length / (grip_Right.Max_X - grip_Left.Min_X);
                }
            }

            OnMoveEnd();
        }

        protected override void dwgForm_KeyDown(object sender, KeyEventArgs e)
        {
            base.dwgForm_KeyDown(sender, e);
            
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox.Visible && !grip_Left.On && !grip_middle.On && !grip_Right.On)
                {
                    textBox.Visible = false;
                    (sender as Form).Focus();

                    double mag = eUtility.Convert(textBox.DoubleValue, this.forceUnit, eUtility.SFU) / eUtility.Convert(1, this.lengthUnit, eUtility.SLU);
                    if (mag != load.UnfactoredMagnitude)
                    {
                        load.UnfactoredMagnitude = mag;
                        GenerateDwgObjects();
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
                (sender as Form).Focus();
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
                    textBox.Location = new Point((int)((this.grip_Left.Min_X + grip_Left.Location.X) / 2.0), (int)(grip_Left.Location.Y));
                }
                else if (grip_Right.On)
                {
                    textBox.Visible = true;
                    textBox.Text = "";
                    textBox.Focus();
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

        protected override void member_Resize(object sender, eMemberGraphicsEventArgs e)
        {
            if (load.Member.Length == e.Length)
                return;

            load.Start = e.Length * load.Start / load.Member.Length;
            load.End = e.Length * load.End / load.Member.Length;

            location.X = e.Location.X + (float)(load.Start * (e.End.X - e.Location.X) / e.Length);
            endPt.X = e.End.X - (float)(load.End * (e.End.X - e.Location.X) / e.Length);

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
        #endregion
    }
}
