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
using ESADS.GUI.Controls;

namespace ESADS.GUI
{
    public partial class eNewModelDialog : Form
    {  
        #region Feilds
        private eStructureType structureType;
        private string[,] projectInfo;
        private eLengthUnits lengthUnit;
        private eForceUints forceUnit;
        #endregion

        #region Constructors
        public eNewModelDialog()
        {
            InitializeComponent();
            InitializeCustomComponent();
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets the structure type chosen by the user
        /// </summary>
        public eStructureType StructureType
        {
            get
            {
                return this.structureType;
            }
        }

        /// <summary>
        /// Gets the project information of the chosen structure
        /// </summary>
        public string[,] ProjectInformation
        {
            get
            {
                return this.projectInfo;
            }
        }

        /// <summary>
        /// Gets the length unit chosen by the user.
        /// </summary>
        public eLengthUnits LengthUnit
        {
            get
            {
                return this.lengthUnit;
            }
        }

        /// <summary>
        /// Gets the force unit chosen by the user.
        /// </summary>
        public eForceUints ForceUnit
        {
            get
            {
                return this.forceUnit;
            }
        }
        #endregion

        #region Methods

        private void InitializeCustomComponent()
        {
            eUtility.FillComboBox<eLengthUnits>(cbxLengthUnit, true);
            eUtility.FillComboBox<eForceUints>(cbxForceUnit, true);

            cbxForceUnit.SelectedItem = eForceUints.KN;
            cbxLengthUnit.SelectedItem = eLengthUnits.m;

            this.projectInfo = new string[11, 2];
            projectInfo[0, 0] = "Company Name";
            projectInfo[1, 0] = "Client Name";
            projectInfo[2, 0] = "Project Name";
            projectInfo[3, 0] = "Model Name";
            projectInfo[4, 0] = "Model Description";
            projectInfo[5, 0] = "Revision Number";
            projectInfo[6, 0] = "Engineer";
            projectInfo[7, 0] = "Checker";
            projectInfo[8, 0] = "Supervisor";
            projectInfo[9, 0] = "Issued Date";
            projectInfo[10, 0] = "Design Code";

            projectInfo[0, 1] = "";
            projectInfo[1, 1] = "";
            projectInfo[2, 1] = "";
            projectInfo[3, 1] = "";
            projectInfo[4, 1] = "";
            projectInfo[5, 1] = "";
            projectInfo[6, 1] = "";
            projectInfo[7, 1] = "";
            projectInfo[8, 1] = "";
            projectInfo[9, 1] = "";
            projectInfo[10, 1] = "";

        }

        #endregion

        #region Event Handlers
        private void btnEditProjectInfo_Click(object sender, EventArgs e)
        {
            ProjectInformationDialog pid = new ProjectInformationDialog(projectInfo);
            //if (pid.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    this.projectInfo = pid.ProjectInformation;
            //}
        }

        private void pbxBeamTemplate_Click(object sender, EventArgs e)
        {
            this.structureType = eStructureType.Beam;
            this.lengthUnit = (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cbxLengthUnit.Text);
            this.forceUnit = (eForceUints)Enum.Parse(typeof(eForceUints), cbxForceUnit.Text);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void pbxColumnTemplate_Click(object sender, EventArgs e)
        {
            this.structureType = eStructureType.Column;
            this.lengthUnit = (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cbxLengthUnit.Text);
            this.forceUnit = (eForceUints)Enum.Parse(typeof(eForceUints), cbxForceUnit.Text);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close(); 
        }

        private void pbxSlabTemplate_Click(object sender, EventArgs e)
        {
            this.structureType = eStructureType.Slab;
            this.lengthUnit = (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cbxLengthUnit.Text);
            this.forceUnit = (eForceUints)Enum.Parse(typeof(eForceUints), cbxForceUnit.Text);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void pbxFootingTemplate_Click(object sender, EventArgs e)
        {
            this.structureType = eStructureType.Footing;
            this.lengthUnit = (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cbxLengthUnit.Text);
            this.forceUnit = (eForceUints)Enum.Parse(typeof(eForceUints), cbxForceUnit.Text);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close(); 
        }
 
        private void pbxBeamTemplate_MouseEnter(object sender, EventArgs e)
        {
            pbxBeamTemplate.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pbxBeamTemplate_MouseLeave(object sender, EventArgs e)
        {
            pbxBeamTemplate.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pbxColumnTemplate_MouseEnter(object sender, EventArgs e)
        {
            pbxColumnTemplate.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pbxColumnTemplate_MouseLeave(object sender, EventArgs e)
        {
            pbxColumnTemplate.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pbxSlabTemplate_MouseEnter(object sender, EventArgs e)
        {
            pbxSlabTemplate.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pbxSlabTemplate_MouseLeave(object sender, EventArgs e)
        {
            pbxSlabTemplate.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pbxFootingTemplate_MouseEnter(object sender, EventArgs e)
        {
            pbxFootingTemplate.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pbxFootingTemplate_MouseLeave(object sender, EventArgs e)
        {
            pbxFootingTemplate.BorderStyle = BorderStyle.FixedSingle;
        }

        #endregion

        private void eNewModelDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pbxBeamTemplate_Click(sender, new EventArgs());
            }
        }
    }
}
