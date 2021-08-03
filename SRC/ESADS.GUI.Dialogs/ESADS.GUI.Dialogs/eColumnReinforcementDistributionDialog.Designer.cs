namespace ESADS.GUI
{
    partial class eColumnReinforcementDistributionDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(eColumnReinforcementDistributionDialog));
            this.gbxNumberOfBars = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ntxtBarsInYDirxn = new ESADS.GUI.Controls.eNumericTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.ntxtBarsInXdirxn = new ESADS.GUI.Controls.eNumericTextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCacel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnType4 = new System.Windows.Forms.RadioButton();
            this.rbtnType3 = new System.Windows.Forms.RadioButton();
            this.rbtnType2 = new System.Windows.Forms.RadioButton();
            this.rbtnType1 = new System.Windows.Forms.RadioButton();
            this.pbxType4 = new System.Windows.Forms.PictureBox();
            this.pbxType2 = new System.Windows.Forms.PictureBox();
            this.pbxType1 = new System.Windows.Forms.PictureBox();
            this.pbxType3 = new System.Windows.Forms.PictureBox();
            this.gbxNumberOfBars.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxType4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxType2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxType1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxType3)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxNumberOfBars
            // 
            this.gbxNumberOfBars.Controls.Add(this.label13);
            this.gbxNumberOfBars.Controls.Add(this.ntxtBarsInYDirxn);
            this.gbxNumberOfBars.Controls.Add(this.label12);
            this.gbxNumberOfBars.Controls.Add(this.ntxtBarsInXdirxn);
            this.gbxNumberOfBars.Enabled = false;
            this.gbxNumberOfBars.Location = new System.Drawing.Point(0, 201);
            this.gbxNumberOfBars.Name = "gbxNumberOfBars";
            this.gbxNumberOfBars.Size = new System.Drawing.Size(298, 60);
            this.gbxNumberOfBars.TabIndex = 19;
            this.gbxNumberOfBars.TabStop = false;
            this.gbxNumberOfBars.Text = "Number of Bars";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(61, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Horizontally";
            // 
            // ntxtBarsInYDirxn
            // 
            this.ntxtBarsInYDirxn.AutomaticResize = false;
            this.ntxtBarsInYDirxn.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtBarsInYDirxn.DoubleValue = 4D;
            this.ntxtBarsInYDirxn.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtBarsInYDirxn.IntValue = 4;
            this.ntxtBarsInYDirxn.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtBarsInYDirxn.Location = new System.Drawing.Point(236, 22);
            this.ntxtBarsInYDirxn.Measurment = ESADS.GUI.Controls.eMeasurment.None;
            this.ntxtBarsInYDirxn.Name = "ntxtBarsInYDirxn";
            this.ntxtBarsInYDirxn.Size = new System.Drawing.Size(47, 20);
            this.ntxtBarsInYDirxn.SU = 4D;
            this.ntxtBarsInYDirxn.TabIndex = 17;
            this.ntxtBarsInYDirxn.Text = "4";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(173, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "Vertically";
            // 
            // ntxtBarsInXdirxn
            // 
            this.ntxtBarsInXdirxn.AutomaticResize = false;
            this.ntxtBarsInXdirxn.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtBarsInXdirxn.DoubleValue = 3D;
            this.ntxtBarsInXdirxn.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtBarsInXdirxn.IntValue = 3;
            this.ntxtBarsInXdirxn.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtBarsInXdirxn.Location = new System.Drawing.Point(93, 22);
            this.ntxtBarsInXdirxn.Measurment = ESADS.GUI.Controls.eMeasurment.None;
            this.ntxtBarsInXdirxn.Name = "ntxtBarsInXdirxn";
            this.ntxtBarsInXdirxn.Size = new System.Drawing.Size(47, 20);
            this.ntxtBarsInXdirxn.SU = 3D;
            this.ntxtBarsInXdirxn.TabIndex = 16;
            this.ntxtBarsInXdirxn.Text = "3";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(310, 222);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCacel
            // 
            this.btnCacel.Location = new System.Drawing.Point(399, 222);
            this.btnCacel.Name = "btnCacel";
            this.btnCacel.Size = new System.Drawing.Size(73, 23);
            this.btnCacel.TabIndex = 22;
            this.btnCacel.Text = "Cancel";
            this.btnCacel.UseVisualStyleBackColor = true;
            this.btnCacel.Click += new System.EventHandler(this.btnCacel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnType4);
            this.groupBox1.Controls.Add(this.pbxType4);
            this.groupBox1.Controls.Add(this.rbtnType3);
            this.groupBox1.Controls.Add(this.rbtnType2);
            this.groupBox1.Controls.Add(this.rbtnType1);
            this.groupBox1.Controls.Add(this.pbxType2);
            this.groupBox1.Controls.Add(this.pbxType1);
            this.groupBox1.Controls.Add(this.pbxType3);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 192);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Types ";
            // 
            // rbtnType4
            // 
            this.rbtnType4.AutoSize = true;
            this.rbtnType4.Location = new System.Drawing.Point(375, 160);
            this.rbtnType4.Name = "rbtnType4";
            this.rbtnType4.Size = new System.Drawing.Size(55, 17);
            this.rbtnType4.TabIndex = 25;
            this.rbtnType4.Text = "Type4";
            this.rbtnType4.UseVisualStyleBackColor = true;
            this.rbtnType4.CheckedChanged += new System.EventHandler(this.rbtnType4_CheckedChanged);
            // 
            // rbtnType3
            // 
            this.rbtnType3.AutoSize = true;
            this.rbtnType3.Location = new System.Drawing.Point(257, 160);
            this.rbtnType3.Name = "rbtnType3";
            this.rbtnType3.Size = new System.Drawing.Size(55, 17);
            this.rbtnType3.TabIndex = 23;
            this.rbtnType3.Text = "Type3";
            this.rbtnType3.UseVisualStyleBackColor = true;
            this.rbtnType3.CheckedChanged += new System.EventHandler(this.rbtnType3_CheckedChanged);
            // 
            // rbtnType2
            // 
            this.rbtnType2.AutoSize = true;
            this.rbtnType2.Location = new System.Drawing.Point(143, 160);
            this.rbtnType2.Name = "rbtnType2";
            this.rbtnType2.Size = new System.Drawing.Size(55, 17);
            this.rbtnType2.TabIndex = 22;
            this.rbtnType2.Text = "Type2";
            this.rbtnType2.UseVisualStyleBackColor = true;
            // 
            // rbtnType1
            // 
            this.rbtnType1.AutoSize = true;
            this.rbtnType1.Checked = true;
            this.rbtnType1.Location = new System.Drawing.Point(33, 160);
            this.rbtnType1.Name = "rbtnType1";
            this.rbtnType1.Size = new System.Drawing.Size(55, 17);
            this.rbtnType1.TabIndex = 21;
            this.rbtnType1.TabStop = true;
            this.rbtnType1.Text = "Type1";
            this.rbtnType1.UseVisualStyleBackColor = true;
            // 
            // pbxType4
            // 
            this.pbxType4.Image = ((System.Drawing.Image)(resources.GetObject("pbxType4.Image")));
            this.pbxType4.Location = new System.Drawing.Point(354, 19);
            this.pbxType4.Name = "pbxType4";
            this.pbxType4.Size = new System.Drawing.Size(101, 126);
            this.pbxType4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxType4.TabIndex = 24;
            this.pbxType4.TabStop = false;
            this.pbxType4.Click += new System.EventHandler(this.pbxType4_Click);
            // 
            // pbxType2
            // 
            this.pbxType2.Image = ((System.Drawing.Image)(resources.GetObject("pbxType2.Image")));
            this.pbxType2.Location = new System.Drawing.Point(121, 19);
            this.pbxType2.Name = "pbxType2";
            this.pbxType2.Size = new System.Drawing.Size(101, 126);
            this.pbxType2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxType2.TabIndex = 18;
            this.pbxType2.TabStop = false;
            this.pbxType2.Click += new System.EventHandler(this.pbxType2_Click);
            // 
            // pbxType1
            // 
            this.pbxType1.Image = ((System.Drawing.Image)(resources.GetObject("pbxType1.Image")));
            this.pbxType1.Location = new System.Drawing.Point(6, 19);
            this.pbxType1.Name = "pbxType1";
            this.pbxType1.Size = new System.Drawing.Size(101, 126);
            this.pbxType1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxType1.TabIndex = 17;
            this.pbxType1.TabStop = false;
            this.pbxType1.Click += new System.EventHandler(this.pbxType1_Click);
            // 
            // pbxType3
            // 
            this.pbxType3.Image = ((System.Drawing.Image)(resources.GetObject("pbxType3.Image")));
            this.pbxType3.Location = new System.Drawing.Point(237, 19);
            this.pbxType3.Name = "pbxType3";
            this.pbxType3.Size = new System.Drawing.Size(101, 126);
            this.pbxType3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxType3.TabIndex = 20;
            this.pbxType3.TabStop = false;
            this.pbxType3.Click += new System.EventHandler(this.pbxType3_Click);
            // 
            // eColumnReinforcementDistributionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 275);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCacel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbxNumberOfBars);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eColumnReinforcementDistributionDialog";
            this.ShowInTaskbar = false;
            this.Text = "Distribution Types";
            this.gbxNumberOfBars.ResumeLayout(false);
            this.gbxNumberOfBars.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxType4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxType2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxType1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxType3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxType1;
        private System.Windows.Forms.PictureBox pbxType2;
        private System.Windows.Forms.GroupBox gbxNumberOfBars;
        private System.Windows.Forms.Label label13;
        private Controls.eNumericTextBox ntxtBarsInYDirxn;
        private System.Windows.Forms.Label label12;
        private Controls.eNumericTextBox ntxtBarsInXdirxn;
        private System.Windows.Forms.PictureBox pbxType3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCacel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnType1;
        private System.Windows.Forms.RadioButton rbtnType3;
        private System.Windows.Forms.RadioButton rbtnType2;
        private System.Windows.Forms.RadioButton rbtnType4;
        private System.Windows.Forms.PictureBox pbxType4;
    }
}