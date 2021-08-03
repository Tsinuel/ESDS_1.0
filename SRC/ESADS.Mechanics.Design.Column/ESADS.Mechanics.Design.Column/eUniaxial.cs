using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Code;
using ESADS.Code.EBCS_1995;

namespace ESADS.Mechanics.Design.Column
{
    /// <summary>
    /// Represents a rectangular column having detailing requirment specified in EBCS-2 Part-2 ChartNo.1-5(one upto five).
    /// </summary>
    public class eUniaxial : eDColumn
    {
        #region Costructors

        public eUniaxial(double b, double h, eConcrete C, eSteel S, double p, double mx, eDetailType typeOfDetail)
            : base(b, h, C, S, p, mx, typeOfDetail)
        {
            this.barsInX = 1;
            this.barsInY = 0;
            this.action = eColumnAction.Uniaxial;
        }

        public eUniaxial(double b, double h, eConcrete C, eSteel S, double p, double mx, int barsInX, int barsInY)
            : base(b, h, C, S, p, mx, barsInX, barsInY)
        {
            this.action = eColumnAction.Uniaxial;
        }
        #endregion 

        #region Methods
    
        /// <summary>
        /// Initializes the area of steel used in the cross section.
        /// </summary>
        /// <param name="As1">Value to be passed by reference for initialising arae of seel used in the previous iteration.</param>
        private void InitializeAs(out double As1, out double As2, out double M1, out double M2)
        {
            As1 = Math.Max(GetAsMin(), 1.5 * GetAxialAs());
            As2 = GetAsMax();
            double m, As;
            SetAreasOfReinf(As1);
            M1 = GetM(p);
            if (M1 > mx)
            {
                compState = eCompletionState.UnderReinforcedFailur;
                M2 = 0;
                return;
            }
            SetAreasOfReinf(As2);
            M2 = GetM(p);
            if (M2 < mx)
            {
                compState = eCompletionState.OverReinforcedFailur;
                return;
            }
            As = Interpolate(As1, As2, M1, M2, mx);
            SetAreasOfReinf(As);
            m = GetM(p);
            Interchange(ref As1, ref As2, ref M1, ref M2, As, m, mx);
        }

        /// <summary>
        /// Returns the depth of nutral axis for the given axial load.
        /// </summary>
        /// <param name="p">Applied axial load.</param>
        /// <returns></returns>
        public double GetX(double p)
        {
            double x1, x2, p1, p2;
            InitializeXandP(out x1, out x2, out p1, out p2, p);

            while (CheckPrecision(p2, p, precision))
            {
                ChangeXandP(ref x1, ref  x2, ref p1, ref p2, p);
            }
            return x2;
        }

        /// <summary>
        /// Returns the moment capacity at the given axial load in the interaction surface.
        /// </summary>
        /// <param name="p">Applied axial load.</param>
        /// <returns></returns>
        public double GetM(double p)
        {
            if (p > GetMaxAxialCapacity())
                throw new Exception("The applied load is above the pure axial capacity.");
            else if (Math.Round(p, precision) == Math.Round(GetMaxAxialCapacity(), precision))
                return 0;
            return GetMx(GetX(p));
        }

        public override void CalculateAs()
        {
            compState = eCompletionState.NotDesigned;
            double As1, As2, Mx1, Mx2;
            InitializeAs(out As1, out As2, out Mx1, out Mx2);

            if (compState == eCompletionState.UnderReinforcedFailur||compState == eCompletionState.OverReinforcedFailur)
                return;

            while(CheckPrecision(Mx2, mx, precision))
            {
                ChangeAsAndM(ref As1, ref As2, ref Mx1, ref Mx2);                               
            }
        }

        /// <summary>
        /// Details the column section.
        /// </summary>
        public override void Design()
        {
            if (!desingAndDetail)
            {
                if (typeOfDetail == eDetailType.Type1)
                {
                    barsInX = 1;
                    barsInY = 2;
                }
                else if (typeOfDetail == eDetailType.Type2)
                {
                    barsInX = 9;
                    barsInY = 9;
                }
                FillAss();
                CalculateAs();
                AsCalc = Ass[0].Area;
            }
            else
                switch (typeOfDetail)
                {
                    case eDetailType.Type1:
                        DetailType1();
                        break;
                    case eDetailType.Type2:
                        DetailType2();
                        break;
                    case eDetailType.Type3:
                        DetailType3();
                        break;
                    case eDetailType.Type4:
                        DetailType3();
                        break;
                }
        }

        /// <summary>
        /// Performs a reinforcement detailing and design revision for Column DetailType-1.
        /// DetailType-1 is a type of column detail in which uniformly distributed 
        /// reinforcement is placed at the top and bottom side of the column.
        /// </summary>
        protected override void DetailType1()
        {
            barsInX = 1;
            barsInY = 2;
            double diam = minDiam;
            int n;
            List<double> diamList = eDetailing.GetBarsBetwee(minDiam, maxDiam).ToList();
            do
            {
                FillAss(diam);
                CalculateAs();
                mainBarDiam = diam;
                diam = GetEcoDiam(out n, diamList, As);
                if (n > GetMaxNumbOfBars(diam, b))
                {
                    diamList.RemoveRange(0, diamList.IndexOf(diam) + 1);
                    if (diamList.Count == 0)
                        throw new Exception("The reinforcements are conjusted.");
                    diam = diamList[0];
                }
            } while (mainBarDiam != diam);
            barsInX = n;
            AsCalc = Ass[0].Area / n;
            FillAss(mainBarDiam);
            SetAreasOfReinf(AsCalc);

        }

        private void ChangeAsAndM(ref double As1, ref double As2, ref double M1, ref double M2)
        {
            double As;
            As = As2;
            As2 = Interpolate(As1, As2, M1, M2, mx);
            As1 = As;
            SetAreasOfReinf(As2);
            M1 = M2;
            M2 = GetM(p);
        }
        public override List<double> GetInteractionDiag(double pDiff, int n, double r = double .PositiveInfinity, eAnalysisReinf reinf = eAnalysisReinf.AsCalculated)
        {
            List<double> diag = new List<double>();
            double Pb = GetPb();
            double nDiff;
            int compN, tensN;
            if (reinf == eAnalysisReinf.AsProvided)
            {
                if (desingAndDetail)
                    SetAreasOfReinf(GetBarArea(mainBarDiam));
                else
                    SetAreasOfReinf(AsCalc);
            }

            GetCompAndTensDivisions(out compN, out tensN, pDiff);
            nDiff = Pb / tensN;

            for (int i = 0; i < tensN + 1; i++)
            {
                diag.Add(nDiff * i);
                diag.Add(GetM(nDiff * i));
            }
            nDiff = (GetMaxAxialCapacity() - Pb) / compN;
            for (int i = 1; i < compN; i++)
            {
                diag.Add(Pb + nDiff * i);
                diag.Add(GetM(Pb + nDiff * i));
            }
            diag.Add(GetMaxAxialCapacity());
            diag.Add(0);
            AddMdPd(diag, reinf);
            SetAreasOfReinf(AsCalc);
            return diag;
        }

        private void GetCompAndTensDivisions(out int compN, out int tensN, double pDiff)
        {
            double Pb = GetPb();
            compN = (int)Math.Ceiling((GetMaxAxialCapacity() - Pb) / pDiff);
            tensN = (int)Math.Ceiling(Pb / pDiff);
        }

        public override List<double> GetInteractionDiag(int nDiv, double r = double.PositiveInfinity, eAnalysisReinf reinf = eAnalysisReinf.AsCalculated)
        {
            return GetInteractionDiag(GetMaxAxialCapacity(reinf) / nDiv, nDiv, r, reinf);
        }

        private double GetXb()
        {
            double yFar = Ass[0].Y;
            
            for (int i = 0; i < Ass.Length; i++)
            {
                if (Ass[i].Y < yFar)
                    yFar = Ass[i].Y;
            }
            return (h / 2 + Math.Abs(yFar)) * C.εb / (S.εs + C.εb);           
        }

        /// <summary>
        /// Returns the balaced moment capacity of the section;
        /// </summary>
        /// <returns></returns>
        public double GetMb()
        {
            return GetMx(GetXb());
        }

        /// <summary>
        /// Returns the balanced axial load capacity of the section.
        /// </summary>
        /// <returns></returns>
        public double GetPb()
        {
            return GetPatX(GetXb());
        }

        protected override void AddMdPd(List<double> values, eAnalysisReinf reinf, double r = double.PositiveInfinity)
        {
            for (int i = 0; i < values.Count / 2; i++)
            {
                if (p < values[2 * i] && reinf == eAnalysisReinf.AsCalculated && compState != eCompletionState.UnderReinforcedFailur)
                {
                    values.Insert(2 * i, p);
                    values.Insert(2 * i + 1, mx);
                    break;
                }
            }
        }
        #endregion      
    }
}
