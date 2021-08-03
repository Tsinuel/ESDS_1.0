namespace ESADS.GUI
{
    partial class eNewSlabDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gpxUseUniformGrid = new System.Windows.Forms.GroupBox();
            this.ntxtNumV_Grids = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtNumH_Grids = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtXGridSpacing = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtYGridSpacing = new ESADS.GUI.Controls.eNumericTextBox();
            this.lblYGridSpacing = new System.Windows.Forms.Label();
            this.lblXGridSpacing = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEditGridData = new System.Windows.Forms.Button();
            this.radCustomGridSpacing = new System.Windows.Forms.RadioButton();
            this.radUniformGridSpacing = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkUseMinReqDepth = new System.Windows.Forms.CheckBox();
            this.ntxtGrossDepth = new ESADS.GUI.Controls.eNumericTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxForce = new System.Windows.Forms.ComboBox();
            this.cbxLength = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ntxtBeamDepth = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtBeamWidth = new ESADS.GUI.Controls.eNumericTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radCircularColumn = new System.Windows.Forms.RadioButton();
            this.radRectColumn = new System.Windows.Forms.RadioButton();
            this.ntxtColumnWidth = new ESADS.GUI.Controls.eNumericTextBox();
            this.lblColumnWidth = new System.Windows.Forms.Label();
            this.lblColumnDepthOrDiam = new System.Windows.Forms.Label();
            this.ntxtColumnDiamOrDepth = new ESADS.GUI.Controls.eNumericTextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chkConsiderSelfWeight = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.gpxUseUniformGrid.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gpxUseUniformGrid);
            this.groupBox1.Controls.Add(this.btnEditGridData);
            this.groupBox1.Controls.Add(this.radCustomGridSpacing);
            this.groupBox1.Controls.Add(this.radUniformGridSpacing);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 204);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Grids";
            // 
            // gpxUseUniformGrid
            // 
            this.gpxUseUniformGrid.Controls.Add(this.ntxtNumV_Grids);
            this.gpxUseUniformGrid.Controls.Add(this.ntxtNumH_Grids);
            this.gpxUseUniformGrid.Controls.Add(this.ntxtXGridSpacing);
            this.gpxUseUniformGrid.Controls.Add(this.ntxtYGridSpacing);
            this.gpxUseUniformGrid.Controls.Add(this.lblYGridSpacing);
            this.gpxUseUniformGrid.Controls.Add(this.lblXGridSpacing);
            this.gpxUseUniformGrid.Controls.Add(this.label1);
            this.gpxUseUniformGrid.Controls.Add(this.label3);
            this.gpxUseUniformGrid.Location = new System.Drawing.Point(6, 39);
            this.gpxUseUniformGrid.Name = "gpxUseUniformGrid";
            this.gpxUseUniformGrid.Size = new System.Drawing.Size(277, 124);
            this.gpxUseUniformGrid.TabIndex = 11;
            this.gpxUseUniformGrid.TabStop = false;
            // 
            // ntxtNumV_Grids
            // 
            this.ntxtNumV_Grids.AutomaticResize = false;
            this.ntxtNumV_Grids.DataType = ESADS.GUI.Controls.eDataType.Integer;
            this.ntxtNumV_Grids.DoubleValue = 0D;
            this.ntxtNumV_Grids.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtNumV_Grids.IntValue = 0;
            this.ntxtNumV_Grids.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtNumV_Grids.Location = new System.Drawing.Point(155, 16);
            this.ntxtNumV_Grids.Measurment = ESADS.GUI.Controls.eMeasurment.None;
            this.ntxtNumV_Grids.Name = "ntxtNumV_Grids";
            this.ntxtNumV_Grids.Size = new System.Drawing.Size(100, 20);
            this.ntxtNumV_Grids.SU = 0D;
            this.ntxtNumV_Grids.TabIndex = 7;
            this.ntxtNumV_Grids.Text = "0";
            // 
            // ntxtNumH_Grids
            // 
            this.ntxtNumH_Grids.AutomaticResize = false;
            this.ntxtNumH_Grids.DataType = ESADS.GUI.Controls.eDataType.Integer;
            this.ntxtNumH_Grids.DoubleValue = 0D;
            this.ntxtNumH_Grids.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtNumH_Grids.IntValue = 0;
            this.ntxtNumH_Grids.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtNumH_Grids.Location = new System.Drawing.Point(155, 42);
            this.ntxtNumH_Grids.Measurment = ESADS.GUI.Controls.eMeasurment.None;
            this.ntxtNumH_Grids.Name = "ntxtNumH_Grids";
            this.ntxtNumH_Grids.Size = new System.Drawing.Size(100, 20);
            this.ntxtNumH_Grids.SU = 0D;
            this.ntxtNumH_Grids.TabIndex = 6;
            this.ntxtNumH_Grids.Text = "0";
            // 
            // ntxtXGridSpacing
            // 
            this.ntxtXGridSpacing.AutomaticResize = false;
            this.ntxtXGridSpacing.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtXGridSpacing.DoubleValue = 0D;
            this.ntxtXGridSpacing.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtXGridSpacing.IntValue = 0;
            this.ntxtXGridSpacing.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtXGridSpacing.Location = new System.Drawing.Point(155, 68);
            this.ntxtXGridSpacing.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtXGridSpacing.Name = "ntxtXGridSpacing";
            this.ntxtXGridSpacing.Size = new System.Drawing.Size(100, 20);
            this.ntxtXGridSpacing.SU = 0D;
            this.ntxtXGridSpacing.TabIndex = 5;
            this.ntxtXGridSpacing.Text = "0";
            // 
            // ntxtYGridSpacing
            // 
            this.ntxtYGridSpacing.AutomaticResize = false;
            this.ntxtYGridSpacing.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtYGridSpacing.DoubleValue = 0D;
            this.ntxtYGridSpacing.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtYGridSpacing.IntValue = 0;
            this.ntxtYGridSpacing.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtYGridSpacing.Location = new System.Drawing.Point(155, 94);
            this.ntxtYGridSpacing.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtYGridSpacing.Name = "ntxtYGridSpacing";
            this.ntxtYGridSpacing.Size = new System.Drawing.Size(100, 20);
            this.ntxtYGridSpacing.SU = 0D;
            this.ntxtYGridSpacing.TabIndex = 4;
            this.ntxtYGridSpacing.Text = "0";
            // 
            // lblYGridSpacing
            // 
            this.lblYGridSpacing.AutoSize = true;
            this.lblYGridSpacing.Location = new System.Drawing.Point(16, 97);
            this.lblYGridSpacing.Name = "lblYGridSpacing";
            this.lblYGridSpacing.Size = new System.Drawing.Size(134, 13);
            this.lblYGridSpacing.TabIndex = 1;
            this.lblYGridSpacing.Text = "Grid Spacing in Y-Direction";
            // 
            // lblXGridSpacing
            // 
            this.lblXGridSpacing.AutoSize = true;
            this.lblXGridSpacing.Location = new System.Drawing.Point(15, 71);
            this.lblXGridSpacing.Name = "lblXGridSpacing";
            this.lblXGridSpacing.Size = new System.Drawing.Size(134, 13);
            this.lblXGridSpacing.TabIndex = 3;
            this.lblXGridSpacing.Text = "Grid Spacing in X-Direction";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of Vertical Grids";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Number of Horizontal Grids";
            // 
            // btnEditGridData
            // 
            this.btnEditGridData.Location = new System.Drawing.Point(161, 166);
            this.btnEditGridData.Name = "btnEditGridData";
            this.btnEditGridData.Size = new System.Drawing.Size(100, 23);
            this.btnEditGridData.TabIndex = 10;
            this.btnEditGridData.Text = "Edit Grid Data...";
            this.btnEditGridData.UseVisualStyleBackColor = true;
            this.btnEditGridData.Click += new System.EventHandler(this.btnEditGridSpacing_Click);
            // 
            // radCustomGridSpacing
            // 
            this.radCustomGridSpacing.AutoSize = true;
            this.radCustomGridSpacing.Location = new System.Drawing.Point(6, 169);
            this.radCustomGridSpacing.Name = "radCustomGridSpacing";
            this.radCustomGridSpacing.Size = new System.Drawing.Size(146, 17);
            this.radCustomGridSpacing.TabIndex = 9;
            this.radCustomGridSpacing.Text = "Use Custom Grid Spacing";
            this.radCustomGridSpacing.UseVisualStyleBackColor = true;
            this.radCustomGridSpacing.CheckedChanged += new System.EventHandler(this.rbtCustomGridSpacing_CheckedChanged);
            // 
            // radUniformGridSpacing
            // 
            this.radUniformGridSpacing.AutoSize = true;
            this.radUniformGridSpacing.Checked = true;
            this.radUniformGridSpacing.Location = new System.Drawing.Point(6, 22);
            this.radUniformGridSpacing.Name = "radUniformGridSpacing";
            this.radUniformGridSpacing.Size = new System.Drawing.Size(147, 17);
            this.radUniformGridSpacing.TabIndex = 8;
            this.radUniformGridSpacing.TabStop = true;
            this.radUniformGridSpacing.Text = "Use Uniform Grid Spacing";
            this.radUniformGridSpacing.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkUseMinReqDepth);
            this.groupBox3.Controls.Add(this.ntxtGrossDepth);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(307, 84);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(226, 76);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Slab Thickness";
            // 
            // chkUseMinReqDepth
            // 
            this.chkUseMinReqDepth.AutoSize = true;
            this.chkUseMinReqDepth.Checked = true;
            this.chkUseMinReqDepth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseMinReqDepth.Location = new System.Drawing.Point(21, 45);
            this.chkUseMinReqDepth.Name = "chkUseMinReqDepth";
            this.chkUseMinReqDepth.Size = new System.Drawing.Size(159, 17);
            this.chkUseMinReqDepth.TabIndex = 10;
            this.chkUseMinReqDepth.Text = "Use minimum required depth";
            this.chkUseMinReqDepth.UseVisualStyleBackColor = true;
            this.chkUseMinReqDepth.CheckedChanged += new System.EventHandler(this.chbxMinReqDepth_CheckedChanged);
            // 
            // ntxtGrossDepth
            // 
            this.ntxtGrossDepth.AutomaticResize = false;
            this.ntxtGrossDepth.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtGrossDepth.DoubleValue = 0D;
            this.ntxtGrossDepth.Enabled = false;
            this.ntxtGrossDepth.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtGrossDepth.IntValue = 0;
            this.ntxtGrossDepth.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtGrossDepth.Location = new System.Drawing.Point(103, 19);
            this.ntxtGrossDepth.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtGrossDepth.Name = "ntxtGrossDepth";
            this.ntxtGrossDepth.Size = new System.Drawing.Size(100, 20);
            this.ntxtGrossDepth.SU = 0D;
            this.ntxtGrossDepth.TabIndex = 9;
            this.ntxtGrossDepth.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Gross Depth";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.cbxForce);
            this.groupBox5.Controls.Add(this.cbxLength);
            this.groupBox5.Location = new System.Drawing.Point(307, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(226, 66);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Units";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(114, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Force";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Length";
            // 
            // cbxForce
            // 
            this.cbxForce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxForce.FormattingEnabled = true;
            this.cbxForce.Location = new System.Drawing.Point(154, 22);
            this.cbxForce.Name = "cbxForce";
            this.cbxForce.Size = new System.Drawing.Size(49, 21);
            this.cbxForce.TabIndex = 7;
            // 
            // cbxLength
            // 
            this.cbxLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLength.FormattingEnabled = true;
            this.cbxLength.Location = new System.Drawing.Point(61, 23);
            this.cbxLength.Name = "cbxLength";
            this.cbxLength.Size = new System.Drawing.Size(47, 21);
            this.cbxLength.TabIndex = 6;
            this.cbxLength.SelectedIndexChanged += new System.EventHandler(this.cbxLength_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(455, 340);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(374, 340);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ntxtBeamDepth);
            this.groupBox2.Controls.Add(this.ntxtBeamWidth);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(307, 222);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(226, 109);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Beam Dimension";
            // 
            // ntxtBeamDepth
            // 
            this.ntxtBeamDepth.AutomaticResize = false;
            this.ntxtBeamDepth.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtBeamDepth.DoubleValue = 0D;
            this.ntxtBeamDepth.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtBeamDepth.IntValue = 0;
            this.ntxtBeamDepth.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtBeamDepth.Location = new System.Drawing.Point(105, 27);
            this.ntxtBeamDepth.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtBeamDepth.Name = "ntxtBeamDepth";
            this.ntxtBeamDepth.Size = new System.Drawing.Size(100, 20);
            this.ntxtBeamDepth.SU = 0D;
            this.ntxtBeamDepth.TabIndex = 12;
            this.ntxtBeamDepth.Text = "0";
            // 
            // ntxtBeamWidth
            // 
            this.ntxtBeamWidth.AutomaticResize = false;
            this.ntxtBeamWidth.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtBeamWidth.DoubleValue = 0D;
            this.ntxtBeamWidth.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtBeamWidth.IntValue = 0;
            this.ntxtBeamWidth.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtBeamWidth.Location = new System.Drawing.Point(104, 57);
            this.ntxtBeamWidth.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtBeamWidth.Name = "ntxtBeamWidth";
            this.ntxtBeamWidth.Size = new System.Drawing.Size(100, 20);
            this.ntxtBeamWidth.SU = 0D;
            this.ntxtBeamWidth.TabIndex = 11;
            this.ntxtBeamWidth.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(60, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Depth";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(60, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Width";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radCircularColumn);
            this.groupBox4.Controls.Add(this.radRectColumn);
            this.groupBox4.Controls.Add(this.ntxtColumnWidth);
            this.groupBox4.Controls.Add(this.lblColumnWidth);
            this.groupBox4.Controls.Add(this.lblColumnDepthOrDiam);
            this.groupBox4.Controls.Add(this.ntxtColumnDiamOrDepth);
            this.groupBox4.Location = new System.Drawing.Point(12, 222);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(289, 109);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Column Type and Dimension";
            // 
            // radCircularColumn
            // 
            this.radCircularColumn.AutoSize = true;
            this.radCircularColumn.Location = new System.Drawing.Point(146, 19);
            this.radCircularColumn.Name = "radCircularColumn";
            this.radCircularColumn.Size = new System.Drawing.Size(60, 17);
            this.radCircularColumn.TabIndex = 20;
            this.radCircularColumn.Text = "Circular";
            this.radCircularColumn.UseVisualStyleBackColor = true;
            // 
            // radRectColumn
            // 
            this.radRectColumn.AutoSize = true;
            this.radRectColumn.Checked = true;
            this.radRectColumn.Location = new System.Drawing.Point(22, 19);
            this.radRectColumn.Name = "radRectColumn";
            this.radRectColumn.Size = new System.Drawing.Size(83, 17);
            this.radRectColumn.TabIndex = 19;
            this.radRectColumn.TabStop = true;
            this.radRectColumn.Text = "Rectangular";
            this.radRectColumn.UseVisualStyleBackColor = true;
            this.radRectColumn.CheckedChanged += new System.EventHandler(this.rbtnRectColumn_CheckedChanged);
            // 
            // ntxtColumnWidth
            // 
            this.ntxtColumnWidth.AutomaticResize = false;
            this.ntxtColumnWidth.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtColumnWidth.DoubleValue = 0D;
            this.ntxtColumnWidth.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtColumnWidth.IntValue = 0;
            this.ntxtColumnWidth.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtColumnWidth.Location = new System.Drawing.Point(96, 76);
            this.ntxtColumnWidth.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtColumnWidth.Name = "ntxtColumnWidth";
            this.ntxtColumnWidth.Size = new System.Drawing.Size(100, 20);
            this.ntxtColumnWidth.SU = 0D;
            this.ntxtColumnWidth.TabIndex = 15;
            this.ntxtColumnWidth.Text = "0";
            // 
            // lblColumnWidth
            // 
            this.lblColumnWidth.AutoSize = true;
            this.lblColumnWidth.Location = new System.Drawing.Point(46, 79);
            this.lblColumnWidth.Name = "lblColumnWidth";
            this.lblColumnWidth.Size = new System.Drawing.Size(35, 13);
            this.lblColumnWidth.TabIndex = 13;
            this.lblColumnWidth.Text = "Width";
            // 
            // lblColumnDepthOrDiam
            // 
            this.lblColumnDepthOrDiam.AutoSize = true;
            this.lblColumnDepthOrDiam.Location = new System.Drawing.Point(46, 49);
            this.lblColumnDepthOrDiam.Name = "lblColumnDepthOrDiam";
            this.lblColumnDepthOrDiam.Size = new System.Drawing.Size(36, 13);
            this.lblColumnDepthOrDiam.TabIndex = 14;
            this.lblColumnDepthOrDiam.Text = "Depth";
            this.lblColumnDepthOrDiam.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ntxtColumnDiamOrDepth
            // 
            this.ntxtColumnDiamOrDepth.AutomaticResize = false;
            this.ntxtColumnDiamOrDepth.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtColumnDiamOrDepth.DoubleValue = 0D;
            this.ntxtColumnDiamOrDepth.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtColumnDiamOrDepth.IntValue = 0;
            this.ntxtColumnDiamOrDepth.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtColumnDiamOrDepth.Location = new System.Drawing.Point(96, 46);
            this.ntxtColumnDiamOrDepth.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtColumnDiamOrDepth.Name = "ntxtColumnDiamOrDepth";
            this.ntxtColumnDiamOrDepth.Size = new System.Drawing.Size(100, 20);
            this.ntxtColumnDiamOrDepth.SU = 0D;
            this.ntxtColumnDiamOrDepth.TabIndex = 16;
            this.ntxtColumnDiamOrDepth.Text = "0";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chkConsiderSelfWeight);
            this.groupBox6.Location = new System.Drawing.Point(307, 166);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(226, 50);
            this.groupBox6.TabIndex = 18;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Load";
            // 
            // chkConsiderSelfWeight
            // 
            this.chkConsiderSelfWeight.AutoSize = true;
            this.chkConsiderSelfWeight.Checked = true;
            this.chkConsiderSelfWeight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConsiderSelfWeight.Location = new System.Drawing.Point(21, 20);
            this.chkConsiderSelfWeight.Name = "chkConsiderSelfWeight";
            this.chkConsiderSelfWeight.Size = new System.Drawing.Size(120, 17);
            this.chkConsiderSelfWeight.TabIndex = 18;
            this.chkConsiderSelfWeight.Text = "Consider self weight";
            this.chkConsiderSelfWeight.UseVisualStyleBackColor = true;
            // 
            // eNewSlabDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(542, 375);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eNewSlabDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Slab";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gpxUseUniformGrid.ResumeLayout(false);
            this.gpxUseUniformGrid.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnEditGridData;
        private GUI.Controls.eNumericTextBox ntxtNumV_Grids;
        private GUI.Controls.eNumericTextBox ntxtNumH_Grids;
        private GUI.Controls.eNumericTextBox ntxtXGridSpacing;
        private GUI.Controls.eNumericTextBox ntxtYGridSpacing;
        private System.Windows.Forms.Label lblXGridSpacing;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblYGridSpacing;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkUseMinReqDepth;
        private GUI.Controls.eNumericTextBox ntxtGrossDepth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxForce;
        private System.Windows.Forms.ComboBox cbxLength;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox2;
        private GUI.Controls.eNumericTextBox ntxtBeamDepth;
        private GUI.Controls.eNumericTextBox ntxtBeamWidth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private GUI.Controls.eNumericTextBox ntxtColumnDiamOrDepth;
        private GUI.Controls.eNumericTextBox ntxtColumnWidth;
        private System.Windows.Forms.Label lblColumnDepthOrDiam;
        private System.Windows.Forms.Label lblColumnWidth;
        private System.Windows.Forms.RadioButton radCircularColumn;
        private System.Windows.Forms.RadioButton radRectColumn;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox chkConsiderSelfWeight;
        private System.Windows.Forms.RadioButton radCustomGridSpacing;
        private System.Windows.Forms.RadioButton radUniformGridSpacing;
        private System.Windows.Forms.GroupBox gpxUseUniformGrid;
    }
}