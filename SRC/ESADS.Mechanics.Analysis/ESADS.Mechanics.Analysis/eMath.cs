using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Analysis
{
    /// <summary>
    /// Constains all static methods to analyze matrix operation,geometry,calculus and other matimatical computations.
    /// </summary>
    public static class eMath
    {
        /// <summary>
        /// Adds or substructs two columen or row matrixes.
        /// </summary>
        /// <param name="mtrx1">The first matrix to be added or substructed.</param>
        /// <param name="mtrx2">The second matrix to be added or substructed.</param>
        /// <param name="add">Value indicating wether addition or sebstruction is done. True for addition and false for substruction.</param>
        /// <returns></returns>
        public static double[] Superpose(double[] mtrx1, double[] mtrx2, bool add = true)
        {

            double[] result;
            if (mtrx1.Length == mtrx2.Length)
            {
                result = new double[mtrx1.Length];
                if (add)
                {
                    for (int i = 0; i < mtrx1.Length; i++)
                    {
                        result[i] = mtrx1[i] + mtrx2[i];
                    }
                }
                else
                {
                    for (int i = 0; i < mtrx1.Length; i++)
                    {
                        result[i] = mtrx1[i] - mtrx2[i];
                    }
                }
                return result;
            }
            else
            {
                throw new Exception("The length of the two matrixes to be added is not equal.");
            }
        }

        /// <summary>
        /// Multiply one two dimensional matrix with other one dimensional matrix.
        /// </summary>
        /// <param name="mtrx1">The first two dimensional matrix to be multiplied. </param>
        /// <param name="mtrx2">The second one dimensional matrix to be multiplied.</param>
        public static double[] Multiply(double[,] mtrx1, double[] mtrx2)
        {
            double[] result;
            if (mtrx1.GetLength(1) == mtrx2.GetLength(0))
            {
                result = new double[mtrx1.GetLength(0)];
                for (int i = 0; i < result.Length; i++)
                {
                    double tempSum = 0;
                    for (int j = 0; j < mtrx1.GetLength(1); j++)
                    {
                        tempSum += mtrx1[i, j] * mtrx2[j];
                    }
                    result[i] = tempSum;
                }
                return result;
            }
            else
            {
                throw new Exception("The number of columen of the first matrix must be equal with the row of the second matrix. ");
            }
        }

        /// <summary>
        /// Returns the inverse of a square matrix if invertible.
        /// </summary>
        /// <param name="Matrix">The matrix to be inverted.</param>
        public static double[,] Inverse(double[,] Matrix)
        {
            if (Matrix.GetLength(0) != Matrix.GetLength(1))
                throw new Exception("A matrix should be square in order to be inverted.");

            double[,] combined = new double[Matrix.GetLength(0), Matrix.GetLength(1) * 2];

            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < combined.GetLength(1); j++)
                {
                    if (j < Matrix.GetLength(0))
                        combined[i, j] = Matrix[i, j];
                    else if (j == i + Matrix.GetLength(0))
                        combined[i, j] = 1;
                }
            }

            ReduceLowerPart(combined);
            ReduceDiagonal(combined);
            ReduceUpperPart(combined);

            double[,] result = new double[Matrix.GetLength(0), Matrix.GetLength(1)];

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = combined[i, j + result.GetLength(0)];
                }
            }

            return result;
        }

        /// <summary>
        /// Reduces the diagonal elements of the matrix to unity.
        /// </summary>
        /// <param name="Matrix"></param>
        private static void ReduceDiagonal(double[,] Matrix)
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                if (Matrix[i, i] != 1)
                {
                    if (Matrix[i, i] == 0)
                    {
                        for (int j = i + 1; j < Matrix.GetLength(0); j++)
                        {
                            if (Matrix[j, i] != 0)
                            {
                                RowInterchange(Matrix, i, j);
                                break;
                            }
                        }
                    }

                    RowMult(Matrix, i, 1 / Matrix[i, i]);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Matrix"></param>
        private static void ReduceUpperPart(double[,] Matrix)
        {
            for (int i = Matrix.GetLength(0) - 1; i >= 0; i--)
            {
                for (int k = i - 1; k >= 0; k--)
                {
                    if (Matrix[k, i] != 0)
                        RowAdd(Matrix, k, i, (-1 * Matrix[k, i] / Matrix[i, i]));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Matrix"></param>
        private static void ReduceLowerPart(double[,] Matrix)
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int k = i + 1; k < Matrix.GetLength(0); k++)
                {
                    CheckPivotElement(Matrix, i);

                    if (Matrix[k, i] != 0)
                        RowAdd(Matrix, k, i, (-1 * Matrix[k, i] / Matrix[i, i]));

                }
            }
        }

        /// <summary>
        /// Solves a linear system of equations given the coefficient matrix and the constant matrix.
        /// </summary>
        /// <param name="CoefficientMatrix">The matrix 'A' in the equation 'AX = B'</param>
        /// <param name="ConstantMatrix">The matrix B in the above equation.</param>
        /// <returns></returns>
        public static double[] Solve(double[,] CoefficientMatrix, double[] ConstantMatrix)
        {
            double[,] augmented = new double[CoefficientMatrix.GetLength(0), CoefficientMatrix.GetLength(1) + 1];

            for (int i = 0; i < CoefficientMatrix.GetLength(0); i++)
            {
                int j = 0;
                for (; j < CoefficientMatrix.GetLength(1); j++)
                {
                    augmented[i, j] = CoefficientMatrix[i, j];
                }
                augmented[i, j] = ConstantMatrix[i];
            }

            ReduceLowerPart(augmented);
            ReduceDiagonal(augmented);
            ReduceUpperPart(augmented);

            double[] result = new double[augmented.GetLength(0)];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = augmented[i, augmented.GetLength(1) - 1];
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Matrix"></param>
        /// <param name="Index"></param>
        private static void CheckPivotElement(double[,] Matrix, int Index)
        {
            if (Matrix[Index, Index] == 0)
                for (int k = Index + 1; k < Matrix.GetLength(0); k++)
                    if (Matrix[k, Index] != 0)
                        RowInterchange(Matrix, Index, k);

        }

        /// <summary>
        /// Interchanges the two rows of the matrix given.
        /// </summary>
        /// <param name="Matrix">The matrix whose two rows are to be interchanged.</param>
        /// <param name="Index1">The index of the first row to be interchanged.</param>
        /// <param name="Index2">The index of the second row to be interchanged.</param>
        private static void RowInterchange(double[,] Matrix, int Index1, int Index2)
        {
            if (Matrix == null || Matrix.GetLength(0) == 0 || Matrix.GetLength(1) == 0)
                throw new Exception("Row operations cannot be conducted on a null or empty valued matrix.");
            if (Index1 < 0 || Index1 >= Matrix.GetLength(0) || Index2 < 0 || Index2 >= Matrix.GetLength(1))
                throw new Exception("One or more of the given indices is out of range of the matrix size.");
            if (Index1 == Index2)
                return;

            double temp = 0;

            for (int j = 0; j < Matrix.GetLength(1); j++)
            {
                temp = Matrix[Index1, j];
                Matrix[Index1, j] = Matrix[Index2, j];
                Matrix[Index2, j] = temp;
            }
        }

        /// <summary>
        /// Adds a constant multiple of a row to another row.
        /// </summary>
        /// <param name="Matrix">The matrix whose one row is to be added to a constant multiple of other row of itself.</param>
        /// <param name="Index1">The index of the row which is going to receive the sum of itself and a constant multiple of another row.</param>
        /// <param name="Index2">The index of the row which is goint to multiplied by a constant and added to another row.</param>
        /// <param name="Constant">The constant to multiply the second row with.</param>
        private static void RowAdd(double[,] Matrix, int Index1, int Index2, double Constant)
        {
            if (Matrix == null || Matrix.GetLength(0) == 0 || Matrix.GetLength(1) == 0)
                throw new Exception("Row operations cannot be conducted on a null or empty valued matrix.");
            if (Index1 < 0 || Index1 >= Matrix.GetLength(0) || Index2 < 0 || Index2 >= Matrix.GetLength(1))
                throw new Exception("One or more of the given indices is out of range of the matrix size.");
            if (Index1 == Index2)
                return;

            for (int j = 0; j < Matrix.GetLength(1); j++)
            {
                Matrix[Index1, j] += Constant * Matrix[Index2, j];
            }
        }

        /// <summary>
        /// Multiplies a row with a non zero constant.
        /// </summary>
        /// <param name="Matrix">The matrix whose single row is going to be multiplied by a constant.</param>
        /// <param name="Index">The index of the row to be multiplied by the constant.</param>
        /// <param name="Constant">The constant to multiply the row with.</param>
        public static void RowMult(double[,] Matrix, int Index, double Constant)
        {
            if (Matrix == null || Matrix.GetLength(0) == 0 || Matrix.GetLength(1) == 0)
                throw new Exception("Row operations cannot be conducted on a null or empty valued matrix.");
            if (Index < 0 || Index >= Matrix.GetLength(0))
                throw new Exception("The index given is out of range of the matrix size.");

            for (int j = 0; j < Matrix.GetLength(1); j++)
            {
                Matrix[Index, j] *= Constant;
            }
        }

        /// <summary>
        /// Gets the maximum positive value of a function when approximated by a degree three polynomial within the interval between the first and the last point given to approximate the function.
        /// </summary>
        /// <param name="x1">X coordinate of first point</param>
        /// <param name="y1">Y coordinate of first point</param>
        /// <param name="x2">X coordinate of second point</param>
        /// <param name="y2">Y coordinate of second point</param>
        /// <param name="x3">X coordinate of third point</param>
        /// <param name="y3">Y coordinate of third point</param>
        /// <param name="x4">X coordinate of fourth point</param>
        /// <param name="y4">Y coordinate of fourth point</param>
        /// <param name="MaxNegative">The maximum negative value of the function within the interval.</param>
        /// <param name="X_atMaxNeg">The x value at the maximum negative y value.</param>
        /// <param name="X_atMaxPos">The x value at the maximum positive y value.</param>
        public static double GetMaxOf(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4, out double MaxNegative, out double X_atMaxNeg, out double X_atMaxPos)
        {
            double[] constants = GetFunction(x1, y1, x2, y2, x3, y3, x4, y4);

            double maxPos = 0;
            MaxNegative = 0;
            X_atMaxNeg = X_atMaxPos = 0;
            double xc1 = 0, xc2 = 0, yc1 = 0, yc2 = 0;

            if (constants[0] != 0)
            {
                xc1 = (-2 * constants[1] + Math.Sqrt(4 * Math.Pow(constants[1], 2) - 12 * constants[0] * constants[2])) / (6 * constants[0]);
                xc2 = (-2 * constants[1] - Math.Sqrt(4 * Math.Pow(constants[1], 2) - 12 * constants[0] * constants[2])) / (6 * constants[0]);

                if (x1 < xc1 && xc1 < x4)
                    yc1 = constants[0] * Math.Pow(xc1, 3) + constants[1] * Math.Pow(xc1, 2) + constants[2] * xc1 + constants[3];
                if (x1 < xc2 && xc2 < x4)
                    yc2 = constants[0] * Math.Pow(xc2, 3) + constants[1] * Math.Pow(xc2, 2) + constants[2] * xc2 + constants[3];
            }
            else if (constants[1] != 0)
            {
                xc1 = -constants[2] / (2 * constants[1]);

                if (x1 < xc1 && xc1 < x4)
                    yc1 = constants[1] * Math.Pow(xc1, 2) + constants[2] * xc1 + constants[3];
            }

            if (y1 > 0 && y1 > maxPos)
            {
                maxPos = y1;
                X_atMaxPos = x1;
            }
            else if (y1 < 0 && y1 < MaxNegative)
            {
                MaxNegative = y1;
                X_atMaxNeg = x1;
            }

            if (yc1 > 0 && yc1 > maxPos)
            {
                maxPos = yc1;
                X_atMaxPos = xc1;
            }
            else if (yc1 < 0 && yc1 < MaxNegative)
            {
                MaxNegative = yc1;
                X_atMaxNeg = xc1;
            }

            if (yc2 > 0 && yc2 > maxPos)
            {
                maxPos = yc2;
                X_atMaxPos = xc2;
            }
            else if (yc2 < 0 && yc2 < MaxNegative)
            {
                MaxNegative = yc2;
                X_atMaxNeg = xc2;
            }

            if (y4 > 0 && y4 > maxPos)
            {
                maxPos = y4;
                X_atMaxPos = x4;
            }
            else if (y4 < 0 && y4 < MaxNegative)
            {
                MaxNegative = y4;
                X_atMaxNeg = x4;
            }

            return maxPos;
        }

        /// <summary>
        /// Gets the maximum positive value of a function when approximated by a degree three polynomial within the interval between the first and the last point given to approximate the function.
        /// </summary>
        /// <param name="x1">X coordinate of first point</param>
        /// <param name="y1">Y coordinate of first point</param>
        /// <param name="x2">X coordinate of second point</param>
        /// <param name="y2">Y coordinate of second point</param>
        /// <param name="x3">X coordinate of third point</param>
        /// <param name="y3">Y coordinate of third point</param>
        /// <param name="MaxNegative">The maximum negative value of the function within the interval.</param>
        /// <param name="X_atMaxNeg">The x value at the maximum negative y value.</param>
        /// <param name="X_atMaxPos">The x value at the maximum positive y value.</param>
        public static double GetMaxOf(double x1, double y1, double x2, double y2, double x3, double y3, out double MaxNegative, out double X_atMaxNeg, out double X_atMaxPos)
        {
            double[] constants = GetFunction(x1, y1, x2, y2, x3, y3);

            double maxPos = 0;
            MaxNegative = 0;
            X_atMaxNeg = X_atMaxPos = 0;
            double xc1 = 0, xc2 = 0, yc1 = 0, yc2 = 0;

            if (constants[0] != 0)
            {
                xc1 = -constants[1] / (2 * constants[0]);

                if (x1 < xc1 && xc1 < x3)
                    yc1 = constants[0] * Math.Pow(xc1, 2) + constants[1] * xc1 + constants[2];
            }

            if (y1 > 0 && y1 > maxPos)
            {
                maxPos = y1;
                X_atMaxPos = x1;
            }
            else if (y1 < 0 && y1 < MaxNegative)
            {
                MaxNegative = y1;
                X_atMaxNeg = x1;
            }

            if (yc1 > 0 && yc1 > maxPos)
            {
                maxPos = yc1;
                X_atMaxPos = xc1;
            }
            else if (yc1 < 0 && yc1 < MaxNegative)
            {
                MaxNegative = yc1;
                X_atMaxNeg = xc1;
            }

            if (yc2 > 0 && yc2 > maxPos)
            {
                maxPos = yc2;
                X_atMaxPos = xc2;
            }
            else if (yc2 < 0 && yc2 < MaxNegative)
            {
                MaxNegative = yc2;
                X_atMaxNeg = xc2;
            }

            if (y3 > 0 && y3 > maxPos)
            {
                maxPos = y3;
                X_atMaxPos = x3;
            }
            else if (y3 < 0 && y3 < MaxNegative)
            {
                MaxNegative = y3;
                X_atMaxNeg = x3;
            }

            return maxPos;

        }

        /// <summary>
        /// Gets the x coordinates within an interval where a degree three polynomial function has a value of zero. The function doesn't change the sign of the slope within the range.
        /// </summary>
        /// <param name="a">Coeficient of x to the power of three.</param>
        /// <param name="b">Coeficient of x square.</param>
        /// <param name="c">Coeficient of x.</param>
        /// <param name="d">constant.</param>
        /// <param name="x1">left boundary of the interval.</param>
        /// <param name="x2">right boundary of the interval.</param>
        public static double GetZeroOf(double a, double b, double c, double d, double x1, double x2)
        {
            double y1 = a * Math.Pow(x1, 3) + b * Math.Pow(x1, 2) + c * x1 + d;
            double y2 = a * Math.Pow(x2, 3) + b * Math.Pow(x2, 2) + c * x2 + d;

            if (y1 * y2 > 0)
                return double.NaN;
            else if (y1 == 0)
                return x1;
            else if (y2 == 0)
                return x2;

            double x3, y3;

            while (true)
            {
                y1 = a * Math.Pow(x1, 3) + b * Math.Pow(x1, 2) + c * x1 + d;
                y2 = a * Math.Pow(x2, 3) + b * Math.Pow(x2, 2) + c * x2 + d;

                x3 = (x2 * y1 - x1 * y2) / (y1 - y2);

                y3 = a * Math.Pow(x3, 3) + b * Math.Pow(x3, 2) + c * x3 + d;

                if (Math.Round(y3, 5) == 0)
                    return x3;

                if (y1 * y3 < 0)
                    x2 = x3;
                else
                    x1 = x3;
            }
        }

        /// <summary>
        /// Gets the x coordinates within an interval where a degree two(parabola) polynomial function has a value of zero
        /// </summary>
        /// <param name="a">Coeficient of x squared.</param>
        /// <param name="b">Coeficient of x.</param>
        /// <param name="c">Constant</param>
        /// <param name="X1">left boundary of the interval.</param>
        /// <param name="X2">right boundary of the interval.</param>
        /// <remarks>If there is no solution within  the interval the returned array will contain the value 'double.NaN' </remarks>
        public static double[] GetZeroOf(double a, double b, double c, double X1, double X2)
        {
            double xc1 = Math.Round((-b + Math.Sqrt(Math.Pow(b, 2) - 4.0 * a * c)) / (2.0 * a), 8);
            double xc2 = Math.Round((-b - Math.Sqrt(Math.Pow(b, 2) - 4.0 * a * c)) / (2.0 * a), 8);

            List<double> soln = new List<double>();

            if (xc1 == xc2 && X1 <= xc1 && xc1 <= X2)
                soln.Add(xc1);
            else
            {
                if (xc1 < xc2)
                {
                    if (X1 < xc1 && xc1 < X2)
                        soln.Add(xc1);
                    if (X1 < xc2 && xc2 < X2)
                        soln.Add(xc2);
                }
                else
                {
                    if (X1 < xc2 && xc2 < X2)
                        soln.Add(xc2);
                    if (X1 < xc1 && xc1 < X2)
                        soln.Add(xc1);
                }
            }

            //if (soln.Count == 0)
            //    soln.Add(double.NaN);

            return soln.ToArray();
        }

        /// <summary>
        /// Gets all the zeros of a degree three polynomial within a given range of interval. The polynomial is represented by four points from it.
        /// </summary>
        public static double[] GetZeroOf(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4, double x_min, double x_max)
        {
            List<double> solutionSet = new List<double>();

            double[] constants = GetFunction(x1, y1, x2, y2, x3, y3, x4, y4);

            double a = constants[0];
            double b = constants[1];
            double c = constants[2];
            double d = constants[3];

            double y_min = a * Math.Pow(x_min, 3) + b * Math.Pow(x_min, 2) + c * x_min + d;
            double y_max = a * Math.Pow(x_max, 3) + b * Math.Pow(x_max, 2) + c * x_max + d;

            if (a != 0) //degree three              
                return GetZeros(a, b, c, d, x_min, x_max);
            else if (b != 0)//degree two
            {
                double[] soln = GetZeroOf(b, c, d, x_min, x_max);

                if (soln.Length > 0 && !double.IsNaN(soln[0]))
                {
                    foreach (double val in soln)
                        solutionSet.Add(val);
                }
            }
            else if (c != 0)
            {
                if (y_max * y_min < 0)
                {
                    solutionSet.Add((x_max * y_min - x_min * y_max) / (y_min - y_max));
                }
            }
            else if (d == 0)
            {
                solutionSet.Add(x_min);
            }

            //if (solutionSet.Count == 0)
            //    return null;

            return solutionSet.ToArray();
        }

        /// <summary>
        /// Gets the constants of a degree three polynomial given four ordered pairs.
        /// </summary>
        private static double[] GetFunction(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            double[,] coeficientMtx = { {Math.Pow(x1, 3), Math.Pow(x1, 2), x1, 1 }, 
                                      {Math.Pow(x2, 3), Math.Pow(x2, 2), x2, 1 }, 
                                      {Math.Pow(x3, 3), Math.Pow(x3, 2), x3, 1 }, 
                                      { Math.Pow(x4, 3), Math.Pow(x4, 2), x4, 1} };
            double[] constMtx = { y1, y2, y3, y4 };

            double[] soln = Solve(coeficientMtx, constMtx);

            for (int i = 0; i < soln.Length; i++)
            {
                soln[i] = Math.Round(soln[i], 8);
            }

            return soln;
        }

        /// <summary>
        /// Gets the constants of a degree two polynomial given three ordered pairs.
        /// </summary>
        private static double[] GetFunction(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            double[,] coeficientMtx = { {Math.Pow(x1, 2), x1, 1 }, 
                                      {Math.Pow(x2, 2), x2, 1 }, 
                                      {Math.Pow(x3, 2), x3, 1 }};
            double[] constMtx = { y1, y2, y3 };

            double[] soln = Solve(coeficientMtx, constMtx);

            for (int i = 0; i < soln.Length; i++)
            {
                soln[i] = Math.Round(soln[i], 8);
            }

            return soln;
        }

        /// <summary>
        /// Returns the divid difference from given two pair coordinates
        /// </summary>
        /// <param name="x1">The x-coordinate of the first point.</param>
        /// <param name="y1">The y-coordinate of the first point.</param>
        /// <param name="x2">The x-coordinate of the second point.</param>
        /// <param name="y2">The y-coordinate of the second point.</param>
        /// <returns></returns>
        private static double GetDiff(double x1, double y1, double x2, double y2)
        {
            return (y2 - y1) / (x2 - x1);
        }

        /// <summary>
        /// Returns the divid difference from given three pair coordinates
        /// </summary>
        /// <param name="x1">The x-coordinate of the first point.</param>
        /// <param name="y1">The y-coordinate of the first point.</param>
        /// <param name="x2">The x-coordinate of the second point.</param>
        /// <param name="y2">The y-coordinate of the second point.</param>
        /// <param name="x3">The x-coordinate of the third point.</param>
        /// <param name="y3">The y-coordinate of the third point.</param>
        /// <returns></returns>
        private static double GetDiff(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            return (GetDiff(x2, y2, x3, y3) - GetDiff(x1, y1, x2, y2)) / (x3 - x1);
        }

        /// <summary>
        /// Returns the Muller's iterative quadratic formula result for the given parameters.
        /// </summary>
        /// <param name="x1">Bound from the left.</param>
        /// <param name="x2">Bound rom the right.</param>
        /// <param name="a">Varaible in Mullers formula. </param>
        /// <param name="b">Varaible in Mullers formula.</param>
        /// <param name="c">Varaible in Mullers formula.</param>
        /// <param name="x">The most resent aproximation of the root in the iteration.</param>
        /// <returns></returns>
        public static double Muller(double x1, double x2, double a, double b, double c, double x)
        {
            double disc; // represents the discriminat of the quadratic equation.
            double r1, r2; // represetns the first and the second aproximation of the roots.

            disc = Math.Sqrt(b * b - 4 * a * c);
            if (double.IsNaN(disc))
                return double.NaN;
            r1 = x - 2 * c / (b + disc);
            r2 = x - 2 * c / (b - disc);
            if ((r1 < r2) && (r1 > x1) && (r1 < x2))
                return r1;
            else if ((r2 < r1) && (r2 > x1) && (r2 < x2))
                return r2;
            else if ((r1 > x1) && (r1 < x2))
                return r1;
            else if ((r2 > x1) && (r2 < x2))
                return r2;
            else
                return double.NaN;
        }

        /// <summary>
        /// Returns the zeros of dgree three polynomial int the given interval.
        /// </summary>
        /// <param name="a">Coeficient of x to the power of three.</param>
        /// <param name="b">Coeficient of x square.</param>
        /// <param name="c">Coeficient of x.</param>
        /// <param name="d">constant.</param>
        /// <param name="x1">The left bound.</param>
        /// <param name="x2">The right bound.</param>
        /// <returns>Returns null if no zero found on the given interval.</returns>
        public static double[] GetZeros(double a, double b, double c, double d, double x1, double x2)
        {
            List<double> roots = new List<double>();
            double[] crtPoints = GetZeroOf(3 * a, 2 * b, c, x1, x2);

            if (crtPoints.Length == 0)
                return new double[0];

            double r1, r2, r3, y1, y2, y3, r;
            double ma, mb, x = x1;
            if (double.IsNaN(crtPoints[0]))
            {
                if (F(a, b, c, d, x1) * F(a, b, c, d, x2) > 0)
                    return new double[0];
                else if (F(a, b, c, d, x1) * F(a, b, c, d, x2) < 0)
                {
                    r1 = x1;
                    r2 = (x1 + x2) / 2;
                    r3 = x2;
                    y1 = F(a, b, c, d, r1);
                    y2 = F(a, b, c, d, r2);
                    y3 = F(a, b, c, d, r3);
                    while (Math.Round(r3, 9) != Math.Round(r2, 9))
                    {
                        ma = GetDiff(r1, y1, r2, y2, r3, y3);
                        mb = GetDiff(r2, y2, r3, y3) + ma * (r3 - r2);
                        r = Muller(x1, x2, ma, mb, y3, r3);
                        if (!double.IsNaN(r))
                        {
                            r1 = r2;
                            y1 = y2;
                            r2 = r3;
                            y2 = y3;
                            r3 = r;
                            y3 = F(a, b, c, d, r3);
                        }
                        if (y3 == 0)
                            break;
                    }
                    roots.Add(r3);
                }
                else
                    if (F(a, b, c, d, x1) == 0)
                        return new double[1] { x1 };
                    else
                        return new double[1] { x2 };
            }
            else
            {
                for (int i = 0, j = 0; j <= crtPoints.Length; j++)
                {
                    if (F(a, b, c, d, x) * F(a, b, c, d, crtPoints[i]) < 0)
                    {
                        r1 = x;
                        r2 = (x + crtPoints[i]) / 2;
                        r3 = crtPoints[i];
                        y1 = F(a, b, c, d, r1);
                        y2 = F(a, b, c, d, r2);
                        y3 = F(a, b, c, d, r3);
                        while (Math.Round(r3, 12) != Math.Round(r2, 12))
                        {
                            ma = GetDiff(r1, y1, r2, y2, r3, y3);
                            mb = GetDiff(r2, y2, r3, y3) + ma * (r3 - r2);
                            r = Muller(Math.Min(x, crtPoints[i]), Math.Max(x, crtPoints[i]), ma, mb, y3, r3);
                            if (!double.IsNaN(r))
                            {
                                r1 = r2;
                                y1 = y2;
                                r2 = r3;
                                y2 = y3;
                                r3 = r;
                                y3 = F(a, b, c, d, r3);
                            }
                            else
                                break;
                            if (y3 == 0)
                                break;
                        }
                        roots.Add(r3);
                    }
                    x = crtPoints[i];
                    if (i == crtPoints.Length - 1)
                        x = x2;
                    else if (i < crtPoints.Length - 1)
                        i++;
                }
            }
            if (roots.Count == 0)
                return new double[0];
            return roots.ToArray() as double[];
        }

        /// <summary>
        /// Returns the ordinate for given absisa of dgree three polynomial.
        /// </summary>
        /// <param name="a">Coeficient of x to the power of three.</param>
        /// <param name="b">Coeficient of x square.</param>
        /// <param name="c">Coeficient of x.</param>
        /// <param name="d">constant.</param>
        /// <param name="x">value where the polynomial is to be evaluated.</param>
        /// <returns></returns>
        private static double F(double a, double b, double c, double d, double x)
        {
            return a * x * x * x + b * x * x + c * x + d;
        }

        /// <summary>
        /// Returns the length of the line connecting the given two points.
        /// </summary>
        /// <param name="start">Start point of the line.</param>
        /// <param name="end">End point of the line.</param>
        /// <returns></returns>
        public static double GetLength(ePoint start, ePoint end)
        {
            return Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
        }

        /// <summary>
        /// Returns the intersection point of lines connecting the given points.
        /// </summary>
        /// <param name="start1">Start point of the first line  </param>
        /// <param name="end1">End point of the first line</param>
        /// <param name="start2">Start point of second line.</param>
        /// <param name="end2">End point of second line.</param>
        /// <returns></returns>
        public static ePoint GetIntersectionPoint(ePoint start1, ePoint end1, ePoint start2, ePoint end2)
        {
            double a1, b1, a2, b2, x, y;
            a1 = (end1.Y - start1.Y) / (end1.X - start1.X);
            a2 = (end2.Y - start2.Y) / (end2.X - start2.X);
            b1 = start1.Y - a1 * start1.X;
            b2 = start2.Y - a2 * start2.X;
            x = (b2 - b1) / (a1 - a2);
            y = a1 * x + b1;
            return new ePoint(x, y);
        }

        private static bool Contain(List<ePoint> pts, ePoint p)
        {

            if (pts[0].X < p.X && p.X < pts[1].X && pts[1].Y > p.X && p.Y > pts[2].Y)
                return true;
            else
                return false;
        }

        private static bool CheckPrecision(double calculatedValue, double ActualValue, int precision)
        {
            if (Math.Round(ActualValue, precision) == Math.Round(calculatedValue, precision))
                return true;
            else
                return false;
        }

        private static bool IsLeftOrAbove(ePoint start, ePoint end, ePoint p)
        {
            double teta1, teta2, d;
            teta1 = Math.Atan((end.Y - start.Y) / (end.X - start.X));
            teta2 = Math.Atan((p.Y - start.Y) / (p.X - start.X));
            d = GetLength(start, p) * Math.Sign(teta2 - teta1);
            return d > 0;
        }

        private static double GetAngle(ePoint p1, ePoint p2)
        {
            throw new NotImplementedException();
        }

        private static List<List<ePoint>> GetAreasLeftOrAbove(List<ePoint> pts, ePoint start, ePoint end)
        {
            List<List<ePoint>> areas = new List<List<ePoint>>();
            List<ePoint> area = new List<ePoint>();
            bool started = false;
            for (int i = 0; i < pts.Count - 1; i++)
            {
                if (IsLeftOrAbove(start, end, pts[i]) && IsLeftOrAbove(start, end, pts[i + 1]))
                {
                    if (i != 0)
                    {
                        areas.Add(area);
                        area = new List<ePoint>();
                        area.Add(GetIntersectionPoint(start, end, pts[i], pts[i + 1]));
                    }
                    started = true;
                }
                if (!IsLeftOrAbove(start, end, pts[i]) && !IsLeftOrAbove(start, end, pts[i + 1]))
                    started = false;
                if (started)
                {
                    area.Add(pts[i + 1]);
                }
            }
            return areas;
        }

        private static List<List<ePoint>> GetAreasRightOrBelow(List<ePoint> pts, ePoint start, ePoint end)
        {
            List<List<ePoint>> areas = new List<List<ePoint>>();
            List<ePoint> area = new List<ePoint>();
            bool started = false;
            for (int i = 0; i < pts.Count - 1; i++)
            {
                if (!IsLeftOrAbove(start, end, pts[i]) && !IsLeftOrAbove(start, end, pts[i + 1]))
                {
                    if (i != 0)
                    {
                        areas.Add(area);
                        area = new List<ePoint>();
                        area.Add(GetIntersectionPoint(start, end, pts[i], pts[i + 1]));
                    }
                    started = true;
                }
                if (IsLeftOrAbove(start, end, pts[i]) && IsLeftOrAbove(start, end, pts[i + 1]))
                    started = false;
                if (started)
                {
                    area.Add(pts[i + 1]);
                }
            }
            return areas;
        }

        public static List<List<ePoint>> GetAreas(List<ePoint> polPts, params ePoint[]rectPts)
        {
            List<List<ePoint>> areas = new List<List<ePoint>>();
            List<List<ePoint>> tempAs = new List<List<ePoint>>();
            areas = GetAreasRightOrBelow(polPts, rectPts[0], rectPts[1]);
            FillAreas(areas, rectPts[1], rectPts[2]);
            FillAreas(areas, rectPts[2], rectPts[3]);
            FillAreas(areas, rectPts[3], rectPts[1], false);
            return areas;
        }

        private static void FillAreas(List<List<ePoint>> areas, ePoint start, ePoint end ,bool leftOrObove = true)
        {
            List<List<ePoint>> tempAs = new List<List<ePoint>>();
            for (int i = 0; i < areas.Count; i++)
            {
                if (leftOrObove)
                    tempAs = GetAreasLeftOrAbove(areas[i], start, end);
                else
                    tempAs = GetAreasRightOrBelow(areas[i], start, end);
                for (int j = 0; j < tempAs.Count; j++)
                    areas.Add(tempAs[j]);
            }
        }

        /// <summary>
        /// Returns the area of polygon surounded by the points using traverse method.
        /// </summary>
        /// <param name="pts">Array of point forming the polygon.</param>
        /// <returns></returns>
        public static double GetArea(List<ePoint> pts)
        {
            pts.Add(pts[0]);
            double A1 = 0, A2 = 0;
            for (int i = 0; i < pts.Count - 1; i++)
            {
                A1 += pts[i].X * pts[i + 1].Y;
                A2 += pts[i + 1].X * pts[i].Y;
            }

            return Math.Abs(A1 - A2) / 2;
        }



    }
}
