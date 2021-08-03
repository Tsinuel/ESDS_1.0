using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ESADS.EGraphics
{
    public class eBreakLine : eIDrawing
    {
        #region Fields
        /// <summary>
        /// Holds the value of 'Extension'.
        /// </summary>
        private float extension;
        /// <summary>
        /// Holds the value of 'Amplitude'.
        /// </summary>
        private float amplitude;
        /// <summary>
        /// Holds the value of 'Point1'.
        /// </summary>
        private PointF point1;
        /// <summary>
        /// Holds the value of 'Point2'.
        /// </summary>
        private PointF point2;
        /// <summary>
        /// Holds the value of 'LineWeight'.
        /// </summary>
        private eLineWeight lineWeight;
        /// <summary>
        /// Holds the value of 'Tension'.
        /// </summary>
        private float tension;
        /// <summary>
        /// Holds the value of 'LineType'.
        /// </summary>
        private eLineType lineType;
        /// <summary>
        /// Holds the value of 'Color'.
        /// </summary>
        private eColor color;
        /// <summary>
        /// Holds the value of 'Layer'.
        /// </summary>
        private eLayer layer;
        /// <summary>
        /// Holds the value of 'Size'.
        /// </summary>
        private float size;
        private List<eLine> dwgObjects;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a set of lines which is used to indicate discontinuity in drawings.
        /// </summary>
        /// <param name="layer">The layer on which the break line is to be drawn.</param>
        /// <param name="point1">The first point of the break line.</param>
        /// <param name="point2">The second point of the break line.</param>
        public eBreakLine(eLayer layer, PointF point1, PointF point2)
        {
            this.layer = layer;
            this.point1 = point1;
            this.point2 = point2;

            float l = (float)Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));

            this.extension = 0.1f * l;
            this.amplitude = 0.25f * l;
            this.tension = 0.35f;
            this.size = 1.0f;

            GenerateDwgObjects();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the first point of the break line.
        /// </summary>
        public PointF Location
        {
            get
            {
                return this.point1;
            }
            set
            {
                this.point2.X += (value.X - point1.X);
                this.point2.Y += (value.Y - point1.Y);
                this.point1 = value;
                GenerateDwgObjects();
            }
        }

        /// <summary>
        /// Gets or sets the layer on which the break line is drawn.
        /// </summary>
        public eLayer Layer
        {
            get { return this.layer; }
        }

        /// <summary>
        /// Gets or sets the color of the break line
        /// </summary>
        public eColor Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }

        /// <summary>
        /// Gets or sets the extent by which the middle wavy lines protrude from the horizontal lines. Value should be greater than zero.
        /// </summary>
        public float Amplitude
        {
            get
            {
                return this.amplitude;
            }
            set
            {
                if (value > 0)
                {
                    this.amplitude = value;
                    GenerateDwgObjects();
                }
                else
                    throw new Exception("Break line amplitude value cannot be zero or negative");
            }
        }

        /// <summary>
        /// Gets or sets the distance by which the horizontal line extends beyond the end points. Value should be greater than zero.
        /// </summary>
        public float Extention
        {
            get
            {
                return this.extension;
            }
            set
            {
                if (value > 0)
                {
                    this.extension = value;
                    GenerateDwgObjects();
                }
                else
                    throw new Exception("Break line extension value cannot be zero or negative");
            }
        }

        /// <summary>
        /// Gets or sets the first point at which one end of the break line lies.
        /// </summary>
        public PointF Point1
        {
            get
            {
                return this.point1;
            }
            set
            {
                this.point1 = value;
                GenerateDwgObjects();
            }
        }

        /// <summary>
        /// Gets or sets the second point at which the other end of the break line lies.
        /// </summary>
        public PointF Point2
        {
            get
            {
                return this.point2;
            }
            set
            {
                this.point2 = value;
                GenerateDwgObjects();
            }
        }

        /// <summary>
        /// Gets or sets the line weight of the break line.
        /// </summary>
        public eLineWeight LineWeight
        {
            get
            {
                return this.lineWeight;
            }
            set
            {
                this.lineWeight = value;
                foreach (var v in dwgObjects)
                    v.LineWeight = value;
            }
        }

        /// <summary>
        /// Gets or sets the line type of the break line.
        /// </summary>
        public eLineType LineType
        {
            get
            {
                return this.lineType;
            }
            set
            {
                this.lineType = value;
                foreach (var v in dwgObjects)
                    v.LineType = value;
            }
        }

        /// <summary>
        /// Gets or sets the distance the middle wavy part covers relative to the total distance between the two end points. Value shall be between 0.001f and 1.0f.
        /// </summary>
        public float Tension
        {
            get
            {
                return this.tension;
            }
            set
            {
                if (value >= 0.001f && value <= 1.0f)
                {
                    this.tension = value;
                    GenerateDwgObjects();
                }
                else
                    throw new Exception("The value of break line tension should be between 0.001f and 1.0f");
            }
        }

        /// <summary>
        /// Gets or sets the value which indicates the overall largeness of the breakline. It cannot be zero or negative.
        /// </summary>
        public float Size
        {
            get
            {
                return this.size;
            }
            set
            {
                if (value > 0)
                {
                    this.size = value;
                    GenerateDwgObjects();
                }
                else
                    throw new Exception("The value of break line size cannot be zero or negative.");
            }
        }
        #endregion

        #region Methods
        public void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            //if the break line does not zoom properly, go to 'GenerateDwgObjects' method and avoid the addition of the drawing objects directly to the layer and do
            //the jobs of the layer here, i.e. zoom every dwg object in this method.
            foreach (var v in dwgObjects)
                v.Zoom(ZoomCenter, ZoomFactor);
        }

        public void Pan(float Xoffset, float Yoffset)
        {
            //if the break line does not pan properly, go to 'GenerateDwgObjects' method and avoid the addition of the drawing objects directly to the layer and do
            //the jobs of the layer here, i.e. pan every dwg object in this method.
            foreach (var v in dwgObjects)
                v.Pan(Xoffset, Yoffset);
        }

        public void Draw(Graphics g)
        {
            //if the break line does not draw properly, go to 'GenerateDwgObjects' method and avoid the addition of the drawing objects directly to the layer and do
            //the jobs of the layer here, i.e. draw every dwg object in this method.
            foreach (var v in dwgObjects)
                v.Draw(g);
        }

        /// <summary>
        /// Generates all the necessary objects to draw the break line.
        /// </summary>
        private void GenerateDwgObjects()
        {
            if (this.dwgObjects != null)
            {
                foreach (var v in dwgObjects)
                {
                    layer.Remove(v);
                }
            }
            dwgObjects = new List<eLine>();

            PointF extPt1, extPt2, ampPtTop, ampPtBottom, mdlPt1, mdlPt2;
            double θ, l;

            θ = Math.Atan((point1.Y - point2.Y) / (point2.X - point1.X));
            l = Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));

            extPt1 = extPt2 = ampPtBottom = ampPtTop = mdlPt1 = mdlPt2 = new PointF();

            extPt1.X = (float)(point1.X - size * extension * Math.Cos(θ));
            extPt1.Y = (float)(point1.Y + size * extension * Math.Sin(θ));

            extPt2.X = (float)(point2.X + size * extension * Math.Cos(θ));
            extPt2.Y = (float)(point2.Y + size * extension * Math.Sin(θ));

            mdlPt1.X = (float)(point1.X + tension * l * Math.Cos(θ));
            mdlPt1.Y = (float)(point1.Y - tension * l * Math.Sin(θ));

            mdlPt2.X = (float)(point2.X - tension * l * Math.Cos(θ));
            mdlPt2.Y = (float)(point2.Y + tension * l * Math.Sin(θ));

            ampPtTop.X = (float)(mdlPt1.X + (l / 16) * Math.Cos(θ) - amplitude * Math.Sin(θ));
            ampPtTop.Y = (float)(mdlPt1.Y - (l / 16) * Math.Sin(θ) - amplitude * Math.Cos(θ));

            ampPtBottom.X = (float)(mdlPt2.X - (l / 16) * Math.Cos(θ) + amplitude * Math.Sin(θ));
            ampPtBottom.Y = (float)(mdlPt2.Y + (l / 16) * Math.Sin(θ) + amplitude * Math.Cos(θ));

            //this.dwgObjects.Add(layer.AddLine(extPt1, mdlPt1));
            //this.dwgObjects.Add(layer.AddLine(mdlPt1, ampPtTop));
            //this.dwgObjects.Add(layer.AddLine(ampPtTop, ampPtBottom));
            //this.dwgObjects.Add(layer.AddLine(ampPtBottom, mdlPt2));
            //this.dwgObjects.Add(layer.AddLine(mdlPt2, extPt2));

            this.dwgObjects.Add(new eLine(extPt1, mdlPt1, layer));
            this.dwgObjects.Add(new eLine(mdlPt1, ampPtTop, layer));
            this.dwgObjects.Add(new eLine(ampPtTop, ampPtBottom, layer));
            this.dwgObjects.Add(new eLine(ampPtBottom, mdlPt2, layer));
            this.dwgObjects.Add(new eLine(mdlPt2, extPt2, layer));
        }
        #endregion
    }
}
