namespace ESADS.GUI
{
    partial class eEditSlabGridDialog
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
            this.dgvHor_Grid = new System.Windows.Forms.DataGridView();
            this.clmnHGridID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnYCoordinateOrSpacing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnHShow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmnHBubbleLocation = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvVert_Grid = new System.Windows.Forms.DataGridView();
            this.clmnVGridID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnXCoordinateOrSpacing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnVShow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmnVBubbleLocation = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxLength = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pbxPreview = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnQuickGrid = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ntxtBubbleSize = new ESADS.GUI.Controls.eNumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkShowAllGrids = new System.Windows.Forms.CheckBox();
            this.btnReorderCoordinates = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.radSpacing = new System.Windows.Forms.RadioButton();
            this.radOrdinate = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHor_Grid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVert_Grid)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPreview)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvHor_Grid
            // 
            this.dgvHor_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHor_Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmnHGridID,
            this.clmnYCoordinateOrSpacing,
            this.clmnHShow,
            this.clmnHBubbleLocation});
            this.dgvHor_Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHor_Grid.Location = new System.Drawing.Point(3, 16);
            this.dgvHor_Grid.Name = "dgvHor_Grid";
            this.dgvHor_Grid.Size = new System.Drawing.Size(489, 231);
            this.dgvHor_Grid.TabIndex = 1;
            this.dgvHor_Grid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGrid_CellValueChanged);
            this.dgvHor_Grid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvGrid_RowsAdded);
            this.dgvHor_Grid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvGrid_RowsRemoved);
            // 
            // clmnHGridID
            // 
            this.clmnHGridID.HeaderText = "Grid ID";
            this.clmnHGridID.Name = "clmnHGridID";
            // 
            // clmnYCoordinateOrSpacing
            // 
            this.clmnYCoordinateOrSpacing.HeaderText = "Y Coordinate";
            this.clmnYCoordinateOrSpacing.Name = "clmnYCoordinateOrSpacing";
            // 
            // clmnHShow
            // 
            this.clmnHShow.HeaderText = "Show";
            this.clmnHShow.Name = "clmnHShow";
            this.clmnHShow.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmnHShow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // clmnHBubbleLocation
            // 
            this.clmnHBubbleLocation.HeaderText = "BubbleLocation";
            this.clmnHBubbleLocation.Name = "clmnHBubbleLocation";
            this.clmnHBubbleLocation.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmnHBubbleLocation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvHor_Grid);
            this.groupBox1.Location = new System.Drawing.Point(12, 268);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 250);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Horizontal (Y) Grid Data";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvVert_Grid);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(495, 250);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Vertical (X) Grid Data";
            // 
            // dgvVert_Grid
            // 
            this.dgvVert_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVert_Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmnVGridID,
            this.clmnXCoordinateOrSpacing,
            this.clmnVShow,
            this.clmnVBubbleLocation});
            this.dgvVert_Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVert_Grid.Location = new System.Drawing.Point(3, 16);
            this.dgvVert_Grid.Name = "dgvVert_Grid";
            this.dgvVert_Grid.Size = new System.Drawing.Size(489, 231);
            this.dgvVert_Grid.TabIndex = 1;
            this.dgvVert_Grid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGrid_CellValueChanged);
            this.dgvVert_Grid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvGrid_RowsAdded);
            this.dgvVert_Grid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvGrid_RowsRemoved);
            // 
            // clmnVGridID
            // 
            this.clmnVGridID.HeaderText = "Grid ID";
            this.clmnVGridID.Name = "clmnVGridID";
            // 
            // clmnXCoordinateOrSpacing
            // 
            this.clmnXCoordinateOrSpacing.HeaderText = "X Coordinate";
            this.clmnXCoordinateOrSpacing.Name = "clmnXCoordinateOrSpacing";
            // 
            // clmnVShow
            // 
            this.clmnVShow.HeaderText = "Show";
            this.clmnVShow.Name = "clmnVShow";
            this.clmnVShow.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmnVShow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // clmnVBubbleLocation
            // 
            this.clmnVBubbleLocation.HeaderText = "BubbleLocation";
            this.clmnVBubbleLocation.Name = "clmnVBubbleLocation";
            this.clmnVBubbleLocation.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmnVBubbleLocation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(636, 530);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(555, 530);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.cbxLength);
            this.groupBox5.Location = new System.Drawing.Point(513, 245);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(196, 50);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Units";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Length Unit";
            // 
            // cbxLength
            // 
            this.cbxLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLength.FormattingEnabled = true;
            this.cbxLength.Location = new System.Drawing.Point(108, 19);
            this.cbxLength.Name = "cbxLength";
            this.cbxLength.Size = new System.Drawing.Size(80, 21);
            this.cbxLength.TabIndex = 6;
            this.cbxLength.SelectedIndexChanged += new System.EventHandler(this.cbxLength_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.pbxPreview);
            this.groupBox3.Location = new System.Drawing.Point(513, 68);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(196, 171);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Preview";
            // 
            // pbxPreview
            // 
            this.pbxPreview.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxPreview.Location = new System.Drawing.Point(3, 16);
            this.pbxPreview.Name = "pbxPreview";
            this.pbxPreview.Size = new System.Drawing.Size(190, 152);
            this.pbxPreview.TabIndex = 0;
            this.pbxPreview.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnQuickGrid);
            this.groupBox4.Location = new System.Drawing.Point(513, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(199, 50);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Quick Definition";
            // 
            // btnQuickGrid
            // 
            this.btnQuickGrid.Location = new System.Drawing.Point(6, 19);
            this.btnQuickGrid.Name = "btnQuickGrid";
            this.btnQuickGrid.Size = new System.Drawing.Size(187, 23);
            this.btnQuickGrid.TabIndex = 0;
            this.btnQuickGrid.Text = "Quick Grid Definition...";
            this.btnQuickGrid.UseVisualStyleBackColor = true;
            this.btnQuickGrid.Click += new System.EventHandler(this.btnQuickGrid_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.ntxtBubbleSize);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.chkShowAllGrids);
            this.groupBox6.Location = new System.Drawing.Point(513, 390);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(196, 74);
            this.groupBox6.TabIndex = 17;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Applicable to all grids";
            // 
            // ntxtBubbleSize
            // 
            this.ntxtBubbleSize.AutomaticResize = false;
            this.ntxtBubbleSize.DataType = ESADS.GUI.Controls.eDataType.Decimal;
            this.ntxtBubbleSize.DoubleValue = 0D;
            this.ntxtBubbleSize.ForceUnit = ESADS.eForceUints.KN;
            this.ntxtBubbleSize.IntValue = 0;
            this.ntxtBubbleSize.LengthUnit = ESADS.eLengthUnits.m;
            this.ntxtBubbleSize.Location = new System.Drawing.Point(92, 42);
            this.ntxtBubbleSize.Measurment = ESADS.GUI.Controls.eMeasurment.None;
            this.ntxtBubbleSize.Name = "ntxtBubbleSize";
            this.ntxtBubbleSize.Size = new System.Drawing.Size(80, 20);
            this.ntxtBubbleSize.SU = 0D;
            this.ntxtBubbleSize.TabIndex = 9;
            this.ntxtBubbleSize.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Bubble size";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkShowAllGrids
            // 
            this.chkShowAllGrids.AutoSize = true;
            this.chkShowAllGrids.Checked = true;
            this.chkShowAllGrids.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkShowAllGrids.Location = new System.Drawing.Point(47, 19);
            this.chkShowAllGrids.Name = "chkShowAllGrids";
            this.chkShowAllGrids.Size = new System.Drawing.Size(91, 17);
            this.chkShowAllGrids.TabIndex = 0;
            this.chkShowAllGrids.Text = "Show all grids";
            this.chkShowAllGrids.UseVisualStyleBackColor = true;
            this.chkShowAllGrids.CheckedChanged += new System.EventHandler(this.chkShowAllGrids_CheckedChanged);
            // 
            // btnReorderCoordinates
            // 
            this.btnReorderCoordinates.Location = new System.Drawing.Point(10, 51);
            this.btnReorderCoordinates.Name = "btnReorderCoordinates";
            this.btnReorderCoordinates.Size = new System.Drawing.Size(162, 23);
            this.btnReorderCoordinates.TabIndex = 10;
            this.btnReorderCoordinates.Text = "Reorder Coordinates";
            this.btnReorderCoordinates.UseVisualStyleBackColor = true;
            this.btnReorderCoordinates.Click += new System.EventHandler(this.btnReorderCoordinates_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.btnReorderCoordinates);
            this.groupBox7.Controls.Add(this.radSpacing);
            this.groupBox7.Controls.Add(this.radOrdinate);
            this.groupBox7.Location = new System.Drawing.Point(513, 301);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(193, 83);
            this.groupBox7.TabIndex = 18;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Input Options";
            // 
            // radSpacing
            // 
            this.radSpacing.AutoSize = true;
            this.radSpacing.Location = new System.Drawing.Point(108, 28);
            this.radSpacing.Name = "radSpacing";
            this.radSpacing.Size = new System.Drawing.Size(64, 17);
            this.radSpacing.TabIndex = 0;
            this.radSpacing.Text = "Spacing";
            this.radSpacing.UseVisualStyleBackColor = true;
            this.radSpacing.CheckedChanged += new System.EventHandler(this.radSpacing_CheckedChanged);
            // 
            // radOrdinate
            // 
            this.radOrdinate.AutoSize = true;
            this.radOrdinate.Checked = true;
            this.radOrdinate.Location = new System.Drawing.Point(15, 28);
            this.radOrdinate.Name = "radOrdinate";
            this.radOrdinate.Size = new System.Drawing.Size(65, 17);
            this.radOrdinate.TabIndex = 0;
            this.radOrdinate.TabStop = true;
            this.radOrdinate.Text = "Ordinate";
            this.radOrdinate.UseVisualStyleBackColor = true;
            // 
            // eEditSlabGridDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(723, 565);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eEditSlabGridDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Grid Data";
            ((System.ComponentModel.ISupportInitialize)(this.dgvHor_Grid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVert_Grid)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxPreview)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHor_Grid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvVert_Grid;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxLength;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pbxPreview;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnQuickGrid;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox chkShowAllGrids;
        private Controls.eNumericTextBox ntxtBubbleSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnReorderCoordinates;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton radSpacing;
        private System.Windows.Forms.RadioButton radOrdinate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnHGridID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnYCoordinateOrSpacing;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmnHShow;
        private System.Windows.Forms.DataGridViewComboBoxColumn clmnHBubbleLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnVGridID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnXCoordinateOrSpacing;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmnVShow;
        private System.Windows.Forms.DataGridViewComboBoxColumn clmnVBubbleLocation;

    }
}