namespace ESADS.GUI
{
    partial class eAssignSlabLoad
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("General");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Stair");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Balconies");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Category A", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Category B");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("C1");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("C2");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("C3");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("C4");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("C5");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Category C", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("D1");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("D2");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Category D", new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Category E");
            this.gbxCategory = new System.Windows.Forms.GroupBox();
            this.trvCategory = new System.Windows.Forms.TreeView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ntxtMagnitude = new ESADS.GUI.Controls.eNumericTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxForceUnit = new System.Windows.Forms.ComboBox();
            this.cbxLengthUnit = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAssign = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cbxActionType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbxCategory.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxCategory
            // 
            this.gbxCategory.Controls.Add(this.trvCategory);
            this.gbxCategory.Location = new System.Drawing.Point(9, 79);
            this.gbxCategory.Name = "gbxCategory";
            this.gbxCategory.Size = new System.Drawing.Size(184, 171);
            this.gbxCategory.TabIndex = 2;
            this.gbxCategory.TabStop = false;
            this.gbxCategory.Text = "Category";
            // 
            // trvCategory
            // 
            this.trvCategory.Location = new System.Drawing.Point(19, 19);
            this.trvCategory.Name = "trvCategory";
            treeNode1.Name = "ndGeneral";
            treeNode1.Text = "General";
            treeNode2.Name = "ndStair";
            treeNode2.Text = "Stair";
            treeNode3.Name = "ndBalconies";
            treeNode3.Text = "Balconies";
            treeNode4.Name = "ndCategory";
            treeNode4.Text = "Category A";
            treeNode5.Name = "ndCategoryB";
            treeNode5.Text = "Category B";
            treeNode6.Name = "ndC1";
            treeNode6.Text = "C1";
            treeNode7.Name = "cdC2";
            treeNode7.Text = "C2";
            treeNode8.Name = "cdC3";
            treeNode8.Text = "C3";
            treeNode9.Name = "ndC4";
            treeNode9.Text = "C4";
            treeNode10.Name = "ndC5";
            treeNode10.Text = "C5";
            treeNode11.Name = "ndCategoryC";
            treeNode11.Text = "Category C";
            treeNode12.Name = "ndD1";
            treeNode12.Text = "D1";
            treeNode13.Name = "ndD2";
            treeNode13.Text = "D2";
            treeNode14.Name = "ndCategoryD";
            treeNode14.Text = "Category D";
            treeNode15.Name = "ndCategoryE";
            treeNode15.Text = "Category E";
            this.trvCategory.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode11,
            treeNode14,
            treeNode15});
            this.trvCategory.Size = new System.Drawing.Size(153, 146);
            this.trvCategory.TabIndex = 4;
            this.trvCategory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvCategory_AfterSelect);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ntxtMagnitude);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(202, 79);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(226, 48);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Magnitude";
            // 
            // ntxtMagnitude
            // 
            this.ntxtMagnitude.AutomaticResize = false;
            this.ntxtMagnitude.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtMagnitude.DoubleValue = 0D;
            this.ntxtMagnitude.IntValue = 0;
            this.ntxtMagnitude.Location = new System.Drawing.Point(94, 19);
            this.ntxtMagnitude.Name = "ntxtMagnitude";
            this.ntxtMagnitude.Size = new System.Drawing.Size(100, 20);
            this.ntxtMagnitude.TabIndex = 11;
            this.ntxtMagnitude.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Magnitude";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.cbxForceUnit);
            this.groupBox5.Controls.Add(this.cbxLengthUnit);
            this.groupBox5.Location = new System.Drawing.Point(203, -1);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(226, 79);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Units";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(128, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Force";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Length";
            // 
            // cbxForceUnit
            // 
            this.cbxForceUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxForceUnit.FormattingEnabled = true;
            this.cbxForceUnit.Location = new System.Drawing.Point(168, 28);
            this.cbxForceUnit.Name = "cbxForceUnit";
            this.cbxForceUnit.Size = new System.Drawing.Size(42, 21);
            this.cbxForceUnit.TabIndex = 7;
            this.cbxForceUnit.SelectedIndexChanged += new System.EventHandler(this.cbxForceUnit_SelectedIndexChanged);
            // 
            // cbxLengthUnit
            // 
            this.cbxLengthUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLengthUnit.FormattingEnabled = true;
            this.cbxLengthUnit.Location = new System.Drawing.Point(52, 28);
            this.cbxLengthUnit.Name = "cbxLengthUnit";
            this.cbxLengthUnit.Size = new System.Drawing.Size(43, 21);
            this.cbxLengthUnit.TabIndex = 6;
            this.cbxLengthUnit.SelectedIndexChanged += new System.EventHandler(this.cbxLengthUnit_SelectedIndexChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblDescription);
            this.groupBox6.Location = new System.Drawing.Point(199, 133);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(226, 117);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Description";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 16);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(0, 13);
            this.lblDescription.TabIndex = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(338, 256);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAssign
            // 
            this.btnAssign.Location = new System.Drawing.Point(252, 256);
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.Size = new System.Drawing.Size(75, 23);
            this.btnAssign.TabIndex = 10;
            this.btnAssign.Text = "Assign";
            this.btnAssign.UseVisualStyleBackColor = true;
            this.btnAssign.Click += new System.EventHandler(this.btnAssign_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.cbxActionType);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(9, -1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(187, 79);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Action ";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(19, 53);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(122, 17);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.Text = "Assign factored load";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // cbxActionType
            // 
            this.cbxActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxActionType.FormattingEnabled = true;
            this.cbxActionType.Items.AddRange(new object[] {
            "Live ",
            "Dead"});
            this.cbxActionType.Location = new System.Drawing.Point(90, 20);
            this.cbxActionType.Name = "cbxActionType";
            this.cbxActionType.Size = new System.Drawing.Size(70, 21);
            this.cbxActionType.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Action Type";
            // 
            // eAssingSlabLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 288);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAssign);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbxCategory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eAssingSlabLoad";
            this.ShowInTaskbar = false;
            this.Text = "Assing Slab Load";
            this.gbxCategory.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxCategory;
        private System.Windows.Forms.TreeView trvCategory;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxForceUnit;
        private System.Windows.Forms.ComboBox cbxLengthUnit;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAssign;
        private GUI.Controls.eNumericTextBox ntxtMagnitude;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox cbxActionType;
        private System.Windows.Forms.Label label4;

    }
}