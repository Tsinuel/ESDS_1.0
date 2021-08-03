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
using ESADS.Mechanics.Design.Column;
using ESADS.EGraphics.Column;
using ESADS.Mechanics.Design;

namespace ESADS.GUI
{
    /// <summary>
    /// Represents dialog used to create new column.
    /// </summary>
    public partial class eNewColumnDialog : Form
    {
        private eDocument doc;
        private eLengthUnits lengthUnit;
        private eForceUints forceUnit;
        private eDetailType detailType;
        private int xBars;
        private int yBars;
        

        /// <summary>
        /// Creates un instance of ESADS.GUI.eNewColumnDialog from a givne ESAD.eDocument object.
        /// </summary>
        /// <param name="doc">Document which contaings all the required data.</param>
        public eNewColumnDialog(eDocument doc)
        {
            InitializeComponent();
            this.doc = doc;
            if (doc.column != null)
                InitializeCustomComponents(doc.column.Column);
            else 
            InitializeCustomComponents();
        }

        //private void InitializeCustomComponents(eDColumn c)
        //{
        //    InitializeCustomComponents();
        //    ntxDepth.SU = c.Depth;
        //    ntxtWidth.SU = c.Width;
        //    ntxtNsd.SU = c.P;
        //    ntxtMx.SU = c.Mx;
        //    ntxtHprim.SU = c.Hprim;
        //    ntxtBprim.SU = c.Bprim;
        //    txtCover.SU = c.Cover;
        //    if (c.Action == eColumnAction.Biaxial)
        //    {
        //        rbtnBiaxial.Checked = true;
        //        ntxtMy.SU = (c as eBiaxial).My;
        //    }
        //    else
        //    {
        //        rbtnBiaxial.Checked = true;
        //    }
        //}

        private void InitializeCustomComponents()
        {
            this.lengthUnit = doc.LengthUnit;
            this.forceUnit = doc.ForceUnit;
            this.cbxLengthUnit.Items.AddRange(Enum.GetNames(typeof(eLengthUnits)));
            this.cbxForceUnit.Items.AddRange(Enum.GetNames(typeof(eForceUints)));         
            this.cbxExposureType.Items.AddRange(Enum.GetNames(typeof(eExposureType)));
            for (int i = 0; i < doc.Concretes.Count; i++)
                this.cbxCocrete.Items.Add(doc.Concretes[i].ToString());
            for (int i = 0; i < doc.Steels.Count; i++)
                this.cbxSteel.Items.Add(doc.Steels[i].ToString());
            cbxCocrete.Text = eConcreteGrade.C30.ToString();
            cbxSteel.Text = eSteelGrade.S400.ToString();
            this.cbxForceUnit.Text  = forceUnit.ToString();
            this.cbxLengthUnit.Text = lengthUnit.ToString();
            this.cbxExposureType.Text = eExposureType.Moderate.ToString();
            this.detailType = eDetailType.Type1;

            cbxCocrete.SelectedItem = doc.Concretes[4];
            cbxSteel.SelectedItem = doc.Steels[2];
            cbxTiesDiam.Items.Add(6);
            cbxTiesDiam.Items.Add(8);
            cbxTiesDiam.Items.Add(10);
            cbxTiesDiam.Items.Add(12);
            cbxTiesDiam.Items.Add(14);
            double[] diams = eDetailing.GetBarsBetwee(12, 32);

            for (int i = 0; i < diams.Length; i++)
            {
                cbxMinDiam.Items.Add(diams[i]);
                cbxMaxDiam.Items.Add(diams[i]);
                cbxMainBarDiam.Items.Add(diams[i]);
            }
            
            cbxTiesDiam.SelectedItem = 8;
            cbxMinDiam.Text = "12";
            cbxMaxDiam.Text = "32";
            cbxMainBarDiam.Text = "20";

            txtCover.Text = eUtility.ConvertFrom(eDetailing.GetMinConcreteCover((eExposureType)Enum.Parse(typeof(eExposureType), cbxExposureType.Text)), lengthUnit).ToString();
            this.ntxDepth.Text = eUtility.ConvertFrom(500, lengthUnit).ToString();
            this.ntxtWidth.Text = eUtility.ConvertFrom(400, lengthUnit).ToString();
            this.ntxtNsd.Text = eUtility.ConvertFrom(2000000, forceUnit).ToString();
            if (rbtnBiaxial.Checked)
            {
                this.ntxtMx.Text = eUtility.ConvertFrom(300000000, lengthUnit, forceUnit).ToString();
                this.ntxtMy.Text = eUtility.ConvertFrom(100000000, lengthUnit, forceUnit).ToString();
            }
            else 
            {
                this.ntxtMx.Text = eUtility.ConvertFrom(300000000, lengthUnit, forceUnit).ToString();
                this.ntxtMy.Text = "";
            }
            ntxtMaxAgrtSize.SU = 20;
            txtMinMaxSpacing.SU = 20;
            ntxtDepthTolerance.SU = 5;
        }

        private void InitializeCustomComponents(eDColumn col)
        {
            this.lengthUnit = doc.LengthUnit;
            this.forceUnit = doc.ForceUnit;
            this.cbxLengthUnit.Items.AddRange(Enum.GetNames(typeof(eLengthUnits)));
            this.cbxForceUnit.Items.AddRange(Enum.GetNames(typeof(eForceUints)));
            this.cbxExposureType.Items.AddRange(Enum.GetNames(typeof(eExposureType)));
            for (int i = 0; i < doc.Concretes.Count; i++)
                this.cbxCocrete.Items.Add(doc.Concretes[i].ToString());
            for (int i = 0; i < doc.Steels.Count; i++)
                this.cbxSteel.Items.Add(doc.Steels[i].ToString());
            cbxCocrete.Text = col.Concrete.ToString();
            cbxSteel.Text = col.Steel.ToString();
            this.cbxForceUnit.Text = forceUnit.ToString();
            this.cbxLengthUnit.Text = lengthUnit.ToString();
            this.cbxExposureType.Text = col.ExposureType.ToString();
            this.detailType = col.TypeOfDetail;
            cbxTiesDiam.Items.Add(6);
            cbxTiesDiam.Items.Add(8);
            cbxTiesDiam.Items.Add(10);
            cbxTiesDiam.Items.Add(12);
            cbxTiesDiam.Items.Add(14);
            double[] diams = eDetailing.GetBarsBetwee(12, 32);
            for (int i = 0; i < diams.Length; i++)
            {
                cbxMinDiam.Items.Add(diams[i]);
                cbxMaxDiam.Items.Add(diams[i]);
                cbxMainBarDiam.Items.Add(diams[i]);
            }

            cbxTiesDiam.SelectedItem = 8;
            cbxMinDiam.Text = col.MinDiam.ToString();
            cbxMaxDiam.Text = col.MaxDiam.ToString();
            cbxMainBarDiam.Text = col.BarDiam.ToString();
            txtCover.SU = col.Cover;
            this.ntxDepth.SU = col.Depth;
            this.ntxtWidth.SU = col.Width;
            this.ntxtNsd.SU = col.P;
            this.rbtnUniaxial.Checked = col.GetType() == typeof(eUniaxial);
            this.rbtnBiaxial.Checked = col.GetType() == typeof(eBiaxial);
            this.ntxtMx.SU = col.Mx;
            if (rbtnBiaxial.Checked)
                this.ntxtMy.SU = (col as eBiaxial).My;
            else
                this.ntxtMy.Text = "";
            rbtnEconomicalBarDiam.Checked = col.UseEcoDiam;
            ntxtMaxAgrtSize.SU = col.Concrete.MaxAgrtSize;
            txtMinMaxSpacing.SU = col.MaxSpacing;
            ntxtDepthTolerance.SU = 5;
            ntxtBprim.SU = col.Bprim;
            ntxtHprim.SU = col.Hprim;
            xBars = col.XBarNumber;
            yBars = col.YBarNumber;
            rbtnDesnAndDtl.Checked = col.DesingAndDetail;
            rbtnOnlyDesn.Checked = !col.DesingAndDetail;
            gbxEffectiveDims.Enabled = !gbxEffectiveDims.Enabled;
            for (int i = 0; i < tpDetailing.Controls.Count; i++)
            {
                tpDetailing.Controls[i].Enabled = !tpDetailing.Controls[i].Enabled;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtnUniaxialInXdxn_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnUniaxial.Checked )
            {
                ntxtMx.Enabled = true;
                ntxtMy.Enabled = false;
            }
            else
            {
                ntxtMx.Enabled = false;
                ntxtMy.Enabled = true;
            }
            if (rbtnUniaxial.Checked)
            {
                if (detailType == eDetailType.Type1)
                    pbxPreview.Image = Properties.Resources.UniaxialType1;
                if (detailType == eDetailType.Type2)
                    pbxPreview.Image = Properties.Resources.UniaxialType2;
                if (detailType == eDetailType.Type3)
                    pbxPreview.Image = Properties.Resources.UniaxialType3;
                if (detailType == eDetailType.Type4)
                    pbxPreview.Image = Properties.Resources.Type4Uniaxial;
            }
        }

        private void rbtnBiaxial_CheckedChanged(object sender, EventArgs e)
        {
            ntxtMx.Enabled = !ntxtMx.Enabled;
            if (rbtnBiaxial.Checked)
            {
                this.ntxtMy.Text = eUtility.ConvertFrom(100000000, lengthUnit, forceUnit).ToString();
            }
            else
                this.ntxtMy.Text = "";

            if (rbtnBiaxial.Checked)
            {
                if (detailType == eDetailType.Type1)
                    pbxPreview.Image = Properties.Resources.BiaxialType1;
                if (detailType == eDetailType.Type2)
                    pbxPreview.Image = Properties.Resources.BiaxialType2;
                if (detailType == eDetailType.Type3)
                    pbxPreview.Image = Properties.Resources.BiaxialType3;
                if (detailType == eDetailType.Type4)
                    pbxPreview.Image = Properties.Resources.Type4Biaxial;
            }
        }

        private void btnReinfDistribution_Click(object sender, EventArgs e)
        {
            eColumnReinforcementDistributionDialog d = new eColumnReinforcementDistributionDialog(detailType);
            if (doc.column != null)
            {
                d.DetailType = doc.column.Column.TypeOfDetail;
                d.XBars = doc.column.Column.XBarNumber;
                d.YBars = doc.column.Column.YBarNumber;
            }
            d.ShowDialog();
            this.detailType = d.DetailType;
            this.xBars = d.XBars;
            this.yBars = d.YBars;
            if (rbtnUniaxial.Checked)
            {
                if (detailType == eDetailType.Type1)
                    pbxPreview.Image = Properties.Resources.UniaxialType1;
                else if (detailType == eDetailType.Type2)
                    pbxPreview.Image = Properties.Resources.UniaxialType2;
                else if (detailType == eDetailType.Type3)
                    pbxPreview.Image = Properties.Resources.UniaxialType3;
                else
                    pbxPreview.Image = Properties.Resources.Type4Uniaxial;
            }
            else
            {
                if (detailType == eDetailType.Type1)
                    pbxPreview.Image = Properties.Resources.BiaxialType1;
                else if (detailType == eDetailType.Type2)
                    pbxPreview.Image = Properties.Resources.BiaxialType2;
                else if (detailType == eDetailType.Type3)
                    pbxPreview.Image = Properties.Resources.BiaxialType3;
                else
                    pbxPreview.Image = Properties.Resources.Type4Biaxial;
            }

        }

        private void rbtnEconomicalBarDiam_CheckedChanged(object sender, EventArgs e)
        {
            cbxMainBarDiam.Enabled = !cbxMainBarDiam.Enabled;
            cbxMaxDiam.Enabled = !cbxMaxDiam.Enabled;
            cbxMinDiam.Enabled = !cbxMinDiam.Enabled;
        }

        private void FillLoadAndDimension(eLengthUnits InputLengthUnit, eLengthUnits OutPutLeghtUnit, eForceUints InputForceUnit, eForceUints OutputForceUnit)
        {
            this.ntxDepth.Text = eUtility.Convert(ntxDepth, InputLengthUnit, OutPutLeghtUnit).ToString();
            this.ntxtWidth.Text = eUtility.Convert(ntxtWidth, InputLengthUnit, OutPutLeghtUnit).ToString();
            this.ntxtNsd.Text = eUtility.Convert(ntxtNsd, InputForceUnit, OutputForceUnit).ToString();
            if (rbtnBiaxial.Checked)
            {
                this.ntxtMx.Text = eUtility.Convert(ntxtMx, InputLengthUnit, OutPutLeghtUnit, InputForceUnit, OutputForceUnit).ToString();
                this.ntxtMy.Text = eUtility.Convert(ntxtMy, InputLengthUnit, OutPutLeghtUnit, InputForceUnit, OutputForceUnit).ToString();
            }
            else
            {
                this.ntxtMx.Text = eUtility.Convert(ntxtMx, InputLengthUnit, OutPutLeghtUnit, InputForceUnit, OutputForceUnit).ToString();
                this.ntxtMy.Text = "";
            }
            this.txtCover.Text = eUtility.Convert(txtCover, InputLengthUnit, OutPutLeghtUnit).ToString();
        }

        private void cbxLengthUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FillLoadAndDimension(lengthUnit, (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cbxLengthUnit.Text), forceUnit, forceUnit);
            this.lengthUnit = (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cbxLengthUnit.Text);
            ntxtBprim.LengthUnit = lengthUnit;
            ntxtHprim.LengthUnit = lengthUnit;
            ntxDepth.LengthUnit = lengthUnit;
            ntxtWidth.LengthUnit = lengthUnit;
            ntxtMaxAgrtSize.LengthUnit = lengthUnit;
            ntxtNsd.LengthUnit = lengthUnit;
            ntxtMx.LengthUnit = lengthUnit;
            ntxtMy.LengthUnit = lengthUnit;
            ntxtDepthTolerance.LengthUnit = lengthUnit;
            txtCover.LengthUnit = lengthUnit;
            txtMinMaxSpacing.LengthUnit = lengthUnit;
        }

        private void cbxForceUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
           // FillLoadAndDimension(lengthUnit, lengthUnit, forceUnit, (eForceUints)Enum.Parse(typeof(eForceUints), cbxForceUnit.Text));
            this.forceUnit = (eForceUints)Enum.Parse(typeof(eForceUints), cbxForceUnit.Text);
            ntxtBprim.ForceUnit = forceUnit;
            ntxtHprim.ForceUnit = forceUnit;
            ntxDepth.ForceUnit = forceUnit;
            ntxtWidth.ForceUnit = forceUnit;
            ntxtMaxAgrtSize.ForceUnit = forceUnit;
            ntxtNsd.ForceUnit = forceUnit;
            ntxtMx.ForceUnit = forceUnit;
            ntxtMy.ForceUnit = forceUnit;
            ntxtDepthTolerance.ForceUnit = forceUnit;
            txtCover.ForceUnit = forceUnit;
            txtMinMaxSpacing.ForceUnit = forceUnit;
        }

        private void cbxExposureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCover.Text = eUtility.ConvertFrom(eDetailing.GetMinConcreteCover((eExposureType)Enum.Parse(typeof(eExposureType), cbxExposureType.Text)),lengthUnit).ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            double minDim, maxDiam;
            eDColumn c;
            if (rbtnUniaxial.Checked)
                c = new eUniaxial(eUtility.ConvertTo(ntxtWidth, lengthUnit), eUtility.ConvertTo(ntxDepth, lengthUnit), eUtility.GetConcret(doc.Concretes,cbxCocrete.SelectedItem.ToString().ToString()),
                    eUtility.GetSteel(doc.Steels,cbxSteel.SelectedItem.ToString()), eUtility.ConvertTo(ntxtNsd, forceUnit), eUtility.ConvertTo(ntxtMx, lengthUnit, forceUnit), detailType);
            else
                c = new eBiaxial(eUtility.ConvertTo(ntxtWidth, lengthUnit), eUtility.ConvertTo(ntxDepth, lengthUnit), eUtility.GetConcret(doc.Concretes,cbxCocrete.SelectedItem.ToString()),
                      eUtility.GetSteel(doc.Steels, cbxSteel.SelectedItem.ToString()), eUtility.ConvertTo(ntxtNsd, forceUnit), eUtility.ConvertTo(ntxtMx, lengthUnit, forceUnit), eUtility.ConvertTo(ntxtMy, lengthUnit, forceUnit), detailType);
            c.Hprim = ntxtHprim.SU;
            c.Bprim = ntxtBprim.SU;
            c.DesingAndDetail = rbtnDesnAndDtl.Checked;

            if (detailType == eDetailType.Type3||detailType == eDetailType.Type4)
            {
                c.XBarNumber = xBars;
                c.YBarNumber = yBars;
            }
            if (rbtnEconomicalBarDiam.Checked)
            {
                maxDiam = double.Parse(cbxMaxDiam.Text);
                minDim = double.Parse(cbxMinDiam.Text);
            }
            else
                minDim = maxDiam = double.Parse(cbxMainBarDiam.Text);
            c.SetDetailingData(eUtility.ConvertTo(txtCover, lengthUnit), maxDiam, minDim, double.Parse(cbxTiesDiam.Text));
            if (doc.column != null)
                doc.column.ReleasHandlers();
            doc.Model = new eGColumn(doc.ModelForm, c);
        }

        private void rbtnOnlyDesn_CheckedChanged(object sender, EventArgs e)
        {
            gbxEffectiveDims.Enabled = !gbxEffectiveDims.Enabled;
            for (int i = 0; i < tpDetailing.Controls.Count; i++)
            {
                tpDetailing.Controls[i].Enabled = !tpDetailing.Controls[i].Enabled;
            }
            ntxtHprim.SU = 0.1 * ntxDepth.SU;
            ntxtBprim.SU = 0.1 * ntxtWidth.SU;         
        }

        private void rbtnBarDiam_CheckedChanged(object sender, EventArgs e)
        {
            cbxMainBarDiam.Enabled = !cbxMainBarDiam.Enabled;
        }
    }
}
