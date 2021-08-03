using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ESADS.GUI
{
    public partial class eColumnCustomReinforcementDistributionDialog : Form
    {
        private int numberOfSegments;
        private double horizontalRatio;

        
        public eColumnCustomReinforcementDistributionDialog()
        {
            InitializeComponent();
        }

        public int NumberOfSegments
        {
            get
            {
                return numberOfSegments;
            }
        }
        public double HorizontalRatio
        {
            get
            {
                return horizontalRatio;
            }
        }
        private void ntxtHorizontal_TextChanged(object sender, EventArgs e)
        {
            if (ntxtHorizontal.DoubleValue >= 0 && ntxtHorizontal.DoubleValue <= 1)           
                ntxtVertical.DoubleValue = 1 - ntxtHorizontal.DoubleValue;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ntxtHorizontal.DoubleValue > 0 && ntxtHorizontal.DoubleValue < 1 && ntxtNumberOfSeg >= 5)
            {
                horizontalRatio = ntxtHorizontal;
                numberOfSegments = ntxtNumberOfSeg;
                this.Close();
            }
            else if (ntxtNumberOfSeg < 5)
                MessageBox.Show("Using smaller number of points bellow 5 to apporximate a segment may lead to approximation error.", "Bellow minimum requirment!", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            else
                MessageBox.Show("The given distribution factor should be above zero and bellow one.", "Invalid Distribution Factor", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void chFiniteElements_CheckedChanged(object sender, EventArgs e)
        {
            label1.Enabled = !label1.Enabled;
            ntxtNumberOfSeg.Enabled = !ntxtNumberOfSeg.Enabled;
        }
    }
}
