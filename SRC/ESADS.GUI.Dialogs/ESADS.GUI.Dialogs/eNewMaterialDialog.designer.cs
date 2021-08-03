namespace ESADS.GUI
{
    partial class eNewMaterialDialog
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.GroupBox grbxGeneral;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.GroupBox grpbxMethodOfInput;
            this.txtMaterialName = new System.Windows.Forms.TextBox();
            this.cmbxGrade = new System.Windows.Forms.ComboBox();
            this.chbxUsePredefinedGrades = new System.Windows.Forms.CheckBox();
            this.grbxGeneralPhysicalProperties = new System.Windows.Forms.GroupBox();
            this.ntxtModulusOfElasticity = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtUnitWeight = new ESADS.GUI.Controls.eNumericTextBox();
            this.cmbxFroceUnit = new System.Windows.Forms.ComboBox();
            this.cmbxLengthUnit = new System.Windows.Forms.ComboBox();
            this.grbxConcSpecificProperties = new System.Windows.Forms.GroupBox();
            this.ntxtCharConcTensileStrength = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtCharConcCompStrength = new ESADS.GUI.Controls.eNumericTextBox();
            this.grbxSteelSpecificProperties = new System.Windows.Forms.GroupBox();
            this.ntxtSteelYieldStrength = new ESADS.GUI.Controls.eNumericTextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            grbxGeneral = new System.Windows.Forms.GroupBox();
            label6 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            grpbxMethodOfInput = new System.Windows.Forms.GroupBox();
            grbxGeneral.SuspendLayout();
            grpbxMethodOfInput.SuspendLayout();
            this.grbxGeneralPhysicalProperties.SuspendLayout();
            this.grbxConcSpecificProperties.SuspendLayout();
            this.grbxSteelSpecificProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(42, 16);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(107, 13);
            label1.TabIndex = 0;
            label1.Text = "Name of the material:";
            // 
            // grbxGeneral
            // 
            grbxGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            grbxGeneral.Controls.Add(this.txtMaterialName);
            grbxGeneral.Controls.Add(label1);
            grbxGeneral.Location = new System.Drawing.Point(12, 12);
            grbxGeneral.Name = "grbxGeneral";
            grbxGeneral.Size = new System.Drawing.Size(282, 41);
            grbxGeneral.TabIndex = 0;
            grbxGeneral.TabStop = false;
            grbxGeneral.Text = "General";
            // 
            // txtMaterialName
            // 
            this.txtMaterialName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaterialName.Location = new System.Drawing.Point(155, 13);
            this.txtMaterialName.Name = "txtMaterialName";
            this.txtMaterialName.Size = new System.Drawing.Size(121, 20);
            this.txtMaterialName.TabIndex = 1;
            // 
            // label6
            // 
            label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(41, 54);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(108, 13);
            label6.TabIndex = 0;
            label6.Text = "Modulus Of Elasticity:";
            // 
            // label3
            // 
            label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(83, 28);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(66, 13);
            label3.TabIndex = 0;
            label3.Text = "Unit Weight:";
            // 
            // label8
            // 
            label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(62, 54);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(87, 26);
            label8.TabIndex = 0;
            label8.Text = "    Characterstic \r\nTensile Strength:";
            // 
            // label9
            // 
            label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(35, 19);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(114, 26);
            label9.TabIndex = 0;
            label9.Text = "              Characterstic \r\nCompressive Strength:";
            // 
            // label11
            // 
            label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(2, 28);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(147, 13);
            label11.TabIndex = 0;
            label11.Text = "  Characterstic Yield Strength:";
            // 
            // grpbxMethodOfInput
            // 
            grpbxMethodOfInput.Controls.Add(this.cmbxGrade);
            grpbxMethodOfInput.Controls.Add(this.chbxUsePredefinedGrades);
            grpbxMethodOfInput.Location = new System.Drawing.Point(12, 61);
            grpbxMethodOfInput.Name = "grpbxMethodOfInput";
            grpbxMethodOfInput.Size = new System.Drawing.Size(282, 75);
            grpbxMethodOfInput.TabIndex = 6;
            grpbxMethodOfInput.TabStop = false;
            grpbxMethodOfInput.Text = "Method Of Input";
            // 
            // cmbxGrade
            // 
            this.cmbxGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxGrade.Enabled = false;
            this.cmbxGrade.FormattingEnabled = true;
            this.cmbxGrade.Location = new System.Drawing.Point(155, 42);
            this.cmbxGrade.Name = "cmbxGrade";
            this.cmbxGrade.Size = new System.Drawing.Size(121, 21);
            this.cmbxGrade.TabIndex = 1;
            this.cmbxGrade.SelectedIndexChanged += new System.EventHandler(this.cmbxGrade_SelectedIndexChanged);
            // 
            // chbxUsePredefinedGrades
            // 
            this.chbxUsePredefinedGrades.AutoSize = true;
            this.chbxUsePredefinedGrades.Location = new System.Drawing.Point(6, 19);
            this.chbxUsePredefinedGrades.Name = "chbxUsePredefinedGrades";
            this.chbxUsePredefinedGrades.Size = new System.Drawing.Size(234, 17);
            this.chbxUsePredefinedGrades.TabIndex = 0;
            this.chbxUsePredefinedGrades.Text = "Use predefined grades to define the material";
            this.chbxUsePredefinedGrades.UseVisualStyleBackColor = true;
            this.chbxUsePredefinedGrades.CheckedChanged += new System.EventHandler(this.chbxMethodOfInput_CheckedChanged);
            // 
            // grbxGeneralPhysicalProperties
            // 
            this.grbxGeneralPhysicalProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grbxGeneralPhysicalProperties.Controls.Add(this.ntxtModulusOfElasticity);
            this.grbxGeneralPhysicalProperties.Controls.Add(this.ntxtUnitWeight);
            this.grbxGeneralPhysicalProperties.Controls.Add(label6);
            this.grbxGeneralPhysicalProperties.Controls.Add(label3);
            this.grbxGeneralPhysicalProperties.Location = new System.Drawing.Point(12, 142);
            this.grbxGeneralPhysicalProperties.Name = "grbxGeneralPhysicalProperties";
            this.grbxGeneralPhysicalProperties.Size = new System.Drawing.Size(282, 81);
            this.grbxGeneralPhysicalProperties.TabIndex = 2;
            this.grbxGeneralPhysicalProperties.TabStop = false;
            this.grbxGeneralPhysicalProperties.Text = "General Physical Properties";
            // 
            // ntxtModulusOfElasticity
            // 
            this.ntxtModulusOfElasticity.AutomaticResize = false;
            this.ntxtModulusOfElasticity.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtModulusOfElasticity.DoubleValue = 0D;
            this.ntxtModulusOfElasticity.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtModulusOfElasticity.IntValue = 0;
            this.ntxtModulusOfElasticity.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtModulusOfElasticity.Location = new System.Drawing.Point(155, 51);
            this.ntxtModulusOfElasticity.Measurment = ESADS.GUI.Controls.eMeasurment.Stress;
            this.ntxtModulusOfElasticity.Name = "ntxtModulusOfElasticity";
            this.ntxtModulusOfElasticity.Size = new System.Drawing.Size(121, 20);
            this.ntxtModulusOfElasticity.SU = 0D;
            this.ntxtModulusOfElasticity.TabIndex = 1;
            this.ntxtModulusOfElasticity.Text = "0";
            // 
            // ntxtUnitWeight
            // 
            this.ntxtUnitWeight.AutomaticResize = false;
            this.ntxtUnitWeight.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtUnitWeight.DoubleValue = 0D;
            this.ntxtUnitWeight.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtUnitWeight.IntValue = 0;
            this.ntxtUnitWeight.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtUnitWeight.Location = new System.Drawing.Point(155, 25);
            this.ntxtUnitWeight.Measurment = ESADS.GUI.Controls.eMeasurment.UnitWeight;
            this.ntxtUnitWeight.Name = "ntxtUnitWeight";
            this.ntxtUnitWeight.Size = new System.Drawing.Size(121, 20);
            this.ntxtUnitWeight.SU = 0D;
            this.ntxtUnitWeight.TabIndex = 1;
            this.ntxtUnitWeight.Text = "0";
            // 
            // cmbxFroceUnit
            // 
            this.cmbxFroceUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbxFroceUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxFroceUnit.FormattingEnabled = true;
            this.cmbxFroceUnit.Location = new System.Drawing.Point(58, 348);
            this.cmbxFroceUnit.Name = "cmbxFroceUnit";
            this.cmbxFroceUnit.Size = new System.Drawing.Size(40, 21);
            this.cmbxFroceUnit.TabIndex = 2;
            this.cmbxFroceUnit.SelectedIndexChanged += new System.EventHandler(this.cmbxFroceUnit_SelectedIndexChanged);
            // 
            // cmbxLengthUnit
            // 
            this.cmbxLengthUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbxLengthUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxLengthUnit.FormattingEnabled = true;
            this.cmbxLengthUnit.Location = new System.Drawing.Point(11, 348);
            this.cmbxLengthUnit.Name = "cmbxLengthUnit";
            this.cmbxLengthUnit.Size = new System.Drawing.Size(40, 21);
            this.cmbxLengthUnit.TabIndex = 1;
            this.cmbxLengthUnit.SelectedIndexChanged += new System.EventHandler(this.cmbxLengthUnit_SelectedIndexChanged);
            // 
            // grbxConcSpecificProperties
            // 
            this.grbxConcSpecificProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grbxConcSpecificProperties.Controls.Add(label9);
            this.grbxConcSpecificProperties.Controls.Add(this.ntxtCharConcTensileStrength);
            this.grbxConcSpecificProperties.Controls.Add(this.ntxtCharConcCompStrength);
            this.grbxConcSpecificProperties.Controls.Add(label8);
            this.grbxConcSpecificProperties.Location = new System.Drawing.Point(12, 229);
            this.grbxConcSpecificProperties.Name = "grbxConcSpecificProperties";
            this.grbxConcSpecificProperties.Size = new System.Drawing.Size(282, 100);
            this.grbxConcSpecificProperties.TabIndex = 3;
            this.grbxConcSpecificProperties.TabStop = false;
            this.grbxConcSpecificProperties.Text = "Properties Specific to selected material type";
            // 
            // ntxtCharConcTensileStrength
            // 
            this.ntxtCharConcTensileStrength.AutomaticResize = false;
            this.ntxtCharConcTensileStrength.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtCharConcTensileStrength.DoubleValue = 0D;
            this.ntxtCharConcTensileStrength.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtCharConcTensileStrength.IntValue = 0;
            this.ntxtCharConcTensileStrength.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtCharConcTensileStrength.Location = new System.Drawing.Point(155, 60);
            this.ntxtCharConcTensileStrength.Measurment = ESADS.GUI.Controls.eMeasurment.Stress;
            this.ntxtCharConcTensileStrength.Name = "ntxtCharConcTensileStrength";
            this.ntxtCharConcTensileStrength.Size = new System.Drawing.Size(121, 20);
            this.ntxtCharConcTensileStrength.SU = 0D;
            this.ntxtCharConcTensileStrength.TabIndex = 1;
            this.ntxtCharConcTensileStrength.Text = "0";
            // 
            // ntxtCharConcCompStrength
            // 
            this.ntxtCharConcCompStrength.AutomaticResize = false;
            this.ntxtCharConcCompStrength.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtCharConcCompStrength.DoubleValue = 0D;
            this.ntxtCharConcCompStrength.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtCharConcCompStrength.IntValue = 0;
            this.ntxtCharConcCompStrength.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtCharConcCompStrength.Location = new System.Drawing.Point(155, 25);
            this.ntxtCharConcCompStrength.Measurment = ESADS.GUI.Controls.eMeasurment.Stress;
            this.ntxtCharConcCompStrength.Name = "ntxtCharConcCompStrength";
            this.ntxtCharConcCompStrength.Size = new System.Drawing.Size(121, 20);
            this.ntxtCharConcCompStrength.SU = 0D;
            this.ntxtCharConcCompStrength.TabIndex = 1;
            this.ntxtCharConcCompStrength.Text = "0";
            // 
            // grbxSteelSpecificProperties
            // 
            this.grbxSteelSpecificProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grbxSteelSpecificProperties.Controls.Add(label11);
            this.grbxSteelSpecificProperties.Controls.Add(this.ntxtSteelYieldStrength);
            this.grbxSteelSpecificProperties.Location = new System.Drawing.Point(12, 229);
            this.grbxSteelSpecificProperties.Name = "grbxSteelSpecificProperties";
            this.grbxSteelSpecificProperties.Size = new System.Drawing.Size(282, 100);
            this.grbxSteelSpecificProperties.TabIndex = 3;
            this.grbxSteelSpecificProperties.TabStop = false;
            this.grbxSteelSpecificProperties.Text = "Properties Specific to selected material type";
            // 
            // ntxtSteelYieldStrength
            // 
            this.ntxtSteelYieldStrength.AutomaticResize = false;
            this.ntxtSteelYieldStrength.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtSteelYieldStrength.DoubleValue = 0D;
            this.ntxtSteelYieldStrength.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtSteelYieldStrength.IntValue = 0;
            this.ntxtSteelYieldStrength.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtSteelYieldStrength.Location = new System.Drawing.Point(155, 25);
            this.ntxtSteelYieldStrength.Measurment = ESADS.GUI.Controls.eMeasurment.Stress;
            this.ntxtSteelYieldStrength.Name = "ntxtSteelYieldStrength";
            this.ntxtSteelYieldStrength.Size = new System.Drawing.Size(121, 20);
            this.ntxtSteelYieldStrength.SU = 0D;
            this.ntxtSteelYieldStrength.TabIndex = 1;
            this.ntxtSteelYieldStrength.Text = "0";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(219, 346);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(138, 346);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // eNewMaterialDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(306, 381);
            this.Controls.Add(this.cmbxFroceUnit);
            this.Controls.Add(grpbxMethodOfInput);
            this.Controls.Add(this.cmbxLengthUnit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(grbxGeneral);
            this.Controls.Add(this.grbxGeneralPhysicalProperties);
            this.Controls.Add(this.grbxConcSpecificProperties);
            this.Controls.Add(this.grbxSteelSpecificProperties);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eNewMaterialDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Material";
            grbxGeneral.ResumeLayout(false);
            grbxGeneral.PerformLayout();
            grpbxMethodOfInput.ResumeLayout(false);
            grpbxMethodOfInput.PerformLayout();
            this.grbxGeneralPhysicalProperties.ResumeLayout(false);
            this.grbxGeneralPhysicalProperties.PerformLayout();
            this.grbxConcSpecificProperties.ResumeLayout(false);
            this.grbxConcSpecificProperties.PerformLayout();
            this.grbxSteelSpecificProperties.ResumeLayout(false);
            this.grbxSteelSpecificProperties.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtMaterialName;
        private System.Windows.Forms.ComboBox cmbxFroceUnit;
        private System.Windows.Forms.ComboBox cmbxLengthUnit;
        private System.Windows.Forms.GroupBox grbxConcSpecificProperties;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox grbxSteelSpecificProperties;
        private ESADS.GUI.Controls.eNumericTextBox ntxtModulusOfElasticity;
        private ESADS.GUI.Controls.eNumericTextBox ntxtUnitWeight;
        private ESADS.GUI.Controls.eNumericTextBox ntxtCharConcTensileStrength;
        private ESADS.GUI.Controls.eNumericTextBox ntxtCharConcCompStrength;
        private ESADS.GUI.Controls.eNumericTextBox ntxtSteelYieldStrength;
        private System.Windows.Forms.ComboBox cmbxGrade;
        private System.Windows.Forms.CheckBox chbxUsePredefinedGrades;
        private System.Windows.Forms.GroupBox grbxGeneralPhysicalProperties;
    }
}