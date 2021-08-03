using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESADS.Mechanics.Analysis;
using ESADS.Mechanics.Analysis.Beam;

namespace ESADS.GUI
{
    public partial class eAssignJointDialog : Form
    {
        private eJointType jointType;
        private double supportWidth;
        private eDocument document;

        public eJointType JointTpe
        {
            get
            {
                return jointType;
            }
        }

        public double SupportWidth
        {
            get
            {
                return supportWidth;
            }
        }

        public eAssignJointDialog(eDocument document)
        {
            this.document = document;
            InitializeComponent();
            ntxtSupportWidth.DoubleValue = eUtility.Convert(300, eLengthUnits.mm, document.LengthUnit);
        }

        private void picbxPin_Click(object sender, EventArgs e)
        {
            if (!radPin.Checked)
            {
                radPin.Checked = true;

                lblDescription.Text = "It is a type of support that does not have resistance to rotation but only for axial load and shear.";
            }
        }

        private void picbxHinge_Click(object sender, EventArgs e)
        {
            if (!radHinge.Checked)
            {
                radHinge.Checked = true;

                lblDescription.Text = "It is a type of connection between member that transfers shear and axial force from one member to the other.";
            }
        }

        private void picbxFixed_Click(object sender, EventArgs e)
        {
            if (!radFixed.Checked)
            {
                radFixed.Checked = true;

                lblDescription.Text = "It is a type of support that has resistance to shear, axial force and moment.";
            }
        }

        private void picbxHRoller_Click(object sender, EventArgs e)
        {
            if (!radHRoller.Checked)
            {
                radHRoller.Checked = true;

                lblDescription.Text = "It is a type of support that has resistance to shear only and not axial load and moment.";
            }
        }

        private void picbxVRoller_Click(object sender, EventArgs e)
        {
            if (!radVRoller.Checked)
            {
                radVRoller.Checked = true;

                lblDescription.Text = "It is a type of support that has resistance to axial load and moment and not shear. " +
                "When it assigned for an interior joint, it becomes a connection that transfers moment and axial force only.";
            }
        }

        private void picbxContinuous_Click(object sender, EventArgs e)
        {
            if (!radContinuous.Checked)
            {
                radContinuous.Checked = true;

                lblDescription.Text = "It is a connection between two members whereby both moment and shear.";
            }
        }

        private void radPin_CheckedChanged(object sender, EventArgs e)
        {
            if (radPin.Checked)
            {
                btnOk.Enabled = true;
                picbxPin.BorderStyle = BorderStyle.Fixed3D;
            }
            else
                picbxPin.BorderStyle = BorderStyle.FixedSingle;
        }

        private void radHinge_CheckedChanged(object sender, EventArgs e)
        {
            if (radHinge.Checked)
            {
                btnOk.Enabled = true;
                picbxHinge.BorderStyle = BorderStyle.Fixed3D;
            }
            else
                picbxHinge.BorderStyle = BorderStyle.FixedSingle;
        }

        private void radFixed_CheckedChanged(object sender, EventArgs e)
        {
            if (radFixed.Checked)
            {
                btnOk.Enabled = true;
                picbxFixed.BorderStyle = BorderStyle.Fixed3D;
            }
            else
                picbxFixed.BorderStyle = BorderStyle.FixedSingle;
        }

        private void radHRoller_CheckedChanged(object sender, EventArgs e)
        {
            if (radHRoller.Checked)
            {
                btnOk.Enabled = true;
                picbxHRoller.BorderStyle = BorderStyle.Fixed3D;
            }
            else
                picbxHRoller.BorderStyle = BorderStyle.FixedSingle;
        }

        private void radVRoller_CheckedChanged(object sender, EventArgs e)
        {
            if (radVRoller.Checked)
            {
                btnOk.Enabled = true;
                picbxVRoller.BorderStyle = BorderStyle.Fixed3D;
            }
            else
                picbxVRoller.BorderStyle = BorderStyle.FixedSingle;
        }

        private void radContinuous_CheckedChanged(object sender, EventArgs e)
        {
            if (radContinuous.Checked)
            {
                btnOk.Enabled = true;
                picbxContinuous.BorderStyle = BorderStyle.Fixed3D;
            }
            else
                picbxContinuous.BorderStyle = BorderStyle.FixedSingle;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ntxtSupportWidth.DoubleValue <= 0)
            {
                MessageBox.Show("The support width value cannot be zero or negative.");
                return;
            }

            if (radFixed.Checked)
                this.jointType = eJointType.Fixed;
            else if (radHinge.Checked)
                this.jointType = eJointType.Hinge;
            else if (radHRoller.Checked)
                this.jointType = eJointType.Roller;
            else if (radPin.Checked)
                this.jointType = eJointType.Pin;
            else if (radVRoller.Checked)
                this.jointType = eJointType.VerticalRoller;
            else
                this.jointType = eJointType.Continious;

            this.supportWidth = eUtility.Convert(ntxtSupportWidth.DoubleValue, this.document.LengthUnit, eUtility.SLU);

            this.Close();
        }
    }
}
