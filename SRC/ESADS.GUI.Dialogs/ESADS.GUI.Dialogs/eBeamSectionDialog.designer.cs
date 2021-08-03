namespace ESADS.GUI
{
    partial class eBeamSectionDialog
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
            this.label8 = new System.Windows.Forms.Label();
            this.cbxLength = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ntxtWidth = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtDepth = new ESADS.GUI.Controls.eNumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnShowProperties = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ntxtNominal_EI = new ESADS.GUI.Controls.eNumericTextBox();
            this.chkUseNominal_EI = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.cbxLength);
            this.groupBox5.Location = new System.Drawing.Point(233, 235);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(232, 51);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Units";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(66, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Length";
            // 
            // cbxLength
            // 
            this.cbxLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLength.FormattingEnabled = true;
            this.cbxLength.Location = new System.Drawing.Point(112, 19);
            this.cbxLength.Name = "cbxLength";
            this.cbxLength.Size = new System.Drawing.Size(104, 21);
            this.cbxLength.TabIndex = 6;
            this.cbxLength.SelectedIndexChanged += new System.EventHandler(this.cbxLength_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pnlPreview);
            this.groupBox4.Location = new System.Drawing.Point(233, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(232, 217);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Preview";
            // 
            // pnlPreview
            // 
            this.pnlPreview.BackColor = System.Drawing.Color.Black;
            this.pnlPreview.Location = new System.Drawing.Point(14, 22);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(200, 180);
            this.pnlPreview.TabIndex = 0;
            this.pnlPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPreview_Paint);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtName);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(215, 48);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(109, 17);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(98, 20);
            this.txtName.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ntxtWidth);
            this.groupBox2.Controls.Add(this.ntxtDepth);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(12, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(215, 77);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dimensions";
            // 
            // ntxtWidth
            // 
            this.ntxtWidth.AutomaticResize = false;
            this.ntxtWidth.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtWidth.DoubleValue = 0D;
            this.ntxtWidth.IntValue = 0;
            this.ntxtWidth.Location = new System.Drawing.Point(109, 45);
            this.ntxtWidth.Name = "ntxtWidth";
            this.ntxtWidth.Size = new System.Drawing.Size(100, 20);
            this.ntxtWidth.TabIndex = 9;
            this.ntxtWidth.Text = "0";
            this.ntxtWidth.TextChanged += new System.EventHandler(this.ntxtWidth_TextChanged);
            // 
            // ntxtDepth
            // 
            this.ntxtDepth.AutomaticResize = false;
            this.ntxtDepth.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtDepth.DoubleValue = 0D;
            this.ntxtDepth.IntValue = 0;
            this.ntxtDepth.Location = new System.Drawing.Point(109, 19);
            this.ntxtDepth.Name = "ntxtDepth";
            this.ntxtDepth.Size = new System.Drawing.Size(100, 20);
            this.ntxtDepth.TabIndex = 8;
            this.ntxtDepth.Text = "0";
            this.ntxtDepth.TextChanged += new System.EventHandler(this.ntxtDepth_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(67, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Depth";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(67, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Width";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(392, 299);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(311, 299);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnShowProperties
            // 
            this.btnShowProperties.Location = new System.Drawing.Point(6, 97);
            this.btnShowProperties.Name = "btnShowProperties";
            this.btnShowProperties.Size = new System.Drawing.Size(201, 23);
            this.btnShowProperties.TabIndex = 8;
            this.btnShowProperties.Text = "Show Section Properties...";
            this.btnShowProperties.UseVisualStyleBackColor = true;
            this.btnShowProperties.Click += new System.EventHandler(this.btnShowProperties_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnShowProperties);
            this.groupBox1.Controls.Add(this.ntxtNominal_EI);
            this.groupBox1.Controls.Add(this.chkUseNominal_EI);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 150);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 136);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Section Properties";
            // 
            // ntxtNominal_EI
            // 
            this.ntxtNominal_EI.AutomaticResize = false;
            this.ntxtNominal_EI.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtNominal_EI.DoubleValue = 0D;
            this.ntxtNominal_EI.Enabled = false;
            this.ntxtNominal_EI.IntValue = 0;
            this.ntxtNominal_EI.Location = new System.Drawing.Point(109, 45);
            this.ntxtNominal_EI.Name = "ntxtNominal_EI";
            this.ntxtNominal_EI.Size = new System.Drawing.Size(100, 20);
            this.ntxtNominal_EI.TabIndex = 9;
            this.ntxtNominal_EI.Text = "0";
            // 
            // chkUseNominal_EI
            // 
            this.chkUseNominal_EI.AutoSize = true;
            this.chkUseNominal_EI.Location = new System.Drawing.Point(6, 19);
            this.chkUseNominal_EI.Name = "chkUseNominal_EI";
            this.chkUseNominal_EI.Size = new System.Drawing.Size(99, 17);
            this.chkUseNominal_EI.TabIndex = 0;
            this.chkUseNominal_EI.Text = "Use Nominal EI";
            this.chkUseNominal_EI.UseVisualStyleBackColor = true;
            this.chkUseNominal_EI.CheckedChanged += new System.EventHandler(this.chkUseNominal_EI_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Nominal EI Value";
            // 
            // eBeamSectionDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(479, 334);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eBeamSectionDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Beam Section";
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbxLength;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private GUI.Controls.eNumericTextBox ntxtWidth;
        private GUI.Controls.eNumericTextBox ntxtDepth;
        private System.Windows.Forms.Button btnShowProperties;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.eNumericTextBox ntxtNominal_EI;
        private System.Windows.Forms.CheckBox chkUseNominal_EI;
        private System.Windows.Forms.Label label2;
    }
}