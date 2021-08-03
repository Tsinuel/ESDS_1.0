using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Code;
using ESADS.Code.EBCS_1995;
using ESADS.Mechanics.Design;
using ESADS;

namespace ESADS.Mechanics.Design.Footing
{
    /// <summary>
    /// Represents a footing design component.
    /// </summary>
    public class eDFooting
    {
        #region Fields
        /// <summary>
        /// Represents steel ratio in L direction.
        /// </summary>
        private double lSteelRatio;
        /// <summary>
        /// Represents calculated steel ratio in B direction.
        /// </summary>
        private double bSteelRatio;
        /// <summary>
        /// Represents avarage bearing stress used for punching shear
        /// </summary>
        private double qav;
        /// <summary>
        /// Represets ultimate bearsing stress used for wide beam and flexur.
        /// </summary>
        private double qu;
        /// <summary>
        /// Represets Depth incremetation by which the  gross depth of the footing increases
        /// </summary>
        private double dIncr;
        /// <summary>
        /// Holds a value for publilc property 'Cover'.
        /// </summary>
        private double cover;
        /// <summary>
        /// Holds a value for property 'MaxBarDiam'.
        /// </summary>
        private double maxBarDiam;
        /// <summary>
        /// Holds a value for porperty 'MinBarDiam'.
        /// </summary>
        private double minBarDiam;
        /// <summary>
        /// Holds a value for porperty 'SpacingIncrement'.
        /// </summary>
        private double sIncrmt;
        /// <summary>
        /// Holds a value for property 'LBar'.
        /// </summary>
        private eFBar Lbar;
        /// <summary>
        /// Holds a value for property 'BBar'.
        /// </summary>
        private eFBar Bbar;
        /// <summary>
        /// Holds a value for property 'CompletionState'.
        /// </summary>
        private eDesignCompletionState completionState;
        /// <summary>
        /// Holds a value for property 'Combinations'.
        /// </summary>
        private List<double[]> combs;

        /// <summary>
        /// Holds a value for property 'Length'
        /// </summary>
        private double L;
        /// <summary>
        /// Holds a value for property 'gross depth D'
        /// </summary>
        private double D;
        /// <summary>
        /// Holds a value for property 'effective depth d' 
        /// </summary>
        private double d;

        /// <summary>
        /// Holds a value for property 'Width'
        /// </summary>
        private double B;
        /// <summary>
        /// Holds a value for property 'S'
        /// </summary>
        private eSteel S;
        /// <summary>
        /// Holds a value for property 'ConcreteColumnOrPedestal'
        /// </summary>
        private eConcrete C;
        /// <summary>
        /// Holds a value for property 'p'
        /// </summary>
        private double p;
        /// <summary>
        /// Holds a value for property 'MB'
        /// </summary>
        private double Mb;
        /// <summary>
        /// Holds a value for property 'ML'
        /// </summary>
        private double Ml;
        /// <summary>
        /// Holds a value for property 'ColumnWidth'
        /// </summary>
        private double Lc;
        /// <summary>
        /// Holds a value for property 'ColumnDepth'
        /// </summary>
        private double Bc;
        /// <summary>
        /// Holds a value for property 'ColumnType'
        /// </summary>
        private eColumnType columnType;
        /// <summary>
        /// Holds a value for property 'ColumnDiameter'
        /// </summary>
        private double columnDiam;
        /// <summary>
        /// Holds a value for public property 'MinSpacing'.
        /// </summary>
        private double minSpacing;
        /// <summary>
        /// Holds a value for public property 'MaxSpacing'.
        /// </summary>
        private double maxSpacing;

        /// <summary>
        /// Holds a value for property 'IsDepthGiven'
        /// </summary>
        private bool isDepthGiven;

        /// <summary>
        /// Holds a value for property 'LongSideHorizontal'.
        /// </summary>
        private bool logSideHorizontal;

        /// <summary>
        /// Holds a value for public property 'ConsiderSelefWeight'.
        /// </summary>
        private bool considerSelfWeight;

        /// <summary>
        /// Holds a value for public property 'BarDiam'.
        /// </summary>
        private double barDiam;
        /// <summary>
        /// Holds a value for public property 'Spacing'.
        /// </summary>
        private double spacing;

        private bool useDiameter;

        private bool useSpacing;

        private eExposureType exposureType;

        private bool useMaxHookLength;

        private double hookLength;

        private double lengthIncrement;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Design.eDFooting class from a given basic parameters
        /// </summary>
        /// <param name="L">Length of the footing</param>
        /// <param name="B">Width of the footing</param>
        /// <param name="S">Steel Strength</param>
        /// <param name="C">Concrete material used for the design.</param>
        /// <param name="D">Gross depth of the footiong</param>
        /// <param name="P">Axial load on the footing</param>
        /// <param name="Mb">Moment in B direction</param>
        /// <param name="Ml">Moment in L direction</param>
        public eDFooting(double L, double B, eSteel S, eConcrete C, double D, double P, double Mb, double Ml, bool logSideHorizontal = true)
        {
            this.L = L;
            this.B = B;
            this.S = S;
            this.C = C;
            this.D = D;
            this.p = P;
            this.Mb = Mb;
            this.Ml = Ml;
            this.lSteelRatio = eDetailing.Get_ρ_min(eStructureType.Footing, S.Grade);
            this.bSteelRatio = eDetailing.Get_ρ_min(eStructureType.Footing, S.Grade);
            this.isDepthGiven = true;
            this.dIncr = 1;
            this.sIncrmt = 10;
            this.combs = new List<double[]>();
            this.minBarDiam = 8;
            this.maxBarDiam = 32;
            this.minSpacing = 0;
            this.maxSpacing = double.PositiveInfinity;
            this.logSideHorizontal = logSideHorizontal;
            this.barDiam = 0;
            this.spacing = 0;
            this.hookLength = 0;
            this.useMaxHookLength = false;
            this.lengthIncrement = 10;
            this.useMaxHookLength = true;
        }
        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Design.eDFooting class from a given basic parameters
        /// </summary>
        /// <param name="L">Length of the footing</param>
        /// <param name="B">Width of the footing</param>
        /// <param name="S">Steel Strength</param>
        /// <param name="C">Concrete material used for the design.</param>
        /// <param name="P">Axial load on the footing</param>
        /// <param name="Mb">Moment in B direction</param>
        /// <param name="Ml">Moment in L direction</param>
        public eDFooting(double L, double B, eSteel S, eConcrete C, double P, double Mb, double Ml, bool logSideHorizontal = true)
        {
            this.L = L;
            this.B = B;
            this.S = S;
            this.C = C;
            this.p = P;
            this.Mb = Mb;
            this.Ml = Ml;
            this.lSteelRatio = eDetailing.Get_ρ_min(eStructureType.Footing, S.Grade);
            this.bSteelRatio = eDetailing.Get_ρ_min(eStructureType.Footing, S.Grade);
            this.isDepthGiven = false;
            this.dIncr = 1;
            this.sIncrmt = 10;
            this.combs = new List<double[]>();
            this.minBarDiam = 8;
            this.maxBarDiam = 32;
            this.minSpacing = 0;
            this.maxSpacing = double.PositiveInfinity;
            this.logSideHorizontal = logSideHorizontal;
            this.barDiam = 0;
            this.spacing = 0;
            this.hookLength = 0;
            this.useMaxHookLength = false;
            this.lengthIncrement = 10;
            this.useMaxHookLength = true;
        }
        #endregion

        #region Properties

        public bool UseMaxHookLength
        {
            get { return useMaxHookLength; }
            set { useMaxHookLength = value; }
        }
        /// <summary>
        /// True if diameter is pecified in the drawing.False if diameter is going to be calculated.
        /// </summary>
        public bool UseDiameter
        {
            get { return useDiameter; }
            set { useDiameter = value; }
        }

        public double HookLength
        {
            get { return hookLength; }
            set { hookLength = value; }
        }
        /// <summary>
        /// True if spacing is pecified in the drawing.False if spacing is going to be calculated.
        /// </summary>
        public bool UseSpacing
        {
            get { return useSpacing; }
            set { useSpacing = value; }
        }
        /// <summary>
        /// Gets or set the Exposure type of the footing usually sever exposure in soil.
        /// </summary>
        public eExposureType ExposureType
        {
            get { return exposureType; }
            set { exposureType = value; }
        }
        /// <summary>
        /// Gets or set the only spacing used in the design;
        /// </summary>
        public double Spacing
        {
            get { return spacing; }
            set { spacing = value; }
        }
        /// <summary>
        /// Get or set the only bar diameter used in the design.
        /// </summary>
        public double BarDiam
        {
            get { return barDiam; }
            set { barDiam = value; }
        }
        /// <summary>
        /// Gets or sets whether to includ self wheight in the design or not.
        /// </summary>
        public bool ConsiderSelfWeight
        {
            get { return considerSelfWeight; }
            set { considerSelfWeight = value; }
        }
        /// <summary>
        /// Gets or sets the value indicating whether the loger side is horizonal or not.
        /// </summary>
        public bool LongSideHorizontal
        {
            get { return logSideHorizontal; }
            set { logSideHorizontal = value; }
        }
        /// <summary>
        /// Gets or sets the minimum spacing of bars used in the design.
        /// </summary>
        public double MinSpacing
        {
            get { return minSpacing; }
            set { minSpacing = value; }
        }

        /// <summary>
        /// Gets or sets the maximum spacing of bars used in the design.
        /// </summary>
        public double MaxSpacing
        {
            get { return maxSpacing; }
            set { maxSpacing = value; }
        }
        /// <summary>
        /// Gets List of combination design output of the footing at different bar combination in both directions.
        /// </summary>
        public List<double[]> Combinations
        {
            get { return combs; }
        }

        /// <summary>
        /// Gets the design completion state.
        /// </summary>
        public eDesignCompletionState CompletionState
        {
            get { return completionState; }
        }

        /// <summary>
        /// Gets or sets the width of the footing
        /// </summary>
        public double Width
        {
            get
            {
                return B;
            }
            set
            {
                B = value;
            }
        }

        /// <summary>
        /// Gets or sets the length of the footing
        /// </summary>
        public double Length
        {
            get
            {
                return L;
            }
            set
            {
                L = value;
            }
        }

        /// <summary>
        /// Gets or setst the ConcreteColumnOrPedestal material used for the design.
        /// </summary>
        public eConcrete Concrete
        {
            get
            {
                return C;
            }
            set
            {
                C = value;
            }
        }

        /// <summary>
        /// Gets or sets the calculated area of steel extending in L-Direction
        /// </summary>
        public double CalcAsL
        {
            get
            {
                return GetAs(lSteelRatio, B, Lbar.EffD);
            }

        }

        /// <summary>
        /// Gets or sets the calculated area of steel extending in B-Direction
        /// </summary>
        public double CalcAsB
        {
            get
            {
                return GetAs(bSteelRatio, L, Bbar.EffD);
            }

        }
        /// <summary>
        /// Gets the provided area of steel in B direction.
        /// </summary>
        public double ProvAsB
        {
            get
            {
                return GetAs(GetSteelRation(Bbar.ProvSpacing, Bbar.Diameter, Bbar.EffD), L, Bbar.EffD);
            }
        }

        /// <summary>
        /// Gets the provided area of steel in L direction.
        /// </summary>
        public double ProvAsL
        {
            get
            {
                return GetAs(GetSteelRation(Lbar.ProvSpacing, Lbar.Diameter, Lbar.EffD), B, Lbar.EffD);
            }
        }

        /// <summary>
        /// Gets or sets the ConcreteColumnOrPedestal cover used.
        /// </summary>
        public double Cover
        {
            get
            {
                return cover;
            }
            set
            {
                cover = value;
            }
        }

        /// <summary>
        /// Gets or sets the gross depth of the footing
        /// </summary>
        public double Depth
        {
            get
            {
                return D;
            }
        }

        /// <summary>
        /// Gets or sets the effective depth of the footing
        /// </summary>
        public double Deff
        {
            get
            {
                return d;
            }
        }

        /// <summary>
        /// Gets or sets the width of the footing column.
        /// </summary>
        public double ColumnWidth
        {
            get
            {
                return Bc;
            }
            set
            {
                Bc = value;
            }
        }

        /// <summary>
        /// Gets or sets the shape of footing column used.
        /// </summary>
        public eColumnType ColumnType
        {
            get
            {
                return columnType;
            }
            set
            {
                columnType = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the footing column.
        /// </summary>
        public double ColumnLength
        {
            get
            {
                return Lc;
            }
            set
            {
                Lc = value;
            }
        }

        /// <summary>
        /// Gets or sets the diameter of the footing column.
        /// </summary>
        public double ColumnDiameter
        {
            get
            {
                return columnDiam;
            }
            set
            {
                columnDiam = value;
            }
        }

        /// <summary>
        /// Gets or sets the moment along B direction
        /// </summary>
        public double MB
        {
            get
            {
                return Mb;
            }
            set
            {
                Mb = value;
            }
        }

        /// <summary>
        /// Gets or sets the moment along L direction
        /// </summary>
        public double ML
        {
            get
            {
                return Ml;
            }
            set
            {
                Ml = value;
            }
        }

        /// <summary>
        /// Gets or sets the axial load
        /// </summary>
        public double P
        {
            get
            {
                return p;
            }
            set
            {
                p = value;
            }
        }

        /// <summary>
        /// Gets or sets the steel material used for the design
        /// </summary>
        public eSteel Steel
        {
            get
            {
                return S;
            }
            set
            {
                S = value;
            }
        }

        /// <summary>
        /// Gets or sets the value indicating wether the depth is providied or to be designed. True if depth is given and false if depth is not given.
        /// </summary>
        public bool IsDepthGiven
        {
            get
            {
                return isDepthGiven;
            }
            set
            {
                isDepthGiven = value;
            }
        }
        /// <summary>
        /// Gets the bar extending in L direction.
        /// </summary>
        public eFBar LBar
        {
            get { return Lbar; }
        }

        /// <summary>
        /// Gets the bar extending in B Direction.
        /// </summary>
        public eFBar BBar
        {
            get { return Bbar; }
        }

        /// <summary>
        /// Gets or set the value by which gross depth is incremented.
        /// </summary>
        public double DepthIncrement
        {
            get { return dIncr; }
            set { dIncr = value; }
        }

        /// <summary>
        /// Gets or sets the value by which spacing is incremented.
        /// </summary>
        public double SpacingIncrement
        {
            get { return sIncrmt; }
            set { sIncrmt = value; }
        }

        /// <summary>
        /// Gets or sets the minimum bar diameter used.
        /// </summary>
        public double MinBar
        {
            get { return minBarDiam; }
            set { minBarDiam = value; }
        }

        /// <summary>
        /// Gets or set the maximum bar diameter used.
        /// </summary>
        public double MaxBar
        {
            get { return maxBarDiam; }
            set { maxBarDiam = value; }
        }

        /// <summary>
        /// Gets or set the maximum aggregate size used in the design.
        /// </summary>
        public double MaxAggrSize
        {
            get { return C.MaxAgrtSize; }
            set { C.MaxAgrtSize = value; }
        }

        public double LengthIncrement
        {
            get { return lengthIncrement; }
            set { lengthIncrement = value; }
        }
        #endregion

        #region Methods

        #region Design

        private void InitializeForDesign()
        {
            if (useDiameter)
                minBarDiam = maxBarDiam = barDiam;
            if (useSpacing)
            {
                minBarDiam = (int)eReinforcement.Φ8;
                maxBarDiam = (int)eReinforcement.Φ32;
            }
            Lbar = new eFBar(minBarDiam, L, this, lengthIncrement);
            Bbar = new eFBar(minBarDiam, B, this, lengthIncrement);
            if (IsDepthGiven)
            {
                Lbar.SetEffD(D);
                Bbar.SetEffD(D, Lbar.Diameter);
            }
            else
            {
                D = Math.Ceiling((eSpecialStructures.GetMinFootingDepth(eFootingType.FootingOnSoil) + cover + Lbar.Diameter / 2) / dIncr) * dIncr;
                Lbar.SetEffD(D);
                Bbar.SetEffD(D, Lbar.Diameter);
            }
            d = (Bbar.EffD + Lbar.EffD) / 2;
        }

        /// <summary>
        /// Designs the isolated footing.
        /// </summary>
        private void CalculateDepthAndAs()
        {
            FillBearingStress();     
            if (isDepthGiven)
            { 
                FillEffectiveDepths();
                if (!CheckDepth())
                    throw new eInsufficientDephtException();
            }
            else
                CalculateDepth(D / 2);

            lSteelRatio = GetSteelRatio(GetML(), B, Lbar.EffD);
            bSteelRatio = GetSteelRatio(GetMB(), L, Bbar.EffD);

            lSteelRatio = lSteelRatio > eDetailing.Get_ρ_min(eStructureType.Footing, S.Grade) ? lSteelRatio : eDetailing.Get_ρ_min(eStructureType.Footing, S.Grade);
            bSteelRatio = bSteelRatio > eDetailing.Get_ρ_min(eStructureType.Footing, S.Grade) ? bSteelRatio : eDetailing.Get_ρ_min(eStructureType.Footing, S.Grade);
        }

        /// <summary>
        /// Fills the average of the L and B direction depths
        /// </summary>
        private void FillEffectiveDepths()
        {
            Lbar.SetEffD(D);
            Bbar.SetEffD(D, Lbar.Diameter);
            d = (Lbar.EffD + Bbar.EffD) / 2;
        }

        /// <summary>
        /// return the eccentricity
        /// </summary>
        /// <param name="M">the given moment</param>
        /// <param name="P">the given axial load</param>
        /// <returns></returns>
        private double GetEcentricity(double M, double P)
        {
            return M / P;
        }

        /// <summary>
        /// returns the value of stress on the footing
        /// </summary>
        /// <param name="P">the given axial load</param>
        /// <param name="L">Length of the footing</param>
        /// <param name="B">Width of the footing</param>
        /// <returns></returns>
        private double GetStress(double P, double L, double B)
        {
            return P / (L * B);
        }

        /// <summary>
        /// returns the value of punching shear resistance 
        /// </summary>
        /// <returns></returns>
        private double GetVprd(double d)
        {
            double U;
            if (columnType == eColumnType.Rectangular)
                U = 2 * (3 * d + Lc) + 2 * (3 * d + Bc);
            else
                U = Math.PI * (3 * d + columnDiam);
            return ePunching.GetVrd(U, C.fctd, d, CalcAsL / (B * Lbar.EffD), CalcAsB / (L * Bbar.EffD));
        }

        /// <summary>
        /// returns the value of acting punching shear
        /// </summary>
        /// <returns></returns>
        private double GetVp(double d)
        {
            double Ap;
            if (columnType == eColumnType.Rectangular)
                Ap = (3 * d + Lc) * (3 * d + Bc);
            else
                Ap = GetArea(3 * d + columnDiam);
            if (Ap > B * L)
                return 0;
            return (L * B - Ap) * qav;
        }

        /// <summary>
        /// returns the value of Moment in B direction
        /// </summary>
        /// <returns></returns>
        private double GetMB()
        {
            return qu * L * Math.Pow((B / 2 - Bc / 2), 2) / 2;
        }

        /// <summary>
        /// returns the value of Moment in L direction
        /// </summary>
        /// <returns></returns>
        private double GetML()
        {
            return qu * B * Math.Pow((L / 2 - Lc / 2), 2) / 2;
        }

        /// <summary>
        /// returns the value of steel ratio in both L and B direction
        /// </summary>
        /// <param name="M">Moment in the corresponding direction</param>
        /// <param name="b">width of the footing</param>
        /// <param name="d">effective depth</param>
        /// <returns></returns>
        private double GetSteelRatio(double M, double b, double d)
        {
            return (1 - Math.Sqrt(1 - 2 * M / (b * d * d * C.fcd))) / (S.fyd / C.fcd);
        }

        private double GetSteelRation(double spacing, double diameter, double d)
        {
            return GetArea(diameter) / (spacing * d);
        }

        /// <summary>
        /// returns the value of Area of steel in both L and B direction
        /// </summary>
        /// <param name="steelRatio"></param>
        /// <param name="b"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        private double GetAs(double steelRatio, double b, double d)
        {
            return steelRatio * b * d;
        }

        /// <summary>
        /// returns the area of single bar for a given bar diameter
        /// </summary>
        /// <param name="diam"></param>
        /// <returns></returns>
        public static double GetArea(double diam)
        {
            return Math.PI * diam * diam / 4;
        }

        /// <summary>
        /// returns provided steel ratio 
        /// </summary>
        /// <param name="diam">bar diameter</param>
        /// <param name="b">width or length of the footing</param>
        /// <param name="d">depth along the corresponding directions</param>
        /// <returns></returns>
        private double GetProvidedSteelRatio(double diam, double b, double d)
        {
            return GetArea(diam) / (b * d);
        }

        /// <summary>
        /// fills the ultimate bearing stress value
        /// </summary>
        private void FillBearingStress()
        {
            qav = GetStress(p, L, B);
            double Lprim = L - 2 * GetEcentricity(Mb, p);
            double Bprim = B - 2 * GetEcentricity(Ml, p);
            qu = GetStress(p, Lprim, Bprim);
        }

        /// <summary>
        /// returns the value of wide beam shear resistance in B direction
        /// </summary>
        /// <returns></returns>
        private double GetBVrd(double d)
        {
            return eShear.GetVc(C.fctd, B, d, CalcAsL);
        }

        /// <summary>
        /// returns the value of wide beam shear resistance in L direction
        /// </summary>
        /// <returns></returns>
        private double GetLVrd(double d)
        {
            return eShear.GetVc(C.fctd, L, d, CalcAsB);
        }

        /// <summary>
        /// returns the value of acting wide beam shear in shorter(B) direction
        /// </summary>
        /// <returns></returns>
        private double GetBV(double d)
        {
            return (L / 2 - (d + Lc / 2)) * B * qu;
        }

        /// <summary>
        /// returns the value of acting wide beam shear in shorter(B) direction
        /// </summary>
        /// <returns></returns>
        private double GetLV(double d)
        {
            return (B / 2 - (d + Bc / 2)) * L * qu;
        }

        /// <summary>
        /// checks the depth for wide beam shear and punching shear
        /// </summary>
        /// <returns></returns>
        private bool CheckDepth()
        {
            double Vp, Vprd, Vl, Vb, Vlrd, Vbrd;
            Vp = GetVp(d);
            Vprd = GetVprd(d);
            Vl = GetLV(Bbar.EffD);
            Vlrd = GetLVrd(Bbar.EffD);
            Vb = GetBV(Lbar.EffD);
            Vbrd = GetBVrd(Lbar.EffD);
            if (Vp > Vprd)
                return false;
            if (Vl > Vlrd)
                return false;
            if (Vb > Vbrd)
                return false;
            return true;
        }

        /// <summary>
        /// sets the data about footing column 
        /// </summary>
        /// <param name="Lc">Length of column</param>
        /// <param name="Bc">Width of column</param>
        /// <param name="columnType">ColumnWithSteelBasePlate type</param>
        /// <param name="columnDiam">ColumnWithSteelBasePlate Diameter</param>
        public void SetRectangularFootingColumnData(double Lc, double Bc)
        {
            this.Lc = Lc;
            this.Bc = Bc;
            this.columnType = eColumnType.Rectangular;
        }

        /// <summary>
        /// Sets the diameter of the column for circular footing column.
        /// </summary>
        /// <param name="columnDiam">Column diameter</param>
        public void SetCircularFootingColumnData(double columnDiam)
        {
            this.columnType = eColumnType.Circular;
            this.columnDiam = columnDiam;
        }
        #endregion

        #region Detailing

        private void FillCombinations()
        {
            double[] comb = new double[8];
            double[] bars = eDetailing.GetBarsBetwee(minBarDiam, maxBarDiam);
            double minSL, minSB, LAsPrev, BAsPrev;
            for (int i = 0; i < bars.Length; i++)
            {
                for (int j = 0; j < bars.Length; j++)
                {
                    Lbar.Diameter = comb[0] = bars[i];
                    Bbar.Diameter = comb[1] = bars[j];
                    FillEffectiveDepths();
                    do
                    {
                        LAsPrev = ProvAsL;
                        BAsPrev = ProvAsB;
                        try
                        {
                            CalculateDepthAndAs();
                        }
                        catch (Exception exx)
                        {
                            if (combs.Count == 0)
                                throw exx;
                        }
                        Lbar.CalcSpacing = comb[2] = GetSpacing(CalcAsL, Lbar.Diameter);
                        Bbar.CalcSpacing = comb[3] = GetSpacing(CalcAsB, Bbar.Diameter);
                        Lbar.ProvSpacing = comb[4] = GetPractSpacing(Lbar.CalcSpacing);
                        Bbar.ProvSpacing = comb[5] = GetPractSpacing(Bbar.CalcSpacing);
                        minSL = eDetailing.GetMinSpacing(Lbar.Diameter, C.MaxAgrtSize);
                        minSB = eDetailing.GetMinSpacing(Bbar.Diameter, C.MaxAgrtSize);

                    } while (CheckPrecision(BAsPrev, ProvAsB, 0) || CheckPrecision(LAsPrev, ProvAsL, 0) && (Lbar.ProvSpacing > minSL && Bbar.ProvSpacing > minSB));

                    if (Lbar.ProvSpacing > minSL && Bbar.ProvSpacing > minSB)
                    {
                        comb[6] = GetArea(Lbar.Diameter) * B / Lbar.ProvSpacing + GetArea(Bbar.Diameter) * L / Bbar.ProvSpacing;
                        comb[7] = D;
                        combs.Add(comb);
                        comb = new double[8];
                        if (!isDepthGiven)
                            D = eSpecialStructures.GetMinFootingDepth(eFootingType.FootingOnSoil) + cover + bars[j] / 2;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the diameter of single bar that can economically apporximate the given area of steel.
        /// </summary>
        /// <param name="diamList">List of diameter on which the selection of bar diamter is going to be done.</param>
        /// <returns></returns>
        private double GetEcoDiam(double As)
        {
            double[] diamList = eDetailing.GetBarsBetwee(minBarDiam, maxBarDiam);
            for (int i = 0; i < diamList.Length; i++)
            {
                if (GetArea(diamList[i]) > As)
                    return diamList[i];
            }
            throw new Exception("Diameter of bar is above the limit.");
        }

        private void DesignGivenSpacing()
        {
            double Ldiam = minBarDiam, Bdiam = minBarDiam;
            do
            {
                Lbar.Diameter = Ldiam;
                Bbar.Diameter = Bdiam;
                FillEffectiveDepths();
                CalculateDepthAndAs();
                Ldiam = GetEcoDiam(spacing * Lbar.EffD * lSteelRatio);
                Bdiam = GetEcoDiam(spacing * Bbar.EffD * bSteelRatio);
            } while (Ldiam != Lbar.Diameter || Bdiam != Bbar.Diameter);

            Lbar.CalcSpacing = GetSpacing(CalcAsL, Lbar.Diameter);
            Bbar.CalcSpacing = GetSpacing(CalcAsB, Bbar.Diameter);
            Lbar.ProvSpacing = spacing;
            Bbar.ProvSpacing = spacing;

        }

        public void Design()
        {
            InitializeForDesign();
            if (!useSpacing)
            {
                FillCombinations();
                if (combs.Count == 0)
                    throw new eReinfCongestedException();
                double[] EcoComb = GetEcoComb(minSpacing, Math.Max(maxSpacing, eDetailing.GetMaxBarSpacingForSlab(D)));
                FillToBars(EcoComb);
            }
            else DesignGivenSpacing();

            if (!CheckAchorageLength())
             //   throw new eInSufficientAnchorageLengthException();

            if (useMaxHookLength)
                hookLength = D - 2 * cover;

            Lbar.GenerateBarLengths(hookLength, hookLength != 0);
            Bbar.GenerateBarLengths(hookLength - Lbar.Diameter - Bbar.Diameter / 2, hookLength != 0);
            Lbar.FillBarNumber(B - 2 * cover);
            Bbar.FillBarNumber(L - 2 * cover);
            Lbar.Name = "1";
            Bbar.Name = "2";
            double vrd = GetVprd(d);
            double vp = GetVp(d);
            double vrdl = GetLVrd(Lbar.EffD);
            double vrdb = GetBVrd(Bbar.EffD);
            double vl = GetLV(Lbar.EffD);
            double vb = GetBV(Bbar.EffD);
        }

        private void FillToBars(double[] comb)
        {
            Lbar.Diameter = comb[0];
            Bbar.Diameter = comb[1];
            Lbar.CalcSpacing = comb[2];
            Bbar.CalcSpacing = comb[3];
            Lbar.ProvSpacing = comb[4];
            Bbar.ProvSpacing = comb[5];
            D = comb[7];
            FillEffectiveDepths();
        }

        private double[] GetEcoComb(double minS, double maxS)
        {
            double Vol = double.PositiveInfinity;
            int n = 0;
            for (int i = 0; i < combs.Count; i++)
            {
                if (combs[i][4] <= maxS && combs[i][4] >= minS && combs[i][5] <= maxS && combs[i][5] >= minS)
                {
                    if (combs[i][6] < Vol)
                    {
                        Vol = combs[i][6];
                        n = i;
                    }
                }
            }
            if (Vol == double.PositiveInfinity)
                throw new eNoBarBetweenSpacingLimitException();
            return (double[])combs[n].Clone();
        }

        private double[] GetEcoComb()
        {
            double Vol = combs[0][6];
            int n = 0;
            for (int i = 0; i < combs.Count; i++)
            {
                if (combs[i][6] < Vol)
                {
                    Vol = combs[i][6];
                    n = i;
                }
            }
            return (double[])combs[n].Clone();
        }


        //private double[] GetLEcoComb()
        //{

        //    double diff = double.PositiveInfinity;
        //    int n = 0;
        //    for (int i = 0; i < combs.Count; i++)
        //    {
        //        if (combs[i][6] < diff)
        //        {
        //            diff = combs[i][6];
        //            n = i;
        //        }
        //    }
        //    return (double[]) combs[n].Clone();
        //}

        //private double[] GetBEcoComb()
        //{
        //    double diff = double.PositiveInfinity;
        //    int n = 0;
        //    for (int i = 0; i < combs.Count; i++)
        //    {
        //        if (combs[i][7] < diff)
        //        {
        //            diff = combs[i][7];
        //            n = i;
        //        }
        //    }
        //    return (double[])combs[n].Clone();
        //}

        private bool CheckAchorageLength()
        {
            double Llbnet, LlbA, Blbnet, BlbA;
            Llbnet = eDetailing.GetReqAnchorageLength(CalcAsL, ProvAsL, Lbar.Diameter, S.fyd, C.fctd, anchorType: Lbar.AnchorType);
            Blbnet = eDetailing.GetReqAnchorageLength(CalcAsB, ProvAsB, Bbar.Diameter, S.fyd, C.fctd, anchorType: Bbar.AnchorType);
            LlbA = GetLAvailInLdxn();
            BlbA = GetLAvailInBdxn();
            if (Llbnet < LlbA && Blbnet < BlbA)
                return true;
            Lbar.AnchorType = Llbnet > LlbA ? eAnchorageType.Hook90 : Lbar.AnchorType;
            Bbar.AnchorType = Blbnet > BlbA ? eAnchorageType.Hook90 : Bbar.AnchorType;

            Llbnet = Llbnet > LlbA ? eDetailing.GetReqAnchorageLength(CalcAsL, ProvAsL, Lbar.Diameter, S.fyd, C.fctd, anchorType: Lbar.AnchorType) : Llbnet;
            Blbnet = Blbnet > BlbA ? eDetailing.GetReqAnchorageLength(CalcAsB, ProvAsB, Bbar.Diameter, S.fyd, C.fctd, anchorType: Bbar.AnchorType) : Blbnet;

            if (Llbnet > LlbA || Blbnet > BlbA)
                return false;
            return true;
        }

        /// <summary>
        /// returns the value of  practical spacing 
        /// </summary>
        /// <param name="spacing">the actual spacing from calculation</param>
        /// <returns></returns>
        private double GetPractSpacing(double spacing)
        {
            return (int)(spacing / sIncrmt) * sIncrmt;
        }

        private double GetPractSpacing(double As, double diam)
        {
            return GetPractSpacing(GetSpacing(As, diam));
        }


        /// <summary>
        /// Returns the actual spacing between bars
        /// </summary>
        /// <param name="As">Total area of steel in the corresponding direction</param>
        /// <param name="diam">diameter of  bar in that direction</param>
        /// <returns></returns>
        private double GetSpacing(double As, double diam)
        {
            return 1000 * eDFooting.GetArea(diam) / As;

        }

        /// <summary>
        /// returns the value of available anchorage length in L direction
        /// </summary>
        /// <returns></returns>
        private double GetLAvailInLdxn()
        {
            double projection;
            double La;
            if (columnType == eColumnType.Rectangular)
            {
                projection = L / 2 - Lc / 2;
                La = L / 2 - Lc / 2 - cover;
            }
            else
            {
                projection = L / 2 - columnDiam / 2;
                La = L / 2 - columnDiam / 2 - cover;
            }

            if (projection < Lbar.EffD)
                return La;
            else
                return La - Lbar.EffD;
        }

        /// <summary>
        /// returns the value of available anchorage length in B direction
        /// </summary>
        /// <returns></returns>
        private double GetLAvailInBdxn()
        {
            double projection;
            double La;
            if (columnType == eColumnType.Rectangular)
            {
                projection = B / 2 - Bc / 2;
                La = B / 2 - Bc / 2 - cover;
            }
            else
            {
                projection = B / 2 - columnDiam / 2;
                La = B / 2 - columnDiam / 2 - cover;
            }

            if (projection < Bbar.EffD)
                return La;
            else
                return La - Bbar.EffD;

        }

        /// <summary>
        /// returns the value of basic anchorage length
        /// </summary>
        /// <param name="diam">diameter of the bar</param>
        /// <returns></returns>
        private double GetLb(double diam)
        {
            return diam * S.fyd / (4 * 2 * C.fctd);
        }


        /// <summary>
        /// returns number of bars
        /// </summary>
        /// <param name="diam">diameter of the bar</param>
        /// <param name="steelRatio">steel ratio</param>
        /// <returns></returns>
        private double GetNoOfBars(double diam, double steelRatio)
        {
            double n = GetAs(steelRatio, B, d) / GetArea(diam);
            if (n / (int)n == 1)
                return n;
            else
                return (int)n + 1;
        }

        /// <summary>
        /// returns the value of area of steel provided
        /// </summary>
        /// <param name="diam">diameter of the bar</param>
        /// <param name="steelRatio">steel ratio</param>
        /// <returns></returns>
        private double GetAsProvided(double diam, double steelRatio)
        {
            return GetNoOfBars(diam, steelRatio) * GetArea(diam);
        }

        /// <summary>
        /// sets bar preference
        /// </summary>
        /// <param name="Lbar.Diameter">diameter of bar to be placed in L direction </param>
        /// <param name="bBarDiam">diameter of bar to be placed in B direction</param>
        /// <param name="maxBarDiam">maximum bar diameter</param>
        /// <param name="minBarDiam">minimum bar diameter</param>
        /// <param name="maxSpacing">maximum spacing</param>
        /// <param name="minSpacing">minimum spacing</param>
        public void SetDetailingData(double minBarDiam, double maxBarDiam, double minSpacing, double maxSpacing, double cover)
        {
            this.maxBarDiam = maxBarDiam;
            this.minBarDiam = minBarDiam;
            this.minSpacing = minSpacing;
            this.maxSpacing = maxSpacing;
            this.cover = cover;
        }

        private void FillBars(ref eFBar bar, double diam, double d, eAnchorageType achor)
        {
            bar.Diameter = diam;
            bar.EffD = d;
            bar.AnchorType = achor;
        }
        #endregion

        #endregion

        #region New Methods

        public void CalculateDepth(double diff)
        {
            while (!CheckDepth())
            {
                D += diff;
                FillEffectiveDepths();
            }
            if (diff < 1)
            {
                D = Math.Ceiling(D / dIncr) * dIncr;
                FillEffectiveDepths();
                return;
            }
            D -= diff;
            FillEffectiveDepths();
            CalculateDepth(diff / 10);
        }

        /// <summary>
        /// Checks whether a given value statisfied the given precision relative to its actual value.
        /// </summary>
        /// <param name="value">Value to be checked for precision.</param>
        /// <param name="actualValue">Actual value used to check the precision.</param>
        /// <param name="precision">Precision interms of number of correct decimal paceses.</param>
        /// <returns></returns>
        protected bool CheckPrecision(double value, double actualValue, int precision)
        {
            if (Math.Round(value, precision) == Math.Round(actualValue, precision))
                return false;
            return true;
        }

        #endregion
    }
}
