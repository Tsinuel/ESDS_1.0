using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using ESADS.Mechanics.Analysis;
using ESADS.Mechanics.Analysis.Beam;
using ESADS.Mechanics.Design;
using ESADS.Mechanics.Design.Beam;
using ESADS.GUI;
using ESADS.GUI.Controls;

namespace ESADS.EGraphics.Beam
{
    /// <summary>
    /// Presents continious beam_Analysis graphically.
    /// </summary>
    public class eGBeam: eIGModel
    {

        #region Feilds
        /// <summary>
        /// Tells if the member input is going on.
        /// </summary>
        private bool memberInputOn;
        private double defaultEI;
        /// <summary>
        /// Value of the 'DefaultJoint' property.
        /// </summary>
        private eJointType defaultJoint;
        /// <summary>
        /// value idicating whether a member waiting the next point to finish the input.
        /// </summary>
        private bool membStarted;
        /// <summary>
        /// The single horizontal grid along the beam_Analysis axis.
        /// </summary>
        private eGrid hGrid;
        /// <summary>
        /// The temporary dimension shown with the temporary member.
        /// </summary>
        //private eDimension tempDim;
        /// <summary>
        /// The user length unit.
        /// </summary>
        private eLengthUnits lengthUnit
        {
            get
            {
                return dwgForm.Document.LengthUnit;
            }
        }
        /// <summary>
        /// Textbox used to receive numeric input from the user.
        /// </summary>
        private eNumericTextBox textBox;
        /// <summary>
        /// Holds a value for property 'Beam_Analysis'.
        /// </summary>
        private eABeam beam_Analysis;
        /// <summary>
        /// Holds the value of the 'eDBeam' property.
        /// </summary>
        private eDBeam beam_Design;
        /// <summary>
        /// Holds a value for property 'location'.
        /// </summary>
        private PointF location;
        /// <summary>
        /// Holds a value for property 'Joints'.
        /// </summary>
        private List<eGJoint> joints;
        /// <summary>
        /// Holds a value for property 'Members'.
        /// </summary>
        private List<eGMember> members;
        /// <summary>
        /// Holds a value for property 'SFD'.
        /// </summary>
        private eDiagram sFD;
        /// <summary>
        /// Holds a value for property 'BMD'.
        /// </summary>
        private eDiagram bMD;
        /// <summary>
        /// Contains all the layers that are used to present this beam_Analysis graphically.
        /// </summary>
        private eLayers layers;
        /// <summary>
        /// The Form on which the drawing is done.
        /// </summary>
        private eModelForm dwgForm;
        /// <summary>
        /// Line representing a temporary member.
        /// </summary>
        private eLine tempMemb;
        /// <summary>
        /// Holds the value of 'Extent_V'.
        /// </summary>
        private double extent_V;
        #endregion  

        #region Constructors
        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eGBeam given the drawing Form and the Mechanics Beam_Analysis.
        /// </summary>
        /// <param name="beam_Analysis">The beam_Analysis for which graphic componets to be initialized.</param>
        /// <param name="dwgForm">The Form on which the drawing is done.</param>
        public eGBeam(eABeam beam, eDocument document, eBeamSection defaultSection)
        {
            this.maxMagnitude = 0.0;
            this.beam_Analysis = beam;
            this.beam_Design = new eDBeam(this.beam_Analysis, defaultSection);
            this.dwgForm = document.ModelForm;
            this.document = document;
            this.layers = new eLayers(dwgForm);
            this.diagramStyle = eDiagramStyle.Fill;
            layers.Scale = 0.06f;
            InitializeComponents();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the default value of the EI value when members are drawn.
        /// </summary>
        public double DefaultEI
        {
            get
            {
                return defaultEI;
            }
            set
            {
                defaultEI = value;
            }
        }

        /// <summary>
        /// Gets or sets the default joint when members are drawn first.
        /// </summary>
        public eJointType DefaultJoint
        {
            get
            {
                return defaultJoint;
            }
            set
            {
                defaultJoint = value;
            }
        }

        /// <summary>
        /// Gets the collection of layers holding all the drawings of the beam_Analysis.
        /// </summary>
        public eLayers Layers
        {
            get
            {
                return layers;
            }
        }

        /// <summary>
        /// Gets the largest vertical distance of any dwg object point from the center of the beam_Analysis. The number is positive.
        /// </summary>
        private float MaxNegOffset
        {
            get
            {
                float max = 0;
                foreach (eGMember m in members)
                {
                    float mx = m.MaxNegOffset;
                    if (mx > max)
                        max = mx;
                }

                foreach (eGJoint j in joints)
                {
                    float mx = j.MaxNegOffset;
                    if (mx > max)
                        max = mx;
                }

                return max;
            }
        }

        /// <summary>
        /// Gets all the members of the beam_Analysis
        /// </summary>
        public List<eGMember> Members
        {
            get
            {
                return members;
            }
        }

        /// <summary>
        /// Gets the maximum negative offset from the beam_Analysis center including the dimension line.
        /// </summary>
        public float MaxTotalNegOffset
        {
            get
            {
                float max = 0.0f;

                foreach (eGMember m in members)
                {
                    if (m.MaxTotalNegOffset > max)
                        max = m.MaxTotalNegOffset;
                }
                return max;
            }
        }

        /// <summary>
        /// Gets all the joint drawings of the beam_Analysis.
        /// </summary>
        public List<eGJoint> Joints
        {
            get
            {
                return joints;
            }
        }
        /// <summary>
        /// Gets the starting point of the beam_Analysis or the location of the first joint as we move from the left to the right.
        /// </summary>
        public PointF Location
        {
            get
            {
                return members[0].Start;
            }
        }

        /// <summary>
        /// Gets the analysis Beam_Analysis to be presented graphicaly.
        /// </summary>
        public eABeam Beam_Analysis
        {
            get { return beam_Analysis; }
        }

        /// <summary>
        /// Gets the design beam to manage its design.
        /// </summary>
        public eDBeam Beam_Design
        {
            get { return beam_Design; }
        }

        /// <summary>
        /// Gets Shear Force Diagram.
        /// </summary>
        public eDiagram SFD
        {
            get
            {
                return sFD;
            }
        }

        /// <summary>
        /// Gets Bending Moment Diagram.
        /// </summary>
        public eDiagram BMD
        {
            get
            {
                return bMD;
            }
        }

        /// <summary>
        /// Gets the maximum screen distance from  the beam to the tip of the maximum load.
        /// </summary>
        public double Extent_V
        {
            get
            {
                return extent_V;
            }
        }  

        #endregion

        #region Methods

        private void InitializeComponents()
        { 
            //
            //Adds all the layers
            //
            this.layers.Add("Grids", Color.Gray, eLineTypes.Continuous, 0.5f, new Font("Arial", 10));
            this.layers.Add("Dimensions", Color.Crimson, eLineTypes.Continuous, 1, new Font("Arial", 7));
            this.layers.Add("Members", Color.Yellow, eLineTypes.Continuous, 1, new Font("Arial", 10));
            this.layers.Add("Loads", Color.LightGray, eLineTypes.Continuous, 1, new Font("Arial", 9));
            this.layers.Add("Joints", Color.Cyan, eLineTypes.Continuous, 1, new Font("Arial", 10));
            //
            //Initializes Other componets.
            //
            this.members = new List<eGMember>();
            this.joints = new List<eGJoint>();

            this.tempMemb = layers["Members"].AddLine(new PointF(), new PointF());
            //this.tempDim = layers["Members"].AddDim(tempMemb.Location, tempMemb.End, "", eDimensionType.LinearHorizontal, eDimensionLinePosition.RightOrBottom, 15);
            //this.tempDim.Color = new eColor( Color.FromArgb(100, 250, 250, 250));
            //this.tempDim.TextStyle = new eTextStyle(new Font("Arial", 7), eChangeBy.ByObject);
            //this.tempDim.Visible = false;
            this.tempMemb.Visible = false;

            this.textBox = new eNumericTextBox();
            this.dwgForm.Controls.Add(textBox);
            this.textBox.BackColor = System.Drawing.SystemColors.MenuText;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.ForeColor = System.Drawing.Color.White;
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(100, 20);
            this.textBox.TabIndex = 0;
            this.textBox.AutomaticResize = true;
            this.textBox.Visible = false;
            this.defaultJoint = eJointType.Pin;
            this.memberInputOn = false;

            this.dwgForm.KeyPreview = true;
            this.dwgForm.KeyDown += new KeyEventHandler(dwgForm_KeyDown);
            this.dwgForm.MouseClick += new MouseEventHandler(dwgForm_MouseClick);
            this.dwgForm.MouseMove += new MouseEventHandler(dwgForm_MouseMove);
            this.dwgForm.UnitChanged += new eUnitChangedEventHandler(dwgForm_UnitChanged);
        }

        private void dwgForm_UnitChanged(object sender, eUnitChangedEventArgs e)
        {
            //this.lengthUnit = e.LengthUnit;
        }

        private bool NumericKeyPressed(KeyEventArgs e)
        {
            if (e.KeyCode != Keys.NumPad0 && e.KeyCode != Keys.NumPad1 && e.KeyCode != Keys.NumPad2 && e.KeyCode != Keys.NumPad3 && e.KeyCode != Keys.NumPad4 &&
                e.KeyCode != Keys.NumPad5 && e.KeyCode != Keys.NumPad6 && e.KeyCode != Keys.NumPad7 && e.KeyCode != Keys.NumPad8 && e.KeyCode != Keys.NumPad9 &&
                e.KeyCode != Keys.D0 && e.KeyCode != Keys.D1 && e.KeyCode != Keys.D2 && e.KeyCode != Keys.D3 && e.KeyCode != Keys.D4 && e.KeyCode != Keys.D5 &&
                e.KeyCode != Keys.D6 && e.KeyCode != Keys.D7 && e.KeyCode != Keys.D8 && e.KeyCode != Keys.D9 && e.KeyCode != Keys.Back && e.KeyCode != Keys.OemMinus)
                return false;
            else
                return true;
        }

        private void dwgForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (memberInputOn && membStarted)
            {
                if (e.Location.X > tempMemb.Location.X)
                {
                    tempMemb.End = new PointF(e.Location.X, tempMemb.End.Y);
                    //tempDim.End = tempMemb.End;
                    //tempDim.Text = Math.Round(eUtility.Convert(tempMemb.Length / layers.Scale, eUtility.SLU, this.lengthUnit), 3).ToString();
                    if (!textBox.Enabled)
                    {
                        textBox.Location = new Point((int)((tempMemb.End.X + tempMemb.Location.X) / 2), (int)(tempMemb.Location.Y + textBox.Size.Height));
                        textBox.DoubleValue = Math.Round(eUtility.Convert(tempMemb.Length / layers.Scale, eUtility.SLU, this.lengthUnit), 3);
                    }

                    dwgForm.Invalidate();
                }
            }
            
        }

        private void dwgForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (!memberInputOn)
                return;

            if (e.Button != MouseButtons.Left)
                return;

            (sender as eModelForm).ObjFoundBelowClickPt = true;

            if (membStarted && e.Location.X < tempMemb.Location.X)
                return;

            if (membStarted)
            {
                AddMember();
                tempMemb.Location = tempMemb.End;
                tempMemb.End = tempMemb.Location;
                textBox.Location = new Point((int)((tempMemb.End.X + tempMemb.Location.X) / 2), (int)(tempMemb.Location.Y + textBox.Size.Height));
                textBox.Visible = true;
                textBox.Enabled = false;
                //tempDim.Start = tempMemb.Location;
                //tempDim.End = tempMemb.End;
                dwgForm.Invalidate();
            }
            else
            {
                if (members.Count > 0)
                {
                    tempMemb.Location = members[members.Count - 1].End;
                    tempMemb.End = tempMemb.Location;
                }
                else
                {
                    tempMemb.Location = e.Location;
                }
                tempMemb.Visible = true;
                //tempDim.Visible = true;
                //tempDim.Start = tempMemb.Location;
                //tempDim.End = tempMemb.End;
                textBox.Location = new Point((int)((tempMemb.End.X + tempMemb.Location.X) / 2), (int)(tempMemb.Location.Y + textBox.Size.Height / 4));
                textBox.Visible = true;
                textBox.Enabled = false;

                membStarted = true;

            }
        }

        /// <summary>
        /// Toggles the section name labels of each member, 'ON'.
        /// </summary>
        public void ShowSectionNames()
        {
            foreach (var memb in members)
                memb.ShowSectionName = true;
        }

        /// <summary>
        /// Toggles the section name labels of each member, 'Off'.
        /// </summary>
        public void HideSectionNames()
        {
            foreach (var memb in members)
                memb.ShowSectionName = false;
        }

        private void AddMember()
        {
            double length = (tempMemb.End.X - tempMemb.Location.X) / layers.Scale;
            if (textBox.Visible)
                length = eUtility.Convert(textBox.DoubleValue, this.lengthUnit, eUtility.SLU);
            if (length <= 0)
                return;
            eAMember m;

            if (members.Count == 0)
            {
                m = new eAMember(new eJoint(this.defaultJoint, beam_Analysis), new eJoint(this.defaultJoint, beam_Analysis), length, beam_Analysis);
                hGrid = layers["Grids"].AddGrid(tempMemb.Location, "A", dwgForm, eGridType.Horizontal);
                AddJoint(tempMemb.Location, m.NEJoint, eJointOrientation.RightSideConnected);
            }
            else
                m = new eAMember(members[members.Count - 1].Member_Analysis.FEJoint, new eJoint(this.defaultJoint, beam_Analysis), length, beam_Analysis);

            AddJoint(tempMemb.End, m.FEJoint);
            AddMember(m, tempMemb.Location);
        }

        public void AddMember(double Length, double EI, eJointType FE_Joint)
        {
            eAMember m;
            if (members.Count == 0)
            {
                m = new eAMember(new eJoint(this.defaultJoint, beam_Analysis), new eJoint(FE_Joint, beam_Analysis), Length, beam_Analysis);
                hGrid = layers["Grids"].AddGrid(new PointF(250, 250), "A", dwgForm, eGridType.Horizontal);
                AddJoint(new PointF(250, 250), m.NEJoint, eJointOrientation.LeftSideConnected);
            }
            else
            {
                m = new eAMember(members[members.Count - 1].Member_Analysis.FEJoint, new eJoint(FE_Joint, beam_Analysis), Length, beam_Analysis);
            }

            AddMember(m, joints[joints.Count - 1].Location);
            AddJoint(members[members.Count - 1].End, m.FEJoint);
        }

        public void AddMembers(int Number, double Length, double EI, eJointType JointType)
        {
            this.defaultEI = EI;
            this.defaultJoint = JointType;

            for (int i = 1; i <= Number; i++)
            {
                AddMember(Length, EI, JointType);
            }

            OnChanged();
        }

        private void dwgForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (dwgForm.Locked)
                return;

            if (e.KeyCode == Keys.Delete)
            {
                if ((sender as eModelForm).EditingText)
                    return;

                for (int i = 0; i < joints.Count; i++)
                {
                    if (joints[i].IsSelected)
                        RemoveJoint(i);
                    else if (joints[i].Grid != null && joints[i].Grid.IsSelected)
                        joints[i].DeleteGrid(dwgForm);
                }

                for (int i = members.Count - 1; i >= 0; i--)
                {
                    if (members[i].IsSelected)
                        RemoveLastMember();
                    else
                        break;
                }

                if (hGrid != null && hGrid.IsSelected)
                {
                    hGrid.ReleaseHandlers(dwgForm);
                    hGrid.Layer.Remove(hGrid);
                    hGrid = null;
                }

                dwgForm.Invalidate();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (textBox.Visible)
                {
                    if (textBox.DoubleValue <= 0)
                    {
                        MessageBox.Show("A member length cannot be zero or negative.", "Invalid member length", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox.Focus();
                        textBox.SelectAll();
                    }
                    else
                    {
                        double l = eUtility.Convert(textBox.DoubleValue, this.lengthUnit, eUtility.SLU);
                        tempMemb.End = new PointF((float)(tempMemb.Location.X + layers.Scale * l), tempMemb.End.Y);
                        AddMember();
                        textBox.Enabled = false;
                        this.dwgForm.Focus();
                        textBox.DoubleValue = 0.0;
                        tempMemb.Location = tempMemb.End;
                        tempMemb.End = tempMemb.Location;
                        //tempDim.Start = tempMemb.Location;
                        //tempDim.End = tempMemb.End;
                        dwgForm.Invalidate();
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (textBox.Visible)
                {
                    textBox.Visible = false;
                    this.dwgForm.Focus();
                    textBox.DoubleValue = 0;
                }
                else if (membStarted)
                {
                    tempMemb.Visible = false;
                    //tempDim.Visible = false;
                    membStarted = false;
                    dwgForm.Invalidate();
                }
                else if (memberInputOn)
                    this.StopDrawingMemeber();
            }
            else if (memberInputOn && membStarted && NumericKeyPressed(e))
            {
                textBox.Visible = true;
                textBox.Focus();
                textBox.Font = new Font("Arial", 12);
                textBox.Location = new Point((int)tempMemb.End.X, (int)tempMemb.End.Y);
            }
            else if (e.KeyCode == Keys.A && e.Control)
            {
                SelectAll();
            }
        }

        internal void SelectAll()
        {
            foreach (eGMember m in members)
                m.SelectAll();

            foreach (eGJoint j in joints)
                j.SelectAll();

            if (hGrid != null)
                hGrid.IsSelected = true;
            dwgForm.Invalidate();
        }

        public float GetX(double X)
        {
            return (float)(X * layers.Scale);
        }

        public double GetX(float X)
        {
            return X /layers.Scale;
        }

        public void AddJoint(PointF location, eJoint joint, eJointOrientation orientation = eJointOrientation.LeftSideConnected)
        {
            beam_Analysis.AddJoint(joint);
            joints.Add(layers["Joints"].AddJoint(this, location, joint, layers["Loads"], layers["Grids"], (joints.Count + 1).ToString(), orientation, this.dwgForm));
            dwgForm.Invalidate();
        }

        public void AddMember(eAMember member, PointF start)
        {
            dwgForm.KeyDown -= new KeyEventHandler(dwgForm_KeyDown); 

            PointF end = new PointF((float)(start.X + member.Length * layers.Scale), start.Y);

            beam_Analysis.AddMember(member);
            beam_Design.Members.Add(member.Member_Design);
            members.Add(layers["Members"].AddMember(this, member, start, end, layers["Loads"], layers["Dimensions"], this.dwgForm));
            dwgForm.Invalidate();
            members[members.Count - 1].Resize += new eMemberGraphicsEventHandler(Member_Resize);
            members[members.Count - 1].ReloadDimension += new EventHandler(Member_ReloadDimension);

            dwgForm.KeyDown += new KeyEventHandler(dwgForm_KeyDown);

            Member_ReloadDimension(members[members.Count - 1], new EventArgs());
            OnChanged();
        }

        private void Member_ReloadDimension(object sender, EventArgs e)
        {
            float max =  this.MaxNegOffset;

            foreach (eGMember m in members)
                m.ReloadDimensions(max);
        }

        private void Member_Resize(object sender, eMemberGraphicsEventArgs e)
        {
            int n = members.IndexOf(sender as eGMember);

            if (members[n].Member_Analysis.Length == e.Length)
                return;

            joints[n + 1].Location = e.End;

            for (int i = n + 1; i < members.Count; i++)
            {
                members[i].Location = new PointF(members[i].Location.X + e.End.X - members[n].End.X, e.End.Y);
                joints[i + 1].Location = members[i].End;
            }

            
        }

        public void RemoveJoint(int index, bool pseudoType = true)
        {
            if (pseudoType)
            {
                if (index != 0 && index != joints.Count - 1)
                {
                    joints[index].DeleteGrid(dwgForm);
                    joints[index].ChangeType(eJointType.Continious);
                    joints[index].IsSelected = false;
                }
                else
                {
                    joints[index].DeleteGrid(dwgForm);
                    joints[index].ChangeType(eJointType.Free);
                    joints[index].IsSelected = false;
                }
            }
            else
            {
                beam_Analysis.Joints.Remove(joints[index].Joint);
                joints[index].ReleaseHandlers(dwgForm);
                joints.RemoveAt(index);
                layers["Joints"].DwgObjects.RemoveAt(index);
                dwgForm.Invalidate();
            }
        }

        public void RemoveLastMember()
        {
            if (members.Count == 0)
                return;

            RemoveJoint(members.Count, false);

            if (members.Count == 1)
            {
                RemoveJoint(0, false);
                if (hGrid != null)
                {
                    hGrid.ReleaseHandlers(dwgForm);
                    hGrid.Layer.Remove(hGrid);
                    hGrid = null;
                }
            }
            else if (members[members.Count - 1].Member_Analysis.NEJoint.Type == eJointType.Hinge || members[members.Count - 1].Member_Analysis.NEJoint.Type == eJointType.VerticalGuidedRoller)
            {
                joints[joints.Count - 1].ChangeType(eJointType.Free);
            }

            beam_Design.Members.Remove(members[members.Count - 1].Member_Design);
            beam_Analysis.Members.Remove(members[members.Count - 1].Member_Analysis);
            members[members.Count - 1].ReleaseHandlers();
            members[members.Count - 1].DeleteComponenets();
            layers["Members"].Remove(members[members.Count - 1]);
            members[members.Count - 1].Resize -= new eMemberGraphicsEventHandler(Member_Resize);
            members.RemoveAt(members.Count - 1);

            OnChanged();
        }

        /// <summary>
        /// Shows the layer_ShearBars force diagram of the beam_Analysis.
        /// <param name="diagramStyle">Indicates the displayf mode of the diagram.</param>
        /// </summary>
        public void ShowSFD()
        {
            if (beam_Analysis.AnaysisCompleted)
            {
                if (!this.layers.Contains("SFD"))
                    this.layers.Add("SFD", Color.Yellow, eLineTypes.Continuous, 1, new Font("Courier New", 70));
                else
                {
                    this.layers["SFD"].RemoveObjects();
                    this.layers["SFD"].TextStyle = new eTextStyle(new Font("Courier New", 70), eChangeBy.ByLayer);
                    this.layers["SFD"].LineType = new eLineType(eLineTypes.Continuous, eChangeBy.ByLayer);
                }
                this.sFD = new eDiagram(this, eDiagramType.SFD, this.layers["SFD"], this.dwgForm);
                this.sFD.DiagramStyle = diagramStyle;
                this.sFD.ScaleFactor = (float)(GetShortestMember().Length / (4 * this.beam_Analysis.GetMaxShear()));
                this.sFD.ArrageDiagram();
                this.sFD.Arranged = false;
                if (this.bMD != null)
                    ShowBMD();
                this.dwgForm.Invalidate();
            }
           
        }

        /// <summary>
        /// Shows the beanding moment diagram of the beam_Analysis.
        /// <param name="diagramStyle">The display mode of the diagram.</param>
        /// </summary>
        public void ShowBMD()
        {
            if (beam_Analysis.AnaysisCompleted)
            {
                if (!this.layers.Contains("BMD"))
                    this.layers.Add("BMD", Color.Yellow, eLineTypes.Continuous, 1, new Font("Courier New", 70));
                else
                {
                    this.layers["BMD"].RemoveObjects();
                    this.layers["BMD"].TextStyle = new eTextStyle(new Font("Courier New", 70), eChangeBy.ByLayer);
                    this.layers["BMD"].LineType = new eLineType(eLineTypes.Continuous, eChangeBy.ByLayer);
                }
                this.bMD = new eDiagram(this, eDiagramType.BMD, this.layers["BMD"], this.dwgForm);
                this.bMD.DiagramStyle = diagramStyle;
                this.bMD.ScaleFactor = GetShortestMember().Length / (4 * this.beam_Analysis.GetMaxMoment());
                this.bMD.ArrageDiagram();
                this.dwgForm.Invalidate();
            }
        }

        /// <summary>
        /// Returns the member with the shortest lenght.
        /// </summary>
        /// <returns></returns>
        private eAMember GetShortestMember()
        {
            eAMember shortest = beam_Analysis.Members[0];
            for (int i = 0; i < beam_Analysis.Members.Count; i++)
            {
                if (beam_Analysis.Members[i].Length < shortest.Length)
                    shortest = beam_Analysis.Members[i];
            }
            return shortest;
        }

        /// <summary>
        /// Starts drawing member using mouse and keyboard.
        /// </summary>
        public void StartDrawingMember(double defaultEI, eJointType defaultJoint)
        {
            if (this.dwgForm.Locked)
            {
                MessageBox.Show("The model cannot be edited while it is locked. Unlock it to continue.", "Model locked", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

                this.defaultEI = defaultEI;
                this.defaultJoint = defaultJoint;
                this.memberInputOn = true;
                this.dwgForm.ObjFoundBelowClickPt = true;
                dwgForm.Cursor = Cursors.UpArrow;

            if(members.Count > 0)
            {
                tempMemb.Location = members[members.Count - 1].End;
                tempMemb.Visible = true;
                //tempDim.Visible = true;
                //tempDim.Start = tempMemb.Location;
               // tempDim.End = tempMemb.End;
                membStarted = true;
            }
        }

        /// <summary>
        /// Stops drawing member using mouse and keyboard.
        /// </summary>
        public void StopDrawingMemeber()
        {
            this.memberInputOn = false;
            this.dwgForm.ObjFoundBelowClickPt = false;
            membStarted = false;
            tempMemb.Visible = false;
            //tempDim.Visible = false;
            tempMemb.End = tempMemb.Location;
            dwgForm.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Removes the SFD and BMD diagrams
        /// </summary>
        public void RemoveDiagrams()
        {
            layers["SFD"].RemoveObjects();
            layers["BMD"].RemoveObjects();
            if (sFD != null)
                sFD.ReleaseHandlers(dwgForm);
            if (bMD != null)
                bMD.ReleaseHandlers(dwgForm);
            sFD = null;
            bMD = null;
            dwgForm.Invalidate();
        }

        /// <summary>
        /// Shows the dimensions of the loads relative to its member.
        /// </summary>
        public void ShowLoadDimensions()
        {
            float max = this.MaxNegOffset;

            foreach (eGMember m in members)
                m.ShowLoadDimensions(max);

            dwgForm.Invalidate();
        }

        /// <summary>
        /// Hides the dimensions of the loads.
        /// </summary>
        public void HideLoadDimensions()
        {
            foreach (eGMember m in members)
                m.HideLoadDimensions();

            dwgForm.Invalidate();
        }

        /// <summary>
        /// Fits this beam_Analysis drawing to the screen.
        /// </summary>
        public void ZoomFit()
        {
            float dx, dy, kx, ky, vGridOffset;
            PointF loc;
            vGridOffset = 0;
            for (int i = 0; i < joints.Count; i++)
            {
                if (joints[i].Grid != null)
                {
                    vGridOffset = joints[i].Grid.GetMaxOffset();
                    break;
                }
            }

            if (hGrid != null)
                dx = Math.Abs(joints[joints.Count - 1].Location.X - joints[0].Location.X) + hGrid.GetMaxOffset();
            else
                dx = Math.Abs(joints[joints.Count - 1].Location.X - joints[0].Location.X);

            if ((this.sFD != null) && (this.bMD != null))
                dy = Math.Abs(this.joints[0].Location.Y - this.bMD.GetMinPoint().Y) + vGridOffset;
            else if (this.sFD != null && this.bMD == null)
                dy = Math.Abs(this.joints[0].Location.Y - this.sFD.GetMinPoint().Y) + vGridOffset;
            else if (this.sFD == null && this.bMD != null)
                dy = Math.Abs(this.joints[0].Location.Y - this.bMD.GetMinPoint().Y) + vGridOffset;
            else
                dy = MaxTotalNegOffset + vGridOffset;

            kx = this.dwgForm.ClientSize.Width / dx;
            kx *= (1 - 100f / this.dwgForm.ClientSize.Width);
            ky = this.dwgForm.ClientSize.Height / dy;
            ky *= (1 - 100f / this.dwgForm.ClientSize.Height);
            loc = this.joints[0].Location;

            if (kx < ky)
            {
                layers.Zoom(loc, kx);
                layers.Pan(-loc.X + 50, -loc.Y + vGridOffset * kx + (dwgForm.ClientSize.Height - kx * dy) / 2f - 50);
            }
            else
            {
                layers.Zoom(loc, ky);
                layers.Pan(-loc.X + (dwgForm.ClientSize.Width - ky * dx) / 2f, -loc.Y + vGridOffset * ky + 50);
            }
        }

        /// <summary>
        /// Fills the extent of the beam.
        /// </summary>
        private void FillExtentValues()
        {
            this.maxMagnitude = 0.0;
            longestMemb = 0;
            foreach (eGMember m in members)
            {
                if (m.Member_Analysis.Length > longestMemb)
                    longestMemb = m.Member_Analysis.Length;

                foreach (eGLoad l in m.Loads)
                {
                    if (l.GetType() == typeof(eGRectangularLoad))
                        this.maxMagnitude = Math.Max(Math.Abs((l as eGRectangularLoad).Load.UnfactoredMagnitude), maxMagnitude);
                    else if (l.GetType() == typeof(eGTriangularLoad))
                        this.maxMagnitude = Math.Max(Math.Abs((l as eGTriangularLoad).Load.UnfactoredMagnitude), maxMagnitude);
                    else if (l.GetType() == typeof(eGTrapezoidalLoad))
                        this.maxMagnitude = Math.Max(Math.Abs((l as eGTrapezoidalLoad).Load_Rect.UnfactoredMagnitude + (l as eGTrapezoidalLoad).Load_Tri.UnfactoredMagnitude), maxMagnitude);
                }
            }

            this.extent_V = 0.005 * longestMemb;
        }

        /// <summary>
        /// Gets the maximum magnitude of distributed load all over the beam.
        /// </summary>
        public double MaxMagnitude
        {
            get
            {
                return maxMagnitude;
            }
        }

        /// <summary>
        /// Gets the longest member length among all the members.
        /// </summary>
        internal double LongestMemberLength
        {
            get
            {
                return this.longestMemb;
            }
        }

        /// <summary>
        /// Holds the value of 'MaxMagnitude'.
        /// </summary>
        private double maxMagnitude;

        /// <summary>
        /// Occurs when the beam's member length, number of member, magnitude of any distributed load changes.
        /// </summary>
        public event EventHandler Changed;

        /// <summary>
        /// Fires the 'Changed' event.
        /// </summary>
        internal void OnChanged()
        {
            if (this.Changed != null)
            {
                FillExtentValues();
                this.Changed(this, new EventArgs());
            }
        }
        #endregion

        /// <summary>
        /// Gets or sets the value whether to show the member dimensions or not.
        /// </summary>
        public bool DimensionsShown
        {
            get
            {
                return this.dimensionsShown;
            }
            set
            {
                this.dimensionsShown = value;
            }
        }

        /// <summary>
        /// Gets or sets the value if the detailed dimension of the loads is shown or not. When it is turned on, it automatically turns 'DimensionsShow' property to 'true'.
        /// </summary>
        public bool LoadDiamensionsShown
        {
            get
            {
                return this.LoadDimensionsShown;
            }
            set
            {
                this.LoadDimensionsShown = value;
                if (!dimensionsShown)
                    this.DimensionsShown = true;
            }
        }

        /// <summary>
        /// Holds the value of 'DimensionsShown'.
        /// </summary>
        private bool dimensionsShown;
        /// <summary>
        /// Holds the value of 'LoadDimensionsShown'.
        /// </summary>
        private bool LoadDimensionsShown;
        private eBeamDetail detail;
        private double longestMemb;
        private eDiagramStyle diagramStyle;
        private eDocument document;

        /// <summary>
        /// Turns the dimensions on.
        /// </summary>
        public void ShowDimensions()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Turns all dimensions off.
        /// </summary>
        public void HideDimensions()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the document which holds the beam.
        /// </summary>
        internal eDocument Document
        {
            get
            {
                return this.document;
            }
        }

        /// <summary>
        /// Distroys the detail drawing of the beam if it is already drawn.
        /// </summary>
        public void DestroyDetailing()
        {
            if (detail != null)
                detail.Destroy();
        }

        public void Design()
        {
            if (beam_Analysis.AnaysisCompleted)
            {
                beam_Design.Design();
                bMD.Layer.LayerOn = false;
                sFD.Layer.LayerOn = false;
                this.detail = new eBeamDetail(this, layers, new PointF(50, 50));
                detail.Generate();
            }
        }

        /// <summary>
        /// Gets the total width of the beam graphics excluding the grids.
        /// </summary>
        internal float GetOverallWidth()
        {
            float width = 0.0f;

            foreach (var memb in this.members)
            {
                width += (memb.End.X - memb.Start.X);
            }

            return width;
        }

        /// <summary>
        /// Gets the maximum graphical distance from the members' line to the bottom most drawing component.
        /// </summary>
        internal float GetMaxNegativeOffset()
        {
            float h = 0.0f, temp;

            h = this.MaxTotalNegOffset;

            if (this.sFD != null)
            {
                temp = sFD.GetMinPoint().Y;
                if ((temp - location.Y) > h)
                    h = temp - location.Y;
            }
            if (this.bMD != null)
            {
                temp = bMD.GetMinPoint().Y;
                if ((temp - location.Y) > h)
                    h = temp - location.Y;
            }

            return h;
        }

        /// <summary>
        /// Gets or sets  the diagram type used to draw SFD and BMD.
        /// </summary>
        public eDiagramStyle DiagramStyle
        {
            get
            {
                return this.diagramStyle;
            }
            set
            {
                this.diagramStyle = value;
                if (this.bMD != null)
                    this.bMD.DiagramStyle = value;
                if (this.sFD != null)
                    this.sFD.DiagramStyle = value;
            }
        }

        /// <summary>
        /// Gets the detail drawing of the beam.
        /// </summary>
        public eBeamDetail Detail
        {
            get
            {
                return this.detail;
            }
        }
    }
}
