using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ESADS
{
    public struct ePoint
    {
        /// <summary>
        /// Holds the x coordinate of the point.
        /// </summary>
        private double x;
        /// <summary>
        /// Holds the y coordinate of the point.
        /// </summary>
        private double y;

        /// <summary>
        /// Creates a point which stores the coordinates in double type.
        /// </summary>
        /// <param name="X">The x coordinate of the point</param>
        /// <param name="Y">The y coordinate of the point.</param>
        public ePoint(double X, double Y)
        {
            this = new ePoint();
            this.x = X;
            this.y = Y;
        }
        /// <summary>
        /// Gets or sets the x coordinate of the point
        /// </summary>
        public double X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        /// <summary>
        /// Gets or sets the y coordinate of the point.
        /// </summary>
        public double Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        /// <summary>
        /// Gets or sets the x coordinate in float.
        /// </summary>
        public float X_float
        {
            get
            {
                return (float)this.x;
            }
            set
            {
                this.x = value;
            }
        }

        /// <summary>
        /// Gets or sets the y coordinate in float.
        /// </summary>
        public float Y_float
        {
            get
            {
                return (float)this.y;
            }
            set
            {
                this.y = value;
            }
        }

        /// <summary>
        /// Returns a System.Drawing.Point instance with the same X and Y coordinates
        /// </summary>
        /// <param name="point">The point to be converted</param>
        /// <returns>Point format of the point</returns>
        public static implicit operator PointF(ePoint point)
        {
            return new PointF((float)point.x, (float)point.y);
        }

        /// <summary>
        /// Returns a System.Drawing.PointF instance with the same X and Y coordinates
        /// </summary>
        /// <param name="point">The point to be converted</param>
        /// <returns>PointF format of the point</returns>
        public static implicit operator Point(ePoint point)
        {
            return new Point((int)point.x, (int)point.y);
        }

        public static bool operator ==(ePoint left, ePoint right)
        {
            return (left.x == right.x && left.y == right.y);
        }

        public static bool operator !=(ePoint left, ePoint right)
        {
            return (left.x != right.x || left.y != right.y);
        }

        public override string ToString()
        {
            return "X=" + Math.Round(x, 3).ToString() + " Y=" + Math.Round(y, 3).ToString();
        }
    }
}
