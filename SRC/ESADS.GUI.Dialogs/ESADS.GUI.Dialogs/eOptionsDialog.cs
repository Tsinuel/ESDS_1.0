using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESADS;
using ESADS.Code;
using ESADS.Code.EBCS_1995;
using ESADS.EGraphics.Beam;
using ESADS.Mechanics.Analysis;
using ESADS.Mechanics.Analysis.Beam;
using ESADS.Mechanics.Design;
using ESADS.Mechanics.Design.Beam;

namespace ESADS.GUI
{
    public partial class eOptionsDialog : Form
    {
        private eDocument document;
        private eForceUints forceUnit;
        private eLengthUnits lengthUnit;
        private bool valid;
        public eOptionsDialog(eDocument document)
        {
            this.document = document;
            InitializeComponent();
            InitializeCustomComponents();

            btnApply.Enabled = false;
        }

        private void InitializeCustomComponents()
        {
            foreach (var conc in document.Concretes)
                cmbxConcrete.Items.Add(conc.Name);
            foreach (var stl in document.Steels)
                cmbxSteel.Items.Add(stl.Name);

            eUtility.FillComboBox<eLengthUnits>(cmbxLengthUnit, true);
            eUtility.FillComboBox<eForceUints>(cmbxForceUnit, true);

            cmbxForceUnit.SelectedItem = document.ForceUnit;
            cmbxLengthUnit.SelectedItem = document.LengthUnit;
            eUtility.FillComboBox<eExposureType>(cmbxBeamExposureType);
            eUtility.FillComboBox<eExposureType>(cmbxSlabExposureType);
            eUtility.FillComboBox<eExposureType>(cmbxFootingExposureType);

            eUtility.FillComboBox<eClassWork>(cmbxClassWork);
            btnDwgBackColor.BackColor = document.ModelForm.BackColor;
            ntxtMaxAggregateSize.SU = 20;
            switch (document.ModelType)
            {
                case eStructureType.Beam:
                    InitializeBeamComponents();
                    break;
                case eStructureType.Column:
                    InitializeColumnComponents();
                    break;
                case eStructureType.Footing:
                    InitializeFootingComponents();
                    break;
                case eStructureType.Slab:
                    InitializeSlabComponents();
                    break;
            }
        }

        private void InitializeBeamComponents()
        {
            cmbxConcrete.Text = document.Beam.Beam_Design.Concrete.Name;
            cmbxSteel.Text = document.Beam.Beam_Design.Steel.Name;
           
            ntxtBeamReinfCover.DoubleValue = 0.0;
            cmbxBeamExposureType.SelectedItem = document.Beam.Beam_Design.ExposureType;

            cmbxClassWork.SelectedItem = document.Beam.Beam_Design.ClassWork;
            //
            //Fill values for default joint types
            //
            eUtility.FillComboBox<eJointType>(cmbxDefaultSupport, true);
            chkBeamConsiderSelfWeight.Checked = document.Beam.Beam_Analysis.ConsiderSelfWeight;
            cmbxDefaultSupport.SelectedItem = document.Beam.DefaultJoint;
            cmbxBeamDiagramStyle.Items.AddRange(Enum.GetNames(typeof(eDiagramStyle)));
            cmbxBeamDiagramStyle.SelectedItem = document.Beam.DiagramStyle.ToString();
            //
            //Fill beam bar choices
            //
            eUtility.FillComboBox<eReinforcement>(cmbxBeamBar1, 1, true);
            eUtility.FillComboBox<eReinforcement>(cmbxBeamBar2, 1, true);
            eUtility.FillComboBox<eReinforcement>(cmbxBeamStirrupBar, 0, 6, true);
            eUtility.FillComboBox<eReinforcement>(cmbxFootingBar, 1, true);
            eUtility.FillComboBox<eRelativeStirrupPosition>(cmbxBeamStirrupPosition, true);

            cmbxBeamBar1.SelectedItem = document.Beam.Beam_Design.MainBar1;
            chkbxUseBarChoice2.Checked = document.Beam.Beam_Design.UseTwoBars;
            if (document.Beam.Beam_Design.UseTwoBars)
                cmbxBeamBar2.SelectedItem = document.Beam.Beam_Design.MainBar2;

            chkUseCornerBars.Checked = document.Beam.Beam_Design.UseCornerBars;

            cmbxBeamStirrupBar.SelectedItem = document.Beam.Beam_Design.StirupBar;
            cmbxBeamStirrupPosition.SelectedItem = document.Beam.Beam_Design.StirrupPosn;
            //
            //Text boxes and other values
            //
            ntxtMaxAggregateSize.DoubleValue = eUtility.Convert(document.Beam.Beam_Design.MaxAggSize, eUtility.SLU, document.LengthUnit);
            ntxtPermanentLoadFactor.DoubleValue = document.Beam.Beam_Analysis.LoadCombination.PermanentLoadFactor;
            ntxtVariableLoadFactor.DoubleValue = document.Beam.Beam_Analysis.LoadCombination.VariableLoadFactor;
            ntxtStirrupHookLength.DoubleValue = eUtility.Convert(document.Beam.Beam_Design.StirrupHookLength, eUtility.SLU, lengthUnit);
            ntxtBeamReinfCover.DoubleValue = eUtility.Convert(document.Beam.Beam_Design.Cover, eUtility.SLU, lengthUnit);
        }

        private void InitializeSlabComponents()
        {
            //
            //Fill Available bars for slab
            //
            eReinforcement[] bars = (eReinforcement[])Enum.GetValues(typeof(eReinforcement));

            foreach (eReinforcement r in bars)
            {
                lstbxAvailableBars.Items.Add(r);
            }

            lstbxAvailableBars.Items.Remove(eReinforcement.Φ10);

            lstbxChosenBars.Items.Add(eReinforcement.Φ10);

        }

        private void InitializeFootingComponents()
        {
            ntxtDowlesbentLength.SU = 500;
            ntxtHookLength.SU = eSpecialStructures.GetMinFootingDepth(eFootingType.FootingOnSoil) - 2 * document.Footing.Footing.Cover;
            ntxtBarLengthIncrement.SU = document.Footing.Footing.LengthIncrement;
        }

        private void InitializeColumnComponents()
        {
            //throw new NotImplementedException();
        }

        private void cmbxExposureType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnUseEBCSloadFactors_Click(object sender, EventArgs e)
        {
            double p = ntxtPermanentLoadFactor.DoubleValue;
            double v = ntxtVariableLoadFactor.DoubleValue;

            ntxtPermanentLoadFactor.DoubleValue = eBasisOfDesign.GetActionPartialSafetyFactor(eActionType.Permanent);
            ntxtVariableLoadFactor.DoubleValue = eBasisOfDesign.GetActionPartialSafetyFactor(eActionType.Variable);

            if (p != ntxtPermanentLoadFactor.DoubleValue || v != ntxtVariableLoadFactor.DoubleValue)
                btnApply.Enabled = true;
        }

        private void btnDwgBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog col = new ColorDialog();

            if (col.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btnDwgBackColor.BackColor = col.Color;

                lblDwgBackColor.Text = col.Color.Name;
                btnApply.Enabled = true;
            }
        }

        private void btnGridColor_Click(object sender, EventArgs e)
        {
            ColorDialog col = new ColorDialog();

            if (col.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btnGridColor.BackColor = col.Color;

                lblGridColor.Text = col.Color.Name;
            }
        }

        private void chkbxShowGrid_CheckedChanged(object sender, EventArgs e)
        {
            chkbxMultipleFineness.Enabled = chkbxShowGrid.Checked;
            chkbxSnapToGrid.Enabled = chkbxShowGrid.Checked;
            btnGridColor.Enabled = chkbxShowGrid.Checked;
            ntxtGridSize.Enabled = chkbxShowGrid.Checked;
            nudGridInterval.Enabled = chkbxMultipleFineness.Checked && chkbxShowGrid.Checked;
        }

        private void chkbxShowDim_CheckedChanged(object sender, EventArgs e)
        {
            chkbxShowLoadDim.Enabled = chkbxShowDim.Checked;
        }

        private void cmbxBeamExposureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eDetailing.GetMinConcreteCover((eExposureType)Enum.Parse(typeof(eExposureType), cmbxBeamExposureType.Text)) > ntxtBeamReinfCover.DoubleValue)
                ntxtBeamReinfCover.DoubleValue = eDetailing.GetMinConcreteCover((eExposureType)Enum.Parse(typeof(eExposureType), cmbxBeamExposureType.Text));
            btnApply.Enabled = true;
        }

        private void cmbxSlabExposureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eDetailing.GetMinConcreteCover((eExposureType)Enum.Parse(typeof(eExposureType), cmbxSlabExposureType.Text)) >
                ntxtSlabReinfCover.DoubleValue)
                ntxtSlabReinfCover.DoubleValue = eDetailing.GetMinConcreteCover((eExposureType)Enum.Parse(typeof(eExposureType), cmbxSlabExposureType.Text));
        }

        private void cmbxFootingExposureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eDetailing.GetMinConcreteCover((eExposureType)Enum.Parse(typeof(eExposureType), cmbxFootingExposureType.Text)) >
                ntxtFootingReinfCover.DoubleValue)
                ntxtFootingReinfCover.DoubleValue = eDetailing.GetMinConcreteCover((eExposureType)Enum.Parse(typeof(eExposureType), cmbxFootingExposureType.Text));
        }

        private void btnLayers_Click(object sender, EventArgs e)
        {
           
            eLayersDialog layers = new eLayersDialog(document.Model.Layers);

            layers.ShowDialog();
        }

        private void picbxNormalPin_Click(object sender, EventArgs e)
        {
            radNormalPin.Checked = true;
        }

        private void picbxFixed_Click(object sender, EventArgs e)
        {
            radFixed.Checked = true;
        }

        private void lstbxChosenBars_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstbxChosenBars.SelectedItems.Count == 0)
            {
                btnSlabBarMoveDown.Enabled = false;
                btnSlabBarMoveUp.Enabled = false;
            }
            else
            {
                btnSlabBarMoveDown.Enabled = lstbxChosenBars.SelectedItems.Count == 1;
                btnSlabBarMoveUp.Enabled = lstbxChosenBars.SelectedItems.Count == 1;
                btnSlabRemoveBarChoices.Enabled = true;
            }
        }

        private void lstbxAvailableBars_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstbxAvailableBars.SelectedItems.Count == 0)
                btnSlabAddBarChoices.Enabled = false;
            else
                btnSlabAddBarChoices.Enabled = true;
        }

        private void btnSlabAddBarChoices_Click(object sender, EventArgs e)
        {
            while (lstbxAvailableBars.SelectedItems.Count > 0)
            {
                lstbxChosenBars.Items.Add(lstbxAvailableBars.SelectedItems[0]);
                lstbxAvailableBars.Items.Remove(lstbxAvailableBars.SelectedItems[0]);
            }
        }

        private void btnSlabRemoveBarChoices_Click(object sender, EventArgs e)
        {
            while (lstbxChosenBars.SelectedItems.Count > 0)
            {
                lstbxAvailableBars.Items.Add(lstbxChosenBars.SelectedItems[0]);
                lstbxChosenBars.Items.Remove(lstbxChosenBars.SelectedItems[0]);
            }
        }

        private void btnSlabBarMoveUp_Click(object sender, EventArgs e)
        {
            if (lstbxChosenBars.Items.Count <= 1)
                return;

            if (lstbxChosenBars.SelectedIndex == 0)
                return;

            int indx = lstbxChosenBars.SelectedIndex;

            object obj = lstbxChosenBars.SelectedItem;
            lstbxChosenBars.Items[indx] = lstbxChosenBars.Items[indx - 1];
            lstbxChosenBars.Items[indx - 1] = obj;
            
            lstbxChosenBars.ClearSelected();
            lstbxChosenBars.SelectedItem = lstbxChosenBars.Items[indx - 1];
        }

        private void btnSlabBarMoveDown_Click(object sender, EventArgs e)
        {
            if (lstbxChosenBars.Items.Count <= 1)
                return;

            if (lstbxChosenBars.SelectedIndex == lstbxChosenBars.Items.Count - 1)
                return;

            int indx = lstbxChosenBars.SelectedIndex;

            object obj = lstbxChosenBars.SelectedItem;
            lstbxChosenBars.Items[indx] = lstbxChosenBars.Items[indx + 1];
            lstbxChosenBars.Items[indx + 1] = obj;

            lstbxChosenBars.ClearSelected();
            lstbxChosenBars.SelectedItem = lstbxChosenBars.Items[indx + 1];
        }

        private void chkbxMultipleFineness_CheckedChanged(object sender, EventArgs e)
        {
            nudGridInterval.Enabled = chkbxMultipleFineness.Checked;
        }

        private void btnBeamCurtailmentRules_Click(object sender, EventArgs e)
        {
            eCurtailmentRules cr = new eCurtailmentRules(document);

            cr.ShowDialog();
        }
        private void radNormalPin_CheckedChanged(object sender, EventArgs e)
        {
            if (radNormalPin.Checked)
                picbxNormalPin.BorderStyle = BorderStyle.Fixed3D;
            else
                picbxNormalPin.BorderStyle = BorderStyle.FixedSingle;
        }

        private void radFixed_CheckedChanged(object sender, EventArgs e)
        {
            if (radFixed.Checked)
                picbxFixed.BorderStyle = BorderStyle.Fixed3D;
            else
                picbxFixed.BorderStyle = BorderStyle.FixedSingle;
        }

        private void chkSlab_UseCodeProvisions_CheckedChanged(object sender, EventArgs e)
        {
            ntxtSlabMaxBarSpacing.Enabled = !chkSlab_UseCodeProvisions.Checked;
            ntxtSlabMinBarSpacing.Enabled = !chkSlab_UseCodeProvisions.Checked;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!Valid())
                return;
            document.LengthUnit = lengthUnit;
            document.ForceUnit = forceUnit;

            document.ModelForm.BackColor = btnDwgBackColor.BackColor;
            
            switch (document.ModelType)
            {
                case eStructureType.Beam:
                    document.Beam.Beam_Design.ClassWork = (eClassWork)Enum.Parse(typeof(eClassWork), cmbxClassWork.Text);
                    document.Beam.Beam_Design.MaxAggSize = eUtility.Convert(ntxtMaxAggregateSize.DoubleValue, lengthUnit, eUtility.SLU);
                    document.Beam.Beam_Analysis.LoadCombination.PermanentLoadFactor = ntxtPermanentLoadFactor.DoubleValue;
                    document.Beam.Beam_Analysis.LoadCombination.VariableLoadFactor = ntxtVariableLoadFactor.DoubleValue;
                    document.Beam.DefaultJoint = (eJointType)Enum.Parse(typeof(eJointType), cmbxDefaultSupport.Text);

                    document.Beam.Beam_Design.UseTwoBars = chkbxUseBarChoice2.Checked;
                    document.Beam.Beam_Design.MainBar1 = (eReinforcement)Enum.Parse(typeof(eReinforcement), cmbxBeamBar1.Text);
                    if (chkbxUseBarChoice2.Checked)
                        document.Beam.Beam_Design.MainBar2 = (eReinforcement)Enum.Parse(typeof(eReinforcement), cmbxBeamBar2.Text);

                    document.Beam.Beam_Design.StirupBar = (eReinforcement)Enum.Parse(typeof(eReinforcement), cmbxBeamStirrupBar.Text);
                    document.Beam.Beam_Design.StirrupPosn = (eRelativeStirrupPosition)Enum.Parse(typeof(eRelativeStirrupPosition), cmbxBeamStirrupPosition.Text);
                    document.Beam.Beam_Design.ExposureType = (eExposureType)Enum.Parse(typeof(eExposureType), cmbxBeamExposureType.Text);
                    document.Beam.Beam_Design.Cover = eUtility.Convert(ntxtBeamReinfCover.DoubleValue, lengthUnit, eUtility.SLU);

                    document.Beam.Beam_Analysis.ConsiderSelfWeight = chkBeamConsiderSelfWeight.Checked;
                    document.Beam.DiagramStyle = (eDiagramStyle)Enum.Parse(typeof(eDiagramStyle), cmbxBeamDiagramStyle.Text);
                    document.Beam.Beam_Design.UseCornerBars = chkUseCornerBars.Checked;
                    document.Beam.Beam_Design.StirrupHookLength = eUtility.Convert(ntxtStirrupHookLength, lengthUnit, eUtility.SLU);
                    document.Beam.Beam_Design.Concrete = ConcFromName(cmbxConcrete.Text);
                    document.Beam.Beam_Design.Steel = SteelFromName(cmbxSteel.Text);
                    break;
                case eStructureType.Column:
                    break;
                case eStructureType.Footing:
                    document.Footing.BowelsBentLength = ntxtDowlesbentLength.SU;
                    document.Footing.Footing.LengthIncrement = ntxtBarLengthIncrement.SU;
                    document.Footing.Footing.UseMaxHookLength = chTakeMaxValue.Checked;
                    if (radNormalPin.Checked)
                        document.Footing.FootingColumnConnection = EGraphics.Footing.eFootingColumnConnection.Pin;
                    else
                        document.Footing.FootingColumnConnection = EGraphics.Footing.eFootingColumnConnection.Fixed;
                    if (chTakeMaxValue.Checked)
                        document.Footing.Footing.HookLength = document.Footing.Footing.Depth - 2 * document.Footing.Footing.Cover;
                    else
                        document.Footing.Footing.HookLength = ntxtHookLength.SU;
                    break;
                case eStructureType.Slab:
                    break;
            }

            btnApply.Enabled = false;
        }

        private eSteel SteelFromName(string name)
        {
            foreach (var stl in document.Steels)
                if (stl.Name == name)
                    return stl;
            throw new Exception("The Steel is not defined in the document");
        }

        private eConcrete ConcFromName(string name)
        {
            foreach (var conc in document.Concretes)
                if (conc.Name == name)
                    return conc;
            throw new Exception("The Concrete is not defined in the document");
        }

        private bool Valid()
        {
            valid = true;
            string note = "";

            if (ntxtMaxAggregateSize.DoubleValue <= 0.0)
            {
                valid = false;
                note = "The value of maximum aggregate size cannot be zero or negative.";
                goto A;
            }

            switch (document.ModelType)
            {
                case eStructureType.Beam:
                    if (ntxtStirrupHookLength.DoubleValue <= 0.0)
                    {
                        valid = false;
                        note = "The value of stirrup hook length cannot be zero or negative.";
                        goto A;
                    }
                    if (eUtility.Convert(ntxtStirrupHookLength.DoubleValue, lengthUnit, eUtility.SLU) < 5.0 * eXBar.GetDiam((eReinforcement)Enum.Parse(typeof(eReinforcement),
                        cmbxBeamStirrupBar.Text)))
                    {
                        valid = false;
                        note = "The minimum value of stirrup hook length is 5 times the diameter of the stirrup";
                        goto A;
                    }
                    if (ntxtPermanentLoadFactor.DoubleValue < 0 || ntxtVariableLoadFactor.DoubleValue < 0)
                    {
                        valid = false;
                        note = "The load factors cannot have a negative value.";
                        goto A;
                    }
                    if (ntxtBeamReinfCover.DoubleValue <= 0)
                    {
                        valid = false;
                        note = "The reinforcement cover of a beam cannot be zero or negative.";
                        goto A;
                    }
                    if (eUtility.Convert(ntxtBeamReinfCover.DoubleValue, lengthUnit, eUtility.SLU) < eDetailing.GetMinConcreteCover(
                         (eExposureType)Enum.Parse(typeof(eExposureType), cmbxBeamExposureType.Text)))
                    {
                        valid = false;
                        note = "The reinforcement cover provided is less than the minimum provision of EBCS-1995 for the selected exposure type.";
                        goto A;
                    }
                    if (chkbxUseBarChoice2.Checked && cmbxBeamBar2.Text == "")
                    {
                        valid = false;
                        note = "If the check box to use second bar is checked, a second bar must be chosen for beam.";
                        goto A;
                    }
                    break;
                case eStructureType.Column:
                    break;
                case eStructureType.Footing:
                    break;
                case eStructureType.Slab:
                    break;
            }

        A:
            if (!valid)
                MessageBox.Show(note, "Data Invalid!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return valid;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            btnApply_Click(sender, e);
            if (!valid)
                return;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void cmbxForceUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            eForceUints fu_new = (eForceUints)Enum.Parse(typeof(eForceUints), cmbxForceUnit.Text);

            this.forceUnit = fu_new;
            btnApply.Enabled = true;
        }

        private void cmbxLengthUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            eLengthUnits lu_new = (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cmbxLengthUnit.Text);
            ntxtMaxAggregateSize.DoubleValue = eUtility.Convert(ntxtMaxAggregateSize.DoubleValue, lengthUnit, lu_new);

            switch (document.ModelType)
            {
                case eStructureType.Beam:

                    ntxtStirrupHookLength.DoubleValue = eUtility.Convert(ntxtStirrupHookLength.DoubleValue, lengthUnit, lu_new);
                    ntxtBeamReinfCover.DoubleValue = eUtility.Convert(ntxtBeamReinfCover.DoubleValue, lengthUnit, lu_new);
                    break;
                case eStructureType.Column:
                    break;
                case eStructureType.Slab:
                    ntxtSlabMaxBarSpacing.DoubleValue = eUtility.Convert(ntxtSlabMaxBarSpacing.DoubleValue, lengthUnit, lu_new);
                    ntxtSlabMinBarSpacing.DoubleValue = eUtility.Convert(ntxtSlabMinBarSpacing.DoubleValue, lengthUnit, lu_new);
                    ntxtSlabReinfCover.DoubleValue = eUtility.Convert(ntxtSlabReinfCover.DoubleValue, lengthUnit, lu_new);
                    break;
                case eStructureType.Footing:
                    ntxtFootingReinfCover.DoubleValue = eUtility.Convert(ntxtFootingReinfCover.DoubleValue, lengthUnit, lu_new);
                    ntxtBarLengthIncrement.LengthUnit = lu_new;
                    ntxtDowlesbentLength.LengthUnit = lu_new;
                    ntxtHookLength.LengthUnit = lu_new;
                    break;
            }
            this.lengthUnit = lu_new;
            btnApply.Enabled = true;
        }

        private void chkbxUseBarChoice2_CheckedChanged(object sender, EventArgs e)
        {
            cmbxBeamBar2.Enabled = chkbxUseBarChoice2.Checked;
            btnApply.Enabled = true;
        }

        private void ntxtMaxAggregateSize_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void cmbxClassWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void ntxtVariableLoadFactor_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void ntxtPermanentLoadFactor_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void cmbxDefaultSupport_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void cmbxBeamBar1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void cmbxBeamBar2_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void cmbxBeamStirrupBar_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void cmbxBeamStirrupPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void ntxtStirrupHookLength_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void ntxtBeamReinfCover_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void chkBeamConsiderSelfWeight_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void cmbxBeamDiagramStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void chkUseCornerBars_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void btnDefineConcrete_Click(object sender, EventArgs e)
        {
            eDefineMaterialDialog md = new eDefineMaterialDialog(document, eMaterialType.Concrete);

            if (md.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                while (cmbxConcrete.Items.Count > 0)
                    cmbxConcrete.Items.RemoveAt(0);

                foreach (var conc in document.Concretes)
                    cmbxConcrete.Items.Add(conc);

                cmbxConcrete.Text = document.Beam.Beam_Design.Concrete.Name;
            }
        }

        private void btnDefineSteel_Click(object sender, EventArgs e)
        {
            eDefineMaterialDialog md = new eDefineMaterialDialog(document, eMaterialType.ReinforcingSteel);

            if (md.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                while (cmbxSteel.Items.Count > 0)
                    cmbxSteel.Items.RemoveAt(0);

                foreach (var stl in document.Steels)
                    cmbxSteel.Items.Add(stl);

                cmbxSteel.Text = document.Beam.Beam_Design.Steel.Name;
            }
        }

        private void chTakeMaxValue_CheckedChanged(object sender, EventArgs e)
        {
            ntxtHookLength.Enabled = !ntxtHookLength.Enabled;
            lblHookLength.Enabled = !lblHookLength.Enabled;
        }


    }
}
