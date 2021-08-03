using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ESADS.GUI
{
    partial class eNewFootingDialog:Form
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
            this.gbxPreview = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.cbxLengthUnit = new System.Windows.Forms.ComboBox();
            this.cbxForceUnit = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chDesignDepth = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ntxtWidth = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtLength = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtDepth = new ESADS.GUI.Controls.eNumericTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxSteel = new System.Windows.Forms.ComboBox();
            this.cbxConcrete = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ntxtP = new ESADS.GUI.Controls.eNumericTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chConsiderSelfWeight = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ntxtMy = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtMx = new ESADS.GUI.Controls.eNumericTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ntxtColumnWidth = new ESADS.GUI.Controls.eNumericTextBox();
            this.rbtnCircular = new System.Windows.Forms.RadioButton();
            this.rbtnRectangular = new System.Windows.Forms.RadioButton();
            this.lblBc = new System.Windows.Forms.Label();
            this.lblColumnDiam = new System.Windows.Forms.Label();
            this.lblColumnWidth = new System.Windows.Forms.Label();
            this.ntxtColumnDiameter = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtColumnLength = new ESADS.GUI.Controls.eNumericTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.ntxtSpacingIncrement = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtDepthIncrment = new ESADS.GUI.Controls.eNumericTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.gbBarDiam = new System.Windows.Forms.GroupBox();
            this.cbxDiameter = new System.Windows.Forms.ComboBox();
            this.cbxMaxDiam = new System.Windows.Forms.ComboBox();
            this.cbxMinDiam = new System.Windows.Forms.ComboBox();
            this.chUseDiam = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.ntxtMaxAggrtSize = new ESADS.GUI.Controls.eNumericTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.gbxSpacing = new System.Windows.Forms.GroupBox();
            this.ntxtSpacing = new ESADS.GUI.Controls.eNumericTextBox();
            this.chUseSpacing = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.ntxtMaxS = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtMinS = new ESADS.GUI.Controls.eNumericTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ntxtCover = new ESADS.GUI.Controls.eNumericTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbxExposureType = new System.Windows.Forms.ComboBox();
            this.gbxPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.gbBarDiam.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.gbxSpacing.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxPreview
            // 
            this.gbxPreview.Controls.Add(this.pictureBox1);
            this.gbxPreview.Location = new System.Drawing.Point(469, 15);
            this.gbxPreview.Name = "gbxPreview";
            this.gbxPreview.Size = new System.Drawing.Size(282, 361);
            this.gbxPreview.TabIndex = 5;
            this.gbxPreview.TabStop = false;
            this.gbxPreview.Text = "Preview";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = global::ESADS.GUI.Properties.Resources.newfootingpreview21;
            this.pictureBox1.Location = new System.Drawing.Point(6, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(270, 339);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(366, 336);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(273, 336);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cbxLengthUnit
            // 
            this.cbxLengthUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLengthUnit.FormattingEnabled = true;
            this.cbxLengthUnit.Location = new System.Drawing.Point(61, 23);
            this.cbxLengthUnit.Name = "cbxLengthUnit";
            this.cbxLengthUnit.Size = new System.Drawing.Size(44, 21);
            this.cbxLengthUnit.TabIndex = 10;
            this.cbxLengthUnit.SelectedIndexChanged += new System.EventHandler(this.cbxLengthUnit_SelectedIndexChanged);
            // 
            // cbxForceUnit
            // 
            this.cbxForceUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxForceUnit.FormattingEnabled = true;
            this.cbxForceUnit.Location = new System.Drawing.Point(166, 23);
            this.cbxForceUnit.Name = "cbxForceUnit";
            this.cbxForceUnit.Size = new System.Drawing.Size(47, 21);
            this.cbxForceUnit.TabIndex = 9;
            this.cbxForceUnit.SelectedIndexChanged += new System.EventHandler(this.cbxForceUnit_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cbxForceUnit);
            this.groupBox3.Controls.Add(this.cbxLengthUnit);
            this.groupBox3.Location = new System.Drawing.Point(3, 315);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(250, 61);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Units";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(122, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Force";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Length";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(451, 306);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(443, 280);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chDesignDepth);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.ntxtWidth);
            this.groupBox4.Controls.Add(this.ntxtLength);
            this.groupBox4.Controls.Add(this.ntxtDepth);
            this.groupBox4.Location = new System.Drawing.Point(8, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(223, 138);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Pad Geometery";
            // 
            // chDesignDepth
            // 
            this.chDesignDepth.AutoSize = true;
            this.chDesignDepth.Location = new System.Drawing.Point(93, 107);
            this.chDesignDepth.Name = "chDesignDepth";
            this.chDesignDepth.Size = new System.Drawing.Size(94, 17);
            this.chDesignDepth.TabIndex = 0;
            this.chDesignDepth.Text = "Design Depth ";
            this.chDesignDepth.UseVisualStyleBackColor = true;
            this.chDesignDepth.CheckedChanged += new System.EventHandler(this.chDesignDepth_CheckedChanged_1);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Width (B)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Length (L)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(22, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Depth (D)";
            // 
            // ntxtWidth
            // 
            this.ntxtWidth.AutomaticResize = false;
            this.ntxtWidth.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtWidth.DoubleValue = 0D;
            this.ntxtWidth.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtWidth.IntValue = 0;
            this.ntxtWidth.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtWidth.Location = new System.Drawing.Point(93, 51);
            this.ntxtWidth.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtWidth.Name = "ntxtWidth";
            this.ntxtWidth.Size = new System.Drawing.Size(84, 20);
            this.ntxtWidth.SU = 0D;
            this.ntxtWidth.TabIndex = 0;
            this.ntxtWidth.Text = "0";
            // 
            // ntxtLength
            // 
            this.ntxtLength.AutomaticResize = false;
            this.ntxtLength.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtLength.DoubleValue = 0D;
            this.ntxtLength.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtLength.IntValue = 0;
            this.ntxtLength.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtLength.Location = new System.Drawing.Point(93, 23);
            this.ntxtLength.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtLength.Name = "ntxtLength";
            this.ntxtLength.Size = new System.Drawing.Size(84, 20);
            this.ntxtLength.SU = 0D;
            this.ntxtLength.TabIndex = 0;
            this.ntxtLength.Text = "0";
            // 
            // ntxtDepth
            // 
            this.ntxtDepth.AutomaticResize = false;
            this.ntxtDepth.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtDepth.DoubleValue = 0D;
            this.ntxtDepth.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtDepth.IntValue = 0;
            this.ntxtDepth.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtDepth.Location = new System.Drawing.Point(93, 81);
            this.ntxtDepth.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtDepth.Name = "ntxtDepth";
            this.ntxtDepth.Size = new System.Drawing.Size(84, 20);
            this.ntxtDepth.SU = 0D;
            this.ntxtDepth.TabIndex = 0;
            this.ntxtDepth.Text = "0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxSteel);
            this.groupBox1.Controls.Add(this.cbxConcrete);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(247, 150);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 107);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Material";
            // 
            // cbxSteel
            // 
            this.cbxSteel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSteel.FormattingEnabled = true;
            this.cbxSteel.Location = new System.Drawing.Point(91, 61);
            this.cbxSteel.Name = "cbxSteel";
            this.cbxSteel.Size = new System.Drawing.Size(68, 21);
            this.cbxSteel.TabIndex = 3;
            // 
            // cbxConcrete
            // 
            this.cbxConcrete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxConcrete.FormattingEnabled = true;
            this.cbxConcrete.Location = new System.Drawing.Point(91, 28);
            this.cbxConcrete.Name = "cbxConcrete";
            this.cbxConcrete.Size = new System.Drawing.Size(68, 21);
            this.cbxConcrete.TabIndex = 2;
            this.cbxConcrete.Tag = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Steel";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Concrete";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ntxtP);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.chConsiderSelfWeight);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.ntxtMy);
            this.groupBox2.Controls.Add(this.ntxtMx);
            this.groupBox2.Location = new System.Drawing.Point(247, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(188, 138);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Load";
            // 
            // ntxtP
            // 
            this.ntxtP.AutomaticResize = false;
            this.ntxtP.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtP.DoubleValue = 0D;
            this.ntxtP.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtP.IntValue = 0;
            this.ntxtP.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtP.Location = new System.Drawing.Point(54, 19);
            this.ntxtP.Measurment = ESADS.GUI.Controls.eMeasurment.Force;
            this.ntxtP.Name = "ntxtP";
            this.ntxtP.Size = new System.Drawing.Size(84, 20);
            this.ntxtP.SU = 0D;
            this.ntxtP.TabIndex = 0;
            this.ntxtP.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "P";
            // 
            // chConsiderSelfWeight
            // 
            this.chConsiderSelfWeight.AutoSize = true;
            this.chConsiderSelfWeight.Location = new System.Drawing.Point(39, 107);
            this.chConsiderSelfWeight.Name = "chConsiderSelfWeight";
            this.chConsiderSelfWeight.Size = new System.Drawing.Size(120, 17);
            this.chConsiderSelfWeight.TabIndex = 1;
            this.chConsiderSelfWeight.Text = "Consider self weight";
            this.chConsiderSelfWeight.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "My";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Mx";
            // 
            // ntxtMy
            // 
            this.ntxtMy.AutomaticResize = false;
            this.ntxtMy.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtMy.DoubleValue = 0D;
            this.ntxtMy.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtMy.IntValue = 0;
            this.ntxtMy.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtMy.Location = new System.Drawing.Point(54, 75);
            this.ntxtMy.Measurment = ESADS.GUI.Controls.eMeasurment.Moment;
            this.ntxtMy.Name = "ntxtMy";
            this.ntxtMy.Size = new System.Drawing.Size(84, 20);
            this.ntxtMy.SU = 0D;
            this.ntxtMy.TabIndex = 0;
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
            this.ntxtMx.Location = new System.Drawing.Point(54, 47);
            this.ntxtMx.Measurment = ESADS.GUI.Controls.eMeasurment.Moment;
            this.ntxtMx.Name = "ntxtMx";
            this.ntxtMx.Size = new System.Drawing.Size(84, 20);
            this.ntxtMx.SU = 0D;
            this.ntxtMx.TabIndex = 0;
            this.ntxtMx.Text = "0";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ntxtColumnWidth);
            this.groupBox5.Controls.Add(this.rbtnCircular);
            this.groupBox5.Controls.Add(this.rbtnRectangular);
            this.groupBox5.Controls.Add(this.lblBc);
            this.groupBox5.Controls.Add(this.lblColumnDiam);
            this.groupBox5.Controls.Add(this.lblColumnWidth);
            this.groupBox5.Controls.Add(this.ntxtColumnDiameter);
            this.groupBox5.Controls.Add(this.ntxtColumnLength);
            this.groupBox5.Location = new System.Drawing.Point(8, 150);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(223, 107);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Column Geometery";
            // 
            // ntxtColumnWidth
            // 
            this.ntxtColumnWidth.AutomaticResize = false;
            this.ntxtColumnWidth.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtColumnWidth.DoubleValue = 0D;
            this.ntxtColumnWidth.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtColumnWidth.IntValue = 0;
            this.ntxtColumnWidth.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtColumnWidth.Location = new System.Drawing.Point(104, 73);
            this.ntxtColumnWidth.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtColumnWidth.Name = "ntxtColumnWidth";
            this.ntxtColumnWidth.Size = new System.Drawing.Size(84, 20);
            this.ntxtColumnWidth.SU = 0D;
            this.ntxtColumnWidth.TabIndex = 0;
            this.ntxtColumnWidth.Text = "0";
            // 
            // rbtnCircular
            // 
            this.rbtnCircular.AutoSize = true;
            this.rbtnCircular.Location = new System.Drawing.Point(127, 19);
            this.rbtnCircular.Name = "rbtnCircular";
            this.rbtnCircular.Size = new System.Drawing.Size(60, 17);
            this.rbtnCircular.TabIndex = 1;
            this.rbtnCircular.Text = "Circular";
            this.rbtnCircular.UseVisualStyleBackColor = true;
            // 
            // rbtnRectangular
            // 
            this.rbtnRectangular.AutoSize = true;
            this.rbtnRectangular.Checked = true;
            this.rbtnRectangular.Location = new System.Drawing.Point(13, 19);
            this.rbtnRectangular.Name = "rbtnRectangular";
            this.rbtnRectangular.Size = new System.Drawing.Size(83, 17);
            this.rbtnRectangular.TabIndex = 0;
            this.rbtnRectangular.TabStop = true;
            this.rbtnRectangular.Text = "Rectangular";
            this.rbtnRectangular.UseVisualStyleBackColor = true;
            this.rbtnRectangular.CheckedChanged += new System.EventHandler(this.rbtnRectangular_CheckedChanged_1);
            // 
            // lblBc
            // 
            this.lblBc.AutoSize = true;
            this.lblBc.Location = new System.Drawing.Point(20, 76);
            this.lblBc.Name = "lblBc";
            this.lblBc.Size = new System.Drawing.Size(60, 13);
            this.lblBc.TabIndex = 1;
            this.lblBc.Text = "Height (Bc)";
            // 
            // lblColumnDiam
            // 
            this.lblColumnDiam.AutoSize = true;
            this.lblColumnDiam.Location = new System.Drawing.Point(26, 49);
            this.lblColumnDiam.Name = "lblColumnDiam";
            this.lblColumnDiam.Size = new System.Drawing.Size(49, 13);
            this.lblColumnDiam.TabIndex = 9;
            this.lblColumnDiam.Text = "Diameter";
            this.lblColumnDiam.Visible = false;
            // 
            // lblColumnWidth
            // 
            this.lblColumnWidth.AutoSize = true;
            this.lblColumnWidth.Location = new System.Drawing.Point(25, 49);
            this.lblColumnWidth.Name = "lblColumnWidth";
            this.lblColumnWidth.Size = new System.Drawing.Size(56, 13);
            this.lblColumnWidth.TabIndex = 1;
            this.lblColumnWidth.Text = "Width (Lc)";
            // 
            // ntxtColumnDiameter
            // 
            this.ntxtColumnDiameter.AutomaticResize = false;
            this.ntxtColumnDiameter.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtColumnDiameter.DoubleValue = 0D;
            this.ntxtColumnDiameter.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtColumnDiameter.IntValue = 0;
            this.ntxtColumnDiameter.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtColumnDiameter.Location = new System.Drawing.Point(104, 46);
            this.ntxtColumnDiameter.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtColumnDiameter.Name = "ntxtColumnDiameter";
            this.ntxtColumnDiameter.Size = new System.Drawing.Size(84, 20);
            this.ntxtColumnDiameter.SU = 0D;
            this.ntxtColumnDiameter.TabIndex = 8;
            this.ntxtColumnDiameter.Text = "0";
            this.ntxtColumnDiameter.Visible = false;
            // 
            // ntxtColumnLength
            // 
            this.ntxtColumnLength.AutomaticResize = false;
            this.ntxtColumnLength.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtColumnLength.DoubleValue = 0D;
            this.ntxtColumnLength.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtColumnLength.IntValue = 0;
            this.ntxtColumnLength.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtColumnLength.Location = new System.Drawing.Point(104, 46);
            this.ntxtColumnLength.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtColumnLength.Name = "ntxtColumnLength";
            this.ntxtColumnLength.Size = new System.Drawing.Size(84, 20);
            this.ntxtColumnLength.SU = 0D;
            this.ntxtColumnLength.TabIndex = 0;
            this.ntxtColumnLength.Text = "0";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox10);
            this.tabPage2.Controls.Add(this.gbBarDiam);
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Controls.Add(this.gbxSpacing);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(443, 280);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Detailing";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.ntxtSpacingIncrement);
            this.groupBox10.Controls.Add(this.ntxtDepthIncrment);
            this.groupBox10.Controls.Add(this.label18);
            this.groupBox10.Controls.Add(this.label19);
            this.groupBox10.Location = new System.Drawing.Point(215, 3);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(200, 90);
            this.groupBox10.TabIndex = 5;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Icrease Dimesions By";
            // 
            // ntxtSpacingIncrement
            // 
            this.ntxtSpacingIncrement.AutomaticResize = false;
            this.ntxtSpacingIncrement.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtSpacingIncrement.DoubleValue = 0D;
            this.ntxtSpacingIncrement.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtSpacingIncrement.IntValue = 0;
            this.ntxtSpacingIncrement.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtSpacingIncrement.Location = new System.Drawing.Point(126, 52);
            this.ntxtSpacingIncrement.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtSpacingIncrement.Name = "ntxtSpacingIncrement";
            this.ntxtSpacingIncrement.Size = new System.Drawing.Size(62, 20);
            this.ntxtSpacingIncrement.SU = 0D;
            this.ntxtSpacingIncrement.TabIndex = 10;
            this.ntxtSpacingIncrement.Text = "0";
            // 
            // ntxtDepthIncrment
            // 
            this.ntxtDepthIncrment.AutomaticResize = false;
            this.ntxtDepthIncrment.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtDepthIncrment.DoubleValue = 0D;
            this.ntxtDepthIncrment.Enabled = false;
            this.ntxtDepthIncrment.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtDepthIncrment.IntValue = 0;
            this.ntxtDepthIncrment.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtDepthIncrment.Location = new System.Drawing.Point(126, 23);
            this.ntxtDepthIncrment.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtDepthIncrment.Name = "ntxtDepthIncrment";
            this.ntxtDepthIncrment.Size = new System.Drawing.Size(62, 20);
            this.ntxtDepthIncrment.SU = 0D;
            this.ntxtDepthIncrment.TabIndex = 9;
            this.ntxtDepthIncrment.Text = "0";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(19, 23);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(101, 13);
            this.label18.TabIndex = 8;
            this.label18.Text = "Increment depth by:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(9, 56);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(111, 13);
            this.label19.TabIndex = 7;
            this.label19.Text = "Increment spacing by:";
            // 
            // gbBarDiam
            // 
            this.gbBarDiam.Controls.Add(this.cbxDiameter);
            this.gbBarDiam.Controls.Add(this.cbxMaxDiam);
            this.gbBarDiam.Controls.Add(this.cbxMinDiam);
            this.gbBarDiam.Controls.Add(this.chUseDiam);
            this.gbBarDiam.Controls.Add(this.label16);
            this.gbBarDiam.Controls.Add(this.label17);
            this.gbBarDiam.Location = new System.Drawing.Point(215, 99);
            this.gbBarDiam.Name = "gbBarDiam";
            this.gbBarDiam.Size = new System.Drawing.Size(200, 114);
            this.gbBarDiam.TabIndex = 4;
            this.gbBarDiam.TabStop = false;
            this.gbBarDiam.Text = "Bar Diameter";
            // 
            // cbxDiameter
            // 
            this.cbxDiameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDiameter.FormattingEnabled = true;
            this.cbxDiameter.Location = new System.Drawing.Point(118, 78);
            this.cbxDiameter.Name = "cbxDiameter";
            this.cbxDiameter.Size = new System.Drawing.Size(62, 21);
            this.cbxDiameter.TabIndex = 9;
            this.cbxDiameter.Visible = false;
            // 
            // cbxMaxDiam
            // 
            this.cbxMaxDiam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMaxDiam.FormattingEnabled = true;
            this.cbxMaxDiam.Location = new System.Drawing.Point(118, 51);
            this.cbxMaxDiam.Name = "cbxMaxDiam";
            this.cbxMaxDiam.Size = new System.Drawing.Size(62, 21);
            this.cbxMaxDiam.TabIndex = 8;
            // 
            // cbxMinDiam
            // 
            this.cbxMinDiam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMinDiam.FormattingEnabled = true;
            this.cbxMinDiam.Location = new System.Drawing.Point(118, 22);
            this.cbxMinDiam.Name = "cbxMinDiam";
            this.cbxMinDiam.Size = new System.Drawing.Size(62, 21);
            this.cbxMinDiam.TabIndex = 7;
            // 
            // chUseDiam
            // 
            this.chUseDiam.AutoSize = true;
            this.chUseDiam.Location = new System.Drawing.Point(12, 80);
            this.chUseDiam.Name = "chUseDiam";
            this.chUseDiam.Size = new System.Drawing.Size(90, 17);
            this.chUseDiam.TabIndex = 2;
            this.chUseDiam.Text = "Use Diameter";
            this.chUseDiam.UseVisualStyleBackColor = true;
            this.chUseDiam.CheckedChanged += new System.EventHandler(this.chUseDiam_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 25);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(93, 13);
            this.label16.TabIndex = 6;
            this.label16.Text = "Minimum Diameter";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 54);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(96, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "Maximum Diameter";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.ntxtMaxAggrtSize);
            this.groupBox8.Controls.Add(this.label15);
            this.groupBox8.Location = new System.Drawing.Point(3, 219);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(203, 54);
            this.groupBox8.TabIndex = 3;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Maximum Aggregate Size";
            // 
            // ntxtMaxAggrtSize
            // 
            this.ntxtMaxAggrtSize.AutomaticResize = false;
            this.ntxtMaxAggrtSize.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtMaxAggrtSize.DoubleValue = 0D;
            this.ntxtMaxAggrtSize.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtMaxAggrtSize.IntValue = 0;
            this.ntxtMaxAggrtSize.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtMaxAggrtSize.Location = new System.Drawing.Point(103, 25);
            this.ntxtMaxAggrtSize.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtMaxAggrtSize.Name = "ntxtMaxAggrtSize";
            this.ntxtMaxAggrtSize.Size = new System.Drawing.Size(85, 20);
            this.ntxtMaxAggrtSize.SU = 0D;
            this.ntxtMaxAggrtSize.TabIndex = 7;
            this.ntxtMaxAggrtSize.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(51, 28);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "Size";
            // 
            // gbxSpacing
            // 
            this.gbxSpacing.Controls.Add(this.ntxtSpacing);
            this.gbxSpacing.Controls.Add(this.chUseSpacing);
            this.gbxSpacing.Controls.Add(this.label14);
            this.gbxSpacing.Controls.Add(this.ntxtMaxS);
            this.gbxSpacing.Controls.Add(this.ntxtMinS);
            this.gbxSpacing.Controls.Add(this.label13);
            this.gbxSpacing.Location = new System.Drawing.Point(3, 99);
            this.gbxSpacing.Name = "gbxSpacing";
            this.gbxSpacing.Size = new System.Drawing.Size(203, 114);
            this.gbxSpacing.TabIndex = 1;
            this.gbxSpacing.TabStop = false;
            this.gbxSpacing.Text = "Spacing";
            // 
            // ntxtSpacing
            // 
            this.ntxtSpacing.AutomaticResize = false;
            this.ntxtSpacing.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtSpacing.DoubleValue = 0D;
            this.ntxtSpacing.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtSpacing.IntValue = 0;
            this.ntxtSpacing.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtSpacing.Location = new System.Drawing.Point(106, 78);
            this.ntxtSpacing.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtSpacing.Name = "ntxtSpacing";
            this.ntxtSpacing.Size = new System.Drawing.Size(85, 20);
            this.ntxtSpacing.SU = 0D;
            this.ntxtSpacing.TabIndex = 7;
            this.ntxtSpacing.Text = "0";
            this.ntxtSpacing.Visible = false;
            // 
            // chUseSpacing
            // 
            this.chUseSpacing.AutoSize = true;
            this.chUseSpacing.Location = new System.Drawing.Point(12, 80);
            this.chUseSpacing.Name = "chUseSpacing";
            this.chUseSpacing.Size = new System.Drawing.Size(85, 17);
            this.chUseSpacing.TabIndex = 2;
            this.chUseSpacing.Text = "Use spacing";
            this.chUseSpacing.UseVisualStyleBackColor = true;
            this.chUseSpacing.CheckedChanged += new System.EventHandler(this.chUseSpacing_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 13);
            this.label14.TabIndex = 6;
            this.label14.Text = "Minimum spacing";
            // 
            // ntxtMaxS
            // 
            this.ntxtMaxS.AutomaticResize = false;
            this.ntxtMaxS.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtMaxS.DoubleValue = 0D;
            this.ntxtMaxS.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtMaxS.IntValue = 0;
            this.ntxtMaxS.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtMaxS.Location = new System.Drawing.Point(106, 51);
            this.ntxtMaxS.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtMaxS.Name = "ntxtMaxS";
            this.ntxtMaxS.Size = new System.Drawing.Size(85, 20);
            this.ntxtMaxS.SU = 0D;
            this.ntxtMaxS.TabIndex = 5;
            this.ntxtMaxS.Text = "0";
            // 
            // ntxtMinS
            // 
            this.ntxtMinS.AutomaticResize = false;
            this.ntxtMinS.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtMinS.DoubleValue = 0D;
            this.ntxtMinS.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtMinS.IntValue = 0;
            this.ntxtMinS.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtMinS.Location = new System.Drawing.Point(106, 22);
            this.ntxtMinS.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtMinS.Name = "ntxtMinS";
            this.ntxtMinS.Size = new System.Drawing.Size(85, 20);
            this.ntxtMinS.SU = 0D;
            this.ntxtMinS.TabIndex = 3;
            this.ntxtMinS.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 54);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Maximum spacing";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.ntxtCover);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.cbxExposureType);
            this.groupBox6.Location = new System.Drawing.Point(6, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(200, 90);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Exposure Type and Cover";
            // 
            // ntxtCover
            // 
            this.ntxtCover.AutomaticResize = false;
            this.ntxtCover.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtCover.DoubleValue = 0D;
            this.ntxtCover.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtCover.IntValue = 0;
            this.ntxtCover.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtCover.Location = new System.Drawing.Point(105, 56);
            this.ntxtCover.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtCover.Name = "ntxtCover";
            this.ntxtCover.Size = new System.Drawing.Size(85, 20);
            this.ntxtCover.SU = 0D;
            this.ntxtCover.TabIndex = 1;
            this.ntxtCover.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(56, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Cover";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Exposure Type";
            // 
            // cbxExposureType
            // 
            this.cbxExposureType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxExposureType.FormattingEnabled = true;
            this.cbxExposureType.Location = new System.Drawing.Point(105, 23);
            this.cbxExposureType.Name = "cbxExposureType";
            this.cbxExposureType.Size = new System.Drawing.Size(85, 21);
            this.cbxExposureType.TabIndex = 1;
            this.cbxExposureType.SelectedIndexChanged += new System.EventHandler(this.cbxExposureType_SelectedIndexChanged);
            // 
            // eNewFootingDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(763, 384);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbxPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eNewFootingDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Footing";
            this.gbxPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.gbBarDiam.ResumeLayout(false);
            this.gbBarDiam.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.gbxSpacing.ResumeLayout(false);
            this.gbxSpacing.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxPreview;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private ComboBox cbxLengthUnit;
        private ComboBox cbxForceUnit;
        private GroupBox groupBox3;
        private Label label6;
        private Label label7;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private GroupBox groupBox4;
        private CheckBox chDesignDepth;
        private Label label11;
        private Label label10;
        private Label label9;
        private Controls.eNumericTextBox ntxtWidth;
        private Controls.eNumericTextBox ntxtLength;
        private Controls.eNumericTextBox ntxtDepth;
        private GroupBox groupBox1;
        private ComboBox cbxSteel;
        private ComboBox cbxConcrete;
        private Label label2;
        private Label label1;
        private GroupBox groupBox2;
        private CheckBox chConsiderSelfWeight;
        private Label label5;
        private Label label4;
        private Label label3;
        private Controls.eNumericTextBox ntxtP;
        private Controls.eNumericTextBox ntxtMy;
        private Controls.eNumericTextBox ntxtMx;
        private GroupBox groupBox5;
        private Controls.eNumericTextBox ntxtColumnWidth;
        private RadioButton rbtnCircular;
        private RadioButton rbtnRectangular;
        private Label lblBc;
        private Controls.eNumericTextBox ntxtColumnDiameter;
        private Controls.eNumericTextBox ntxtColumnLength;
        private Label lblColumnWidth;
        private Label lblColumnDiam;
        private TabPage tabPage2;
        private GroupBox groupBox10;
        private Controls.eNumericTextBox ntxtSpacingIncrement;
        private Controls.eNumericTextBox ntxtDepthIncrment;
        private Label label18;
        private Label label19;
        private GroupBox gbBarDiam;
        private ComboBox cbxDiameter;
        private ComboBox cbxMaxDiam;
        private ComboBox cbxMinDiam;
        private CheckBox chUseDiam;
        private Label label16;
        private Label label17;
        private GroupBox groupBox8;
        private Controls.eNumericTextBox ntxtMaxAggrtSize;
        private Label label15;
        private GroupBox gbxSpacing;
        private Controls.eNumericTextBox ntxtSpacing;
        private CheckBox chUseSpacing;
        private Label label14;
        private Controls.eNumericTextBox ntxtMaxS;
        private Controls.eNumericTextBox ntxtMinS;
        private Label label13;
        private GroupBox groupBox6;
        private Controls.eNumericTextBox ntxtCover;
        private Label label12;
        private Label label8;
        private ComboBox cbxExposureType;
        private PictureBox pictureBox1;
       
    }
}