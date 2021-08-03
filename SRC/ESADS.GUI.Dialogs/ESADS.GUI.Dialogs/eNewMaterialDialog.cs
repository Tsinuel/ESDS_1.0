using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESADS.Code;
using ESADS.Code.EBCS_1995;
using ESADS.Mechanics.Design;

namespace ESADS.GUI
{
    public partial class eNewMaterialDialog : Form 
    {
        private eMaterialType materialType;
        private object material;

        #region Custom Properties
        public object Material
        {
            get
            {
                return material;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new dialog box with empty parameters
        /// </summary>
        /// <paparam name="MaterialType">The type of material to be created by the dialog box.</paparam>
        /// <param name="forceUnit">The window force unit chosen by the user in the main form.</param>
        /// <param name="lengthUnit">The window length unit chosen by the user in the main form.</param>
        public eNewMaterialDialog(eMaterialType materialType, eLengthUnits lengthUnit, eForceUints forceUnit)
        {
            InitializeComponent();

            InitializeCustomComponents(materialType, lengthUnit, forceUnit);
        }

        /// <summary>
        /// Creates a new dialog box in modify mode recieving the dialog box values of the material to be defined.
        /// </summary>
        public eNewMaterialDialog(eConcrete concreteToBeModified, eLengthUnits lengthUnit, eForceUints forceUnit)
        {
            InitializeComponent();

            InitializeCustomComponents(concreteToBeModified, lengthUnit, forceUnit);
        }

        /// <summary>
        /// Creates a new dialog box in modify mode recieving the dialog box values of the material to be defined.
        /// </summary>
        public eNewMaterialDialog(eSteel steelToBeModified, eLengthUnits lengthUnit, eForceUints forceUnit)
        {
            InitializeComponent();

            InitializeCustomComponents(steelToBeModified, lengthUnit, forceUnit);
        }

        #endregion

        #region Custom Methods
        private bool CheckDataValidity()
        {
            if (String.IsNullOrEmpty(txtMaterialName.Text.Trim()))
            {
                MessageBox.Show("The name of the material cannot be empty!", "Invalid input!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaterialName.SelectAll();
                txtMaterialName.Focus();
                return false;
            }
            if (chbxUsePredefinedGrades.Checked && String.IsNullOrEmpty(cmbxGrade.Text))
            {
                MessageBox.Show("A grade should be chosen if the \"Use predefined grades to define the material\" check box is checked.", "Invalid input!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbxGrade.Focus();
                return false;
            }
            if (ntxtUnitWeight.DoubleValue <= 0.0)
            {
                MessageBox.Show("The unit weight of a material cannot be zero or negative value.", "Invalid input!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ntxtUnitWeight.Focus();
                ntxtUnitWeight.SelectAll();
                return false;
            }
            if (ntxtModulusOfElasticity.DoubleValue <= 0.0)
            {
                MessageBox.Show("The modulus of elasticity value cannot be zero or negative.", "Invalid input!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ntxtModulusOfElasticity.Focus();
                ntxtModulusOfElasticity.SelectAll();
                return false;
            }
            if ((this.materialType == eMaterialType.Concrete) && ntxtCharConcCompStrength.DoubleValue <= 0.0)
            {
                MessageBox.Show("The characterstic compressive strength value of the concrete cannot be zero or negative.", "Invalid input!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ntxtCharConcCompStrength.Focus();
                ntxtCharConcCompStrength.SelectAll();
                return false;
            }
            if (this.materialType == eMaterialType.Concrete && ntxtCharConcTensileStrength.DoubleValue <= 0.0)
            {
                MessageBox.Show("The characterstic tensile strength value of the concrete cannot be zero or negative.", "Invalid input!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ntxtCharConcTensileStrength.Focus();
                ntxtCharConcTensileStrength.SelectAll();
                return false;
            }
            if (this.materialType == eMaterialType.ReinforcingSteel && ntxtSteelYieldStrength.DoubleValue <= 0.0)
            {
                MessageBox.Show("The characterstic yield strength value of the steel cannot be zero or negative.", "Invalid input!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ntxtSteelYieldStrength.Focus();
                ntxtSteelYieldStrength.SelectAll();
                return false;
            }
            return true;
        }

        private void InitializeCustomComponents(eMaterialType materialType, eLengthUnits lengthUnit, eForceUints forceUnit)
        {
            eUtility.FillComboBox<eLengthUnits>(cmbxLengthUnit, true);
            eUtility.FillComboBox<eForceUints>(cmbxFroceUnit, true);

            cmbxFroceUnit.Text = forceUnit.ToString();
            cmbxLengthUnit.Text = lengthUnit.ToString();

            this.materialType = materialType;

            //Checks if the material to be managed is concrete
            if (materialType == eMaterialType.Concrete)
            {
                this.Text = "New Concrete";
                grbxSteelSpecificProperties.Visible = false;
                grbxConcSpecificProperties.Visible = true;

                eUtility.FillComboBox<eConcreteGrade>(cmbxGrade, 0, (Enum.GetValues(typeof(eConcreteGrade)).Length - 1), true);
            }
            else
            {
                this.Text = "New Steel";
                grbxSteelSpecificProperties.Visible = true;
                grbxConcSpecificProperties.Visible = false;

                eUtility.FillComboBox<eSteelGrade>(cmbxGrade, 0, (Enum.GetValues(typeof(eSteelGrade)).Length - 1), true);
            }
        }

        private void InitializeCustomComponents(eSteel steelToBeModified, eLengthUnits lengthUnit, eForceUints forceUnit)
        {
            eUtility.FillComboBox<eLengthUnits>(cmbxLengthUnit, lengthUnit, true);
            eUtility.FillComboBox<eForceUints>(cmbxFroceUnit, forceUnit, true);

            //cmbxFroceUnit.Text = forceUnit.ToString();
            //cmbxLengthUnit.Text = lengthUnit.ToString();

            this.Text = "Modifying " + "\"" + steelToBeModified.Name + "\"";
            grbxSteelSpecificProperties.Visible = true;
            grbxConcSpecificProperties.Visible = false;

            eUtility.FillComboBox<eSteelGrade>(cmbxGrade, 0, (Enum.GetValues(typeof(eSteelGrade)).Length - 1), true);

            txtMaterialName.Text = steelToBeModified.Name;

            if (steelToBeModified.Grade == eSteelGrade.Custom)
            {
                chbxUsePredefinedGrades.Checked = false;
            }
            else
            {
                chbxUsePredefinedGrades.Checked = true;
                cmbxGrade.Text = steelToBeModified.Grade.ToString();
            }

            ntxtUnitWeight.SU = steelToBeModified.UnitWeight;
            ntxtSteelYieldStrength.SU = steelToBeModified.fyk;
            ntxtModulusOfElasticity.SU = steelToBeModified.E;
        }

        private void InitializeCustomComponents(eConcrete concreteToBeModified, eLengthUnits lengthUnit, eForceUints forceUnit)
        {
            eUtility.FillComboBox<eLengthUnits>(cmbxLengthUnit, lengthUnit, true);
            eUtility.FillComboBox<eForceUints>(cmbxFroceUnit, forceUnit, true);

            //cmbxFroceUnit.SelectedValue = forceUnit;
            //cmbxLengthUnit.SelectedValue = lengthUnit;

            this.Text = "Modifying " + "\"" + concreteToBeModified.Name + "\"";
            grbxSteelSpecificProperties.Visible = false;
            grbxConcSpecificProperties.Visible = true;

            eUtility.FillComboBox<eConcreteGrade>(cmbxGrade, 0, (Enum.GetValues(typeof(eConcreteGrade)).Length - 1), true);

            txtMaterialName.Text = concreteToBeModified.Name;

            if (concreteToBeModified.Grade == eConcreteGrade.Custom)
                chbxUsePredefinedGrades.Checked = false;
            else
            {
                chbxUsePredefinedGrades.Checked = true;
                cmbxGrade.Text = concreteToBeModified.Grade.ToString();
            }

            ntxtCharConcCompStrength.SU = concreteToBeModified.fck;
            ntxtCharConcTensileStrength.SU = concreteToBeModified.fctk;
            ntxtModulusOfElasticity.SU = concreteToBeModified.E;
            ntxtUnitWeight.SU = concreteToBeModified.UnitWeight;
        }

        private void FillPropertyFromGrade(eConcreteGrade grd)
        {
            txtMaterialName.Text = grd.ToString();
            ntxtUnitWeight.SU =eMaterial.Get_ConcUnitWeight(eConcreteType.NormalWeight);
            ntxtModulusOfElasticity.SU = eMaterial.Get_ConcModOfElasticity(grd);
            ntxtCharConcCompStrength.SU = eMaterial.Get_f_ck(grd);
            ntxtCharConcTensileStrength.SU = eMaterial.Get_f_ctk(grd);
        }

        private void FillPropertyFromGrade(eSteelGrade grd)
        {
            txtMaterialName.Text = grd.ToString();
            ntxtUnitWeight.SU = eMaterial.GetSteelUnitWeight();
            ntxtModulusOfElasticity.SU = eMaterial.GetSteelModulusOfElasticity();
            ntxtSteelYieldStrength.SU = eMaterial.Get_f_yk(grd);
        }

        #endregion

        #region Event Handlers

        private void chbxMethodOfInput_CheckedChanged(object sender, EventArgs e)
        {
            txtMaterialName.Enabled = !chbxUsePredefinedGrades.Checked;
            grbxConcSpecificProperties.Enabled = !chbxUsePredefinedGrades.Checked;
            grbxGeneralPhysicalProperties.Enabled = !chbxUsePredefinedGrades.Checked;
            grbxSteelSpecificProperties.Enabled = !chbxUsePredefinedGrades.Checked;
            cmbxGrade.Enabled = chbxUsePredefinedGrades.Checked;

            if (!String.IsNullOrEmpty(cmbxGrade.Text.Trim()))
            {
                if (this.materialType == eMaterialType.Concrete)
                {
                    eConcreteGrade conc = (eConcreteGrade)Enum.Parse(typeof(eConcreteGrade), cmbxGrade.Text);
                    FillPropertyFromGrade(conc);
                }
                else
                {
                    eSteelGrade st = (eSteelGrade)Enum.Parse(typeof(eSteelGrade), cmbxGrade.Text);
                    FillPropertyFromGrade(st);
                }
            }
        }

        private void cmbxLengthUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            eLengthUnits newUnit = (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cmbxLengthUnit.Text);
            ntxtUnitWeight.LengthUnit = newUnit;
            ntxtModulusOfElasticity.LengthUnit = newUnit;
            if (this.materialType == eMaterialType.Concrete)
            {
                ntxtCharConcCompStrength.LengthUnit = newUnit;
                ntxtCharConcTensileStrength.LengthUnit = newUnit;
            }
            else
            {
                ntxtSteelYieldStrength.LengthUnit = newUnit;
            }
        }

        private void cmbxFroceUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            eForceUints newUnit = (eForceUints)Enum.Parse(typeof(eForceUints), cmbxFroceUnit.Text);

            ntxtUnitWeight.ForceUnit = newUnit;
            ntxtModulusOfElasticity.ForceUnit = newUnit;
            if (this.materialType == eMaterialType.Concrete)
            {
                ntxtCharConcCompStrength.ForceUnit = newUnit;
                ntxtCharConcTensileStrength.ForceUnit = newUnit;
            }
            else
                ntxtSteelYieldStrength.ForceUnit = newUnit;
        }

        private void cmbxGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.materialType == eMaterialType.Concrete)
            {
                eConcreteGrade grd = (eConcreteGrade)Enum.Parse(typeof(eConcreteGrade), cmbxGrade.Text);

                FillPropertyFromGrade(grd);
            }
            else
            {
                eSteelGrade grd = (eSteelGrade)Enum.Parse(typeof(eSteelGrade), cmbxGrade.Text);

                FillPropertyFromGrade(grd);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckDataValidity())
            {
                if (this.materialType == eMaterialType.Concrete)
                {
                    if (!chbxUsePredefinedGrades.Checked)
                    {
                        double fck, fctk, E, w;

                        fck = ntxtCharConcCompStrength.SU;
                        fctk = ntxtCharConcTensileStrength.SU;
                        E = ntxtModulusOfElasticity.SU;
                        w = ntxtUnitWeight.SU;

                        this.material = new eConcrete(txtMaterialName.Text, fck, fctk, E, w);
                    }
                    else
                    {
                        eConcreteGrade c = (eConcreteGrade)Enum.Parse(typeof(eConcreteGrade), cmbxGrade.Text);

                        this.material = new eConcrete(c);
                    }
                }
                else
                {
                    if (!chbxUsePredefinedGrades.Checked)
                    {
                        double fyk, E, w;

                        fyk = ntxtSteelYieldStrength.SU;
                        E = ntxtModulusOfElasticity.SU;
                        w = ntxtUnitWeight.SU;

                        this.material = new eSteel(txtMaterialName.Text, E, fyk, w);
                    }
                    else
                    {
                        eSteelGrade s = (eSteelGrade)Enum.Parse(typeof(eSteelGrade), cmbxGrade.Text);

                        this.material = new eSteel(s);
                    }
                }
            }
        }

        #endregion

    }
}
