using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESADS.Mechanics.Design;
using ESADS.Mechanics.Design.Column;
namespace ESADS.GUI
{
    public partial class eColumnOutputDialog : Form
    {
        private eLengthUnits lengthUnit;
        private eForceUints forceUnit;
        private eDColumn column;
        private int precision;
        public eColumnOutputDialog(eLengthUnits lengthUnit, eForceUints forceUnit, eDColumn column)
        {
            InitializeComponent();
            this.column = column;
            this.lengthUnit = lengthUnit;
            this.forceUnit = forceUnit;
            this.cbxForceUnit.Items.AddRange(Enum.GetNames(typeof(eForceUints)));
            this.cbxLengthUnit.Items.AddRange(Enum.GetNames(typeof(eLengthUnits)));
            this.cbxForceUnit.SelectedText = forceUnit.ToString();
            this.cbxLengthUnit.SelectedText = lengthUnit.ToString();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            precision = 2;
            cbxForceUnit.Text = forceUnit.ToString();
            cbxLengthUnit.Text = lengthUnit.ToString();
            if (column.GetType() == typeof(eUniaxial))
            {
                gbxBiaxial.Visible = false;
                gbxUniaxial.Visible = true;
            }
            else
            {
                gbxBiaxial.Visible = true;
                gbxUniaxial.Visible = false;
                txtMb.Enabled = false;
                txtPb.Enabled = false;
            }
            FillReinf();
            FillLoadInteraction();
        }

        private void rbtnAscalc_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAscalc.Checked)
                column.UseArea(eAnalysisReinf.AsCalculated);
            else
                column.UseArea(eAnalysisReinf.AsProvided);
            FillLoadInteraction();
        }

        private void FillLoadInteraction()
        {
            txtPpure.Text = Math.Round(eUtility.ConvertFrom(column.GetMaxAxialCapacity(), forceUnit), precision).ToString();
           
            if (column.GetType() == typeof(eUniaxial))
            {
                lblAction.Text = "Action: Uniaxial";
                txtMb.Text = Math.Round(eUtility.ConvertFrom((column as eUniaxial).GetMb(), lengthUnit, forceUnit), precision).ToString();
                txtPb.Text = Math.Round(eUtility.ConvertFrom((column as eUniaxial).GetPb(), forceUnit), precision).ToString();
                txtMpure.Text = Math.Round(eUtility.ConvertFrom((column as eUniaxial).GetM(0), lengthUnit, forceUnit), precision).ToString();
            }
            else
            {
                lblAction.Text = "Action: Biaxial  Mx/My = " + (column as eBiaxial).R.ToString();
                txtMb.Text = "";
                txtPb.Text = "";
                txtMpure.Text = Math.Round(eUtility.ConvertFrom((column as eBiaxial).GetMxAt(0, (column as eBiaxial).R), lengthUnit, forceUnit), precision).ToString();
            }
        }

        private void FillReinf()
        {
            txtAscalcualted.Text = Math.Round(column.AsTotal, precision).ToString();
            txtAsprovided.Text = Math.Round(column.AstProvided, precision).ToString();
            txtAsmax.Text = Math.Round(column.AsMax, precision).ToString();
            txtAsmin.Text = Math.Round(column.AsMin, precision).ToString();
            txtEconomy.Text = Math.Round(100 * column.AsTotal / column.AstProvided, 2).ToString();
            if (!column.DesingAndDetail)
            {

                txtAsprovided.Text = Math.Round(column.AsTotal, precision).ToString();
                txtEconomy.Text = "100";
            }
        }

        private void txtUniCalc_Click(object sender, EventArgs e)
        {
            try
            {
                txtM.SU = (column as eUniaxial).GetM(txtPUni.SU);
                ntxtX.SU = (column as eUniaxial).GetX(txtPUni.SU);
                txtM.DoubleValue = Math.Round((double)txtM, precision);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Above Maximum Capacity", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void cbxLengthUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            lengthUnit = (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cbxLengthUnit.Text);
            txtM.LengthUnit = lengthUnit;
            txtMb.LengthUnit = lengthUnit;
            txtMpure.LengthUnit = lengthUnit;
            txtPb.LengthUnit = lengthUnit;
            txtPpure.LengthUnit = lengthUnit;
            txtPUni.LengthUnit = lengthUnit;
            txtMy.LengthUnit = lengthUnit;
            txtMx.LengthUnit = lengthUnit;
            txtPbi.LengthUnit = lengthUnit;
            ntxtX.LengthUnit = lengthUnit;
        }

        private void cbxForceUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            forceUnit = (eForceUints)Enum.Parse(typeof(eForceUints), cbxForceUnit.Text);
            txtM.ForceUnit = forceUnit;
            txtMb.ForceUnit = forceUnit;
            txtMpure.ForceUnit = forceUnit;
            txtPb.ForceUnit = forceUnit;
            txtPpure.ForceUnit = forceUnit;
            txtPUni.ForceUnit = forceUnit;
            txtMy.ForceUnit = forceUnit;
            txtMx.ForceUnit = forceUnit;
            txtPbi.ForceUnit = forceUnit;
        }

        private void eColumnOutputDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            column.UseArea(eAnalysisReinf.AsCalculated);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUniClick_Click(object sender, EventArgs e)
        {
           
        }

        private void btnBiCalc_Click(object sender, EventArgs e)
        {
            double tt = 0, xx = 0;
            try
            {
                txtMx.SU = (column as eBiaxial).GetMxAt(txtPbi.SU, txtR, ref xx, ref tt);
                txtMx.DoubleValue = Math.Round((double)txtMx, precision);
                txtMy.DoubleValue = Math.Round((double)txtMx / txtR, precision);
                txtXbi.SU = xx;
                txtTeta.SU = tt * 180d / Math.PI;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Above Maximum Capacity", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

    }
}
