namespace ESADS.GUI
{
    partial class eNewBeamDialog
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
            this.ntxtNumbOfMembers = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtLength = new ESADS.GUI.Controls.eNumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbxDefaultSupport = new System.Windows.Forms.ComboBox();
            this.ntxtColumnWidth = new ESADS.GUI.Controls.eNumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnDefineDefaultSecn = new System.Windows.Forms.Button();
            this.chkbxConsiderSelfWeight = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDefineSteel = new System.Windows.Forms.Button();
            this.btnDefineConcrete = new System.Windows.Forms.Button();
            this.cmbxSteel = new System.Windows.Forms.ComboBox();
            this.cmbxConcrete = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ntxtNumbOfMembers
            // 
            this.ntxtNumbOfMembers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ntxtNumbOfMembers.AutomaticResize = false;
            this.ntxtNumbOfMembers.DataType = ESADS.GUI.Controls.eDataType.Integer;
            this.ntxtNumbOfMembers.DoubleValue = 0D;
            this.ntxtNumbOfMembers.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtNumbOfMembers.IntValue = 0;
            this.ntxtNumbOfMembers.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtNumbOfMembers.Location = new System.Drawing.Point(118, 19);
            this.ntxtNumbOfMembers.Measurment = ESADS.GUI.Controls.eMeasurment.None;
            this.ntxtNumbOfMembers.Name = "ntxtNumbOfMembers";
            this.ntxtNumbOfMembers.Size = new System.Drawing.Size(100, 20);
            this.ntxtNumbOfMembers.SU = 0D;
            this.ntxtNumbOfMembers.TabIndex = 0;
            this.ntxtNumbOfMembers.Text = "0";
            // 
            // ntxtLength
            // 
            this.ntxtLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ntxtLength.AutomaticResize = false;
            this.ntxtLength.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtLength.DoubleValue = 0D;
            this.ntxtLength.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtLength.IntValue = 0;
            this.ntxtLength.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtLength.Location = new System.Drawing.Point(118, 45);
            this.ntxtLength.Measurment = ESADS.GUI.Controls.eMeasurment.None;
            this.ntxtLength.Name = "ntxtLength";
            this.ntxtLength.Size = new System.Drawing.Size(100, 20);
            this.ntxtLength.SU = 0D;
            this.ntxtLength.TabIndex = 0;
            this.ntxtLength.Text = "0";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number of Members";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmbxDefaultSupport);
            this.groupBox1.Controls.Add(this.ntxtNumbOfMembers);
            this.groupBox1.Controls.Add(this.ntxtColumnWidth);
            this.groupBox1.Controls.Add(this.ntxtLength);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 127);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data for Continuous Beam";
            // 
            // cmbxDefaultSupport
            // 
            this.cmbxDefaultSupport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbxDefaultSupport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxDefaultSupport.FormattingEnabled = true;
            this.cmbxDefaultSupport.Location = new System.Drawing.Point(118, 71);
            this.cmbxDefaultSupport.Name = "cmbxDefaultSupport";
            this.cmbxDefaultSupport.Size = new System.Drawing.Size(100, 21);
            this.cmbxDefaultSupport.TabIndex = 2;
            // 
            // ntxtColumnWidth
            // 
            this.ntxtColumnWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ntxtColumnWidth.AutomaticResize = false;
            this.ntxtColumnWidth.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtColumnWidth.DoubleValue = 0D;
            this.ntxtColumnWidth.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtColumnWidth.IntValue = 0;
            this.ntxtColumnWidth.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtColumnWidth.Location = new System.Drawing.Point(118, 98);
            this.ntxtColumnWidth.Measurment = ESADS.GUI.Controls.eMeasurment.None;
            this.ntxtColumnWidth.Name = "ntxtColumnWidth";
            this.ntxtColumnWidth.Size = new System.Drawing.Size(100, 20);
            this.ntxtColumnWidth.SU = 0D;
            this.ntxtColumnWidth.TabIndex = 0;
            this.ntxtColumnWidth.Text = "0";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Support";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Column Width";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Length";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(200, 276);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(119, 276);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnDefineDefaultSecn
            // 
            this.btnDefineDefaultSecn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefineDefaultSecn.Location = new System.Drawing.Point(0, 95);
            this.btnDefineDefaultSecn.Name = "btnDefineDefaultSecn";
            this.btnDefineDefaultSecn.Size = new System.Drawing.Size(254, 23);
            this.btnDefineDefaultSecn.TabIndex = 0;
            this.btnDefineDefaultSecn.Text = "Define Default Section...";
            this.btnDefineDefaultSecn.UseVisualStyleBackColor = true;
            this.btnDefineDefaultSecn.Click += new System.EventHandler(this.btnDefineDefaultSecn_Click);
            // 
            // chkbxConsiderSelfWeight
            // 
            this.chkbxConsiderSelfWeight.AutoSize = true;
            this.chkbxConsiderSelfWeight.Location = new System.Drawing.Point(6, 73);
            this.chkbxConsiderSelfWeight.Name = "chkbxConsiderSelfWeight";
            this.chkbxConsiderSelfWeight.Size = new System.Drawing.Size(125, 17);
            this.chkbxConsiderSelfWeight.TabIndex = 3;
            this.chkbxConsiderSelfWeight.Text = "Consider Self Weight";
            this.chkbxConsiderSelfWeight.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDefineSteel);
            this.groupBox3.Controls.Add(this.btnDefineConcrete);
            this.groupBox3.Controls.Add(this.btnDefineDefaultSecn);
            this.groupBox3.Controls.Add(this.cmbxSteel);
            this.groupBox3.Controls.Add(this.cmbxConcrete);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.chkbxConsiderSelfWeight);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(12, 145);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(266, 124);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Design Preferences";
            // 
            // btnDefineSteel
            // 
            this.btnDefineSteel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefineSteel.Location = new System.Drawing.Point(224, 46);
            this.btnDefineSteel.Name = "btnDefineSteel";
            this.btnDefineSteel.Size = new System.Drawing.Size(30, 21);
            this.btnDefineSteel.TabIndex = 10;
            this.btnDefineSteel.Text = "...";
            this.btnDefineSteel.UseVisualStyleBackColor = true;
            this.btnDefineSteel.Click += new System.EventHandler(this.btnDefineSteel_Click);
            // 
            // btnDefineConcrete
            // 
            this.btnDefineConcrete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefineConcrete.Location = new System.Drawing.Point(224, 19);
            this.btnDefineConcrete.Name = "btnDefineConcrete";
            this.btnDefineConcrete.Size = new System.Drawing.Size(30, 21);
            this.btnDefineConcrete.TabIndex = 10;
            this.btnDefineConcrete.Text = "...";
            this.btnDefineConcrete.UseVisualStyleBackColor = true;
            this.btnDefineConcrete.Click += new System.EventHandler(this.btnDefineConcrete_Click);
            // 
            // cmbxSteel
            // 
            this.cmbxSteel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbxSteel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxSteel.FormattingEnabled = true;
            this.cmbxSteel.Location = new System.Drawing.Point(118, 46);
            this.cmbxSteel.Name = "cmbxSteel";
            this.cmbxSteel.Size = new System.Drawing.Size(100, 21);
            this.cmbxSteel.TabIndex = 2;
            // 
            // cmbxConcrete
            // 
            this.cmbxConcrete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbxConcrete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxConcrete.FormattingEnabled = true;
            this.cmbxConcrete.Location = new System.Drawing.Point(118, 19);
            this.cmbxConcrete.Name = "cmbxConcrete";
            this.cmbxConcrete.Size = new System.Drawing.Size(100, 21);
            this.cmbxConcrete.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(81, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Steel";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(68, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Concrete";
            // 
            // eNewBeamDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(287, 311);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eNewBeamDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Beam Definition";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GUI.Controls.eNumericTextBox ntxtNumbOfMembers;
        private GUI.Controls.eNumericTextBox ntxtLength;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbxDefaultSupport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnDefineDefaultSecn;
        private System.Windows.Forms.CheckBox chkbxConsiderSelfWeight;
        private System.Windows.Forms.GroupBox groupBox3;
        private GUI.Controls.eNumericTextBox ntxtColumnWidth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDefineSteel;
        private System.Windows.Forms.Button btnDefineConcrete;
        private System.Windows.Forms.ComboBox cmbxSteel;
        private System.Windows.Forms.ComboBox cmbxConcrete;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}