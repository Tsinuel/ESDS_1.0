namespace ESADS.GUI
{
    partial class eStartUpPictureForm
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
            this.components = new System.ComponentModel.Container();
            this.startUpPictureTimer = new System.Windows.Forms.Timer(this.components);
            this.developersDisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.lblDeveloperDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startUpPictureTimer
            // 
            this.startUpPictureTimer.Interval = 1200;
            this.startUpPictureTimer.Tick += new System.EventHandler(this.startUpPictureTimer_Tick);
            // 
            // developersDisplayTimer
            // 
            this.developersDisplayTimer.Interval = 400;
            this.developersDisplayTimer.Tick += new System.EventHandler(this.developersDisplayTimer_Tick);
            // 
            // lblDeveloperDescription
            // 
            this.lblDeveloperDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDeveloperDescription.CausesValidation = false;
            this.lblDeveloperDescription.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblDeveloperDescription.Location = new System.Drawing.Point(161, 9);
            this.lblDeveloperDescription.Name = "lblDeveloperDescription";
            this.lblDeveloperDescription.Size = new System.Drawing.Size(415, 41);
            this.lblDeveloperDescription.TabIndex = 0;
            // 
            // eStartUpPictureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ESADS.GUI.Properties.Resources.ESADS_V3_1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(713, 400);
            this.ControlBox = false;
            this.Controls.Add(this.lblDeveloperDescription);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "eStartUpPictureForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.eStartUpPictureForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer startUpPictureTimer;
        private System.Windows.Forms.Timer developersDisplayTimer;
        private System.Windows.Forms.Label lblDeveloperDescription;
    }
}