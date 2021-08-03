using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESADS.EGraphics.Slab;
using ESADS.EGraphics;

namespace ESADS.GUI
{
    public partial class eNewSlabDialog : Form
    {
        private eDocument document;
        public eNewSlabDialog(eDocument document)
        {
            this.document = document;
            InitializeComponent();
            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {
            if (this.document == null || this.document.Slab == null)
                throw new Exception("The document could not be retrieved.");

            cbxForce.Items.AddRange(Enum.GetNames(typeof(eForceUints)));
            cbxLength.Items.AddRange(Enum.GetNames(typeof(eLengthUnits)));

            cbxForce.Text = document.ForceUnit.ToString();
            cbxLength.Text = document.LengthUnit.ToString();

            ntxtBeamDepth.LengthUnit = document.LengthUnit;
            ntxtBeamWidth.LengthUnit = document.LengthUnit;
            ntxtColumnDiamOrDepth.LengthUnit = document.LengthUnit;
            ntxtColumnWidth.LengthUnit = document.LengthUnit;
            ntxtGrossDepth.LengthUnit = document.LengthUnit;
            ntxtXGridSpacing.LengthUnit = document.LengthUnit;
            ntxtYGridSpacing.LengthUnit = document.LengthUnit;

            ntxtBeamDepth.SU = document.Slab.DefaultBeamDepth;
            ntxtBeamWidth.SU = document.Slab.DefaultBeamWidth;
            if (document.Slab.DefaultColumnType == eColumnType.Circular)
            {
                radCircularColumn.Checked = true;
                ntxtColumnDiamOrDepth.SU = document.Slab.DefaultColumnDiameter;
            }
            else
            {
                radCircularColumn.Checked = false;
                ntxtColumnDiamOrDepth.SU = document.Slab.DefaultColumnDepth;
                ntxtColumnWidth.SU = document.Slab.DefaultColumnWidth;
            }

            ntxtNumH_Grids.IntValue = 4;
            ntxtNumV_Grids.IntValue = 4;
            ntxtXGridSpacing.SU = eUtility.Convert(3, eLengthUnits.m, eUtility.SLU);
            ntxtYGridSpacing.SU = eUtility.Convert(3, eLengthUnits.m, eUtility.SLU);
        }

        private void rbtCustomGridSpacing_CheckedChanged(object sender, EventArgs e)
        {
            gpxUseUniformGrid.Enabled = !radCustomGridSpacing.Checked;
        }

        private void chbxMinReqDepth_CheckedChanged(object sender, EventArgs e)
        {
            ntxtGrossDepth.Enabled = !chkUseMinReqDepth.Checked;
        }

        private void rbtnRectColumn_CheckedChanged(object sender, EventArgs e)
        {
            if (radRectColumn.Checked == true)
            {
                lblColumnDepthOrDiam.Text = "Depth";
                ntxtColumnWidth.Visible = true;
                lblColumnWidth.Visible = true;
            }
            else
            {
                lblColumnDepthOrDiam.Text = "Diameter";
                ntxtColumnWidth.Visible = false;
                lblColumnWidth.Visible = false;
            }
        }

        private void btnEditGridSpacing_Click(object sender, EventArgs e)
        {
            radCustomGridSpacing.Checked = true;

            eEditSlabGridDialog esg = new eEditSlabGridDialog(document);

            if (esg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        private void cbxLength_SelectedIndexChanged(object sender, EventArgs e)
        {
            eLengthUnits lu_new = (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cbxLength.Text);

            ntxtColumnDiamOrDepth.LengthUnit = lu_new;
            ntxtColumnWidth.LengthUnit = lu_new;
            ntxtGrossDepth.LengthUnit = lu_new;
            ntxtXGridSpacing.LengthUnit = lu_new;
            ntxtYGridSpacing.LengthUnit = lu_new;
            ntxtBeamDepth.LengthUnit = lu_new;
            ntxtBeamWidth.LengthUnit = lu_new;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!Valid())
                return;

            document.LengthUnit = (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cbxLength.Text);
            document.ForceUnit = (eForceUints)Enum.Parse(typeof(eForceUints), cbxForce.Text);

            document.Slab.DefaultBeamDepth = ntxtBeamDepth.SU;
            document.Slab.DefaultBeamWidth = ntxtBeamWidth.SU;
            if (radCircularColumn.Checked)
            {
                document.Slab.DefaultColumnType = eColumnType.Circular;
                document.Slab.DefaultColumnDiameter = ntxtColumnDiamOrDepth.SU;
            }
            else
            {
                document.Slab.DefaultColumnType = eColumnType.Rectangular;
                document.Slab.DefaultColumnDepth = ntxtColumnDiamOrDepth.SU;
                document.Slab.DefaultColumnWidth = ntxtColumnWidth.SU;
            }

            if (radUniformGridSpacing.Checked)
                document.Slab.CreateGrids(ntxtNumV_Grids.IntValue, ntxtNumH_Grids.IntValue, ntxtXGridSpacing.SU, ntxtYGridSpacing.SU);

        }

        private bool Valid()
        {
            bool valid = true;
            string note = "";

            if (ntxtBeamDepth.DoubleValue <= 0 || ntxtBeamWidth.SU <= 0)
            {
                valid = false;
                note = "Column and beam dimension cannot be zero or negative.";
            }
            else if (radCircularColumn.Checked && ntxtColumnDiamOrDepth.SU <= 0)
            {
                valid = false;
                note = "Column diameter cannot be zero or negative.";
            }
            else if (radRectColumn.Checked && ntxtColumnDiamOrDepth.SU <= 0 || ntxtColumnWidth.SU <= 0)
            {
                valid = false;
                note = "Column width cannot be zero or negative.";
            }
            else if (radUniformGridSpacing.Checked && ntxtXGridSpacing.SU <= 0 || ntxtYGridSpacing.SU <= 0 || ntxtNumH_Grids.IntValue < 2 || ntxtNumV_Grids.IntValue < 2)
            {
                valid = false;
                note = "Grid spacings cannot be zero and the minimum number of grids is 2.";
            }

            if (!valid)
            {
                MessageBox.Show(note, "Data Invalid!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return valid;
        }
    }
}
