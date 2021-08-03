using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ESADS.Mechanics.Analysis;
using ESADS.Mechanics.Analysis.Beam;
using ESADS.GUI.Controls;
using ESADS.GUI;

namespace ESADS.EGraphics.Beam
{
    /// <summary>
    /// Represents the graphic elements of a flexural moment load on a member.
    /// </summary>
    public class eGConcentratedMoment : eGLoad
    {
        #region Fields

        /// <summary>
        /// Holds the value of the 'Load' property.
        /// </summary>
        private eConcentratedMoment load;
        /// <summary>
        /// The grip box that defines the center of the load_Tri.
        /// </summary>
        private eGripBox grip;
        /// <summary>
        /// The text_left that represents the magnitude of the load_Tri.
        /// </summary>
        private eText text;
        /// <summary>
        /// The arc of moment dwg.
        /// </summary>
        private eArc arc;
        /// <summary>
        /// The arrow head for the moment dwg.
        /// </summary>
        private eArrowHead arrowHead;
        /// <summary>
        /// Holds the value of 'IsOnJoint'.
        /// </summary>
        private bool isOnJoint;
        private eGJoint joint;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new drawing to represent a flexural moment on a member.
        /// </summary>
        /// <param name="load_Tri">The mechanics object associated with the drawing.</param>
        /// <param name="location">The location of the center of the drawing.</param>
        /// <param name="layer">The layer on which the drawing is drawn.</param>
        /// <param name="dwgForm">The form on which the drawing is going to be displayed.</param>
        public eGConcentratedMoment(eConcentratedMoment load, eLayer layer, eGMember memberDWG, eModelForm dwgForm)
            : base(memberDWG, layer, dwgForm)
        {
            this.member.Beam.Changed += new EventHandler(Beam_Changed);
            this.load = load;
            this.location = new PointF((float)(memberDWG.Start.X + (load.Start * (memberDWG.End.X - memberDWG.Start.X) / (load.Member.Length))), memberDWG.Start.Y);

            GenerateDwgObjects();

            InitializeComponents(memberDWG, dwgForm);
        }

        /// <summary>
        /// Creates a new drawing to represent a flexural moment on a member.
        /// </summary>
        /// <param name="load_Tri">The mechanics object associated with the drawing.</param>
        /// <param name="location">The location of the center of the drawing.</param>
        /// <param name="layer">The layer on which the drawing is drawn.</param>
        /// <param name="dwgForm">The form on which the drawing is going to be displayed.</param>
        public eGConcentratedMoment(eConcentratedMoment load, eLayer layer, eGJoint jointDWG, eModelForm dwgForm)
            : base(layer, dwgForm)
        {
            this.load = load;
            this.joint = jointDWG;
            this.location = jointDWG.Location;

            GenerateDwgObjects();

            InitializeComponents(jointDWG, dwgForm);
        }


        private void InitializeComponents(eGJoint jointDWG, eModelForm dwgForm)
        {
            this.visible = true;
            base.InitializeComponents(dwgForm);
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
                grip.Location = value;
                location = value;
                GenerateDwgObjects();
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

        public override eColor Color
        {
            get
            {
                return text.Color;
            }
            set
            {
                text.Color = value;
                arc.Color = value;
                arrowHead.Color = value;
            }
        }
        /// <summary>
        /// Gets the mechanics object associated with this member drawing.
        /// </summary>
        public eConcentratedMoment Load
        {
            get
            {
                return load;
            }
        }

        public override float MaxNegOffset
        {
            get { return arc.Radius; }
        }

        public override float MaxPosOffset
        {
            get { return arc.Radius; }
        }

        #endregion

        #region Methods
        public override void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            base.Zoom(ZoomCenter, ZoomFactor);

            arrowHead.Zoom(ZoomCenter, ZoomFactor);
            arc.Zoom(ZoomCenter, ZoomFactor);
            text.Zoom(ZoomCenter, ZoomFactor);

            if (grip != null)
            {
                grip.Zoom(ZoomCenter, ZoomFactor);
                grip.Location = location; CreateRegion();
            }

            CreateRegion();
        }

        public override void Pan(float Xoffset, float Yoffset)
        {
            base.Pan(Xoffset, Yoffset);

            arrowHead.Pan(Xoffset, Yoffset);
            arc.Pan(Xoffset, Yoffset);
            text.Pan(Xoffset, Yoffset);

            if (grip != null)
            {
                grip.Pan(Xoffset, Yoffset);
                grip.Location = location;
            }

            CreateRegion();
        }

        public override void Draw(Graphics g)
        {
            if (!visible)
                return;
            arrowHead.Draw(g);
            arc.Draw(g);
            text.Draw(g);
            if (grip != null)
                grip.Draw(g);
        }

        private void Beam_Changed(object sender, EventArgs e)
        {
            this.GenerateDwgObjects();
        }

        protected override void GenerateDwgObjects()
        {
            float h;
            if (member != null)
                h = (float)(member.Beam.Extent_V * layer.ZoomFactor * 0.3);
            else
                h = (float)(joint.Beam.Extent_V * layer.ZoomFactor * 0.3);
            this.arc = new eArc(location, h, -135, 270, layer);
            this.arrowHead = new eArrowHead(arc.Start, -15, 0.5f * h, layer);
            if (load.UnfactoredMagnitude < 0)
            {
                arrowHead.Rotation = 15;
                arrowHead.Location = arc.End;
            }

            text = new eText(Math.Round(Math.Abs(eUtility.Convert(load.UnfactoredMagnitude, eUtility.SLU, lengthUnit, eUtility.SFU, this.forceUnit)), 3).ToString(), new PointF(), layer);
            text.Location = new PointF(location.X + (arc.Radius + text.Width / 2.0f) * 0.7f, location.Y - (arc.Radius + text.Height / 2.0f) * 0.7f);
            if (h > 0.0f && !float.IsInfinity(h) && !float.IsNaN(h))
                if (member != null)
                    text.Height = (float)(0.6 * member.Beam.Extent_V * layer.ZoomFactor);
                else
                    text.Height = (float)(0.6 * joint.Beam.Extent_V * layer.ZoomFactor);

            CreateRegion();
        }

        /// <summary>
        /// Creates a graphics region for the drawing.
        /// </summary>
        protected override void CreateRegion()
        {
            this.region = new Region(new RectangleF(text.Location.X - text.Width / 2, text.Location.Y - text.Height / 2, text.Width, text.Height));
            
            this.region.Union(arc.GetRegion(3));
            this.region.Union(arrowHead.GetRegion());

            this.region.Exclude(new RectangleF(location.X - 5, location.Y - 5, 10, 10));
        }

        /// <summary>
        /// Changes the selected property and the corresponding simultaneous changes.
        /// </summary>
        /// <param name="value">The value of the selection state.</param>
        protected override void ChangeSelect()
        {
            if (isSelected)
            {
                if(grip != null)
                grip.Visible = true;
                
                arc.LineType = new eLineType(eLineTypes.Dashed, 0.5f);
            }
            else
            {
                if (grip != null)
                    grip.Visible = false;

                arc.LineType = new eLineType(eLineTypes.Continuous);
            }
        }

        protected override void ChangeHighLight()
        {
            if (isHighlighted)
            {
                arc.LineWeight = new eLineWeight(3);

                if (isSelected)
                    arc.LineType = new eLineType(eLineTypes.Dashed, 1.0f / 6.0f);
            }
            else
            {
                arc.LineWeight = new eLineWeight(1.0f);

                if (isSelected)
                    arc.LineType = new eLineType(eLineTypes.Dashed, 0.5f);
            }
        }

        protected override void InitializeComponents(eGMember memberDWG, eModelForm dwgForm)
        {
            this.visible = true;
            base.InitializeComponents(memberDWG, dwgForm);

            this.grip = new eGripBox(location, layer, memberDWG.Start.X, memberDWG.End.X, dwgForm);

            this.grip.TurnedOn += new eGripBoxEventHandler(grip_TurnedOn);
            this.grip.TurnedOff += new eGripBoxEventHandler(grip_TurnedOff);
            this.grip.Move += new eGripBoxEventHandler(grip_Move);

        }

        protected override void memberDWG_LocationChanged(object sender, eMemberGraphicsEventArgs e)
        {
            location = e.Location;

            grip.Location = e.Location;

            grip.Min_X = e.Location.X;
            grip.Max_X = e.End.X;

            GenerateDwgObjects();
        }

        private void grip_Move(object Sender, eGripBoxEventArgs e)
        {
            this.Location = e.Location;

            GenerateDwgObjects();
        }

        private void grip_TurnedOff(object Sender, eGripBoxEventArgs e)
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
                    else if (l < 0)
                        load.Start = 0;
                    else
                        load.Start = load.Member.Length;
                    isSelected = false;
                    this.location.X = (float)(grip.Min_X + (load.Start * (grip.Max_X - grip.Min_X) / (load.Member.Length)));
                    grip.Location = location;
                    ChangeSelect();
                    GenerateDwgObjects();
                }
                else
                {
                    load.Start = (grip.Location.X - grip.Min_X) * (load.Member.Length / (grip.Max_X - grip.Min_X));
                }
            }
            else
            {
                this.location.X = (float)(grip.Min_X + (load.Start * (grip.Max_X - grip.Min_X) / (load.Member.Length)));
                grip.Location = location;
                GenerateDwgObjects();
            }

            OnMoveEnd();
        }

        private void grip_TurnedOn(object Sender, eGripBoxEventArgs e)
        {
            OnMoveStart();
        }

        protected override void dwgForm_KeyDown(object sender, KeyEventArgs e)
        {
            base.dwgForm_KeyDown(sender, e);

            if (e.KeyCode == Keys.Enter)
            {
                if (textBox.Visible) //entering input
                {
                    if (grip != null && !grip.On) //editing magnitude
                    {
                        textBox.Visible = false;
                        (sender as Form).Focus();

                        load.UnfactoredMagnitude = eUtility.Convert(textBox.DoubleValue, this.lengthUnit, eUtility.SLU, this.forceUnit, eUtility.SFU);
                        isSelected = false;
                        ChangeSelect();
                        GenerateDwgObjects();
                    }
                    if (joint != null)
                    {
                        textBox.Visible = false;
                        (sender as Form).Focus();

                        load.UnfactoredMagnitude = eUtility.Convert(textBox.DoubleValue, this.lengthUnit, eUtility.SLU, this.forceUnit, eUtility.SFU);
                        isSelected = false;
                        ChangeSelect();
                        GenerateDwgObjects();
                    }
                }
                else if (grip != null && isSelected && grip.On) 
                {
                    load.Start = (grip.Location.X - grip.Min_X) * (load.Member.Length / (grip.Max_X - grip.Min_X));
                    isSelected = false;
                    ChangeSelect();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (grip != null)
                {
                    location.X = (float)(grip.Min_X + (load.Start * ((grip.Max_X - grip.Min_X) / load.Member.Length)));
                    grip.Location = location;
                    GenerateDwgObjects();
                }

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
            else if (grip != null && grip.On && NumericKeyPressed(e) && !textBox.Visible)
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
            if ((new Region(new RectangleF(text.Location.X - text.Width / 2.0f, text.Location.Y - text.Height / 2.0f, text.Width, text.Height)).IsVisible(e.Location)))
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
                textBox.DoubleValue = eUtility.Convert(load.UnfactoredMagnitude, eUtility.SLU, this.lengthUnit, eUtility.SFU, this.forceUnit);
                textBox.Location = new Point((int)(text.Location.X - text.Width / 2.0f), (int)(text.Location.Y - text.Height / 2.0f));
            }
        }

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

        internal override void ReleaseHandlers(eModelForm dwgForm)
        {
            if (member != null)
            {
                this.member.Beam.Changed -= new EventHandler(Beam_Changed);
                grip.ReleaseHandlers(dwgForm);
            }

            base.ReleaseHandlers(dwgForm);
        }
        #endregion
    }
}
