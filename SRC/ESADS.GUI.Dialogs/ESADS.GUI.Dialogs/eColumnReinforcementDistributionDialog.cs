using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESADS.Mechanics.Design.Column;

namespace ESADS.GUI
{
    public partial class eColumnReinforcementDistributionDialog : Form
    {
        private eDetailType detailType;
        private int Xbars;
        private int Ybars;      
       

        public eColumnReinforcementDistributionDialog(eDetailType detailType)
        {
            InitializeComponent();
            this.detailType = detailType;
            rbtnType1.Checked = detailType == eDetailType.Type1;
            rbtnType2.Checked = detailType == eDetailType.Type2;
            rbtnType3.Checked = detailType == eDetailType.Type3;
        }

        public eDetailType DetailType
        {
            get
            {
                return detailType;
            }
            set
            {
                detailType = value;
            }
        }
        public int XBars
        {
            get
            {
                return Xbars;
            }
            set
            {
               ntxtBarsInXdirxn.IntValue =  Xbars = value;
                
            }
        }

        public int YBars
        {
            get
            {
                return Ybars;
            }
            set
            {
               ntxtBarsInYDirxn.IntValue =  Ybars = value;
            }
        }
        private void rbtnType3_CheckedChanged(object sender, EventArgs e)
        {
            gbxNumberOfBars.Enabled = !gbxNumberOfBars.Enabled;
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Xbars = ntxtBarsInXdirxn.IntValue;
            this.Ybars = ntxtBarsInYDirxn.IntValue;
            if (rbtnType1.Checked)
                detailType = eDetailType.Type1;
            else if (rbtnType2.Checked)
                detailType = eDetailType.Type2;
            else if (rbtnType3.Checked)
                detailType = eDetailType.Type3;
            else
                detailType = eDetailType.Type4;
            this.Close();
        }

        private void btnCacel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbxType1_Click(object sender, EventArgs e)
        {
            rbtnType1.Checked = true;
        }

        private void pbxType2_Click(object sender, EventArgs e)
        {
            rbtnType2.Checked = true;
        }

        private void pbxType3_Click(object sender, EventArgs e)
        {
            rbtnType3.Checked = true;
        }

        private void rbtnType4_CheckedChanged(object sender, EventArgs e)
        {
            gbxNumberOfBars.Enabled = !gbxNumberOfBars.Enabled;
        }

        private void pbxType4_Click(object sender, EventArgs e)
        {
            rbtnType4.Checked = true;
        }

    }
}
