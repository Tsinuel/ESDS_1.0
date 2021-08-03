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
using ESADS.EGraphics;
using ESADS.Mechanics.Analysis;
using ESADS.Mechanics.Design;
using ESADS.Mechanics.Design.Beam;
using ESADS.EGraphics.Beam;
using ESADS.Mechanics.Analysis.Beam;

namespace ESADS.GUI
{
    public partial class eNewBeamDialog : Form
    {
        private eGBeam beam;
        private eDocument document;
        private eBeamSection defaultSection;

        public eGBeam Beam
        {
            get
            {
                return this.beam;
            }
        }

        public eDocument Document
        {
            get
            {
                return this.document;
            }
        }

        public eBeamSection DefaultSection
        {
            get
            {
                return this.defaultSection;
            }
        }

        public eNewBeamDialog(eDocument document)
        {
            this.beam = document.Beam;
            this.document = document;

            InitializeComponent();
            InitializeCustomComponents();
            
        }

        private void InitializeCustomComponents()
        {
            eUtility.FillComboBox<eJointType>(cmbxDefaultSupport, true);
            cmbxDefaultSupport.Items.Remove(eJointType.Hinge);
            cmbxDefaultSupport.Items.Remove(eJointType.VerticalGuidedRoller);
            cmbxDefaultSupport.Items.Remove(eJointType.VerticalRoller);
            cmbxDefaultSupport.Items.Remove(eJointType.Free);

            cmbxDefaultSupport.SelectedItem = eJointType.Pin;

            ntxtLength.DoubleValue = eUtility.Convert(3.0, eLengthUnits.m, document.LengthUnit);
            ntxtNumbOfMembers.IntValue = 3;
            ntxtColumnWidth.DoubleValue = eUtility.Convert(200, eLengthUnits.mm, document.LengthUnit);

            this.defaultSection = new eBeamSection();

            foreach (var conc in document.Concretes)
                cmbxConcrete.Items.Add(conc.ToString());
            foreach (var stl in document.Steels)
                cmbxSteel.Items.Add(stl.ToString());
            cmbxConcrete.SelectedIndex = 3;
            cmbxSteel.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //doc.ModelForm = new eModelForm();
            eABeam b = new eABeam();
            b.Beam_Design.DefaultSection = this.defaultSection;
            document.Model = new eGBeam(b, document, this.defaultSection);

            if (!this.defaultSection.Equals(document.Beam.Beam_Design.DefinedSections[0]))
                document.Beam.Beam_Design.DefinedSections.Add(this.defaultSection);

            double l = eUtility.Convert(ntxtLength.DoubleValue, document.LengthUnit, eUtility.SLU);

            document.Beam.AddMembers(ntxtNumbOfMembers.IntValue, l, 1.0, (eJointType)Enum.Parse(typeof(eJointType), cmbxDefaultSupport.Text));
            document.Beam.Beam_Analysis.ConsiderSelfWeight = chkbxConsiderSelfWeight.Checked;

            document.Beam.Beam_Design.Concrete = ConcFromName(cmbxConcrete.Text);
            document.Beam.Beam_Design.Steel = SteelFromName(cmbxSteel.Text);
            this.Close();
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

        private void btnDefineDefaultSecn_Click(object sender, EventArgs e)
        {
            eBeamSectionDialog bd = new eBeamSectionDialog(this.document, this.defaultSection);

            if (bd.ShowDialog() == DialogResult.OK)
            {
                this.defaultSection = bd.Section;
            }
        }

        private void btnDefineConcrete_Click(object sender, EventArgs e)
        {
            eDefineMaterialDialog md = new eDefineMaterialDialog(document, eMaterialType.Concrete);

            if (md.ShowDialog() == DialogResult.OK)
            {
                while (cmbxConcrete.Items.Count > 0)
                    cmbxConcrete.Items.RemoveAt(0);

                foreach (var conc in document.Concretes)
                    cmbxConcrete.Items.Add(conc);

                cmbxConcrete.Text = document.Beam.Beam_Design.Concrete.Name;
            }
        }

        private void btnDefineSteel_Click(object sender, EventArgs e)
        {
            eDefineMaterialDialog md = new eDefineMaterialDialog(document, eMaterialType.ReinforcingSteel);

            if (md.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                while (cmbxSteel.Items.Count > 0)
                    cmbxSteel.Items.RemoveAt(0);

                foreach (var stl in document.Steels)
                    cmbxSteel.Items.Add(stl);

                cmbxSteel.Text = document.Beam.Beam_Design.Steel.Name;
            }
        }
        
    }
}
