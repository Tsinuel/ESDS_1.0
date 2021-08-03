namespace ESADS.GUI
{
    partial class eNewModelDialog
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
            System.Windows.Forms.GroupBox gbxProjectDescription;
            System.Windows.Forms.GroupBox gbxUnits;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            this.btnEditProjectInfo = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxLengthUnit = new System.Windows.Forms.ComboBox();
            this.cbxForceUnit = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pbxFootingTemplate = new System.Windows.Forms.PictureBox();
            this.pbxColumnTemplate = new System.Windows.Forms.PictureBox();
            this.pbxSlabTemplate = new System.Windows.Forms.PictureBox();
            this.pbxBeamTemplate = new System.Windows.Forms.PictureBox();
            gbxProjectDescription = new System.Windows.Forms.GroupBox();
            gbxUnits = new System.Windows.Forms.GroupBox();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            gbxProjectDescription.SuspendLayout();
            gbxUnits.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFootingTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxColumnTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSlabTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBeamTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxProjectDescription
            // 
            gbxProjectDescription.Controls.Add(this.btnEditProjectInfo);
            gbxProjectDescription.Location = new System.Drawing.Point(12, 12);
            gbxProjectDescription.Name = "gbxProjectDescription";
            gbxProjectDescription.Size = new System.Drawing.Size(162, 52);
            gbxProjectDescription.TabIndex = 7;
            gbxProjectDescription.TabStop = false;
            gbxProjectDescription.Text = "Project Description";
            // 
            // btnEditProjectInfo
            // 
            this.btnEditProjectInfo.Location = new System.Drawing.Point(6, 19);
            this.btnEditProjectInfo.Name = "btnEditProjectInfo";
            this.btnEditProjectInfo.Size = new System.Drawing.Size(150, 23);
            this.btnEditProjectInfo.TabIndex = 0;
            this.btnEditProjectInfo.Text = "Project Info...";
            this.btnEditProjectInfo.UseVisualStyleBackColor = true;
            this.btnEditProjectInfo.Click += new System.EventHandler(this.btnEditProjectInfo_Click);
            // 
            // gbxUnits
            // 
            gbxUnits.Controls.Add(this.label2);
            gbxUnits.Controls.Add(this.cbxLengthUnit);
            gbxUnits.Controls.Add(this.cbxForceUnit);
            gbxUnits.Controls.Add(this.label1);
            gbxUnits.Location = new System.Drawing.Point(189, 12);
            gbxUnits.Name = "gbxUnits";
            gbxUnits.Size = new System.Drawing.Size(219, 52);
            gbxUnits.TabIndex = 8;
            gbxUnits.TabStop = false;
            gbxUnits.Text = "Units";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Force:";
            // 
            // cbxLengthUnit
            // 
            this.cbxLengthUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxLengthUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLengthUnit.Location = new System.Drawing.Point(55, 21);
            this.cbxLengthUnit.Name = "cbxLengthUnit";
            this.cbxLengthUnit.Size = new System.Drawing.Size(48, 21);
            this.cbxLengthUnit.TabIndex = 7;
            // 
            // cbxForceUnit
            // 
            this.cbxForceUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxForceUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxForceUnit.Location = new System.Drawing.Point(158, 21);
            this.cbxForceUnit.Name = "cbxForceUnit";
            this.cbxForceUnit.Size = new System.Drawing.Size(46, 21);
            this.cbxForceUnit.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Length:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.Location = new System.Drawing.Point(75, 123);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(44, 16);
            label3.TabIndex = 6;
            label3.Text = "Beam";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.Location = new System.Drawing.Point(259, 253);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(53, 16);
            label4.TabIndex = 7;
            label4.Text = "Footing";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.Location = new System.Drawing.Point(75, 253);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(36, 16);
            label5.TabIndex = 8;
            label5.Text = "Slab";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label6.Location = new System.Drawing.Point(259, 123);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(53, 16);
            label6.TabIndex = 9;
            label6.Text = "Column";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(label6);
            this.groupBox2.Controls.Add(label5);
            this.groupBox2.Controls.Add(label4);
            this.groupBox2.Controls.Add(label3);
            this.groupBox2.Controls.Add(this.pbxFootingTemplate);
            this.groupBox2.Controls.Add(this.pbxColumnTemplate);
            this.groupBox2.Controls.Add(this.pbxSlabTemplate);
            this.groupBox2.Controls.Add(this.pbxBeamTemplate);
            this.groupBox2.Location = new System.Drawing.Point(12, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(396, 276);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Template";
            // 
            // pbxFootingTemplate
            // 
            this.pbxFootingTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbxFootingTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxFootingTemplate.Image = global::ESADS.GUI.Properties.Resources.Footing_sd;
            this.pbxFootingTemplate.Location = new System.Drawing.Point(200, 149);
            this.pbxFootingTemplate.Name = "pbxFootingTemplate";
            this.pbxFootingTemplate.Size = new System.Drawing.Size(181, 101);
            this.pbxFootingTemplate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxFootingTemplate.TabIndex = 5;
            this.pbxFootingTemplate.TabStop = false;
            this.pbxFootingTemplate.Click += new System.EventHandler(this.pbxFootingTemplate_Click);
            this.pbxFootingTemplate.MouseEnter += new System.EventHandler(this.pbxFootingTemplate_MouseEnter);
            this.pbxFootingTemplate.MouseLeave += new System.EventHandler(this.pbxFootingTemplate_MouseLeave);
            // 
            // pbxColumnTemplate
            // 
            this.pbxColumnTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbxColumnTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxColumnTemplate.Image = global::ESADS.GUI.Properties.Resources.column;
            this.pbxColumnTemplate.Location = new System.Drawing.Point(200, 19);
            this.pbxColumnTemplate.Name = "pbxColumnTemplate";
            this.pbxColumnTemplate.Size = new System.Drawing.Size(181, 101);
            this.pbxColumnTemplate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxColumnTemplate.TabIndex = 4;
            this.pbxColumnTemplate.TabStop = false;
            this.pbxColumnTemplate.Click += new System.EventHandler(this.pbxColumnTemplate_Click);
            this.pbxColumnTemplate.MouseEnter += new System.EventHandler(this.pbxColumnTemplate_MouseEnter);
            this.pbxColumnTemplate.MouseLeave += new System.EventHandler(this.pbxColumnTemplate_MouseLeave);
            // 
            // pbxSlabTemplate
            // 
            this.pbxSlabTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxSlabTemplate.Image = global::ESADS.GUI.Properties.Resources.SLAB;
            this.pbxSlabTemplate.Location = new System.Drawing.Point(6, 149);
            this.pbxSlabTemplate.Name = "pbxSlabTemplate";
            this.pbxSlabTemplate.Size = new System.Drawing.Size(181, 101);
            this.pbxSlabTemplate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxSlabTemplate.TabIndex = 3;
            this.pbxSlabTemplate.TabStop = false;
            this.pbxSlabTemplate.Click += new System.EventHandler(this.pbxSlabTemplate_Click);
            this.pbxSlabTemplate.MouseEnter += new System.EventHandler(this.pbxSlabTemplate_MouseEnter);
            this.pbxSlabTemplate.MouseLeave += new System.EventHandler(this.pbxSlabTemplate_MouseLeave);
            // 
            // pbxBeamTemplate
            // 
            this.pbxBeamTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbxBeamTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxBeamTemplate.Image = global::ESADS.GUI.Properties.Resources.Beam_Icon1;
            this.pbxBeamTemplate.Location = new System.Drawing.Point(6, 19);
            this.pbxBeamTemplate.Name = "pbxBeamTemplate";
            this.pbxBeamTemplate.Size = new System.Drawing.Size(181, 101);
            this.pbxBeamTemplate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxBeamTemplate.TabIndex = 1;
            this.pbxBeamTemplate.TabStop = false;
            this.pbxBeamTemplate.Click += new System.EventHandler(this.pbxBeamTemplate_Click);
            this.pbxBeamTemplate.MouseEnter += new System.EventHandler(this.pbxBeamTemplate_MouseEnter);
            this.pbxBeamTemplate.MouseLeave += new System.EventHandler(this.pbxBeamTemplate_MouseLeave);
            // 
            // eNewModelDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 358);
            this.Controls.Add(gbxUnits);
            this.Controls.Add(gbxProjectDescription);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eNewModelDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Model ";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.eNewModelDialog_KeyDown);
            gbxProjectDescription.ResumeLayout(false);
            gbxUnits.ResumeLayout(false);
            gbxUnits.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFootingTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxColumnTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSlabTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBeamTemplate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEditProjectInfo;
        private System.Windows.Forms.ComboBox cbxForceUnit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxLengthUnit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pbxFootingTemplate;
        private System.Windows.Forms.PictureBox pbxColumnTemplate;
        private System.Windows.Forms.PictureBox pbxSlabTemplate;
        private System.Windows.Forms.PictureBox pbxBeamTemplate;
    }
}