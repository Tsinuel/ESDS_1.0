using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ESADS.GUI.Controls
{
    public enum eTypeOfStructure
    {
        Beam,
        Column, 
        Slab,
        Footing
    }

    public partial class eStructureTemplate : Button
    {
        private eTypeOfStructure typeOfStructure;

        public eStructureTemplate()
        {
            InitializeComponent();
            this.Text = typeOfStructure.ToString();
        }

        public override string Text
        {
            get
            {
                return typeOfStructure.ToString();
            }
        }

        public eTypeOfStructure TypeOfStructure
        {
            get { return typeOfStructure; }
            set
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(eStructureTemplate));

                this.Text = value.ToString();
                typeOfStructure = value;
                //this.Imag
                this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.Image = ((System.Drawing.Image)(resources.GetObject("$this.Image")));
            }
        }
    }
}
