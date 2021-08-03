namespace ESADS.GUI
{
    partial class eColumnCustomReinforcementDistributionDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(eColumnCustomReinforcementDistributionDialog));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ntxtHorizontal = new ESADS.GUI.Controls.eNumericTextBox();
            this.ntxtVertical = new ESADS.GUI.Controls.eNumericTextBox();
            this.chFiniteElements = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ntxtNumberOfSeg = new ESADS.GUI.Controls.eNumericTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(245, 285);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(166, 385);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cacel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(85, 385);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ntxtHorizontal);
            this.groupBox1.Controls.Add(this.ntxtVertical);
            this.groupBox1.Controls.Add(this.chFiniteElements);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ntxtNumberOfSeg);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(6, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 375);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // ntxtHorizontal
            // 
            this.ntxtHorizontal.AutomaticResize = false;
            this.ntxtHorizontal.BackColor = System.Drawing.SystemColors.MenuText;
            this.ntxtHorizontal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ntxtHorizontal.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtHorizontal.DoubleValue = 0.5D;
            this.ntxtHorizontal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ntxtHorizontal.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtHorizontal.ForeColor = System.Drawing.SystemColors.Info;
            this.ntxtHorizontal.IntValue = 0;
            this.ntxtHorizontal.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtHorizontal.Location = new System.Drawing.Point(107, 62);
            this.ntxtHorizontal.Measurment = ESADS.GUI.Controls.eMeasurment.None;
            this.ntxtHorizontal.Name = "ntxtHorizontal";
            this.ntxtHorizontal.Size = new System.Drawing.Size(58, 29);
            this.ntxtHorizontal.SU = 0.5D;
            this.ntxtHorizontal.TabIndex = 12;
            this.ntxtHorizontal.Text = "0.5";
            this.ntxtHorizontal.TextChanged += new System.EventHandler(this.ntxtHorizontal_TextChanged);
            // 
            // ntxtVertical
            // 
            this.ntxtVertical.AutomaticResize = false;
            this.ntxtVertical.BackColor = System.Drawing.SystemColors.MenuText;
            this.ntxtVertical.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ntxtVertical.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtVertical.DoubleValue = 0.5D;
            this.ntxtVertical.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ntxtVertical.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtVertical.ForeColor = System.Drawing.SystemColors.Info;
            this.ntxtVertical.IntValue = 0;
            this.ntxtVertical.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtVertical.Location = new System.Drawing.Point(58, 150);
            this.ntxtVertical.Measurment = ESADS.GUI.Controls.eMeasurment.None;
            this.ntxtVertical.Name = "ntxtVertical";
            this.ntxtVertical.ReadOnly = true;
            this.ntxtVertical.Size = new System.Drawing.Size(55, 29);
            this.ntxtVertical.SU = 0.5D;
            this.ntxtVertical.TabIndex = 11;
            this.ntxtVertical.Text = "0.5";
            // 
            // chFiniteElements
            // 
            this.chFiniteElements.AutoSize = true;
            this.chFiniteElements.Location = new System.Drawing.Point(20, 314);
            this.chFiniteElements.Name = "chFiniteElements";
            this.chFiniteElements.Size = new System.Drawing.Size(215, 17);
            this.chFiniteElements.TabIndex = 10;
            this.chFiniteElements.Text = "Chenge Finite Elemets for Apporximation";
            this.chFiniteElements.UseVisualStyleBackColor = true;
            this.chFiniteElements.CheckedChanged += new System.EventHandler(this.chFiniteElements_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(41, 339);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Descet elemets per segment";
            // 
            // ntxtNumberOfSeg
            // 
            this.ntxtNumberOfSeg.AutomaticResize = false;
            this.ntxtNumberOfSeg.DataType = ESADS.GUI.Controls.eDataType.Integer;
            this.ntxtNumberOfSeg.DoubleValue = 20D;
            this.ntxtNumberOfSeg.Enabled = false;
            this.ntxtNumberOfSeg.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtNumberOfSeg.IntValue = 20;
            this.ntxtNumberOfSeg.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtNumberOfSeg.Location = new System.Drawing.Point(188, 336);
            this.ntxtNumberOfSeg.Measurment = ESADS.GUI.Controls.eMeasurment.None;
            this.ntxtNumberOfSeg.Name = "ntxtNumberOfSeg";
            this.ntxtNumberOfSeg.Size = new System.Drawing.Size(47, 20);
            this.ntxtNumberOfSeg.SU = 20D;
            this.ntxtNumberOfSeg.TabIndex = 8;
            this.ntxtNumberOfSeg.Text = "20";
            // 
            // eColumnCustomReinforcementDistributionDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 418);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eColumnCustomReinforcementDistributionDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Custom Reifnforcement Distribution";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chFiniteElements;
        private System.Windows.Forms.Label label1;
        private Controls.eNumericTextBox ntxtNumberOfSeg;
        private Controls.eNumericTextBox ntxtHorizontal;
        private Controls.eNumericTextBox ntxtVertical;
    }
}