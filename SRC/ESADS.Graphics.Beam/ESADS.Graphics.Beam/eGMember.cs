using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ESADS.GUI.Controls;
using ESADS.GUI;
using ESADS.Mechanics.Analysis;
using ESADS.Mechanics.Analysis.Beam;
using ESADS.Mechanics.Design;
using ESADS.Mechanics.Design.Beam;
using ESADS.Code;
using ESADS.Code.EBCS_1995;
using ESADS.EGraphics;
using ESADS.EGraphics.Beam;

namespace ESADS.EGraphics.Beam
{
    /// <summary>
    /// Represents a continuous beam_Analysis member graphically.
    /// </summary>
    public class eGMember : eIDrawing
    {
        #region Fields
        /// <summary>
        /// Holds the value of the ChangeBy property.
        /// </summary>
        private eChangeBy changeBy;
        /// <summary>
        /// Holds the value of the 'Layer' property.
        /// </summary>
        private eLayer layer;
        /// <summary>
        /// The region in which the member responds to mouse actions.
        /// </summary>
        private Region region;
        /// <summary>
        /// The line that represents the member
        /// </summary>
        private eLine line;
        /// <summary>
        /// The form on which the member is drawn.
        /// </summary>
        private eModelForm dwgForm;
        /// <summary>
        /// The mechanics member object associated with the drawig.
        /// </summary>
        private eAMember member;
        /// <summary>
        /// Holds the value of the 'Member_Design' property.
        /// </summary>
        private eDMember member_Design;
        private List<eGLoad> loads;
        private eLayer loadLayer;
        /// <summary>
        /// Indicates whether the member is under a mouse pointer so that it is more isHighlighted than ohers.
        /// </summary>
        private bool isHighlighted;
        /// <summary>
        /// Holds the vallue of the 'IsSelected' property.
        /// </summary>
        private bool isSelected;
        /// <summary>
        /// Holds the value of the 'Size' property.
        /// </summary>
        private float size;
        /// <summary>
        /// The distance From the joint up to which the member starts to respond.
        /// </summary>
        private float jointClearance;
        private eNumericTextBox textBox;
        /// <summary>
        /// The continuous dimension to show the distribution and position of load
        /// </summary>
        private List<eDimension> loadDimensions;
        private eLayer dimLayer;
        private eLengthUnits lengthUnit
        {
            get
            {
                return beam.Document.LengthUnit;
            }
        }
        private float scaleFactor;

        /// <summary>
        /// Occurs when the member is going to be resized.
        /// </summary>
        public event eMemberGraphicsEventHandler Resize;
        private bool showLoadDimensions;
        private float extlnth;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new member drawing on a given form.
        /// </summary>
        /// <param name="start">The start coordinate of the member</param>
        /// <param name="end">The end point of the member</param>
        /// <param name="memberLayer">The layer on which to draw the member.</param>
        /// <param name="dwgForm">The form on which the member is going to be drawn.</param>
        public eGMember(eGBeam beam, eAMember member, PointF start, PointF end, eLayer memberLayer, eLayer loadLayer, eLayer dimLayer, eModelForm dwgForm)
        {
            this.beam = beam;
            this.member = member;
            this.member_Design = member.Member_Design;
            this.layer = memberLayer;
            this.isSelected = false;
            this.isHighlighted = false;
            this.loadLayer = loadLayer;
            this.dwgForm = dwgForm;
            this.jointClearance = 10;
            this.dimLayer = dimLayer;
            this.showSectionName = false;

            InitializeComponents(start, end);

            OnReloadDimension();

            this.label = memberLayer.AddText("", new PointF((start.X + end.X) / 2.0f, start.Y));
        }

        private void InitializeComponents(PointF start, PointF end)
        {
            this.loads = new List<eGLoad>();
            this.size = 1.0f;

            this.line = new eLine(start, end, layer);

            this.layer.RectangularSelectionEnded += new eRectangularSelectorEventHandler(layer_RectangularSelection);

            this.dimLayer.TurnedOn += new eLayerModifiedEventHandler(dimLayer_TurnedOn);

            CreateRegion();

            dwgForm.KeyPreview = true;
            dwgForm.KeyDown += new KeyEventHandler(dwgForm_KeyDown);
            dwgForm.MouseClick += new MouseEventHandler(dwgForm_MouseClick);
            dwgForm.MouseMove += new MouseEventHandler(dwgForm_MouseMove);
            dwgForm.MouseDoubleClick += new MouseEventHandler(dwgForm_MouseDoubleClick);
            dwgForm.UnitChanged += new eUnitChangedEventHandler(dwgForm_UnitChanged);

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

            scaleFactor = (float)((end.X - start.X) / member.Length);
            this.showLoadDimensions = false;
            this.extlnth = size * 15;
        }

        private void dimLayer_TurnedOn(eLayer sender, eLayerModifiedEventArgs e)
        {
            OnReloadDimension();
        }

        private void dwgForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (region.IsVisible(e.Location))
            {
                (sender as eModelForm).ObjFoundBelowClickPt = true;

                foreach (eGLoad l in loads)
                    l.Visible = false;
                dimLayer.LayerOn = false;

                textBox.Visible = true;
                textBox.DoubleValue = eUtility.Convert(member.Length, eUtility.SLU, this.lengthUnit);
                textBox.Focus();
                textBox.SelectAll();
                textBox.Font = membDimension.TextObjet.TextStyle;
                textBox.Location = new Point((int)(membDimension.TextObjet.Location.X - membDimension.TextObjet.Width / 2.0f), (int)(membDimension.TextObjet.Location.Y - membDimension.TextObjet.Height / 2.0f));
            }
        }

        private void ReloadDimensionsBasic()
        {
            if (loadDimensions != null && loadDimensions.Count > 0)
            {
                foreach (eDimension d in loadDimensions)
                    dimLayer.Remove(d);
            }
            double l;
            loadDimensions = new List<eDimension>();
            if (showLoadDimensions)
            {
                double[] intrvls = member.GetSectionsInterval();

                PointF s = new PointF(), e = new PointF();

                s.Y = e.Y = line.Location.Y;

                if (intrvls.Length > 2)
                {
                    for (int i = 1; i < intrvls.Length; i++)
                    {
                        l = Math.Round(eUtility.Convert(intrvls[i] - intrvls[i - 1], eUtility.SLU, this.lengthUnit), 3);
                        s.X = (float)(line.Location.X + intrvls[i - 1] * line.Length / member.Length);
                        e.X = (float)(line.Location.X + intrvls[i] * line.Length / member.Length);

                        eDimension d = dimLayer.AddDim(s, e, l.ToString(), eDimensionType.LinearHorizontal, eDimensionLinePosition.RightOrBottom, extlnth);
                        loadDimensions.Add(d);
                    }
                }
            }
            if (membDimension != null)
            {
                dimLayer.Remove(membDimension);
                membDimension = null;
            }

            l = Math.Round(eUtility.Convert(member.Length, eUtility.SLU, this.lengthUnit), 3);
            this.membDimension = dimLayer.AddDim(line.Location, line.End, l.ToString(), eDimensionType.LinearHorizontal, eDimensionLinePosition.RightOrBottom, extlnth);
            if (showLoadDimensions && loadDimensions.Count > 0)
                membDimension.ShortExtensionLength = (extlnth + membDimension.TextObjet.Height * 2.5f) / dimLayer.ZoomFactor;
        }

        private void dwgForm_UnitChanged(object sender, eUnitChangedEventArgs e)
        {
            //this.lengthUnit = e.LengthUnit;
            OnReloadDimension();
        }

        private void layer_RectangularSelection(object sender, eRectangularSelectionEventArgs e)
        {
            if (dwgForm.Locked)
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
                if (r.Equals(region, g))
                {
                    this.isSelected = true;
                    ChangeSelect();
                }
            }
        }

        private void ChangeSelect()
        {
            if (isSelected)
            {
                if (isHighlighted)
                {
                    line.LineType = new eLineType(eLineTypes.Dashed, 1 / 6.0f);
                }
                else
                {
                    line.LineType = new eLineType(eLineTypes.Dashed, 2.0f);
                }
            }
            else
            {
                line.LineType = new eLineType(eLineTypes.Continuous);
            }

        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the location of the member which is the start point.
        /// </summary>
        public PointF Location
        {
            get
            {
                return line.Location;
            }
            set
            {
                line.Location = value;
                GenerateDWGobjects();
                OnLocationChanged(new eMemberGraphicsEventArgs(value, line.End));
                OnReloadDimension();
            }
        }
        /// <summary>
        /// Gets the layer property of the member
        /// </summary>
        public eLayer Layer
        {
            get { return this.layer; }
        }

        /// <summary>
        /// Gets or sets the value if the name of the section of the member is shown or not.
        /// </summary>
        public bool ShowSectionName
        {
            get
            {
                return this.showSectionName;
            }
            set
            {
                this.showSectionName = value;
                if (value)
                {
                    this.label.Text = member_Design.Section.Name;
                    label.TextStyle = new eTextStyle(new Font("Arial", (float)(beam.Extent_V * 0.7)), eChangeBy.ByLayer);
                    label.Location = new PointF((Start.X + End.X) / 2.0f, Start.Y + label.Height / 2.0f);
                }
                else
                    this.label.Text = "";
            }
        }

        /// <summary>
        /// Gets the start point of the member.
        /// </summary>
        public PointF Start
        {
            get
            {
                return line.Location;
            }
        }

        /// <summary>
        /// Gets the end point of the member.
        /// </summary>
        public PointF End
        {
            get
            {
                return line.End;
            }
        }

        /// <summary>
        /// Gets and sets the color of the member
        /// </summary>
        public eColor Color
        {
            get
            {
                return line.Color;
            }
            set
            {
                line.Color = value;
                this.changeBy = eChangeBy.ByObject;
            }
        }

        /// <summary>
        /// Gets or sets the value if the member is currently selected by user.
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
            }
        }

        /// <summary>
        /// Gets or sets the size of the member dwg.
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
                GenerateDWGobjects();
                OnReloadDimension();
            }
        }

        /// <summary>
        /// Gets all the loads on the member.
        /// </summary>
        public List<eGLoad> Loads
        {
            get
            {
                return loads;
            }
        }

        /// <summary>
        /// Gets the analysis part of the member represented by the drawing
        /// </summary>
        public eAMember Member_Analysis
        {
            get
            {
                return this.member;
            }
        }

        /// <summary>
        /// Gets the design component of the member represented by the drawing.
        /// </summary>
        public eDMember Member_Design
        {
            get { return member_Design; }
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when a member wants to reload its dimension.
        /// </summary>
        public event EventHandler ReloadDimension;
        #endregion

        #region Methods
        /// <summary>
        /// Represents the method that will handle keyDown events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dwgForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.isSelected = false;
                line.LineType = new eLineType(eLineTypes.Continuous);

                if (textBox.Visible)
                {
                    foreach (eGLoad l in loads)
                        l.Visible = true;
                    textBox.Visible = false;
                    textBox.Parent.Focus();
                    dimLayer.LayerOn = true;
                }
                (sender as Form).Invalidate();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (textBox.Visible)
                {
                    double length = eUtility.Convert(textBox.DoubleValue, this.lengthUnit, eUtility.SLU);
                    PointF end = new PointF((float)(line.Location.X + length * scaleFactor), line.End.Y);
                    OnResize(new eMemberGraphicsEventArgs(line.Location, end, length));
                    member.Length = length;
                    beam.OnChanged();
                    line.End = end;

                    foreach (eGLoad l in loads)
                        l.Visible = true;
                    textBox.Visible = false;
                    textBox.Parent.Focus();
                    OnReloadDimension();
                    dimLayer.LayerOn = true;
                    dwgForm.Invalidate();
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if ((sender as eModelForm).EditingText)
                    return;

                int nums = loads.Count;
                for (int i = 0, c = 0; i < nums; i++)
                {
                    if (loads[i - c].IsSelected)
                    {
                        dynamic l = loads[i - c];
                        if (loads[i - c].GetType() == typeof(eGTrapezoidalLoad))
                        {
                            member.Loads.Remove(l.Load_Rect);
                            member.Loads.Remove(l.Load_Tri);
                        }
                        else
                            member.Loads.Remove(l.Load);
                        
                        loads[i - c].ReleaseHandlers(dwgForm);
                        loadLayer.Remove(loads[i - c]);
                        loads.RemoveAt(i - c);
                        c++;
                    }
                }
                OnReloadDimension();
                dwgForm.Invalidate();
            }
        }

        public void DeleteAllLoads()
        {
            int nums = loads.Count;
            while(loads.Count > 0)
            {
                dynamic l = loads[0];
                if (loads[0].GetType() == typeof(eGTrapezoidalLoad))
                {
                    member.Loads.Remove(l.Load_Rect);
                    member.Loads.Remove(l.Load_Tri);
                }
                else
                    member.Loads.Remove(l.Load);

                loads[0].ReleaseHandlers(dwgForm);
                loadLayer.Remove(loads[0]);
                loads.RemoveAt(0);
            }
            OnReloadDimension();
        }

        private void dwgForm_MouseMove(object sender, MouseEventArgs e)
        {
            if ((sender as eModelForm).Locked)
                return;
            isHighlighted = region.IsVisible(e.Location);

            if (isHighlighted)
            {
                line.LineWeight = new eLineWeight(3.0f);

                if (isSelected)
                    line.LineType = new eLineType(eLineTypes.Dashed, 1.0f / 6.0f);
            }
            else
            {
                line.LineWeight = new eLineWeight(1.0f);

                if (isSelected)
                    line.LineType = new eLineType(eLineTypes.Dashed, 0.5f);
            }

            (sender as Form).Invalidate(region);
        }
        /// <summary>
        /// Represents the method that handle mouse click event
        /// </summary>
        /// <param name="sender">the sender of the event </param>
        /// <param name="e"></param>
        private void dwgForm_MouseClick(object sender, MouseEventArgs e)
        {
            if ((sender as eModelForm).Locked)
                return;
            
            if (region.IsVisible(e.Location))
            {
                (sender as eModelForm).ObjFoundBelowClickPt = true;

                isSelected = !isSelected;

                ChangeSelect();

                (sender as Form).Invalidate(region);
            }
        }

        public void Zoom(System.Drawing.PointF ZoomCenter, float ZoomFactor)
        {
            //extlnth *= ZoomFactor;
            line.Zoom(ZoomCenter, ZoomFactor);
            scaleFactor *= ZoomFactor;
            CreateRegion();
        }

        public void Pan(float Xoffset, float Yoffset)
        {
            line.Pan(Xoffset, Yoffset);
            CreateRegion();
        }

        public void Draw(System.Drawing.Graphics g)
        {
            line.Draw(g);
        }

        private void GenerateDWGobjects()
        {
            CreateRegion();
        }

        private void CreateRegion()
        {
            this.region = new Region(new RectangleF(line.Location.X +jointClearance, line.Location.Y - line.LineWeight, line.Length - 2*jointClearance, 4.0f * line.LineWeight));
        }

        /// <summary>
        /// Occurs when the location of the load is changed.
        /// </summary>
        public event eMemberGraphicsEventHandler LocationChanged;
        /// <summary>
        /// The dimension to show the length of the member.
        /// </summary>
        private eDimension membDimension;

        /// <summary>
        /// Fires the LocationChanged Event.
        /// </summary>
        /// <param name="e">the event argument.</param>
        private void OnLocationChanged(eMemberGraphicsEventArgs e)
        {
            if (this.LocationChanged != null)
            {
                this.LocationChanged(this, e);
            }
        }

        /// <summary>
        /// Adds a concentrated force to the member.
        /// </summary>
        public void AddLoad(double Magnitude, double Start, eActionType ActionType, bool IsForce = true)
        {
            if (IsForce)
            {
                member.AddConcForce(Magnitude, Start, ActionType);
                loads.Add(loadLayer.AddConcentratedForceLoad(member.Loads[member.Loads.Count - 1] as eConcentratedForce, this, dwgForm));

                OnReloadDimension();

                loads[loads.Count - 1].MoveStart += new EventHandler(Load_MoveStart);
                loads[loads.Count - 1].MoveEnd += new EventHandler(Load_MoveEnd);
                loads[loads.Count - 1].MagnitudeChanged += new EventHandler(Load_MagnitudeChanged);
            }
            else
            {
                member.AddConcMoment(Magnitude, Start, ActionType);
                loads.Add(loadLayer.AddConcentratedMomentLoad(member.Loads[member.Loads.Count - 1] as eConcentratedMoment, this, dwgForm));

                OnReloadDimension();

                loads[loads.Count - 1].MoveStart += new EventHandler(Load_MoveStart);
                loads[loads.Count - 1].MoveEnd += new EventHandler(Load_MoveEnd);
                loads[loads.Count - 1].MagnitudeChanged += new EventHandler(Load_MagnitudeChanged);
            }
        }

        private void Load_MagnitudeChanged(object sender, EventArgs e)
        {
            OnReloadDimension();
            beam.OnChanged();
        }

        /// <summary>
        /// Adds a trapezoidal load to the member.
        /// </summary>
        public void AddLoad(double Tri_Mag, double Rect_Mag, eTriangularLoadOrientation Tri_Orientation, double Start, double End, eActionType ActionType)
        {
            member.AddRectLoad(Rect_Mag, Start, End, ActionType);
            member.AddTriangularLoad(Tri_Mag, Start, End, ActionType, Tri_Orientation);
            loads.Add(loadLayer.AddTrapezoidalLoad(member.Loads[member.Loads.Count - 1] as eTriangularLoad, member.Loads[member.Loads.Count - 2] as eRectangularLoad, this, dwgForm));

            OnReloadDimension();

            loads[loads.Count - 1].MoveStart += new EventHandler(Load_MoveStart);
            loads[loads.Count - 1].MoveEnd += new EventHandler(Load_MoveEnd);
            loads[loads.Count - 1].MagnitudeChanged += new EventHandler(Load_MagnitudeChanged);
        }

        /// <summary>
        /// Adds a triangular load to the member.
        /// </summary>
        public void AddLoad(double Mag, double Start, double End, eTriangularLoadOrientation Orientation, eActionType ActionType)
        {
            member.AddTriangularLoad(Mag, Start, End, ActionType, Orientation);
            loads.Add(loadLayer.AddTriangularLoad(member.Loads[member.Loads.Count - 1] as eTriangularLoad, this, dwgForm));

            OnReloadDimension();

            loads[loads.Count - 1].MoveStart += new EventHandler(Load_MoveStart);
            loads[loads.Count - 1].MoveEnd += new EventHandler(Load_MoveEnd);
            loads[loads.Count - 1].MagnitudeChanged += new EventHandler(Load_MagnitudeChanged);
        }

        /// <summary>
        /// Adds a rectangular load to the member.
        /// </summary>
        public void AddLoad(double Mag, double Start, double End, eActionType ActionType)
        {
            member.AddRectLoad(Mag, Start, End, ActionType);
            loads.Add(loadLayer.AddRectangularLoad(member.Loads[member.Loads.Count - 1] as eRectangularLoad, this, dwgForm));

            OnReloadDimension();

            loads[loads.Count - 1].MoveStart += new EventHandler(Load_MoveStart);
            loads[loads.Count - 1].MoveEnd += new EventHandler(Load_MoveEnd);
            loads[loads.Count - 1].MagnitudeChanged += new EventHandler(Load_MagnitudeChanged);
        }

        /// <summary>
        /// Removes event handlers from the drawing form and the layer when this object is deleted 
        /// </summary>
        internal void ReleaseHandlers()
        {
            this.layer.RectangularSelectionEnded -= new eRectangularSelectorEventHandler(layer_RectangularSelection);
            dwgForm.KeyDown -= new KeyEventHandler(dwgForm_KeyDown);
            dwgForm.MouseClick -= new MouseEventHandler(dwgForm_MouseClick);
            dwgForm.MouseMove -= new MouseEventHandler(dwgForm_MouseMove);

            dwgForm.Controls.Remove(this.textBox);
        }

        private void Load_MoveEnd(object sender, EventArgs e)
        {
            OnReloadDimension();
            dimLayer.LayerOn = true;

            foreach (eGLoad l in loads)
                l.Visible = true;
        }

        private void Load_MoveStart(object sender, EventArgs e)
        {
            dimLayer.LayerOn = false;
            foreach (eGLoad l in loads)
            {
                if (l != (sender as eGLoad))
                    l.Visible = false;
            }
        }

        internal void DeleteComponenets()
        {
            foreach (eDimension d in loadDimensions)
                dimLayer.Remove(d);

            loadDimensions.Clear();
            loadDimensions = null;
            dimLayer.Remove(membDimension);
            membDimension = null;

            while (loads.Count > 0)
            {
                loads[loads.Count - 1].MoveStart -= new EventHandler(Load_MoveStart);
                loads[loads.Count - 1].MoveEnd -= new EventHandler(Load_MoveEnd);
                loads[loads.Count - 1].ReleaseHandlers(dwgForm);
                
                loadLayer.Remove(loads[loads.Count - 1]);
                loads.RemoveAt(loads.Count - 1);                
            }

            layer.Remove(label);
        }

        internal void SelectAll()
        {
            foreach (eGLoad l in loads)
                l.IsSelected = true;

            this.isSelected = true;
            ChangeSelect();
        }

        /// <summary>
        /// Fires Resize event.
        /// </summary>
        private void OnResize(eMemberGraphicsEventArgs e)
        {
            if (Resize != null)
            {
                Resize(this, e);
            }

            if (showSectionName)
            {
                label.TextStyle = new eTextStyle(new Font("Arial", (float)(beam.Extent_V * 0.7)), eChangeBy.ByLayer);
                label.Location = new PointF((Start.X + End.X) / 2.0f, Start.Y + label.Height / 2.0f);
            }
        }

        /// <summary>
        /// Shows the dimensions of loads on the beam_Analysis.
        /// </summary>
        internal void ReloadDimensions(float maxExt)
        {
            if (!dimLayer.LayerOn)
                return;
            this.extlnth = maxExt * 1.1f;
            ReloadDimensionsBasic();
        }

        internal void ShowLoadDimensions(float maxExt)
        {
            if (!dimLayer.LayerOn)
                return;
            this.extlnth = maxExt * 1.1f;
            this.showLoadDimensions = true;
            ReloadDimensionsBasic();
        }

        /// <summary>
        /// Hides the load dimensions.
        /// </summary>
        internal void HideLoadDimensions()
        {
            this.showLoadDimensions = false;
            ReloadDimensionsBasic();
        }

        /// <summary>
        /// Gets the maximum vertical offset to the bottom from the member. The number is positive.
        /// </summary>
        internal float MaxNegOffset
        {
            get
            {
                float max = 0;
                foreach (eGLoad l in loads)
                {
                    if (l.MaxNegOffset > max)
                        max = l.MaxNegOffset;
                }
                return max;
            }
        }

        public void OnReloadDimension()
        {
            if (ReloadDimension != null)
            {
                ReloadDimension(this, new EventArgs());
            }
        }

        /// <summary>
        /// Gets the maximum negative offset from the beam_Analysis including the dimension line.
        /// </summary>
        public float MaxTotalNegOffset
        {
            get
            {
                return membDimension.OffsetFromOrigin + membDimension.ShortExtensionLength + membDimension.ExtBeyondDimLines;
            }
        }

        public float MaxPosOffset
        {
            get
            {
                float max = 0.0f;

                foreach (eGLoad l in loads)
                {
                    if (l.MaxPosOffset > max)
                        max = l.MaxPosOffset;
                }

                return max;
            }
        }

        #endregion

        /// <summary>
        /// Gets the beam bearing the member.
        /// </summary>
        public eGBeam Beam
        {
            get
            {
                return beam;
            }
        }

        /// <summary>
        /// Holds the value of 'Beam'.
        /// </summary>
        private eGBeam beam;
        private bool showSectionName;
        private eText label;
    }
}
