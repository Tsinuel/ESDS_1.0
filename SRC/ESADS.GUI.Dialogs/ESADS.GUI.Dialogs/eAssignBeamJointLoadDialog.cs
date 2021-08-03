using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESADS.Code;
using ESADS.GUI.Controls;

namespace ESADS.GUI
{
    public partial class eAssignBeamJointLoadDialog : Form
    {
        private eDocument document;
        private double magnitude;
        private eAssignOptions assignOption;
        private bool factored;
        private bool isForce;
        private eActionType actionType;

        public double Magnitude
        {
            get
            {
                return magnitude;
            }
        }

        public eAssignOptions AssignOption
        {
            get
            {
                return assignOption;
            }
        }

        public bool Factored
        {
            get
            {
                return factored;
            }
        }

        public eActionType ActionType
        {
            get
            {
                return this.actionType;
            }
        }

        public bool IsForce
        {
            get
            {
                return isForce;
            }
        }

        public eAssignBeamJointLoadDialog(eDocument document)
        {
            this.document = document;
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            eUtility.FillComboBox<eLengthUnits>(cbxLengthUnit, true);
            eUtility.FillComboBox<eForceUints>(cbxForceUnit, true);

            cbxForceUnit.SelectedItem = document.ForceUnit;
            cbxLengthUnit.SelectedItem = document.LengthUnit;

            ntxtMagnitude.LengthUnit = document.LengthUnit;
            ntxtMagnitude.ForceUnit = document.ForceUnit;

            eUtility.FillComboBox<eActionType>(cbxLoadType, true);
            cbxLoadType.SelectedItem = eActionType.Permanent;

            pbxMoment_Click(null, null);

            ntxtMagnitude.SU = eUtility.Convert(100, eLengthUnits.m, eUtility.SLU, eForceUints.KN, eUtility.SFU);
        }

        private void pbxConcentratedForce_Click(object sender, EventArgs e)
        {
            if (pbxConcentratedForce.BorderStyle == BorderStyle.FixedSingle)
            {
                pbxConcentratedForce.BorderStyle = BorderStyle.Fixed3D;
                pbxMoment.BorderStyle = BorderStyle.FixedSingle;
                this.isForce = true;
            }
            else
            {
                pbxConcentratedForce.BorderStyle = BorderStyle.FixedSingle;
                ntxtMagnitude.Measurment = eMeasurment.Force;
                this.isForce = false;
            }
        }

        private void pbxMoment_Click(object sender, EventArgs e)
        {
            if (pbxMoment.BorderStyle == BorderStyle.FixedSingle)
            {
                pbxMoment.BorderStyle = BorderStyle.Fixed3D;
                pbxConcentratedForce.BorderStyle = BorderStyle.FixedSingle;
                this.isForce = false;
                ntxtMagnitude.Measurment = eMeasurment.Moment;
            }
            else
            {
                pbxMoment.BorderStyle = BorderStyle.FixedSingle;
                this.isForce = true;
            }
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            this.magnitude = ntxtMagnitude.SU;

            this.actionType = (eActionType)Enum.Parse(typeof(eActionType), cbxLoadType.Text);

            if (radAddToExisting.Checked)
                this.assignOption = eAssignOptions.Add;
            else if (radRemoveAll.Checked)
                this.assignOption = eAssignOptions.Remove;
            else
                this.assignOption = eAssignOptions.Replace;

            this.factored = chkFactored.Checked;

            this.Close();
        }

        private void cbxLengthUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ntxtMagnitude.LengthUnit = (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cbxLengthUnit.Text);
        }

        private void cbxForceUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ntxtMagnitude.ForceUnit = (eForceUints)Enum.Parse(typeof(eForceUints), cbxForceUnit.Text);
        }
    }
}
