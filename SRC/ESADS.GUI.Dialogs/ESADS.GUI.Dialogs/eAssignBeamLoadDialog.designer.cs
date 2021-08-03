namespace ESADS.GUI
{
    partial class eAssignBeamLoadDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(eAssignBeamLoadDialog));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radRemoveExisting = new System.Windows.Forms.RadioButton();
            this.radReplaceExisting = new System.Windows.Forms.RadioButton();
            this.radAddToExisting = new System.Windows.Forms.RadioButton();
            this.grpbxLoadType = new System.Windows.Forms.GroupBox();
            this.pbxMoment = new System.Windows.Forms.PictureBox();
            this.pbxTrapizoidal = new System.Windows.Forms.PictureBox();
            this.pbxTriagular = new System.Windows.Forms.PictureBox();
            this.pbxDistributed = new System.Windows.Forms.PictureBox();
            this.pbxConcentratedForce = new System.Windows.Forms.PictureBox();
            this.grpbxPreview = new System.Windows.Forms.GroupBox();
            this.lblPrevEnd = new System.Windows.Forms.Label();
            this.lblPrevStart = new System.Windows.Forms.Label();
            this.lblPrevMiddleLength = new System.Windows.Forms.Label();
            this.lblPrevMagnitude_right = new System.Windows.Forms.Label();
            this.lblPrevMagnitude = new System.Windows.Forms.Label();
            this.picbxPreview = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxForceUnit = new System.Windows.Forms.ComboBox();
            this.cbxLengthUnit = new System.Windows.Forms.ComboBox();
            this.gbxTrapizoidal = new System.Windows.Forms.GroupBox();
            this.ntxtStart_Trap = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtEnd_Trap = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtMag_2_Trap = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtMag_1_Trap = new ESADS.GUI.Controls.eNumericTextBox();
            this.lblX2 = new System.Windows.Forms.Label();
            this.lblX1 = new System.Windows.Forms.Label();
            this.lblW2 = new System.Windows.Forms.Label();
            this.lblW1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAssign = new System.Windows.Forms.Button();
            this.gbxOption = new System.Windows.Forms.GroupBox();
            this.radRightToLeft = new System.Windows.Forms.RadioButton();
            this.radLeftToRight = new System.Windows.Forms.RadioButton();
            this.gbxConcentrated = new System.Windows.Forms.GroupBox();
            this.ntxtMag_Conc = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtStart_Conc = new ESADS.GUI.Controls.eNumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.gbxDistributedandTraigular = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ntxtMag_Dist = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtStart_Dist = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtEnd_Dist = new ESADS.GUI.Controls.eNumericTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxLoadType = new System.Windows.Forms.ComboBox();
            this.chkFactored = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpLoadDistanceMeasurement = new System.Windows.Forms.GroupBox();
            this.radAbsoluteDimension = new System.Windows.Forms.RadioButton();
            this.radRelativeDimension = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            this.grpbxLoadType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxMoment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxTrapizoidal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxTriagular)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDistributed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxConcentratedForce)).BeginInit();
            this.grpbxPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbxPreview)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.gbxTrapizoidal.SuspendLayout();
            this.gbxOption.SuspendLayout();
            this.gbxConcentrated.SuspendLayout();
            this.gbxDistributedandTraigular.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpLoadDistanceMeasurement.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radRemoveExisting);
            this.groupBox2.Controls.Add(this.radReplaceExisting);
            this.groupBox2.Controls.Add(this.radAddToExisting);
            this.groupBox2.Location = new System.Drawing.Point(12, 146);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(206, 93);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // radRemoveExisting
            // 
            this.radRemoveExisting.AutoSize = true;
            this.radRemoveExisting.Location = new System.Drawing.Point(29, 65);
            this.radRemoveExisting.Name = "radRemoveExisting";
            this.radRemoveExisting.Size = new System.Drawing.Size(145, 17);
            this.radRemoveExisting.TabIndex = 1;
            this.radRemoveExisting.Text = "Remove All Existing Load";
            this.radRemoveExisting.UseVisualStyleBackColor = true;
            this.radRemoveExisting.CheckedChanged += new System.EventHandler(this.radRemoveExisting_CheckedChanged);
            // 
            // radReplaceExisting
            // 
            this.radReplaceExisting.AutoSize = true;
            this.radReplaceExisting.Location = new System.Drawing.Point(29, 42);
            this.radReplaceExisting.Name = "radReplaceExisting";
            this.radReplaceExisting.Size = new System.Drawing.Size(131, 17);
            this.radReplaceExisting.TabIndex = 1;
            this.radReplaceExisting.Text = "Replace Existing Load";
            this.radReplaceExisting.UseVisualStyleBackColor = true;
            // 
            // radAddToExisting
            // 
            this.radAddToExisting.AutoSize = true;
            this.radAddToExisting.Checked = true;
            this.radAddToExisting.Location = new System.Drawing.Point(29, 19);
            this.radAddToExisting.Name = "radAddToExisting";
            this.radAddToExisting.Size = new System.Drawing.Size(122, 17);
            this.radAddToExisting.TabIndex = 0;
            this.radAddToExisting.TabStop = true;
            this.radAddToExisting.Text = "Add to Existing Load";
            this.radAddToExisting.UseVisualStyleBackColor = true;
            // 
            // grpbxLoadType
            // 
            this.grpbxLoadType.Controls.Add(this.pbxMoment);
            this.grpbxLoadType.Controls.Add(this.pbxTrapizoidal);
            this.grpbxLoadType.Controls.Add(this.pbxTriagular);
            this.grpbxLoadType.Controls.Add(this.pbxDistributed);
            this.grpbxLoadType.Controls.Add(this.pbxConcentratedForce);
            this.grpbxLoadType.Location = new System.Drawing.Point(12, 245);
            this.grpbxLoadType.Name = "grpbxLoadType";
            this.grpbxLoadType.Size = new System.Drawing.Size(516, 91);
            this.grpbxLoadType.TabIndex = 2;
            this.grpbxLoadType.TabStop = false;
            this.grpbxLoadType.Text = "Load Type";
            // 
            // pbxMoment
            // 
            this.pbxMoment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxMoment.Image = ((System.Drawing.Image)(resources.GetObject("pbxMoment.Image")));
            this.pbxMoment.Location = new System.Drawing.Point(77, 19);
            this.pbxMoment.Name = "pbxMoment";
            this.pbxMoment.Size = new System.Drawing.Size(65, 54);
            this.pbxMoment.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxMoment.TabIndex = 12;
            this.pbxMoment.TabStop = false;
            this.pbxMoment.Click += new System.EventHandler(this.pbxMoment_Click);
            // 
            // pbxTrapizoidal
            // 
            this.pbxTrapizoidal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxTrapizoidal.Image = ((System.Drawing.Image)(resources.GetObject("pbxTrapizoidal.Image")));
            this.pbxTrapizoidal.Location = new System.Drawing.Point(399, 19);
            this.pbxTrapizoidal.Name = "pbxTrapizoidal";
            this.pbxTrapizoidal.Size = new System.Drawing.Size(76, 54);
            this.pbxTrapizoidal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxTrapizoidal.TabIndex = 10;
            this.pbxTrapizoidal.TabStop = false;
            this.pbxTrapizoidal.Click += new System.EventHandler(this.pbxTrapizoidal_Click);
            // 
            // pbxTriagular
            // 
            this.pbxTriagular.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxTriagular.Image = ((System.Drawing.Image)(resources.GetObject("pbxTriagular.Image")));
            this.pbxTriagular.Location = new System.Drawing.Point(293, 19);
            this.pbxTriagular.Name = "pbxTriagular";
            this.pbxTriagular.Size = new System.Drawing.Size(83, 54);
            this.pbxTriagular.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxTriagular.TabIndex = 9;
            this.pbxTriagular.TabStop = false;
            this.pbxTriagular.Click += new System.EventHandler(this.pbxTraigular_Click);
            // 
            // pbxDistributed
            // 
            this.pbxDistributed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxDistributed.Image = ((System.Drawing.Image)(resources.GetObject("pbxDistributed.Image")));
            this.pbxDistributed.Location = new System.Drawing.Point(163, 19);
            this.pbxDistributed.Name = "pbxDistributed";
            this.pbxDistributed.Size = new System.Drawing.Size(109, 54);
            this.pbxDistributed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxDistributed.TabIndex = 8;
            this.pbxDistributed.TabStop = false;
            this.pbxDistributed.Click += new System.EventHandler(this.pbxDistributed_Click);
            // 
            // pbxConcentratedForce
            // 
            this.pbxConcentratedForce.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxConcentratedForce.Image = ((System.Drawing.Image)(resources.GetObject("pbxConcentratedForce.Image")));
            this.pbxConcentratedForce.Location = new System.Drawing.Point(10, 19);
            this.pbxConcentratedForce.Name = "pbxConcentratedForce";
            this.pbxConcentratedForce.Size = new System.Drawing.Size(46, 54);
            this.pbxConcentratedForce.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxConcentratedForce.TabIndex = 7;
            this.pbxConcentratedForce.TabStop = false;
            this.pbxConcentratedForce.Click += new System.EventHandler(this.pbxConcentratedForce_Click);
            // 
            // grpbxPreview
            // 
            this.grpbxPreview.Controls.Add(this.lblPrevEnd);
            this.grpbxPreview.Controls.Add(this.lblPrevStart);
            this.grpbxPreview.Controls.Add(this.lblPrevMiddleLength);
            this.grpbxPreview.Controls.Add(this.lblPrevMagnitude_right);
            this.grpbxPreview.Controls.Add(this.lblPrevMagnitude);
            this.grpbxPreview.Controls.Add(this.picbxPreview);
            this.grpbxPreview.Location = new System.Drawing.Point(224, 12);
            this.grpbxPreview.Name = "grpbxPreview";
            this.grpbxPreview.Size = new System.Drawing.Size(304, 227);
            this.grpbxPreview.TabIndex = 3;
            this.grpbxPreview.TabStop = false;
            this.grpbxPreview.Text = "Preview";
            // 
            // lblPrevEnd
            // 
            this.lblPrevEnd.AutoSize = true;
            this.lblPrevEnd.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPrevEnd.ForeColor = System.Drawing.Color.Yellow;
            this.lblPrevEnd.Location = new System.Drawing.Point(223, 128);
            this.lblPrevEnd.Name = "lblPrevEnd";
            this.lblPrevEnd.Size = new System.Drawing.Size(35, 13);
            this.lblPrevEnd.TabIndex = 1;
            this.lblPrevEnd.Text = "label3";
            this.lblPrevEnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPrevStart
            // 
            this.lblPrevStart.AutoSize = true;
            this.lblPrevStart.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPrevStart.ForeColor = System.Drawing.Color.Yellow;
            this.lblPrevStart.Location = new System.Drawing.Point(54, 128);
            this.lblPrevStart.Name = "lblPrevStart";
            this.lblPrevStart.Size = new System.Drawing.Size(35, 13);
            this.lblPrevStart.TabIndex = 1;
            this.lblPrevStart.Text = "label3";
            this.lblPrevStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPrevMiddleLength
            // 
            this.lblPrevMiddleLength.AutoSize = true;
            this.lblPrevMiddleLength.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPrevMiddleLength.ForeColor = System.Drawing.Color.Yellow;
            this.lblPrevMiddleLength.Location = new System.Drawing.Point(143, 129);
            this.lblPrevMiddleLength.Name = "lblPrevMiddleLength";
            this.lblPrevMiddleLength.Size = new System.Drawing.Size(35, 13);
            this.lblPrevMiddleLength.TabIndex = 1;
            this.lblPrevMiddleLength.Text = "label3";
            this.lblPrevMiddleLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPrevMagnitude_right
            // 
            this.lblPrevMagnitude_right.AutoSize = true;
            this.lblPrevMagnitude_right.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPrevMagnitude_right.ForeColor = System.Drawing.Color.Yellow;
            this.lblPrevMagnitude_right.Location = new System.Drawing.Point(223, 47);
            this.lblPrevMagnitude_right.Name = "lblPrevMagnitude_right";
            this.lblPrevMagnitude_right.Size = new System.Drawing.Size(35, 13);
            this.lblPrevMagnitude_right.TabIndex = 1;
            this.lblPrevMagnitude_right.Text = "label3";
            this.lblPrevMagnitude_right.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPrevMagnitude_right.Visible = false;
            // 
            // lblPrevMagnitude
            // 
            this.lblPrevMagnitude.AutoSize = true;
            this.lblPrevMagnitude.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPrevMagnitude.ForeColor = System.Drawing.Color.Yellow;
            this.lblPrevMagnitude.Location = new System.Drawing.Point(205, 109);
            this.lblPrevMagnitude.Name = "lblPrevMagnitude";
            this.lblPrevMagnitude.Size = new System.Drawing.Size(35, 13);
            this.lblPrevMagnitude.TabIndex = 1;
            this.lblPrevMagnitude.Text = "label3";
            this.lblPrevMagnitude.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picbxPreview
            // 
            this.picbxPreview.Image = global::ESADS.GUI.Properties.Resources.trian_negative_r_to_l;
            this.picbxPreview.Location = new System.Drawing.Point(7, 20);
            this.picbxPreview.Name = "picbxPreview";
            this.picbxPreview.Size = new System.Drawing.Size(291, 196);
            this.picbxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbxPreview.TabIndex = 0;
            this.picbxPreview.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.cbxForceUnit);
            this.groupBox5.Controls.Add(this.cbxLengthUnit);
            this.groupBox5.Location = new System.Drawing.Point(12, 88);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(205, 52);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Units";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(111, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Force";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Length";
            // 
            // cbxForceUnit
            // 
            this.cbxForceUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxForceUnit.FormattingEnabled = true;
            this.cbxForceUnit.Location = new System.Drawing.Point(147, 19);
            this.cbxForceUnit.Name = "cbxForceUnit";
            this.cbxForceUnit.Size = new System.Drawing.Size(52, 21);
            this.cbxForceUnit.TabIndex = 7;
            this.cbxForceUnit.SelectedIndexChanged += new System.EventHandler(this.cbxForceUnit_SelectedIndexChanged);
            // 
            // cbxLengthUnit
            // 
            this.cbxLengthUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLengthUnit.FormattingEnabled = true;
            this.cbxLengthUnit.Location = new System.Drawing.Point(51, 19);
            this.cbxLengthUnit.Name = "cbxLengthUnit";
            this.cbxLengthUnit.Size = new System.Drawing.Size(52, 21);
            this.cbxLengthUnit.TabIndex = 6;
            this.cbxLengthUnit.SelectedIndexChanged += new System.EventHandler(this.cbxLengthUnit_SelectedIndexChanged);
            // 
            // gbxTrapizoidal
            // 
            this.gbxTrapizoidal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbxTrapizoidal.Controls.Add(this.ntxtStart_Trap);
            this.gbxTrapizoidal.Controls.Add(this.ntxtEnd_Trap);
            this.gbxTrapizoidal.Controls.Add(this.ntxtMag_2_Trap);
            this.gbxTrapizoidal.Controls.Add(this.ntxtMag_1_Trap);
            this.gbxTrapizoidal.Controls.Add(this.lblX2);
            this.gbxTrapizoidal.Controls.Add(this.lblX1);
            this.gbxTrapizoidal.Controls.Add(this.lblW2);
            this.gbxTrapizoidal.Controls.Add(this.lblW1);
            this.gbxTrapizoidal.Location = new System.Drawing.Point(12, 396);
            this.gbxTrapizoidal.Name = "gbxTrapizoidal";
            this.gbxTrapizoidal.Size = new System.Drawing.Size(319, 94);
            this.gbxTrapizoidal.TabIndex = 10;
            this.gbxTrapizoidal.TabStop = false;
            this.gbxTrapizoidal.Text = "Magnitude and Location";
            // 
            // ntxtStart_Trap
            // 
            this.ntxtStart_Trap.AutomaticResize = false;
            this.ntxtStart_Trap.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtStart_Trap.DoubleValue = 0D;
            this.ntxtStart_Trap.IntValue = 0;
            this.ntxtStart_Trap.Location = new System.Drawing.Point(210, 30);
            this.ntxtStart_Trap.Name = "ntxtStart_Trap";
            this.ntxtStart_Trap.Size = new System.Drawing.Size(100, 20);
            this.ntxtStart_Trap.TabIndex = 18;
            this.ntxtStart_Trap.Text = "0";
            this.ntxtStart_Trap.TextChanged += new System.EventHandler(this.ntxtStart_Trap_TextChanged);
            // 
            // ntxtEnd_Trap
            // 
            this.ntxtEnd_Trap.AutomaticResize = false;
            this.ntxtEnd_Trap.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtEnd_Trap.DoubleValue = 0D;
            this.ntxtEnd_Trap.IntValue = 0;
            this.ntxtEnd_Trap.Location = new System.Drawing.Point(210, 55);
            this.ntxtEnd_Trap.Name = "ntxtEnd_Trap";
            this.ntxtEnd_Trap.Size = new System.Drawing.Size(100, 20);
            this.ntxtEnd_Trap.TabIndex = 17;
            this.ntxtEnd_Trap.Text = "0";
            this.ntxtEnd_Trap.TextChanged += new System.EventHandler(this.ntxtEnd_Trap_TextChanged);
            // 
            // ntxtMag_2_Trap
            // 
            this.ntxtMag_2_Trap.AutomaticResize = false;
            this.ntxtMag_2_Trap.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtMag_2_Trap.DoubleValue = 0D;
            this.ntxtMag_2_Trap.IntValue = 0;
            this.ntxtMag_2_Trap.Location = new System.Drawing.Point(70, 55);
            this.ntxtMag_2_Trap.Name = "ntxtMag_2_Trap";
            this.ntxtMag_2_Trap.Size = new System.Drawing.Size(100, 20);
            this.ntxtMag_2_Trap.TabIndex = 16;
            this.ntxtMag_2_Trap.Text = "0";
            this.ntxtMag_2_Trap.TextChanged += new System.EventHandler(this.ntxtMag_2_Trap_TextChanged);
            // 
            // ntxtMag_1_Trap
            // 
            this.ntxtMag_1_Trap.AutomaticResize = false;
            this.ntxtMag_1_Trap.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtMag_1_Trap.DoubleValue = 0D;
            this.ntxtMag_1_Trap.IntValue = 0;
            this.ntxtMag_1_Trap.Location = new System.Drawing.Point(70, 30);
            this.ntxtMag_1_Trap.Name = "ntxtMag_1_Trap";
            this.ntxtMag_1_Trap.Size = new System.Drawing.Size(100, 20);
            this.ntxtMag_1_Trap.TabIndex = 15;
            this.ntxtMag_1_Trap.Text = "0";
            this.ntxtMag_1_Trap.TextChanged += new System.EventHandler(this.ntxtMag_1_Trap_TextChanged);
            // 
            // lblX2
            // 
            this.lblX2.AutoSize = true;
            this.lblX2.Location = new System.Drawing.Point(185, 59);
            this.lblX2.Name = "lblX2";
            this.lblX2.Size = new System.Drawing.Size(20, 13);
            this.lblX2.TabIndex = 14;
            this.lblX2.Text = "X2";
            // 
            // lblX1
            // 
            this.lblX1.AutoSize = true;
            this.lblX1.Location = new System.Drawing.Point(185, 31);
            this.lblX1.Name = "lblX1";
            this.lblX1.Size = new System.Drawing.Size(20, 13);
            this.lblX1.TabIndex = 12;
            this.lblX1.Text = "X1";
            // 
            // lblW2
            // 
            this.lblW2.AutoSize = true;
            this.lblW2.Location = new System.Drawing.Point(30, 60);
            this.lblW2.Name = "lblW2";
            this.lblW2.Size = new System.Drawing.Size(24, 13);
            this.lblW2.TabIndex = 10;
            this.lblW2.Text = "W2";
            // 
            // lblW1
            // 
            this.lblW1.AutoSize = true;
            this.lblW1.Location = new System.Drawing.Point(30, 31);
            this.lblW1.Name = "lblW1";
            this.lblW1.Size = new System.Drawing.Size(24, 13);
            this.lblW1.TabIndex = 2;
            this.lblW1.Text = "W1";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(452, 503);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnAssign
            // 
            this.btnAssign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAssign.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAssign.Location = new System.Drawing.Point(358, 503);
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.Size = new System.Drawing.Size(75, 23);
            this.btnAssign.TabIndex = 11;
            this.btnAssign.Text = "Assign";
            this.btnAssign.UseVisualStyleBackColor = true;
            this.btnAssign.Click += new System.EventHandler(this.btnAssign_Click);
            // 
            // gbxOption
            // 
            this.gbxOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbxOption.Controls.Add(this.radRightToLeft);
            this.gbxOption.Controls.Add(this.radLeftToRight);
            this.gbxOption.Location = new System.Drawing.Point(337, 396);
            this.gbxOption.Name = "gbxOption";
            this.gbxOption.Size = new System.Drawing.Size(191, 94);
            this.gbxOption.TabIndex = 13;
            this.gbxOption.TabStop = false;
            this.gbxOption.Text = "Orientation";
            // 
            // radRightToLeft
            // 
            this.radRightToLeft.AutoSize = true;
            this.radRightToLeft.Location = new System.Drawing.Point(16, 60);
            this.radRightToLeft.Name = "radRightToLeft";
            this.radRightToLeft.Size = new System.Drawing.Size(83, 17);
            this.radRightToLeft.TabIndex = 9;
            this.radRightToLeft.Text = "Right to Left";
            this.radRightToLeft.UseVisualStyleBackColor = true;
            // 
            // radLeftToRight
            // 
            this.radLeftToRight.AutoSize = true;
            this.radLeftToRight.Checked = true;
            this.radLeftToRight.Location = new System.Drawing.Point(16, 31);
            this.radLeftToRight.Name = "radLeftToRight";
            this.radLeftToRight.Size = new System.Drawing.Size(83, 17);
            this.radLeftToRight.TabIndex = 8;
            this.radLeftToRight.TabStop = true;
            this.radLeftToRight.Text = "Left to Right";
            this.radLeftToRight.UseVisualStyleBackColor = true;
            this.radLeftToRight.CheckedChanged += new System.EventHandler(this.radLeftToRight_CheckedChanged);
            // 
            // gbxConcentrated
            // 
            this.gbxConcentrated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbxConcentrated.Controls.Add(this.ntxtMag_Conc);
            this.gbxConcentrated.Controls.Add(this.ntxtStart_Conc);
            this.gbxConcentrated.Controls.Add(this.label6);
            this.gbxConcentrated.Controls.Add(this.label9);
            this.gbxConcentrated.Location = new System.Drawing.Point(12, 396);
            this.gbxConcentrated.Name = "gbxConcentrated";
            this.gbxConcentrated.Size = new System.Drawing.Size(319, 94);
            this.gbxConcentrated.TabIndex = 15;
            this.gbxConcentrated.TabStop = false;
            this.gbxConcentrated.Text = "Magnitude and Location";
            // 
            // ntxtMag_Conc
            // 
            this.ntxtMag_Conc.AutomaticResize = false;
            this.ntxtMag_Conc.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtMag_Conc.DoubleValue = 0D;
            this.ntxtMag_Conc.IntValue = 0;
            this.ntxtMag_Conc.Location = new System.Drawing.Point(70, 30);
            this.ntxtMag_Conc.Name = "ntxtMag_Conc";
            this.ntxtMag_Conc.Size = new System.Drawing.Size(100, 20);
            this.ntxtMag_Conc.TabIndex = 19;
            this.ntxtMag_Conc.Text = "0";
            this.ntxtMag_Conc.TextChanged += new System.EventHandler(this.ntxtMag_Conc_TextChanged);
            // 
            // ntxtStart_Conc
            // 
            this.ntxtStart_Conc.AutomaticResize = false;
            this.ntxtStart_Conc.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtStart_Conc.DoubleValue = 0D;
            this.ntxtStart_Conc.IntValue = 0;
            this.ntxtStart_Conc.Location = new System.Drawing.Point(210, 30);
            this.ntxtStart_Conc.Name = "ntxtStart_Conc";
            this.ntxtStart_Conc.Size = new System.Drawing.Size(100, 20);
            this.ntxtStart_Conc.TabIndex = 18;
            this.ntxtStart_Conc.Text = "0";
            this.ntxtStart_Conc.TextChanged += new System.EventHandler(this.ntxtStart_Conc_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Magnitude";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(185, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "X1";
            // 
            // gbxDistributedandTraigular
            // 
            this.gbxDistributedandTraigular.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbxDistributedandTraigular.Controls.Add(this.label13);
            this.gbxDistributedandTraigular.Controls.Add(this.ntxtMag_Dist);
            this.gbxDistributedandTraigular.Controls.Add(this.ntxtStart_Dist);
            this.gbxDistributedandTraigular.Controls.Add(this.ntxtEnd_Dist);
            this.gbxDistributedandTraigular.Controls.Add(this.label11);
            this.gbxDistributedandTraigular.Controls.Add(this.label12);
            this.gbxDistributedandTraigular.Location = new System.Drawing.Point(12, 396);
            this.gbxDistributedandTraigular.Name = "gbxDistributedandTraigular";
            this.gbxDistributedandTraigular.Size = new System.Drawing.Size(319, 94);
            this.gbxDistributedandTraigular.TabIndex = 17;
            this.gbxDistributedandTraigular.TabStop = false;
            this.gbxDistributedandTraigular.Text = "Magnitude and Location";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 31);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "Magnitude";
            // 
            // ntxtMag_Dist
            // 
            this.ntxtMag_Dist.AutomaticResize = false;
            this.ntxtMag_Dist.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtMag_Dist.DoubleValue = 0D;
            this.ntxtMag_Dist.IntValue = 0;
            this.ntxtMag_Dist.Location = new System.Drawing.Point(70, 30);
            this.ntxtMag_Dist.Name = "ntxtMag_Dist";
            this.ntxtMag_Dist.Size = new System.Drawing.Size(100, 20);
            this.ntxtMag_Dist.TabIndex = 20;
            this.ntxtMag_Dist.Text = "0";
            this.ntxtMag_Dist.TextChanged += new System.EventHandler(this.ntxtMag_Dist_TextChanged);
            // 
            // ntxtStart_Dist
            // 
            this.ntxtStart_Dist.AutomaticResize = false;
            this.ntxtStart_Dist.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtStart_Dist.DoubleValue = 0D;
            this.ntxtStart_Dist.IntValue = 0;
            this.ntxtStart_Dist.Location = new System.Drawing.Point(210, 30);
            this.ntxtStart_Dist.Name = "ntxtStart_Dist";
            this.ntxtStart_Dist.Size = new System.Drawing.Size(100, 20);
            this.ntxtStart_Dist.TabIndex = 18;
            this.ntxtStart_Dist.Text = "0";
            this.ntxtStart_Dist.TextChanged += new System.EventHandler(this.ntxtStart_Dist_TextChanged);
            // 
            // ntxtEnd_Dist
            // 
            this.ntxtEnd_Dist.AutomaticResize = false;
            this.ntxtEnd_Dist.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtEnd_Dist.DoubleValue = 0D;
            this.ntxtEnd_Dist.IntValue = 0;
            this.ntxtEnd_Dist.Location = new System.Drawing.Point(210, 55);
            this.ntxtEnd_Dist.Name = "ntxtEnd_Dist";
            this.ntxtEnd_Dist.Size = new System.Drawing.Size(100, 20);
            this.ntxtEnd_Dist.TabIndex = 17;
            this.ntxtEnd_Dist.Text = "0";
            this.ntxtEnd_Dist.TextChanged += new System.EventHandler(this.ntxtEnd_Dist_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(185, 59);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "X2";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(185, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(20, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "X1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Action Type";
            // 
            // cbxLoadType
            // 
            this.cbxLoadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLoadType.FormattingEnabled = true;
            this.cbxLoadType.Location = new System.Drawing.Point(79, 20);
            this.cbxLoadType.Name = "cbxLoadType";
            this.cbxLoadType.Size = new System.Drawing.Size(120, 21);
            this.cbxLoadType.TabIndex = 1;
            // 
            // chkFactored
            // 
            this.chkFactored.AutoSize = true;
            this.chkFactored.Location = new System.Drawing.Point(79, 47);
            this.chkFactored.Name = "chkFactored";
            this.chkFactored.Size = new System.Drawing.Size(120, 17);
            this.chkFactored.TabIndex = 18;
            this.chkFactored.Text = "The load is factored";
            this.chkFactored.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkFactored);
            this.groupBox1.Controls.Add(this.cbxLoadType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 70);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Action ";
            // 
            // grpLoadDistanceMeasurement
            // 
            this.grpLoadDistanceMeasurement.Controls.Add(this.radAbsoluteDimension);
            this.grpLoadDistanceMeasurement.Controls.Add(this.radRelativeDimension);
            this.grpLoadDistanceMeasurement.Location = new System.Drawing.Point(12, 342);
            this.grpLoadDistanceMeasurement.Name = "grpLoadDistanceMeasurement";
            this.grpLoadDistanceMeasurement.Size = new System.Drawing.Size(319, 48);
            this.grpLoadDistanceMeasurement.TabIndex = 18;
            this.grpLoadDistanceMeasurement.TabStop = false;
            this.grpLoadDistanceMeasurement.Text = "Load distance measurement";
            // 
            // radAbsoluteDimension
            // 
            this.radAbsoluteDimension.AutoSize = true;
            this.radAbsoluteDimension.Checked = true;
            this.radAbsoluteDimension.Location = new System.Drawing.Point(212, 19);
            this.radAbsoluteDimension.Name = "radAbsoluteDimension";
            this.radAbsoluteDimension.Size = new System.Drawing.Size(63, 17);
            this.radAbsoluteDimension.TabIndex = 0;
            this.radAbsoluteDimension.TabStop = true;
            this.radAbsoluteDimension.Text = "Absolue";
            this.radAbsoluteDimension.UseVisualStyleBackColor = true;
            // 
            // radRelativeDimension
            // 
            this.radRelativeDimension.AutoSize = true;
            this.radRelativeDimension.Location = new System.Drawing.Point(6, 19);
            this.radRelativeDimension.Name = "radRelativeDimension";
            this.radRelativeDimension.Size = new System.Drawing.Size(148, 17);
            this.radRelativeDimension.TabIndex = 0;
            this.radRelativeDimension.TabStop = true;
            this.radRelativeDimension.Text = "Relative to member length";
            this.radRelativeDimension.UseVisualStyleBackColor = true;
            this.radRelativeDimension.CheckedChanged += new System.EventHandler(this.radRelativeDimension_CheckedChanged);
            // 
            // eAssignBeamLoadDialog
            // 
            this.AcceptButton = this.btnAssign;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(539, 538);
            this.Controls.Add(this.grpLoadDistanceMeasurement);
            this.Controls.Add(this.gbxOption);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAssign);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.grpbxPreview);
            this.Controls.Add(this.grpbxLoadType);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbxTrapizoidal);
            this.Controls.Add(this.gbxDistributedandTraigular);
            this.Controls.Add(this.gbxConcentrated);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eAssignBeamLoadDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assign Beam Load";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpbxLoadType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxMoment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxTrapizoidal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxTriagular)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDistributed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxConcentratedForce)).EndInit();
            this.grpbxPreview.ResumeLayout(false);
            this.grpbxPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbxPreview)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.gbxTrapizoidal.ResumeLayout(false);
            this.gbxTrapizoidal.PerformLayout();
            this.gbxOption.ResumeLayout(false);
            this.gbxOption.PerformLayout();
            this.gbxConcentrated.ResumeLayout(false);
            this.gbxConcentrated.PerformLayout();
            this.gbxDistributedandTraigular.ResumeLayout(false);
            this.gbxDistributedandTraigular.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpLoadDistanceMeasurement.ResumeLayout(false);
            this.grpLoadDistanceMeasurement.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radAddToExisting;
        private System.Windows.Forms.RadioButton radReplaceExisting;
        private System.Windows.Forms.GroupBox grpbxLoadType;
        private System.Windows.Forms.GroupBox grpbxPreview;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cbxForceUnit;
        private System.Windows.Forms.ComboBox cbxLengthUnit;
        private System.Windows.Forms.GroupBox gbxTrapizoidal;
        private System.Windows.Forms.Label lblX2;
        private System.Windows.Forms.Label lblX1;
        private System.Windows.Forms.Label lblW2;
        private System.Windows.Forms.Label lblW1;
        private System.Windows.Forms.PictureBox pbxTrapizoidal;
        private System.Windows.Forms.PictureBox pbxTriagular;
        private System.Windows.Forms.PictureBox pbxDistributed;
        private System.Windows.Forms.PictureBox pbxConcentratedForce;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAssign;
        private System.Windows.Forms.GroupBox gbxOption;
        private System.Windows.Forms.RadioButton radRightToLeft;
        private System.Windows.Forms.RadioButton radLeftToRight;
        private System.Windows.Forms.PictureBox pbxMoment;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private GUI.Controls.eNumericTextBox ntxtStart_Trap;
        private GUI.Controls.eNumericTextBox ntxtEnd_Trap;
        private GUI.Controls.eNumericTextBox ntxtMag_2_Trap;
        private GUI.Controls.eNumericTextBox ntxtMag_1_Trap;
        private System.Windows.Forms.GroupBox gbxConcentrated;
        private GUI.Controls.eNumericTextBox ntxtMag_Conc;
        private GUI.Controls.eNumericTextBox ntxtStart_Conc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox gbxDistributedandTraigular;
        private System.Windows.Forms.Label label13;
        private GUI.Controls.eNumericTextBox ntxtMag_Dist;
        private GUI.Controls.eNumericTextBox ntxtStart_Dist;
        private GUI.Controls.eNumericTextBox ntxtEnd_Dist;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxLoadType;
        private System.Windows.Forms.CheckBox chkFactored;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radRemoveExisting;
        private System.Windows.Forms.GroupBox grpLoadDistanceMeasurement;
        private System.Windows.Forms.RadioButton radAbsoluteDimension;
        private System.Windows.Forms.RadioButton radRelativeDimension;
        private System.Windows.Forms.PictureBox picbxPreview;
        private System.Windows.Forms.Label lblPrevMagnitude;
        private System.Windows.Forms.Label lblPrevEnd;
        private System.Windows.Forms.Label lblPrevStart;
        private System.Windows.Forms.Label lblPrevMiddleLength;
        private System.Windows.Forms.Label lblPrevMagnitude_right;
    }
}