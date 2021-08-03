using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Code;
using ESADS.Code.EBCS_1995;
using ESADS.Mechanics;

namespace ESADS.Mechanics.Design.Column
{
    /// <summary>
    /// Represents a base class for all column design sections.
    /// </summary>
    public abstract class eDColumn
    {
        #region Fields
        /// <summary>
        /// Holds a value for property 'P'.
        /// </summary>
        protected double p;
        /// <summary>
        /// Holds a value for public property 'Concrete'.
        /// </summary>
        protected eConcrete C;
        /// <summary>
        /// Holds a value for public property 'Steel'.
        /// </summary>
        protected eSteel S;
        /// <summary>
        /// Holds a value for public 'Dprim'.
        /// </summary>
        protected double hprim;
        /// <summary>
        /// Holds a value for public 'BarDiam'.
        /// </summary>
        protected double mainBarDiam;
        /// <summary>
        /// Holds a value for public property 'Width'.
        /// </summary>
        protected double b;
        /// <summary>
        /// Holds a value for public property 'Depth'.
        /// </summary>
        protected double h;
        /// <summary>
        /// Holds a value for public property 'Precision'.
        /// </summary>
        protected int precision;
        /// <summary>
        /// Holds a value fro public property 'OverReinforcedFailur'.
        /// </summary>
        protected double bprim;
        protected double mx;
        protected double maxSpacing;
        protected double minSpacing;
        protected double minDiam;
        protected double maxDiam;
        protected eAs[] Ass;
        protected eConc conc;
        protected eCompletionState compState;
        protected eExposureType exposureType;
        protected int barsInX;
        protected int barsInY;
        protected double cover;
        protected double stirrupDiam;
        protected eDetailType typeOfDetail;
        protected double AsCalc;
        protected bool desingAndDetail;
        protected double BtoHRatio;
        protected bool useEconomicalBar;
        protected eColumnAction action;
        protected int rows;
        protected int columns;
        protected int nx;
        protected int ny;

        protected int xN = 1;
        protected int yN = 1;

        #endregion

        #region Constructor
        /// <summary>
        /// Creates an instance of ESADS.Mechanics.Design.eDColoumn class from a given basic parameters.
        /// </summary>
        /// <param name="concrete">Concrete material used for the design.</param>
        /// <param name="steel">Steel material used for the design.</param>
        /// <param name="p">Design axial load.</param>
        /// <param name="dprim">The distance from the center of the reinforcement to exterior column surface.</param>
        protected eDColumn(double b, double h, eConcrete C, eSteel S, double p, double mx,eDetailType typeOfDetail)
        {
            this.b = b;
            this.h = h;
            this.S = S;
            this.C = C;
            this.mx = mx;
            this.p = p;
            this.conc = new eConc(b, h, C.fcd);
            this.compState = eCompletionState.NotDesigned;
            this.typeOfDetail = typeOfDetail;
            this.cover = eDetailing.GetMinConcreteCover(eExposureType.Moderate);
            this.stirrupDiam = 8;
            this.hprim = 0.1 * h;
            this.bprim = 0.1 * b;
            this.precision = 0;
        }

        protected eDColumn(double b, double h, eConcrete C, eSteel S, double p, double mx, int barsInX, int barsInY)
        {
            this.b = b;
            this.h = h;
            this.S = S;
            this.C = C;
            this.mx = mx;
            this.p = p;
            this.conc = new eConc(b, h, C.fcd);
            this.compState = eCompletionState.NotDesigned;
            this.barsInX = barsInX;
            this.barsInY = barsInY;
            this.typeOfDetail = eDetailType.Type3;
            this.cover = eDetailing.GetMinConcreteCover(eExposureType.Moderate);
            this.stirrupDiam = 8;
            this.FillAss(20);
            this.hprim = 0.1 * h;
            this.bprim = 0.1 * b;
        }
        #endregion

        #region Properties
        public int Rows
        {
            get
            {
                return rows;
            }
            set
            {
                rows = value;
            }
        }

        public int Columns
        {
            get
            {
                return columns;
            }
            set
            {
                columns = value;
            }
        }

        public int Nx
        {
            get
            {
                return xN;
            }
            set
            {
                xN = value;
            }
        }

        public int Ny
        {
            get
            {
                return yN;
            }
            set
            {
                yN = value;
            }
        }
        public eColumnAction Action
        {
            get
            {
                return action;
            }
        }
        public double Hprim
        {
            get
            {
                return hprim;
            }
            set
            {
                hprim = value;
            }
        }

        public double Bprim
        {
            get
            {
                return hprim;
            }
            set
            {
                hprim = value;
            }
        }
        public bool UseEcoDiam
        {
            get
            {
                return useEconomicalBar;
            }
            set
            {
                useEconomicalBar = value;
            }
        }
        public bool DesingAndDetail
        {
            get
            {
                return desingAndDetail;
            }
            set
            {
                desingAndDetail = value;
            }
        }

        public double BToHRation
        {
            get
            {
                return BtoHRatio;
            }
            set
            {
                BtoHRatio = value;
            }
        }

        public eExposureType ExposureType
        {
            get
            {
                return exposureType;
            }
            set
            {
                exposureType = value;
            }
        }
        /// <summary>
        /// Gets the total number of bars in the section.
        /// </summary>
        public int NumberOfBars
        {
            get { return GetTotalNumbOfBars(barsInX, barsInY); }
        }

        /// <summary>
        /// Gets or sets the number of bars in X-direction.
        /// </summary>
        public int XBarNumber
        {
            get { return barsInX; }
            set { barsInX = value; }
        }

        /// <summary>
        /// Gets or sets the number of bars in Y-direction.
        /// </summary>
        public int YBarNumber
        {
            get { return barsInY; }
            set { barsInY = value; }
        }

        /// <summary>
        /// Gets or ses the applied moment in x direction.
        /// </summary>
        public double Mx
        {
            get { return mx; }
            set { mx = value; }
        }

        /// <summary>
        /// Gets or sets the maximum spacing of bars.
        /// </summary>
        public double MaxSpacing
        {
            get { return maxSpacing; }
            set { maxSpacing = value; }
        }

        /// <summary>
        /// Gets or sets the minimum spacing of bars.
        /// </summary>
        public double MinSpacing
        {
            get { return minSpacing; }
            set { minSpacing = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double MinDiam
        {
            get { return minDiam; }
            set { minDiam = value; }
        }

        /// <summary>
        /// Gets or sets the minimum diameter of bar used in the design.
        /// </summary>
        public double MaxDiam
        {
            get { return maxDiam; }
            set { maxDiam = value; }
        }

      

        /// <summary>
        /// Gets the completion state of the whole design process.
        /// </summary>
        public eCompletionState CompletionState
        {
            get { return compState; }
        }

        /// <summary>
        /// Gets or sets the Concrete material used for the design of the column.
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
        /// Gets or sets the Concrete material used for the design of the column.
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
        /// Gets or sets the distance from the centroid of reinforcement up to exterior of the column including concrete cover.
        /// </summary>
        public double Dprim
        {
            get
            {
                return hprim;
            }
            set
            {
                hprim = value;
            }
        }

        /// <summary>
        /// Gets or sets the axial load on the column.
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
        /// Gets ro sets the bar diameter used on the column.
        /// </summary>
        public double BarDiam
        {
            get
            {
                return mainBarDiam;
            }
            set
            {
                mainBarDiam = value;
            }
        }

        /// <summary>
        /// Gets the total area of reinforcement.
        /// </summary>
        public double AsTotal
        {
            get
            {
                return Ass[0].Area * Ass.Length;
            }
        }

        /// <summary>
        /// Gets or sets the width of the column.
        /// </summary>
        public double Width
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
            }
        }

        /// <summary>
        /// Gets or sets the depth of the column.
        /// </summary>
        public double Depth
        {
            get
            {
                return h;
            }
            set
            {
                h = value;
            }
        }

        /// <summary>
        /// Gets or sets the the degree of precision in terms of decimal place.
        /// </summary>
        public int Precision
        {
            get
            {
                return precision;
            }
            set
            {
                if (precision >= 0)
                    precision = value;
            }
        }

        /// <summary>
        /// Gets area of reinforcement of single unit of bar.
        /// </summary>
        public double As
        {
            get
            {
                return Ass[0].Area;
            }

        }

        /// <summary>
        /// Gets the minimum dimension of the column.
        /// </summary>
        public double MinDim
        {
            get
            {
                return Math.Min(b, h);
            }
        }

        /// <summary>
        /// Gets the maximum dimension of the column.
        /// </summary>
        public double MaxDim
        {
            get
            {
                return Math.Max(b, h);
            }
        }

        /// <summary>
        /// Gets or sets the type of detail for rectangular column detail specifeid in EBCS-2 part 2.
        /// </summary>
        public eDetailType TypeOfDetail
        {
            get { return typeOfDetail; }
            set { typeOfDetail = value; }
        }

        /// <summary>
        /// Gets or sets the concrete cover of the column.
        /// </summary>
        public double Cover
        {
            get { return cover; }
            set { cover = value; }
        }

        /// <summary>
        /// Gets or sets the diameter of the stirrup used in the column.
        /// </summary>
        public double StirrumDiam
        {
            get { return stirrupDiam; }
            set { stirrupDiam = value; }
        }

        /// <summary>
        /// Gets the total area of steel provided.
        /// </summary>
        public double AstProvided
        {
            get
            {
                return Ass.Length * GetBarArea(mainBarDiam);
            }
        }

        /// <summary>
        /// Gets the maximum area of steel that can be used in the column.
        /// </summary>
        public double AsMax
        {
            get { return GetAsMax(); }
        }

        /// <summary>
        /// Gets the minimum area of steel that can be used in the column.
        /// </summary>
        public double AsMin
        {
            get { return GetAsMin(); }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Returns the Moment caried in x direction by the section.
        /// </summary>
        /// <returns></returns>
        protected double GetMx(double x, double teta = 0)
        {
            SetXandTeta(x, teta);
            double mx = conc.Mx;
            for (int i = 0; i < Ass.Length; i++)
            {
                mx += Ass[i].Mx;
            }
            return Math.Abs(mx);
        }

        protected void ChangeXandP(ref double x1, ref double x2, ref double p1, ref double p2, double p, double teta = 0)
        {
            double temX, temP;
            temX = (x1 + x2) / 2;
            temP = GetPatX(temX, teta);
            Interchange(ref x1, ref x2, ref p1, ref p2, temX, temP, p);
            temX = Interpolate(x1, x2, p1, p2, p);
            temP = GetPatX(temX, teta);
            if (!CheckPrecision(p1, p, precision))
            {
                p2 = p1;
                x2 = x1;
                return;
            }
            Interchange(ref x1, ref x2, ref p1, ref p2, temX, temP, p);
        }

        protected double GetPatX(double x,double teta = 0)
        {
            SetXandTeta(x,teta);
            double N = conc.NC;
            for (int i = 0; i < Ass.Length; i++)
            {
                N += Ass[i].NS;
            }
            return N;
        }

        protected void InitializeXandP(out double x1, out double x2, out double p1, out double p2, double p, double teta = 0)
        {
            x1 = double.Epsilon;
            p1 = GetPatX(x1);
            x2 = h;
            p2 = GetPatX(x2, teta);
            while (p2 < p)
            {
                x1 = x2;
                p1 = p2;
                x2 = x1 + 0.01 * p * h * h / mx;
                p2 = GetPatX(x2, teta);
            }
        }

        /// <summary>
        /// Designs the column.
        /// </summary>
        public abstract void CalculateAs();

        /// <summary>
        /// Returns the Area of steel required for pure axial compression.
        /// It returns 0 if the concrete can carry axial load applied.
        /// </summary>
        /// <returns></returns>
        protected double GetAxialAs()
        {
            if (S.εs <= C.εc)
                return Math.Max((this.p - C.fcd * b * h) / (S.fyd - C.fcd), 0) / Ass.Length;
            else
                return Math.Max((this.p - C.fcd * b * h) / (C.εc * S.E - C.fcd), 0) / Ass.Length;
        }

        /// <summary>
        /// Returns the maximum area of steel for a column section given area of concrete.
        /// </summary>
        /// <returns></returns>
        protected double GetAsMax()
        {
            return 0.08 * b * h / Ass.Length;
        }

        /// <summary>
        /// Returns the minimum area of steel for a column section given area of concrete.
        /// </summary>
        /// <returns></returns>
        protected double GetAsMin()
        {
            return 0.008 * b * h / Ass.Length;
        }

        /// <summary>
        /// Interpolates the value of x for a given set of coordinate.
        /// </summary>
        /// <param name="x1">First value of x.</param>
        /// <param name="x2">Second value of x.</param>
        /// <param name="y1">First value of y.</param>
        /// <param name="y2">Second value of y.</param>
        /// <param name="y">Value of y at which x is required.</param>
        /// <returns></returns>
        protected double Interpolate(double x1, double x2, double y1, double y2, double y)
        {
            return (x2 - x1) * (y - y1) / (y2 - y1) + x1;
        }

        /// <summary>
        /// Assigns all necessary data to bars distributed around the cornners of the cross section.
        /// The assigned data includes material,location and detailing requirments.
        /// </summary>
        ///<param name="diam">Diameter of bar used.</param>
        protected void FillAss(double diam)
        {
            if (diam != 0)
                SetHprimAndBprim(diam);

            if (barsInX == 1)
            {
                Ass = new eAs[2];
                Ass[0] = new eAs(b, h, 0, h / 2 - hprim, C.εc, C.εb, S);
                Ass[1] = new eAs(b, h, 0, -h / 2 + hprim, C.εc, C.εb, S);
                return;
            }
            double dy = (h - 2 * hprim) / (barsInY - 1);
            double dx = (b - 2 * bprim) / (barsInX - 1);
            Ass = new eAs[GetTotalNumbOfBars(barsInX, barsInY)];

            for (int i = 0; i < barsInX; i++)
            {
                Ass[i] = new eAs(b, h, -b / 2 + bprim + dx * i, h / 2 - hprim, C.εc, C.εb, S);
                Ass[barsInX + (barsInY - 1) - 1 + i] = new eAs(b, h, b / 2 - bprim - dx * i, -h / 2 + hprim, C.εc, C.εb, S);
            }
            for (int i = 1; i < barsInY - 1; i++)
            {
                Ass[barsInX - 1 + i] = new eAs(b, h, b / 2 - bprim, h / 2 - hprim - i * dy, C.εc, C.εb, S);
                Ass[Ass.Length - i] = new eAs(b, h, -b / 2 + bprim, h / 2 - hprim - i * dy, C.εc, C.εb, S);
            }
        }

        /// <summary>
        /// Sets the area of reinforcement for each bars in the column.
        /// </summary>
        /// <param name="As">Area of reinforcement.</param>
        protected void SetAreasOfReinf(double As)
        {
            for (int i = 0; i < Ass.Length; i++)
            {
                Ass[i].Area = As;
            }
        }

        /// <summary>
        /// Sets the depth and inclination of nutral axis.
        /// </summary>
        /// <param name="x">Depth of nuetral axis.</param>
        /// <param name="teta">Inclination of nuetral axis in radian.</param>
        protected void SetXandTeta(double x, double teta = 0)
        {
            for (int i = 0; i < Ass.Length; i++)
            {
                Ass[i].FillParameters(x, teta);
            }
            conc.FillParameters(GetAsInCompZone(), 0.8 * x, teta);
        }

        /// <summary>
        /// Returns the area of steel in the compresion zone.
        /// </summary>
        /// <returns>The reaturned values are sebstructed from the concrete area to calculate the force resisted by the concrete.</returns>
        protected double GetAsInCompZone()
        {
            double area = 0;
            for (int i = 0; i < Ass.Length; i++)
            {
                if (Ass[i].IsAboveNA())
                    area += Ass[i].Area;
            }
            return area;
        }

        /// <summary>
        /// Interchangeds sucessive values of x and y making the first values
        /// bellow and the second values above the actual value considering only the nearest ones.
        /// </summary>
        /// <param name="x1">Firs value of x</param>
        /// <param name="x2">Second value of x</param>
        /// <param name="y1">First value of y.</param>
        /// <param name="y2">Second value of y.</param>
        /// <param name="x">The current value of x.</param>
        /// <param name="y">The current value of y.</param>
        /// <param name="acValue"></param>
        protected void Interchange(ref double x1, ref double x2, ref double y1, ref double y2, double x, double y, double acValue)
        {
            if (y < acValue)
            {
                x1 = x;
                y1 = y;
                return;
            }
            else
            {
                x2 = x;
                y2 = y;
            }
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

        /// <summary>
        /// Returns the maximum axial capacity of the section.
        /// </summary>
        /// <returns></returns>
        public double GetMaxAxialCapacity(eAnalysisReinf aReinf = eAnalysisReinf.AsCalculated)
        {
            double asCalc = As;
            double result;
            if (aReinf == eAnalysisReinf.AsProvided)
                SetAreasOfReinf(GetBarArea(mainBarDiam));
            if (S.εs <= C.εc)
                result =  C.fcd * (b * h - Ass.Length * Ass[0].Area) + Ass.Length * Ass[0].Area * S.fyd;
            else
                result  =  C.fcd * (b * h - Ass.Length * Ass[0].Area) + Ass.Length * Ass[0].Area * C.εc * S.E;
            SetAreasOfReinf(asCalc);
            return result;
        }

        /// <summary>
        /// Interchangeds sucessive values of x and y making the first values
        /// bellow and the second values above the actual value.
        /// </summary>
        /// <param name="x1">Firs value of x</param>
        /// <param name="x2">Second value of x</param>
        /// <param name="y1">First value of y.</param>
        /// <param name="y2">Second value of y.</param>
        /// <param name="acValue"></param>
        protected void Interchange(ref double x1, ref double x2, ref double y1, ref double y2, double acValue)
        {
            if (y2 < acValue)
            {
                double temp;
                temp = x1;
                x1 = x2;
                x2 = x1;
                temp = y1;
                y1 = y2;
                y2 = y1;
            }
        }

        /// <summary>
        /// Returns the nearest value to the actual value among the viven sets of two values.
        /// </summary>
        /// <param name="Value1">First value.</param>
        /// <param name="Value2">Second value.</param>
        /// <param name="ActualValue">Actual value.</param>
        /// <returns></returns>
        protected double GetNearest(double Value1, double Value2, double ActualValue)
        {
            if (Math.Abs(Value1 - ActualValue) < Math.Abs(Value2 - ActualValue))
                return Value1;
            else
                return Value2;
        }

        /// <summary>
        /// Sets the value of hprim and bprim of the column.
        /// </summary>
        /// <param name="diam">Diameter of bar used.</param>
        private void SetHprimAndBprim(double diam)
        {
            hprim = bprim = cover + stirrupDiam + diam / 2;
        }

        /// <summary>
        /// Fills the mimimum concrete cover.
        /// </summary>
        /// <param name="diam">Diameter of bar used.</param>
        protected void FillMinCover(double diam)
        {
            if (C.MaxAgrtSize < 32)
                cover = Math.Max(eDetailing.GetMinConcreteCover(exposureType), diam);
            else
                cover = Math.Max(eDetailing.GetMinConcreteCover(exposureType), diam + 5);
        }

        /// <summary>
        /// Returns the actual minimum spacing between bars for a given diameter of bars.
        /// First number of bars in x and y direction should be specified.
        /// </summary>
        /// <param name="diam">Diameter of bar used.</param>
        /// <returns></returns>
        protected double GetActualMinSpacing(double diam)
        {
            if (barsInX == 1)
                return h - 2 * hprim - diam;
            else
                return Math.Min((h - 2 * hprim) / (barsInY - 1), (b - 2 * bprim) / (barsInX - 1)) - diam;
        }

        /// <summary>
        /// Returns the diameter of single bar that can economically apporximate the given area of steel.
        /// </summary>
        /// <param name="diamList">List of diameter on which the selection of bar diamter is going to be done.</param>
        /// <returns></returns>
        protected double GetEcoDiam(List<double> diamList, double As)
        {
            for (int i = 0; i < diamList.Count; i++)
            {
                if (GetBarArea(diamList[i]) > As)
                    return diamList[i];
            }
            throw new Exception("Diameter of bar is above the limit.");
        }

        /// <summary>
        /// Returns the area of a bar for a given diameter.
        /// </summary>
        /// <param name="diam">Diameter of bar.</param>
        /// <returns></returns>
        protected double GetBarArea(double diam)
        {
            return Math.PI * diam * diam / 4;
        }

        /// <summary>
        /// Returns the number of bars required to approximate the given area of steel for a given diameter of bar.
        /// </summary>
        /// <param name="diam">Diameter of bar used.</param>
        /// <param name="As">Actual area of steel.</param>
        /// <returns></returns>
        protected int GetNumbOfBar(double diam, double As)
        {
            return (int)Math.Ceiling(As / GetBarArea(diam));
        }


        /// <summary>
        /// Sets all necessary data related to detailing.
        /// </summary>
        /// <param name="cover">Concrete cover.</param>
        /// <param name="maxDiam">Maximum diameter.</param>
        /// <param name="minDiam">Minimum diameter.</param>
        /// <param name="stirrupDiam">Diameter of stirrup.</param>
        public void SetDetailingData(double cover, double maxDiam, double minDiam, double stirrupDiam)
        {
            this.cover = cover;
            this.maxDiam = maxDiam;
            this.minDiam = minDiam;
            this.stirrupDiam = stirrupDiam;
            if (typeOfDetail == eDetailType.Type3)
                FillAss(minDiam);
        }

        /// <summary>
        /// Returns the  bars diameter that can economically approximate the given 
        /// area of steel add fill the number of bars in out parameter.
        /// </summary>
        /// <param name="n">Out parameter to be filled by the number of bars calculated.</param>
        /// <param name="diamList">List of bar diameter on which the calculation is going to be done.</param>
        /// <param name="As">Actual area of steel.</param>
        /// <returns></returns>
        protected double GetEcoDiam(out int n, List<double> diamList, double As)
        {
            double diam = diamList[0];
            double diff = GetAreaDiff(As, diam);
            n = GetNumbOfBar(diam, As);

            for (int i = 1; i < diamList.Count; i++)
            {
                if (GetAreaDiff(As, diamList[i]) < diff)
                {
                    n = GetNumbOfBar(diamList[i], As);
                    diam = diamList[i];
                    diff = GetAreaDiff(As, diamList[i]);
                }
            }
            return diam;
        }

        /// <summary>
        /// Returns the difference of area between area of steel provided and area of steel calculated.
        /// </summary>
        /// <param name="AsCalc">Calculated area of steel.</param>
        /// <param name="diam">Diameter of bar used.</param>
        /// <returns></returns>
        protected double GetAreaDiff(double AsCalc, double diam)
        {
            return GetNumbOfBar(diam, AsCalc) * GetBarArea(diam) - AsCalc;
        }

        /// <summary>
        /// Returns the number of bars that can be distributed in a given width at minimum spacing 
        /// including concrete cover and stirrup.
        /// </summary>
        /// <param name="diam">Diameter of bar to be distributed.</param>
        /// <param name="b">The widht on which the bars are going to be distributed including concrete cover and stirrup diameter.</param>
        /// <returns></returns>
        protected int GetMaxNumbOfBars(double diam, double b)
        {
            double beff = b - 2 * (cover + stirrupDiam);
            double minS = eDetailing.GetMinSpacing(diam, C.MaxAgrtSize);
            return (int)((beff + minS) / (diam + minS));
        }

        /// <summary>
        /// Returns the total number of bars in the column given number of bars in each direction.
        /// </summary>
        /// <param name="xBar">Number of bars in X-direction.</param>
        /// <param name="yBars">Number of bars in Y-direction.</param>
        /// <returns></returns>
        protected int GetTotalNumbOfBars(int xBar, int yBars)
        {
            if (xBar < 2 || yBars < 2)
                throw new Exception("Number of bars in any direction shall not be less than 2");
            return 2 * (barsInX + barsInY) - 4;
        }

        /// <summary>
        /// Performs a reinforcement detailing and design revision for column DetailType-3.
        /// DetailType-3 is a type of column detail in which number of reinforcement and their oreintation is given.
        /// </summary>
        protected void DetailType3()
        {
            double diam = minDiam;
            List<double> diamList = eDetailing.GetBarsBetwee(minDiam, maxDiam).ToList();
            do
            {
                FillAss(diam);
                CalculateAs();
                mainBarDiam = diam;
                diam = GetEcoDiam(diamList,As);
                if (eDetailing.GetMinSpacing(diam, C.MaxAgrtSize) > GetActualMinSpacing(diam))
                    throw new Exception("The reinforcements are conjusted.");

            } while (diam != mainBarDiam);
        }      
       
        /// <summary>
        /// Performs a reinforcement detailing and design revision for column DetailType-2.
        /// DetailType-2 is a type of column detail in which uniformly distributed 
        /// reinforcement is placed around all side fo the column.
        /// </summary>
        protected void DetailType2()
        {
            List<double> AsList = new List<double>();
            List<double> usedBars = new List<double>();
            List<int> nList = new List<int>();
            int n;
            List<double> diamList = eDetailing.GetBarsBetwee(minDiam, maxDiam).ToList();
            n = Math.Min(GetMaxNumbOfBars(minDiam, b), GetMaxNumbOfBars(minDiam, h));
            for (int i = 2; i <= n; i++)
            {
                barsInX = barsInY = i;
                try
                {
                    DetailType3();
                    AsList.Add(AsTotal);
                    nList.Add(i);
                    usedBars.Add(mainBarDiam);
                }
                catch (Exception) { }
            }
            if (AsList.Count == 0)
                throw new Exception("Reinforcement Conjusted!");
            n = AsList.IndexOf(AsList.Min());
            barsInX = barsInY = nList[n];
            FillAss(usedBars[n]);
            SetAreasOfReinf(AsList[n] / NumberOfBars);
            mainBarDiam = usedBars[n];
        }

        protected void DetailType2(int i)
        {
          
        }

        private void InitializeDetailing()
        {
            SetHprimAndBprim(minDiam);
            Descritize();
            FillAss();
        }

        private int GetNumberOfBars(double diam, double AsTotal)
        {
            return (int)Math.Ceiling(As / GetBarArea(diam));
        }

        private int GetIncrement()
        {
            if (typeOfDetail == eDetailType.Type1)
                return 2;
            else if (typeOfDetail == eDetailType.Type2)
                return 2 * Math.Max(xN, yN) + 2;
            else return 0;
        }

        private int[] GenerateNumbers()
        {
            List<int> numbs = new List<int>();
            int Bmax = GetMaxNumbOfBars(minDiam, b);
            if (typeOfDetail == eDetailType.Type1)
                for (int i = 4; i <= Bmax * 2; i += 2)
                    numbs.Add(i);
            if (typeOfDetail == eDetailType.Type2)
                for (int i = Math.Max(yN, xN) * 4; i < 120; i += 2 * Math.Max(xN, yN) + 2)
                    numbs.Add(i);
            return (int[])numbs.ToArray();
        }

        protected void DetailType1(int i)
        {
            double diam = minDiam;
            double Ascalc;
            int n = 0;
            List<double> diamList = eDetailing.GetBarsBetwee(minDiam, maxDiam).ToList();
            do
            {
                SetHprimAndBprim(diam);
                FillAss();
                CalculateAs();
                if (typeOfDetail == eDetailType.Type1 || typeOfDetail == eDetailType.Type2)
                    diam = FindEcoDiam(ref n, AsTotal, diamList, GetIncrement());
                else
                    diam = GetEcoDiam(diamList, Ass[0].Area);
                if (n > GetMaxNumbOfBars(diam, b))
                {
                    diamList.RemoveRange(0, diamList.IndexOf(diam) + 1);
                    if (diamList.Count == 0)
                        throw new Exception("The reinforcements are conjusted.");
                    diam = diamList[0];
                }
            } while (mainBarDiam != diam);
            barsInX = n;
            Ascalc = AsTotal;
            FillAss(mainBarDiam);
            SetAreasOfReinf(Ascalc / NumberOfBars);
        }

        private double FindEcoDiam(ref int n, double AsTotal, List<double> diamList, int increment)
        {
            double diff = double.PositiveInfinity;
            int tempN;
            int smallN = 0;
            for (int i = 0; i < diamList.Count; i++)
            {
                n = GetNumbOfBar(diamList[i], AsTotal);
                tempN = (int)Math.Ceiling((double)n / increment) * increment;

                if (diff > GetBarArea(diamList[i]) * (tempN - n))
                {
                    diff = GetBarArea(diamList[i]) * (tempN - n);
                    smallN = i;
                }
            }
            if (double.IsPositiveInfinity(diff))
                throw new Exception("Bars are not present");

            n = GetNumbOfBar(diamList[smallN], AsTotal);
            n = (int)Math.Ceiling((double)n / increment) * increment;
            return diamList[smallN];
        }

        /// <summary>
        /// Desesigns the column section.
        /// </summary>
        public abstract void Design();

        protected abstract void DetailType1();

        /// <summary>
        /// Returns the reinforcements used in the design.
        /// </summary>
        /// <returns></returns>
        public float [,] GetReinforcements()
        {
            float[,] locns = new float[Ass.Length, 2];
            for (int i = 0; i < Ass.Length; i++)
            {
                locns[i, 0] = (float)(b / 2 + Ass[i].X);
                locns[i, 1] = (float)(h / 2 - Ass[i].Y);
            }
            return locns;
        }

        /// <summary>
        /// Returns the area of steel of each unit of reinforcement as assumed in the detailing.
        /// </summary>
        /// <returns></returns>
        public double GetUnitArea()
        {
            if (typeOfDetail == eDetailType.Type1)
                return AsTotal / 2;
            else if (typeOfDetail == eDetailType.Type2)
                return AsTotal / 4;
            else
                return As;
        }

        public abstract List<double> GetInteractionDiag(int nDiv, double r = double.PositiveInfinity, eAnalysisReinf reinf = eAnalysisReinf.AsCalculated);

        public abstract List<double> GetInteractionDiag(double div, int n, double r = double.PositiveInfinity, eAnalysisReinf reinf = eAnalysisReinf.AsCalculated);

        protected abstract void AddMdPd(List<double> values, eAnalysisReinf reinf, double r = double.PositiveInfinity);

        public void UseArea(eAnalysisReinf reinf)
        {
            if (reinf == eAnalysisReinf.AsCalculated)
                SetAreasOfReinf(AsCalc);
            else
                SetAreasOfReinf(GetBarArea(mainBarDiam));
        }

        public void SetHandBprim(double hprim, double bprim)
        {
            this.hprim = hprim;
            this.bprim = bprim;
            if (typeOfDetail == eDetailType.Type3 || typeOfDetail == eDetailType.Type1)
                FillAss();
        }

        /// <summary>
        /// Assigns all necessary data to bars distributed around the cornners of the cross section.
        /// The assigned data includes material,location and detailing requirments.
        /// </summary>
        protected void FillAss()
        {
            if (typeOfDetail == eDetailType.Type4)
                FillType4();

            if (nx == 1)
            {
                Ass = new eAs[2];
                Ass[0] = new eAs(b, h, 0, h / 2 - hprim, C.εc, C.εb, S);
                Ass[1] = new eAs(b, h, 0, -h / 2 + hprim, C.εc, C.εb, S);
                return;
            }

            
            double dy = (h - 2 * hprim) / (ny - 1);
            double dx = (b - 2 * bprim) / (nx - 1);
            Ass = new eAs[GetTotalNumbOfBars(nx, ny)];

            for (int i = 0; i < nx; i++)
            {
                Ass[i] = new eAs(b, h, -b / 2 + bprim + dx * i, h / 2 - hprim, C.εc, C.εb, S);
                Ass[nx + (ny - 1) - 1 + i] = new eAs(b, h, b / 2 - bprim - dx * i, -h / 2 + hprim, C.εc, C.εb, S);
            }
            for (int i = 1; i < ny - 1; i++)
            {
                Ass[nx - 1 + i] = new eAs(b, h, b / 2 - bprim, h / 2 - hprim - i * dy, C.εc, C.εb, S);
                Ass[Ass.Length - i] = new eAs(b, h, -b / 2 + bprim, h / 2 - hprim - i * dy, C.εc, C.εb, S);
            }
        }

        protected void Descritize()
        {
            switch (typeOfDetail)
            {
                case eDetailType.Type1:
                    if (action == eColumnAction.Biaxial)
                    {
                        nx = 24;
                        ny = 2;
                    }
                    else
                    {
                        nx = 1;
                        ny = 2;
                    }
                    break;
                case eDetailType.Type2:
                    nx = (int)(24 / (1 + BtoHRatio));
                    ny = 48 - 2 * barsInY;
                    break;
                case eDetailType.Type3:
                    nx = barsInX;
                    ny = barsInY;
                    break;
                case eDetailType.Type4:
                    nx = barsInX;
                    ny = barsInY;
                    break;
            }
        }

        protected void FillType4()
        {
            double db = (b - 2 * bprim) / (nx - 1);
            double dh = (h - 2 * hprim) / (ny - 1);
            Ass = new eAs[nx * ny];
            for (int i = 0; i < nx; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    Ass[i] = new eAs(b, h, -b / 2 + bprim + db * i, h / 2 - hprim - dh * j, C.εc, C.εb, S);
                }
            }
        }

        protected double GetAs(eReinfLocation location = eReinfLocation.TopOrBottom)
        {
            if (typeOfDetail == eDetailType.Type4 || typeOfDetail == eDetailType.Type3)
                return Ass[0].Area;
            if (location == eReinfLocation.TopOrBottom)
                return Ass[0].Area * nx;
            else if (location == eReinfLocation.LeftOrRight)
                return Ass[0].Area * ny;
            throw new Exception("No such type of detail is acceptable");
        }
        

        #endregion

    }
}
