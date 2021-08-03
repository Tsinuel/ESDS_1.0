using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using ESADS.GUI;

namespace ESADS.EGraphics.Beam
{
    /// <summary>
    /// Represents a Grid lines.
    /// </summary>
    public class eGrid : eIDrawing
    {
        #region Feilds
        /// <summary>
        /// Holds a value for property 'Layer'.
        /// </summary>
        private eLayer layer;
        /// <summary>
        /// Holds a value for property 'location'
        /// </summary>
        private PointF location;
        /// <summary>
        /// Holds a value for property 'Color'.
        /// </summary>
        private eColor color;
        /// <summary>
        /// Holds a value for property 'BubbleColor'.
        /// </summary>
        private eColor bubbleColor;
        /// <summary>
        /// Holds a value for property 'BubbleColor'.
        /// </summary>
        private float bubbleSize;
        /// <summary>
        /// Holds a value for property 'ExtensionLineRatio'.
        /// </summary>
        private float extLR;
        /// <summary>
        /// Holds a value for property 'OffsetDistance'.
        /// </summary>
        private float offset;
        /// <summary>
        /// Holds a value for property 'GridType'.
        /// </summary>
        private eGridType gridType;
        /// <summary>
        /// Holds a value for property 'GridOrientation'.
        /// </summary>
        private eGridOrientation gridOrientation;
        /// <summary>
        /// Holds a value for property 'Text'. The value is extracted from the eText class instance.
        /// </summary>
        private eText text;
        /// <summary>
        /// Holds a value for property 'LineWeight'.
        /// </summary>
        private eLineWeight lineWeight;
        /// <summary>
        /// Contais all the drawing objects in this grid.
        /// </summary>
        private List<eIDrawing> dwgs = new List<eIDrawing>();
        /// <summary>
        /// Represents the region of the Grid lines.
        /// </summary>
        private Region gridReg;
        /// <summary>
        /// Represents the region of the grid text_left.
        /// </summary>
        private Region textReg;
        /// <summary>
        /// Holds a value indicating whether the grid is active or not.
        /// </summary>
        private bool isActive = false;
        /// <summary>
        /// Holds the color of the drawing Form.
        /// </summary>
        private Color dwgFormColor;
        /// <summary>
        /// Text box used to display the grid text_left during editing.
        /// </summary>
        private TextBox txtBox;
        /// <summary>
        /// Holds a value for property 'IsSelected'.
        /// </summary>
        private bool isSelected = false;

        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eGrid drawing from the given basic parameters
        /// </summary>
        /// <param name="location">The location of the grid point where this grid pass.</param>
        /// <param name="layer">The type of grid. Vertical or Horizontal.</param>
        /// <param name="text_left">The text_left to be displayed in the bubbles.</param>
        /// <param name="dwgForm"> The drawing form on which drawing is done.</param>
        /// <param name="gridType">The orientation of the grid.</param>
        /// <param name="gridOrientation">The layer on which this drawing found.</param>
        public eGrid(PointF location,eLayer layer,string text,eModelForm dwgForm, eGridType gridType = eGridType.Vertical, eGridOrientation gridOrientation = eGridOrientation.TopLeft)
        {
            this.location = location;
            this.gridType = gridType;
            this.gridOrientation = gridOrientation;
            this.lineWeight = layer.LineWeight;
            this.layer = layer;
            this.color = layer.Color;
            this.bubbleColor = layer.Color;
            this.bubbleSize = 10;
            if (text == "" || text == null)
                throw new Exception("Grid text cannot be empty!");
            this.text = new eText(text, location, layer);
            this.text.Color = new eColor(System.Drawing.Color.Green);
            this.offset = 100;
            this.extLR = 0.5f;
            this.dwgFormColor = dwgForm.BackColor;
            this.AddGridElements();
            this.txtBox = new TextBox();
            this.txtBox.BorderStyle = BorderStyle.FixedSingle;          
            this.txtBox.Visible = false;
            this.txtBox.Enabled = false;
            this.FormRegions();
            this.txtBox.KeyDown += new KeyEventHandler(txt_KeyDown);
            dwgForm.MouseClick += new MouseEventHandler(dwgForm_MouseClick);
            dwgForm.MouseMove += new MouseEventHandler(dwgForm_MouseMove);
            dwgForm.MouseDoubleClick += new MouseEventHandler(dwgForm_MouseDoubleClick);
            dwgForm.KeyDown += new KeyEventHandler(dwgForm_KeyDown);
            dwgForm.Controls.Add(txtBox);

            layer.RectangularSelectionEnded += new eRectangularSelectorEventHandler(layer_RectangularSelectionEnded);

            PointF loc = this.text.Location;
            this.text.Zoom(this.location, 1.0f / layer.ZoomFactor);
            this.text.Location = loc;
        }

        private void layer_RectangularSelectionEnded(object sender, eRectangularSelectionEventArgs e)
        {
            if ((this.txtBox.Parent as eModelForm).Locked)
                return;
            
            if (e.SuppressEvent || isSelected)
                return;

            Graphics g = (new Label()).CreateGraphics();
            Region r = e.Region.Clone();

            r.Intersect(gridReg);

            if (!e.IsPositive)
            {
                if (!r.IsEmpty(g) && !isSelected)
                {
                    Select(true);
                }
            }
            else
            {
                if (r.Equals(this.gridReg, g) && !isSelected)
                {
                    Select(true);
                }
            }
        }
        #endregion

        #region Porperties
        /// <summary>
        /// Gets or sets the value indicating whether this grid is selected or not.
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {               
               if (!isSelected)
               {
                   (dwgs[0] as eLine).LineType = new eLineType(eLineTypes.Dashed, eChangeBy.ByLayer);
                   (dwgs[1] as eCircle).LineType = new eLineType(eLineTypes.Dashed, eChangeBy.ByLayer);
                   isActive = false;
               }
               else
               {
                   (dwgs[0] as eLine).LineType = this.layer.LineType;
                   (dwgs[1] as eCircle).LineType = this.layer.LineType;
                   isActive = false;
               }
                isSelected = value;
            }
        }

        /// <summary>
        /// Gets or sets  the color of grid lines.
        /// </summary>
        public eColor Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                dwgs[0].Color = value;
            }
        }

        /// <summary>
        /// Gets or sets the location the joint on which this grid pass.
        /// </summary>
        public PointF Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                AddGridElements();
                FormRegions();
            }
        }

        /// <summary>
        /// Gets or set the layer on which this grid found.
        /// </summary>
        public eLayer Layer
        {
            get { return layer; }
        }

        /// <summary>
        /// Gets or sets the color of  the bubble.
        /// </summary>
        public eColor BubbleColor
        {
            get
            {
                return bubbleColor;
            }
            set
            {
                bubbleColor = value;
                this.dwgs[1].Color = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the grid objs_texts.
        /// </summary>
        public eColor TextColor
        {
            get
            {
                return text.Color;
            }
            set
            {
                text.Color = value;
                this.dwgs[3].Color = value;
            }
        }

        /// <summary>
        /// Gets or sets the text_left style of grid objs_texts.
        /// </summary>
        public eTextStyle TextStyle
        {
            get
            {
                return text.TextStyle ;
            }
            set
            {
                text.TextStyle.SetFont(value);
            }
        }

        /// <summary>
        /// Gets or sets the offset distance of grid bubbls from the joints.
        /// </summary>
        public float OffsetDistance
        {
            get
            {
                return offset;
            }
            set
            {
                offset = value;
            }
        }

        /// <summary>
        /// Gets or sets the ration of extension line to offset distance.
        /// </summary>
        public float ExtensionLineRatio
        {
            get
            {
                return extLR;
            }
            set
            {
                extLR = value;
            }
        }

        /// <summary>
        /// Gets or sets the radiu of bubles.
        /// </summary>
        public float BubbleSize
        {
            get
            {
                return bubbleSize;
            }
            set
            {
                bubbleSize = value;
            }
        }

        /// <summary>
        /// Gets or sets th type of the grid.
        /// </summary>
        public eGridType GridType
        {
            get
            {
                return gridType;
            }
            set
            {
                gridType = value;
            }
        }

        /// <summary>
        /// Gets or sets the Orientation of the grid.
        /// </summary>
        public eGridOrientation GridOrientation
        {
            get
            {
                return gridOrientation;
            }
            set
            {
                gridOrientation = value;
            }
        }

        /// <summary>
        /// Gets or sets the text_left of the grid.
        /// </summary>
        public string Text
        {
            get
            {
                return text.Text;
            }
            set
            {
                text.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the line weight of the grid lines.
        /// </summary>
        public eLineWeight LineWeight
        {
            get
            {
                return lineWeight;
            }
            set
            {
                lineWeight = value;
                lineWeight.ChangeBy = eChangeBy.ByObject;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Zooms all the drawing objects in this gridOrientation.
        /// </summary>
        public void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            foreach (eIDrawing d in dwgs)
                d.Zoom(ZoomCenter, ZoomFactor);
            this.location.X = ZoomFactor * (this.location.X - ZoomCenter.X) + ZoomCenter.X;
            this.location.Y = ZoomFactor * (this.location.Y - ZoomCenter.Y) + ZoomCenter.Y;
            bubbleSize *= ZoomFactor;
            offset *= ZoomFactor;
            if (txtBox.Focused)
                ResetTextBox();
            FormRegions();
        }

        /// <summary>
        /// Pans all the drawing objects in this gridOrientation.
        /// </summary>
        public void Pan(float Xoffset, float Yoffset)
        {
            foreach (eIDrawing d in dwgs)
                d.Pan(Xoffset, Yoffset);
            this.location.X += Xoffset;
            this.location.Y += Yoffset;
            FormRegions();
        }

        /// <summary>
        /// Draws all the drawing objects in this gridOrientation.
        /// </summary>
        public void Draw(System.Drawing.Graphics g)
        {
            foreach (eIDrawing d in dwgs)
                d.Draw(g);
            if (isActive)
            {
                g.FillRegion(new SolidBrush(color), gridReg);
                g.DrawString(this.Text, this.TextStyle, new HatchBrush(HatchStyle.LightDownwardDiagonal, dwgFormColor, this.text.Color), GetTextLoc());
            }
            if (isSelected)
            {
                g.DrawString(this.Text, this.TextStyle, new HatchBrush(HatchStyle.LightDownwardDiagonal, dwgFormColor, this.text.Color), GetTextLoc());
            }
        }

        /// <summary>
        /// Adds all elements ( drawing objects) to form the grid.
        /// </summary>
        public void AddGridElements()
        {
            dwgs = new List<eIDrawing>();

            switch (this.gridOrientation)
            {
                case eGridOrientation.TopLeft:
                    if (gridType == eGridType.Vertical)
                    {
                        dwgs.Add(new eLine(new PointF(location.X, location.Y - offset), new PointF(location.X, location.Y - (1 - extLR) * offset), this.layer));
                        dwgs.Add(new eCircle(new PointF(location.X, location.Y - offset - bubbleSize), bubbleSize, this.layer));
                        text.Location = new PointF(location.X, location.Y - offset - bubbleSize);
                        dwgs.Add(text);
                    }
                    else
                    {
                        dwgs.Add(new eLine(new PointF(location.X - offset, location.Y), new PointF(location.X- (1 - extLR) * offset, location.Y ), this.layer));
                        dwgs.Add(new eCircle(new PointF(location.X- offset - bubbleSize, location.Y ), bubbleSize, this.layer));
                        text.Location = new PointF(location.X- offset - bubbleSize, location.Y );
                        dwgs.Add(text);
                    }
                    break;
                case eGridOrientation.BottomRight:
                    if (gridType == eGridType.Vertical)
                    {
                        dwgs.Add(new eLine(new PointF(location.X, location.Y + offset), new PointF(location.X, location.Y + (1 - extLR) * offset), this.layer));
                        dwgs.Add(new eCircle(new PointF(location.X, location.Y + offset + bubbleSize), bubbleSize, this.layer));
                        text.Location = new PointF(location.X, location.Y + offset + bubbleSize);
                        dwgs.Add(text);
                    }
                    else
                    {
                        dwgs.Add(new eLine(new PointF(location.X +offset, location.Y), new PointF(location.X + (1 - extLR) * offset, location.Y), this.layer));
                        dwgs.Add(new eCircle(new PointF(location.X + offset + bubbleSize, location.Y), bubbleSize, this.layer));
                        text.Location = new PointF(location.X + offset + bubbleSize, location.Y);
                        dwgs.Add(text);
                    }
                    break;
            }
        }

        /// <summary>
        /// Handls the double click event of the drawing Form.
        /// </summary>
        /// <param name="sender">The sender of this event.</param>
        /// <param name="e">Event argument containing all the necessary information related to the event.</param>
        void dwgForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((sender as eModelForm).Locked)
            {
                MessageBox.Show("The model cannot be edited while it is locked. Unlock it to continue.", "Model locked", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (gridReg.IsVisible(e.Location))
            {
                MessageBox.Show("Showing Grid data grid view.");
            }
            if (textReg.IsVisible(e.Location))
            {
                ResetTextBox();
                txtBox.Enabled = true;
                txtBox.Visible = true;
                txtBox.Text = text.Text;
                txtBox.Focus();
                return;
            }
            else
            {
                txtBox.Visible = false;
                txtBox.Enabled = false;
                txtBox.FindForm().Focus();
            }
        }

        /// <summary>
        /// Handls the MouseMove event of the drawing Form.
        /// </summary>
        /// <param name="sender">The sender of this event.</param>
        /// <param name="e">Event argument containing all the necessary information related to the event.</param>
        void dwgForm_MouseMove(object sender, MouseEventArgs e)
        {
            if ((sender as eModelForm).Locked)
                return;
            if (isSelected)
                return;
            Graphics g = ((Form)sender).CreateGraphics();
            if (textReg.IsVisible(e.Location))
            {
                g.DrawString(this.Text, this.TextStyle, new HatchBrush(HatchStyle.LightDownwardDiagonal, dwgFormColor, this.text.Color),GetTextLoc());
                isActive = true;
                return;
            }
            if (gridReg.IsVisible(e.Location))
            {
                g.FillRegion(new SolidBrush(this.color), gridReg);
                isActive = true;
                return;
            }
            else if (isActive)
            {
                isActive = false;
                ((Form)sender).Invalidate(gridReg);
                ((Form)sender).Invalidate(new Rectangle(GetTextLoc(),new Size((int)TextStyle.GetSizeOf(Text).Width,(int)TextStyle.GetSizeOf(Text).Height)));
            }
        }

        /// <summary>
        /// Hands the KeyDown event of the text_left box which allow editing the grid text_left.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event argument containing all the necessary information related to the event.</param>
        void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtBox.Enabled)
                {
                    if (txtBox.Text == "" || txtBox.Text == null)
                    {
                        MessageBox.Show("Grid text_left cannot be empty!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        this.text.Text = (sender as TextBox).Text;
                        this.txtBox.Visible = false;                      
                        this.txtBox.FindForm().Invalidate();                       
                        this.FormRegions();
                    }
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
               this.txtBox.Visible = false;
               this.txtBox.Enabled = false;
               this.txtBox.FindForm().Focus();
               this.txtBox.FindForm().Invalidate();
            }
        }

        /// <summary>
        /// Handles the mouse click event handler of the Form containing this drawing.
        /// </summary>
        /// <param name="sender">Object sending this event.</param>
        /// <param name="e">Event argument containing information realated to the event.</param>
        void dwgForm_MouseClick(object sender, MouseEventArgs e)
        {
            if ((sender as eModelForm).Locked)
                return;
           
            if (gridReg.IsVisible(e.Location) && (e.Button == MouseButtons.Left))
            {     
                (sender as eModelForm).ObjFoundBelowClickPt = true;
                Select();
                (sender as Form).Invalidate(gridReg);             
            }
        }

        /// <summary>
        /// Forms a region for the grid line and the grid text_left.
        /// </summary>
        private void FormRegions()
        {
            gridReg = ((eLine)dwgs[0]).GetRegion(((eLine)dwgs[0]).LineWeight + 2);
            gridReg.Union(((eCircle)dwgs[1]).GetRegion(((eCircle)dwgs[1]).LineWeight + 2));
            textReg = new Region(new RectangleF(text.Location.X - 0.5f * text.TextStyle.GetSizeOf(text.Text).Width / 2, text.Location.Y - 0.5f * text.TextStyle.GetSizeOf(text.Text).Height / 2,
                                  0.5f * text.TextStyle.GetSizeOf(text.Text).Width, 0.5f * text.TextStyle.GetSizeOf(text.Text).Height));
        }

        /// <summary>
        /// Handles the key down event handler of the Form containing this drawing.
        /// </summary>
        /// <param name="sender">Object sending this event.</param>
        /// <param name="e">Event argument containing information realated to the event.</param>
        void dwgForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (isSelected)
            {
                (dwgs[0] as eLine).LineType = this.layer.LineType;
                (dwgs[1] as eCircle).LineType = this.layer.LineType;
                isSelected = isActive = false;
                (sender as Form).Invalidate(gridReg);
                (sender as Form).Invalidate(new Rectangle(GetTextLoc(), new Size((int)text.Height, (int)text.Width)));
                (sender as Form).Focus();
            }
        }

        /// <summary>
        /// Resets the location and the size of the text_left box used to edit the grid tex.
        /// </summary>
        private void ResetTextBox()
        {
            txtBox.Location = GetTextLoc();
            txtBox.Size = new Size((int)this.TextStyle.GetSizeOf(this.Text).Width, (int)(this.TextStyle.GetSizeOf(this.Text).Height));
            txtBox.BackColor = dwgFormColor;
            txtBox.ForeColor = text.Color;
            txtBox.Font = text.TextStyle;
             
        }

        /// <summary>
        /// Gets the location of the grid text_left.
        /// </summary>
        /// <returns></returns>
        private Point GetTextLoc()
        {
            return new Point((int)(text.Location.X - this.TextStyle.GetSizeOf(this.Text).Width / 2), (int)(text.Location.Y - this.TextStyle.GetSizeOf(this.Text).Height / 2));
        }

        /// <summary>
        /// Releases event handlers when the object is deleted.
        /// </summary>
        public void ReleaseHandlers(eModelForm dwgForm)
        {
            this.txtBox.KeyDown -= new KeyEventHandler(txt_KeyDown);
            dwgForm.MouseClick -= new MouseEventHandler(dwgForm_MouseClick);
            dwgForm.MouseMove -= new MouseEventHandler(dwgForm_MouseMove);
            dwgForm.MouseDoubleClick -= new MouseEventHandler(dwgForm_MouseDoubleClick);
            dwgForm.KeyDown -= new KeyEventHandler(dwgForm_KeyDown);
        }

        /// <summary>
        /// Selects this grid.
        /// </summary>
        public void Select()
        {
            isSelected = !isSelected;
            if (isSelected)
            {
                (dwgs[0] as eLine).LineType = new eLineType(eLineTypes.Dashed, eChangeBy.ByLayer);
                (dwgs[0] as eLine).ZoomDashPattern = false;
                (dwgs[1] as eCircle).LineType = new eLineType(eLineTypes.Dashed, eChangeBy.ByLayer);
                (dwgs[1] as eCircle).ZoomDashPatern = false;
                isActive = false;
            }
            else
            {
                (dwgs[0] as eLine).LineType = this.layer.LineType;
                (dwgs[0] as eLine).ZoomDashPattern = true;
                (dwgs[1] as eCircle).LineType = this.layer.LineType;
                (dwgs[1] as eCircle).ZoomDashPatern = true;
                isActive = false;
            }
        }

        internal void Select(bool val)
        {
            isSelected = val;
            if (isSelected)
            {
                (dwgs[0] as eLine).LineType = new eLineType(eLineTypes.Dashed, eChangeBy.ByLayer);
                (dwgs[0] as eLine).ZoomDashPattern = false;
                (dwgs[1] as eCircle).LineType = new eLineType(eLineTypes.Dashed, eChangeBy.ByLayer);
                (dwgs[1] as eCircle).ZoomDashPatern = false;
                isActive = false;
            }
            else
            {
                (dwgs[0] as eLine).LineType = this.layer.LineType;
                (dwgs[0] as eLine).ZoomDashPattern = true;
                (dwgs[1] as eCircle).LineType = this.layer.LineType;
                (dwgs[1] as eCircle).ZoomDashPatern = true;
                isActive = false;
            }
        }
        /// <summary>
        /// Returns the maximum offset of the grid from the joint.
        /// </summary>
        /// <returns></returns>
        public float GetMaxOffset()
        {
            return offset + 2 * bubbleSize;
        }
        
        #endregion
    }
}
