namespace ESADS.GUI
{
    partial class eAdvancedSelectionDialog
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
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Member");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Grid");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Joint Loads");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Member Loads");
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Concentrated Force");
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Concentrated Moment");
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("Uniformaly Distributed");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Triangular");
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("Trapizoidal");
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("Load", new System.Windows.Forms.TreeNode[] {
            treeNode25,
            treeNode26,
            treeNode27,
            treeNode28,
            treeNode29,
            treeNode30,
            treeNode31});
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("Pin Supported");
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("Fixed Supported");
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("Continuous");
            System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("Vertical Roller");
            System.Windows.Forms.TreeNode treeNode37 = new System.Windows.Forms.TreeNode("Hindge");
            System.Windows.Forms.TreeNode treeNode38 = new System.Windows.Forms.TreeNode("Joint", new System.Windows.Forms.TreeNode[] {
            treeNode33,
            treeNode34,
            treeNode35,
            treeNode36,
            treeNode37});
            System.Windows.Forms.TreeNode treeNode39 = new System.Windows.Forms.TreeNode("ndGrid");
            System.Windows.Forms.TreeNode treeNode40 = new System.Windows.Forms.TreeNode("Beam", new System.Windows.Forms.TreeNode[] {
            treeNode23,
            treeNode24,
            treeNode32,
            treeNode38,
            treeNode39});
            System.Windows.Forms.TreeNode treeNode41 = new System.Windows.Forms.TreeNode("Beam");
            System.Windows.Forms.TreeNode treeNode42 = new System.Windows.Forms.TreeNode("Panel");
            System.Windows.Forms.TreeNode treeNode43 = new System.Windows.Forms.TreeNode("Section");
            System.Windows.Forms.TreeNode treeNode44 = new System.Windows.Forms.TreeNode("Slab", new System.Windows.Forms.TreeNode[] {
            treeNode41,
            treeNode42,
            treeNode43});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(13, 12);
            this.treeView1.Name = "treeView1";
            treeNode23.Name = "ndMember";
            treeNode23.Text = "Member";
            treeNode24.Name = "ndGrid";
            treeNode24.Text = "Grid";
            treeNode25.Name = "ndJointLoads";
            treeNode25.Text = "Joint Loads";
            treeNode26.Name = "ndMemberLoads";
            treeNode26.Text = "Member Loads";
            treeNode27.Name = "ndConcentratedForce";
            treeNode27.Text = "Concentrated Force";
            treeNode28.Name = "ndConcentratedMoment";
            treeNode28.Text = "Concentrated Moment";
            treeNode29.Name = "ndUniformlyDistributed";
            treeNode29.Text = "Uniformaly Distributed";
            treeNode30.Name = "ndTriangular";
            treeNode30.Text = "Triangular";
            treeNode31.Name = "ndTrapizoidal";
            treeNode31.Text = "Trapizoidal";
            treeNode32.Name = "mdLoad";
            treeNode32.Text = "Load";
            treeNode33.Name = "ndPin";
            treeNode33.Text = "Pin Supported";
            treeNode34.Name = "ndFixed";
            treeNode34.Text = "Fixed Supported";
            treeNode35.Name = "ndContinuous";
            treeNode35.Text = "Continuous";
            treeNode36.Name = "ndRoller";
            treeNode36.Text = "Vertical Roller";
            treeNode37.Name = "ndHindge";
            treeNode37.Text = "Hindge";
            treeNode38.Name = "ndJoint";
            treeNode38.Text = "Joint";
            treeNode39.Name = "ndGrid";
            treeNode39.Text = "ndGrid";
            treeNode40.Name = "ndBeam";
            treeNode40.Text = "Beam";
            treeNode41.Name = "ndBeam";
            treeNode41.Text = "Beam";
            treeNode42.Name = "ndPanel";
            treeNode42.Text = "Panel";
            treeNode43.Name = "ndSection";
            treeNode43.Text = "Section";
            treeNode44.Name = "Node11";
            treeNode44.Text = "Slab";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode40,
            treeNode44});
            this.treeView1.Size = new System.Drawing.Size(227, 246);
            this.treeView1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(12, 264);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 92);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selection Options";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(29, 65);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(151, 17);
            this.radioButton3.TabIndex = 4;
            this.radioButton3.Text = "Exclude previous selection";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(29, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(174, 17);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Text = "Exclude from previous selection";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(29, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(144, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Add to previous selection";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(165, 362);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(84, 362);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 14;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            // 
            // eAdvancedSelectionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 392);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.treeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eAdvancedSelectionDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advanced Selection";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSelect;
    }
}