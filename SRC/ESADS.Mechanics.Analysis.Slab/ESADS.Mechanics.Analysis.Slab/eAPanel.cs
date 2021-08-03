using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Mechanics.Analysis;
using ESADS.Code;
using ESADS.Code.EBCS_1995;

namespace ESADS.Mechanics.Analysis.Slab
{
    public abstract class eAPanel
    {
        protected double lx;
        protected double ly;
        protected double mxs3;
        protected double mxs4;
        protected double mxf;
        protected double myf;
        protected eAPanel topPanel;
        protected eAPanel bottomPanel;
        protected eAPanel leftPanel;
        protected eAPanel rightPanel;
        protected double height;
        protected double width;
        protected double qk;
        protected double gk;
        protected double pd;
        protected ePoint tL;
        protected ePoint tR;
        protected ePoint bR;
        protected ePoint bL;
        protected ePanelAction panelAction;
        protected eSlabSupports supportCondition;
        protected eLoadCombination combo;
        protected eOrientation oreintation;
        protected eASBMember topBeam;
        protected eASBMember bottomBeam;
        protected eASBMember leftBeam;
        protected eASBMember rightBeam;
        protected List<eASLoad> loads;
        protected eCalculateDesignLoadAs calculatePdAs;

        public eAPanel(ePoint tL, ePoint tR, ePoint bR, ePoint bL)
        {
            this.tL = tL;
            this.tR = tR;
            this.bR = bR;
            this.bL = bL;
            FillGeometricData();
        }

        public List<eASLoad> Loads
        {
            get
            {
                return loads;
            }
        }

        public eASBMember TopBeam
        {
            get
            {
                return topBeam;
            }
            set
            {
                topBeam = value;
            }
        }

        public eASBMember BottomBeam
        {
            get
            {
                return bottomBeam;
            }
            set
            {
                bottomBeam = value;
            }
        }

        public eASBMember LeftBeam
        {
            get
            {
                return leftBeam;
            }
            set
            {
                leftBeam = value;
            }
        }

        public eASBMember RightBeam
        {
            get
            {
                return rightBeam;
            }
            set
            {
                rightBeam = value;
            }
        }

        public eOrientation Oreintation
        {
            get { return oreintation; }
            set { oreintation = value; }
        }

        public eSlabSupports SupportCondition
        {
            get { return supportCondition; }
            set { supportCondition = value; }
        }

        public double Lx
        {
            get { return lx; }
        }

        public eLoadCombination Combo
        {
            get
            {
                return combo;
            }
            set
            {
                combo = value;
            }
        }

        public double Ly
        {
            get { return ly; }
        }

        public ePoint TL
        {
            get
            {
                return tL;
            }
            set
            {
                tL = value;
                FillGeometricData();
            }

        }

        public ePoint TR
        {
            get
            {
                return tR;
            }
            set
            {
                tR = value;
                FillGeometricData();
            }
        }

        public ePoint BR
        {
            get
            {
                return bR;
            }
            set
            {
                bR = value;
                FillGeometricData();
            }
        }

        public ePoint BL
        {
            get
            {
                return bL;
            }

            set
            {
                bL = value;
                FillGeometricData();
            }
        }

        public double Width
        {
            get { return width; }
        }

        public double Height
        {
            get { return height; }
        }

        public double Mxs3
        {
            get
            {
                return mxs3;
            }
            internal set { mxs3 = value; }
        }

        public double Mxs4
        {
            get { return mxs4; }
            internal set { mxs4 = value; }
        }

        public double Mxf
        {
            get { return mxf; }
        }

        public double Myf
        {
            get { return myf; }
        }

        public eAPanel TopPanel
        {
            get
            {
                return topPanel;
            }
            set
            {
                topPanel = value;
            }
        }

        public eAPanel BottomPanel
        {
            get
            {
                return bottomPanel;
            }
            set
            {
                bottomPanel = value;
            }
        }

        public eAPanel LeftPanel
        {
            get
            {
                return leftPanel;
            }
            set
            {
                leftPanel = value;
            }

        }

        public eAPanel RightPanel
        {
            get
            {
                return rightPanel;
            }
            set
            {
                rightPanel = value;
            }
        }

        public eAPanel P1
        {
            get
            {
                if (oreintation == eOrientation.Horizontal)
                    return leftPanel;
                else
                    return bottomPanel;
            }
        }

        public eAPanel P2
        {
            get
            {
                if (oreintation == eOrientation.Horizontal)
                    return rightPanel;
                else
                    return topPanel;
            }
        }

        public eAPanel P3
        {
            get
            {
                if (oreintation == eOrientation.Horizontal)
                    return topPanel;
                else
                    return leftPanel;
            }
        }

        public eAPanel P4
        {
            get
            {
                if (oreintation == eOrientation.Horizontal)
                    return bottomPanel;
                else
                    return rightPanel;
            }
        }

        public eASBMember B1
        {
            get
            {
                if (oreintation == eOrientation.Horizontal)
                    return leftBeam;
                else
                    return bottomBeam;
            }
        }

        public eASBMember B2
        {
            get
            {
                if (oreintation == eOrientation.Horizontal)
                    return rightBeam;
                else
                    return topBeam;
            }
        }

        public eASBMember B3
        {
            get
            {
                if (oreintation == eOrientation.Horizontal)
                    return topBeam;
                else
                    return leftBeam;
            }
        }

        public eASBMember B4
        {
            get
            {
                if (oreintation == eOrientation.Horizontal)
                    return bottomBeam;
                else
                    return rightBeam;
            }
        }

        public bool HasTopBeam
        {
            get
            {
                return topBeam != null;
            }
        }

        public bool HasBottomBeam
        {
            get
            {
                return bottomBeam != null;
            }
        }

        public bool HasRightBeam
        {
            get
            {
                return rightBeam != null;
            }
        }

        public bool HasLeftBeam
        {
            get
            {
                return leftBeam != null;
            }
        }

        private void FillGeometricData()
        {
            this.lx = GetLx();
            this.ly = GetLy();
            this.height = GetHeight();
            this.width = GetWidth();
            this.panelAction = ly / lx > 2 ? ePanelAction.OneWay : ePanelAction.TwoWay;
            this.oreintation = Math.Round(lx, 12) == Math.Round(height, 12) ? eOrientation.Horizontal : eOrientation.Vertical;
        }

        protected abstract void CalculateMoments();

        private void AdjustSpanMoment()
        {
        }

        private void FillSupportCondition()
        {
            if (P1 != null && P2 != null && P3 != null && P4 != null)
                supportCondition = eSlabSupports.Type1;
            else if (P1 != null && P2 == null && P3 != null && P4 != null)
                supportCondition = eSlabSupports.Type2;
            if (P1 != null && P2 != null && P3 != null && P4 == null)
                supportCondition = eSlabSupports.Type3;
            else if (P1 == null && P2 != null && P3 != null && P4 == null)
                supportCondition = eSlabSupports.Type4;
            if (P1 == null && P2 == null && P3 != null && P4 != null)
                supportCondition = eSlabSupports.Type5;
            if (P1 != null && P2 != null && P3 == null && P4 == null)
                supportCondition = eSlabSupports.Type6;
            else if (P1 == null && P2 == null && P3 != null && P4 == null)
                supportCondition = eSlabSupports.Type7;
            if (P1 != null && P2 == null && P3 == null && P4 == null)
                supportCondition = eSlabSupports.Type8;
            else if (P1 == null && P2 == null && P3 == null && P4 == null)
                supportCondition = eSlabSupports.Type9;
        }

        private double GetLx()
        {
            return Math.Min(tL.Y - bL.Y, tR.X - tL.X);
        }

        private double GetLy()
        {
            return Math.Max(tL.Y - bL.Y, tR.X - tL.X);
        }

        private double GetHeight()
        {
            return tL.Y - bL.Y;
        }

        private double GetWidth()
        {
            return tR.X - tL.X;
        }

        internal void CalculatePd(eLoadCombination comb, eCalculateDesignLoadAs calcAs)
        {
            pd = 0;
            double AiPiSum = 0, AtP = 0, AiViSum = 0, AtV = 0;
            for (int i = 0; i < loads.Count; i++)
            {
                if (loads[i].ActionType == eActionType.Permanent && loads[i].LoadType == eSlabLoadTypes.AreaLoad)
                {
                    AiPiSum += loads[i].GetTotalLoadOn(this);
                    AtP += (loads[i] as eSAreaLoad).GetAreaOn(this);
                }

                if (loads[i].ActionType == eActionType.Variable && loads[i].LoadType == eSlabLoadTypes.AreaLoad)
                {
                    AiViSum += loads[i].GetTotalLoadOn(this);
                    AtV += (loads[i] as eSAreaLoad).GetAreaOn(this);
                }
            }
            pd = comb.PermanentLoadFactor * AiPiSum / AtV + comb.VariableLoadFactor * AiPiSum / AtP;
        }
        
    }
}
