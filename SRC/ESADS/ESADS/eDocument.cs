using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using ESADS.Code;
using ESADS.Code.EBCS_1995;
using ESADS.EGraphics;
using ESADS.EGraphics.Beam;
using ESADS.EGraphics.Column;
using ESADS.EGraphics.Footing;
using ESADS.EGraphics.Slab;
using ESADS.GUI;
using ESADS.Mechanics.Design;
namespace ESADS
{
    /// <summary>
    /// Stores all necessary information related to ESADS application.
    /// </summary>
    [Serializable]
    public class eDocument
    {
        #region Fields

        /// <summary>
        /// Holds a value for public property 'Model'.
        /// </summary>
        private eIGModel model;
        /// <summary>
        /// Holds a value for public property 'Concretes'.
        /// </summary>
        private List< eConcrete > concretes;
        /// <summary>
        ///Holds a value for public property 'Steels'.
        /// </summary>
        private List<eSteel> steels;
        /// <summary>
        /// Holds a value for public property 'FileName'.
        /// </summary>
        private string name;
        /// <summary>
        /// Holds a value for public property 'ModelForm'.
        /// </summary>
        [NonSerialized]
        private eModelForm modelForm;    
        /// <summary>
        /// Holds a value for public property 'IsSaved'.
        /// </summary>
        private bool isSaved = false;
        /// <summary>
        /// Holds a value for 'LengthUnit' property.
        /// </summary>
        private eLengthUnits lengthUnit;
        /// <summary>
        /// Holds a value for 'ForceUnit' property.
        /// </summary>
        private eForceUints forceUnit;
        /// <summary>
        /// Holds a value for property 'ModelType'.
        /// </summary>
        private eStructureType modelType;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the graphics object of the model if it is a beam. If not, it returns null.
        /// </summary>
        public eGBeam Beam
        {
            get
            {
                return model as eGBeam;
            }
        }

        /// <summary>
        /// Gets the graphics object of the column.
        /// </summary>
        public eGColumn column
        {

            get
            {
                return model as eGColumn;
            }
        }
        /// <summary>
        /// Gets or sets the length unit used in the document.
        /// </summary>
        public eLengthUnits LengthUnit
        {
            get
            {
                return lengthUnit;
            }
            set
            {
                lengthUnit = value;
            }
        }

        /// <summary>
        /// Gets or sets the force unit used in the document
        /// </summary>
        public eForceUints ForceUnit
        {
            get
            {
                return forceUnit;
            }

            set
            {
                forceUnit = value;
            }
        }

        /// <summary>
        /// Gets or sets the model Form on which the design is done.
        /// </summary>
        public eModelForm ModelForm
        {
            get { return modelForm; }
            set 
            { 
                modelForm = value;
                OnModified();
            }
        }

        /// <summary>
        /// Gets or sets collection of defiend concrete materials.
        /// </summary>
        public List<eConcrete> Concretes
        {
            get { return concretes; }
            set { concretes = value; }
        }

        /// <summary>
        /// Gets or sets collection of defiend steel materials.
        /// </summary>
        public List<eSteel> Steels
        {
            get { return steels; }
            set { steels = value; }
        }
        
        /// <summary>
        /// Gets information related to  model and some user inputs.
        /// </summary>
        public eIGModel Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
                OnModified();
            }
        }

        /// <summary>
        /// Gets the file name of the  document.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnModified();
            }
        }

        /// <summary>
        /// Gets or sets the value indication either the document is saved or not.
        /// </summary>
        public bool IsSaved
        {
            get { return isSaved; }
            set 
            { 
                isSaved = value;
            }
        }

        /// <summary>
        /// Gets the type of model.
        /// </summary>
        public eStructureType ModelType
        {
            get { return modelType; }
        }

        public eGFooting Footing
        {
            get { return model as eGFooting; }
        }
        #endregion   

        /// <summary>
        /// Gets the slab object if the document is created for slab.
        /// </summary>
        public eGSlab Slab
        {
            get
            {
                if (this.modelType == eStructureType.Slab)
                    return this.model as eGSlab;
                else
                    throw new Exception("Cannot retrieve slab object from a document not created for slab");
            }
        }

        #region Constructor

        /// <summary>
        /// Creates un instance of ESADS.eDocument class for a given basic parameters.
        /// </summary>
        /// <param name="modelType">Type of model.</param>
        /// <param name="lengthUnit">Length unit used in the design.</param>
        /// <param name="forceUnit">Force unit used in the design.</param>
        public eDocument(eStructureType modelType, eLengthUnits lengthUnit, eForceUints forceUnit)
        {
            this.modelType = modelType;
            this.lengthUnit = lengthUnit;
            this.forceUnit = forceUnit;
            this.modelForm = new eModelForm(this);

            this.concretes = new List<eConcrete>();
            this.steels = new List<eSteel>();

            Array concs = Enum.GetValues(typeof(eConcreteGrade));
            Array steels = Enum.GetValues(typeof(eSteelGrade));

            foreach (var v in concs)
            {
                if (v.ToString() != eConcreteGrade.Custom.ToString())
                    concretes.Add(new eConcrete((eConcreteGrade)v));
            }
            foreach (var v in steels)
            {
                if (v.ToString() != eSteelGrade.Custom.ToString())
                    this.steels.Add(new eSteel((eSteelGrade)v));
            }

            if (modelType == eStructureType.Slab)
            {
                this.model = new eGSlab(this);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves the document in the prefvious location.
        /// </summary>
        public void Save()
        {
            //if it has no file name it will save the document with the new name.
            if (name == null)
            {
                SaveAs();
                return;
            }
            try
            {
                Stream stream = new FileStream(name, FileMode.Create, FileAccess.Write);
                BinaryFormatter writer = new BinaryFormatter();
                writer.Serialize(stream, this);
                this.isSaved = true;
            }
            catch (IOException)
            {
                //Does nothing but stops interuption
            }

        }

        /// <summary>
        /// Saves the document in the specified location.
        /// </summary>
        public void SaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "|*.sdd*";
            //saveFileDialog.FileName = input.SectionName+".sdd";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                this.name = saveFileDialog.FileName;
            else return;
            try
            {
                Stream stream = new FileStream(this.name, FileMode.Create, FileAccess.Write);
                BinaryFormatter writer = new BinaryFormatter();
                writer.Serialize(stream, this);
                this.isSaved = true;
            }
            catch (IOException)
            {
                //Does nothing but stops interuption.
            }
        }

        /// <summary>
        /// Firs the the Document Modified event.
        /// </summary>
        private void OnModified()
        { 
            if (Modified != null)
            {
                Modified(this, new eDocumentModifiedEventArgs());
            }
        }

        /// <summary>
        /// Opnes pre-existing document from the specified location.
        /// </summary>
        /// <returns>Returns the document if it exists or in correct format and returns null if it is not in the correct format or doesn't exist.</returns>
        public static eDocument Open()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "|*.sdd*";
        Lable:
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream stream = new FileStream(openDialog.FileName, FileMode.Open, FileAccess.Read);
                    BinaryFormatter formater = new BinaryFormatter();
                    eDocument doc = (eDocument)formater.Deserialize(stream);
                    doc.ModelForm = new eModelForm(doc);
                    doc.IsSaved = true;
                    return doc;
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("The specified format cannot be opened in ESADS application", "Invalid File Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto Lable;
                }
                catch (SerializationException)
                {
                    MessageBox.Show("The specified File format cannot be opened in ESADS application.", "File Format Not Supported!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto Lable;
                }
                catch (IOException)
                {
                    //Does nothing but stops interuption.                 
                }
            }
            return null;
        }

        #endregion

        #region Events
        /// <summary>
        /// Occures when the document is modified.
        /// </summary>
        [field: NonSerialized]
        public event eDocumentModifiedEventHandler Modified;
        #endregion
    }
}
