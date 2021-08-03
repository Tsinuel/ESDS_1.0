using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Code;
using ESADS.Code.EBCS_1995;

namespace ESADS.Mechanics.Design.Beam
{
    /// <summary>
    /// Contains information related to combination of longitudinal bar1 in rows along an effective width.
    /// </summary>
    [Serializable]
    public partial class eCombination
    {
        #region Feilds
        /// <summary>
        /// Accesses the public property 'Rows'.
        /// </summary>
        private eRow[] rows;
        /// <summary>
        /// Accesses the public property 'Area'.
        /// </summary>
        private double area;
        /// <summary>
        /// Accesses the public property 'EffectiveDepth'.
        /// </summary>
        private double effectivedepth;
        /// <summary>
        /// Holds the section for which shearBar arrangment is being done.
        /// </summary>
        private eDFlexureSection section;
        /// <summary>
        /// Holds the value of 'LongtBar1'.
        /// </summary>
        private eLongtBar longtBar1;
        /// <summary>
        /// Holds the value of 'LongtBar2'.
        /// </summary>
        private eLongtBar longtBar2;
        private bool isInCompression;
        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance of eCombination class provided all the rows in a combination.
        /// </summary>
        ///<param name="maxBarDiam">The diameter of the maximum shearBar diameter to be used in the combination.</param>
        ///<param name="minBarDiam">The diameter of the minimum shearBar diameter to be used in the combination.</param>
        ///<param name="section">The section for which the shearBar combination is going to be done.</param>
        public eCombination(eDFlexureSection section, bool isInCompression)
        {
            this.section = section;
            this.isInCompression = isInCompression;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minBarDiam"></param>
        /// <param name="maxBarDiam"></param>
        /// <param name="section"></param>
        /// <param name="extendedBars"></param>
        public eCombination(eDFlexureSection section, eXBar[] extendedBars, bool isInCompression)
        {
            this.section = section;
            this.isInCompression = isInCompression;
        }

        #endregion

        #region Properties
           
        /// <summary>
        /// Gets the all the Rows in the Combination.
        /// </summary>
        public eRow[] Rows
        {
            get
            {
                if (rows != null)
                    return rows;
                else
                    return new eRow[0];
            }
        }

        /// <summary>
        /// Gets the area of the Combination.
        /// </summary>
        public double Area
        {
            get { return area; }
        }

        /// <summary>
        /// Gets the Effective Width of the Combination.
        /// </summary>
        public double EffectiveDepth
        {
            get { return effectivedepth; }
        }

        /// <summary>
        /// Gets or sets the flexure section that bears this combination.
        /// </summary>
        public eDFlexureSection Section
        {
            get
            {
                return section;
            }
            set
            {
                section = value;
            }
        }

        /// <summary>
        /// Gets the longitudinal bar that represents all the bars with barType1.
        /// </summary>
        public eLongtBar LongtBar1
        {
            get
            {
                return longtBar1;
            }
        }

        /// <summary>
        /// Gets the longitudinal bar that represents all the bars with barType2.
        /// </summary>
        public eLongtBar LongtBar2
        {
            get
            {
                return longtBar2;
            }
        }

        /// <summary>
        /// Gets the number of bar type 1 found in the combination.
        /// </summary>
        public int NumOfBar1
        {
            get
            {
                int res = 0;
                foreach (var r in this.rows)
                    res += r.NumOfBar1;
                return res;
            }
        }

        /// <summary>
        /// Gets the number of bar type 2 found in the combination.
        /// </summary>
        public int NumOfBar2
        {
            get
            {
                int res = 0;
                foreach (var r in this.rows)
                    res += r.NumOfBar2;
                return res;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Fills the Area property of a Combination.
        /// </summary>
        private void FillArea()
        {
            area = 0.0;

            for (int i = 0; i < rows.Length; i++)
            {
                area += rows[i].Area;
            }
        }

        /// <summary>
        /// Fills the Effective Width property of a combination.
        /// </summary>
        private void FillEffectiveDepth()
        {
            double momentOfArea = 0;// contains the moments of area of a combination from a speciefed point of reference.
            //Calculates the moment of area by adding the moment of area of each row.
            for (int i = 0; i < rows.Length; i++)
            {
                momentOfArea += rows[i].Area * rows[i].EffectiveDepth;
            }
            //computes the center of mass for a given set of rows.
            effectivedepth = momentOfArea / area;

            if (double.IsNaN(effectivedepth))
                effectivedepth = 0.0;
        }

        /// <summary>
        /// Fills the most economical combination.
        /// </summary>
        /// <param name="AsCalc">Calculated area of steel.</param>
        /// <param name="AsMax">Maximum area of steel.</param>
        public void FillCombinations(double AsCalc, double AsMax, double effDepth)
        {
            double[] comb = new double[4];
            double AsProvided = AsMax;
            AsCalc = Math.Max(AsCalc, 0);

            if (section.Beam.UseTwoBars)
                comb = FillCombinations(section.Bar2, section.Bar1, AsCalc, AsMax);
            else
                comb = FillCombinations(section.Bar1, AsCalc);

            ArrangeIntoRows(comb);
            FillArea();
            FillEffectiveDepth();
        }

        /// <summary>
        /// Returns the most economical combination for a given shearBar diameters and calculated area of steel.
        /// </summary>
        /// <param name="diam1">Diameter of the first shearBar.</param>
        /// <param name="diam2">Diameter of the second shearBar.</param>
        /// <param name="AsCalc">Calculated area of steel for which combinations are going to be generated.</param>
        /// <param name="AsMax">Maximum area of steel.</param>
        private double[] FillCombinations(double diam1, double diam2, double AsCalc, double AsMax)
        {
            if (diam2 > diam1)  //making sure that diam1 is the bigger Bar
            {
                double temp = diam1;
                diam1 = diam2;
                diam2 = temp;
            }

            double AsProvided = AsMax;
            double[] comb = new double[4];
            int numbOfDiam1 = eUtility.RoundUp(AsCalc / eXBar.GetArea(diam1));
            int numbOfDiam2;
            for (int i = 0; i <= numbOfDiam1; i++)
            {
                numbOfDiam2 = eUtility.RoundUp((AsCalc - i * eXBar.GetArea(diam1)) / eXBar.GetArea(diam2));
                if (GetArea(new double[4] { diam1, diam2, i, numbOfDiam2 }) < AsProvided)
                {
                    comb = new double[4] { diam1, diam2, i, numbOfDiam2 };
                    AsProvided = GetArea(comb);
                }
            }

            if (section.Beam.UseCornerBars)
            {
                if (comb[2] == 0 && comb[3] == 0)
                    comb[3] += 2;
                if (comb[2] == 0 && comb[3] == 1)
                    comb[3]++;
                if (comb[2] == 1 && comb[3] == 0)
                    comb[2]++;
            }

            if (AsMax < AsProvided)
                section.IsOverReninforced = true;

            return comb;
        }

        /// <summary>
        /// Returns the most economical combination for a given shearBar diameters and calculated area of steel.
        /// </summary>
        /// <param name="diam1">Diameter of the first shearBar.</param>
        /// <param name="AsCalc">Calculated area of steel for which combinations are going to be generated.</param>
        /// <param name="AsMax">Maximum area of steel.</param>
        private double[] FillCombinations(double diam1, double AsCalc)
        {
            int numbOfDiam1 = eUtility.RoundUp(AsCalc / eXBar.GetArea(diam1));

            if (section.Beam.UseCornerBars && numbOfDiam1 < 2)
                numbOfDiam1 = 2;

            return new double[] { diam1, 0.0, numbOfDiam1, 0.0 };
        }

        /// <summary>
        /// Arranges a given combination in to rows.
        /// </summary>
        /// <param name="comb">Combination to be arranged. The array should have four elements, the first two, diameters of the involved shearBar, the other two, 
        /// their respective number.</param>
        private void ArrangeIntoRows(double[] comb)
        {
            List<double[]> rows = new List<double[]>();
            double minSpacing = eDetailing.GetMinSpacing(comb[1], section.Beam.MaxAggSize);
            int numbOfDiam1, numbOfDiam2;

            int n1 = (int)comb[2], n2 = (int)comb[3];

            double effWidth = section.Width - 2 * (section.Beam.Cover + eXBar.GetDiam(section.Beam.StirupBar));

            while (n1 > 0)
            {
                CheckCapacity(rows, effWidth, minSpacing);

                if (rows.Count == 1)
                    numbOfDiam1 = Math.Min(n1, GetCapacity(rows[rows.Count - 1], comb[0], effWidth, minSpacing));
                else
                    numbOfDiam1 = Math.Min((int)(rows[0][2] + rows[0][3]), n1);

                rows[rows.Count - 1][0] = comb[0];
                rows[rows.Count - 1][2] = numbOfDiam1;

                n1 -= numbOfDiam1;
            }

            if (section.Beam.UseTwoBars)
            {
                while (n2 > 0)
                {
                    CheckCapacity(rows, effWidth, minSpacing);

                    if (rows.Count == 1)
                        numbOfDiam2 = Math.Min(n2, GetCapacity(rows[rows.Count - 1], comb[1], effWidth, minSpacing));
                    else
                        numbOfDiam2 = Math.Min((int)(rows[0][2] + rows[0][3] - rows[rows.Count - 1][2]), n2);

                    rows[rows.Count - 1][1] = comb[1];
                    rows[rows.Count - 1][3] = numbOfDiam2;

                    n2 -= numbOfDiam2;
                }
            }

            RefineRows(rows);
            FillRows(rows, minSpacing);
        }

        private void RefineRows(List<double[]> rows)
        {
            if (rows.Count == 0)
                return;
            //Making sure that each row has a maximum of one Bar bartype of odd number
            for (int i = 0; i < rows.Count; i++)
            {
                if (rows[i][2] % 2 == 1 && rows[i][3] % 2 == 1)
                {
                    if (rows.Count == 1) //Just change one of the smaller Bar to bigger one if there is only one row
                    {
                        rows[0][2]++;
                        rows[0][3]--;
                    }
                    else    
                    {
                        for (int j = 0; j < rows.Count; j++)    //search for other row which has more than one odd number
                        {
                            if (i == j)
                                continue;
                            if (rows[j][2] % 2 == 1 && rows[j][3] % 2 == 1)
                            {
                                if (i > j)  //more larger Bar to lower row
                                {
                                    rows[i][2]--;
                                    rows[i][3]++;
                                    rows[j][2]++;
                                    rows[j][3]--;
                                }
                                else
                                {
                                    rows[i][2]++;
                                    rows[i][3]--;
                                    rows[j][2]--;
                                    rows[j][3]++;
                                }
                                break;
                            }
                        }

                        if (rows[i][2] % 2 == 1 && rows[i][3] % 2 == 1)     //If it is still not corrected
                        {
                            if (i == 0 || rows[i - 1][2] % 2 == 0 && rows[i - 1][3] % 2 == 1)     //If the row immediately following has both numbers even, it shouldn't be touched, so this row should adjust by itself
                            {
                                rows[i][2]--;
                                rows[i][3]++;
                            }
                            else
                            {
                                rows[i][2]--;
                                rows[i][3]++;
                                rows[i - 1][2]++;
                                rows[i - 1][3]--;
                            }
                        }
                    }
                }
            }

            //
            //Ensuring that the last row at least contains one Bar
            //

            if (rows[rows.Count - 1][2] + rows[rows.Count - 1][3] == 1)
            {
                if (rows.Count == 1)
                {
                    if (rows[0][2] == 1)
                        rows[0][2]++;
                    else
                        rows[0][3]++;
                }
                else
                {
                    if (rows[rows.Count - 1][2] == 1)
                    {
                        rows[rows.Count - 1][2]++;
                        rows[rows.Count - 2][2]--;
                    }
                    else
                    {
                        rows[rows.Count - 1][3]++;
                        rows[rows.Count - 2][3]--;
                    }
                }
            }
        }

        private int GetCapacity(double[] row, double diam, double effWidth, double min_S)
        {
            double remWidth = effWidth - (min_S * (row[2] + row[3]) + row[0] * row[2] + row[1] * row[3]);

            return (int)((remWidth + min_S) / (diam + min_S));
        }

        private void CheckCapacity(List<double[]> rows, double effWidth, double min_S)
        {
            if (rows == null)
                rows = new List<double[]>();
            if (rows.Count == 0)
            {
                rows.Add(new double[4]);
                return;
            }

            if (GetCapacity(rows[rows.Count - 1], rows[rows.Count - 1][0], effWidth, min_S) == 0 && GetCapacity(rows[rows.Count - 1], rows[rows.Count - 1][1], effWidth, min_S) == 0)
            {
                rows.Add(new double[4]);
                return;
            }

        }

        /// <summary>
        /// Fills the rows of the combination.
        /// </summary>
        /// <param name="rows">List containing information related to the rows.</param>
        /// <param name="minSpacing">The minimum spacing shearBar.</param>
        private void FillRows(List<double[]> rows, double minSpacing)
        {
            double common_Y = section.Depth - section.Beam.Cover - eXBar.GetDiam(section.Beam.StirupBar);
            //double common_Y = 0.0;

            this.rows = new eRow[rows.Count];
            for (int i = 0; i < this.rows.Length; i++)
            {
                if (i == 0)
                {
                    this.rows[i] = new eRow(new eXBar(rows[i][0]), new eXBar(rows[i][1]), (int)rows[i][2], (int)rows[i][3], common_Y, true, minSpacing, section.GetEffWidth(), section.Beam.StirrupPosn);
                    this.rows[i].RowNumb = i + 1;
                    this.rows[i].Arrange();
                }
                else
                {
                    this.rows[i] = new eRow(new eXBar(rows[i][0]), new eXBar(rows[i][1]), (int)rows[i][2], (int)rows[i][3], common_Y, false, minSpacing, section.GetEffWidth(), section.Beam.StirrupPosn);
                    this.rows[i].RowNumb = i + 1;
                    this.rows[i].Arrange(this.rows[i - 1].GetXcoordinates());
                }

                if (section.Beam.StirrupPosn == eRelativeStirrupPosition.StirrupAtBottom)
                    common_Y -= (Math.Max(rows[i][2], rows[i][3]) + minSpacing + eXBar.GetDiam(section.Beam.StirupBar));
                else if (rows.Count - 1 > i)
                {
                    if (i == 0)
                        common_Y -= (Math.Max(rows[i][2], rows[i][3]) + Math.Max(rows[i + 1][2], rows[i + 1][3]) + minSpacing);
                    else
                        common_Y -= (Math.Max(rows[i + 1][2], rows[i + 1][3]) + minSpacing + eXBar.GetDiam(section.Beam.StirupBar));
                }
            }
        }

        /// <summary>
        /// Returns the area of shearBar for a given combination.
        /// </summary>
        ///<param name="comb">Combination for which the area is to be calculated.</param>
        private double GetArea(double[] comb)
        {
            return eXBar.GetArea(comb[0]) * comb[2] + eXBar.GetArea(comb[1]) * comb[3];
        }

        /// <summary>
        /// Returns the nubmer of maximum possible shearBar that can be arraged in the given effective width.
        /// </summary>
        /// <param name="b_eff">Effective width on which the shearBar to be arranged.</param>
        /// <param name="diam">Diameter of the shearBar to be arranged.</param>
        /// <param name="minSpacing">Minimum spacing of shearBar.</param>
        /// <returns></returns>
        private int GetBarPerWidth(double b_eff, double diam, double minSpacing, int availableBars)
        {
            int numb = (int)((b_eff + minSpacing) / (minSpacing + diam));
            return numb > availableBars ? availableBars : numb;
        }

        /// <summary>
        /// Returns the effective width on which shearBar can be distrubuted.
        /// </summary>
        /// <param name="diam">Diameter of the shearBar to be distributed.</param>
        /// <param name="minSpacing">Minimum clear spacing between two shearBar.</param>
        /// <param name="initialWidth">Initial width where the shearBar are distributed.</param>
        /// <param name="numOfBars">Number of shearBar distributed in the intitial width.</param>
        /// <returns></returns>
        private double GetRemainingWidth(double diam, double minSpacing, double initialWidth, int numOfBars)
        {
            return initialWidth - (diam * numOfBars + numOfBars * minSpacing);
        }

        /// <summary>
        /// Checks if the effective depth of the last row is below the nutral axis depth.
        /// </summary>
        /// <param name="X">Location of nutral axis.</param>
        /// <returns></returns>
        public bool CheckConjustion(double X)
        {
            if (rows.Length == 0)
                return false;
            if (X > (section.Depth - (rows[rows.Length - 1].EffectiveDepth + section.Beam.Cover + eXBar.GetDiam(section.Beam.StirupBar))))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Gets the number of the specified bar diameter in this combination.
        /// </summary>
        /// <param name="D">The diameter of the bar to be counted.</param>
        public int GetNumberOf(double D)
        {
            if (D != section.Bar1 && D != section.Bar2)
                return 0;

            int num = 0;

            foreach (eRow r in this.rows)
            {
                foreach (eXBar b in r.Bars)
                {
                    if (D == b.Diameter)
                        num++;
                }
            }

            return num;
        }

        /// <summary>
        /// Fills the longitudinal bars that represent the combination.
        /// </summary>
        internal void FillLongtBars()
        {
            if (this.rows == null)
                return;

            int count1 = 0, count2 = 0;

            foreach (var r in this.rows)
            {
                foreach (var b in r.Bars)
                {
                    if (b.Diameter == section.Bar1)
                        count1++;
                    else if (b.Diameter == section.Bar2)
                        count2++;
                }
            }

            this.longtBar1 = new eLongtBar(section.Bar1, section, count1, this.isInCompression, !section.IsNegative, section == section.Member.SupportSxn_Left);
            this.longtBar2 = new eLongtBar(section.Bar2, section, count2, this.isInCompression, !section.IsNegative, section == section.Member.SupportSxn_Left);
        }

        public override string ToString()
        {
            return this.NumOfBar1.ToString() + "Φ" + section.Bar1.ToString() + ", " + NumOfBar2.ToString() + "Φ" + section.Bar2.ToString();
        }
        #endregion
    }
}
