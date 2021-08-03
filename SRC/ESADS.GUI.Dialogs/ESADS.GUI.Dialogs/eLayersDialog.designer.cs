namespace ESADS.GUI
{
    partial class eLayersDialog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.dgrvLayers = new System.Windows.Forms.DataGridView();
            this.dgrvclmnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgrvclmnColor = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dgrvclmnLineType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgrvclmnLineWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgrvclmnFont = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dgrvclmnShow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgrvLayers)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(520, 356);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(439, 356);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(358, 356);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // dgrvLayers
            // 
            this.dgrvLayers.AllowUserToAddRows = false;
            this.dgrvLayers.AllowUserToDeleteRows = false;
            this.dgrvLayers.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgrvLayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrvLayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgrvclmnName,
            this.dgrvclmnColor,
            this.dgrvclmnLineType,
            this.dgrvclmnLineWeight,
            this.dgrvclmnFont,
            this.dgrvclmnShow});
            this.dgrvLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrvLayers.Location = new System.Drawing.Point(3, 16);
            this.dgrvLayers.Name = "dgrvLayers";
            this.dgrvLayers.RowHeadersVisible = false;
            this.dgrvLayers.Size = new System.Drawing.Size(577, 276);
            this.dgrvLayers.TabIndex = 3;
            this.dgrvLayers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrvLayers_CellClick);
            this.dgrvLayers.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrvLayers_CellValueChanged);
            // 
            // dgrvclmnName
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgrvclmnName.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgrvclmnName.HeaderText = "Name";
            this.dgrvclmnName.Name = "dgrvclmnName";
            this.dgrvclmnName.ReadOnly = true;
            this.dgrvclmnName.Width = 120;
            // 
            // dgrvclmnColor
            // 
            this.dgrvclmnColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dgrvclmnColor.HeaderText = "Color";
            this.dgrvclmnColor.Name = "dgrvclmnColor";
            this.dgrvclmnColor.ReadOnly = true;
            this.dgrvclmnColor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgrvclmnColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgrvclmnColor.Width = 50;
            // 
            // dgrvclmnLineType
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgrvclmnLineType.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgrvclmnLineType.HeaderText = "Line Type";
            this.dgrvclmnLineType.Name = "dgrvclmnLineType";
            this.dgrvclmnLineType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgrvclmnLineType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgrvclmnLineType.Width = 80;
            // 
            // dgrvclmnLineWeight
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgrvclmnLineWeight.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgrvclmnLineWeight.HeaderText = "Line Weight";
            this.dgrvclmnLineWeight.Name = "dgrvclmnLineWeight";
            // 
            // dgrvclmnFont
            // 
            this.dgrvclmnFont.HeaderText = "Font";
            this.dgrvclmnFont.Name = "dgrvclmnFont";
            this.dgrvclmnFont.ReadOnly = true;
            this.dgrvclmnFont.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgrvclmnFont.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgrvclmnFont.Width = 150;
            // 
            // dgrvclmnShow
            // 
            this.dgrvclmnShow.HeaderText = "Show";
            this.dgrvclmnShow.Name = "dgrvclmnShow";
            this.dgrvclmnShow.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgrvclmnShow.Width = 50;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgrvLayers);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(583, 295);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(15, 313);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(577, 37);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(446, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scroll through the available layers in the above table and alter the values by cl" +
                "icking on them.";
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHelp.Location = new System.Drawing.Point(12, 356);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 6;
            this.btnHelp.Text = "&Help...";
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // eLayersDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(607, 391);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eLayersDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Layers";
            ((System.ComponentModel.ISupportInitialize)(this.dgrvLayers)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.DataGridView dgrvLayers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgrvclmnName;
        private System.Windows.Forms.DataGridViewButtonColumn dgrvclmnColor;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgrvclmnLineType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgrvclmnLineWeight;
        private System.Windows.Forms.DataGridViewButtonColumn dgrvclmnFont;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgrvclmnShow;
    }
}