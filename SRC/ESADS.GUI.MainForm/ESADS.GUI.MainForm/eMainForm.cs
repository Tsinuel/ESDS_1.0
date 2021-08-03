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
using ESADS.EGraphics;
using ESADS.EGraphics.Beam;
using ESADS.GUI;
using ESADS.Mechanics;
using ESADS.Mechanics.Analysis;
using ESADS.Mechanics.Analysis.Beam;
using ESADS.Mechanics.Design;
using ESADS.Mechanics.Design.Beam;
using ESADS.Mechanics.Design.Column;
using ESADS.Mechanics.Design.Footing;
using ESADS.EGraphics.Footing;

namespace ESADS.GUI.MainForm
{
    public partial class eMainForm : Form
    {
        #region Feilds
        /// <summary>
        /// The Current active Document on which the application is working on.
        /// </summary>
        eDocument doc;
        #endregion

        #region Constructors

        public eMainForm(eDocument document)
        {          
            InitializeComponent();
            InitializeCustomComponets();
            tlstrpcontMain.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            doc = document;
            doc.ModelForm.MdiParent = this;
        }

        public eMainForm()
        {
            InitializeComponent();
            InitializeCustomComponets();
            tlstrpcontMain.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;       
        }
        #endregion

        #region Custom methods

        /// <summary>
        /// Generates the next unoccupied name for files depending on the structure type.
        /// </summary>
        /// <param name="eStructureType">the type of the structure to be named.</param>
        /// <returns></returns>
        private string GetNextUnoccupiedName(eStructureType StructureType)
        {
            int n = 1;
            string name = "Section " + n.ToString() + "-" + n.ToString();
            switch (StructureType)
            {
                case eStructureType.Beam:
                    {
                        eModelForm  frm;
                        for (int i = 0; i < this.MdiChildren.Length; i++)
                        {
                            frm = (eModelForm)this.MdiChildren[i];
                            if (string.Compare(name, frm.Document.Name) == 0)
                            {
                                n++;
                                name = "Section " + n.ToString() + "-" + n.ToString();
                            }
                            else
                            {
                                return name;
                            }
                        }
                        break;
                    }
            }
            return name;
        }

        /// <summary>
        /// Initailizes custom componetes that are not conained in default windows form application.
        /// </summary>
        private void InitializeCustomComponets()
        {
            eUtility.FillComboBox<eLengthUnits>(cbxWindowsLengthUnit, true);
            eUtility.FillComboBox<eForceUints>(cbxWindowsForceUnit, true);
            this.MdiChildActivate += new EventHandler(eMainForm_MdiChildActivate);
        }

        /// <summary>
        /// Checks if a material is found in the defined materials list and adds it to the list if it is not found.
        /// </summary>
        /// <param name="Concrete">Concrete item to be verified.</param>
        /// <param name="Steel">Steel item to be verified.</param>
        private void VerifyPresence()
        {
            //bool notFound = true;
            //foreach (var mat in activeDoc.ConcreteItems)
            //{
            //    if (mat.Name == Concrete.Name)
            //    {
            //        notFound = false;
            //        break;
            //    }
            //}

            //if (notFound)
            //    activeDoc.ConcreteItems.Add(Concrete.GetDeepCopy());

            //notFound = true;

            //foreach (var mat in activeDoc.SteelItems)
            //{
            //    if (mat.Name == Steel.Name)
            //    {
            //        notFound = false;
            //        break;
            //    }
            //}

            //if (notFound)
            //    activeDoc.DefinedMaterials.SteelItems.Add(Steel.GetDeepCopy());

        }

        #endregion        

        #region Properties
        #endregion
        
        #region Event Handlers

        #region ToolStrip Items

        private void cbxMainForceUint_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.doc.ForceUnit = (eForceUints)Enum.Parse(typeof(eForceUints), cbxWindowsForceUnit.Text);

            }
            catch (NullReferenceException)
            {
                //deos nothing but stops interuption.
            }
            this.ActiveControl = doc.ModelForm; 
        }

        private void cbxMainLengthUint_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                doc.LengthUnit = (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cbxWindowsLengthUnit.Text);
            }
            catch (NullReferenceException)
            {
                //does nothing but stops interuption.
            }
            this.ActiveControl = doc.ModelForm; 
        }
        #endregion

        private void eMainForm_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
                return;
            this.doc =  ((eModelForm)this.ActiveMdiChild).Document;
        }

        void ActiveMdiChild_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }


        #endregion

        #region Toolbar Events
       

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            helpToolStripMenuItem_Click(sender, e);
        }

        private void tlbtnZoomIn_Click(object sender, EventArgs e)
        {

        }

        private void tlbtnZoomOut_Click(object sender, EventArgs e)
        {

        }

        private void tlbtnPan_Click(object sender, EventArgs e)
        {
            if (doc.ModelForm.Cursor == Cursors.Default)
                doc.ModelForm.Cursor = Cursors.No;
            else
                doc.ModelForm.Cursor = Cursors.Default;
            doc.Model.Layers.IsPan = !doc.Model.Layers.IsPan;
        }
        #endregion

        #region Menu Bar

        #region File
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length >= 1)
            {
                this.MdiChildren[0].Close();
            }

            if (this.MdiChildren.Length == 0)
            {
                eNewModelDialog nm = new eNewModelDialog();

                if (nm.ShowDialog() == DialogResult.OK)
                {
                    doc = new eDocument(nm.StructureType, nm.LengthUnit, nm.ForceUnit);
                    doc.ModelForm = new eModelForm(doc);
                    eStructureType s = nm.StructureType;

                    switch (s)
                    {
                        case eStructureType.Beam:
                            eNewBeamDialog nb = new eNewBeamDialog(doc);
                            if (nb.ShowDialog() == DialogResult.OK)
                            {
                                cbxWindowsForceUnit.Enabled = cbxWindowsLengthUnit.Enabled = true;
                                doc.ModelForm.MdiParent = this;
                                doc.ModelForm.Show();
                                cbxWindowsForceUnit.SelectedItem = doc.ForceUnit;
                                cbxWindowsLengthUnit.SelectedItem = doc.LengthUnit;
                            }
                            break;
                        case eStructureType.Column:
                            eNewColumnDialog nc = new eNewColumnDialog(doc);
                            if (nc.ShowDialog() == DialogResult.OK)
                            {
                                cbxWindowsForceUnit.Enabled = cbxWindowsLengthUnit.Enabled = true;
                                doc.ModelForm.MdiParent = this;
                                doc.ModelForm.Show();
                                cbxWindowsForceUnit.SelectedItem = doc.ForceUnit;
                                cbxWindowsLengthUnit.SelectedItem = doc.LengthUnit;
                            }
                            InitializeGUIForColumn();
                            break;
                        case eStructureType.Footing:
                            eNewFootingDialog nf = new eNewFootingDialog(doc);
                            if (nf.ShowDialog() == DialogResult.OK)
                            {
                                cbxWindowsForceUnit.Enabled = cbxWindowsLengthUnit.Enabled = true;
                                doc.ModelForm.MdiParent = this;
                                doc.ModelForm.Show();
                                cbxWindowsForceUnit.SelectedItem = doc.ForceUnit;
                                cbxWindowsLengthUnit.SelectedItem = doc.LengthUnit;
                            }
                            break;
                        case eStructureType.Slab:
                            eNewSlabDialog ns = new eNewSlabDialog(doc);
                            if (ns.ShowDialog() == DialogResult.OK)
                            {
                                cbxWindowsForceUnit.Enabled = cbxWindowsLengthUnit.Enabled = true;
                                doc.ModelForm.MdiParent = this;
                                doc.ModelForm.Show();
                                cbxWindowsForceUnit.SelectedItem = doc.ForceUnit;
                                cbxWindowsLengthUnit.SelectedItem = doc.LengthUnit;
                            }
                            break;
                    }

                    if (doc.ModelForm != null)
                    {
                        doc.ModelForm.WindowState = FormWindowState.Normal;
                        doc.ModelForm.WindowState = FormWindowState.Maximized;
                    }
                }
            }
        }

        private void InitializeGUIForColumn()
        {
            runAnalysisToolStripMenuItem.Enabled = false;
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eDocument openedDoc = eDocument.Open();
            if (openedDoc != null)
            {
                doc = openedDoc;
                doc.ModelForm.MdiParent = this;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (doc != null)
                doc.Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (doc != null)
                doc.SaveAs();
        }
        #endregion

        #region Edit
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region View

        #endregion

        #region Tools

        #endregion

        #region Define/Modify
        private void materialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eDefineMaterialDialog dm = new eDefineMaterialDialog(doc);

            dm.ShowDialog();
        }
        #endregion

        #region Design
        private void runDesingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (doc != null)
            {
                doc.Model.Design();
                doc.ModelForm.Invalidate();
                doc.IsSaved = false;
            }
        }
        #endregion

        #region Display 
        private void showInputDrawingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.doc == null)
                return;
            eLayersDialog layerDlg = new eLayersDialog(doc.Model.Layers);
            layerDlg.ShowDialog();
            
            this.doc.IsSaved = false;
        }
        #endregion

        #region Window
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
                return;
            else
                this.ActiveMdiChild.Close();
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
                f.Close();
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void tileHorizontatlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVerticalyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
}
        #endregion

        #region Help
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #endregion

        #region ToolBars
       
        #region Standard
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem_Click(sender, e);
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem_Click(sender, e);
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem_Click(sender, e);
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem_Click(sender, e);
        }

       
        #endregion

        #region Editing

        #endregion

        #region View

        #endregion

        #region Drawing
        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void outToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void panToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void jointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (doc == null || doc.Beam == null)
                return;
            eAssignJointDialog ajd = new eAssignJointDialog(doc);

            if (ajd.ShowDialog() == DialogResult.OK)
            {
                foreach (var v in doc.Beam.Joints)
                {
                    if (v.IsSelected)
                    {
                        if (ajd.JointTpe == eJointType.Continious)
                            if (v == doc.Beam.Joints[0] || v == doc.Beam.Joints[doc.Beam.Joints.Count - 1])
                                v.Joint.Type = eJointType.Free;
                            else
                                v.Joint.Type = eJointType.Continious;
                        else if (ajd.JointTpe == eJointType.VerticalRoller)
                            if (v == doc.Beam.Joints[0] || v == doc.Beam.Joints[doc.Beam.Joints.Count - 1])
                                v.Joint.Type = eJointType.VerticalRoller;
                            else
                                v.Joint.Type = eJointType.VerticalGuidedRoller;
                        else
                            v.Joint.Type = ajd.JointTpe;

                        v.Joint.SupportWidth = ajd.SupportWidth;
                    }
                }
            }

            doc.ModelForm.Invalidate();
        }

        private void jointLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (doc == null || doc.Beam == null)
                return;
            eAssignBeamJointLoadDialog ajld = new eAssignBeamJointLoadDialog(doc);

            if (ajld.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var v in doc.Beam.Joints)
                {
                    if (v.IsSelected)
                    {
                        eLoad l;

                        if (ajld.IsForce)
                        {
                            l = new eConcentratedForce(ajld.Magnitude, v.Joint, ajld.ActionType);
                            v.AddLoad((eConcentratedForce)l);
                        }
                        else
                        {
                            l = new eConcentratedMoment(ajld.Magnitude, v.Joint, ajld.ActionType);
                            v.AddLoad((eConcentratedMoment)l);
                        }
                    }
                }
                doc.ModelForm.Invalidate();
            }
        }

        private void memberLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eAssignBeamLoadDialog abl = new eAssignBeamLoadDialog(doc);

            if (abl.ShowDialog() == DialogResult.OK)
            {
                foreach (var memb in doc.Beam.Members)
                {
                    if (memb.IsSelected)
                    {
                        if (abl.AssignOption == eAssignOptions.Remove || abl.AssignOption == eAssignOptions.Replace)
                        {
                            memb.DeleteAllLoads();
                        }
                        if (abl.AssignOption == eAssignOptions.Add || abl.AssignOption == eAssignOptions.Replace)
                        {
                            double factor = abl.AbsoluteDistance ? 1.0 : memb.Member_Analysis.Length;

                            switch (abl.LoadType)
                            {
                                case eMemberLoadTypes.ConcentratedForce:
                                    memb.AddLoad(abl.Magnitude, abl.Start * factor, abl.ActionType);
                                    break;
                                case eMemberLoadTypes.ConcentratedMoment:
                                    memb.AddLoad(abl.Magnitude, abl.Start * factor, abl.ActionType, false);
                                    break;
                                case eMemberLoadTypes.TriangularlyDistributed:
                                    memb.AddLoad(abl.Magnitude, abl.Start * factor, abl.End * factor, abl.Orientation, abl.ActionType);
                                    break;
                                case eMemberLoadTypes.UniformlyDistributed:
                                    memb.AddLoad(abl.Magnitude, abl.Start * factor, abl.End * factor, abl.ActionType);
                                    break;
                                case eMemberLoadTypes.TrapizoidallyDistributed:
                                    double tri_Mag, rect_Mag;
                                    eTriangularLoadOrientation ornttion;

                                    if (abl.Magnitude_Left > abl.Magnitude_Right)
                                    {
                                        ornttion = eTriangularLoadOrientation.RightToLeft;
                                        tri_Mag = abl.Magnitude_Left - abl.Magnitude_Right;
                                        rect_Mag = abl.Magnitude_Right;
                                    }
                                    else
                                    {
                                        ornttion = eTriangularLoadOrientation.LeftToRight;
                                        tri_Mag = abl.Magnitude_Right - abl.Magnitude_Left;
                                        rect_Mag = abl.Magnitude_Left;
                                    }

                                    memb.AddLoad(tri_Mag, rect_Mag, ornttion, abl.Start * factor, abl.End * factor, abl.ActionType);
                                    break;
                            }
                        }
                    }
                }
            }

            doc.ModelForm.Invalidate();
        }

        private void runAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (doc == null)
                return;
            if (doc.Beam.Beam_Analysis.CanBeAnalysed)
            {
                doc.Beam.Beam_Analysis.Analyze();
                doc.Beam.ShowSFD();
                doc.Beam.ShowBMD();

                doc.ModelForm.Locked = true;
                doc.ModelForm.ObjFoundBelowClickPt = true;               
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (doc == null || doc.Beam == null)
                return;
            doc.Beam.StartDrawingMember(1.0, eJointType.Pin);
        }

        private void unlockModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("This will erase all analysis data. \n \nDo you want to continue unlocking?", "Unlocking the model", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == System.Windows.Forms.DialogResult.Yes)
            {
                doc.ModelForm.Locked = false;
                doc.Beam.RemoveDiagrams();
                doc.Beam.DestroyDetailing();
                doc.Beam.Beam_Analysis.AnaysisCompleted = false;
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (doc == null || doc.Beam == null)
                return;
            doc.Beam.StopDrawingMemeber();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (doc == null)
                return;
            eOptionsDialog od = new eOptionsDialog(doc);

            if (od.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                switch (doc.ModelType)
                {
                    case eStructureType.Beam:

                        break;
                    case eStructureType.Column:
                        break;
                    case eStructureType.Footing:
                        break;
                    case eStructureType.Slab:
                        break;
                }
            }

            doc.ModelForm.Invalidate();
        }

        private void defineBeamSectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (doc == null || doc.Beam == null)
                return;
            eDefineBeamSectionDialog dsd = new eDefineBeamSectionDialog(doc);

            dsd.ShowDialog();
        }

        private void memberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (doc == null || doc.Beam == null)
                return;
            eDefineBeamSectionDialog abs = new eDefineBeamSectionDialog(doc, true);

            if (abs.ShowDialog() == DialogResult.OK)
            {
                foreach (var v in doc.Beam.Members)
                {
                    if (v.IsSelected)
                    {
                        v.Member_Design.Section = abs.SelectedSection;
                    }
                }

                doc.Beam.ShowSectionNames();
                doc.ModelForm.Invalidate();
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (doc == null)
                return;
            List<object> objects = new List<object>();
            bool found = false;

            foreach (var v in doc.Beam.Members)
            {
                if (v.IsSelected)
                {
                    found = true;
                    //mainPropertyGrid.Visible = true;
                    objects.Add(v.Member_Design);
                }
            }

            //if (found)
               // mainPropertyGrid.SelectedObjects = objects.ToArray();
        }

        private void fitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (doc != null)
            {
                try
                {
                    doc.Beam.ZoomFit();
                    doc.Footing.ZoomFit();
                }
                catch (Exception) { }
            }
        }

        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("D:\\Accademics\\Books\\#References\\Codes and Standards\\EBCS&ESCP.pdf");
        }

        private void autoCADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (doc == null)
                return;
            //eExportToAutoCAD2007Dialog ex = new eExportToAutoCAD2007Dialog(doc.ModelType);
            //ex.ShowDialog();
        }

        private void dimensionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (doc == null || doc.Beam == null)
                return;
            if (doc.ModelType == eStructureType.Beam)
            {
                if (dimensionsToolStripMenuItem.Checked)
                    doc.Beam.ShowLoadDimensions();
                else
                    doc.Beam.HideLoadDimensions();
                doc.ModelForm.Invalidate();
            }
        }

        private void showSectionNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (doc == null || doc.Beam == null)
                return;
            if (doc.ModelType == eStructureType.Beam)
            {
                if (showSectionNamesToolStripMenuItem.Checked)
                    doc.Beam.ShowSectionNames();
                else
                    doc.Beam.HideSectionNames();
                doc.ModelForm.Invalidate();
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (doc != null)
            {
                doc.ModelForm.Close();
                doc.ModelForm = null;
                doc = null;
            }
        }

        private void addMembersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void inputOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void footingInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (doc != null && doc.ModelType == eStructureType.Footing)
            {
                eNewFootingDialog nf = new eNewFootingDialog(doc, false);
                nf.ShowDialog();
            }
        }

        private void columnInputsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eNewColumnDialog nc = new eNewColumnDialog(doc);
            if (nc.ShowDialog() == DialogResult.OK)
            {
                cbxWindowsForceUnit.Enabled = cbxWindowsLengthUnit.Enabled = true;
                doc.ModelForm.MdiParent = this;
                doc.ModelForm.Show();
                cbxWindowsForceUnit.SelectedItem = doc.ForceUnit;
                cbxWindowsLengthUnit.SelectedItem = doc.LengthUnit;
            }
        }
    }
}
