using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Code;
using ESADS.Code.EBCS_1995;

namespace ESADS.Mechanics.Design.Column
{
    /// <summary>
    /// Represetns columns subjected to biaxial loading reinforcemetns being descritized around the corners of the section.
    /// </summary>
    public class eBiaxial : eDColumn
    {
        #region Fields
        private double my;
        private double r;
        #endregion

        #region Constructors

        public eBiaxial(double b, double h, eConcrete C, eSteel S, double p, double mx, double my, int barsInX, int barsInY)
            : base(b, h, C, S, p, mx, barsInX, barsInY)
        {
            this.my = my;
            this.typeOfDetail = eDetailType.Type3;
            this.action = eColumnAction.Biaxial;
        }

        public eBiaxial(double b, double h, eConcrete C, eSteel S, double p, double mx, double my,eDetailType typeOfDetail)
            : base(b, h, C, S, p, mx,typeOfDetail)
        {
            this.my = my;
            this.action = eColumnAction.Biaxial;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the design moment in X direction.
        /// </summary>
        public double R
        {
            get { return mx / my; }
        }

        /// <summary>
        /// Gets or sets the design moment in Y direction.
        /// </summary>
        public double My
        {
            get { return my; }
            set { my = value; }
        }
        #endregion

        #region Methods


        private void InitializeAs(out double As1, out double As2, out double Mx1, out double Mx2)
        {
            As1 = GetAsMin();
            As2 = GetAsMax();
            double As;
            SetAreasOfReinf(As1);
            if (GetMaxAxialCapacity() > p)
            {
                Mx1 = GetMxAt(p, mx / my);
                if (Mx1 > mx)
                {
                    compState = eCompletionState.UnderReinforcedFailur;
                    Mx2 = 0;
                    return;
                }
            }
            else
            {
                As1 = 1.5 * GetAxialAs();
                SetAreasOfReinf(As1);
                Mx1 = GetMxAt(p, mx / my);
            }

            SetAreasOfReinf(As2);
            Mx2 = GetMxAt(p, mx / my);
            if (Mx2 < mx)
            {
                compState = eCompletionState.OverReinforcedFailur;
                return;
            }

            As = As2;
            As2 = Interpolate(As1, As2, Mx1, Mx2, mx);
            As1 = As;
            SetAreasOfReinf(As2);
            Mx1 = Mx2;
            Mx2 = GetMxAt(p, mx / my);
        }

        private void InitializeTeta(out double teta, out double r, double x, double acR)
        {
            double tetaO = Math.Atan(acR);
            SetXandTeta(x, tetaO);
            r = GetMx(x, tetaO) / GetMy(x, tetaO);
            teta = GetTeta(tetaO, r,acR);
            SetXandTeta(x, teta);
            r = GetMx(x, teta) / GetMy(x, teta);
        }

        private double GetTeta(double teta1, double r1,double acR)
        {
            double a, b;
            a = r1 * teta1 / (1 - 2 * teta1 / Math.PI);
            b = -2 * a / Math.PI;
            return a / (acR - b);
        }

        private void ChangeAsAndM(ref double As1, ref double As2, ref double Mx1, ref double Mx2)
        {
            double As;
            As = As2;
            As2 = Interpolate(As1, As2, Mx1, Mx2, mx);
            As1 = As;
            SetAreasOfReinf(As2);
            Mx1 = Mx2;
            Mx2 = GetMxAt(p, mx / my);
        }
       
        /// <summary>
        /// Returns the Moment caried in y direction by the section.
        /// </summary>
        /// <returns></returns>
        private double GetMy(double x, double teta)
        {
            SetXandTeta(x, teta);
            double my = conc.My;
            for (int i = 0; i < Ass.Length; i++)
            {
                my += Ass[i].My;
            }
            return Math.Abs(my);
        }

        /// <summary>
        /// Returns the axial load carried by the section.
        /// </summary>
        /// <returns></returns>
        private double GetP(double x,double teta)
        {

            SetXandTeta(x, teta);
            double p = conc.NC;
            for (int i = 0; i < Ass.Length; i++)
            {
                p += Ass[i].NS;
            }
            return Math.Abs(p);
        }

        private double GetR(double x, double teta)
        {
            return GetMx(x, teta) / GetMy(x, teta);
        }

        public double GetMxAt(double p, double r)
        {
            double tempR, teta, x = h;
            InitializeTetaAndR(out teta, out tempR, x, r);
            do
            {
                x = GetX(p, teta);
                teta = GetTeta(teta, tempR, r);
                tempR = GetR(x, teta);
            } while (CheckPrecision(tempR, r, 6));
            return GetMx(x, teta);
        }

        public double GetMxAt(double p, double r, ref double xxxx, ref double ttt )
        {
            double tempR, teta, x = h;
            InitializeTetaAndR(out teta, out tempR, x, r);
            do
            {
                x = GetX(p, teta);
                teta = GetTeta(teta, tempR, r);
                tempR = GetR(x, teta);
            } while (CheckPrecision(tempR, r, 6));
            ttt = teta;
            xxxx = x;
            return GetMx(x, teta);
        }

        private double GetX(double p, double teta)
        {
            double x1, x2, p1, p2;
            InitializeXandP(out x1, out x2, out p1, out p2, p, teta);
            while (CheckPrecision(p2, p, precision))
                ChangeXandP(ref x1, ref x2, ref p1, ref p2, p, teta);
            return x2;
        }

        public override void CalculateAs()
        {
            compState = eCompletionState.NotDesigned;
            double As1, As2, Mx1, Mx2;
            InitializeAs(out As1, out As2, out Mx1, out Mx2);
            if (compState == eCompletionState.UnderReinforcedFailur || compState == eCompletionState.OverReinforcedFailur)
                return;
            while (CheckPrecision(Mx2, mx, precision))
                ChangeAsAndM(ref As1, ref As2, ref Mx1, ref Mx2);
            AsCalc = As;
        }

        private void ChangeTetaAndR(ref double teta1, ref double teta2, ref double r1, ref double r2, double x, double acR)
        {
            double a, b;
            a = (r1 - r2) * teta1 * teta2 / (teta2 - teta1);
            b = r1 - a / teta1;
            teta1 = teta2;
            teta2 = a / (acR - b);
            if (teta2 > Math.PI / 2)
                teta2 = GetTeta(teta1, r2, acR);
            r1 = r2;
            r2 = GetMx(x, teta2) / GetMy(x, teta2);
        }

        private void InitializeTetaAndR(out double teta, out double r, double x, double acR)
        {
            double tt = Math.Atan(acR);

            teta = GetTeta(tt, GetR(x,tt), acR);
            r = GetR(x, teta);
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
                    barsInX = 16;
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
            {
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
        }

        /// <summary>
        /// Performs a reinforcement detailing and design revision for Column DetailType-1.
        /// DetailType-1 is a type of column detail in which uniformly distributed 
        /// reinforcement is placed at the top and bottom side of the column.
        /// </summary>
        protected override void DetailType1()
        {
            List<double> AsList = new List<double>();
            List<double> usedBars = new List<double>();
            List<int> nList = new List<int>();
            int n;
            barsInY = 2;
            List<double> diamList = eDetailing.GetBarsBetwee(minDiam, maxDiam).ToList();
            n = GetMaxNumbOfBars(minDiam, b);
            for (int i = 2; i <= n; i++)
            {
                barsInX = i;
                try
                {
                    DetailType3();
                    AsList.Add(AsTotal);
                    nList.Add(i);
                    usedBars.Add(mainBarDiam);
                }
                catch (Exception) { }
            }
            n = AsList.IndexOf(AsList.Min());
            barsInX = nList[n];
            mainBarDiam = usedBars[n];
            FillAss(mainBarDiam);
            AsCalc = AsList[n] / NumberOfBars;
            SetAreasOfReinf(AsCalc);
        }

        public override List<double> GetInteractionDiag(double pDiff, int n, double r = double.PositiveInfinity, eAnalysisReinf reinf = eAnalysisReinf.AsCalculated)
        {
            List<double> diag = new List<double>();

            if (reinf == eAnalysisReinf.AsProvided)
            {
                if (desingAndDetail)
                    SetAreasOfReinf(GetBarArea(mainBarDiam));
                else
                    SetAreasOfReinf(AsCalc);
            }
            double maxP = GetMaxAxialCapacity();

            for (int i = 0; i < n; i++)
            {
                diag.Add(pDiff * i);
                diag.Add(GetMxAt(pDiff * i, r));
            }
            diag.Add(maxP);
            diag.Add(0);
            AddMdPd(diag, reinf, r);
            SetAreasOfReinf(AsCalc);
            return diag;
        }

        public override List<double> GetInteractionDiag(int nDiv, double r = double.PositiveInfinity, eAnalysisReinf reinf = eAnalysisReinf.AsCalculated)
        {
            return GetInteractionDiag(GetMaxAxialCapacity(reinf) / nDiv, nDiv, r, reinf);
        }

        protected override void AddMdPd(List<double> values, eAnalysisReinf reinf, double r = double.PositiveInfinity)
        {
            for (int i = 0; i < values.Count / 2; i++)
            {
                if (p < values[2 * i] && reinf == eAnalysisReinf.AsCalculated && compState != eCompletionState.UnderReinforcedFailur && r == mx / my)
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
