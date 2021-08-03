namespace ESADS.GUI
{
    partial class eAssignJointDialog
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
            this.picbxContinuous = new System.Windows.Forms.PictureBox();
            this.picbxVRoller = new System.Windows.Forms.PictureBox();
            this.picbxHinge = new System.Windows.Forms.PictureBox();
            this.picbxFixed = new System.Windows.Forms.PictureBox();
            this.radContinuous = new System.Windows.Forms.RadioButton();
            this.radVRoller = new System.Windows.Forms.RadioButton();
            this.picbxHRoller = new System.Windows.Forms.PictureBox();
            this.radHinge = new System.Windows.Forms.RadioButton();
            this.picbxPin = new System.Windows.Forms.PictureBox();
            this.radFixed = new System.Windows.Forms.RadioButton();
            this.radHRoller = new System.Windows.Forms.RadioButton();
            this.radPin = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.grpbxDescription = new System.Windows.Forms.GroupBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ntxtSupportWidth = new ESADS.GUI.Controls.eNumericTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbxContinuous)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbxVRoller)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbxHinge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbxFixed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbxHRoller)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbxPin)).BeginInit();
            this.grpbxDescription.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picbxContinuous);
            this.groupBox1.Controls.Add(this.picbxVRoller);
            this.groupBox1.Controls.Add(this.picbxHinge);
            this.groupBox1.Controls.Add(this.picbxFixed);
            this.groupBox1.Controls.Add(this.radContinuous);
            this.groupBox1.Controls.Add(this.radVRoller);
            this.groupBox1.Controls.Add(this.picbxHRoller);
            this.groupBox1.Controls.Add(this.radHinge);
            this.groupBox1.Controls.Add(this.picbxPin);
            this.groupBox1.Controls.Add(this.radFixed);
            this.groupBox1.Controls.Add(this.radHRoller);
            this.groupBox1.Controls.Add(this.radPin);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 195);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Joint type to be assigned";
            // 
            // picbxContinuous
            // 
            this.picbxContinuous.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picbxContinuous.Location = new System.Drawing.Point(209, 102);
            this.picbxContinuous.Name = "picbxContinuous";
            this.picbxContinuous.Size = new System.Drawing.Size(71, 54);
            this.picbxContinuous.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picbxContinuous.TabIndex = 1;
            this.picbxContinuous.TabStop = false;
            this.picbxContinuous.Click += new System.EventHandler(this.picbxContinuous_Click);
            // 
            // picbxVRoller
            // 
            this.picbxVRoller.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picbxVRoller.Image = global::ESADS.GUI.Properties.Resources.vertical_roller;
            this.picbxVRoller.Location = new System.Drawing.Point(113, 102);
            this.picbxVRoller.Name = "picbxVRoller";
            this.picbxVRoller.Size = new System.Drawing.Size(71, 54);
            this.picbxVRoller.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picbxVRoller.TabIndex = 1;
            this.picbxVRoller.TabStop = false;
            this.picbxVRoller.Click += new System.EventHandler(this.picbxVRoller_Click);
            // 
            // picbxHinge
            // 
            this.picbxHinge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picbxHinge.Image = global::ESADS.GUI.Properties.Resources.hinge;
            this.picbxHinge.Location = new System.Drawing.Point(212, 19);
            this.picbxHinge.Name = "picbxHinge";
            this.picbxHinge.Size = new System.Drawing.Size(71, 54);
            this.picbxHinge.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picbxHinge.TabIndex = 1;
            this.picbxHinge.TabStop = false;
            this.picbxHinge.Click += new System.EventHandler(this.picbxHinge_Click);
            // 
            // picbxFixed
            // 
            this.picbxFixed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picbxFixed.Image = global::ESADS.GUI.Properties.Resources._fixed;
            this.picbxFixed.Location = new System.Drawing.Point(113, 19);
            this.picbxFixed.Name = "picbxFixed";
            this.picbxFixed.Size = new System.Drawing.Size(71, 54);
            this.picbxFixed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picbxFixed.TabIndex = 1;
            this.picbxFixed.TabStop = false;
            this.picbxFixed.Click += new System.EventHandler(this.picbxFixed_Click);
            // 
            // radContinuous
            // 
            this.radContinuous.AutoSize = true;
            this.radContinuous.Location = new System.Drawing.Point(206, 162);
            this.radContinuous.Name = "radContinuous";
            this.radContinuous.Size = new System.Drawing.Size(78, 17);
            this.radContinuous.TabIndex = 0;
            this.radContinuous.Text = "Continuous";
            this.radContinuous.UseVisualStyleBackColor = true;
            this.radContinuous.CheckedChanged += new System.EventHandler(this.radContinuous_CheckedChanged);
            // 
            // radVRoller
            // 
            this.radVRoller.AutoSize = true;
            this.radVRoller.Location = new System.Drawing.Point(110, 162);
            this.radVRoller.Name = "radVRoller";
            this.radVRoller.Size = new System.Drawing.Size(90, 17);
            this.radVRoller.TabIndex = 0;
            this.radVRoller.Text = "Vertical Roller";
            this.radVRoller.UseVisualStyleBackColor = true;
            this.radVRoller.CheckedChanged += new System.EventHandler(this.radVRoller_CheckedChanged);
            // 
            // picbxHRoller
            // 
            this.picbxHRoller.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picbxHRoller.Image = global::ESADS.GUI.Properties.Resources.roller;
            this.picbxHRoller.Location = new System.Drawing.Point(6, 102);
            this.picbxHRoller.Name = "picbxHRoller";
            this.picbxHRoller.Size = new System.Drawing.Size(71, 54);
            this.picbxHRoller.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picbxHRoller.TabIndex = 1;
            this.picbxHRoller.TabStop = false;
            this.picbxHRoller.Click += new System.EventHandler(this.picbxHRoller_Click);
            // 
            // radHinge
            // 
            this.radHinge.AutoSize = true;
            this.radHinge.Location = new System.Drawing.Point(221, 79);
            this.radHinge.Name = "radHinge";
            this.radHinge.Size = new System.Drawing.Size(53, 17);
            this.radHinge.TabIndex = 0;
            this.radHinge.Text = "Hinge";
            this.radHinge.UseVisualStyleBackColor = true;
            this.radHinge.CheckedChanged += new System.EventHandler(this.radHinge_CheckedChanged);
            // 
            // picbxPin
            // 
            this.picbxPin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picbxPin.Image = global::ESADS.GUI.Properties.Resources.pin1;
            this.picbxPin.Location = new System.Drawing.Point(6, 19);
            this.picbxPin.Name = "picbxPin";
            this.picbxPin.Size = new System.Drawing.Size(71, 54);
            this.picbxPin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picbxPin.TabIndex = 1;
            this.picbxPin.TabStop = false;
            this.picbxPin.Click += new System.EventHandler(this.picbxPin_Click);
            // 
            // radFixed
            // 
            this.radFixed.AutoSize = true;
            this.radFixed.Location = new System.Drawing.Point(122, 79);
            this.radFixed.Name = "radFixed";
            this.radFixed.Size = new System.Drawing.Size(50, 17);
            this.radFixed.TabIndex = 0;
            this.radFixed.Text = "Fixed";
            this.radFixed.UseVisualStyleBackColor = true;
            this.radFixed.CheckedChanged += new System.EventHandler(this.radFixed_CheckedChanged);
            // 
            // radHRoller
            // 
            this.radHRoller.AutoSize = true;
            this.radHRoller.Location = new System.Drawing.Point(6, 162);
            this.radHRoller.Name = "radHRoller";
            this.radHRoller.Size = new System.Drawing.Size(102, 17);
            this.radHRoller.TabIndex = 0;
            this.radHRoller.Text = "Horizontal Roller";
            this.radHRoller.UseVisualStyleBackColor = true;
            this.radHRoller.CheckedChanged += new System.EventHandler(this.radHRoller_CheckedChanged);
            // 
            // radPin
            // 
            this.radPin.AutoSize = true;
            this.radPin.Checked = true;
            this.radPin.Location = new System.Drawing.Point(23, 79);
            this.radPin.Name = "radPin";
            this.radPin.Size = new System.Drawing.Size(40, 17);
            this.radPin.TabIndex = 0;
            this.radPin.TabStop = true;
            this.radPin.Text = "Pin";
            this.radPin.UseVisualStyleBackColor = true;
            this.radPin.CheckedChanged += new System.EventHandler(this.radPin_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(250, 341);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(169, 341);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // grpbxDescription
            // 
            this.grpbxDescription.Controls.Add(this.lblDescription);
            this.grpbxDescription.Location = new System.Drawing.Point(12, 270);
            this.grpbxDescription.Name = "grpbxDescription";
            this.grpbxDescription.Size = new System.Drawing.Size(310, 65);
            this.grpbxDescription.TabIndex = 2;
            this.grpbxDescription.TabStop = false;
            this.grpbxDescription.Text = "Description";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(11, 16);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(182, 13);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "Select a joint from the above choices";
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHelp.Location = new System.Drawing.Point(12, 341);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 3;
            this.btnHelp.Text = "&Help...";
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.ntxtSupportWidth);
            this.groupBox2.Location = new System.Drawing.Point(12, 213);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(310, 51);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Support Width";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Width of the selected joint type";
            // 
            // ntxtSupportWidth
            // 
            this.ntxtSupportWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ntxtSupportWidth.AutomaticResize = false;
            this.ntxtSupportWidth.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtSupportWidth.DoubleValue = 0D;
            this.ntxtSupportWidth.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtSupportWidth.IntValue = 0;
            this.ntxtSupportWidth.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtSupportWidth.Location = new System.Drawing.Point(204, 19);
            this.ntxtSupportWidth.Measurment = ESADS.GUI.Controls.eMeasurment.None;
            this.ntxtSupportWidth.Name = "ntxtSupportWidth";
            this.ntxtSupportWidth.Size = new System.Drawing.Size(100, 20);
            this.ntxtSupportWidth.SU = 0D;
            this.ntxtSupportWidth.TabIndex = 1;
            this.ntxtSupportWidth.Text = "0";
            // 
            // eAssignJointDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(337, 376);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.grpbxDescription);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eAssignJointDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assign Joint";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbxContinuous)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbxVRoller)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbxHinge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbxFixed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbxHRoller)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbxPin)).EndInit();
            this.grpbxDescription.ResumeLayout(false);
            this.grpbxDescription.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox picbxVRoller;
        private System.Windows.Forms.PictureBox picbxHinge;
        private System.Windows.Forms.PictureBox picbxFixed;
        private System.Windows.Forms.RadioButton radVRoller;
        private System.Windows.Forms.PictureBox picbxHRoller;
        private System.Windows.Forms.RadioButton radHinge;
        private System.Windows.Forms.PictureBox picbxPin;
        private System.Windows.Forms.RadioButton radFixed;
        private System.Windows.Forms.RadioButton radHRoller;
        private System.Windows.Forms.RadioButton radPin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox grpbxDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private Controls.eNumericTextBox ntxtSupportWidth;
        private System.Windows.Forms.PictureBox picbxContinuous;
        private System.Windows.Forms.RadioButton radContinuous;

    }
}