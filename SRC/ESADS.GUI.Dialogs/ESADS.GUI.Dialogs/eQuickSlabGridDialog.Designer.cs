namespace ESADS.GUI
{
    partial class eQuickSlabGridDialog
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxLength = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ntxtNumOfHor = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtYGridSpacing = new ESADS.GUI.Controls.eNumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblYGridSpacing = new System.Windows.Forms.Label();
            this.ntxtNumOfVer = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtXGridSpacing = new ESADS.GUI.Controls.eNumericTextBox();
            this.lblXGridSpacing = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.cbxLength);
            this.groupBox5.Location = new System.Drawing.Point(12, 184);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(262, 49);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Units";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(103, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Length";
            // 
            // cbxLength
            // 
            this.cbxLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLength.FormattingEnabled = true;
            this.cbxLength.Location = new System.Drawing.Point(149, 19);
            this.cbxLength.Name = "cbxLength";
            this.cbxLength.Size = new System.Drawing.Size(43, 21);
            this.cbxLength.TabIndex = 6;
            this.cbxLength.SelectedIndexChanged += new System.EventHandler(this.cbxLength_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ntxtNumOfHor);
            this.groupBox1.Controls.Add(this.ntxtYGridSpacing);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblYGridSpacing);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 81);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Horizontal (Y) Grids";
            // 
            // ntxtNumOfHor
            // 
            this.ntxtNumOfHor.AutomaticResize = false;
            this.ntxtNumOfHor.DataType = ESADS.GUI.Controls.eDataType.Integer;
            this.ntxtNumOfHor.DoubleValue = 0D;
            this.ntxtNumOfHor.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtNumOfHor.IntValue = 0;
            this.ntxtNumOfHor.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtNumOfHor.Location = new System.Drawing.Point(149, 19);
            this.ntxtNumOfHor.Measurment = ESADS.GUI.Controls.eMeasurment.None;
            this.ntxtNumOfHor.Name = "ntxtNumOfHor";
            this.ntxtNumOfHor.Size = new System.Drawing.Size(100, 20);
            this.ntxtNumOfHor.SU = 0D;
            this.ntxtNumOfHor.TabIndex = 6;
            this.ntxtNumOfHor.Text = "0";
            // 
            // ntxtYGridSpacing
            // 
            this.ntxtYGridSpacing.AutomaticResize = false;
            this.ntxtYGridSpacing.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtYGridSpacing.DoubleValue = 0D;
            this.ntxtYGridSpacing.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtYGridSpacing.IntValue = 0;
            this.ntxtYGridSpacing.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtYGridSpacing.Location = new System.Drawing.Point(149, 45);
            this.ntxtYGridSpacing.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtYGridSpacing.Name = "ntxtYGridSpacing";
            this.ntxtYGridSpacing.Size = new System.Drawing.Size(100, 20);
            this.ntxtYGridSpacing.SU = 0D;
            this.ntxtYGridSpacing.TabIndex = 4;
            this.ntxtYGridSpacing.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Number of Horizontal Grids";
            // 
            // lblYGridSpacing
            // 
            this.lblYGridSpacing.AutoSize = true;
            this.lblYGridSpacing.Location = new System.Drawing.Point(9, 48);
            this.lblYGridSpacing.Name = "lblYGridSpacing";
            this.lblYGridSpacing.Size = new System.Drawing.Size(134, 13);
            this.lblYGridSpacing.TabIndex = 1;
            this.lblYGridSpacing.Text = "Grid Spacing in Y-Direction";
            // 
            // ntxtNumOfVer
            // 
            this.ntxtNumOfVer.AutomaticResize = false;
            this.ntxtNumOfVer.DataType = ESADS.GUI.Controls.eDataType.Integer;
            this.ntxtNumOfVer.DoubleValue = 0D;
            this.ntxtNumOfVer.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtNumOfVer.IntValue = 0;
            this.ntxtNumOfVer.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtNumOfVer.Location = new System.Drawing.Point(149, 19);
            this.ntxtNumOfVer.Measurment = ESADS.GUI.Controls.eMeasurment.None;
            this.ntxtNumOfVer.Name = "ntxtNumOfVer";
            this.ntxtNumOfVer.Size = new System.Drawing.Size(100, 20);
            this.ntxtNumOfVer.SU = 0D;
            this.ntxtNumOfVer.TabIndex = 7;
            this.ntxtNumOfVer.Text = "0";
            // 
            // ntxtXGridSpacing
            // 
            this.ntxtXGridSpacing.AutomaticResize = false;
            this.ntxtXGridSpacing.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtXGridSpacing.DoubleValue = 0D;
            this.ntxtXGridSpacing.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtXGridSpacing.IntValue = 0;
            this.ntxtXGridSpacing.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtXGridSpacing.Location = new System.Drawing.Point(149, 45);
            this.ntxtXGridSpacing.Measurment = ESADS.GUI.Controls.eMeasurment.Length;
            this.ntxtXGridSpacing.Name = "ntxtXGridSpacing";
            this.ntxtXGridSpacing.Size = new System.Drawing.Size(100, 20);
            this.ntxtXGridSpacing.SU = 0D;
            this.ntxtXGridSpacing.TabIndex = 5;
            this.ntxtXGridSpacing.Text = "0";
            // 
            // lblXGridSpacing
            // 
            this.lblXGridSpacing.AutoSize = true;
            this.lblXGridSpacing.Location = new System.Drawing.Point(9, 48);
            this.lblXGridSpacing.Name = "lblXGridSpacing";
            this.lblXGridSpacing.Size = new System.Drawing.Size(134, 13);
            this.lblXGridSpacing.TabIndex = 3;
            this.lblXGridSpacing.Text = "Grid Spacing in X-Direction";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of Vertical Grids";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(200, 245);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(119, 245);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ntxtNumOfVer);
            this.groupBox2.Controls.Add(this.ntxtXGridSpacing);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblXGridSpacing);
            this.groupBox2.Location = new System.Drawing.Point(12, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(262, 79);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Vertical (X) Grids";
            // 
            // eQuickSlabGridDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(287, 280);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eQuickSlabGridDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick Slab Grid";
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxLength;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.eNumericTextBox ntxtNumOfVer;
        private Controls.eNumericTextBox ntxtNumOfHor;
        private Controls.eNumericTextBox ntxtXGridSpacing;
        private Controls.eNumericTextBox ntxtYGridSpacing;
        private System.Windows.Forms.Label lblXGridSpacing;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblYGridSpacing;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}