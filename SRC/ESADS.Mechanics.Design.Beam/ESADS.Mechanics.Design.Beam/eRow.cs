using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Design.Beam
{
    public partial class eCombination
    {
        /// <summary>
        /// Contains information related a row of longitudinal barType1 residing in a combination.
        /// </summary>
        [Serializable]
        public struct eRow
        {
            #region Fields
            /// <summary>
            /// Holds the value of the property 'BarType1'.
            /// </summary>
            private eXBar barType1;
            /// <summary>
            /// Holds the value of the 'BarType2' property.
            /// </summary>
            private eXBar barType2;
            /// <summary>
            /// The effective width on which to place all the shearBar of the row together with the spacing between them.
            /// </summary>
            private double effectiveWidth;
            /// <summary>
            /// All the shearBar of the row.
            /// </summary>
            private eXBar[] bars;
            private double commonY;
            private eRelativeStirrupPosition stirrupPosition;
            /// <summary>
            /// Holds the value of the 'IsBottomRow' property.
            /// </summary>
            private bool isBottomRow;
            private double[] xCoordinates;
            private int numOfBar1;
            private int numOfBar2;
            private double minSpacing;
            private int rowNumb;
            #endregion

            #region Constructors
            /// <summary>
            /// Creates an instance of 'eRow' class given X-Coordinates , Y- Coordinates and used diameters in a row.
            /// </summary>
            /// <param name="barType1">The bigger type of shearBar used in the row.</param>
            /// <param name="barType2">The smaller type of the shearBar to be used in the row.</param>
            public eRow(eXBar barType1, eXBar barType2, int numOfBarType1, int numOfBarType2, double commonY, bool isBottomRow, double minSpacing,
                double effectiveWidth, eRelativeStirrupPosition stirrupPosition)
            {
                this = new eRow();

                this.barType1 = barType1;
                this.barType2 = barType2;
                this.numOfBar1 = numOfBarType1;
                this.numOfBar2 = numOfBarType2;
                this.commonY = commonY;
                this.minSpacing = minSpacing;
                this.effectiveWidth = effectiveWidth;
                this.isBottomRow = isBottomRow;
                this.stirrupPosition = stirrupPosition;

                this.bars = new eXBar[numOfBar1 + numOfBar2];

                //if (this.numOfBar1 % 2 == 1 && this.numOfBar2 % 2 == 1)
                //{
                //    numOfBar1 -= 1;
                //    numOfBar2 += 1;
                //}

            }

            /// <summary>
            /// Creates an instance of 'eRow' class given X-Coordinates , Y- Coordinates and used diameters in a row.
            /// </summary>
            /// <param name="barType1">The bigger type of shearBar used in the row.</param>
            /// <param name="barType2">The smaller type of the shearBar to be used in the row.</param>
            /// <param name="numOfBarType1">The number of shearBar type 1 in the row.</param>
            /// <param name="numOfBarType2">The number of shearBar type 2 in the row.</param>
            public eRow(eXBar barType1, eXBar barType2, int numOfBarType1, int numOfBarType2)
            {
                this = new eRow();
            }
            #endregion

            #region Properties

            /// <summary>
            /// Gets the bigger shearBar used in the row.
            /// </summary>
            public eXBar BarType1
            {
                get { return barType1; }
            }

            /// <summary>
            /// Gets the total steel area of the row.
            /// </summary>
            public double Area
            {
                get
                {
                    double tot = 0;

                    foreach (eXBar b in bars)
                    {
                        tot += b.Area;
                    }

                    return tot;
                }
            }

            /// <summary>
            /// Gets the Effective depth of the row measured from the furthest compressive concrete fiber.
            /// </summary>
            public double EffectiveDepth
            {
                get
                {
                    double avg = 0;
                    foreach (eXBar b in bars)
                    {
                        avg += b.YCoord * b.Area;
                    }

                    return (avg / this.Area);
                }
            }

            /// <summary>
            /// Gets the smaller shearBar used in the row.
            /// </summary>
            public eXBar BarType2
            {
                get
                {
                    return barType1;
                }
            }

            /// <summary>
            /// Gets or sets the value if the row is the furthest row from the opposite concrete fibre.
            /// </summary>
            public bool IsBottomRow
            {
                get
                {
                    return isBottomRow;
                }
                set
                {
                    isBottomRow = value;
                }
            }

            /// <summary>
            /// Gets the shearBar of the row.
            /// </summary>
            public eXBar[] Bars
            {
                get
                {
                    return bars;
                }
            }

            /// <summary>
            /// Gets or set the number of the row as counted from the bottom.
            /// </summary>
            public int RowNumb
            {
                get { return rowNumb; }
                set { rowNumb = value; }
            }

            /// <summary>
            /// Gets the number of bar type 1 in the row.
            /// </summary>
            public int NumOfBar1
            {
                get
                {
                    return this.numOfBar1;
                }
            }

            /// <summary>
            /// Gets the number of bar type 2 in the row.
            /// </summary>
            public int NumOfBar2
            {
                get
                {
                    return this.numOfBar2;
                }
            }

            /// <summary>
            /// Gets the y coordinate that does not depend on the bar diameter. It is found at the contact between the stirrup and the longitudinal bars.
            /// </summary>
            public double Common_Y
            {
                get
                {
                    return this.commonY;
                }
            }
            #endregion

            #region Methods
            private void FillY_Coordinates()
            {
                double fctr = stirrupPosition == eRelativeStirrupPosition.StirrupAtBottom || isBottomRow ? 1.0 : -1.0;

                for (int i = 0; i < bars.Length; i++)
                {
                    bars[i].YCoord = commonY - fctr * bars[i].Diameter / 2.0;
                }
            }

            private void InsertBars(List<eXBar> bars, int index, double minSpacing, ref int n_big, ref int n_small)
            {
                if (this.isBottomRow)
                {

                }
                else
                {

                }

                //if (prerequisiteBars[index].XCoord <= 0)    //Left side
                //{
                //    if (index == 0)     //To insert between the very first prerequisite shearBar and the left end.
                //    {
                //        double b_eff = Math.Abs(-effectiveWidth / 2 - prerequisiteBars[index].XCoord) - prerequisiteBars[index].Diameter / 2 - minSpacing;
                //        InsertBars(shearBar, b_eff, -effectiveWidth / 2.0, ref n_big,ref n_small);
                //    }
                //    else 
                //    {
                //        double b_eff = Math.Abs(prerequisiteBars[index - 1].XCoord - prerequisiteBars[index].XCoord) -
                //            prerequisiteBars[index].Diameter / 2 - prerequisiteBars[index - 1].Diameter / 2 - minSpacing * 2.0;

                //        InsertBars(shearBar, b_eff, prerequisiteBars[index - 1].XCoord + prerequisiteBars[index - 1].Diameter / 2.0, ref n_big, ref n_small);
                //        if (index + 1 < prerequisiteBars.Length && prerequisiteBars[index + 1].XCoord > 0)
                //        {
                //            if (numOfBar1 % 2 == 1 && numOfBar2 % 2 == 1)
                //            {
                //                double D1 = barType1.Diameter, D2 = barType1.Diameter;
                //                double x = (2 * minSpacing * Math.Pow(D1, 2.0) + Math.Pow(D1, 3) + Math.Pow(D1, 2) * D2) / (2 * Math.Pow(D1, 2) + 2.0 * Math.Pow(D2, 2));
                //                shearBar.Add(new eXBar(barType2.Diameter, x));
                //                shearBar.Add(new eXBar(barType1.Diameter, -(minSpacing + (D1 + D2) / 2.0 - shearBar[shearBar.Count - 1].XCoord)));
                //            }
                //            else if (numOfBar1 % 2 == 1)
                //            {
                //                shearBar.Add(new eXBar(barType1.Diameter, 0.0));

                //            }

                //            b_eff = Math.Abs(prerequisiteBars[index].XCoord) - prerequisiteBars[index].Diameter / 2.0 - minSpacing;
                //        }
                //    }
                //}
                //else              //Right side
                //{
                //    if (index == prerequisiteBars.Length - 1)       //To insert between the last prerequisite shearBar and the end of the row.
                //    {
                //        double b_eff = effectiveWidth / 2.0 - prerequisiteBars[prerequisiteBars.Length - 1].XCoord - prerequisiteBars[prerequisiteBars.Length - 1].Diameter / 2.0;
                //        InsertBars(shearBar, b_eff, effectiveWidth / 2.0, ref n_big, ref n_small);
                //    }
                //    else
                //    {
                //        double b_eff = prerequisiteBars[index + 1].XCoord - prerequisiteBars[index].XCoord - prerequisiteBars[index + 1].Diameter / 2.0 - 
                //            prerequisiteBars[index].Diameter / 2.0 - 2.0 * minSpacing;
                //        InsertBars(shearBar, b_eff, prerequisiteBars[index + 1].XCoord - prerequisiteBars[index + 1].Diameter / 2.0, ref n_big, ref n_small);
                //    }
                //}
            }

            private void InsertBars(List<eXBar> bars, double b_eff, double boundaryCoord, ref int n_Big, ref int n_Small)
            {
                if ((n_Big == 0 && n_Small == 0) || b_eff <= 0)
                    return;

                int capct_Big = (int)((b_eff + minSpacing) / (barType1.Diameter + minSpacing));

                double s;

                if (capct_Big > n_Big)
                {
                    if (n_Big > 0)
                    {
                        double b = n_Big * minSpacing + n_Big * minSpacing;
                        int capct_Small = (int)((b + minSpacing) / (barType1.Diameter + minSpacing));

                        int n_s = Math.Min(capct_Small, n_Small);

                        s = (b_eff - n_Big * barType1.Diameter - n_s * barType2.Diameter) / (n_Big + n_s - 1);

                        if (n_Big > 0)
                        {
                            bars.Add(new eXBar(barType1.Diameter, boundaryCoord + barType1.Diameter / 2.0 + (s > 0 ? s : 0)));
                            n_Big--;
                        }

                        while (n_Big-- > 0)
                        {
                            bars.Add(new eXBar(barType1.Diameter, bars[bars.Count - 1].XCoord + s + barType1.Diameter));
                        }

                        if (b_eff == barType2.Diameter * n_s + s * (n_s - 1) && n_s > 0)
                        {
                            bars.Add(new eXBar(barType2.Diameter, boundaryCoord + barType2.Diameter / 2.0 + (s > 0 ? s : 0)));
                            n_s--;
                        }
                        else
                        {
                            bars.Add(new eXBar(barType2.Diameter, bars[bars.Count - 1].XCoord + s + barType2.Diameter / 2.0 + barType1.Diameter / 2.0));
                            n_s--;
                        }

                        while (n_s-- > 0)
                        {
                            bars.Add(new eXBar(barType2.Diameter, bars[bars.Count - 1].XCoord + s + barType2.Diameter));
                        }
                    }
                    else
                    {
                        int capct_Small = (int)((b_eff + minSpacing) / (barType2.Diameter + minSpacing));
                        s = (b_eff - capct_Small * barType2.Diameter) / (capct_Small - 1);

                        for (int i = 0; i < capct_Small; i++)
                        {
                            if (i == 0)
                                bars.Add(new eXBar(barType1.Diameter, boundaryCoord + barType1.Diameter / 2.0 + (s < 0 || double.IsNaN(s) || double.IsInfinity(s) ? minSpacing : s)));
                            else
                                bars.Add(new eXBar(barType1.Diameter, bars[bars.Count - 1].XCoord + s + barType1.Diameter / 2.0));
                            n_Small--;
                        }
                    }
                }
                else
                {
                    s = (b_eff - capct_Big * barType1.Diameter) / (capct_Big - 1);

                    for (int i = 0; i < capct_Big; i++)
                    {
                        if (i == 0)
                            bars.Add(new eXBar(barType1.Diameter, boundaryCoord + barType1.Diameter / 2.0 + (s < 0 || double.IsNaN(s) || double.IsInfinity(s) ? minSpacing : s)));
                        else
                            bars.Add(new eXBar(barType1.Diameter, bars[bars.Count - 1].XCoord + s + barType1.Diameter / 2.0));
                        n_Big--;
                    }
                }
            }

            /// <summary>
            /// Arranges the shearBar of the row that is not at the bottom into the most symmetric arrangement on predefined X_Coordinates coordinates.
            /// </summary>
            /// <param name="numOfBar1">The number of barType1 in addition to those in the prerequisite shearBar.</param>
            /// <param name="numOfBar2">The number of barType2 in addition to those in the prerequisite shearBar.</param>
            /// <param name="x_Coordinates">The x coordinates of the bottom most row in the order from the left most to the right most.</param>
            public void Arrange(double[] X_Coordinates = null)
            {
                this.xCoordinates = X_Coordinates;

                if (!isBottomRow)            //Case when the row is not a bottom row
                {
                    bars = new eXBar[numOfBar1 + numOfBar2];

                    int n1 = numOfBar1;
                    int n2 = numOfBar2;
                    int i = 0;

                    bool left = true;

                    if (n1 % 2 == 1)
                    {
                        bars[(int)(bars.Length / 2)] = new eXBar(barType1.Diameter, xCoordinates[(xCoordinates.Length / 2)]);
                        n1--;
                    }
                    if (n2 % 2 == 1)
                    {
                        bars[(int)(bars.Length / 2)] = new eXBar(barType1.Diameter, xCoordinates[(xCoordinates.Length / 2)]);
                        n2--;
                    }

                    while (n1 > 0)
                    {
                        if (left)
                        {
                            bars[i] = new eXBar(barType1.Diameter, xCoordinates[i], commonY + barType1.Diameter / 2.0);
                        }
                        else
                        {
                            bars[bars.Length - 1 - i] = new eXBar(barType1.Diameter, xCoordinates[xCoordinates.Length - 1 - i], commonY + barType1.Diameter / 2.0);
                            i++;
                        }

                        left = !left;
                        n1--;
                    }

                    while (n2 > 0)
                    {
                        if (left)
                        {
                            bars[i] = new eXBar(barType2.Diameter, xCoordinates[i], commonY + barType2.Diameter / 2.0);

                        }
                        else
                        {
                            bars[bars.Length - 1 - i] = new eXBar(barType2.Diameter, xCoordinates[xCoordinates.Length - 1 - i], commonY + barType2.Diameter / 2.0);
                            i++;
                        }

                        left = !left;
                        n2--;
                    }

                }
                else            //Case when the row is a bottom row.
                {
                    List<eXBar> tempBars = new List<eXBar>();

                    int n_big = numOfBar1 / 2;
                    int n_small = numOfBar2 / 2;

                    double s = (effectiveWidth - barType1.Diameter * numOfBar1 - barType2.Diameter * numOfBar2) / (numOfBar1 + numOfBar2 - 1);

                    double xCoord;

                    for (int i = 0; i < n_big; i++) //filling bartype 1
                    {
                        xCoord = -((effectiveWidth - barType1.Diameter) / 2.0 - i * (s + barType1.Diameter));
                        tempBars.Add(new eXBar(barType1.Diameter, xCoord, commonY + barType1.Diameter / 2.0));

                        xCoord *= -1;
                        tempBars.Add(new eXBar(barType1.Diameter, xCoord, commonY + barType1.Diameter / 2.0));
                    }

                    for (int i = 0; i < n_small; i++)  //filling bartype 2
                    {
                        if (tempBars.Count == 0)
                        {
                            xCoord = -((effectiveWidth - barType2.Diameter) / 2.0 - i * (s + barType2.Diameter));
                            tempBars.Add(new eXBar(barType2.Diameter, xCoord, commonY + barType2.Diameter / 2.0));

                            xCoord *= -1;
                            tempBars.Add(new eXBar(barType2.Diameter, xCoord, commonY + barType2.Diameter / 2.0));
                        }
                        else
                        {
                            xCoord = -(effectiveWidth / 2.0 - n_big * (barType2.Diameter + s) - i * (barType2.Diameter + s) - barType2.Diameter / 2.0);
                            tempBars.Add(new eXBar(barType2.Diameter, xCoord, commonY + barType2.Diameter / 2.0));

                            xCoord *= -1;
                            tempBars.Add(new eXBar(barType2.Diameter, xCoord, commonY + barType2.Diameter / 2.0));
                        }
                    }
                    //
                    //Adding the single bar if the row has odd number of shearBar
                    //
                    if (numOfBar1 % 2 == 1)
                    {
                        if (numOfBar2 % 2 == 1)
                            throw new Exception("Unsymetric combination found!");

                        tempBars.Add(new eXBar(barType1.Diameter, 0.0, commonY + barType1.Diameter / 2.0));
                    }

                    if (numOfBar2 % 2 == 1)
                    {
                        if (numOfBar1 % 2 == 1)
                            throw new Exception("Unsymetric combination found!");

                        tempBars.Add(new eXBar(barType2.Diameter, 0.0, commonY + barType2.Diameter / 2.0));
                    }
                    //
                    // Sorting the Bar
                    //
                    eXBar tempbar = new eXBar();
                    for (int i = 0; i < tempBars.Count; i++)
                    {
                        for (int j = i + 1; j < tempBars.Count; j++)
                        {
                            if (tempBars[i].XCoord > tempBars[j].XCoord)
                            {
                                tempbar = tempBars[i];
                                tempBars[i] = tempBars[j];
                                tempBars[j] = tempbar;
                            }
                        }
                    }

                    bars = tempBars.ToArray();
                }

                FillY_Coordinates();
            }

            /// <summary>
            /// Gets the average stress on the reinforcement steel all over the row.
            /// </summary>
            internal double GetStress()
            {
                throw new System.NotImplementedException();
            }

            /// <summary>
            /// Gets the number of shearBar with the bigger available diameter that can be added to the existing prerequisite shearBar.
            /// </summary>
            internal int GetExtraCapacity()
            {
                throw new System.NotImplementedException();
            }

            /// <summary>
            /// Gets the x coordinates of the shearBar in the row in the order from the left most to the right most.
            /// </summary>
            internal double[] GetXcoordinates()
            {
                this.xCoordinates = new double[bars.Length];

                for (int i = 0; i < bars.Length; i++)
                {
                    xCoordinates[i] = bars[i].XCoord;
                }

                return (double[])xCoordinates.Clone();
            }

            public override string ToString()
            {
                return this.numOfBar1.ToString() + "Φ" + barType1.Diameter.ToString() + ", " + numOfBar2.ToString() + "Φ" + barType2.Diameter.ToString();
            }
            #endregion
        }
    }

}
