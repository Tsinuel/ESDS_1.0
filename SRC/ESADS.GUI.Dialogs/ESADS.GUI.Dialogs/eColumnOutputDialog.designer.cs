namespace ESADS.GUI
{
    partial class eColumnOutputDialog
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
            this.button1 = new System.Windows.Forms.Button();
            this.gbxUniaxial = new System.Windows.Forms.GroupBox();
            this.txtUniCalc = new System.Windows.Forms.Button();
            this.txtM = new ESADS.GUI.Controls.eNumericTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPUni = new ESADS.GUI.Controls.eNumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtEconomy = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAsmin = new System.Windows.Forms.TextBox();
            this.txtAsprovided = new System.Windows.Forms.TextBox();
            this.txtAsmax = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAscalcualted = new System.Windows.Forms.TextBox();
            this.gbxBiaxial = new System.Windows.Forms.GroupBox();
            this.txtMy = new ESADS.GUI.Controls.eNumericTextBox();
            this.txtMx = new ESADS.GUI.Controls.eNumericTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtR = new ESADS.GUI.Controls.eNumericTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPbi = new ESADS.GUI.Controls.eNumericTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnBiCalc = new System.Windows.Forms.Button();
            this.rbtnAscalc = new System.Windows.Forms.RadioButton();
            this.rbtnAsprov = new System.Windows.Forms.RadioButton();
            this.cbxLengthUnit = new System.Windows.Forms.ComboBox();
            this.cbxForceUnit = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPb = new ESADS.GUI.Controls.eNumericTextBox();
            this.txtMpure = new ESADS.GUI.Controls.eNumericTextBox();
            this.txtPpure = new ESADS.GUI.Controls.eNumericTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtMb = new ESADS.GUI.Controls.eNumericTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblAction = new System.Windows.Forms.Label();
            this.ntxtX = new ESADS.GUI.Controls.eNumericTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtTeta = new ESADS.GUI.Controls.eNumericTextBox();
            this.txtXbi = new ESADS.GUI.Controls.eNumericTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.gbxUniaxial.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbxBiaxial.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(378, 479);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gbxUniaxial
            // 
            this.gbxUniaxial.Controls.Add(this.ntxtX);
            this.gbxUniaxial.Controls.Add(this.label18);
            this.gbxUniaxial.Controls.Add(this.txtUniCalc);
            this.gbxUniaxial.Controls.Add(this.txtM);
            this.gbxUniaxial.Controls.Add(this.label7);
            this.gbxUniaxial.Controls.Add(this.txtPUni);
            this.gbxUniaxial.Controls.Add(this.label6);
            this.gbxUniaxial.Location = new System.Drawing.Point(12, 359);
            this.gbxUniaxial.Name = "gbxUniaxial";
            this.gbxUniaxial.Size = new System.Drawing.Size(438, 84);
            this.gbxUniaxial.TabIndex = 1;
            this.gbxUniaxial.TabStop = false;
            this.gbxUniaxial.Text = " Load Interation";
            // 
            // txtUniCalc
            // 
            this.txtUniCalc.Location = new System.Drawing.Point(334, 27);
            this.txtUniCalc.Name = "txtUniCalc";
            this.txtUniCalc.Size = new System.Drawing.Size(75, 23);
            this.txtUniCalc.TabIndex = 7;
            this.txtUniCalc.Text = "Calculate";
            this.txtUniCalc.UseVisualStyleBackColor = true;
            this.txtUniCalc.Click += new System.EventHandler(this.txtUniCalc_Click);
            // 
            // txtM
            // 
            this.txtM.AutomaticResize = false;
            this.txtM.BackColor = System.Drawing.SystemColors.Window;
            this.txtM.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.txtM.DoubleValue = 0D;
            this.txtM.ForceUnit = ESADS.eForceUints.KN;
            this.txtM.IntValue = 0;
            this.txtM.LengthUnit = ESADS.eLengthUnits.m;
            this.txtM.Location = new System.Drawing.Point(195, 29);
            this.txtM.Measurment = ESADS.GUI.Controls.eMeasurment.Moment;
            this.txtM.Name = "txtM";
            this.txtM.ReadOnly = true;
            this.txtM.Size = new System.Drawing.Size(100, 20);
            this.txtM.SU = 0D;
            this.txtM.TabIndex = 6;
            this.txtM.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(175, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "M";
            // 
            // txtPUni
            // 
            this.txtPUni.AutomaticResize = false;
            this.txtPUni.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.txtPUni.DoubleValue = 0D;
            this.txtPUni.ForceUnit = ESADS.eForceUints.KN;
            this.txtPUni.IntValue = 0;
            this.txtPUni.LengthUnit = ESADS.eLengthUnits.m;
            this.txtPUni.Location = new System.Drawing.Point(52, 29);
            this.txtPUni.Measurment = ESADS.GUI.Controls.eMeasurment.Force;
            this.txtPUni.Name = "txtPUni";
            this.txtPUni.Size = new System.Drawing.Size(100, 20);
            this.txtPUni.SU = 0D;
            this.txtPUni.TabIndex = 4;
            this.txtPUni.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "P";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtEconomy);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtAsmin);
            this.groupBox2.Controls.Add(this.txtAsprovided);
            this.groupBox2.Controls.Add(this.txtAsmax);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtAscalcualted);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(438, 128);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reinforcement ";
            // 
            // txtEconomy
            // 
            this.txtEconomy.BackColor = System.Drawing.SystemColors.Info;
            this.txtEconomy.Location = new System.Drawing.Point(96, 89);
            this.txtEconomy.Name = "txtEconomy";
            this.txtEconomy.ReadOnly = true;
            this.txtEconomy.Size = new System.Drawing.Size(100, 20);
            this.txtEconomy.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Economy (%)";
            // 
            // txtAsmin
            // 
            this.txtAsmin.BackColor = System.Drawing.SystemColors.Info;
            this.txtAsmin.Location = new System.Drawing.Point(314, 27);
            this.txtAsmin.Name = "txtAsmin";
            this.txtAsmin.ReadOnly = true;
            this.txtAsmin.Size = new System.Drawing.Size(100, 20);
            this.txtAsmin.TabIndex = 7;
            // 
            // txtAsprovided
            // 
            this.txtAsprovided.BackColor = System.Drawing.SystemColors.Info;
            this.txtAsprovided.Location = new System.Drawing.Point(96, 58);
            this.txtAsprovided.Name = "txtAsprovided";
            this.txtAsprovided.ReadOnly = true;
            this.txtAsprovided.Size = new System.Drawing.Size(100, 20);
            this.txtAsprovided.TabIndex = 6;
            // 
            // txtAsmax
            // 
            this.txtAsmax.BackColor = System.Drawing.SystemColors.Info;
            this.txtAsmax.Location = new System.Drawing.Point(314, 54);
            this.txtAsmax.Name = "txtAsmax";
            this.txtAsmax.ReadOnly = true;
            this.txtAsmax.Size = new System.Drawing.Size(100, 20);
            this.txtAsmax.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "As,provided";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(257, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "As,min";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "As,max";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "As,calculated";
            // 
            // txtAscalcualted
            // 
            this.txtAscalcualted.BackColor = System.Drawing.SystemColors.Info;
            this.txtAscalcualted.Location = new System.Drawing.Point(96, 27);
            this.txtAscalcualted.Name = "txtAscalcualted";
            this.txtAscalcualted.ReadOnly = true;
            this.txtAscalcualted.Size = new System.Drawing.Size(100, 20);
            this.txtAscalcualted.TabIndex = 0;
            // 
            // gbxBiaxial
            // 
            this.gbxBiaxial.Controls.Add(this.txtTeta);
            this.gbxBiaxial.Controls.Add(this.txtXbi);
            this.gbxBiaxial.Controls.Add(this.label19);
            this.gbxBiaxial.Controls.Add(this.label20);
            this.gbxBiaxial.Controls.Add(this.txtMy);
            this.gbxBiaxial.Controls.Add(this.txtMx);
            this.gbxBiaxial.Controls.Add(this.label11);
            this.gbxBiaxial.Controls.Add(this.label10);
            this.gbxBiaxial.Controls.Add(this.txtR);
            this.gbxBiaxial.Controls.Add(this.label8);
            this.gbxBiaxial.Controls.Add(this.txtPbi);
            this.gbxBiaxial.Controls.Add(this.label9);
            this.gbxBiaxial.Controls.Add(this.btnBiCalc);
            this.gbxBiaxial.Location = new System.Drawing.Point(12, 359);
            this.gbxBiaxial.Name = "gbxBiaxial";
            this.gbxBiaxial.Size = new System.Drawing.Size(438, 114);
            this.gbxBiaxial.TabIndex = 3;
            this.gbxBiaxial.TabStop = false;
            this.gbxBiaxial.Text = " Load Interation";
            // 
            // txtMy
            // 
            this.txtMy.AutomaticResize = false;
            this.txtMy.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtMy.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.txtMy.DoubleValue = 0D;
            this.txtMy.ForceUnit = ESADS.eForceUints.KN;
            this.txtMy.IntValue = 0;
            this.txtMy.LengthUnit = ESADS.eLengthUnits.m;
            this.txtMy.Location = new System.Drawing.Point(217, 52);
            this.txtMy.Measurment = ESADS.GUI.Controls.eMeasurment.Moment;
            this.txtMy.Name = "txtMy";
            this.txtMy.ReadOnly = true;
            this.txtMy.Size = new System.Drawing.Size(100, 20);
            this.txtMy.SU = 0D;
            this.txtMy.TabIndex = 19;
            this.txtMy.Text = "0";
            // 
            // txtMx
            // 
            this.txtMx.AutomaticResize = false;
            this.txtMx.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtMx.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.txtMx.DoubleValue = 0D;
            this.txtMx.ForceUnit = ESADS.eForceUints.KN;
            this.txtMx.IntValue = 0;
            this.txtMx.LengthUnit = ESADS.eLengthUnits.m;
            this.txtMx.Location = new System.Drawing.Point(217, 26);
            this.txtMx.Measurment = ESADS.GUI.Controls.eMeasurment.Moment;
            this.txtMx.Name = "txtMx";
            this.txtMx.ReadOnly = true;
            this.txtMx.Size = new System.Drawing.Size(100, 20);
            this.txtMx.SU = 0D;
            this.txtMx.TabIndex = 18;
            this.txtMx.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(184, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(21, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "My";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(184, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Mx";
            // 
            // txtR
            // 
            this.txtR.AutomaticResize = false;
            this.txtR.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.txtR.DoubleValue = 0D;
            this.txtR.ForceUnit = ESADS.eForceUints.KN;
            this.txtR.IntValue = 0;
            this.txtR.LengthUnit = ESADS.eLengthUnits.m;
            this.txtR.Location = new System.Drawing.Point(59, 52);
            this.txtR.Measurment = ESADS.GUI.Controls.eMeasurment.None;
            this.txtR.Name = "txtR";
            this.txtR.Size = new System.Drawing.Size(100, 20);
            this.txtR.SU = 0D;
            this.txtR.TabIndex = 12;
            this.txtR.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Mx/My";
            // 
            // txtPbi
            // 
            this.txtPbi.AutomaticResize = false;
            this.txtPbi.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.txtPbi.DoubleValue = 0D;
            this.txtPbi.ForceUnit = ESADS.eForceUints.KN;
            this.txtPbi.IntValue = 0;
            this.txtPbi.LengthUnit = ESADS.eLengthUnits.m;
            this.txtPbi.Location = new System.Drawing.Point(59, 26);
            this.txtPbi.Measurment = ESADS.GUI.Controls.eMeasurment.Force;
            this.txtPbi.Name = "txtPbi";
            this.txtPbi.Size = new System.Drawing.Size(100, 20);
            this.txtPbi.SU = 0D;
            this.txtPbi.TabIndex = 10;
            this.txtPbi.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(30, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "P";
            // 
            // btnBiCalc
            // 
            this.btnBiCalc.Location = new System.Drawing.Point(348, 34);
            this.btnBiCalc.Name = "btnBiCalc";
            this.btnBiCalc.Size = new System.Drawing.Size(75, 23);
            this.btnBiCalc.TabIndex = 17;
            this.btnBiCalc.Text = "Calculate";
            this.btnBiCalc.UseVisualStyleBackColor = true;
            this.btnBiCalc.Click += new System.EventHandler(this.btnBiCalc_Click);
            // 
            // rbtnAscalc
            // 
            this.rbtnAscalc.AutoSize = true;
            this.rbtnAscalc.Checked = true;
            this.rbtnAscalc.Location = new System.Drawing.Point(37, 31);
            this.rbtnAscalc.Name = "rbtnAscalc";
            this.rbtnAscalc.Size = new System.Drawing.Size(111, 17);
            this.rbtnAscalc.TabIndex = 0;
            this.rbtnAscalc.TabStop = true;
            this.rbtnAscalc.Text = "Use As,calculated";
            this.rbtnAscalc.UseVisualStyleBackColor = true;
            this.rbtnAscalc.CheckedChanged += new System.EventHandler(this.rbtnAscalc_CheckedChanged);
            // 
            // rbtnAsprov
            // 
            this.rbtnAsprov.AutoSize = true;
            this.rbtnAsprov.Location = new System.Drawing.Point(37, 54);
            this.rbtnAsprov.Name = "rbtnAsprov";
            this.rbtnAsprov.Size = new System.Drawing.Size(103, 17);
            this.rbtnAsprov.TabIndex = 3;
            this.rbtnAsprov.Text = "Use As,provided";
            this.rbtnAsprov.UseVisualStyleBackColor = true;
            // 
            // cbxLengthUnit
            // 
            this.cbxLengthUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLengthUnit.FormattingEnabled = true;
            this.cbxLengthUnit.Location = new System.Drawing.Point(92, 20);
            this.cbxLengthUnit.Name = "cbxLengthUnit";
            this.cbxLengthUnit.Size = new System.Drawing.Size(48, 21);
            this.cbxLengthUnit.TabIndex = 4;
            this.cbxLengthUnit.SelectedIndexChanged += new System.EventHandler(this.cbxLengthUnit_SelectedIndexChanged);
            // 
            // cbxForceUnit
            // 
            this.cbxForceUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxForceUnit.FormattingEnabled = true;
            this.cbxForceUnit.Location = new System.Drawing.Point(92, 52);
            this.cbxForceUnit.Name = "cbxForceUnit";
            this.cbxForceUnit.Size = new System.Drawing.Size(48, 21);
            this.cbxForceUnit.TabIndex = 5;
            this.cbxForceUnit.SelectedIndexChanged += new System.EventHandler(this.cbxForceUnit_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnAscalc);
            this.groupBox1.Controls.Add(this.rbtnAsprov);
            this.groupBox1.Location = new System.Drawing.Point(12, 146);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 88);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Analysis Reinforcement";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.cbxLengthUnit);
            this.groupBox3.Controls.Add(this.cbxForceUnit);
            this.groupBox3.Location = new System.Drawing.Point(211, 147);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(239, 87);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Unit";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(36, 55);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "Force";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(36, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Length";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(261, 49);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 13);
            this.label14.TabIndex = 8;
            this.label14.Text = "Balaced P";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(259, 78);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 13);
            this.label15.TabIndex = 9;
            this.label15.Text = "Balaced M";
            // 
            // txtPb
            // 
            this.txtPb.AutomaticResize = false;
            this.txtPb.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtPb.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.txtPb.DoubleValue = 0D;
            this.txtPb.ForceUnit = ESADS.eForceUints.KN;
            this.txtPb.IntValue = 0;
            this.txtPb.LengthUnit = ESADS.eLengthUnits.m;
            this.txtPb.Location = new System.Drawing.Point(323, 46);
            this.txtPb.Measurment = ESADS.GUI.Controls.eMeasurment.Force;
            this.txtPb.Name = "txtPb";
            this.txtPb.ReadOnly = true;
            this.txtPb.Size = new System.Drawing.Size(100, 20);
            this.txtPb.SU = 0D;
            this.txtPb.TabIndex = 10;
            this.txtPb.Text = "0";
            // 
            // txtMpure
            // 
            this.txtMpure.AutomaticResize = false;
            this.txtMpure.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtMpure.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.txtMpure.DoubleValue = 0D;
            this.txtMpure.ForceUnit = ESADS.eForceUints.KN;
            this.txtMpure.IntValue = 0;
            this.txtMpure.LengthUnit = ESADS.eLengthUnits.m;
            this.txtMpure.Location = new System.Drawing.Point(133, 75);
            this.txtMpure.Measurment = ESADS.GUI.Controls.eMeasurment.Moment;
            this.txtMpure.Name = "txtMpure";
            this.txtMpure.ReadOnly = true;
            this.txtMpure.Size = new System.Drawing.Size(100, 20);
            this.txtMpure.SU = 0D;
            this.txtMpure.TabIndex = 11;
            this.txtMpure.Text = "0";
            // 
            // txtPpure
            // 
            this.txtPpure.AutomaticResize = false;
            this.txtPpure.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtPpure.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.txtPpure.DoubleValue = 0D;
            this.txtPpure.ForceUnit = ESADS.eForceUints.KN;
            this.txtPpure.IntValue = 0;
            this.txtPpure.LengthUnit = ESADS.eLengthUnits.m;
            this.txtPpure.Location = new System.Drawing.Point(135, 46);
            this.txtPpure.Measurment = ESADS.GUI.Controls.eMeasurment.Force;
            this.txtPpure.Name = "txtPpure";
            this.txtPpure.ReadOnly = true;
            this.txtPpure.Size = new System.Drawing.Size(100, 20);
            this.txtPpure.SU = 0D;
            this.txtPpure.TabIndex = 12;
            this.txtPpure.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(15, 78);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(112, 13);
            this.label16.TabIndex = 13;
            this.label16.Text = "Pure Flexural Capacity";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(15, 49);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(98, 13);
            this.label17.TabIndex = 14;
            this.label17.Text = "Pure Axial Capacity";
            // 
            // txtMb
            // 
            this.txtMb.AutomaticResize = false;
            this.txtMb.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtMb.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.txtMb.DoubleValue = 0D;
            this.txtMb.ForceUnit = ESADS.eForceUints.KN;
            this.txtMb.IntValue = 0;
            this.txtMb.LengthUnit = ESADS.eLengthUnits.m;
            this.txtMb.Location = new System.Drawing.Point(323, 75);
            this.txtMb.Measurment = ESADS.GUI.Controls.eMeasurment.Moment;
            this.txtMb.Name = "txtMb";
            this.txtMb.ReadOnly = true;
            this.txtMb.Size = new System.Drawing.Size(100, 20);
            this.txtMb.SU = 0D;
            this.txtMb.TabIndex = 15;
            this.txtMb.Text = "0";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblAction);
            this.groupBox4.Controls.Add(this.txtMb);
            this.groupBox4.Controls.Add(this.txtPpure);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.txtPb);
            this.groupBox4.Controls.Add(this.txtMpure);
            this.groupBox4.Location = new System.Drawing.Point(12, 240);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(438, 111);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Load Interaction";
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Location = new System.Drawing.Point(132, 19);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(40, 13);
            this.lblAction.TabIndex = 16;
            this.lblAction.Text = "Mx/My";
            // 
            // ntxtX
            // 
            this.ntxtX.AutomaticResize = false;
            this.ntxtX.BackColor = System.Drawing.SystemColors.Window;
            this.ntxtX.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtX.DoubleValue = 0D;
            this.ntxtX.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtX.IntValue = 0;
            this.ntxtX.LengthUnit = ESADS.eLengthUnits.mm;
            this.ntxtX.Location = new System.Drawing.Point(195, 58);
            this.ntxtX.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtX.Name = "ntxtX";
            this.ntxtX.ReadOnly = true;
            this.ntxtX.Size = new System.Drawing.Size(100, 20);
            this.ntxtX.SU = 0D;
            this.ntxtX.TabIndex = 9;
            this.ntxtX.Text = "0";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(175, 61);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(14, 13);
            this.label18.TabIndex = 8;
            this.label18.Text = "X";
            // 
            // txtTeta
            // 
            this.txtTeta.AutomaticResize = false;
            this.txtTeta.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtTeta.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.txtTeta.DoubleValue = 0D;
            this.txtTeta.ForceUnit = ESADS.eForceUints.KN;
            this.txtTeta.IntValue = 0;
            this.txtTeta.LengthUnit = ESADS.eLengthUnits.m;
            this.txtTeta.Location = new System.Drawing.Point(217, 79);
            this.txtTeta.Measurment = ESADS.GUI.Controls.eMeasurment.None;
            this.txtTeta.Name = "txtTeta";
            this.txtTeta.ReadOnly = true;
            this.txtTeta.Size = new System.Drawing.Size(100, 20);
            this.txtTeta.SU = 0D;
            this.txtTeta.TabIndex = 23;
            this.txtTeta.Text = "0";
            // 
            // txtXbi
            // 
            this.txtXbi.AutomaticResize = false;
            this.txtXbi.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtXbi.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.txtXbi.DoubleValue = 0D;
            this.txtXbi.ForceUnit = ESADS.eForceUints.N;
            this.txtXbi.IntValue = 0;
            this.txtXbi.LengthUnit = ESADS.eLengthUnits.mm;
            this.txtXbi.Location = new System.Drawing.Point(59, 78);
            this.txtXbi.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.txtXbi.Name = "txtXbi";
            this.txtXbi.ReadOnly = true;
            this.txtXbi.Size = new System.Drawing.Size(100, 20);
            this.txtXbi.SU = 0D;
            this.txtXbi.TabIndex = 22;
            this.txtXbi.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(184, 85);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(13, 13);
            this.label19.TabIndex = 21;
            this.label19.Text = "θ";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(34, 82);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(14, 13);
            this.label20.TabIndex = 20;
            this.label20.Text = "X";
            // 
            // eColumnOutputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 514);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gbxBiaxial);
            this.Controls.Add(this.gbxUniaxial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eColumnOutputDialog";
            this.ShowIcon = false;
            this.Text = "Design Output";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.eColumnOutputDialog_FormClosing);
            this.gbxUniaxial.ResumeLayout(false);
            this.gbxUniaxial.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbxBiaxial.ResumeLayout(false);
            this.gbxBiaxial.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox gbxUniaxial;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAsmin;
        private System.Windows.Forms.TextBox txtAsprovided;
        private System.Windows.Forms.TextBox txtAsmax;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAscalcualted;
        private System.Windows.Forms.GroupBox gbxBiaxial;
        private GUI.Controls.eNumericTextBox txtM;
        private System.Windows.Forms.Label label7;
        private GUI.Controls.eNumericTextBox txtPUni;
        private System.Windows.Forms.RadioButton rbtnAsprov;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rbtnAscalc;
        private System.Windows.Forms.TextBox txtEconomy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private GUI.Controls.eNumericTextBox txtR;
        private System.Windows.Forms.Label label8;
        private GUI.Controls.eNumericTextBox txtPbi;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbxLengthUnit;
        private System.Windows.Forms.ComboBox cbxForceUnit;
        private System.Windows.Forms.Button txtUniCalc;
        private System.Windows.Forms.Button btnBiCalc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private GUI.Controls.eNumericTextBox txtPb;
        private GUI.Controls.eNumericTextBox txtMpure;
        private GUI.Controls.eNumericTextBox txtPpure;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private GUI.Controls.eNumericTextBox txtMb;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblAction;
        private GUI.Controls.eNumericTextBox txtMy;
        private GUI.Controls.eNumericTextBox txtMx;
        private Controls.eNumericTextBox ntxtX;
        private System.Windows.Forms.Label label18;
        private Controls.eNumericTextBox txtTeta;
        private Controls.eNumericTextBox txtXbi;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
    }
}