using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ESADS.GUI.Controls;
using ESADS.GUI;

namespace ESADS.EGraphics.Beam
{
    /// <summary>
    /// Provides the common graphic elements for any  type of load put onto a member.
    /// </summary>
    public abstract class eGLoad : eIDrawing
    {

        #region Fields
        /// <summary>
        /// Value of the 'Size' property.
        /// </summary>
        protected float size = 1;
        /// <summary>
        /// Value of the 'IsSelected' property.
        /// </summary>
        protected bool isSelected = false;
        /// <summary>
        /// Value indicating if the load is focused.
        /// </summary>
        protected bool isHighlighted = false;
        /// <summary>
        /// Used to receive numeric inputs from the user.
        /// </summary>
        protected eNumericTextBox textBox;
        /// <summary>
        /// Dimension showing the distance from the start of the member to the start of the load.
        /// </summary>
        //protected eDimension dim_Left;
        /// <summary>
        /// Dimensio showing the distance from the end of the load to the end of the member.
        /// </summary>
        //protected eDimension dim_Right;
        /// <summary>
        /// Value of the 'Location' property.
        /// </summary>
        protected PointF location;
        /// <summary>
        /// The responsive region of the load.
        /// </summary>
        protected Region region;
        /// <summary>
        /// Value of the 'Layer' property.
        /// </summary>
        protected eLayer layer;
        /// <summary>
        /// The scale of the load.
        /// </summary>
        protected float zoomFactor = 1;
        #endregion

        #region Constructors

        /// <summary>
        /// Creates the basic drawing components of a load provided, the layer, the member drawing and the drawing form.
        /// </summary>
        /// <param name="layer">The layer onto which the load is drawn.</param>
        /// <param name="membDWG">The member drawing onto which the load is placed.</param>
        /// <param name="dwgForm">The form on which the drawings are drawn.</param>
        protected eGLoad(eGMember member, eLayer layer, eModelForm dwgForm)
        {
            this.member = member;
            this.layer = layer;
        }

        public eGLoad(eLayer layer, eModelForm dwgForm)
        {
            // TODO: Complete member initialization
            this.layer = layer;
            this.dwgForm = dwgForm;
        }
        #endregion
        private void layer_RectangularSelection(object sender, eRectangularSelectionEventArgs e)
        {
            if ((textBox.Parent as eModelForm).Locked)
                return;
            
            if (e.SuppressEvent)
                return;

            Graphics g = (new Label()).CreateGraphics();
            Region r = e.Region.Clone();
            r.Intersect(region);

            if (!e.IsPositive)
            {
                if (!r.IsEmpty(g))
                {
                    this.isSelected = true;
                    ChangeSelect();
                }
            }
            else
            {
                if (r.Equals(this.region, g))
                {
                    this.isSelected = true;
                    ChangeSelect();
                }
            }
        }


        #region Properties

        /// <summary>
        /// Gets or sets the value whether the load is selected or not.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                ChangeSelect();
            }
        }

        /// <summary>
        /// Gets or sets the size of the load dwg.
        /// </summary>
        public float Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
                GenerateDwgObjects();
            }
        }

        /// <summary>
        /// Gets or sets the left most point of intersection of the load with member
        /// </summary>
        public abstract PointF Location { get; set; }

        /// <summary>
        /// Gets the layer on which the load is drawn.
        /// </summary>
        public eLayer Layer
        {
            get { return this.layer; }
        }

        /// <summary>
        /// Implemented to get or set the color of the load dwg.
        /// </summary>
        public abstract eColor Color { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Implemented to initialize the load by adding event handlers and other components.
        /// </summary>
        /// <param name="memberDWG">The member drawing.</param>
        /// <param name="dwgForm">The drawing form on which the drawings are placed.</param>
        protected virtual void InitializeComponents(eGMember memberDWG, eModelForm dwgForm)
        {
            dwgForm.KeyPreview = true;
            dwgForm.MouseClick += new MouseEventHandler(dwgForm_MouseClick);
            dwgForm.KeyDown += new KeyEventHandler(dwgForm_KeyDown);
            dwgForm.MouseDoubleClick += new MouseEventHandler(dwgForm_MouseDoubleClick);
            dwgForm.MouseMove += new MouseEventHandler(dwgForm_MouseMove);
            dwgForm.UnitChanged += new eUnitChangedEventHandler(dwgForm_UnitChanged);

            memberDWG.LocationChanged += new eMemberGraphicsEventHandler(memberDWG_LocationChanged);
            memberDWG.Resize += new eMemberGraphicsEventHandler(member_Resize);

            this.layer.RectangularSelectionEnded += new eRectangularSelectorEventHandler(layer_RectangularSelection);

            this.textBox = new eNumericTextBox();
            dwgForm.Controls.Add(textBox);
            this.textBox.BackColor = System.Drawing.SystemColors.MenuText;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.ForeColor = System.Drawing.Color.White;
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(100, 20);
            this.textBox.TabIndex = 0;
            this.textBox.AutomaticResize = true;
            this.textBox.Visible = false;

            //eLayer lay = new eLayer("", System.Drawing.Color.FromArgb(100, 250, 250, 250));
            //Font f = new Font("Arial", 7);
            //lay.TextStyle = new eTextStyle(f, eChangeBy.ByObject);

            //this.dim_Left = new eDimension(memberDWG.Start, location, "", eDimensionType.LinearHorizontal, lay, eDimensionLinePosition.RightOrBottom, size * 15);
            //this.dim_Left.Visible = false;

            //this.dim_Right = new eDimension(location, memberDWG.End, "", eDimensionType.LinearHorizontal, lay, eDimensionLinePosition.RightOrBottom, size * 15);
            //this.dim_Right.Visible = false;

        }

        /// <summary>
        /// Implemented to handle the 'Resize' event of the member.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        protected abstract void member_Resize(object sender, eMemberGraphicsEventArgs e);

        /// <summary>
        /// Implemented to handle the 'LocationChanged' event of the member.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        protected abstract void memberDWG_LocationChanged(object sender, eMemberGraphicsEventArgs e);

        /// <summary>
        /// Handler for the drawing form's 'UnitChanged' event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        private void dwgForm_UnitChanged(object sender, eUnitChangedEventArgs e)
        {
            GenerateDwgObjects();
            //(sender as Form).Invalidate();
        }

        /// <summary>
        /// Handler for the drawing form's 'MouseMove' event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        private void dwgForm_MouseMove(object sender, MouseEventArgs e)
        {
            if ((sender as eModelForm).Locked)
                return;
            bool res = region.IsVisible(e.Location);

            if (res != isHighlighted)
            {
                isHighlighted = res;

                ChangeHighLight();

                (sender as Form).Invalidate(region);
            }
        }

        /// <summary>
        /// Handler to be implemented for the drawing form's 'MouseDoubleClick' event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        protected abstract void dwgForm_MouseDoubleClick(object sender, MouseEventArgs e);

        /// <summary>
        /// Handler to be implemented for the drawing form's 'KeyDown' event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        protected virtual void dwgForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((sender as eModelForm).Locked)
                return;
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
            {
                (sender as eModelForm).EditingText = false;
            }
        }

        /// <summary>
        /// Handler for the drawing form's 'MouseClick' event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Information about the event.</param>
        private void dwgForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if ((sender as eModelForm).Locked)
                return;

            if (region.IsVisible(e.Location))
            {
                isSelected = !isSelected;
                ChangeSelect();

                (sender as Form).Invalidate(region);
                (sender as eModelForm).ObjFoundBelowClickPt = true;
            }
        }

        /// <summary>
        /// Returns true if the last pressed key from the keyboard is a numeric key.
        /// </summary>
        /// <param name="e">The key event argument bearing the information.</param>
        protected bool NumericKeyPressed(KeyEventArgs e)
        {
            if (e.KeyCode != Keys.NumPad0 && e.KeyCode != Keys.NumPad1 && e.KeyCode != Keys.NumPad2 && e.KeyCode != Keys.NumPad3 && e.KeyCode != Keys.NumPad4 &&
                e.KeyCode != Keys.NumPad5 && e.KeyCode != Keys.NumPad6 && e.KeyCode != Keys.NumPad7 && e.KeyCode != Keys.NumPad8 && e.KeyCode != Keys.NumPad9 &&
                e.KeyCode != Keys.D0 && e.KeyCode != Keys.D1 && e.KeyCode != Keys.D2 && e.KeyCode != Keys.D3 && e.KeyCode != Keys.D4 && e.KeyCode != Keys.D5 &&
                e.KeyCode != Keys.D6 && e.KeyCode != Keys.D7 && e.KeyCode != Keys.D8 && e.KeyCode != Keys.D9 && e.KeyCode != Keys.Back && e.KeyCode != Keys.OemMinus)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Implemented to generate all the drawing components needed for the load drawing.
        /// </summary>
        protected abstract void GenerateDwgObjects();

        /// <summary>
        /// Implemented to create the responsive region of the load.
        /// </summary>
        protected abstract void CreateRegion();

        /// <summary>
        /// Implemented to change the graphics components based on the current highlight state.
        /// </summary>
        protected abstract void ChangeHighLight();

        /// <summary>
        /// Implemented to change the graphics components of the load based on the current selection state.
        /// </summary>
        protected abstract void ChangeSelect();

        /// <summary>
        /// Implemented to scale the drawing with a scale factor about a point.
        /// </summary>
        /// <param name="ZoomCenter">The center of the scaling.</param>
        /// <param name="ZoomFactor">The factor by which the dwg is scaled.</param>
        public virtual void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            this.zoomFactor *= ZoomFactor;

            location.X = ZoomFactor * (location.X - ZoomCenter.X) + ZoomCenter.X;
            location.Y = ZoomFactor * (location.Y - ZoomCenter.Y) + ZoomCenter.Y;
        }

        /// <summary>
        /// Implemented to move the load drawing according the X and Y offsetes.
        /// </summary>
        /// <param name="Xoffset">Offset in the X direction.</param>
        /// <param name="Yoffset">Offset in the Y direction.</param>
        public virtual void Pan(float Xoffset, float Yoffset)
        {
            location.X += Xoffset;
            location.Y += Yoffset;
        }

        /// <summary>
        /// Component of the eIDrawing interface and implemented to draw all the graphics components onto a graphics object.
        /// </summary>
        /// <param name="g">The graphics object onto which to draw the components.</param>
        public abstract void Draw(Graphics g);

        /// <summary>
        /// Removes all handlers added to external events before the deletion of the load.
        /// </summary>
        /// <param name="dwgForm">The form on which the load is drawn.</param>
        /// <param name="memberDWG">The member onto which the load is placed.</param>
        internal virtual void ReleaseHandlers(eModelForm dwgForm)
        {
            dwgForm.MouseClick -= new MouseEventHandler(dwgForm_MouseClick);
            dwgForm.KeyDown -= new KeyEventHandler(dwgForm_KeyDown);
            dwgForm.MouseDoubleClick -= new MouseEventHandler(dwgForm_MouseDoubleClick);
            dwgForm.MouseMove -= new MouseEventHandler(dwgForm_MouseMove);
            dwgForm.UnitChanged -= new eUnitChangedEventHandler(dwgForm_UnitChanged);

            dwgForm.Controls.Remove(textBox);

            this.layer.RectangularSelectionEnded -= new eRectangularSelectorEventHandler(layer_RectangularSelection);

            if (member != null)
                member.LocationChanged -= new eMemberGraphicsEventHandler(memberDWG_LocationChanged);
        }

        #endregion

        /// <summary>
        /// Occurs when a load starts moving or resizing.
        /// </summary>
        public event EventHandler MoveStart;

        /// <summary>
        /// Occurs when a load ends moving or resizing.
        /// </summary>
        public event EventHandler MoveEnd;

        /// <summary>
        /// Fires the MoveStart event.
        /// </summary>
        protected void OnMoveStart()
        {
            if (this.MoveStart != null)
            {
                this.MoveStart(this, new EventArgs());
            }
        }

        /// <summary>
        /// Fires the MoveEnd event.
        /// </summary>
        protected void OnMoveEnd()
        {
            if (this.MoveEnd != null)
            {
                this.MoveEnd(this, new EventArgs());
            }
        }

        /// <summary>
        /// Value of the 'Visible' property.
        /// </summary>
        protected bool visible;

        /// <summary>
        /// Gets or sets the value if the load is shown or not.
        /// </summary>       
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        }

        public abstract float MaxNegOffset { get; }

        /// <summary>
        /// Occurs when the magnitude of the load is changed.
        /// </summary>
        public event EventHandler MagnitudeChanged;

        /// <summary>
        /// Fires the 'MagnitudeChanged' event.
        /// </summary>
        protected void OnMagnitudeChanged()
        {
            if (MagnitudeChanged != null)
            {
                MagnitudeChanged(this, new EventArgs());
            }
        }

        public abstract float MaxPosOffset { get; }

        /// <summary>
        /// Gets the member bearing the load.
        /// </summary>
        public eGMember Member
        {
            get
            {
                return this.member;
            }
        }

        /// <summary>
        /// Holds the value of 'Member'.
        /// </summary>
        protected eGMember member;
        private eModelForm dwgForm;

        /// <summary>
        /// Gets the length unit of the system
        /// </summary>
        protected eLengthUnits lengthUnit
        {
            get
            {
                if (member != null)
                    return member.Beam.Document.LengthUnit;
                else
                    throw new Exception("A joint load has been assigned on member");
            }
        }

        /// <summary>
        /// Gets the force unit of the system
        /// </summary>
        protected eForceUints forceUnit
        {
            get
            {
                if (member != null)
                    return member.Beam.Document.ForceUnit;
                else
                    throw new Exception("A joint load has been assigned on member");
            }
        }

        internal void InitializeComponents(eModelForm dwgForm)
        {
            dwgForm.KeyPreview = true;
            dwgForm.MouseClick += new MouseEventHandler(dwgForm_MouseClick);
            dwgForm.KeyDown += new KeyEventHandler(dwgForm_KeyDown);
            dwgForm.MouseDoubleClick += new MouseEventHandler(dwgForm_MouseDoubleClick);
            dwgForm.MouseMove += new MouseEventHandler(dwgForm_MouseMove);
            dwgForm.UnitChanged += new eUnitChangedEventHandler(dwgForm_UnitChanged);

            this.layer.RectangularSelectionEnded += new eRectangularSelectorEventHandler(layer_RectangularSelection);

            this.textBox = new eNumericTextBox();
            dwgForm.Controls.Add(textBox);
            this.textBox.BackColor = System.Drawing.SystemColors.MenuText;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.ForeColor = System.Drawing.Color.White;
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(100, 20);
            this.textBox.TabIndex = 0;
            this.textBox.AutomaticResize = true;
            this.textBox.Visible = false;
        }
    }
}
