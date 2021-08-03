using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESADS.Mechanics.Design;
using ESADS.Mechanics.Design.Beam;

namespace ESADS.GUI
{
    public partial class eDefineBeamSectionDialog : Form
    {
        private eDocument document;
        private bool isInAssignMode;
        private eBeamSection selectedSection;

        public eBeamSection SelectedSection
        {
            get
            {
                return this.selectedSection;
            }
        }

        public eDefineBeamSectionDialog(eDocument document, bool isInAssignMode = false)
        {
            this.isInAssignMode = isInAssignMode;
            this.document = document;
            InitializeComponent();
            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {
            if (this.isInAssignMode)
            {
                btnOK.Text = "&Assign";
                btnOK.Enabled = false;
                btnApply.Visible = true;
                btnApply.Enabled = false;
            }

            foreach (var sec in document.Beam.Beam_Design.DefinedSections)
            {
                lstbxDefinedSections.Items.Add(sec);
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
            eBeamSectionDialog nb = new eBeamSectionDialog(this.document);

        A:
            if (nb.ShowDialog() == DialogResult.OK)
            {
                if (!IsDuplicate(nb.Section.Name))
                    lstbxDefinedSections.Items.Add(nb.Section);
                else
                {
                    if (MessageBox.Show("A section has been defined with the same name before!\n\nDo you want to modify your section?",
                        "Duplicate section name!", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                        goto A;
                }
            }
        }

        private bool IsDuplicate(string name, int exclude = -1)
        {
            if (exclude == -1)
            {
                foreach (var v in this.lstbxDefinedSections.Items)
                {
                    if (v.ToString() == name)
                        return true;
                }
            }
            else
            {
                for (int i = 0; i < lstbxDefinedSections.Items.Count; i++)
                {
                    if (i != exclude && lstbxDefinedSections.Items[i].ToString() == name)
                        return true;
                }
            }
            return false;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
            eBeamSectionDialog mb = new eBeamSectionDialog(this.document, (eBeamSection)lstbxDefinedSections.SelectedItem);

        A:
            if (mb.ShowDialog() == DialogResult.OK)
            {
                if (!IsDuplicate(mb.Section.Name, lstbxDefinedSections.SelectedIndex))
                {
                    lstbxDefinedSections.Items.Remove(lstbxDefinedSections.SelectedItem);
                    lstbxDefinedSections.Items.Add(mb.Section);
                }
                else
                {
                    if (MessageBox.Show("A section has been defined with the same name before!\n\nDo you want to modify your section?",
                        "Duplicate section name!", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                        goto A;
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            btnApply.Enabled = true;

            while (lstbxDefinedSections.SelectedItems.Count > 0)
            {
                lstbxDefinedSections.Items.Remove(lstbxDefinedSections.SelectedItems[0]);
            }
        }

        private void lstbxDefinedSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstbxDefinedSections.SelectedItems.Count > 0)
            {
                if (lstbxDefinedSections.SelectedItems.Count == 1)
                {
                    btnModify.Enabled = true;
                    if (this.isInAssignMode)
                        btnOK.Enabled = true;
                }
                else
                {
                    btnModify.Enabled = false;
                    if (this.isInAssignMode)
                        btnOK.Enabled = false;
                }
                btnRemove.Enabled = true;
            }
            else
            {
                btnRemove.Enabled = false;
                btnModify.Enabled = false;
                if (this.isInAssignMode)
                    btnOK.Enabled = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.isInAssignMode)
            {
                this.selectedSection = (eBeamSection)lstbxDefinedSections.SelectedItem;
                this.selectedSection.Used = true;
            }
            btnApply_Click(sender, e);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.document.Beam.Beam_Design.DefinedSections = new List<eBeamSection>();

            foreach (var v in lstbxDefinedSections.Items)
            {
                this.document.Beam.Beam_Design.DefinedSections.Add((eBeamSection)v);
            }
            btnApply.Enabled = false;
        }
    }
}
