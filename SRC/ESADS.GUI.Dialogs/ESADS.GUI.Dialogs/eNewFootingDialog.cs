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
using ESADS.Mechanics.Design.Footing;
using ESADS.Mechanics.Design;
using ESADS.GUI.Controls;
using ESADS.EGraphics.Footing;

namespace ESADS.GUI
{
    public partial class eNewFootingDialog:Form
    {
        private eDocument doc;
        private eLengthUnits lengthUnit;
        private eForceUints forceUnit;
        private bool isNewDialog;
        /// <summary>
        /// Creates ESADS.GUI.eNewFootingDialog from a given document.
        /// </summary>
        /// <param name="doc">Document to be used in the design.</param>
        public eNewFootingDialog(eDocument doc, bool isNewDialog = true)
        {
            this.doc = doc;
            this.lengthUnit = doc.LengthUnit;
            this.forceUnit = doc.ForceUnit;
            this.isNewDialog  = isNewDialog;
            InitializeComponent();
            if (isNewDialog)
                InitializeCustomComponents();
            else
            {
                InInitializeCustomComponents(doc.Footing.Footing);
                if (doc.Footing.Footing.CompletionState == eDesignCompletionState.InsufficeintDepth)
                    ntxtDepth.SelectAll();
                if(doc.Footing.Footing.CompletionState == eDesignCompletionState.NoBarBetweenSpacingLimit)
                {
                    tabPage2.Select();
                    ntxtMinS.SelectAll();
                    ntxtMaxS.SelectAll();
                }
            }
        }

        public eNewFootingDialog()
        {
            InitializeComponent();
            InitializeCustomComponents();           
        }
        private void InitializeCustomComponents()
        {
            FillComboBoxs();
            ntxtLength.SU = 3000;
            ntxtWidth.SU = 3000;
            chDesignDepth.Checked = true;
            ntxtMx.SU = 100000000;
            ntxtMy.SU = 100000000;
            ntxtP.SU = 1000000;
            ntxtColumnLength.SU = 400;
            ntxtColumnWidth.SU = 400;
            ntxtCover.SU = 50;
            ntxtMinS.SU = 50;
            ntxtMaxS.SU = 350;
            ntxtDepthIncrment.SU = 10;
            ntxtSpacingIncrement.SU = 10;
            ntxtMaxAggrtSize.SU = 20;
        }

        private void InInitializeCustomComponents(eDFooting f)
        {
            FillComboBoxs();
            if (f.LongSideHorizontal)
            {
                ntxtLength.SU = f.Length;
                ntxtWidth.SU = f.Width;
                ntxtColumnWidth.SU = f.ColumnWidth;
                ntxtColumnLength.SU = f.ColumnLength;
                ntxtMx.SU = f.ML;
                ntxtMy.SU = f.MB;
            }
            else
            {
                ntxtLength.SU = f.Width;
                ntxtWidth.SU = f.Length;
                ntxtColumnWidth.SU = f.ColumnLength;
                ntxtColumnLength.SU = f.ColumnWidth;
                ntxtMx.SU = f.MB;
                ntxtMy.SU = f.ML;
            }
            ntxtP.SU = f.P;
            chDesignDepth.Checked = !f.IsDepthGiven;
            rbtnRectangular.Checked = f.ColumnType == eColumnType.Rectangular;
            chConsiderSelfWeight.Checked = f.ConsiderSelfWeight;
            cbxConcrete.SelectedItem = f.Concrete;
            cbxSteel.SelectedItem = f.Steel;
            cbxMaxDiam.SelectedItem = f.MaxBar;
            cbxMinDiam.SelectedItem = f.MinBar;
            cbxDiameter.SelectedItem = f.BarDiam != 0 ? f.BarDiam : f.MinBar;
            chUseSpacing.Checked = f.Spacing != 0;
            chUseDiam.Checked = f.BarDiam != 0;
            ntxtMaxAggrtSize.SU = f.Concrete.MaxAgrtSize;
            ntxtMaxS.SU = f.MaxSpacing;
            ntxtMinS.SU = f.MinSpacing;
            cbxExposureType.SelectedItem = f.ExposureType;
            ntxtCover.SU = f.Cover;
            ntxtDepthIncrment.SU = f.DepthIncrement;
            ntxtSpacingIncrement.SU = f.SpacingIncrement;
            ntxtSpacing.SU = 150;
            ntxtDepth.SU = f.Depth;
        }

        private void chDesignDepth_CheckedChanged(object sender, EventArgs e)
        {
            ntxtDepth.Enabled = !ntxtDepth.Enabled;
        }

        private void chUseSpacing_CheckedChanged(object sender, EventArgs e)
        {
            ntxtSpacing.Visible = !ntxtSpacing.Visible;
            ntxtMinS.Enabled = !ntxtMinS.Enabled;
            ntxtMaxS.Enabled = !ntxtMaxS.Enabled;
            ntxtSpacingIncrement.Enabled = !ntxtSpacingIncrement.Enabled;
            if (ntxtSpacing.Visible)
                ntxtSpacing.SU = 150; ;

            gbBarDiam.Enabled = !gbBarDiam.Enabled;
        }

        private void chUseDiam_CheckedChanged(object sender, EventArgs e)
        {
            cbxDiameter.Visible = !cbxDiameter.Visible;
            cbxMinDiam.Enabled = !cbxMinDiam.Enabled;
            cbxMaxDiam.Enabled = !cbxMaxDiam.Enabled;
        }

        private void cbxExposureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ntxtCover.SU =  eDetailing.GetMinConcreteCover((eExposureType)Enum.Parse(typeof(eExposureType), cbxExposureType.Text));
        }

        private void cbxLengthUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            lengthUnit = (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cbxLengthUnit.Text);
            ntxtLength.LengthUnit = lengthUnit;
            ntxtWidth.LengthUnit = lengthUnit;
            ntxtMx.LengthUnit = lengthUnit;
            ntxtMy.LengthUnit = lengthUnit;
            ntxtP.LengthUnit = lengthUnit;
            ntxtColumnLength.LengthUnit = lengthUnit;
            ntxtColumnWidth.LengthUnit = lengthUnit;
            ntxtCover.LengthUnit = lengthUnit;
            ntxtMinS.LengthUnit = lengthUnit;
            ntxtMaxS.LengthUnit = lengthUnit;
            ntxtDepthIncrment.LengthUnit = lengthUnit;
            ntxtSpacingIncrement.LengthUnit = lengthUnit;
            ntxtMaxAggrtSize.LengthUnit = lengthUnit;
            ntxtSpacing.LengthUnit = lengthUnit;
            ntxtDepth.LengthUnit = lengthUnit;
        }

        private void cbxForceUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            forceUnit = (eForceUints)Enum.Parse(typeof(eForceUints), cbxForceUnit.Text);
            ntxtLength.ForceUnit = forceUnit;
            ntxtWidth.ForceUnit = forceUnit;
            ntxtMx.ForceUnit = forceUnit;
            ntxtMy.ForceUnit = forceUnit;
            ntxtP.ForceUnit = forceUnit;
            ntxtColumnLength.ForceUnit = forceUnit;
            ntxtColumnWidth.ForceUnit = forceUnit;
            ntxtCover.ForceUnit = forceUnit;
            ntxtMinS.ForceUnit = forceUnit;
            ntxtMaxS.ForceUnit = forceUnit;
            ntxtDepthIncrment.ForceUnit = forceUnit;
            ntxtSpacingIncrement.ForceUnit = forceUnit;
            ntxtMaxAggrtSize.ForceUnit = forceUnit;
            ntxtSpacing.ForceUnit = forceUnit;
        }


        private void rbtnRectangular_CheckedChanged_1(object sender, EventArgs e)
        {
            ntxtColumnDiameter.Visible = !ntxtColumnDiameter.Visible;
            ntxtColumnLength.Visible = !ntxtColumnLength.Visible;
            ntxtColumnWidth.Visible = !ntxtColumnWidth.Visible;
            lblBc.Visible = !lblBc.Visible;
            lblColumnWidth.Visible = !lblColumnWidth.Visible;
            lblColumnDiam.Visible = !lblColumnDiam.Visible;
        }

        private void chDesignDepth_CheckedChanged_1(object sender, EventArgs e)
        {
            ntxtDepth.Enabled = !ntxtDepth.Enabled;
            ntxtDepthIncrment.Enabled = chDesignDepth.Checked;
        }

        private void FillComboBoxs()
        {
            double[] diams = eDetailing.GetBarsBetwee(8, 32);
            eUtility.FillComboBox<eLengthUnits>(cbxLengthUnit, lengthUnit);
            eUtility.FillComboBox<eForceUints>(cbxForceUnit, forceUnit);
            eUtility.FillComboBox<eExposureType>(cbxExposureType, eExposureType.Severe);
            eUtility.FillComboBox<eReinforcement>(cbxMinDiam, 1);
            eUtility.FillComboBox<eReinforcement>(cbxMaxDiam, 1);
            eUtility.FillComboBox<eReinforcement>(cbxDiameter, 1);
            for (int i = 0; i < doc.Concretes.Count; i++)
                cbxConcrete.Items.Add(doc.Concretes[i].Name);
            for (int i = 0; i < doc.Steels.Count; i++)
                cbxSteel.Items.Add(doc.Steels[i]);
            cbxConcrete.Text = eConcreteGrade.C25.ToString();
            cbxSteel.Text = eSteelGrade.S300.ToString();
            cbxMinDiam.SelectedItem = eReinforcement.Φ14;
            cbxMaxDiam.SelectedItem = eReinforcement.Φ24;
            cbxDiameter.SelectedItem = eReinforcement.Φ20;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            eDFooting f;
            eConcrete c = eUtility.GetConcret(doc.Concretes, cbxConcrete.Text);
            eSteel s = eUtility.GetSteel(doc.Steels, cbxSteel.Text);

            if (ntxtLength > ntxtWidth)
            {
                if (!chDesignDepth.Checked)
                    f = new eDFooting(ntxtLength.SU, ntxtWidth.SU, s, c, ntxtDepth.SU, ntxtP.SU, ntxtMy.SU, ntxtMx.SU);
                else
                    f = new eDFooting(ntxtLength.SU, ntxtWidth.SU, s, c, ntxtP.SU, ntxtMy.SU, ntxtMx.SU);
                f.ColumnType = rbtnRectangular.Checked ? eColumnType.Rectangular : eColumnType.Circular;
                f.ColumnWidth = ntxtColumnWidth.SU;
                f.ColumnLength = ntxtColumnLength.SU;
            }
            else
            {
                if (!chDesignDepth.Checked)
                    f = new eDFooting(ntxtWidth.SU, ntxtLength.SU, s, c, ntxtDepth.SU, ntxtP.SU, ntxtMx.SU, ntxtMy.SU);
                else
                    f = new eDFooting(ntxtLength.SU, ntxtWidth.SU, s, c, ntxtP.SU, ntxtMx.SU, ntxtMy.SU);

                f.ColumnType = rbtnRectangular.Checked ? eColumnType.Rectangular : eColumnType.Circular;
                f.ColumnWidth = ntxtColumnLength.SU;
                f.ColumnLength = ntxtColumnWidth.SU;
            }
            f.ColumnDiameter = ntxtColumnDiameter.SU;
            f.MaxBar = (int)((eReinforcement)Enum.Parse(typeof(eReinforcement), cbxMaxDiam.Text));
            f.MinBar = (int)((eReinforcement)Enum.Parse(typeof(eReinforcement), cbxMinDiam.Text));
            f.BarDiam = (int)((eReinforcement)Enum.Parse(typeof(eReinforcement), cbxDiameter.Text));
            f.MaxSpacing = ntxtMaxS.SU;
            f.MinSpacing = ntxtMinS.SU;
            f.Spacing = ntxtSpacing.SU;
            f.MaxAggrSize = ntxtMaxAggrtSize.SU;
            f.DepthIncrement = ntxtDepthIncrment.SU;
            f.SpacingIncrement = ntxtSpacingIncrement.SU;
            f.IsDepthGiven = !chDesignDepth.Checked;
            f.UseDiameter = chUseDiam.Checked;
            f.UseSpacing = chUseSpacing.Checked;
            f.Cover = ntxtCover.SU;
            f.ExposureType = (eExposureType)cbxExposureType.SelectedItem;
            if (doc.Footing != null)
                doc.Footing.Layers.ReleaseHandlers();
            doc.Model = new eGFooting(doc.ModelForm, f);
            doc.Footing.DrawModel();
        }   
    }
}
