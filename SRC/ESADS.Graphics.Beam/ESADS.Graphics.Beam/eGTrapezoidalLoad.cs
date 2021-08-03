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
    /// Represents the graphic elements of a trapezoidally distributed force load on a member.
    /// </summary>
    public class eGTrapezoidalLoad : eGLoad 
    {
        #region Fields

        /// <summary>
        /// Holds the value of the 'Load' property.
        /// </summary>
        private eTriangularLoad load_Tri;
        /// <summary>
        /// List of all the necessary drawing objects for the member drawing.
        /// </summary>
        private List<eIDrawing> dwgObjects;
        /// <summary>
        /// The text_left that represents the magnitude of the load_Tri.
        /// </summary>
        private eText text_left;
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
        private eText text_right;
        /// <summary>
        /// The rectangular load part of the trapezoidal load.
        /// </summary>
        private eRectangularLoad load_Rect;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates new drawing object to represent a triangular member on a member.
        ///  </summary>
        /// <param name="member">The mechanics object which the drawing is going to represent.</param>
        /// <param name="location">The far left point of intersection of the member with the member on which it is loaded on.</param>
        /// <param name="load_Tri">The triangular load component of the load.</param>
        /// <param name="load_Rect">The rectangular load component of the load.</param>
        /// <param name="layer">The layer on which the member is drawn.</param>
        /// <param name="membDWG">The member drawing on which the load is placed.</param>
        /// <param name="dwgForm">The form on which the member is shown to the user.</param>
        public eGTrapezoidalLoad(eTriangularLoad load_Tri, eRectangularLoad load_Rect, eLayer layer, eGMember membDWG, ESADS.GUI.eModelForm dwgForm)
            : base(membDWG, layer, dwgForm)
        {
            member.Beam.Changed += new EventHandler(Beam_Changed);
            if (load_Rect.Member != load_Tri.Member)
                throw new Exception("The triangular and rectangular loads belong to different members.");

            if (load_Rect.Start != load_Tri.Start || load_Rect.End != load_Tri.End)
                throw new Exception("The triangular load and rectangular load have different start or end length. Therefore, they cannot superposed to give a trapezoidal load.");

            if (load_Rect.UnfactoredMagnitude * load_Tri.UnfactoredMagnitude < 0)
                throw new Exception("The triangular and rectangular loads need to be on the same direction to make a trapezoidal load.");

            this.load_Tri = load_Tri;
            this.load_Rect = load_Rect;
            this.location = new PointF((float)(membDWG.Start.X + (load_Tri.Start * (membDWG.End.X - membDWG.Start.X) / load_Tri.Member.Length)), membDWG.Start.Y);
            this.endPt = new PointF((float)(membDWG.Start.X + ((load_Tri.Member.Length - load_Tri.End) * (membDWG.End.X - membDWG.Start.X) / (load_Tri.Member.Length))), location.Y);

            GenerateDwgObjects();

            InitializeComponents(membDWG, dwgForm);
        }

        private void Beam_Changed(object sender, EventArgs e)
        {
            GenerateDwgObjects();
        }
        #endregion

        #region Properties
        public override PointF Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                GenerateDwgObjects();
            }
        }
        public override eColor Color
        {
            get
            {
                return text_left.Color;
            }
            set
            {
                text_left.Color = value;
                text_right.Color = value;
                topLine.Color = value;

                foreach (eIDrawing dwg in dwgObjects)
                    dwg.Color = value;
            }
        }
        /// <summary>
        /// Gets the mechanics object associated with this member drawing.
        /// </summary>
        public eTriangularLoad Load_Tri
        {
            get
            {
                return load_Tri;
            }
        }

        public eRectangularLoad Load_Rect
        {
            get
            {
                return this.load_Rect;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Scales the load drawing about a point with a certain scale factor.
        /// </summary>
        public override void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            base.Zoom(ZoomCenter, ZoomFactor);

            textBox.Visible = false;

            foreach (eIDrawing dwg in dwgObjects)
            {
                dwg.Zoom(ZoomCenter, ZoomFactor);
            }

            endPt.X = ZoomFactor * (endPt.X - ZoomCenter.X) + ZoomCenter.X;
            endPt.Y = ZoomFactor * (endPt.Y - ZoomCenter.Y) + ZoomCenter.Y;

            topLine.Zoom(ZoomCenter, ZoomFactor);
            text_left.Zoom(ZoomCenter, ZoomFactor);
            text_right.Zoom(ZoomCenter, ZoomFactor);

            grip_Left.Zoom(ZoomCenter, ZoomFactor);
            grip_middle.Zoom(ZoomCenter, ZoomFactor);
            grip_Right.Zoom(ZoomCenter, ZoomFactor);

            grip_Left.Location = location;
            grip_Right.Location = endPt;
            grip_middle.Location = new PointF((endPt.X + location.X) / 2.0f, location.Y);

            CreateRegion();
        }

        /// <summary>
        /// Moves the load drawing with a given x and y offsets.
        /// </summary>
        /// <param name="Xoffset">Amount of offset in X.</param>
        /// <param name="Yoffset">Amount of offset in Y.</param>
        public override void Pan(float Xoffset, float Yoffset)
        {
            base.Pan(Xoffset, Yoffset);

            textBox.Visible = false;

            foreach (eIDrawing dwg in dwgObjects)
            {
                dwg.Pan(Xoffset, Yoffset);
            }

            endPt.X += Xoffset;
            endPt.Y += Yoffset;

            topLine.Pan(Xoffset, Yoffset);
            text_left.Pan(Xoffset, Yoffset);
            text_right.Pan(Xoffset, Yoffset);

            grip_Left.Pan(Xoffset, Yoffset);
            grip_middle.Pan(Xoffset, Yoffset);
            grip_Right.Pan(Xoffset, Yoffset);

            grip_Left.Location = location;
            grip_Right.Location = endPt;
            grip_middle.Location = new PointF((endPt.X + location.X) / 2.0f, location.Y);

            CreateRegion();
        }

        /// <summary>
        /// Draws the trapezoidal load using a graphics object.
        /// </summary>
        /// <param name="g">The graphics object to draw the load dwg.</param>
        public override void Draw(Graphics g)
        {
            if (!visible)
                return;
            foreach (eIDrawing dwg in dwgObjects)
            {
                dwg.Draw(g);
            }

            topLine.Draw(g);
            text_left.Draw(g);
            text_right.Draw(g);

            grip_Left.Draw(g);
            grip_middle.Draw(g);
            grip_Right.Draw(g);

            //dim_Left.Draw(g);
            //dim_middle.Draw(g);
            //dim_Right.Draw(g);

            //g.FillRegion(new SolidBrush(System.Drawing.Color.FromArgb(50, 0, 0, 250)), region);
        }

        private void InitializeComponents(eGMember membDWG, ESADS.GUI.eModelForm dwgForm)
        {
            this.visible = true;
            base.InitializeComponents(membDWG, dwgForm);

            grip_Left = new eGripBox(location, layer, membDWG.Start.X, endPt.X, dwgForm);
            grip_Right = new eGripBox(endPt, layer, location.X, membDWG.End.X, dwgForm);
            float x = (location.X + endPt.X) / 2.0f;
            grip_middle = new eGripBox(new PointF(x, location.Y), layer, x - (location.X - membDWG.Start.X), x + (membDWG.End.X - endPt.X), dwgForm);

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

            //this.dim_middle = new eDimension(location, endPt, "", eDimensionType.LinearHorizontal, lay, eDimensionLinePosition.RightOrBottom, size * 15.0f);
            //this.dim_middle.Visible = false;

            //dim_Right.Start = endPt;

            //if (load_Tri.UnfactoredMagnitude < 0)
            //{
            //    dim_Left.DimensionLinePosition = eDimensionLinePosition.LeftOrAbove;
            //    dim_middle.DimensionLinePosition = eDimensionLinePosition.LeftOrAbove;
            //    dim_Right.DimensionLinePosition = eDimensionLinePosition.LeftOrAbove;
            //}
        }

        /// <summary>
        /// Handles the 'LocationChanged' event of the member.
        /// </summary>
        /// <param name="sender">The sender member dwg of the event.</param>
        /// <param name="e">Information about the event.</param>
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

        private void grip_middle_Move(object Sender, eGripBoxEventArgs e)
        {
            PointF loc = location;
            location = new PointF(e.Location.X - (endPt.X - loc.X) / 2.0f, loc.Y);
            endPt = new PointF(e.Location.X + (endPt.X - loc.X) / 2.0f, loc.Y);

            grip_Left.Location = location;
            grip_Right.Location = endPt;
            grip_Left.Max_X = grip_Right.Location.X;
            grip_Right.Min_X = grip_Left.Location.X;
            GenerateDwgObjects();

            //dim_Left.Text = Math.Round((location.X - grip_Left.Min_X) * eUtility.Convert(load_Rect.Member.Length, eUtility.SLU, this.lengthUnit) /
            //    (grip_Right.Max_X - grip_Left.Min_X), 3).ToString();
            //dim_Right.Text = Math.Round((grip_Right.Max_X - endPt.X) * eUtility.Convert(load_Rect.Member.Length, eUtility.SLU, this.lengthUnit) /
            //    (grip_Right.Max_X - grip_Left.Min_X), 3).ToString();

            //dim_Left.End = dim_middle.Start = location;
            //dim_middle.End = dim_Right.Start = endPt;
        }

        private void grip_middle_TurnedOff(object Sender, eGripBoxEventArgs e)
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

                load_Rect.Start = (location.X - grip_Left.Min_X) * load_Rect.Member.Length / (grip_Right.Max_X - grip_Left.Min_X);
                load_Rect.End = (grip_Right.Max_X - endPt.X) * load_Rect.Member.Length / (grip_Right.Max_X - grip_Left.Min_X);

                load_Tri.Start = load_Rect.Start;
                load_Tri.End = load_Rect.End;
            }

            OnMoveEnd();
        }

        private void grip_middle_TurnedOn(object Sender, eGripBoxEventArgs e)
        {
            //dim_Left.Visible = true;
            //dim_Left.Text = Math.Round(eUtility.Convert(load_Rect.Start, eUtility.SLU, this.lengthUnit), 3).ToString();
            //dim_Left.Start = new PointF(grip_Left.Min_X, location.Y);
            //dim_Left.End = location;

            //dim_middle.Visible = true;
            //dim_middle.Text = Math.Round(eUtility.Convert((load_Rect.Member.Length - load_Rect.Start - load_Rect.End), eUtility.SLU, this.lengthUnit), 3).ToString();
            //dim_middle.Start = location;
            //dim_middle.End = endPt;

            //dim_Right.Visible = true;
            //dim_Right.Text = Math.Round(eUtility.Convert(load_Rect.End, eUtility.SLU, this.lengthUnit), 3).ToString();
            //dim_Right.Start = endPt;
            //dim_Right.End = new PointF(grip_Right.Max_X, location.Y);

            OnMoveStart();
        }

        private void grip_Right_Move(object Sender, eGripBoxEventArgs e)
        {
            endPt = e.Location;
            GenerateDwgObjects();

            grip_Left.Max_X = endPt.X;
            grip_middle.Location = new PointF((location.X + endPt.X) / 2.0f, endPt.Y);
            grip_middle.Max_X = grip_middle.Location.X + (grip_Right.Max_X - endPt.X);
            grip_middle.Min_X = grip_middle.Location.X - (location.X - grip_Left.Min_X);

            //dim_middle.Text = Math.Round((endPt.X - location.X) * eUtility.Convert(load_Tri.Member.Length, eUtility.SLU, this.lengthUnit) / (grip_Right.Max_X - grip_Left.Min_X), 3).ToString();
            //dim_Right.Text = Math.Round((grip_Right.Max_X - endPt.X) * eUtility.Convert(load_Rect.End, eUtility.SLU, this.lengthUnit) / (grip_Right.Max_X - grip_Left.Min_X), 3).ToString();
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

                    double l = eUtility.Convert(textBox.DoubleValue, this.lengthUnit, eUtility.SLU);

                    if (l < 0)
                        load_Tri.End = 0;
                    else if (l > load_Tri.Member.Length - load_Tri.Start)
                        load_Tri.End = load_Tri.Member.Length - load_Tri.Start;
                    else
                        load_Tri.End = l;

                    load_Rect.End = load_Tri.End;

                    endPt.X = grip_Right.Max_X - (float)(load_Tri.End * (grip_Right.Max_X - grip_Left.Min_X) / load_Tri.Member.Length);

                    GenerateDwgObjects();

                    grip_Left.Location = location;
                    grip_middle.Location = new PointF((location.X + endPt.X) / 2.0f, location.Y);
                    grip_Right.Location = endPt;

                    grip_Left.Max_X = endPt.X;
                    grip_Right.Min_X = location.X;
                    grip_middle.Min_X = grip_middle.Location.X - location.X + grip_Left.Min_X;
                    grip_middle.Max_X = grip_middle.Location.X + grip_Right.Max_X - endPt.X;

                    //dim_Left.Visible = false;
                    //dim_middle.Visible = false;
                    //dim_Right.Visible = false;
                }
                else
                {
                    //dim_Left.Visible = false;
                    //dim_middle.Visible = false;
                    //dim_Right.Visible = false;

                    load_Tri.End = (grip_Right.Max_X - endPt.X) * load_Tri.Member.Length / (grip_Right.Max_X - grip_Left.Min_X);
                    load_Rect.End = load_Tri.End;
                }
            }

            OnMoveEnd();
        }

        private void grip_Right_TurnedOn(object Sender, eGripBoxEventArgs e)
        {
            //dim_Left.Visible = true;
            //dim_Left.Start = new PointF(grip_Left.Min_X, location.Y);
            //dim_Left.End = location;
            //dim_Left.Text = Math.Round(eUtility.Convert(load_Tri.Start, eUtility.SLU, this.lengthUnit), 3).ToString();

            //dim_middle.Visible = true;
            //dim_middle.Start = location;
            //dim_middle.End = endPt;
            //dim_middle.Text = Math.Round(eUtility.Convert((load_Tri.Member.Length - load_Tri.Start - load_Tri.End), eUtility.SLU, this.lengthUnit), 3).ToString();

            //dim_Right.Visible = true;
            //dim_Right.Start = endPt;
            //dim_Right.End = new PointF(grip_Right.Max_X, location.Y);
            //dim_Right.Text = Math.Round(eUtility.Convert(load_Tri.End, eUtility.SLU, this.lengthUnit), 3).ToString();

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

            //dim_middle.Text = Math.Round((endPt.X - location.X) * eUtility.Convert(load_Tri.Member.Length, eUtility.SLU, this.lengthUnit) / (grip_Right.Max_X - grip_Left.Min_X), 3).ToString();
            //dim_Left.Text = Math.Round((location.X - grip_Left.Min_X) * eUtility.Convert(load_Tri.Member.Length, eUtility.SLU, this.lengthUnit) / (grip_Right.Max_X - grip_Left.Min_X), 3).ToString();
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
                        load_Tri.Start = 0;
                    else if (l > (load_Tri.Member.Length - load_Tri.End))
                        load_Tri.Start = load_Tri.Member.Length - load_Tri.End;
                    else
                        load_Tri.Start = l;

                    load_Rect.Start = load_Tri.Start;
                    location.X = grip_Left.Min_X + (float)(load_Tri.Start * (grip_Right.Max_X - grip_Left.Min_X) / load_Tri.Member.Length);

                    grip_Left.Location = location;
                    grip_middle.Location = new PointF((endPt.X + location.X) / 2.0f, endPt.Y);
                    grip_Right.Location = endPt;

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

                    load_Rect.Start = (location.X - grip_Left.Min_X) * load_Rect.Member.Length / (grip_Right.Max_X - grip_Left.Min_X);
                    load_Tri.Start = (location.X - grip_Left.Min_X) * load_Tri.Member.Length / (grip_Right.Max_X - grip_Left.Min_X);
                }
            }

            OnMoveEnd();
        }

        private void grip_Left_TurnedOn(object Sender, eGripBoxEventArgs e)
        {
            //dim_Left.Visible = true;
            //dim_Left.Start = new PointF(grip_Left.Min_X, location.Y);
            //dim_Left.End = location;
            //dim_Left.Text = Math.Round(eUtility.Convert(load_Rect.Start, eUtility.SLU, this.lengthUnit), 3).ToString();

            //dim_Right.Visible = true;
            //dim_Right.Start = endPt;
            //dim_Right.End = new PointF(grip_Right.Max_X, location.Y);
            //dim_Right.Text = Math.Round(eUtility.Convert(load_Rect.End, eUtility.SLU, this.lengthUnit), 3).ToString();

            //dim_middle.Visible = true;
            //dim_middle.Start = location;
            //dim_middle.End = endPt;
            //dim_middle.Text = Math.Round(eUtility.Convert(load_Rect.Member.Length - load_Rect.Start - load_Rect.End, eUtility.SLU, this.lengthUnit), 3).ToString();

            OnMoveStart();
        }

        /// <summary>
        /// Handles the KeyDown event of the drawing form.
        /// </summary>
        /// <param name="sender">The sender form of the event.</param>
        /// <param name="e">Information about the event.</param>
        protected override void dwgForm_KeyDown(object sender, KeyEventArgs e)
        {
            base.dwgForm_KeyDown(sender, e);

            if (e.KeyCode == Keys.Enter)
            {
                if (textBox.Visible && !grip_Left.On && !grip_middle.On && !grip_Right.On) //editing one of the magnitudes.
                {
                    textBox.Visible = false;
                    textBox.Parent.Focus();

                    if (textBox.Location.X == (int)(text_left.Location.X - text_left.Width / 2.0f)) //editing the left text
                    {
                        AcceptMagnitude(textBox.DoubleValue, true);
                    }
                    else
                    {
                        AcceptMagnitude(textBox.DoubleValue, false);
                    }
                    GenerateDwgObjects();
                    OnMagnitudeChanged();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                location.X = (float)(grip_Left.Min_X + (load_Rect.Start * (grip_Right.Max_X - grip_Left.Min_X) / load_Rect.Member.Length));
                endPt.X = (float)(grip_Right.Max_X - (load_Rect.End * (grip_Right.Max_X - grip_Left.Min_X) / load_Rect.Member.Length));

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
                    //textBox.Font = dim_Left.TextObjet.TextStyle;
                    //textBox.Location = new Point((int)(dim_Left.TextObjet.Location.X - dim_Left.TextObjet.Width / 2.0f), (int)(dim_Left.TextObjet.Location.Y - dim_Left.TextObjet.Height / 2.0f));
                    textBox.Location = new Point((int)((this.grip_Left.Min_X + grip_Left.Location.X) / 2.0), (int)(grip_Left.Location.Y));
                }
                else if (grip_Right.On)
                {
                    textBox.Visible = true;
                    textBox.Text = "";
                    textBox.Focus();
                    //textBox.Font = dim_Right.TextObjet.TextStyle;
                    //textBox.Location = new Point((int)(dim_Right.TextObjet.Location.X - dim_Right.TextObjet.Width / 2.0f), (int)(dim_Right.TextObjet.Location.Y - dim_Right.TextObjet.Height / 2.0f));
                    textBox.Location = new Point((int)((this.grip_Right.Max_X + grip_Right.Location.X) / 2.0), (int)(grip_Right.Location.Y));
                }
            }
        }

        private void AcceptMagnitude(double Magnitude, bool IsLeft)
        {
            Magnitude = eUtility.Convert(Magnitude, this.forceUnit, eUtility.SFU) / eUtility.Convert(1, this.lengthUnit, eUtility.SLU);

            if (IsLeft)
            {
                if (load_Tri.Orientation == eTriangularLoadOrientation.LeftToRight)
                {
                    if (Math.Abs(Magnitude) > Math.Abs(load_Tri.UnfactoredMagnitude + load_Rect.UnfactoredMagnitude))
                    {
                        if (Magnitude * load_Rect.UnfactoredMagnitude < 0)
                        {
                            load_Rect.UnfactoredMagnitude *= -1;
                            load_Tri.UnfactoredMagnitude *= -1;
                        }
                        load_Tri.Orientation = eTriangularLoadOrientation.RightToLeft;
                        load_Rect.UnfactoredMagnitude += load_Tri.UnfactoredMagnitude;
                        load_Tri.UnfactoredMagnitude = Magnitude - load_Rect.UnfactoredMagnitude;
                    }
                    else
                    {
                        if (Magnitude * load_Rect.UnfactoredMagnitude < 0)
                        {
                            load_Rect.UnfactoredMagnitude *= -1;
                            load_Tri.UnfactoredMagnitude *= -1;
                        }
                        load_Tri.UnfactoredMagnitude += load_Rect.UnfactoredMagnitude - Magnitude;
                        load_Rect.UnfactoredMagnitude = Magnitude;
                    }
                }
                else
                {
                    if (Math.Abs(Magnitude) < Math.Abs(load_Rect.UnfactoredMagnitude))
                    {
                        if (Magnitude * load_Rect.UnfactoredMagnitude < 0)
                        {
                            load_Rect.UnfactoredMagnitude *= -1;
                            load_Tri.UnfactoredMagnitude *= -1;
                        }
                        load_Tri.Orientation = eTriangularLoadOrientation.LeftToRight;
                        load_Tri.UnfactoredMagnitude = load_Rect.UnfactoredMagnitude - Magnitude;
                        load_Rect.UnfactoredMagnitude = Magnitude;
                    }
                    else
                    {
                        if (Magnitude * load_Rect.UnfactoredMagnitude < 0)
                        {
                            load_Rect.UnfactoredMagnitude *= -1;
                            load_Tri.UnfactoredMagnitude *= -1;
                        }
                        load_Tri.UnfactoredMagnitude = Magnitude - load_Rect.UnfactoredMagnitude;
                    }
                }
            }
            else
            {
                if (load_Tri.Orientation == eTriangularLoadOrientation.LeftToRight)
                {
                    if (Math.Abs(load_Rect.UnfactoredMagnitude) > Math.Abs(Magnitude))
                    {
                        if (Magnitude * load_Rect.UnfactoredMagnitude < 0)
                        {
                            load_Rect.UnfactoredMagnitude *= -1;
                            load_Tri.UnfactoredMagnitude *= -1;
                        }
                        load_Tri.Orientation = eTriangularLoadOrientation.RightToLeft;
                        load_Tri.UnfactoredMagnitude = load_Rect.UnfactoredMagnitude - Magnitude;
                        load_Rect.UnfactoredMagnitude = Magnitude;
                    }
                    else
                    {
                        if (Magnitude * load_Rect.UnfactoredMagnitude < 0)
                        {
                            load_Rect.UnfactoredMagnitude *= -1;
                            load_Tri.UnfactoredMagnitude *= -1;
                        }
                        load_Tri.UnfactoredMagnitude = Magnitude - load_Rect.UnfactoredMagnitude;
                    }
                }
                else
                {
                    if (Math.Abs(Magnitude) > Math.Abs(load_Rect.UnfactoredMagnitude + load_Tri.UnfactoredMagnitude))
                    {
                        if (Magnitude * load_Rect.UnfactoredMagnitude < 0)
                        {
                            load_Rect.UnfactoredMagnitude *= -1;
                            load_Tri.UnfactoredMagnitude *= -1;
                        }
                        load_Tri.Orientation = eTriangularLoadOrientation.LeftToRight;
                        load_Rect.UnfactoredMagnitude += load_Tri.UnfactoredMagnitude;
                        load_Tri.UnfactoredMagnitude = Magnitude - load_Rect.UnfactoredMagnitude;
                    }
                    else
                    {
                        if (Magnitude * load_Rect.UnfactoredMagnitude < 0)
                        {
                            load_Rect.UnfactoredMagnitude *= -1;
                            load_Tri.UnfactoredMagnitude *= -1;
                        }
                        load_Tri.UnfactoredMagnitude += load_Rect.UnfactoredMagnitude - Magnitude;
                        load_Rect.UnfactoredMagnitude = Magnitude;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the 'MouseDoubleClick' event of the drawing form.
        /// </summary>
        /// <param name="sender">The sender form of the event.</param>
        /// <param name="e">Information about the event.</param>
        protected override void dwgForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Region r = new Region(new RectangleF(text_left.Location.X - text_left.Width / 2.0f, text_left.Location.Y - text_left.Height / 2.0f, text_left.Width, text_left.Height));

            if (r.IsVisible(e.Location)) //left text double clicked
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
                textBox.Location = new Point((int)(text_left.Location.X - text_left.Width / 2.0f), (int)(text_left.Location.Y - text_left.Height / 2.0f));
                textBox.Font = text_left.TextStyle;
                if (load_Tri.Orientation == eTriangularLoadOrientation.LeftToRight)
                    textBox.DoubleValue = eUtility.Convert(load_Rect.UnfactoredMagnitude, eUtility.SFU, this.forceUnit) / eUtility.Convert(1, eUtility.SLU, this.lengthUnit);
                else
                    textBox.DoubleValue = eUtility.Convert(load_Rect.UnfactoredMagnitude + load_Tri.UnfactoredMagnitude, eUtility.SFU, this.forceUnit) / eUtility.Convert(1, eUtility.SLU, this.lengthUnit);
            }
            else
            {
                r = new Region(new RectangleF(text_right.Location.X - text_right.Width / 2.0f, text_right.Location.Y - text_right.Height / 2.0f, text_right.Width, text_right.Height));

                if (r.IsVisible(e.Location))
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
                    textBox.Location = new Point((int)(text_right.Location.X - text_right.Width / 2.0f), (int)(text_right.Location.Y - text_right.Height / 2.0f));
                    textBox.Font = text_right.TextStyle;
                    if (load_Tri.Orientation == eTriangularLoadOrientation.RightToLeft)
                        textBox.DoubleValue = eUtility.Convert(load_Rect.UnfactoredMagnitude, eUtility.SFU, this.forceUnit) / eUtility.Convert(1, eUtility.SLU, this.lengthUnit);
                    else
                        textBox.DoubleValue = eUtility.Convert(load_Rect.UnfactoredMagnitude + load_Tri.UnfactoredMagnitude, eUtility.SFU, this.forceUnit) / eUtility.Convert(1, eUtility.SLU, this.lengthUnit);

                }
            }
        }

        /// <summary>
        /// Changes the dwg components according to the current selection state of the member.
        /// </summary>
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

        /// <summary>
        /// Generates all the drawing components of the load.
        /// </summary>
        protected override void GenerateDwgObjects()
        {
            dwgObjects = new List<eIDrawing>();

            double mag;
            float L, L_i, h1, h2, h_i, s, angle;
            int n, sign;
            PointF p1, p2, p_ei, p_si;

            L = endPt.X - location.X;
            h1 = (float)((member.Beam.Extent_V * layer.ZoomFactor * 0.1) +
                (Math.Abs(load_Tri.UnfactoredMagnitude + load_Rect.UnfactoredMagnitude) * member.Beam.Extent_V * layer.ZoomFactor / member.Beam.MaxMagnitude));
            h2 = (float)((member.Beam.Extent_V * layer.ZoomFactor * 0.1) +
                Math.Abs(load_Rect.UnfactoredMagnitude) * member.Beam.Extent_V * layer.ZoomFactor / member.Beam.MaxMagnitude);
            s = (h1 + h2) / 4.0f;

            if (L % s == 0)
                n = (int)(L / s);
            else
                n = (int)(L / s) + 1;

            n = n <= 1 ? 2 : n; //minimum two spacings

            n = n > 30 ? 30 : n;

            s = L / (float)n;

            n++; //to get the number of lines

            if (load_Rect.UnfactoredMagnitude >= 0 || load_Tri.UnfactoredMagnitude >= 0)
            {
                sign = -1;
                angle = 270;
            }
            else
            {
                sign = 1;
                angle = 90;
            }

            if (load_Tri.Orientation == eTriangularLoadOrientation.LeftToRight)
            {
                p1 = new PointF(location.X, location.Y + sign * h2);
                p2 = new PointF(endPt.X, endPt.Y + sign * h1);

                mag = eUtility.Convert(load_Rect.UnfactoredMagnitude, eUtility.SFU, this.forceUnit) / eUtility.Convert(1, eUtility.SLU, this.lengthUnit);
                text_left = new eText(Math.Round(Math.Abs(mag), 3).ToString(), p1, layer);
                mag = eUtility.Convert(load_Rect.UnfactoredMagnitude + load_Tri.UnfactoredMagnitude, eUtility.SFU, forceUnit) / eUtility.Convert(1, eUtility.SLU, this.lengthUnit);
                text_right = new eText(Math.Round(Math.Abs(mag), 3).ToString(), p2, layer);

                for (int i = 0; i < n; i++)
                {
                    L_i = i * s;
                    h_i = ((h1 * L_i) + h2 * (L - L_i)) / L;

                    p_si = new PointF(location.X + L_i, location.Y);
                    p_ei = new PointF(location.X + L_i, location.Y + sign * h_i);

                    dwgObjects.Add(new eLine(p_si, p_ei, layer));

                    float arr_h = 0.3f * h1 > h_i ? h_i : 0.3f * h1;
                    float arr_w = (1.0f / 3.0f) * arr_h;
                    dwgObjects.Add(new eArrowHead(p_si, angle, arr_w, arr_h, layer));
                }
            }
            else
            {
                p1 = new PointF(location.X, location.Y + sign * h1);
                p2 = new PointF(endPt.X, endPt.Y + sign * h2);

                mag = eUtility.Convert(load_Rect.UnfactoredMagnitude + load_Tri.UnfactoredMagnitude, eUtility.SFU, forceUnit) / eUtility.Convert(1, eUtility.SLU, this.lengthUnit);
                text_left = new eText(Math.Round(Math.Abs(mag), 3).ToString(), p1, layer);
                mag = eUtility.Convert(load_Rect.UnfactoredMagnitude, eUtility.SFU, this.forceUnit) / eUtility.Convert(1, eUtility.SLU, this.lengthUnit);
                text_right = new eText(Math.Round(Math.Abs(mag), 3).ToString(), p2, layer);

                for (int i = 0; i < n; i++)
                {
                    L_i = i * s;
                    h_i = ((h1 * L_i) + h2 * (L - L_i)) / L;

                    p_si = new PointF(endPt.X - L_i, endPt.Y);
                    p_ei = new PointF(endPt.X - L_i, endPt.Y + sign * h_i);

                    dwgObjects.Add(new eLine(p_si, p_ei, layer));

                    float arr_h = 0.3f * h1 > h_i ? h_i : 0.3f * h1;
                    float arr_w = (1.0f / 3.0f) * arr_h;
                    dwgObjects.Add(new eArrowHead(p_si, angle, arr_w, arr_h, layer));
                }
            }

            topLine = new eLine(p1, p2, layer);

            text_left.Location = new PointF(location.X, p1.Y + sign * text_left.Height / 2.0f);
            text_right.Location = new PointF(endPt.X, p2.Y + sign * text_right.Height / 2.0f);

            if (h1 > 0.0f && !float.IsInfinity(h1) && !float.IsNaN(h1))
            {
                text_left.Height = (float)(0.6 * member.Beam.Extent_V * layer.ZoomFactor);
                text_right.Height = (float)(0.6 * member.Beam.Extent_V * layer.ZoomFactor);
            }

            CreateRegion();
        }

        /// <summary>
        /// Creates the responsive region of the load dwg according the latest location point.
        /// </summary>
        protected override void CreateRegion()
        {
            GraphicsPath p = new GraphicsPath();

            if (location != topLine.Location && endPt != topLine.End)
                p.AddLines(new PointF[] { location, endPt, topLine.End, topLine.Location });
            else if (location != topLine.Location)
                p.AddLines(new PointF[] { location, topLine.Location, endPt });
            else if (endPt != topLine.End)
                p.AddLines(new PointF[] { location, topLine.End, endPt });
            else
                p = null;

            this.region = new Region(new RectangleF(text_left.Location.X - text_left.Width / 2.0f, text_left.Location.Y - text_left.Height / 2.0f, text_left.Width, text_left.Height));

            if (p != null)
                region.Union(new Region(p));

            region.Union(new RectangleF(text_right.Location.X - text_right.Width / 2.0f, text_right.Location.Y - text_right.Height / 2.0f, text_right.Width, text_right.Height));

            this.region.Exclude(new RectangleF(location.X - 5, location.Y - 5, 10, 10));
            this.region.Exclude(new RectangleF(endPt.X - 5, endPt.Y - 5, 10, 10));
            this.region.Exclude(new RectangleF((location.X + endPt.X) / 2.0f - 5, location.Y - 5, 10, 10));
        }

        /// <summary>
        /// Changes the drawing components according to the current highlight state of the load dwg.
        /// </summary>
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

        #endregion

        /// <summary>
        /// Handles the 'Resize' event of the member.
        /// </summary>
        /// <param name="sender">The sender member dwg of the event.</param>
        /// <param name="e">Information about the event.</param>
        protected override void member_Resize(object sender, eMemberGraphicsEventArgs e)
        {
            if (load_Rect.Member.Length == e.Length)
                return;

            load_Rect.Start = e.Length * load_Rect.Start / load_Rect.Member.Length;
            load_Rect.End = e.Length * load_Rect.End / load_Rect.Member.Length;

            load_Tri.Start = load_Rect.Start;
            load_Tri.End = load_Rect.End;

            location.X = e.Location.X + (float)(load_Rect.Start * (e.End.X - e.Location.X) / e.Length);
            endPt.X = e.End.X - (float)(load_Rect.End * (e.End.X - e.Location.X) / e.Length);

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
                if (load_Rect.UnfactoredMagnitude >= 0)
                    return 0.0f;
                else
                    return Math.Abs(location.Y - Math.Max(topLine.Location.Y, topLine.End.Y)) + text_left.Height;
            }
        }

        public override float MaxPosOffset
        {
            get
            {
                if (load_Rect.UnfactoredMagnitude < 0)
                    return 0.0f;
                else
                    return Math.Abs(location.Y - Math.Max(topLine.Location.Y, topLine.End.Y)) + text_left.Height;
            }
        }
    }
}
