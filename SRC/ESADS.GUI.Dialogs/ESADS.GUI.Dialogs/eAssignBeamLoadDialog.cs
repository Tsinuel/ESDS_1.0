using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESADS.Code;
using ESADS.Mechanics.Analysis;
using ESADS.EGraphics;
using ESADS.EGraphics.Beam;
using ESADS.Mechanics.Analysis.Beam;

namespace ESADS.GUI
{
    public partial class eAssignBeamLoadDialog : Form
    {
        #region Fields
        private eDocument document;
        private eAssignOptions assignOption;
        private eMemberLoadTypes loadType;
        private bool factored;
        private double start;
        private double magnitude;
        private double end;
        private double magnitude_Left;
        private double magnitude_Right;
        private eTriangularLoadOrientation orientation;
        private eActionType actionType;
        private bool absoluteDistance;
        private eForceUints forceUnit;
        private eLengthUnits lengthUnit;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the assign option chosen by the user.
        /// </summary>
        public eAssignOptions AssignOption
        {
            get
            {
                return assignOption;
            }
        }

        /// <summary>
        /// Gets the type of laod from those supported by this version of ESADS.
        /// </summary>
        public eMemberLoadTypes LoadType
        {
            get
            {
                return loadType;
            }
        }

        /// <summary>
        /// Gets the magnitude at the left end of a trapezoidal load if it is the one chosen.
        /// </summary>
        public double Magnitude_Left
        {
            get
            {
                if (loadType == eMemberLoadTypes.TrapizoidallyDistributed)
                    return magnitude_Left;
                else
                    throw new Exception("Only trapezoidal load can have two magnitude");
            }
        }

        /// <summary>
        /// Gets the magnitude at the right end of a trapezoidal load if is the one chosen by the user.
        /// </summary>
        public double Magnitude_Right
        {
            get
            {
                if (loadType == eMemberLoadTypes.TrapizoidallyDistributed)
                    return magnitude_Right;
                else
                    throw new Exception("Only trapezoidal load can have two magnitude");
            }
        }

        /// <summary>
        /// Gets the magnitude of the load for all load types except trapezoidal.
        /// </summary>
        public double Magnitude
        {
            get
            {
                if (loadType != eMemberLoadTypes.TrapizoidallyDistributed)
                    return magnitude;
                else
                    throw new Exception("Trapezoidal laod has two magnitudes");
            }
        }

        /// <summary>
        /// Gets the distance from the left end of the member to the point of start of the load.
        /// </summary>
        public double Start
        {
            get
            {
                return start;
            }
        }

        /// <summary>
        /// Gets the distance from the right end to the end of the member.
        /// </summary>
        public double End
        {
            get
            {
                if (this.loadType != eMemberLoadTypes.ConcentratedForce && this.loadType != eMemberLoadTypes.ConcentratedMoment)
                    return this.end;
                else
                    throw new Exception("Concentration loads cannot have end load distance");
            }
        }

        /// <summary>
        /// Gets the orientation of the load for triangular laod.
        /// </summary>
        public eTriangularLoadOrientation Orientation
        {
            get
            {
                return this.orientation;
            }
        }

        /// <summary>
        /// Gets the value if the load is factored or not.
        /// </summary>
        public bool Factored
        {
            get
            {
                return factored;
            }
        }

        /// <summary>
        /// Gets the type of action given in EBCS.
        /// </summary>
        public eActionType ActionType
        {
            get
            {
                return actionType;
            }
        }

        /// <summary>
        /// Gets the value if the load distances are measured as absolute distances or not(i.e. relative to the member length).
        /// </summary>
        public bool AbsoluteDistance
        {
            get
            {
                return absoluteDistance;
            }
        }
        #endregion

        #region Constructor
        public eAssignBeamLoadDialog(eDocument document)
        {
            this.document = document;
            InitializeComponent();
            InitializeCustomComponents();
        }
        #endregion

        #region Methods
        private void InitializeCustomComponents()
        {
            eUtility.FillComboBox<eLengthUnits>(cbxLengthUnit, true);
            eUtility.FillComboBox<eForceUints>(cbxForceUnit, true);
            cbxForceUnit.SelectedItem = this.forceUnit = document.ForceUnit;
            cbxLengthUnit.SelectedItem = this.lengthUnit = document.LengthUnit;

            eUtility.FillComboBox<eActionType>(cbxLoadType, true);
            cbxLoadType.SelectedItem = eActionType.Permanent;

            pbxDistributed_Click(null, null);

            ntxtMag_1_Trap.DoubleValue = eUtility.Convert(100.0, eForceUints.KN, eUtility.SFU) / eUtility.Convert(1.0, eLengthUnits.m, eUtility.SLU);
            ntxtMag_2_Trap.DoubleValue = eUtility.Convert(40.0, eForceUints.KN, eUtility.SFU) / eUtility.Convert(1.0, eLengthUnits.m, eUtility.SLU);

            ntxtMag_Conc.DoubleValue = eUtility.Convert(40.0, eForceUints.KN, eUtility.SFU);
            ntxtMag_Dist.DoubleValue = eUtility.Convert(100.0, eForceUints.KN, eUtility.SFU) / eUtility.Convert(1.0, eLengthUnits.m, eUtility.SLU);

            RefreshPreview();
        }

        private bool Valid()
        {
            string note = "";
            bool valid = true;

            if (ntxtEnd_Dist.DoubleValue < 0 || ntxtEnd_Trap.DoubleValue < 0 || ntxtStart_Conc.DoubleValue < 0 || ntxtStart_Dist.DoubleValue < 0 || ntxtStart_Trap.DoubleValue < 0)
            {
                valid = false;
                note = "Distance values like start distance and end distance cannot be zero or negative.";
            }

            if (radRelativeDimension.Checked)
            {
                if (ntxtEnd_Dist.DoubleValue + ntxtStart_Dist.DoubleValue >= 1 && loadType == eMemberLoadTypes.UniformlyDistributed ||loadType == eMemberLoadTypes.TriangularlyDistributed ||
                    ntxtStart_Conc.DoubleValue > 1 && loadType == eMemberLoadTypes.ConcentratedForce || loadType == eMemberLoadTypes.ConcentratedMoment
                    || ntxtStart_Trap.DoubleValue + ntxtEnd_Trap.DoubleValue > 1 && loadType == eMemberLoadTypes.TrapizoidallyDistributed)
                {
                    valid = false;
                    note = "Relative distance cannot exceed 1.0 in sum. The length of distributed loads cannot be zero.";
                }
            }

            if (!valid)
                MessageBox.Show(note, "Data invalid!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return valid;
        }

        private void RefreshPreview()
        {
            string mod = "";
            if(radRelativeDimension.Checked)
                mod = " x L";

            switch (loadType)
            {
                case eMemberLoadTypes.ConcentratedForce:
                    lblPrevMagnitude_right.Visible = false;
                    lblPrevMiddleLength.Visible = false;

                    lblPrevMagnitude.Text = Math.Round(ntxtMag_Conc.DoubleValue, 3).ToString();
                    lblPrevStart.Text = Math.Round(ntxtStart_Conc.DoubleValue, 3).ToString() + mod;
                    if (radRelativeDimension.Checked)
                        lblPrevEnd.Text = Math.Round((1.0 - ntxtStart_Conc.DoubleValue),3).ToString() + mod;
                    else
                        lblPrevEnd.Text = "";
                    
                    if (ntxtMag_Conc.DoubleValue < 0)
                    {
                        picbxPreview.Image = global::ESADS.GUI.Properties.Resources.concF_negative;
                        lblPrevStart.Location = new Point(76, 113);
                        lblPrevEnd.Location = new Point(199, 112);
                        lblPrevMagnitude.Location = new Point(126, 99);
                    }
                    else
                    {
                        picbxPreview.Image = global::ESADS.GUI.Properties.Resources.concF_normal;
                        lblPrevStart.Location = new Point(76, 113);
                        lblPrevEnd.Location = new Point(199, 112);
                        lblPrevMagnitude.Location = new Point(161, 47);
                    }
                    break;
                case eMemberLoadTypes.ConcentratedMoment:
                    lblPrevMagnitude_right.Visible = false;
                    lblPrevMiddleLength.Visible = false;
                    
                    lblPrevMagnitude.Text = ntxtMag_Conc.Text+mod;
                    lblPrevStart.Text = ntxtStart_Conc.Text+mod;
                    if (radRelativeDimension.Checked)
                        lblPrevEnd.Text = (1.0 - ntxtStart_Conc.DoubleValue).ToString() + mod;
                    else
                        lblPrevEnd.Text = "";
                    
                    if (ntxtMag_Conc.DoubleValue < 0)
                    {
                        this.picbxPreview.Image = global::ESADS.GUI.Properties.Resources.concM_negative;

                        lblPrevStart.Location = new Point(76, 125);
                        lblPrevEnd.Location = new Point(206,126);
                        lblPrevMagnitude.Location = new Point(167,41);
                    }
                    else
                    {
                        picbxPreview.Image = global::ESADS.GUI.Properties.Resources.concM_normal;
                        lblPrevStart.Location = new Point(76, 113);
                        lblPrevEnd.Location = new Point(199, 112);
                        lblPrevMagnitude.Location = new Point(165,47);
                    }
                    break;
                case eMemberLoadTypes.TrapizoidallyDistributed:
                    lblPrevMagnitude_right.Visible = true;
                    lblPrevMiddleLength.Visible = true;

                    lblPrevStart.Text = ntxtStart_Trap.Text + mod;
                    lblPrevMagnitude.Text = ntxtMag_1_Trap.Text + mod;
                    lblPrevMagnitude_right.Text = ntxtMag_2_Trap.Text + mod;
                    lblPrevEnd.Text = ntxtEnd_Trap.Text + mod;

                    if (radAbsoluteDimension.Checked)
                        lblPrevMiddleLength.Text = "";
                    else
                        lblPrevMiddleLength.Text = (1 - ntxtStart_Trap.DoubleValue - ntxtEnd_Trap.DoubleValue).ToString() + mod;

                    if (ntxtMag_1_Trap.DoubleValue < 0 || ntxtMag_2_Trap.DoubleValue < 0)
                    {
                        picbxPreview.Image = global::ESADS.GUI.Properties.Resources.trap_negative;

                        lblPrevStart.Location = new Point(55, 133);
                        lblPrevMiddleLength.Location = new Point(141, 133);
                        lblPrevEnd.Location = new Point(218, 133);
                        lblPrevMagnitude.Location = new Point(72, 114);
                        lblPrevMagnitude_right.Location = new Point(205, 92);
                    }
                    else
                    {
                        picbxPreview.Image = global::ESADS.GUI.Properties.Resources.trap_normal;

                        lblPrevStart.Location = new Point(53, 115);
                        lblPrevMiddleLength.Location = new Point(141, 115);
                        lblPrevEnd.Location = new Point(220, 115);
                        lblPrevMagnitude.Location = new Point(72, 32);
                        lblPrevMagnitude_right.Location = new Point(205, 57);
                    }
                    break;
                case eMemberLoadTypes.TriangularlyDistributed:
                    lblPrevMagnitude_right.Visible = false;
                    lblPrevMiddleLength.Visible = true;

                    lblPrevStart.Text = ntxtStart_Dist.Text + mod;
                    lblPrevEnd.Text = ntxtEnd_Dist.Text + mod;
                    lblPrevMagnitude.Text = ntxtMag_Dist.Text;
                    if (radRelativeDimension.Checked)
                        lblPrevMiddleLength.Text = (1 - ntxtStart_Dist.DoubleValue - ntxtEnd_Dist.DoubleValue).ToString() + mod;
                    else
                        lblPrevMiddleLength.Text = "";

                    if (ntxtMag_Dist.DoubleValue < 0)
                    {
                        if (radLeftToRight.Checked)
                        {
                            picbxPreview.Image = global::ESADS.GUI.Properties.Resources.trian_negative_r_to_l;

                            lblPrevStart.Location = new Point(54, 128);
                            lblPrevMiddleLength.Location = new Point(143, 129);
                            lblPrevEnd.Location = new Point(223, 128);
                            lblPrevMagnitude.Location = new Point(205, 109);
                        }
                        else
                        {
                            picbxPreview.Image = global::ESADS.GUI.Properties.Resources.trian_negative;

                            lblPrevStart.Location = new Point(57, 125);
                            lblPrevMiddleLength.Location = new Point(143, 124);
                            lblPrevEnd.Location = new Point(219, 124);
                            lblPrevMagnitude.Location = new Point(78, 106);                            
                        }
                    }
                    else
                    {
                        if (radLeftToRight.Checked)
                        {
                            picbxPreview.Image = global::ESADS.GUI.Properties.Resources.trian_normal_r_to_l;

                            lblPrevStart.Location = new Point(50, 120);
                            lblPrevMiddleLength.Location = new Point(143, 120);
                            lblPrevEnd.Location = new Point(229, 120);
                            lblPrevMagnitude.Location = new Point(215, 28);
                        }
                        else
                        {
                            picbxPreview.Image = global::ESADS.GUI.Properties.Resources.trian_normal;

                            lblPrevStart.Location = new Point(54, 114);
                            lblPrevMiddleLength.Location = new Point(143, 114);
                            lblPrevEnd.Location = new Point(230, 114);
                            lblPrevMagnitude.Location = new Point(78, 32);                            
                        }
                    }
                    break;
                case eMemberLoadTypes.UniformlyDistributed:
                    lblPrevMagnitude_right.Visible = false;
                    lblPrevMiddleLength.Visible = true;
                    
                    lblPrevStart.Text = ntxtStart_Dist.Text + mod;
                    lblPrevEnd.Text = ntxtEnd_Dist.Text + mod;
                    lblPrevMagnitude.Text = ntxtMag_Dist.Text;
                    if (radRelativeDimension.Checked)
                        lblPrevMiddleLength.Text = (1 - ntxtStart_Dist.DoubleValue - ntxtEnd_Dist.DoubleValue).ToString() + mod;
                    else
                        lblPrevMiddleLength.Text = "";

                    if (ntxtMag_Dist.DoubleValue < 0)
                    {
                        picbxPreview.Image = global::ESADS.GUI.Properties.Resources.dist_negative;

                        lblPrevStart.Location = new Point(45, 118);
                        lblPrevMiddleLength.Location = new Point(140, 118);
                        lblPrevEnd.Location = new Point(228, 118);
                        lblPrevMagnitude.Location = new Point(140, 102);
                    }
                    else
                    {
                        picbxPreview.Image = global::ESADS.GUI.Properties.Resources.dist_normal;

                        lblPrevStart.Location = new Point(45, 115);
                        lblPrevMiddleLength.Location = new Point(140, 115);
                        lblPrevEnd.Location = new Point(228, 115);
                        lblPrevMagnitude.Location = new Point(140, 35);
                    }
                    break;
            }
        }
        #endregion

        #region Event Handlers
        private void pbxConcentratedForce_Click(object sender, EventArgs e)
        {
            if (pbxConcentratedForce.BorderStyle == BorderStyle.FixedSingle)
            {
                loadType = eMemberLoadTypes.ConcentratedForce;
                pbxConcentratedForce.BorderStyle = BorderStyle.Fixed3D;
                pbxMoment.BorderStyle = BorderStyle.FixedSingle;
                pbxDistributed.BorderStyle = BorderStyle.FixedSingle;
                pbxTriagular.BorderStyle = BorderStyle.FixedSingle;
                pbxTrapizoidal.BorderStyle = BorderStyle.FixedSingle;
                gbxConcentrated.Visible = true;
                gbxTrapizoidal.Visible = false;
                gbxDistributedandTraigular.Visible = false;
                gbxOption.Visible = false;
            }
            else
            {
                pbxConcentratedForce.BorderStyle = BorderStyle.FixedSingle;
                gbxConcentrated.Visible = false;
            }
            RefreshPreview();
        }

        private void pbxMoment_Click(object sender, EventArgs e)
        {
            if (pbxMoment.BorderStyle == BorderStyle.FixedSingle)
            {
                this.loadType = eMemberLoadTypes.ConcentratedMoment;
                pbxMoment.BorderStyle = BorderStyle.Fixed3D;
                pbxConcentratedForce.BorderStyle = BorderStyle.FixedSingle;
                pbxDistributed.BorderStyle = BorderStyle.FixedSingle;
                pbxTriagular.BorderStyle = BorderStyle.FixedSingle;
                pbxTrapizoidal.BorderStyle = BorderStyle.FixedSingle;
                gbxConcentrated.Visible = true;
                gbxTrapizoidal.Visible = false;
                gbxDistributedandTraigular.Visible = false;
                gbxOption.Visible = false;
            }
            else
            {
                pbxMoment.BorderStyle = BorderStyle.FixedSingle;
                gbxConcentrated.Visible = false;
            }
            RefreshPreview();
        }

        private void pbxDistributed_Click(object sender, EventArgs e)
        {
            if (pbxDistributed.BorderStyle == BorderStyle.FixedSingle)
            {
                this.loadType = eMemberLoadTypes.UniformlyDistributed;
                pbxDistributed.BorderStyle = BorderStyle.Fixed3D;
                pbxMoment.BorderStyle = BorderStyle.FixedSingle;
                pbxConcentratedForce.BorderStyle = BorderStyle.FixedSingle;
                pbxTriagular.BorderStyle = BorderStyle.FixedSingle;
                pbxTrapizoidal.BorderStyle = BorderStyle.FixedSingle;
                gbxConcentrated.Visible = false;
                gbxTrapizoidal.Visible = false;
                gbxDistributedandTraigular.Visible = true;
                gbxOption.Visible = false;
            }
            else
            {
                pbxDistributed.BorderStyle = BorderStyle.FixedSingle;
                gbxDistributedandTraigular.Visible = false;
            }
            RefreshPreview();
        }

        private void pbxTraigular_Click(object sender, EventArgs e)
        {
            if (pbxTriagular.BorderStyle == BorderStyle.FixedSingle)
            {
                this.loadType = eMemberLoadTypes.TriangularlyDistributed;
                pbxTriagular.BorderStyle = BorderStyle.Fixed3D;
                pbxMoment.BorderStyle = BorderStyle.FixedSingle;
                pbxDistributed.BorderStyle = BorderStyle.FixedSingle;
                pbxConcentratedForce.BorderStyle = BorderStyle.FixedSingle;
                pbxTrapizoidal.BorderStyle = BorderStyle.FixedSingle;
                gbxConcentrated.Visible = false;
                gbxTrapizoidal.Visible = false;
                gbxDistributedandTraigular.Visible = true;
                gbxOption.Visible = true;
            }
            else
            {
                pbxTriagular.BorderStyle = BorderStyle.FixedSingle;
                gbxDistributedandTraigular.Visible = false;
                gbxOption.Enabled = false;
            }
            RefreshPreview();
        }

        private void pbxTrapizoidal_Click(object sender, EventArgs e)
        {
            if (pbxTrapizoidal.BorderStyle == BorderStyle.FixedSingle)
            {
                this.loadType = eMemberLoadTypes.TrapizoidallyDistributed;
                pbxTrapizoidal.BorderStyle = BorderStyle.Fixed3D;
                pbxMoment.BorderStyle = BorderStyle.FixedSingle;
                pbxDistributed.BorderStyle = BorderStyle.FixedSingle;
                pbxTriagular.BorderStyle = BorderStyle.FixedSingle;
                pbxConcentratedForce.BorderStyle = BorderStyle.FixedSingle;
                gbxConcentrated.Visible = false;
                gbxTrapizoidal.Visible = true;
                gbxDistributedandTraigular.Visible = false;
                gbxOption.Visible = false;
            }
            else
            {
                pbxTrapizoidal.BorderStyle = BorderStyle.FixedSingle;
                gbxTrapizoidal.Visible = false;
            }
            RefreshPreview();
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (!Valid())
                return;

            this.actionType = (eActionType)Enum.Parse(typeof(eActionType), cbxLoadType.Text);

            this.factored = chkFactored.Checked;
            this.absoluteDistance = radAbsoluteDimension.Checked;

            if (radAddToExisting.Checked)
                this.assignOption = eAssignOptions.Add;
            else if (radReplaceExisting.Checked)
                this.assignOption = eAssignOptions.Replace;
            else if (radRemoveExisting.Checked)
                this.assignOption = eAssignOptions.Remove;

            switch (this.loadType)
            {
                case eMemberLoadTypes.ConcentratedForce:
                    this.start = absoluteDistance ? eUtility.Convert(ntxtStart_Conc.DoubleValue, lengthUnit, eUtility.SLU) : ntxtStart_Conc.DoubleValue;
                    this.magnitude = eUtility.Convert(ntxtMag_Conc.DoubleValue, forceUnit, eUtility.SFU);
                    break;
                case eMemberLoadTypes.ConcentratedMoment:
                    this.start = absoluteDistance ? eUtility.Convert(ntxtStart_Conc.DoubleValue, lengthUnit, eUtility.SLU) : ntxtStart_Conc.DoubleValue;
                    this.magnitude = eUtility.Convert(ntxtMag_Conc.DoubleValue, lengthUnit, eUtility.SLU, forceUnit, eUtility.SFU);
                    break;
                case eMemberLoadTypes.TrapizoidallyDistributed:
                    this.start = absoluteDistance ? eUtility.Convert(ntxtStart_Trap.DoubleValue, lengthUnit, eUtility.SLU) : ntxtStart_Trap.DoubleValue;
                    this.end = absoluteDistance ? eUtility.Convert(ntxtEnd_Trap.DoubleValue, lengthUnit, eUtility.SLU) : ntxtEnd_Trap.DoubleValue;
                    this.magnitude_Left = eUtility.Convert(ntxtMag_1_Trap.DoubleValue, forceUnit, eUtility.SFU) / eUtility.Convert(1.0, lengthUnit, eUtility.SLU);
                    this.magnitude_Right = eUtility.Convert(ntxtMag_2_Trap.DoubleValue, forceUnit, eUtility.SFU) / eUtility.Convert(1.0, lengthUnit, eUtility.SLU);
                    break;
                case eMemberLoadTypes.TriangularlyDistributed:
                    this.start = absoluteDistance ? eUtility.Convert(ntxtStart_Dist.DoubleValue, lengthUnit, eUtility.SLU) : ntxtStart_Dist.DoubleValue;
                    this.end = absoluteDistance ? eUtility.Convert(ntxtEnd_Dist.DoubleValue, lengthUnit, eUtility.SLU) : ntxtEnd_Dist.DoubleValue;
                    this.magnitude = eUtility.Convert(ntxtMag_Dist.DoubleValue, forceUnit, eUtility.SFU) / eUtility.Convert(1.0, lengthUnit, eUtility.SLU);
                    if (radLeftToRight.Checked)
                        this.orientation = eTriangularLoadOrientation.LeftToRight;
                    else
                        this.orientation = eTriangularLoadOrientation.RightToLeft;
                    break;
                case eMemberLoadTypes.UniformlyDistributed:
                    this.start = absoluteDistance ? eUtility.Convert(ntxtStart_Dist.DoubleValue, lengthUnit, eUtility.SLU) : ntxtStart_Dist.DoubleValue;
                    this.end = absoluteDistance ? eUtility.Convert(ntxtEnd_Dist.DoubleValue, lengthUnit, eUtility.SLU) : ntxtEnd_Dist.DoubleValue;
                    this.magnitude = eUtility.Convert(ntxtMag_Dist.DoubleValue, forceUnit, eUtility.SFU) / eUtility.Convert(1.0, lengthUnit, eUtility.SLU);
                    break;
            }

            
        }

        private void cbxLengthUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            eLengthUnits lu_new = (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cbxLengthUnit.Text);

            ntxtEnd_Dist.DoubleValue = eUtility.Convert(ntxtEnd_Dist.DoubleValue, lengthUnit, lu_new);
            ntxtEnd_Trap.DoubleValue = eUtility.Convert(ntxtEnd_Trap.DoubleValue, lengthUnit, lu_new);
            ntxtStart_Conc.DoubleValue = eUtility.Convert(ntxtStart_Conc.DoubleValue, lengthUnit, lu_new);
            ntxtStart_Dist.DoubleValue = eUtility.Convert(ntxtStart_Dist.DoubleValue, lengthUnit, lu_new);
            ntxtStart_Trap.DoubleValue = eUtility.Convert(ntxtStart_Trap.DoubleValue, lengthUnit, lu_new);

            ntxtMag_1_Trap.DoubleValue = ntxtMag_1_Trap.DoubleValue / eUtility.Convert(1.0, lengthUnit, lu_new);
            ntxtMag_2_Trap.DoubleValue = ntxtMag_2_Trap.DoubleValue / eUtility.Convert(1.0, lengthUnit, lu_new);
            ntxtMag_Dist.DoubleValue = ntxtMag_Dist.DoubleValue / eUtility.Convert(1.0, lengthUnit, lu_new);
            if (loadType == eMemberLoadTypes.ConcentratedMoment)
                ntxtMag_Conc.DoubleValue = eUtility.Convert(ntxtMag_Conc.DoubleValue, lengthUnit, lu_new);

            this.lengthUnit = lu_new;
            RefreshPreview();
        }

        private void cbxForceUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            eForceUints fu_new = (eForceUints)Enum.Parse(typeof(eForceUints), cbxForceUnit.Text);

            ntxtMag_1_Trap.DoubleValue = eUtility.Convert(ntxtMag_1_Trap.DoubleValue, forceUnit, fu_new);
            ntxtMag_2_Trap.DoubleValue = eUtility.Convert(ntxtMag_2_Trap.DoubleValue, forceUnit, fu_new);
            ntxtMag_Dist.DoubleValue = eUtility.Convert(ntxtMag_Dist.DoubleValue, forceUnit, fu_new);
            ntxtMag_Conc.DoubleValue = eUtility.Convert(ntxtMag_Conc.DoubleValue, forceUnit, fu_new);

            this.forceUnit = fu_new;
            RefreshPreview();
        }

        private void radRemoveExisting_CheckedChanged(object sender, EventArgs e)
        {
            grpbxLoadType.Enabled = !radRemoveExisting.Checked;
            grpLoadDistanceMeasurement.Enabled = !radRemoveExisting.Checked;
            gbxConcentrated.Enabled = !radRemoveExisting.Checked;
            gbxDistributedandTraigular.Enabled = !radRemoveExisting.Checked;
            gbxOption.Enabled = !radRemoveExisting.Checked;
            gbxTrapizoidal.Enabled = !radRemoveExisting.Checked;
            grpbxPreview.Visible = !radRemoveExisting.Checked;
        }

        private void radRelativeDimension_CheckedChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void ntxtMag_Conc_TextChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void ntxtStart_Conc_TextChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void radLeftToRight_CheckedChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void ntxtMag_1_Trap_TextChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void ntxtMag_2_Trap_TextChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void ntxtStart_Trap_TextChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void ntxtEnd_Trap_TextChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void ntxtMag_Dist_TextChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void ntxtStart_Dist_TextChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void ntxtEnd_Dist_TextChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }
        #endregion
    }
}
