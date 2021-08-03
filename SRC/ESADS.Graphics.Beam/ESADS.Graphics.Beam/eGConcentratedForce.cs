using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ESADS.Mechanics.Analysis;
using ESADS.GUI.Controls;
using ESADS.GUI;
using ESADS.Mechanics.Analysis.Beam;

namespace ESADS.EGraphics.Beam
{
    /// <summary>
    /// Represents the graphic elements of a concentrated force load on a member.
    /// </summary>
    public class eGConcentratedForce : eGLoad
    {
        #region Fields
        /// <summary>
        /// The mechanics object holding the properties of the member.
        /// </summary>
        private eConcentratedForce load;
        /// <summary>
        /// The grip box to move the load_Tri.
        /// </summary>
        private eGripBox grip;
        /// <summary>
        /// The line to represent the load_Tri.
        /// </summary>
        private eLine line;
        /// <summary>
        /// The arrow head.
        /// </summary>
        private eArrowHead arrowHead;
        /// <summary>
        /// The text_left to show the magnitude.
        /// </summary>
        private eText text;
        /// <summary>
        /// Holds the value if the load is found on joint or member.
        /// </summary>
        private bool isOnJoint;
        private eGJoint joint;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a member drawing to represent a concentrated force.
        /// </summary>
        /// <param name="location">The location of the member.</param>
        /// <param name="member">The mechanics class representing a concentrated member.</param>
        /// <param name="layer">The layer onto which to draw the member.</param>
        /// <param name="dwgForm">The form onto which to draw the member.</param>
        /// <param name="size">The size of the member drawing.</param>
        /// <param name="memberDWG">The member drawing on which the load_Tri is loaded on .</param>
        public eGConcentratedForce(eConcentratedForce load, eLayer layer, eGMember memberDWG, eModelForm dwgForm, float size = 0.5f)
            : base(memberDWG, layer, dwgForm)
        {
            this.member.Beam.Changed += new EventHandler(Beam_Changed);
            this.location = new PointF((float)(memberDWG.Start.X + (load.Start * (memberDWG.End.X - memberDWG.Start.X) / (load.Member.Length))), memberDWG.Start.Y);
            this.load = load;

            GenerateDwgObjects();

            InitializeComponents(memberDWG, dwgForm);
        }

        /// <summary>
        /// Creates a member drawing to represent a concentrated force.
        /// </summary>
        /// <param name="location">The location of the member.</param>
        /// <param name="member">The mechanics class representing a concentrated member.</param>
        /// <param name="layer">The layer onto which to draw the member.</param>
        /// <param name="dwgForm">The form onto which to draw the member.</param>
        /// <param name="size">The size of the member drawing.</param>
        /// <param name="memberDWG">The member drawing on which the load_Tri is loaded on .</param>
        public eGConcentratedForce(eConcentratedForce load, eLayer layer, eGJoint jointDWG, eModelForm dwgForm, float size = 0.5f)
            : base(layer, dwgForm)
        {
            this.location = jointDWG.Location;
            this.load = load;
            this.joint = jointDWG;

            GenerateDwgObjects();

            InitializeComponents(jointDWG, dwgForm);
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets the concentrated load represented by this drawing.
        /// </summary>
        public eConcentratedForce Load
        {
            get
            {
                return load;
            }
        }

        private new eLengthUnits lengthUnit
        {
            get
            {
                if (member != null)
                    return member.Beam.Document.LengthUnit;
                else
                    return joint.Beam.Document.LengthUnit;
            }
        }

        private new eForceUints forceUnit
        {
            get
            {
                if (member != null)
                    return member.Beam.Document.ForceUnit;
                else
                    return joint.Beam.Document.ForceUnit;
            }
        }

        /// <summary>
        /// Gets or sets the location of the load.
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
                if (grip != null)
                    grip.Location = value;
                GenerateDwgObjects();
            }
        }
        /// <summary>
        /// Gets or sets the color of the laod.
        /// </summary>
        public override eColor Color
        {
            get
            {
                return text.Color;
            }
            set
            {
                text.Color = value;
                line.Color = value;
                arrowHead.Color = value;
            }
        }

        public override float MaxNegOffset
        {
            get
            {
                if (load.UnfactoredMagnitude >= 0)
                    return 0.0f;
                else
                {
                    return Math.Abs(line.Location.Y - line.End.Y) + text.Height;
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
                    return Math.Abs(line.Location.Y - line.End.Y) + text.Height;
                }

            }
        }

        public bool IsOnJoint
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
        #endregion

        #region Methods
        private void InitializeComponents(eGJoint jointDWG, eModelForm dwgForm)
        {
            this.visible = true;

            this.load.Changed += new eLoadChangedEventHandler(load_Changed_ForJoint);

            base.InitializeComponents(dwgForm);
        }

        /// <summary>
        /// Initializes the drawing components of the concentrated force drawing.
        /// </summary>
        protected override void InitializeComponents(eGMember memberDWG, eModelForm dwgForm)
        {
            this.visible = true;

            this.grip = new eGripBox(this.location, layer, memberDWG.Start.X, memberDWG.End.X, dwgForm);

            this.grip.TurnedOff += new eGripBoxEventHandler(grip_TurnedOff);
            this.grip.TurnedOn += new eGripBoxEventHandler(grip_TurnedOn);
            this.grip.Move += new eGripBoxEventHandler(grip_Move);

            this.load.Changed += new eLoadChangedEventHandler(load_Changed);

            base.InitializeComponents(memberDWG, dwgForm);

            if (load.UnfactoredMagnitude < 0)
            {
                //dim_Left.DimensionLinePosition = eDimensionLinePosition.LeftOrAbove;
                //dim_Right.DimensionLinePosition = eDimensionLinePosition.LeftOrAbove;
            }

        }
        
        /// <summary>
        /// Generate all the necessary drawing objects for the load_Tri.
        /// </summary>
        protected override void GenerateDwgObjects()
        {
            float h;
            PointF end = new PointF();

            if (member != null)
                h = (float)(member.Beam.Extent_V * layer.ZoomFactor * 1.7);
            else
                h = (float)(joint.Beam.Extent_V * layer.ZoomFactor * 1.7);

            end.X = location.X;

            if (load.UnfactoredMagnitude > 0)
            {
                end.Y = (float)(location.Y - h);
                arrowHead = new eArrowHead(location, 270, 0.15f * h, layer);

                line = new eLine(location, end, this.layer);

                text = new eText(Math.Round(Math.Abs(eUtility.Convert(load.UnfactoredMagnitude, eUtility.SFU, this.forceUnit)), 3).ToString(), new PointF(), layer);
                text.Location = new PointF(location.X, end.Y - text.Height / 2.0f);
            }
            else
            {
                end.Y = (float)(location.Y + h);
                arrowHead = new eArrowHead(location, 90, 0.15f * h, layer);

                line = new eLine(end, location, this.layer);

                text = new eText(Math.Round(Math.Abs(eUtility.Convert(load.UnfactoredMagnitude, eUtility.SFU, this.forceUnit)), 3).ToString(), new PointF(), layer);
                text.Location = new PointF(location.X, end.Y + text.Height / 2.0f);
            }

            if (h > 0.0f && !float.IsInfinity(h) && !float.IsNaN(h))
                if (member != null)
                    text.Height = (float)(0.6 * member.Beam.Extent_V * layer.ZoomFactor);
                else
                    text.Height = (float)(0.6 * joint.Beam.Extent_V * layer.ZoomFactor);

            line.ZoomDashPattern = false;

            CreateRegion();
        }

        protected override void ChangeSelect()
        {
            if (isSelected)
            {
                if (grip != null)
                    grip.Visible = true;

                if (isHighlighted)
                {
                    line.LineType = new eLineType(eLineTypes.Dashed, 1 / 6.0f);
                }
                else
                {
                    line.LineType = new eLineType(eLineTypes.Dashed, 0.5f);
                }
            }
            else
            {
                if (grip != null)
                    grip.Visible = false;
                line.LineType = new eLineType(eLineTypes.Continuous);
            }
        }

        protected override void ChangeHighLight()
        {
            if (isHighlighted)
            {
                line.LineWeight = new eLineWeight(4.0f);

                if (isSelected)
                {
                    line.LineType = new eLineType(eLineTypes.Dashed, 1.0f / 6.0f);
                }
            }
            else if (line.LineWeight != 1.0f)
            {
                line.LineWeight = new eLineWeight(2.0f);

                if (isSelected)
                    line.LineType = new eLineType(eLineTypes.Dashed, 0.5f);
            }
        }

        /// <summary>
        /// Scales the conc. load dwg.
        /// </summary>
        /// <param name="ZoomCenter">The center of scaling.</param>
        /// <param name="ZoomFactor">The scale factor.</param>
        public override void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            base.Zoom(ZoomCenter, ZoomFactor);

            arrowHead.Zoom(ZoomCenter, ZoomFactor);
            line.Zoom(ZoomCenter, ZoomFactor);
            text.Zoom(ZoomCenter, ZoomFactor);

            if (grip != null)
            {
                grip.Zoom(ZoomCenter, ZoomFactor);
                grip.Location = location;
            }

            CreateRegion();
        }

        /// <summary>
        /// Moves the Concentrated load drawing.
        /// </summary>
        /// <param name="Xoffset">Amount of offset in X.</param>
        /// <param name="Yoffset">Amount of offset in Y</param>
        public override void Pan(float Xoffset, float Yoffset)
        {
            base.Pan(Xoffset, Yoffset);

            arrowHead.Pan(Xoffset, Yoffset);
            line.Pan(Xoffset, Yoffset);
            text.Pan(Xoffset, Yoffset);

            if (grip != null)
            {
                grip.Pan(Xoffset, Yoffset);
                grip.Location = location;
            }

            CreateRegion();
        }

        /// <summary>
        /// Draws the Concentrated laod using a graphics object.
        /// </summary>
        /// <param name="g">The graphics object used to draw the load.</param>
        public override void Draw(Graphics g)
        {
            if (!visible)
                return;
            line.Draw(g);
            text.Draw(g);
            arrowHead.Draw(g);

            if (grip != null)
                grip.Draw(g);

            //dim_Left.Draw(g);
            //dim_Right.Draw(g);
        }

        /// <summary>
        /// Creates the region from the other points.
        /// </summary>
        protected override void CreateRegion()
        {
            region = new Region(new RectangleF(text.Location.X - text.Width / 2.0f, text.Location.Y - text.Height / 2.0f, text.Width, text.Height));

            region.Union(arrowHead.GetRegion());

            region.Union(new RectangleF(location.X - line.LineWeight, line.End.Y, 2 * line.LineWeight, line.Length));

            this.region.Exclude(new RectangleF(location.X - 5, location.Y - 5, 10, 10));
        }

        internal override void ReleaseHandlers(eModelForm dwgForm)
        {
            if (this.member != null)
            {
                member.Beam.Changed -= new EventHandler(Beam_Changed);
                base.ReleaseHandlers(dwgForm);

                grip.ReleaseHandlers(dwgForm);
            }
            else
            {
                base.ReleaseHandlers(dwgForm);
            }
        }

        private void load_Changed_ForJoint(object sender, eLoadChangedEventArgs e)
        {
            GenerateDwgObjects();
        }

        private void Beam_Changed(object sender, EventArgs e)
        {
            this.GenerateDwgObjects();
        }

        /// <summary>
        /// Handles the LocationChanged event of the member.
        /// </summary>
        protected override void memberDWG_LocationChanged(object sender, eMemberGraphicsEventArgs e)
        {
            float strt = location.X - grip.Min_X;

            grip.Min_X = e.Location.X;
            grip.Max_X = e.End.X;
            location = new PointF(e.Location.X + strt, e.Location.Y);
            grip.Location = location;

            GenerateDwgObjects();
        }

        private void load_Changed(object sender, eLoadChangedEventArgs e)
        {
            location.X = (float)((load.Start) * (grip.Max_X - grip.Min_X) / load.Member.Length);
            grip.Location = location;

            GenerateDwgObjects();
        }

        private void grip_Move(object sender, eGripBoxEventArgs e)
        {
            this.Location = e.Location;

            //dim_Left.Text = Math.Round((grip.Location.X - grip.Min_X) * (eUtility.Convert(load.Member.Length, eUtility.SLU, this.lengthUnit) / (grip.Max_X - grip.Min_X)), 3).ToString();
            //dim_Right.Text = Math.Round((eUtility.Convert(load.Member.Length, eUtility.SLU, this.lengthUnit) - double.Parse(dim_Left.Text)), 3).ToString();

            //dim_Left.End = location;
            //dim_Right.Start = location;
            GenerateDwgObjects();
        }

        private void grip_TurnedOn(object sender, eGripBoxEventArgs e)
        {
            //dim_Left.Visible = true;
            //dim_Right.Visible = true;

            //dim_Left.Text = Math.Round(eUtility.Convert( load.Start,eUtility.SLU, this.lengthUnit), 3).ToString();
            //dim_Right.Text = Math.Round(eUtility.Convert(load.Member.Length - load.Start, eUtility.SLU, this.lengthUnit), 3).ToString();

            //dim_Left.Start = new PointF(grip.Min_X, location.Y);
            //dim_Left.End = location;

            //dim_Right.Start = location;
            //dim_Right.End = new PointF(grip.Max_X, location.Y);

            OnMoveStart();
        }

        private void grip_TurnedOff(object sender, eGripBoxEventArgs e)
        {
            if (!e.SuppressChanges)
            {
                if (textBox.Visible)
                {
                    textBox.Visible = false;
                    (textBox.Parent as Form).Focus();
                    double l = eUtility.Convert(textBox.DoubleValue, this.lengthUnit, eUtility.SLU);

                    if (l >= 0 && l <= load.Member.Length)
                        load.Start = l;
                    else if (textBox.DoubleValue < 0)
                        load.Start = 0;
                    else
                        load.Start = load.Member.Length;
                    isSelected = false;
                    this.location.X = (float)(grip.Min_X + (load.Start * (grip.Max_X - grip.Min_X) / (load.Member.Length)));
                    grip.Location = location;
                    ChangeSelect();
                    GenerateDwgObjects();
                    //dim_Left.Visible = false;
                    //dim_Right.Visible = false;
                }
                else
                {
                    load.Start = (grip.Location.X - grip.Min_X) * (load.Member.Length / (grip.Max_X - grip.Min_X));
                    //dim_Left.Visible = false;
                    //dim_Right.Visible = false;
                }
            }
            else
            {
                this.location.X = (float)(grip.Min_X + (load.Start * (grip.Max_X - grip.Min_X) / (load.Member.Length)));
                grip.Location = location;
                //dim_Left.Visible = false;
                //dim_Right.Visible = false;
                GenerateDwgObjects();
            }

            OnMoveEnd();
        }

        protected override void dwgForm_KeyDown(object sender, KeyEventArgs e)
        {
            base.dwgForm_KeyDown(sender, e);

            if (e.KeyCode == Keys.Enter)
            {
                if (textBox.Visible) //entering input
                {
                    textBox.Visible = false;
                    (textBox.Parent as Form).Focus();
                    double mag = eUtility.Convert(textBox.DoubleValue, this.forceUnit, eUtility.SFU);
                    if (mag != load.UnfactoredMagnitude)
                    {
                        load.UnfactoredMagnitude = mag;
                        isSelected = false;
                        ChangeSelect();
                        GenerateDwgObjects();
                        OnMagnitudeChanged();
                    }
                }
                else if (member != null && isSelected && grip.On)
                {
                    load.Start = (grip.Location.X - grip.Min_X) * (load.Member.Length / (grip.Max_X - grip.Min_X));
                    isSelected = false;
                    ChangeSelect();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (member != null)
                {
                    location.X = (float)(grip.Min_X + (load.Start * (grip.Max_X - grip.Min_X) / load.Member.Length));
                    grip.Location = location;
                }
                GenerateDwgObjects();

                if (textBox.Visible)
                    textBox.Visible = false;
                else if (isSelected)
                {
                    isSelected = false;
                    ChangeSelect();
                }

                (sender as Form).Focus();
                (sender as Form).Invalidate();
            }
            else if (member != null && grip.On && NumericKeyPressed(e) && !textBox.Visible)
            {
                (sender as eModelForm).EditingText = true;
                textBox.Visible = true;
                textBox.Text = "";
                textBox.Focus();
                //textBox.Location = new Point((int)(dim_Left.TextObjet.Location.X - dim_Left.TextObjet.Width / 2.0f), (int)(dim_Left.TextObjet.Location.Y - dim_Left.TextObjet.Height / 2.0f));
                textBox.Location = new Point((int)((this.grip.Min_X + grip.Location.X) / 2.0), (int)(grip.Location.Y));
            }
        }

        protected override void dwgForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (((member != null && !grip.On) || joint != null && isSelected) &&
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
                textBox.DoubleValue = eUtility.Convert(load.UnfactoredMagnitude, eUtility.SFU, this.forceUnit);
                textBox.Location = new Point((int)(text.Location.X - text.Width / 2.0f), (int)(text.Location.Y - text.Height / 2.0f));
            }
        }

        /// <summary>
        /// Handles the Resize event of the member.
        /// </summary>
        protected override void member_Resize(object sender, eMemberGraphicsEventArgs e)
        {
            if (load.Member.Length == e.Length)
                return;

            load.Start = e.Length * load.Start / load.Member.Length;

            location.X = e.Location.X + (float)(load.Start * (e.End.X - e.Location.X) / e.Length);

            grip.Location = location;
            grip.Min_X = e.Location.X;
            grip.Max_X = e.End.X;

            GenerateDwgObjects();
        }
        #endregion
    }
}
