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
using ESADS.Mechanics.Design;

namespace ESADS.GUI
{
    public partial class eDefineMaterialDialog : Form
    {
        #region Fields
        private eDocument document;
        #endregion

        #region Constructors

        /// <summary>
        /// Creates new instance of the define materials dialog box.
        /// </summary>
        /// <param name="Values">values to be managed by the dialog box.</param>
        public eDefineMaterialDialog(eDocument document, eMaterialType firstPageLook = eMaterialType.Concrete)
        {
            this.document = document;
            
            InitializeComponent();
            InitializeCustomComponents();
            if(firstPageLook == eMaterialType.Concrete)
                cmbxMaterialType.Text = eMaterialType.Concrete.ToString();
            else
                cmbxMaterialType.Text = eMaterialType.ReinforcingSteel.ToString();
        }

        #endregion

        #region Cusotm Methods
        /// <summary>
        /// Chcecks if the data entered is valid.
        /// </summary>
        /// <returns></returns>
        private bool CheckDataValidity()
        {
            if (lstbxConcrete.Items.Count == 0)
            {
                MessageBox.Show("Atleast one concrete material has to be defined to conduct any design.", "Invalid input!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (lstbxSteel.Items.Count == 0)
            {
                MessageBox.Show("Atleast one steel material has to be defined to conduct any design.", "Invalid input!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Initializes the members of the dialog box with the values in Values property.
        /// </summary>
        private void InitializeCustomComponents()
        {
            eUtility.FillComboBox<eMaterialType>(cmbxMaterialType, true);

            foreach (var v in document.Concretes)
                lstbxConcrete.Items.Add(v.Name);
            foreach (var v in document.Steels)
                lstbxSteel.Items.Add(v.Name);

            eUtility.FillComboBox<eMaterialType>(cmbxMaterialType, true);
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

        /// <summary>
        /// Resets the collection to its original values.
        /// </summary>
        /// <param name="MaterialType"></param>
        public void ResetCollection(eMaterialType MaterialType)
        {
            if (MaterialType == eMaterialType.Concrete)
            {
                while (lstbxConcrete.Items.Count > 0)
                    lstbxConcrete.Items.RemoveAt(0);

                Array concs = Enum.GetValues(typeof(eConcreteGrade));

                foreach (var v in concs)
                {
                    if (v.ToString() != eConcreteGrade.Custom.ToString())
                        lstbxConcrete.Items.Add((new eConcrete((eConcreteGrade)v)).Name);
                }
            }
            else
            {
                while (lstbxSteel.Items.Count > 0)
                    lstbxSteel.Items.RemoveAt(0);

                Array steels = Enum.GetValues(typeof(eSteelGrade));

                foreach (var v in steels)
                {
                    if (v.ToString() != eSteelGrade.Custom.ToString())
                        lstbxSteel.Items.Add((new eSteel((eSteelGrade)v)).Name);
                }
            }
        }

        /// <summary>
        /// Checks if an item is not found a collection considering their name.
        /// </summary>
        /// <param name="Item">The name of the material to be checked for duplicateness.</param>
        /// <param name="Collection">collection to be checked for duplicate.</param>
        /// <returns>Returns true if there is no item with the same name as the item.</returns>
        /// <see cref="eDefineMaterialDialog.Sort"/>
        private bool ItemIsNotFoundIn(string Item, ListBox.ObjectCollection Collection)
        {
            foreach (var v in Collection)
            {
                if (v.ToString().Trim() == Item.Trim())
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if an item is not found a collection except the element at a specified index considering their names.
        /// </summary>
        /// <param name="Item">The name of the material to be checked for duplicateness.</param>
        /// <param name="MatList">collection to be checked for duplicate.</param>
        /// <returns>Returns true if there is no item with the same name as the item.</returns>
        private bool ItemIsNotFoundIn(string Item,ListBox.ObjectCollection Collection, int ExcludeIndex)
        {
            for (int i = 0; i < Collection.Count; i++)
            {
                if (Collection[i].ToString().Trim() == Item.Trim() && ExcludeIndex != i)
                    return false;
            }

            return true;
        }
        #endregion

        #region Event Handlers
        private void cmbxMaterialType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((eMaterialType)Enum.Parse(typeof(eMaterialType), cmbxMaterialType.Text) == eMaterialType.Concrete)
            {
                grbxConcrete.Visible = true;
                grbxSteel.Visible = false;
            }
            else
            {
                grbxConcrete.Visible = false;
                grbxSteel.Visible = true;
            }
        }

        private void btnNewSteel_Click(object sender, EventArgs e)
        {
            eNewMaterialDialog newSteelDialog = new eNewMaterialDialog(eMaterialType.ReinforcingSteel, document.LengthUnit, document.ForceUnit);
        A:

            if (newSteelDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ItemIsNotFoundIn(newSteelDialog.Material.ToString(), lstbxSteel.Items))
                {
                    lstbxSteel.Items.Add(newSteelDialog.Material.ToString());
                    btnApply.Enabled = true;
                }
                else
                {
                    DialogResult r;
                    r = MessageBox.Show("The material name is duplicate. Spaces at the begining and at \nthe end of the text don't differ one name from the other.\n\nDo you want to modify your input?",
                        "Name Duplicate!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (r == System.Windows.Forms.DialogResult.Yes)
                    {
                        goto A;
                    }
                }
            }
        }

        private void btnNewConcrete_Click(object sender, EventArgs e)
        {
            eNewMaterialDialog newConcDialog = new eNewMaterialDialog(eMaterialType.Concrete, document.LengthUnit, document.ForceUnit);
        A:

            if (newConcDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ItemIsNotFoundIn(newConcDialog.Material.ToString(), lstbxConcrete.Items))
                {
                    lstbxConcrete.Items.Add(newConcDialog.Material.ToString());
                    btnApply.Enabled = true;
                }
                else
                {
                    DialogResult r;
                    r = MessageBox.Show("The material name is duplicate. Spaces at the begining and at \nthe end of the text don't differ one name from the other.\n\nDo you want to modify your input?",
                        "Name Duplicate!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (r == System.Windows.Forms.DialogResult.Yes)
                    {
                        goto A;
                    }
                }
            }
        }

        private void btnModifySteel_Click(object sender, EventArgs e)
        {
            int idx = lstbxSteel.SelectedIndex;
            eNewMaterialDialog modifyDialog = new eNewMaterialDialog((eSteel)lstbxSteel.SelectedItem, document.LengthUnit, document.ForceUnit);
        A:
            if (modifyDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ItemIsNotFoundIn(modifyDialog.Material.ToString(),lstbxSteel.Items, idx))
                {
                    lstbxSteel.Items.Remove(lstbxSteel.SelectedItem);
                    lstbxSteel.Items.Insert(idx, modifyDialog.Material.ToString());
                    btnApply.Enabled = true;
                }
                else
                {
                    DialogResult r;
                    r = MessageBox.Show("The material name is duplicate. Spaces at the begining and at \nthe end of the text don't differ one name from the other.\n\nDo you want to modify your input?",
                        "Name Duplicate!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (r == System.Windows.Forms.DialogResult.Yes)
                    {
                        goto A;
                    }
                }
            }
        }

        private void btnModifyConcrete_Click(object sender, EventArgs e)
        {
            int idx = lstbxConcrete.SelectedIndex;

            eNewMaterialDialog modifyDialog = new eNewMaterialDialog((eConcrete)lstbxConcrete.SelectedItem, document.LengthUnit, document.ForceUnit);
        A:
            if (modifyDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ItemIsNotFoundIn(modifyDialog.Material.ToString(),lstbxConcrete.Items, idx))
                {
                    lstbxConcrete.Items.Remove(lstbxConcrete.SelectedItem);
                    lstbxConcrete.Items.Insert(idx, modifyDialog.Material.ToString());
                    btnApply.Enabled = true;
                }
                else
                {
                    DialogResult r;
                    r = MessageBox.Show("The material name is duplicate. Spaces at the begining and at \nthe end of the text don't differ one name from the other.\n\nDo you want to modify your input?",
                        "Name Duplicate!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (r == System.Windows.Forms.DialogResult.Yes)
                    {
                        goto A;
                    }
                }
            }
        }

        private void btnRemoveSteel_Click(object sender, EventArgs e)
        {
            bool removed = false;
            int val = 0;
            switch (document.ModelType)
            {
                case eStructureType.Beam:
                    while (lstbxSteel.SelectedItems.Count > val)
                    {
                        if (document.Beam != null && (lstbxSteel.SelectedItems[val] as eSteel).Name != document.Beam.Beam_Design.Steel.Name)
                        {
                            lstbxSteel.Items.Remove(lstbxSteel.SelectedItems[0]);
                            removed = true;
                        }
                        else
                            val++;
                    }
                    break;
                case eStructureType.Column:
                    break;
                case eStructureType.Footing:
                    break;
                case eStructureType.Slab:
                    break;
            }

            btnApply.Enabled = removed;
            btnRemoveSteel.Enabled = !removed;
        }

        private void btnRemoveConcrete_Click(object sender, EventArgs e)
        {
            bool removed = false;
            int val = 0;
            switch (document.ModelType)
            {
                case eStructureType.Beam:
                    while (lstbxConcrete.SelectedItems.Count > val)
                    {
                        if (document.Beam != null && (lstbxConcrete.SelectedItems[val] as eConcrete).Name != document.Beam.Beam_Design.Concrete.Name)
                        {
                            lstbxConcrete.Items.Remove(lstbxConcrete.SelectedItems[0]);
                            removed = true;
                        }
                        else
                            val++;
                    }
                    break;
                case eStructureType.Column:
                    break;
                case eStructureType.Footing:
                    break;
                case eStructureType.Slab:
                    break;
            }

            btnApply.Enabled = removed;
            btnRemoveConcrete.Enabled = !removed;
        }

        private void lstbxConcrete_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lstbxConcrete.SelectedItems.Count == 1)
                btnModifyConcrete.Enabled = true;
            else
                btnModifyConcrete.Enabled = false;

            if (lstbxConcrete.SelectedItems.Count > 0)
                btnRemoveConcrete.Enabled = true;
            else
                btnRemoveConcrete.Enabled = false;
        }

        private void lstbxSteel_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lstbxSteel.SelectedItems.Count == 1)
                btnModifySteel.Enabled = true;
            else
                btnModifySteel.Enabled = false;

            if (lstbxSteel.SelectedItems.Count > 0)
                btnRemoveSteel.Enabled = true;
            else
                btnRemoveSteel.Enabled = false;
        }

        private void btnResetSteel_Click(object sender, EventArgs e)
        {
            if (lstbxSteel.Items.Count > 0)
            {
                DialogResult res;

                res = MessageBox.Show("You may loose your defined steel materials. \n\nAre you sure you want to reset the steel materials collection to the system default?",
                    "Defined Steel materials are about to be lost!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    ResetCollection(eMaterialType.ReinforcingSteel);
                }
            }
            else
            {
                ResetCollection(eMaterialType.ReinforcingSteel);
            }
        }

        private void btnResetConcrete_Click(object sender, EventArgs e)
        {
            if (lstbxConcrete.Items.Count > 0)
            {
                DialogResult res;

                res = MessageBox.Show("You may loose your defined cocrete materials. \n\nAre you sure you want to reset the cocrete materials collection to the system default?",
                    "Defined concrete materials are about to be lost!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    ResetCollection(eMaterialType.Concrete);
                }
            }
            else
            {
                ResetCollection(eMaterialType.Concrete);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (CheckDataValidity())
            {
                document.Concretes = new List<eConcrete>();
                document.Steels = new List<eSteel>();
                foreach (var v in lstbxConcrete.Items)
                    document.Concretes.Add(ConcFromName(v.ToString()));

                foreach (var v in lstbxSteel.Items)
                    document.Steels.Add(SteelFromName(v.ToString()));
                
                btnApply.Enabled = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckDataValidity())
            {
                btnApply_Click(sender, e);
                this.Close();
            }
        }
        #endregion
    }
}