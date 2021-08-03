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
    public partial class eQuickSlabGridDialog : Form
    {
        private int numOfHor;
        private int numOfVer;
        private double ySpacing;
        private double xSpacing;

        public int NumOfH_Grids
        {
            get
            {
                return this.numOfHor;
            }
        }

        public int NumOfV_Grids
        {
            get
            {
                return this.numOfVer;
            }
        }

        public double X_Spacing
        {
            get
            {
                return this.xSpacing;
            }
        }

        public double Y_Spacing
        {
            get
            {
                return this.ySpacing;
            }
        }

        public eQuickSlabGridDialog(eLengthUnits lengthUnit, int numOfVer, int numOfHor)
        {
            InitializeComponent();
            cbxLength.Items.AddRange(Enum.GetNames(typeof(eLengthUnits)));
            cbxLength.Text = lengthUnit.ToString();

            ntxtNumOfHor.IntValue = numOfHor;
            ntxtNumOfVer.IntValue = numOfVer;
        }

        private void cbxLength_SelectedIndexChanged(object sender, EventArgs e)
        {
            ntxtXGridSpacing.LengthUnit = (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cbxLength.Text);
            ntxtYGridSpacing.LengthUnit = (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cbxLength.Text);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!Valid())
                return;

            this.numOfHor = ntxtNumOfHor.IntValue;
            this.numOfVer = ntxtNumOfVer.IntValue;
            this.xSpacing = ntxtXGridSpacing.SU;
            this.ySpacing = ntxtYGridSpacing.SU;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool Valid()
        {
            bool valid = true;
            string note = "";

            if (ntxtNumOfHor.IntValue < 2 || ntxtNumOfVer.IntValue < 2)
            {
                valid = false;
                note = "The minimum number of grids in each direction is two";
            }
            if (ntxtXGridSpacing.DoubleValue <= 0.0 || ntxtYGridSpacing.DoubleValue <= 0.0)
            {
                valid = false;
                note = "The spacing of grids cannot be zero or negative";
            }
            if (!valid)
                MessageBox.Show(note, "Data invalid!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return valid;
        }


    }
}
