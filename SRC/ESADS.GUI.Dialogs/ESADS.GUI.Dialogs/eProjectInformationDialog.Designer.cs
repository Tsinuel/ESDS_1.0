namespace ESADS.GUI
{
    partial class ProjectInformationDialog
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
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.btnInsertRow = new System.Windows.Forms.Button();
            this.btnDeletRow = new System.Windows.Forms.Button();
            this.dgvProjectInfo = new System.Windows.Forms.DataGridView();
            this.Items = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbxProjectInfo = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjectInfo)).BeginInit();
            this.gbxProjectInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(628, 203);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(628, 243);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(75, 23);
            this.btnClearAll.TabIndex = 5;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(429, 437);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(628, 57);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(75, 23);
            this.btnAddRow.TabIndex = 1;
            this.btnAddRow.Text = "Add Row";
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // btnInsertRow
            // 
            this.btnInsertRow.Location = new System.Drawing.Point(628, 95);
            this.btnInsertRow.Name = "btnInsertRow";
            this.btnInsertRow.Size = new System.Drawing.Size(75, 23);
            this.btnInsertRow.TabIndex = 2;
            this.btnInsertRow.Text = "Insert Row";
            this.btnInsertRow.UseVisualStyleBackColor = true;
            this.btnInsertRow.Click += new System.EventHandler(this.btnInsertRow_Click);
            // 
            // btnDeletRow
            // 
            this.btnDeletRow.Location = new System.Drawing.Point(628, 133);
            this.btnDeletRow.Name = "btnDeletRow";
            this.btnDeletRow.Size = new System.Drawing.Size(75, 23);
            this.btnDeletRow.TabIndex = 6;
            this.btnDeletRow.Text = "Delete Row";
            this.btnDeletRow.UseVisualStyleBackColor = true;
            this.btnDeletRow.Click += new System.EventHandler(this.btnDeletRow_Click);
            // 
            // dgvProjectInfo
            // 
            this.dgvProjectInfo.AllowUserToAddRows = false;
            this.dgvProjectInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProjectInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Items,
            this.Data});
            this.dgvProjectInfo.Location = new System.Drawing.Point(18, 19);
            this.dgvProjectInfo.Name = "dgvProjectInfo";
            this.dgvProjectInfo.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvProjectInfo.RowHeadersWidth = 45;
            this.dgvProjectInfo.ShowEditingIcon = false;
            this.dgvProjectInfo.Size = new System.Drawing.Size(594, 378);
            this.dgvProjectInfo.TabIndex = 0;
            // 
            // Items
            // 
            this.Items.HeaderText = "Items";
            this.Items.Name = "Items";
            this.Items.Width = 150;
            // 
            // Data
            // 
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.Width = 375;
            // 
            // gbxProjectInfo
            // 
            this.gbxProjectInfo.Controls.Add(this.btnReset);
            this.gbxProjectInfo.Controls.Add(this.dgvProjectInfo);
            this.gbxProjectInfo.Controls.Add(this.btnDeletRow);
            this.gbxProjectInfo.Controls.Add(this.btnInsertRow);
            this.gbxProjectInfo.Controls.Add(this.btnAddRow);
            this.gbxProjectInfo.Controls.Add(this.btnClearAll);
            this.gbxProjectInfo.Controls.Add(this.btnClear);
            this.gbxProjectInfo.Location = new System.Drawing.Point(12, 28);
            this.gbxProjectInfo.Name = "gbxProjectInfo";
            this.gbxProjectInfo.Size = new System.Drawing.Size(716, 403);
            this.gbxProjectInfo.TabIndex = 7;
            this.gbxProjectInfo.TabStop = false;
            this.gbxProjectInfo.Text = "Project Info";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(628, 282);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(549, 437);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ProjectInformationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 472);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbxProjectInfo);
            this.Name = "ProjectInformationDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Information";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjectInfo)).EndInit();
            this.gbxProjectInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnInsertRow;
        private System.Windows.Forms.Button btnDeletRow;
        private System.Windows.Forms.DataGridView dgvProjectInfo;
        private System.Windows.Forms.GroupBox gbxProjectInfo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.DataGridViewTextBoxColumn Items;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
    }
}