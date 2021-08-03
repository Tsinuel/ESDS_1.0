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
    public partial class eCurtailmentRules : Form
    {
        private eDocument document;
        public eCurtailmentRules(eDocument document)
        {
            this.document = document;
            InitializeComponent();
            ntxtCompressive.DoubleValue = document.Beam.Beam_Design.SupportCompBarCuttingLength;
            ntxtTensile.DoubleValue = document.Beam.Beam_Design.SupportTensBarCuttingLength;
            btnApply.Enabled = false;
        }

        private void ntxtTensile_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void ntxtCompressive_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (ntxtCompressive.DoubleValue <= 0 || ntxtCompressive.DoubleValue > 1 || ntxtTensile.DoubleValue <= 0 || ntxtTensile.DoubleValue > 1)
            {
                MessageBox.Show("The value of the length ratio cannot be negative, zero or greater than one", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            document.Beam.Beam_Design.SupportCompBarCuttingLength = ntxtCompressive;
            document.Beam.Beam_Design.SupportTensBarCuttingLength = ntxtTensile;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ntxtCompressive.DoubleValue <= 0 || ntxtCompressive.DoubleValue > 1 || ntxtTensile.DoubleValue <= 0 || ntxtTensile.DoubleValue > 1)
            {
                MessageBox.Show("The value of the length ratio cannot be negative, zero or greater than one", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnApply_Click(sender, e);
        }
    }
}
