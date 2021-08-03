namespace ESADS.GUI
{
    partial class eAssignBeamJointLoadDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(eAssignBeamJointLoadDialog));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radRemoveAll = new System.Windows.Forms.RadioButton();
            this.radReplaceExisting = new System.Windows.Forms.RadioButton();
            this.radAddToExisting = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxForceUnit = new System.Windows.Forms.ComboBox();
            this.cbxLengthUnit = new System.Windows.Forms.ComboBox();
            this.ntxtMagnitude = new ESADS.GUI.Controls.eNumericTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.gbDistributionPatern = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pbxMoment = new System.Windows.Forms.PictureBox();
            this.pbxConcentratedForce = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAssign = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkFactored = new System.Windows.Forms.CheckBox();
            this.cbxLoadType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.gbDistributionPatern.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxMoment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxConcentratedForce)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radRemoveAll);
            this.groupBox2.Controls.Add(this.radReplaceExisting);
            this.groupBox2.Controls.Add(this.radAddToExisting);
            this.groupBox2.Location = new System.Drawing.Point(217, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(214, 151);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // radRemoveAll
            // 
            this.radRemoveAll.AutoSize = true;
            this.radRemoveAll.Location = new System.Drawing.Point(6, 65);
            this.radRemoveAll.Name = "radRemoveAll";
            this.radRemoveAll.Size = new System.Drawing.Size(111, 17);
            this.radRemoveAll.TabIndex = 1;
            this.radRemoveAll.Text = "Remove All Loads";
            this.radRemoveAll.UseVisualStyleBackColor = true;
            // 
            // radReplaceExisting
            // 
            this.radReplaceExisting.AutoSize = true;
            this.radReplaceExisting.Location = new System.Drawing.Point(6, 42);
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
            this.radAddToExisting.Location = new System.Drawing.Point(6, 19);
            this.radAddToExisting.Name = "radAddToExisting";
            this.radAddToExisting.Size = new System.Drawing.Size(122, 17);
            this.radAddToExisting.TabIndex = 0;
            this.radAddToExisting.TabStop = true;
            this.radAddToExisting.Text = "Add to Existing Load";
            this.radAddToExisting.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.cbxForceUnit);
            this.groupBox5.Controls.Add(this.cbxLengthUnit);
            this.groupBox5.Location = new System.Drawing.Point(217, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(214, 79);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Units";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(69, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Force";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Length";
            // 
            // cbxForceUnit
            // 
            this.cbxForceUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxForceUnit.FormattingEnabled = true;
            this.cbxForceUnit.Location = new System.Drawing.Point(109, 20);
            this.cbxForceUnit.Name = "cbxForceUnit";
            this.cbxForceUnit.Size = new System.Drawing.Size(99, 21);
            this.cbxForceUnit.TabIndex = 7;
            this.cbxForceUnit.SelectedIndexChanged += new System.EventHandler(this.cbxForceUnit_SelectedIndexChanged);
            // 
            // cbxLengthUnit
            // 
            this.cbxLengthUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLengthUnit.FormattingEnabled = true;
            this.cbxLengthUnit.Location = new System.Drawing.Point(109, 49);
            this.cbxLengthUnit.Name = "cbxLengthUnit";
            this.cbxLengthUnit.Size = new System.Drawing.Size(99, 21);
            this.cbxLengthUnit.TabIndex = 6;
            this.cbxLengthUnit.SelectedIndexChanged += new System.EventHandler(this.cbxLengthUnit_SelectedIndexChanged);
            // 
            // ntxtMagnitude
            // 
            this.ntxtMagnitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ntxtMagnitude.AutomaticResize = false;
            this.ntxtMagnitude.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtMagnitude.DoubleValue = 0D;
            this.ntxtMagnitude.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtMagnitude.IntValue = 0;
            this.ntxtMagnitude.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtMagnitude.Location = new System.Drawing.Point(96, 122);
            this.ntxtMagnitude.Measurment = ESADS.GUI.Controls.eMeasurment.Force;
            this.ntxtMagnitude.Name = "ntxtMagnitude";
            this.ntxtMagnitude.Size = new System.Drawing.Size(96, 20);
            this.ntxtMagnitude.SU = 0D;
            this.ntxtMagnitude.TabIndex = 19;
            this.ntxtMagnitude.Text = "0";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Magnitude";
            // 
            // gbDistributionPatern
            // 
            this.gbDistributionPatern.Controls.Add(this.ntxtMagnitude);
            this.gbDistributionPatern.Controls.Add(this.label8);
            this.gbDistributionPatern.Controls.Add(this.label4);
            this.gbDistributionPatern.Controls.Add(this.label3);
            this.gbDistributionPatern.Controls.Add(this.pbxMoment);
            this.gbDistributionPatern.Controls.Add(this.pbxConcentratedForce);
            this.gbDistributionPatern.Location = new System.Drawing.Point(6, 97);
            this.gbDistributionPatern.Name = "gbDistributionPatern";
            this.gbDistributionPatern.Size = new System.Drawing.Size(205, 151);
            this.gbDistributionPatern.TabIndex = 18;
            this.gbDistributionPatern.TabStop = false;
            this.gbDistributionPatern.Text = "Load Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(107, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 26);
            this.label4.TabIndex = 13;
            this.label4.Text = "Concentrated\r\nMoment";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 26);
            this.label3.TabIndex = 13;
            this.label3.Text = "Concentrated\r\nForce";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbxMoment
            // 
            this.pbxMoment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxMoment.Image = ((System.Drawing.Image)(resources.GetObject("pbxMoment.Image")));
            this.pbxMoment.Location = new System.Drawing.Point(103, 19);
            this.pbxMoment.Name = "pbxMoment";
            this.pbxMoment.Size = new System.Drawing.Size(75, 63);
            this.pbxMoment.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxMoment.TabIndex = 12;
            this.pbxMoment.TabStop = false;
            this.pbxMoment.Click += new System.EventHandler(this.pbxMoment_Click);
            // 
            // pbxConcentratedForce
            // 
            this.pbxConcentratedForce.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxConcentratedForce.Image = ((System.Drawing.Image)(resources.GetObject("pbxConcentratedForce.Image")));
            this.pbxConcentratedForce.Location = new System.Drawing.Point(28, 19);
            this.pbxConcentratedForce.Name = "pbxConcentratedForce";
            this.pbxConcentratedForce.Size = new System.Drawing.Size(52, 63);
            this.pbxConcentratedForce.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxConcentratedForce.TabIndex = 7;
            this.pbxConcentratedForce.TabStop = false;
            this.pbxConcentratedForce.Click += new System.EventHandler(this.pbxConcentratedForce_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(356, 260);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnAssign
            // 
            this.btnAssign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAssign.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAssign.Location = new System.Drawing.Point(275, 260);
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.Size = new System.Drawing.Size(75, 23);
            this.btnAssign.TabIndex = 19;
            this.btnAssign.Text = "Assign";
            this.btnAssign.UseVisualStyleBackColor = true;
            this.btnAssign.Click += new System.EventHandler(this.btnAssign_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkFactored);
            this.groupBox1.Controls.Add(this.cbxLoadType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 79);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Action ";
            // 
            // chkFactored
            // 
            this.chkFactored.AutoSize = true;
            this.chkFactored.Location = new System.Drawing.Point(58, 48);
            this.chkFactored.Name = "chkFactored";
            this.chkFactored.Size = new System.Drawing.Size(120, 17);
            this.chkFactored.TabIndex = 18;
            this.chkFactored.Text = "The load is factored";
            this.chkFactored.UseVisualStyleBackColor = true;
            // 
            // cbxLoadType
            // 
            this.cbxLoadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLoadType.FormattingEnabled = true;
            this.cbxLoadType.Location = new System.Drawing.Point(76, 19);
            this.cbxLoadType.Name = "cbxLoadType";
            this.cbxLoadType.Size = new System.Drawing.Size(116, 21);
            this.cbxLoadType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Action Type";
            // 
            // eAssignBeamJointLoadDialog
            // 
            this.AcceptButton = this.btnAssign;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(443, 295);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAssign);
            this.Controls.Add(this.gbDistributionPatern);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eAssignBeamJointLoadDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assign Beam Joint Load";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.gbDistributionPatern.ResumeLayout(false);
            this.gbDistributionPatern.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxMoment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxConcentratedForce)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radReplaceExisting;
        private System.Windows.Forms.RadioButton radAddToExisting;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxForceUnit;
        private System.Windows.Forms.ComboBox cbxLengthUnit;
        private GUI.Controls.eNumericTextBox ntxtMagnitude;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox gbDistributionPatern;
        private System.Windows.Forms.PictureBox pbxMoment;
        private System.Windows.Forms.PictureBox pbxConcentratedForce;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAssign;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkFactored;
        private System.Windows.Forms.ComboBox cbxLoadType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radRemoveAll;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}