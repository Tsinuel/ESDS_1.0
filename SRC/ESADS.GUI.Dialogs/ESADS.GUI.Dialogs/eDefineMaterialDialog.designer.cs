namespace ESADS.GUI
{
    partial class eDefineMaterialDialog
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.grbxConcrete = new System.Windows.Forms.GroupBox();
            this.btnNewConcrete = new System.Windows.Forms.Button();
            this.btnResetConcrete = new System.Windows.Forms.Button();
            this.btnRemoveConcrete = new System.Windows.Forms.Button();
            this.btnModifyConcrete = new System.Windows.Forms.Button();
            this.lstbxConcrete = new System.Windows.Forms.ListBox();
            this.grbxSteel = new System.Windows.Forms.GroupBox();
            this.btnResetSteel = new System.Windows.Forms.Button();
            this.btnRemoveSteel = new System.Windows.Forms.Button();
            this.btnModifySteel = new System.Windows.Forms.Button();
            this.btnNewSteel = new System.Windows.Forms.Button();
            this.lstbxSteel = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbxMaterialType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grbxConcrete.SuspendLayout();
            this.grbxSteel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(217, 345);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(136, 345);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(55, 345);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // grbxConcrete
            // 
            this.grbxConcrete.Controls.Add(this.btnNewConcrete);
            this.grbxConcrete.Controls.Add(this.btnResetConcrete);
            this.grbxConcrete.Controls.Add(this.btnRemoveConcrete);
            this.grbxConcrete.Controls.Add(this.btnModifyConcrete);
            this.grbxConcrete.Controls.Add(this.lstbxConcrete);
            this.grbxConcrete.Location = new System.Drawing.Point(12, 67);
            this.grbxConcrete.Name = "grbxConcrete";
            this.grbxConcrete.Size = new System.Drawing.Size(270, 258);
            this.grbxConcrete.TabIndex = 0;
            this.grbxConcrete.TabStop = false;
            this.grbxConcrete.Text = "Concrete";
            // 
            // btnNewConcrete
            // 
            this.btnNewConcrete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewConcrete.Location = new System.Drawing.Point(192, 19);
            this.btnNewConcrete.Name = "btnNewConcrete";
            this.btnNewConcrete.Size = new System.Drawing.Size(72, 23);
            this.btnNewConcrete.TabIndex = 1;
            this.btnNewConcrete.Text = "New...";
            this.btnNewConcrete.UseVisualStyleBackColor = true;
            this.btnNewConcrete.Click += new System.EventHandler(this.btnNewConcrete_Click);
            // 
            // btnResetConcrete
            // 
            this.btnResetConcrete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetConcrete.Location = new System.Drawing.Point(192, 222);
            this.btnResetConcrete.Name = "btnResetConcrete";
            this.btnResetConcrete.Size = new System.Drawing.Size(72, 23);
            this.btnResetConcrete.TabIndex = 2;
            this.btnResetConcrete.Text = "Reset...";
            this.btnResetConcrete.UseVisualStyleBackColor = true;
            this.btnResetConcrete.Click += new System.EventHandler(this.btnResetConcrete_Click);
            // 
            // btnRemoveConcrete
            // 
            this.btnRemoveConcrete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveConcrete.Enabled = false;
            this.btnRemoveConcrete.Location = new System.Drawing.Point(192, 77);
            this.btnRemoveConcrete.Name = "btnRemoveConcrete";
            this.btnRemoveConcrete.Size = new System.Drawing.Size(72, 23);
            this.btnRemoveConcrete.TabIndex = 2;
            this.btnRemoveConcrete.Text = "Remove";
            this.btnRemoveConcrete.UseVisualStyleBackColor = true;
            this.btnRemoveConcrete.Click += new System.EventHandler(this.btnRemoveConcrete_Click);
            // 
            // btnModifyConcrete
            // 
            this.btnModifyConcrete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModifyConcrete.Enabled = false;
            this.btnModifyConcrete.Location = new System.Drawing.Point(192, 48);
            this.btnModifyConcrete.Name = "btnModifyConcrete";
            this.btnModifyConcrete.Size = new System.Drawing.Size(72, 23);
            this.btnModifyConcrete.TabIndex = 2;
            this.btnModifyConcrete.Text = "Modify...";
            this.btnModifyConcrete.UseVisualStyleBackColor = true;
            this.btnModifyConcrete.Click += new System.EventHandler(this.btnModifyConcrete_Click);
            // 
            // lstbxConcrete
            // 
            this.lstbxConcrete.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstbxConcrete.FormattingEnabled = true;
            this.lstbxConcrete.Location = new System.Drawing.Point(6, 20);
            this.lstbxConcrete.Name = "lstbxConcrete";
            this.lstbxConcrete.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstbxConcrete.Size = new System.Drawing.Size(180, 225);
            this.lstbxConcrete.TabIndex = 0;
            this.lstbxConcrete.SelectedValueChanged += new System.EventHandler(this.lstbxConcrete_SelectedValueChanged);
            // 
            // grbxSteel
            // 
            this.grbxSteel.Controls.Add(this.btnResetSteel);
            this.grbxSteel.Controls.Add(this.btnRemoveSteel);
            this.grbxSteel.Controls.Add(this.btnModifySteel);
            this.grbxSteel.Controls.Add(this.btnNewSteel);
            this.grbxSteel.Controls.Add(this.lstbxSteel);
            this.grbxSteel.Location = new System.Drawing.Point(12, 67);
            this.grbxSteel.Name = "grbxSteel";
            this.grbxSteel.Size = new System.Drawing.Size(270, 258);
            this.grbxSteel.TabIndex = 1;
            this.grbxSteel.TabStop = false;
            this.grbxSteel.Text = "Steel";
            // 
            // btnResetSteel
            // 
            this.btnResetSteel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetSteel.Location = new System.Drawing.Point(192, 222);
            this.btnResetSteel.Name = "btnResetSteel";
            this.btnResetSteel.Size = new System.Drawing.Size(72, 23);
            this.btnResetSteel.TabIndex = 2;
            this.btnResetSteel.Text = "Reset...";
            this.btnResetSteel.UseVisualStyleBackColor = true;
            this.btnResetSteel.Click += new System.EventHandler(this.btnResetSteel_Click);
            // 
            // btnRemoveSteel
            // 
            this.btnRemoveSteel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveSteel.Enabled = false;
            this.btnRemoveSteel.Location = new System.Drawing.Point(192, 77);
            this.btnRemoveSteel.Name = "btnRemoveSteel";
            this.btnRemoveSteel.Size = new System.Drawing.Size(72, 23);
            this.btnRemoveSteel.TabIndex = 2;
            this.btnRemoveSteel.Text = "Remove";
            this.btnRemoveSteel.UseVisualStyleBackColor = true;
            this.btnRemoveSteel.Click += new System.EventHandler(this.btnRemoveSteel_Click);
            // 
            // btnModifySteel
            // 
            this.btnModifySteel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModifySteel.Enabled = false;
            this.btnModifySteel.Location = new System.Drawing.Point(192, 48);
            this.btnModifySteel.Name = "btnModifySteel";
            this.btnModifySteel.Size = new System.Drawing.Size(72, 23);
            this.btnModifySteel.TabIndex = 2;
            this.btnModifySteel.Text = "Modify...";
            this.btnModifySteel.UseVisualStyleBackColor = true;
            this.btnModifySteel.Click += new System.EventHandler(this.btnModifySteel_Click);
            // 
            // btnNewSteel
            // 
            this.btnNewSteel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewSteel.Location = new System.Drawing.Point(192, 19);
            this.btnNewSteel.Name = "btnNewSteel";
            this.btnNewSteel.Size = new System.Drawing.Size(72, 23);
            this.btnNewSteel.TabIndex = 1;
            this.btnNewSteel.Text = "New...";
            this.btnNewSteel.UseVisualStyleBackColor = true;
            this.btnNewSteel.Click += new System.EventHandler(this.btnNewSteel_Click);
            // 
            // lstbxSteel
            // 
            this.lstbxSteel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstbxSteel.FormattingEnabled = true;
            this.lstbxSteel.Location = new System.Drawing.Point(6, 20);
            this.lstbxSteel.Name = "lstbxSteel";
            this.lstbxSteel.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstbxSteel.Size = new System.Drawing.Size(180, 225);
            this.lstbxSteel.TabIndex = 0;
            this.lstbxSteel.SelectedValueChanged += new System.EventHandler(this.lstbxSteel_SelectedValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbxMaterialType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 49);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose the material type to define or modify";
            // 
            // cmbxMaterialType
            // 
            this.cmbxMaterialType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxMaterialType.FormattingEnabled = true;
            this.cmbxMaterialType.Location = new System.Drawing.Point(143, 19);
            this.cmbxMaterialType.Name = "cmbxMaterialType";
            this.cmbxMaterialType.Size = new System.Drawing.Size(121, 21);
            this.cmbxMaterialType.TabIndex = 1;
            this.cmbxMaterialType.SelectedIndexChanged += new System.EventHandler(this.cmbxMaterialType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type of Material to define:";
            // 
            // eDefineMaterialDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(304, 382);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grbxSteel);
            this.Controls.Add(this.grbxConcrete);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(310, 408);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(310, 408);
            this.Name = "eDefineMaterialDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Define Material";
            this.grbxConcrete.ResumeLayout(false);
            this.grbxSteel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.GroupBox grbxConcrete;
        private System.Windows.Forms.Button btnModifyConcrete;
        private System.Windows.Forms.Button btnNewConcrete;
        private System.Windows.Forms.ListBox lstbxConcrete;
        private System.Windows.Forms.GroupBox grbxSteel;
        private System.Windows.Forms.Button btnModifySteel;
        private System.Windows.Forms.Button btnNewSteel;
        private System.Windows.Forms.ListBox lstbxSteel;
        private System.Windows.Forms.Button btnRemoveConcrete;
        private System.Windows.Forms.Button btnRemoveSteel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbxMaterialType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnResetConcrete;
        private System.Windows.Forms.Button btnResetSteel;
    }
}