using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS;
using ESADS.EGraphics;
using ESADS.Mechanics.Design.Footing;
namespace ESADS.EGraphics.Footing
{
    public abstract class eFDrawing
    {

        protected eDFooting f;
        protected List<eIDrawing> dwgs;
        protected eRectangle contRect;
        protected eLayers layers;
        protected eLengthUnits lengthUnit;
        protected eForceUints forceUnit;
        protected int precision = 2;
        /// <summary>
        /// Dimension line extension factor that multiplies the length to be measured.
        /// </summary>
        protected float dimF = 1f / 6f;
        protected float arrowsiz = 0.02f;

        protected eDrawingStage dwgStage;

        public eDrawingStage DrawingStage
        {
            get { return dwgStage; }
            set { dwgStage = value; }
        }

        internal List<eIDrawing> Dwgs
        {
            get { return dwgs;}
            set { dwgs = value; }
        }

        public int Precision
        {
            get { return precision; }
            set { precision = value; }
        }

        public eDFooting Footing
        {
            get { return f; }
        }

        public eLayers Layers
        {
            get { return layers; }
            set { layers = value; }
        }

        public eLengthUnits LengthUnit
        {
            get { return lengthUnit; }
            set
            {
                lengthUnit = value;
            }
        }

        public eForceUints ForceUnit
        {
            get
            {
                return forceUnit;
            }
            set { forceUnit = value; }
        }

        public eRectangle ContRect
        {
            get { return contRect; }
        }

        protected abstract void AddColumn();

        protected abstract void AddFootingExterior();

        protected abstract void AddDimension();

        public abstract void DrawModelingStage();

        public abstract void DrawDetailingStage();

        protected abstract void AddBars();

        protected abstract void CreateContainingRectangle();

        protected abstract void AddLoads();
    }
}
