using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESADS.Code;
using ESADS.EGraphics;
using ESADS.Mechanics;
using ESADS.Mechanics.Analysis;
using ESADS.Mechanics.Design;
using ESADS;

namespace ESADS.GUI
{
    public partial class eModelForm : Form
    {
        #region Custom Fields
        /// <summary>
        /// Holds a value for public property 'Document'.
        /// </summary>
        private eDocument document;
        #endregion

        #region Custom Events
        #endregion

        /// <summary>
        /// Occurs when the window unit is changed by the user.
        /// </summary>
        public event eUnitChangedEventHandler UnitChanged;
        private bool objFoundBelowClickPt = false;
        private bool editingText = false;
        private bool locked;

        #region Constructors
        /// <summary>
        /// Creates new form for a model according to the preferences passed through the parameter.
        /// </summary>
        /// <param name="input">The inputs required for the design of a beam. Some of them may be modified later.</param>
        /// <param name="preferences">The preferences of the user for the whole design process.</param>
        /// <param name="projectInfo">The information summary of the design defined by the user.</param>
        public eModelForm(eDocument document)
        {
            this.document = document;
            InitializeComponent();
        }
        public eModelForm()
        {
            InitializeComponent();
            this.document = new eDocument(eStructureType.Beam, eLengthUnits.mm, eForceUints.KN);
        }
        #endregion

        #region Custom properties

        /// <summary>
        /// Gets the document of the related to this Model form.
        /// </summary>
        public eDocument Document
        {
            get { return document; }
        }

        /// <summary>
        /// Gets or sets the value if a drawing object was found below the last click point.
        /// </summary>
        public bool ObjFoundBelowClickPt
        {
            get
            {
                return objFoundBelowClickPt;
            }
            set
            {
                objFoundBelowClickPt = value;
            }
        }

        /// <summary>
        /// Gets or sets the value if a beam is locked so that the analysis result may not be lost due to changes to the input.
        /// </summary>
        public bool Locked
        {
            get
            {
                return locked;
            }
            set
            {
                locked = value;
            }
        }

        /// <summary>
        /// Gets or sets the value if a drawing text is being edited in a temporary text box.
        /// </summary>
        public bool EditingText
        {
            get
            {
                return editingText;
            }
            set
            {
                editingText = value;
            }
        }
        #endregion

        #region Custom Methods
     


        #endregion

        #region Event Handlers
        private void eModelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DialogResult result;
            //if (!this.document.IsSaved)
            //    result = MessageBox.Show("Do you want to save changes to " + this.document.Inputs.SectionName + "?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //else return;            
            //if (result == DialogResult.Yes)
            //{
            //    this.document.Save();
            //    if (!document.IsSaved)
            //        e.Cancel = true;
            //}
            //else if(result == DialogResult.Cancel)
            //    e.Cancel = true;
        }
        void document_Modified(object sender, eDocumentModifiedEventArgs e)
        {
            this.document.IsSaved = false;
        }
        #endregion     

        /// <summary>
        /// Fires the 'UnitChanged' event.
        /// </summary>
        /// <param name="e">The event argument holding the changes.</param>
        protected void OnUnitChanged(eUnitChangedEventArgs e)
        {
            if (this.UnitChanged != null)
            {
                this.UnitChanged(this, e);
            }
        }

        private void eModelForm_MouseClick(object sender, MouseEventArgs e)
        {
            this.Activate();
        }
    }
}
