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
    /// Represents the Graphic graphic representation a joint.
    /// </summary>
    public class eGJoint : eIDrawing
    {
        #region Feilds
        /// <summary>
        /// Holds a value for property 'Color'.
        /// </summary>
        private eColor color;
        /// <summary>
        ///  Holds a value for property 'LineType'.
        /// </summary>
        private eLineType lineType;
        /// <summary>
        ///  Holds a value for property 'LineWeight'.
        /// </summary>
        private eLineWeight lineWeight;
        /// <summary>
        /// Holds a value for public property 'location'.
        /// </summary>
        private PointF location;
        /// <summary>
        /// Holds a value for public property 'Layer'.
        /// </summary>
        private eLayer layer;
        /// <summary>
        ///  Holds a value for public property 'Joint'.
        /// </summary>
        private eJoint joint;
        /// <summary>
        ///  Holds a value for public property 'Size'.
        /// </summary>
        private float size;
        /// <summary>
        ///  Holds a value for public property 'IsSelected'.
        /// </summary>
        private bool isSelected;
        /// <summary>
        ///  Holds a value for public property 'Orientation'.
        /// </summary>
        private eJointOrientation orientation;
        /// <summary>
        /// Holds the region srounded by the joint bounderies.
        /// </summary>
        private Region region;
        /// <summary>
        /// Contains all the graphics objects used to draw this joint.
        /// </summary>
        private List<eIDrawing> dwgs;
        /// <summary>
        /// Holds the color of the drawing background.
        /// </summary>
        private Color dwgBackColor;
        /// <summary>
        /// Used to displaye information about joint.
        /// </summary>
        private Label lable;
        /// <summary>
        /// Holds a value for public property 'JointSnapRadius'.
        /// </summary>
        private float jointSnapRadius;
        /// <summary>
        /// Holds a value for property 'GridPointRadius'.
        /// </summary>
        private float gridPointRadius;
        /// <summary>
        /// Holds a value for public property 'GridPointColor'.
        /// </summary>
        private Color gridPointColor;
        /// <summary>
        /// Holds a value for public property 'SelectionCrossColor'.
        /// </summary>
        public Color selectionCrossColor;
        /// <summary>
        /// Holds the value of the 'Grid' property.
        /// </summary>
        private eGrid grid;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the location of the Joint.
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
                if (grid != null)
                    grid.Location = value;
                foreach (var load in loads)
                    load.Location = value;
                GenerateDwgObjects();
                FormRegion();
            }
        }

        /// <summary>
        /// Gets the layer of the Joint on which it is drawn.
        /// </summary>
        public eLayer Layer
        {
            get { return layer; }
        }

        /// <summary>
        /// Gets or sets the joint represented by this graphic object.
        /// </summary>
        public eJoint Joint
        {
            get
            {
                return joint;
            }
            set { joint = value; }
        }

        /// <summary>
        /// Gets or sets the size of the Joint
        /// </summary>
        public float Size
        {
            get { return size; }
            set
            {
                size = value;
                dwgs = new List<eIDrawing>();
                GenerateDwgObjects();
            }
        }

        /// <summary>
        /// Gets or sets the value indication whether the joint is selected or not.
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        /// <summary>
        /// Gets or sets the Joint Orientation this value indicates which side of the joint is connected to the member.
        /// </summary>
        public eJointOrientation Orientation
        {
            get { return orientation; }
            set { 
                orientation = value;
                if (joint.Type == eJointType.Fixed || joint.Type == eJointType.VerticalRoller)
                {
                    dwgs = new List<eIDrawing>();
                    GenerateDwgObjects();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of display for the joint.
        /// </summary>
        public eColor Color
        {
            get { return color; }
            set
            {
                color = value;
                foreach (eIDrawing d in this.dwgs)
                    d.Color = value;
            }
        }

        /// <summary>
        /// Gets or sets the line type of the joint.
        /// </summary>
        public eLineType LineType
        {
            get { return lineType; }
            set
            {
                lineType = value;
                foreach (eIDrawing d in this.dwgs)
                {
                    if (d.GetType() == typeof(eCircle))
                        (d as eCircle).LineType = value;
                    if (d.GetType() == typeof(eLine))
                        (d as eLine).LineType = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the line weight used when drawing the joint.
        /// </summary>
        public eLineWeight LineWeight
        {
            get { return lineWeight; }
            set { 
                lineWeight = value;
                foreach (eIDrawing d in this.dwgs)
                {
                    if (d.GetType() == typeof(eCircle))
                        (d as eCircle).LineWeight = value;
                    if (d.GetType() == typeof(eLine))
                        (d as eLine).LineWeight = value;
                }
                
            }
        }

        /// <summary>
        /// Gets or sets the joint snap radius
        /// </summary>
        public float JointSnapRadius
        {
            get { return jointSnapRadius; }
            set { jointSnapRadius = value; }
        }

        /// <summary>
        /// Gets or sets the radius of the grid piont which will glow when the mouse aproaches.
        /// </summary>
        public float GridPointRadius
        {
            get { return gridPointRadius; }
            set { gridPointRadius = value; ; }
        }

        /// <summary>
        /// Gets or sets the color of the grid piont which will glow when the mouse aproaches.
        /// </summary>
        public Color GridPointColor
        {
            get { return gridPointColor; }
            set { gridPointColor = value; }
        }

        /// <summary>
        /// Gets the region on which this joint responds.
        /// </summary>
        public Region Region
        {
            get { return region; }
        }

        /// <summary>
        /// Gets or sets the selection cross color of the joint which is drawn when the joint is selected.
        /// </summary>
        public Color SelectionCrossColor
        {
            get { return selectionCrossColor; }
            set { selectionCrossColor = value; }
        }

        /// <summary>
        /// Gets or sets the grid drawn at the joint.
        /// </summary>
        public eGrid Grid
        {
            get
            {
                return grid;
            }
            set
            {
                grid = value;
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eGJoint class from the basic parameters for all type of joints.
        /// </summary>
        /// <param name="location">location of the joint inidicating the point where the joint connect to the member.</param>
        /// <param name="joint">The joint related to this joint drawing.</param>
        /// <param name="layer">The layer on which the joint is drawn.</param>
        /// <param name="orientation"> The alignment of the joint.</param>
        /// <param name="dwgForm">The form on which the drawing is done.</param>
        public eGJoint(eGBeam beam, PointF location, eJoint joint, eLayer layer,eLayer loadLayer, eLayer gridLayer, string gridName, eModelForm dwgForm, eJointOrientation orientation)
        {
            this.beam = beam;
            this.beam.Changed += new EventHandler(beam_Changed);
            this.location = location;
            this.joint = joint;
            this.layer = layer;
            this.loads = new List<eGLoad>();
            this.loadLayer = loadLayer;
            this.lineType = layer.LineType;
            this.color = layer.Color;
            this.lineWeight = layer.LineWeight;
            this.orientation = orientation;
            this.size = 40;
            this.jointSnapRadius = 10;
            this.gridPointRadius = 3;
            this.dwgBackColor = dwgForm.BackColor;
            this.gridPointColor = System.Drawing.Color.Red;
            this.selectionCrossColor = System.Drawing.Color.Gold;
            this.dwgs = new List<eIDrawing>();
            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified);
            dwgForm.MouseClick += new MouseEventHandler(dwgForm_MouseClick);
            dwgForm.MouseMove += new MouseEventHandler(dwgForm_MouseMove);
            dwgForm.KeyDown += new KeyEventHandler(dwgForm_KeyDown);
            lable = new Label();
            lable.BackColor = System.Drawing.Color.Beige;
            lable.ForeColor = System.Drawing.Color.Black;
            lable.Visible = false;
            FormRegion();
            GenerateDwgObjects();

            this.grid = gridLayer.AddGrid(location, gridName, dwgForm);

            this.joint.TypeChanged += new eJointChangedEventHandler(joint_TypeChanged);
        }

        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eGJoint class from the basic parameters for common types of joints like Roller,Pin,Hindge.
        /// </summary>
        /// <param name="location">location of the joint inidicating the point where the joint connect to the member.</param>
        /// <param name="joint">The joint related to this joint drawing.</param>
        /// <param name="layer">The layer on which the joint is drawn.</param>
        /// <param name="dwgForm">The form on which the drawing is done.</param>
        public eGJoint(eGBeam beam, PointF location, eJoint joint, eLayer layer, eLayer loadLayer, eLayer gridLayer, string gridName,eModelForm dwgForm)
        {
            this.beam = beam;
            this.beam.Changed += new EventHandler(beam_Changed);
            this.location = location;
            this.joint = joint;
            this.layer = layer;
            this.lineType = layer.LineType;
            this.loads = new List<eGLoad>();
            this.loadLayer = loadLayer;
            this.color = layer.Color;
            this.lineWeight = layer.LineWeight;
            if (joint.Type == eJointType.Fixed || joint.Type == eJointType.VerticalRoller)
                this.orientation = eJointOrientation.RightSideConnected;
            else
                this.orientation = eJointOrientation.Default;
            this.size = 40;
            this.jointSnapRadius = 10;
            this.gridPointRadius = 3;
            this.dwgBackColor = dwgForm.BackColor;
            this.gridPointColor = System.Drawing.Color.Red;
            this.selectionCrossColor = System.Drawing.Color.Gold;
            this.dwgs = new List<eIDrawing>();
            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified);
            dwgForm.MouseClick += new MouseEventHandler(dwgForm_MouseClick);
            dwgForm.MouseMove += new MouseEventHandler(dwgForm_MouseMove);
            dwgForm.KeyDown += new KeyEventHandler(dwgForm_KeyDown);
            this.layer.RectangularSelectionEnded += new eRectangularSelectorEventHandler(layer_RectangularSelection);
            lable = new Label();
            lable.BackColor = System.Drawing.Color.Beige;
            lable.ForeColor = System.Drawing.Color.Black;
            lable.Visible = false;
            dwgForm.Controls.Add(lable);
            GenerateDwgObjects();
            FormRegion();
            this.grid = gridLayer.AddGrid(location, gridName, dwgForm);
        }

        private void beam_Changed(object sender, EventArgs e)
        {
            GenerateDwgObjects();
        }

        private void joint_TypeChanged(object sender, EventArgs e)
        {
            GenerateDwgObjects();
            FormRegion();
        }

        private void layer_RectangularSelection(object sender, eRectangularSelectionEventArgs e)
        {
            if ((lable.Parent as eModelForm).Locked)
                return;
            
            Graphics g = (new Label()).CreateGraphics();
            Region r = e.Region.Clone();
            r.Intersect(region);

            if (!e.IsPositive)
            {
                if (!r.IsEmpty(g))
                {
                    this.isSelected = true;
                    
                }
            }
            else
            {
                if (r.Equals(region, g))
                {
                    this.isSelected = true;
                }
            }

        }          
        #endregion

        #region Methods
        /// <summary>
        /// Zooms the this drawing object.
        /// </summary>
        /// <param name="ZoomCenter">The zoom center from which zooming is done.</param>
        /// <param name="ZoomFactor">The factor by which the drawin is enlarged.</param>
        public void Zoom(System.Drawing.PointF ZoomCenter, float ZoomFactor)
        {
            for (int i = 0; i < dwgs.Count; i++)
            {
                dwgs[i].Zoom(ZoomCenter, ZoomFactor);
            }
            this.location.X = (this.location.X - ZoomCenter.X) * ZoomFactor + ZoomCenter.X;
            this.location.Y = (this.location.Y - ZoomCenter.Y) * ZoomFactor + ZoomCenter.Y;         
            FormRegion();
        }

        /// <summary>
        /// Pans this drawing object.
        /// </summary>
        /// <param name="Xoffset">The X-Offset by which the drawing is going to be moved.</param>
        /// <param name="Yoffset">The Y-Offset by which the drawing is going to be moved.</param>
        public void Pan(float Xoffset, float Yoffset)
        {
            for (int i = 0; i < dwgs.Count; i++)
            {
                dwgs[i].Pan(Xoffset, Yoffset);
            }
            this.location.X += Xoffset;
            this.location.Y += Yoffset;
            FormRegion();
        }

        /// <summary>
        /// Draws this drawing object.
        /// </summary>
        /// <param name="g">The graphic object on which this drawing is done</param>
        public void Draw(System.Drawing.Graphics g)
        {
            for (int i = 0; i < dwgs.Count; i++)
            {
                dwgs[i].Draw(g);
            }
            if (this.isSelected)
            {
                Pen p = new Pen(selectionCrossColor,2);
                p.DashStyle = DashStyle.Dash;
                g.DrawLine(p, new PointF(location.X - jointSnapRadius, location.Y - jointSnapRadius), new PointF(location.X + jointSnapRadius, location.Y + jointSnapRadius));
                g.DrawLine(p, new PointF(location.X - jointSnapRadius, location.Y + jointSnapRadius), new PointF(location.X + jointSnapRadius, location.Y - jointSnapRadius));
            }
        }

        /// <summary>
        /// Adds all drawing objects for the joint.
        /// </summary>
        private void GenerateDwgObjects()
        {
            this.size = (float)(beam.Extent_V * layer.ZoomFactor);
            float factr = 1.0f;
            if (dwgs != null)
                factr = layer.ZoomFactor;
            dwgs = new List<eIDrawing>();
            switch (joint.Type)
            {
                case eJointType.Continious:
                    break;
                case eJointType.Fixed:
                    AddFixed();
                    break;
                case eJointType.Hinge:
                    AddHinge();
                    break;
                case eJointType.Pin:
                    AddPin();
                    break;
                case eJointType.Roller:
                    AddRoller();
                    break;
                case eJointType.VerticalGuidedRoller:
                    AddVerticalGuidedRoller();
                    break;
                case eJointType.VerticalRoller:
                    AddVerticalRoller();
                    break;
                default:
                    break;
            }

            //foreach (eIDrawing dwg in dwgs)
            //    dwg.Zoom(location, factr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void dwgForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (isSelected && (e.KeyCode == Keys.Escape))
            {
                isSelected = false;
                (sender as Form).Invalidate(new Rectangle((int)(location.X - jointSnapRadius * 1.2), (int)(location.Y - jointSnapRadius * 1.2), (int)(jointSnapRadius * 2.4), (int)(jointSnapRadius * 2.4)));
                (sender as Form).Focus();
            }
        }

        /// <summary>
       /// Handls the mouse move event of the Form.
       /// </summary>
        /// <param name="sender">Object sending this event.</param>
        /// <param name="e">Event argument containing information realated to the event.</param>
        private  void dwgForm_MouseMove(object sender, MouseEventArgs e)
        {
            if ((sender as eModelForm).Locked)
                return;
            Graphics g = ((Form)sender).CreateGraphics();
            if (region.IsVisible(e.Location))
            {

                ((Form)sender).Controls.Add(lable);
                lable.Visible = true;
                lable.AutoSize = true;
                lable.Location = new Point((int)location.X + 10, (int)location.Y - (10 + lable.Size.Height));
                lable.Text = "Joint";
                g.FillEllipse(new SolidBrush(gridPointColor), this.location.X - gridPointRadius, this.location.Y - gridPointRadius, 2 * gridPointRadius, 2 * gridPointRadius);
            }
            else
            {
                lable.Visible = false;
               ((Form)sender).Invalidate(new Rectangle((int)(this.location.X - gridPointRadius), (int)(this.location.Y - gridPointRadius), (int)(2 * gridPointRadius+1), (int)(2 * gridPointRadius+1)));
            }
        }

        /// <summary>
        /// Handles the mouse click event of the From.
        /// </summary>
        /// <param name="sender">Object sending this event.</param>
        /// <param name="e">Event argument containing information realated to the event.</param>
        private void dwgForm_MouseClick(object sender, MouseEventArgs e)
        {
            if ((sender as eModelForm).Locked)
                return;
            
            if (region.IsVisible(e.Location) && (e.Button == MouseButtons.Left))
            {
                (sender as eModelForm).ObjFoundBelowClickPt = true;
                isSelected = !isSelected;
                (sender as Form).Invalidate(new Rectangle((int)(this.location.X - jointSnapRadius), (int)(this.location.Y - jointSnapRadius), (int)(2 * jointSnapRadius + 1), (int)(2 * jointSnapRadius + 1)));
            }
        }

        /// <summary>
        /// Adds pin joint type.
        /// </summary>
        private void AddPin()
        {
            //Adds lines for the pin.
            dwgs.Add(new eLine(location, new PointF(location.X + 0.425f * size, location.Y + 0.5f * size), this.layer));
            dwgs.Add(new eLine(location, new PointF(location.X - 0.425f * size, location.Y + 0.5f * size), this.layer));
            dwgs.Add(new eLine(new PointF(location.X - size / 2, location.Y + size / 2), new PointF(location.X + size / 2, location.Y + size / 2), this.layer));
            
            //Adds hatchs for the pin.
            for (int i = 0; i <= 10; i++)
            {
                dwgs.Add(new eLine(new PointF(location.X - size / 2 + 0.1f * size * (i-1), location.Y + size / 2 + size * 0.1f), new PointF(location.X - size / 2 + 0.1f * size * i, location.Y + size / 2), this.layer));
            }
        }

        /// <summary>
        /// Adds fixed Joint Type.
        /// </summary>
        private void AddFixed()
        {
            dwgs.Add(new eLine(new PointF(location.X, location.Y - size / 2), new PointF(location.X, location.Y + size / 2), this.layer));
            switch (orientation)
            {
                case eJointOrientation.RightSideConnected:
                    for (int i = 0; i <= 10; i++)
                    {
                        dwgs.Add(new eLine(new PointF(location.X, location.Y - size / 2 + 0.1f * size * i), 
                                 new PointF(location.X - 0.2f * size, location.Y - size / 2 + 0.1f * size * (i+1)), this.layer));
                    }
                    return;
                case eJointOrientation.LeftSideConnected:
                    for (int i = 0; i <= 10; i++)
                    {
                        dwgs.Add(new eLine(new PointF(location.X, location.Y - size / 2 + 0.1f * size * i), 
                                 new PointF(location.X + 0.2f * size, location.Y - size / 2 + 0.1f * size * (i + 1)), this.layer));
                    }
                    return;
            }
        }

        /// <summary>
        /// Adds vertical roller Joint Type.
        /// </summary>
        private void AddVerticalRoller()
        {
            AddVerticalGuidedRoller();
            switch (orientation)
            {
                case eJointOrientation.RightSideConnected:
                    for (int i = 0; i <= 10; i++)
                    {
                        dwgs.Add(new eLine(new PointF(location.X-0.05f*size, location.Y - size / 2 + 0.1f * size * i),
                                 new PointF(location.X - 0.15f * size, location.Y - size / 2 + 0.1f * size * (i + 1)), this.layer));
                    }
                    for (int i = 0; i < dwgs.Count; i++)
                    {

                        dwgs[i].Pan(-0.05f * size, 0);
                    }
                    return;
                case eJointOrientation.LeftSideConnected:
                    for (int i = 0; i <= 10; i++)
                    {
                        dwgs.Add(new eLine(new PointF(location.X + 0.05f * size, location.Y - size / 2 + 0.1f * size * i),
                                 new PointF(location.X + 0.15f * size, location.Y - size / 2 + 0.1f * size * (i + 1)), this.layer));
                    }
                    for (int i = 0; i < dwgs.Count; i++)
                    {

                        dwgs[i].Pan(0.05f * size, 0);
                    }
                    return;
            }
        }

        /// <summary>
        /// Adds Hinge Joint Type.
        /// </summary>
        private void AddHinge()
        {
            eCircle c = new eCircle(this.location, 0.05f * size, eDrawType.FillAndDraw, this.layer);
            c.FillColor = new eColor(dwgBackColor);
            dwgs.Add(c);
        }

        /// <summary>
        /// Adds a roller type of joint.
        /// </summary>
        private void AddRoller()
        {
            //Adds lines for roller                       
            dwgs.Add(new eLine(new PointF(location.X - size / 2, location.Y + size / 2), new PointF(location.X + size / 2, location.Y + size / 2), this.layer));

            //Adds the hatch for the roller.
            for (int i = 0; i <= 10; i++)
            {
                dwgs.Add(new eLine(new PointF(location.X - size / 2 + 0.1f * size * (i - 1), location.Y + size / 2 + size * 0.1f), new PointF(location.X - size / 2 + 0.1f * size * i, location.Y + size / 2), this.layer));
            }

            //Adds cirlce for the roller.
            dwgs.Add(new eCircle(new PointF(location.X, location.Y + size / 4), 0.25f * size, this.layer));

        }

        /// <summary>
        /// Adds Vertical Guided Roller Joint Type.
        /// </summary>
        private void AddVerticalGuidedRoller()
        {
            eRectangle r = new eRectangle(new PointF(location.X - 0.05f * size, location.Y - size / 2), 0.1f * size, size, eDrawType.Fill, this.layer);
            r.FillColor = new eColor(dwgBackColor);
            dwgs.Add(r);
            dwgs.Add(new eLine(new PointF(location.X - 0.05f * size, location.Y - size / 2), 
                     new PointF(location.X - 0.05f * size, location.Y + size / 2), this.layer));
            dwgs.Add(((eLine)dwgs[dwgs.Count-1]).OffSet(0.1f * size, 0));
            for (int i = 0; i < 4; i++)
            {
                dwgs.Add(new eCircle(new PointF(location.X, location.Y - size / 2 + 0.05f * size + 0.3f *size* i), 0.05f * size, this.layer));
            }
        }

        /// <summary>
        /// Form a rigion for the Joint.
        /// </summary>
        private void FormRegion()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(new RectangleF(location.X - jointSnapRadius, location.Y - jointSnapRadius, 2 * jointSnapRadius, 2 * jointSnapRadius));
            region = new Region(path);
        }

        /// <summary>
        /// Delets this grid;
        /// </summary>
        /// <param name="dwgForm"></param>
        internal void DeleteGrid(eModelForm dwgForm)
        {
            if (grid == null)
                return;

            grid.ReleaseHandlers(dwgForm);
            grid.Layer.Remove(grid);
            grid = null;
        }

        /// <summary>
        /// Event handler for ESADS.EGraphics.eLayer.Modifeid event;
        /// </summary>
        /// <param name="sender">The layer sending this event.</param>
        /// <param name="e">Event argument containing all the necessary information related to the event.</param>
        private void layer_Modified(eLayer sender, eLayerModifiedEventArgs e)
        {
            if (this.color.ChangeBy == eChangeBy.ByLayer)
                this.color.SetColor(e.Color);
            if (this.lineType.ChangeBy == eChangeBy.ByLayer)
                this.lineType.SetLineType(e.LineType);
            if (this.lineWeight.ChangeBy == eChangeBy.ByLayer)
                this.lineWeight.SetLineWeight(e.LineWeight);
        }

        /// <summary>
        /// ReleaseResources the event handlers from this object.
        /// </summary>
        /// <param name="dwgForm"></param>
        internal void ReleaseHandlers(eModelForm dwgForm)
        {
            DeleteGrid(dwgForm);

            this.layer.Modified -= new eLayerModifiedEventHandler(layer_Modified);
            dwgForm.MouseClick -= new MouseEventHandler(dwgForm_MouseClick);
            dwgForm.MouseMove -= new MouseEventHandler(dwgForm_MouseMove);
            dwgForm.KeyDown -= new KeyEventHandler(dwgForm_KeyDown);
            this.layer.RectangularSelectionEnded -= new eRectangularSelectorEventHandler(layer_RectangularSelection);
            this.beam.Changed -= new EventHandler(beam_Changed);

            joint.RemoveAllLoads();
            foreach (var ld in this.loads)
            {
                ld.ReleaseHandlers(dwgForm);
                loadLayer.Remove(ld);
            }
            this.loads = new List<eGLoad>();
        }  

        internal void SelectAll()
        {
            this.isSelected = true;
            if (grid != null)
                grid.IsSelected = true;
        }
        #endregion

        #region Events
        /// <summary>
        /// Changes the joint type of the joint
        /// </summary>
        /// <param name="type">The joint type represented by the drawing.</param>
        public void ChangeType(eJointType type)
        {
            this.joint.Type = type;
            GenerateDwgObjects();
            FormRegion();
        }
        #endregion

        public float MaxNegOffset
        {
            get
            {
                return size * 0.5f * layer.ZoomFactor;
            }
        }

        /// <summary>
        /// Holds the beam bearing the joint.
        /// </summary>
        private eGBeam beam;
        private eLayer loadLayer;
        private List<eGLoad> loads;

        /// <summary>
        /// Adds a joint load to the joint object.
        /// </summary>
        /// <param name="load">The concentrated force to be added.</param>
        public void AddLoad(eConcentratedForce load)
        {
            this.joint.AddLoad(load);
            this.loads.Add(loadLayer.AddConcentratedForceLoad(load, this, beam.Document.ModelForm));
        }

        /// <summary>
        /// Adds a concentrated moment load to the joint.
        /// </summary>
        /// <param name="load">The moment to be added to the joint.</param>
        public void AddLoad(eConcentratedMoment load)
        {
            this.joint.AddLoad(load);
            this.loads.Add(loadLayer.AddConcentratedMomentLoad(load, this, beam.Document.ModelForm));
        }

        public void RemoveLoad(eGLoad load, eModelForm dwgForm)
        {
            if (load.GetType() != typeof(eGConcentratedForce) || load.GetType() != typeof(eGConcentratedMoment))
                return;
            if (!this.loads.Contains(load))
                return;

            if (load.GetType() == typeof(eGConcentratedForce))
                joint.RemoveLoad((load as eGConcentratedForce).Load);
            else
                joint.RemoveLoad((load as eGConcentratedMoment).Load);

            load.ReleaseHandlers(dwgForm);
            loads.Remove(load);
        }

        /// <summary>
        /// Gets the graphics of the beam bearing the joint.
        /// </summary>
        public eGBeam Beam
        {
            get
            {
                return this.beam;
            }
        }
    }
}
