namespace ESADS.GUI
{
    partial class eNewColumnDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(eNewColumnDialog));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ntxtWidth = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxDepth = new ESADS.GUI.Controls.eNumericTextBox();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblDepth = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxSteel = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbxCocrete = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ntxtMy = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtMx = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtNsd = new ESADS.GUI.Controls.eNumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gbxEffectiveDims = new System.Windows.Forms.GroupBox();
            this.ntxtBprim = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtHprim = new ESADS.GUI.Controls.eNumericTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbtnUniaxial = new System.Windows.Forms.RadioButton();
            this.rbtnBiaxial = new System.Windows.Forms.RadioButton();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.rbtnOnlyDesn = new System.Windows.Forms.RadioButton();
            this.rbtnDesnAndDtl = new System.Windows.Forms.RadioButton();
            this.btnReinfDistribution = new System.Windows.Forms.Button();
            this.tpDetailing = new System.Windows.Forms.TabPage();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.ntxtDepthTolerance = new ESADS.GUI.Controls.eNumericTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtMinMaxSpacing = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtMaxAgrtSize = new ESADS.GUI.Controls.eNumericTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxTiesDiam = new System.Windows.Forms.ComboBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.cbxMaxDiam = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxMinDiam = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxMainBarDiam = new System.Windows.Forms.ComboBox();
            this.rbtnBarDiam = new System.Windows.Forms.RadioButton();
            this.rbtnEconomicalBarDiam = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtCover = new ESADS.GUI.Controls.eNumericTextBox();
            this.cbxExposureType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.pbxPreview = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbxLengthUnit = new System.Windows.Forms.ComboBox();
            this.cbxForceUnit = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbxEffectiveDims.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.tpDetailing.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPreview)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tpDetailing);
            this.tabControl1.Location = new System.Drawing.Point(7, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(417, 333);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.gbxEffectiveDims);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox11);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(409, 307);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ntxtWidth);
            this.groupBox3.Controls.Add(this.ntxDepth);
            this.groupBox3.Controls.Add(this.lblWidth);
            this.groupBox3.Controls.Add(this.lblDepth);
            this.groupBox3.Location = new System.Drawing.Point(7, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(237, 79);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Dimensions";
            // 
            // ntxtWidth
            // 
            this.ntxtWidth.AutomaticResize = false;
            this.ntxtWidth.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtWidth.DoubleValue = 0D;
            this.ntxtWidth.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtWidth.IntValue = 0;
            this.ntxtWidth.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtWidth.Location = new System.Drawing.Point(115, 44);
            this.ntxtWidth.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtWidth.Name = "ntxtWidth";
            this.ntxtWidth.Size = new System.Drawing.Size(90, 20);
            this.ntxtWidth.SU = 0D;
            this.ntxtWidth.TabIndex = 14;
            this.ntxtWidth.Text = "0";
            // 
            // ntxDepth
            // 
            this.ntxDepth.AutomaticResize = false;
            this.ntxDepth.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxDepth.DoubleValue = 0D;
            this.ntxDepth.ForceUnit = ESADS.eForceUints.KN;
            this.ntxDepth.IntValue = 0;
            this.ntxDepth.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxDepth.Location = new System.Drawing.Point(115, 18);
            this.ntxDepth.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxDepth.Name = "ntxDepth";
            this.ntxDepth.Size = new System.Drawing.Size(90, 20);
            this.ntxDepth.SU = 0D;
            this.ntxDepth.TabIndex = 13;
            this.ntxDepth.Text = "0";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(63, 47);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(47, 13);
            this.lblWidth.TabIndex = 9;
            this.lblWidth.Text = "Width(b)";
            // 
            // lblDepth
            // 
            this.lblDepth.AutoSize = true;
            this.lblDepth.Location = new System.Drawing.Point(63, 21);
            this.lblDepth.Name = "lblDepth";
            this.lblDepth.Size = new System.Drawing.Size(48, 13);
            this.lblDepth.TabIndex = 8;
            this.lblDepth.Text = "Depth(h)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxSteel);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.cbxCocrete);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(250, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(149, 103);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Materials";
            // 
            // cbxSteel
            // 
            this.cbxSteel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSteel.FormattingEnabled = true;
            this.cbxSteel.Location = new System.Drawing.Point(64, 56);
            this.cbxSteel.Name = "cbxSteel";
            this.cbxSteel.Size = new System.Drawing.Size(66, 21);
            this.cbxSteel.TabIndex = 16;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(25, 59);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 13);
            this.label14.TabIndex = 15;
            this.label14.Text = "Steel";
            // 
            // cbxCocrete
            // 
            this.cbxCocrete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCocrete.FormattingEnabled = true;
            this.cbxCocrete.Location = new System.Drawing.Point(64, 20);
            this.cbxCocrete.Name = "cbxCocrete";
            this.cbxCocrete.Size = new System.Drawing.Size(66, 21);
            this.cbxCocrete.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Concrete";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ntxtMy);
            this.groupBox1.Controls.Add(this.ntxtMx);
            this.groupBox1.Controls.Add(this.ntxtNsd);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(7, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 103);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Loading";
            // 
            // ntxtMy
            // 
            this.ntxtMy.AutomaticResize = false;
            this.ntxtMy.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtMy.DoubleValue = 0D;
            this.ntxtMy.Enabled = false;
            this.ntxtMy.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtMy.IntValue = 0;
            this.ntxtMy.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtMy.Location = new System.Drawing.Point(114, 71);
            this.ntxtMy.Measurment = ESADS.GUI.Controls.eMeasurment.Moment;
            this.ntxtMy.Name = "ntxtMy";
            this.ntxtMy.Size = new System.Drawing.Size(90, 20);
            this.ntxtMy.SU = 0D;
            this.ntxtMy.TabIndex = 18;
            this.ntxtMy.Text = "0";
            // 
            // ntxtMx
            // 
            this.ntxtMx.AutomaticResize = false;
            this.ntxtMx.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtMx.DoubleValue = 0D;
            this.ntxtMx.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtMx.IntValue = 0;
            this.ntxtMx.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtMx.Location = new System.Drawing.Point(114, 43);
            this.ntxtMx.Measurment = ESADS.GUI.Controls.eMeasurment.Moment;
            this.ntxtMx.Name = "ntxtMx";
            this.ntxtMx.Size = new System.Drawing.Size(90, 20);
            this.ntxtMx.SU = 0D;
            this.ntxtMx.TabIndex = 17;
            this.ntxtMx.Text = "0";
            // 
            // ntxtNsd
            // 
            this.ntxtNsd.AutomaticResize = false;
            this.ntxtNsd.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtNsd.DoubleValue = 0D;
            this.ntxtNsd.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtNsd.IntValue = 0;
            this.ntxtNsd.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtNsd.Location = new System.Drawing.Point(114, 17);
            this.ntxtNsd.Measurment = ESADS.GUI.Controls.eMeasurment.Force;
            this.ntxtNsd.Name = "ntxtNsd";
            this.ntxtNsd.Size = new System.Drawing.Size(90, 20);
            this.ntxtNsd.SU = 0D;
            this.ntxtNsd.TabIndex = 15;
            this.ntxtNsd.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Y-Moment  (My)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "X-Moment  (Mx)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Axial Force (Nsd)";
            // 
            // gbxEffectiveDims
            // 
            this.gbxEffectiveDims.Controls.Add(this.ntxtBprim);
            this.gbxEffectiveDims.Controls.Add(this.ntxtHprim);
            this.gbxEffectiveDims.Controls.Add(this.label16);
            this.gbxEffectiveDims.Controls.Add(this.label15);
            this.gbxEffectiveDims.Enabled = false;
            this.gbxEffectiveDims.Location = new System.Drawing.Point(250, 197);
            this.gbxEffectiveDims.Name = "gbxEffectiveDims";
            this.gbxEffectiveDims.Size = new System.Drawing.Size(149, 98);
            this.gbxEffectiveDims.TabIndex = 11;
            this.gbxEffectiveDims.TabStop = false;
            this.gbxEffectiveDims.Text = "Effective Dimensions";
            // 
            // ntxtBprim
            // 
            this.ntxtBprim.AutomaticResize = false;
            this.ntxtBprim.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtBprim.DoubleValue = 0D;
            this.ntxtBprim.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtBprim.IntValue = 0;
            this.ntxtBprim.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtBprim.Location = new System.Drawing.Point(58, 57);
            this.ntxtBprim.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtBprim.Name = "ntxtBprim";
            this.ntxtBprim.Size = new System.Drawing.Size(72, 20);
            this.ntxtBprim.SU = 0D;
            this.ntxtBprim.TabIndex = 19;
            this.ntxtBprim.Text = "0";
            // 
            // ntxtHprim
            // 
            this.ntxtHprim.AutomaticResize = false;
            this.ntxtHprim.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtHprim.DoubleValue = 0D;
            this.ntxtHprim.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtHprim.IntValue = 0;
            this.ntxtHprim.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtHprim.Location = new System.Drawing.Point(58, 27);
            this.ntxtHprim.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtHprim.Name = "ntxtHprim";
            this.ntxtHprim.Size = new System.Drawing.Size(72, 20);
            this.ntxtHprim.SU = 0D;
            this.ntxtHprim.TabIndex = 18;
            this.ntxtHprim.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(34, 60);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "b\'";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(34, 30);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(15, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "h\'";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbtnUniaxial);
            this.groupBox4.Controls.Add(this.rbtnBiaxial);
            this.groupBox4.Location = new System.Drawing.Point(250, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(149, 79);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Behaviour";
            // 
            // rbtnUniaxial
            // 
            this.rbtnUniaxial.AutoSize = true;
            this.rbtnUniaxial.Checked = true;
            this.rbtnUniaxial.Location = new System.Drawing.Point(42, 17);
            this.rbtnUniaxial.Name = "rbtnUniaxial";
            this.rbtnUniaxial.Size = new System.Drawing.Size(62, 17);
            this.rbtnUniaxial.TabIndex = 3;
            this.rbtnUniaxial.TabStop = true;
            this.rbtnUniaxial.Text = "Uniaxial";
            this.rbtnUniaxial.UseVisualStyleBackColor = true;
            this.rbtnUniaxial.CheckedChanged += new System.EventHandler(this.rbtnUniaxialInXdxn_CheckedChanged);
            // 
            // rbtnBiaxial
            // 
            this.rbtnBiaxial.AutoSize = true;
            this.rbtnBiaxial.Location = new System.Drawing.Point(42, 47);
            this.rbtnBiaxial.Name = "rbtnBiaxial";
            this.rbtnBiaxial.Size = new System.Drawing.Size(55, 17);
            this.rbtnBiaxial.TabIndex = 2;
            this.rbtnBiaxial.Text = "Biaxial";
            this.rbtnBiaxial.UseVisualStyleBackColor = true;
            this.rbtnBiaxial.CheckedChanged += new System.EventHandler(this.rbtnBiaxial_CheckedChanged);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.rbtnOnlyDesn);
            this.groupBox11.Controls.Add(this.rbtnDesnAndDtl);
            this.groupBox11.Controls.Add(this.btnReinfDistribution);
            this.groupBox11.Location = new System.Drawing.Point(7, 197);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(237, 98);
            this.groupBox11.TabIndex = 4;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Options";
            // 
            // rbtnOnlyDesn
            // 
            this.rbtnOnlyDesn.AutoSize = true;
            this.rbtnOnlyDesn.Location = new System.Drawing.Point(132, 26);
            this.rbtnOnlyDesn.Name = "rbtnOnlyDesn";
            this.rbtnOnlyDesn.Size = new System.Drawing.Size(85, 17);
            this.rbtnOnlyDesn.TabIndex = 17;
            this.rbtnOnlyDesn.Text = "Only Design ";
            this.rbtnOnlyDesn.UseVisualStyleBackColor = true;
            this.rbtnOnlyDesn.CheckedChanged += new System.EventHandler(this.rbtnOnlyDesn_CheckedChanged);
            // 
            // rbtnDesnAndDtl
            // 
            this.rbtnDesnAndDtl.AutoSize = true;
            this.rbtnDesnAndDtl.Checked = true;
            this.rbtnDesnAndDtl.Location = new System.Drawing.Point(17, 26);
            this.rbtnDesnAndDtl.Name = "rbtnDesnAndDtl";
            this.rbtnDesnAndDtl.Size = new System.Drawing.Size(109, 17);
            this.rbtnDesnAndDtl.TabIndex = 16;
            this.rbtnDesnAndDtl.TabStop = true;
            this.rbtnDesnAndDtl.Text = "Design and Detail";
            this.rbtnDesnAndDtl.UseVisualStyleBackColor = true;
            // 
            // btnReinfDistribution
            // 
            this.btnReinfDistribution.Location = new System.Drawing.Point(17, 54);
            this.btnReinfDistribution.Name = "btnReinfDistribution";
            this.btnReinfDistribution.Size = new System.Drawing.Size(153, 25);
            this.btnReinfDistribution.TabIndex = 15;
            this.btnReinfDistribution.Text = "Reinforcement Distribution...";
            this.btnReinfDistribution.UseVisualStyleBackColor = true;
            this.btnReinfDistribution.Click += new System.EventHandler(this.btnReinfDistribution_Click);
            // 
            // tpDetailing
            // 
            this.tpDetailing.Controls.Add(this.groupBox13);
            this.tpDetailing.Controls.Add(this.groupBox9);
            this.tpDetailing.Controls.Add(this.groupBox8);
            this.tpDetailing.Controls.Add(this.groupBox7);
            this.tpDetailing.Location = new System.Drawing.Point(4, 22);
            this.tpDetailing.Name = "tpDetailing";
            this.tpDetailing.Padding = new System.Windows.Forms.Padding(3);
            this.tpDetailing.Size = new System.Drawing.Size(409, 307);
            this.tpDetailing.TabIndex = 1;
            this.tpDetailing.Text = "Detailing";
            this.tpDetailing.UseVisualStyleBackColor = true;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.ntxtDepthTolerance);
            this.groupBox13.Controls.Add(this.label17);
            this.groupBox13.Controls.Add(this.txtMinMaxSpacing);
            this.groupBox13.Controls.Add(this.ntxtMaxAgrtSize);
            this.groupBox13.Controls.Add(this.label12);
            this.groupBox13.Controls.Add(this.label13);
            this.groupBox13.Location = new System.Drawing.Point(240, 95);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(163, 194);
            this.groupBox13.TabIndex = 19;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Others";
            // 
            // ntxtDepthTolerance
            // 
            this.ntxtDepthTolerance.AutomaticResize = false;
            this.ntxtDepthTolerance.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtDepthTolerance.DoubleValue = 0D;
            this.ntxtDepthTolerance.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtDepthTolerance.IntValue = 0;
            this.ntxtDepthTolerance.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtDepthTolerance.Location = new System.Drawing.Point(69, 156);
            this.ntxtDepthTolerance.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtDepthTolerance.Name = "ntxtDepthTolerance";
            this.ntxtDepthTolerance.Size = new System.Drawing.Size(62, 20);
            this.ntxtDepthTolerance.SU = 0D;
            this.ntxtDepthTolerance.TabIndex = 20;
            this.ntxtDepthTolerance.Text = "0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(38, 135);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(90, 13);
            this.label17.TabIndex = 19;
            this.label17.Text = "Depth Tolerance:";
            // 
            // txtMinMaxSpacing
            // 
            this.txtMinMaxSpacing.AutomaticResize = false;
            this.txtMinMaxSpacing.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.txtMinMaxSpacing.DoubleValue = 0D;
            this.txtMinMaxSpacing.ForceUnit = ESADS.eForceUints.KN;
            this.txtMinMaxSpacing.IntValue = 0;
            this.txtMinMaxSpacing.LengthUnit = ESADS.eLengthUnits.m;
            this.txtMinMaxSpacing.Location = new System.Drawing.Point(70, 100);
            this.txtMinMaxSpacing.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.txtMinMaxSpacing.Name = "txtMinMaxSpacing";
            this.txtMinMaxSpacing.Size = new System.Drawing.Size(62, 20);
            this.txtMinMaxSpacing.SU = 0D;
            this.txtMinMaxSpacing.TabIndex = 18;
            this.txtMinMaxSpacing.Text = "0";
            // 
            // ntxtMaxAgrtSize
            // 
            this.ntxtMaxAgrtSize.AutomaticResize = false;
            this.ntxtMaxAgrtSize.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtMaxAgrtSize.DoubleValue = 0D;
            this.ntxtMaxAgrtSize.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtMaxAgrtSize.IntValue = 0;
            this.ntxtMaxAgrtSize.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtMaxAgrtSize.Location = new System.Drawing.Point(65, 42);
            this.ntxtMaxAgrtSize.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtMaxAgrtSize.Name = "ntxtMaxAgrtSize";
            this.ntxtMaxAgrtSize.Size = new System.Drawing.Size(67, 20);
            this.ntxtMaxAgrtSize.SU = 0D;
            this.ntxtMaxAgrtSize.TabIndex = 17;
            this.ntxtMaxAgrtSize.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(39, 79);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Minimum Spacing:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(27, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(105, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "Max Aggregate Size:";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label1);
            this.groupBox9.Controls.Add(this.cbxTiesDiam);
            this.groupBox9.Location = new System.Drawing.Point(240, 6);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(163, 83);
            this.groupBox9.TabIndex = 8;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Ties";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Diameter";
            // 
            // cbxTiesDiam
            // 
            this.cbxTiesDiam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTiesDiam.FormattingEnabled = true;
            this.cbxTiesDiam.Location = new System.Drawing.Point(74, 34);
            this.cbxTiesDiam.Name = "cbxTiesDiam";
            this.cbxTiesDiam.Size = new System.Drawing.Size(57, 21);
            this.cbxTiesDiam.TabIndex = 5;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.cbxMaxDiam);
            this.groupBox8.Controls.Add(this.label5);
            this.groupBox8.Controls.Add(this.cbxMinDiam);
            this.groupBox8.Controls.Add(this.label2);
            this.groupBox8.Controls.Add(this.cbxMainBarDiam);
            this.groupBox8.Controls.Add(this.rbtnBarDiam);
            this.groupBox8.Controls.Add(this.rbtnEconomicalBarDiam);
            this.groupBox8.Location = new System.Drawing.Point(6, 95);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(228, 194);
            this.groupBox8.TabIndex = 7;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Main bar diameter";
            // 
            // cbxMaxDiam
            // 
            this.cbxMaxDiam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMaxDiam.FormattingEnabled = true;
            this.cbxMaxDiam.Location = new System.Drawing.Point(158, 95);
            this.cbxMaxDiam.Name = "cbxMaxDiam";
            this.cbxMaxDiam.Size = new System.Drawing.Size(56, 21);
            this.cbxMaxDiam.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(51, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Maximum Diameter";
            // 
            // cbxMinDiam
            // 
            this.cbxMinDiam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMinDiam.FormattingEnabled = true;
            this.cbxMinDiam.Location = new System.Drawing.Point(158, 54);
            this.cbxMinDiam.Name = "cbxMinDiam";
            this.cbxMinDiam.Size = new System.Drawing.Size(56, 21);
            this.cbxMinDiam.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Minimum Diameter";
            // 
            // cbxMainBarDiam
            // 
            this.cbxMainBarDiam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMainBarDiam.Enabled = false;
            this.cbxMainBarDiam.FormattingEnabled = true;
            this.cbxMainBarDiam.Location = new System.Drawing.Point(159, 136);
            this.cbxMainBarDiam.Name = "cbxMainBarDiam";
            this.cbxMainBarDiam.Size = new System.Drawing.Size(56, 21);
            this.cbxMainBarDiam.TabIndex = 10;
            // 
            // rbtnBarDiam
            // 
            this.rbtnBarDiam.AutoSize = true;
            this.rbtnBarDiam.Location = new System.Drawing.Point(18, 140);
            this.rbtnBarDiam.Name = "rbtnBarDiam";
            this.rbtnBarDiam.Size = new System.Drawing.Size(105, 17);
            this.rbtnBarDiam.TabIndex = 9;
            this.rbtnBarDiam.Text = "Use bar diameter";
            this.rbtnBarDiam.UseVisualStyleBackColor = true;
            this.rbtnBarDiam.CheckedChanged += new System.EventHandler(this.rbtnBarDiam_CheckedChanged);
            // 
            // rbtnEconomicalBarDiam
            // 
            this.rbtnEconomicalBarDiam.AutoSize = true;
            this.rbtnEconomicalBarDiam.BackColor = System.Drawing.Color.Transparent;
            this.rbtnEconomicalBarDiam.Checked = true;
            this.rbtnEconomicalBarDiam.Location = new System.Drawing.Point(18, 19);
            this.rbtnEconomicalBarDiam.Name = "rbtnEconomicalBarDiam";
            this.rbtnEconomicalBarDiam.Size = new System.Drawing.Size(168, 17);
            this.rbtnEconomicalBarDiam.TabIndex = 8;
            this.rbtnEconomicalBarDiam.TabStop = true;
            this.rbtnEconomicalBarDiam.Text = "Take economical bar diameter";
            this.rbtnEconomicalBarDiam.UseVisualStyleBackColor = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtCover);
            this.groupBox7.Controls.Add(this.cbxExposureType);
            this.groupBox7.Controls.Add(this.label11);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Location = new System.Drawing.Point(5, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(229, 83);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Concrete Cover";
            // 
            // txtCover
            // 
            this.txtCover.AutomaticResize = false;
            this.txtCover.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.txtCover.DoubleValue = 0D;
            this.txtCover.ForceUnit = ESADS.eForceUints.KN;
            this.txtCover.IntValue = 0;
            this.txtCover.LengthUnit = ESADS.eLengthUnits.m;
            this.txtCover.Location = new System.Drawing.Point(96, 47);
            this.txtCover.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.txtCover.Name = "txtCover";
            this.txtCover.Size = new System.Drawing.Size(115, 20);
            this.txtCover.SU = 0D;
            this.txtCover.TabIndex = 5;
            this.txtCover.Text = "0";
            // 
            // cbxExposureType
            // 
            this.cbxExposureType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxExposureType.FormattingEnabled = true;
            this.cbxExposureType.Location = new System.Drawing.Point(96, 19);
            this.cbxExposureType.Name = "cbxExposureType";
            this.cbxExposureType.Size = new System.Drawing.Size(115, 21);
            this.cbxExposureType.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(45, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Cover";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Exposure type";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.pbxPreview);
            this.groupBox10.Location = new System.Drawing.Point(430, 16);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(238, 320);
            this.groupBox10.TabIndex = 3;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Preview";
            // 
            // pbxPreview
            // 
            this.pbxPreview.Image = ((System.Drawing.Image)(resources.GetObject("pbxPreview.Image")));
            this.pbxPreview.Location = new System.Drawing.Point(6, 19);
            this.pbxPreview.Name = "pbxPreview";
            this.pbxPreview.Size = new System.Drawing.Size(220, 267);
            this.pbxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxPreview.TabIndex = 0;
            this.pbxPreview.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.cbxLengthUnit);
            this.groupBox5.Controls.Add(this.cbxForceUnit);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Location = new System.Drawing.Point(6, 340);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(251, 57);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Units";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(148, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Force";
            // 
            // cbxLengthUnit
            // 
            this.cbxLengthUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLengthUnit.FormattingEnabled = true;
            this.cbxLengthUnit.Location = new System.Drawing.Point(57, 22);
            this.cbxLengthUnit.Name = "cbxLengthUnit";
            this.cbxLengthUnit.Size = new System.Drawing.Size(43, 21);
            this.cbxLengthUnit.TabIndex = 6;
            this.cbxLengthUnit.SelectedIndexChanged += new System.EventHandler(this.cbxLengthUnit_SelectedIndexChanged);
            // 
            // cbxForceUnit
            // 
            this.cbxForceUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxForceUnit.FormattingEnabled = true;
            this.cbxForceUnit.Location = new System.Drawing.Point(188, 22);
            this.cbxForceUnit.Name = "cbxForceUnit";
            this.cbxForceUnit.Size = new System.Drawing.Size(42, 21);
            this.cbxForceUnit.TabIndex = 7;
            this.cbxForceUnit.SelectedIndexChanged += new System.EventHandler(this.cbxForceUnit_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Length";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(588, 365);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(489, 365);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // eNewColumnDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(675, 411);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eNewColumnDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Column Design Input";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbxEffectiveDims.ResumeLayout(false);
            this.gbxEffectiveDims.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.tpDetailing.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxPreview)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbtnUniaxial;
        private System.Windows.Forms.RadioButton rbtnBiaxial;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabPage tpDetailing;
        private System.Windows.Forms.GroupBox gbxEffectiveDims;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbxForceUnit;
        private System.Windows.Forms.ComboBox cbxLengthUnit;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblDepth;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button btnReinfDistribution;
        private GUI.Controls.eNumericTextBox ntxtWidth;
        private GUI.Controls.eNumericTextBox ntxDepth;
        private System.Windows.Forms.GroupBox groupBox1;
        private GUI.Controls.eNumericTextBox ntxtMy;
        private GUI.Controls.eNumericTextBox ntxtMx;
        private GUI.Controls.eNumericTextBox ntxtNsd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbxSteel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbxCocrete;
        private System.Windows.Forms.Label label7;
        private Controls.eNumericTextBox ntxtHprim;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.PictureBox pbxPreview;
        private System.Windows.Forms.RadioButton rbtnOnlyDesn;
        private System.Windows.Forms.RadioButton rbtnDesnAndDtl;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxTiesDiam;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox cbxMaxDiam;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxMinDiam;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox7;
        private Controls.eNumericTextBox txtCover;
        private System.Windows.Forms.ComboBox cbxExposureType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox13;
        private Controls.eNumericTextBox txtMinMaxSpacing;
        private Controls.eNumericTextBox ntxtMaxAgrtSize;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbxMainBarDiam;
        private System.Windows.Forms.RadioButton rbtnBarDiam;
        private System.Windows.Forms.RadioButton rbtnEconomicalBarDiam;
        private Controls.eNumericTextBox ntxtBprim;
        private Controls.eNumericTextBox ntxtDepthTolerance;
        private System.Windows.Forms.Label label17;
    }
}