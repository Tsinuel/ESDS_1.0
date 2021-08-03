using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESADS.Code;
using ESADS.Export.ToAutoCAD;

namespace ESADS.GUI
{
    public partial class eExportToAutoCAD2007Dialog : Form
    {
        private eDocument doc;
        public eExportToAutoCAD2007Dialog(eDocument doc)
        {
            InitializeComponent();
            this.doc = doc;
            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {
            switch (doc.ModelType)
            {
                case eStructureType.Beam:
                    gbxBeamLayers.Visible = true;
                    break;
                case eStructureType.Column:
                    gbxColumnLayers.Visible = true;
                    break;
                case eStructureType.Slab:
                    gbxSlabLayers.Visible = true;
                    break;
                case eStructureType.Footing:
                    gbxFootingLayers.Visible = true;
                    break;
            }
            cbxUnit.Items.AddRange(Enum.GetNames(typeof(eLengthUnits)));
            cbxUnit.Text = eLengthUnits.mm.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            List<string> layrs = new List<string>();
            switch (doc.ModelType)
            {
                case eStructureType.Beam:
                    AddNames(layrs, txtBGrid, "Grid");
                    AddNames(layrs, txtBBars, "Bars");
                    AddNames(layrs, txtBText, "Text");
                    AddNames(layrs, txtBDim, "Dimension");
                    AddNames(layrs, txtBBeam, "Beam");
                    AddNames(layrs, txtBSecLine, "SectionLine");
                    eAcExport.ExportBeam(doc.Beam.Beam_Design, (string[])layrs.ToArray(), doc.LengthUnit);
                    break;
                case eStructureType.Column:
                    AddNames(layrs, txtCBars, "Bars");
                    AddNames(layrs, txtCText, "Text");
                    AddNames(layrs, txtCDim, "Dimension");
                    AddNames(layrs, txtCColumn, "Column");  
                    eAcExport.ExportColumn(doc.column.Column, (string[])layrs.ToArray(), doc.LengthUnit);
                    break;
                case eStructureType.Slab:
                    AddNames(layrs, txtSGrid, "Grid");
                    AddNames(layrs, txtSTopBar, "TopBars");
                    AddNames(layrs, txtSBotBars, "BottomBars");
                    AddNames(layrs, txtSText, "Text");
                    AddNames(layrs, txtSDim, "Dimension");
                    AddNames(layrs, txtSBeam, "Beam");
                    AddNames(layrs, txtSColumn, "Column");
                    AddNames(layrs, txtSsecLine, "SectionLine");
                    break;
                case eStructureType.Footing:
                    AddNames(layrs, txtFBars, "Bars");
                    AddNames(layrs, txtFText, "Text");
                    AddNames(layrs, txtFDim, "Dimension");
                    AddNames(layrs, txtFFooting, "Footing");
                    AddNames(layrs, txtFSecLine, "SectionLine");
                    break;
            }
        }
        private void AddNames(List<string> names, TextBox txtBox, string txt)
        {
            names.Add(txt);
            if (txtBox.Enabled)
                names.Add(txtBox.Text);
            else
                names.Add(null);
        }

        private void cbFBar_CheckedChanged(object sender, EventArgs e)
        {
            txtFBars.Enabled = !txtFBars.Enabled;
        }

        private void cbFText_CheckedChanged(object sender, EventArgs e)
        {
            txtFText.Enabled = !txtFText.Enabled;
        }

        private void cbFDim_CheckedChanged(object sender, EventArgs e)
        {
            txtFDim.Enabled = !txtFDim.Enabled;
        }

        private void cbFFooting_CheckedChanged(object sender, EventArgs e)
        {
            txtFFooting.Enabled = !txtFFooting.Enabled;
        }

        private void cbFSecLine_CheckedChanged(object sender, EventArgs e)
        {
            txtFSecLine.Enabled = !txtFSecLine.Enabled;
        }

        private void cbSTopBars_CheckedChanged(object sender, EventArgs e)
        {
            txtSTopBar.Enabled = !txtSTopBar.Enabled;
        }

        private void cbSBotBars_CheckedChanged(object sender, EventArgs e)
        {
            txtSBotBars.Enabled = !txtSBotBars.Enabled;
        }

        private void cbSText_CheckedChanged(object sender, EventArgs e)
        {
            txtSText.Enabled = !txtSText.Enabled;
        }

        private void cbSDim_CheckedChanged(object sender, EventArgs e)
        {
            txtSDim.Enabled = !txtSDim.Enabled;
        }

        private void cbSSlab_CheckedChanged(object sender, EventArgs e)
        {
            txtSSlab.Enabled = !txtSSlab.Enabled;
        }

        private void cbSBeam_CheckedChanged(object sender, EventArgs e)
        {
            txtSBeam.Enabled = !txtSBeam.Enabled;
        }

        private void cbSColumn_CheckedChanged(object sender, EventArgs e)
        {
            txtSColumn.Enabled = !txtSColumn.Enabled;
        }

        private void cbSSecLine_CheckedChanged(object sender, EventArgs e)
        {
            txtSsecLine.Enabled = !txtSsecLine.Enabled;
        }

        private void cbBGrid_CheckedChanged(object sender, EventArgs e)
        {
            txtBGrid.Enabled = !txtBGrid.Enabled;
        }

        private void cbBBars_CheckedChanged(object sender, EventArgs e)
        {
            txtBBars.Enabled = !txtBBars.Enabled;
        }

        private void cbBText_CheckedChanged(object sender, EventArgs e)
        {
            txtBText.Enabled = !txtBText.Enabled;
        }

        private void cbBDim_CheckedChanged(object sender, EventArgs e)
        {
            txtBDim.Enabled = !txtBDim.Enabled;
        }

        private void cbBBeam_CheckedChanged(object sender, EventArgs e)
        {
            txtBBeam.Enabled = !txtBBeam.Enabled;
        }

        private void cbBSecLine_CheckedChanged(object sender, EventArgs e)
        {
            txtBSecLine.Enabled = !txtBSecLine.Enabled;
        }

        private void cbCBars_CheckedChanged(object sender, EventArgs e)
        {
            txtCBars.Enabled = !txtCBars.Enabled;
        }

        private void cbCText_CheckedChanged(object sender, EventArgs e)
        {
            txtCText.Enabled = !txtCText.Enabled;
        }

        private void cbCDim_CheckedChanged(object sender, EventArgs e)
        {
            txtCDim.Enabled = !txtCDim.Enabled;
        }

        private void cbCColumn_CheckedChanged(object sender, EventArgs e)
        {
            txtCColumn.Enabled = !txtCColumn.Enabled;
        }

        private void cbSGrid_CheckedChanged(object sender, EventArgs e)
        {
            txtSGrid.Enabled = !txtSGrid.Enabled;
        }

       
    }
}
