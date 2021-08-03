using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ESADS.GUI
{
    public partial class eStartUpPictureForm : Form
    {
        /// <summary>
        /// Contains all the necessary information about each developer.
        /// </summary>
        private string[] developersDescription = new string[4];
        /// <summary>
        /// The index of the developer that is currently displayed.
        /// </summary>
        private int developerDescIndex = 0;

        public eStartUpPictureForm()
        {
            InitializeComponent();
        }

        private void startUpPictureTimer_Tick(object sender, EventArgs e)
        {
            startUpPictureTimer.Stop(); // stops the timer since only one tick in specfied tick interval is needed.
            this.Close();// closes the form and let the main application to run.
        }

        private void eStartUpPictureForm_Load(object sender, EventArgs e)
        {
            //Initialize Developers Description.
            developersDescription[0] = "Zeineba Mehedi";
            developersDescription[1] = "Tsinuel Nurillign";
            developersDescription[2] = "Abiy Fantaye";

            //Starts the timers to tick;
            startUpPictureTimer.Start();
            developersDisplayTimer.Start();
        }

        private void developersDisplayTimer_Tick(object sender, EventArgs e)
        {
            //Writes the name and description of each developer during the start up picture show.
            lblDeveloperDescription.Text = "Developers...\n" + developersDescription[developerDescIndex];

            //Checks if there is no any devloper remain.
            if (developerDescIndex < developersDescription.Length - 1)
            {
                // Incrininates the developers description index by one so that the name and
                // the description of the next developer can be displayed.
                developerDescIndex++;
            }
            else
            {
                // Stops the developers timer from ticking if all developers are displayed. 
                developersDisplayTimer.Stop();
            }
        }
    }
}
