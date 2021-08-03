using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESADS;
using ESADS.EGraphics;
using ESADS.Mechanics.Design;
using ESADS.Mechanics.Design.Beam;

namespace ESADS.GUI
{
    public partial class eBeamSectionDialog : Form
    {
        private eDocument document;
        private eBeamSection section;
        private eLengthUnits lengthUnit;
        public eBeamSection Section
        {
            get
            {
                return this.section;
            }
        }

        public eBeamSectionDialog(eDocument document)
        {
            this.document = document;
            InitializeComponent();
            InitialiseCustomComponents();           
        }

        public eBeamSectionDialog(eDocument document, eBeamSection section)
        {
            this.document = document;
            InitializeComponent();
            InitialiseCustomComponents(section);
        }

        private void InitialiseCustomComponents(eBeamSection section)
        {
            InitialiseCustomComponents();

            this.section = section;
            this.Text = "Modifying: " + section.Name;
            this.txtName.Text = section.Name;
            this.ntxtDepth.DoubleValue = eUtility.Convert(section.Depth, eUtility.SLU, document.LengthUnit);
            this.ntxtWidth.DoubleValue = eUtility.Convert(section.Width, eUtility.SLU, document.LengthUnit);
        }

        private void InitialiseCustomComponents()
        {
            eUtility.FillComboBox<eLengthUnits>(cbxLength, true);

            cbxLength.SelectedItem = document.LengthUnit;

            this.lengthUnit = document.LengthUnit;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!Valid())
                return;

            double d, b;
            d = eUtility.Convert(ntxtDepth.DoubleValue, lengthUnit, eUtility.SLU);
            b = eUtility.Convert(ntxtWidth.DoubleValue, lengthUnit, eUtility.SLU);

            this.section = new eBeamSection(txtName.Text, d, b);
            this.section.UseNominal_EI = chkUseNominal_EI.Checked;
            this.section.Nominal_EI = ntxtNominal_EI.DoubleValue;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private bool Valid()
        {
            bool valid = true;
            string note = "";
            
            if (ntxtDepth.DoubleValue <= 0)
            {
                valid = false;
                note = "The depth of a section cannot be zero or negative.";
            }
            if (ntxtWidth.DoubleValue <= 0)
            {
                valid = false;
                note = "The width of a section cannot be zero or negative.";
            }
            if (txtName.Text == "")
            {
                valid = false;
                note = "A section must have a name";
            }
            else
            {
                if(document.Beam != null)
                    foreach (var sec in document.Beam.Beam_Design.DefinedSections)
                    {
                        if (sec.Name.Trim() == txtName.Text.Trim())
                        {
                            valid = false;
                            note = "A section has been defined with the same name before. \nIf you want to modify that, select it in the define section dialog and click modify.";
                            break;
                        }
                    }
            }
            if (chkUseNominal_EI.Checked && ntxtNominal_EI.DoubleValue <=0)
            {
                valid = false;
                note = "The nominal EI value of a section cannot be zero or negative.";
            }
            if (!valid)
                MessageBox.Show(note, "Data invalid!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return valid;
        }

        private void btnShowProperties_Click(object sender, EventArgs e)
        {
            if (!Valid())
                return;

            double d, b;
            d = eUtility.Convert(ntxtDepth.DoubleValue, lengthUnit, eUtility.SLU);
            b = eUtility.Convert(ntxtWidth.DoubleValue, lengthUnit, eUtility.SLU);

            this.section = new eBeamSection(txtName.Text, d, b);

            MessageBox.Show("Area\t\t= " + section.GetArea().ToString() + "mm^2\n" + "Moment of Inertia\t= " + section.GetMomentOfInertia().ToString() +
                "mm^4\n" + "Nominal EI\t=" + ntxtNominal_EI.Text);
        }

        private void chkUseNominal_EI_CheckedChanged(object sender, EventArgs e)
        {
            ntxtNominal_EI.Enabled = chkUseNominal_EI.Checked;
        }

        private void cbxLength_SelectedIndexChanged(object sender, EventArgs e)
        {
            eLengthUnits lu_new = (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cbxLength.Text);

            ntxtDepth.DoubleValue = eUtility.Convert(ntxtDepth.DoubleValue, lengthUnit, lu_new);
            ntxtWidth.DoubleValue = eUtility.Convert(ntxtWidth.DoubleValue, lengthUnit, lu_new);

            this.lengthUnit = lu_new;
            pnlPreview.Invalidate();
        }

        private void pnlPreview_Paint(object sender, PaintEventArgs e)
        {
            //if (!Valid())
            //    return;

            Rectangle sec = new Rectangle();

            if (ntxtDepth.DoubleValue > ntxtWidth.DoubleValue)
            {
                sec.Height = (int)(pnlPreview.ClientRectangle.Height * 0.8);
                sec.Width = (int)(sec.Height * ntxtWidth.DoubleValue / ntxtDepth.DoubleValue);
            }
            else
            {
                sec.Width = (int)(pnlPreview.ClientRectangle.Width * 0.8);
                sec.Height = (int)(sec.Width * ntxtDepth.DoubleValue / ntxtWidth.DoubleValue);
            }

            sec.Location = new Point((int)(pnlPreview.ClientRectangle.X + pnlPreview.ClientRectangle.Width / 2 - sec.Width / 2),
                (int)(pnlPreview.ClientRectangle.Y + pnlPreview.ClientRectangle.Height / 2 - sec.Height / 2));

            e.Graphics.DrawRectangle(Pens.Yellow, sec);
        }

        private void ntxtDepth_TextChanged(object sender, EventArgs e)
        {
            pnlPreview.Invalidate();
        }

        private void ntxtWidth_TextChanged(object sender, EventArgs e)
        {
            pnlPreview.Invalidate();
        }
    }
}
