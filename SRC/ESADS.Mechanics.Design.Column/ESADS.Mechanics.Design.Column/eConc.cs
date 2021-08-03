using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Design.Column
{
    public struct eConc
    {
        #region Fields
        private double A;
        private double nc;
        private double mx;
        private double my;
        private double a;
        private double t;
        private double h;
        private double b;
        private double m;
        private double c;
        private double fcd;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates an instance of this class for a given basic parameters.
        /// </summary>
        /// <param name="b">Width of the column.</param>
        /// <param name="h">Height of the column.</param>
        /// <param name="fcd">Design compressive strength of the column.</param>
        public eConc(double b, double h ,double fcd)
        {
            this.h = h;
            this.b = b;
            this.A = 0;
            this.mx = 0;
            this.my = 0;
            this.t = 0;
            this.a = 0;
            this.nc = 0;
            this.m = 0;
            this.c = 0;
            this.fcd = fcd;
          }
        #endregion

        #region Properties
        public double X
        {
            get
            {
                return a;
            }
            set
            {
                a = value;
            }
        }

        public double Area
        {
            get
            {
                return A;
            }
        }

        public double Mx
        {
            get
            {
                return mx;
            }
        }

        public double NC
        {
            get
            {
                return nc;
            }
        }

        public double Angle
        {
            get
            {
               return ConvertRadToDegree(t);
            }
            set
            {
                t = ConvertDgreeToRad(value);
            }
        }

        public double My
        {
            get
            {
                return my;
            }
        }

        #endregion

        #region Methods
        public void FillParameters(double AsComp,double a,double teta)
        {
            if (a == 0)
            {
                nc = 0;
                mx = 0;
                my = 0;
                return;
            }
            this.t = teta;
            this.a = a;            
            this.m = Math.Tan(t);
            this.c = h / 2 + this.m * b / 2 - this.a / Math.Cos(t);
            
            double xCentroid;
            double yCentroid;
            FillAreaAndCentroids(out xCentroid,out yCentroid);
            nc = fcd * (A - AsComp);
            mx = nc * yCentroid;
            my = nc * xCentroid;           
        }

        public static double ConvertDgreeToRad(double angle)
        {
            return Math.PI * angle / 180;
        }

        public static double ConvertRadToDegree(double angle)
        {
            return 180 * angle / Math.PI;         
        }

        private double GetY(double x)
        {
            return m * x + c;
        }

        private double GetX(double y)
        {
            return (y - c) / m;
        }

        private void FillAreaAndCentroids(out double xCentroid, out double yCentroid)
        {
            if (m == 0)
            {
                xCentroid = 0;
                if (a < h)
                {
                    yCentroid = h / 2 - a / 2;
                    A = a * b;
                }
                else
                {
                    yCentroid = 0;
                    A = b * h;
                }
                return;
            }
            if (Math.Round(t, 12) == Math.Round(Math.PI / 2, 12))
            {
                yCentroid = 0;
                if (a < b)
                {
                    xCentroid = -b / 2 + a / 2;
                    A = a * h;
                }
                else
                {
                    xCentroid = 0;
                    A = b * h;
                }
                return;
            }

            double Y1, Y2, X1, X2;
            Y1 = GetY(-b / 2);
            Y2 = GetY(b / 2);
            X1 = GetX(h / 2);
            X2 = GetX(-h / 2);


            if (Y1 >= -h / 2 && Y1 <= h / 2 && Y2 >= h / 2)
            {
                X1 = b / 2 + X1;
                Y1 = h / 2 - Y1;
                xCentroid = -b / 2 + X1 / 3;
                yCentroid = h / 2 - Y1 / 3;
                A = 0.5 * X1 * Y1;
                return;
            }
            else if (Y1 >= -h / 2 && Y1 <= h / 2 && Y2 <= h / 2 && Y2 >= -h / 2)
            {
                Y1 = h / 2 - Y1;
                Y2 = h / 2 - Y2;
                A = 0.5 * b * (Y1 + Y2);
                xCentroid = 0.5 * b * (Y1 - Y2) * (-b / 2 + b / 3) / A;
                yCentroid = (Y2 * b * (h / 2 - Y2 / 2) + 0.5 * b * (Y1 - Y2) * (h / 2 - Y2 - (Y1 - Y2) / 3)) / A;
                return;
            }
            else if (Y1 <= -h / 2 && Y2 <= h / 2 && Y2 >= -h / 2)
            {
                X2 = b / 2 - X2;
                Y2 = h / 2 + Y2;
                A = b * h - 0.5 * X2 * Y2;
                xCentroid = -0.5 * X2 * Y2 * (b / 2 - X2 / 3) / A;
                yCentroid = -0.5 * X2 * Y2 * (-h / 2 + Y2 / 3) / A;
                return;
            }
            else if (Y1 <= -h / 2 && Y2 >= h / 2)
            {
                X1 = b / 2 + X1;
                X2 = b / 2 + X2;
                A = 0.5 * h * (X1 + X2);
                xCentroid = (X2 * h * (-b / 2 + X1 / 2) + 0.5 * h * (X1 - X2) * (-b / 2 + X2 + (X1 - X2) / 3)) / A;
                yCentroid = 0.5 * h * (X1 - X2) * (h / 2 - h / 3) / A;
                return;
            }
            else
            {
                xCentroid = 0;
                yCentroid = 0;
                A = b * h;
                return;
            }
        }

        private double GetAreaOfR1(double x1,double x2)
        {
            return 0.5 * m * (Math.Pow(x1, 2) - Math.Pow(x2, 2)) + (h / 2 - c) * (x2 - x1);
        }

        private double GetAreaOfR2(double x1, double x2)
        {
            return 0.5 * m * (Math.Pow(x1, 2) - Math.Pow(x2, 2)) + (h / 2 + c) * (x1 - x2);
        }

        #endregion
    }
}
