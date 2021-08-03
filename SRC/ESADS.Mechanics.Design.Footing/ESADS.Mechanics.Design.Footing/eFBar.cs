using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS;
using ESADS.Code;
using ESADS.Code.EBCS_1995;
using ESADS.Mechanics.Design;

namespace ESADS.Mechanics.Design.Footing
{
    public struct eFBar
    {
        private string name;
        private double diam;
        double d;
        private double cover;
        private double[] lengths;
        private double fDim;
        private double provSpacing;
        private double calcSpacing;
        private eAnchorageType achorType;
        private double lbRation;
        private double lAvail;
        private int number;
        private double displayS;
        private double lengthIncrement;

        public eFBar(double diam, double fDim, eDFooting footing ,double lengthIncrement)
        {
            this.diam = diam;
            this.fDim = fDim;
            this.lbRation = footing.Length / footing.Width;
            this.d = 0;
            this.cover = footing.Cover; ;
            this.name = "";
            this.provSpacing = 0;
            this.achorType = eAnchorageType.Straight;
            this.lengths = new double[3] { 0, fDim - 2 * cover, 0 };
            this.lAvail = 0;
            this.number = 0;
            this.calcSpacing = 0;
            this.displayS = 0;
            this.lengthIncrement = lengthIncrement;
        }

        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        public double LAvailable
        {
            get { return lAvail; }
            set { lAvail = value; }
        }

        public eAnchorageType AnchorType
        {
            get { return achorType; }
            set { achorType = value; }
        }
        public double InBandSpacing
        {
            get { return GetBandSapacing(); }
        }

        public double OutBandSpacing
        {
            get { return GetOutSideBandSpacing(); }
        }

        public double ProvSpacing
        {
            get { return provSpacing; }
            internal set
            {
                provSpacing = value;
            }
        }

        public double CalcSpacing
        {
            get { return calcSpacing; }
            internal set { calcSpacing = value; }
        }
        public double EffD
        {
            get { return d; }
            set { d = value; }
        }
        public double[] Legths
        {
            get { return lengths; }
            set { lengths = value; }
        }

        public double Length
        {
            get
            {
                return lengths.Sum();
            }
        }


        public double FDim
        {
            get { return fDim; }
        }

        
        public double Diameter
        {
            get
            {
                return diam;
            }
            set
            {
                diam = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }


        public double DisplaySpacing
        {
            get
            {
                return displayS;
            }
        }

        /// <summary>
        /// returns reinforcement distribution factor in the band
        /// </summary>
        /// <returns></returns>
        private double GetBandSapacing()
        {
            return ProvSpacing * 2 / (lbRation + 1);
        }

        /// <summary>
        /// returns reinforcement distribution outside the band
        /// </summary>
        /// <param name="bspacing"></param>
        /// <returns></returns>
        private double GetOutSideBandSpacing()
        {
            return provSpacing * (lbRation + 1) / 2;
        }

        public void SetEffD(double D)
        {
            d = D - cover - diam / 2;
        }

        public void SetEffD(double D, double botBarDiam)
        {
            d = D - cover - diam / 2 - botBarDiam;
        }

        public void GenerateBarLengths(double hookLength, bool overriedMinValues = false)
        {
            lengths = new double[3];
            lengths[1] = fDim - 2 * cover;
            if (achorType != eAnchorageType.Straight || overriedMinValues)
                lengths[0] = lengths[2] = Math.Ceiling(hookLength / lengthIncrement) * lengthIncrement;
            if (overriedMinValues && hookLength != 0)
                this.achorType = eAnchorageType.Hook90;
        }

        public static int GetNumberOfBars(double distL, double spacing)
        {
            return (int)Math.Ceiling(distL / spacing) + 1;
        }

        internal void FillBarNumber(double distL)
        {
            number = (int)Math.Ceiling((distL - this.diam) / provSpacing + 1);
            displayS = (distL - this.diam) / (number - 1);
        }
      
        
    }
}
