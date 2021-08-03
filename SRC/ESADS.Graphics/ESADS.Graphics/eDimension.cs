using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Represents the combination of lines, arrows and text_left to show the distance between two points on drawing.
    /// </summary>
    [Serializable]
    public class eDimension : eIDrawing
    {
        #region Fields
        /// <summary>
        /// Holds a value for property 'Color'.
        /// </summary>
        private eColor color;
        /// <summary>
        /// Holds a value for property 'LineType'.
        /// </summary>
        private eLineType lineType;
        /// <summary>
        /// Holds a value for property 'LineWeigth'.
        /// </summary>
        private eLineWeight lineWeight;
        /// <summary>
        /// Holds a value for public property 'TextStyle'.
        /// </summary>
        private eTextStyle textStyle;
        /// <summary>
        /// Holds the value of the 'Type' property.
        /// </summary>
        private eDimensionType type;
        /// <summary>
        /// Holds the value of the 'TopLeft' property.
        /// </summary>
        private PointF start;
        /// <summary>
        /// Holds the value of the 'BottomRight' property.
        /// </summary>
        private PointF end;
        /// <summary>
        /// The distance from the nearest object point to the tip of the extension lineType.
        /// </summary>
        private float shortExtLength;
        /// <summary>
        /// The relative position of the membDimension lineType to the object to be measured.
        /// </summary>
        private eDimensionLinePosition dimensionLinePosition;
        /// <summary>
        /// The distance from the point to be measured to the first extension lineType point.
        /// </summary>
        private float offsetFromOrigin;
        /// <summary>
        /// The distance from the intersection of the membDimension lineType and extension lineType to the tip of the extension lineType.
        /// </summary>
        private float extBeyondDimLines;
        /// <summary>
        /// A number to scale the arrow head from a predefined height to width ratio.
        /// </summary>
        private float arrowSize;
        /// <summary>
        /// The rotation of the whole membDimension about the start point measured in degrees.
        /// </summary>
        private float rotation;
        /// <summary>
        /// Holds the value of the 'Layer' property.
        /// </summary>
        private eLayer layer;
        /// <summary>
        /// Holds the value of the 'SuppressExt1' property.
        /// </summary>
        private bool suppressExt1;
        /// <summary>
        /// Holds the value of the 'SuppressExt2' property.
        /// </summary>
        private bool suppressExt2;
        /// <summary>
        /// Holds the value of the 'RotateVerticalText' property.
        /// </summary>
        private bool rotateVerticalText;
        /// <summary>
        /// Holds the value of 'Visible' property.
        /// </summary>
        private bool visible;
        /// <summary>
        /// Value of the 'PopText' property.
        /// </summary>
        private bool popText;
        /// <summary>
        /// All the drawing components of the dimension.
        /// </summary>
        private List<eIDrawing> dwgObjects;
        private eText textObj;
        private string text;
        private bool popTextToTheLeft;
        private bool popTextToTheAbove;
        /// <summary>
        /// The angle by which the dimension is rotated for aligned type dimension.
        /// </summary>
        private float angle;
        #endregion

        #region Constructors

        public eDimension(PointF start, PointF end,string dimensionText,  eDimensionType type, eLayer layer, eDimensionLinePosition dlPosition, float shortExtLength)
        {
            this.start = start;
            this.end = end;
            this.type = type;
            this.dimensionLinePosition = dlPosition;
            this.layer = layer;
            this.textStyle = layer.TextStyle;
            this.text = dimensionText;
            this.shortExtLength = shortExtLength;

            InitializeComponents();
            GenerateDWGs();
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the text_left style of the membDimension.
        /// </summary>
        public eTextStyle TextStyle
        {
            get { return textStyle; }
            set
            {
                textStyle.SetFont(value);
                textObj.TextStyle = value;
                GenerateDWGs();
            }
        }

        /// <summary>
        /// Gets or sets the start of the membDimension, which is by default the start point of the dimensio.
        /// </summary>
        public PointF Location
        {
            get 
            { 
                return this.start;
            }
            set 
            {
                this.end.X += (value.X - start.X);
                this.end.Y += (value.Y - start.Y);

                start = value;
                GenerateDWGs();
            }
        }

        /// <summary>
        /// Gets or sets the first point of the text_left to be measured.
        /// </summary>
        public PointF Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
                GenerateDWGs();
            }
        }

        /// <summary>
        /// Gets or sets the second point of the text_left to be measured.
        /// </summary>
        public PointF End
        {
            get
            {
                return end;
            }
            set
            {
                end = value;
                GenerateDWGs();
            }
        }

        /// <summary>
        /// Gets or sets the text_left specifying the text_left value.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                GenerateDWGs();
            }
        }

        /// <summary>
        /// Gets or sets the membDimension startPoint to measure between two points.
        /// </summary>
        public eDimensionType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        /// <summary>
        /// Gets or sets the shorter length from either the start or end point, whichever is nearer.
        /// </summary>
        public float ShortExtensionLength
        {
            get
            {
                return this.shortExtLength;
            }
            set
            {
                this.shortExtLength = value * layer.ZoomFactor;
                GenerateDWGs();
            }
        }

        /// <summary>
        /// Gets or sets a number determining the size of the arrow head.
        /// </summary>
        public float ArrowSize
        {
            get
            {
                return this.arrowSize;
            }
            set
            {
                this.arrowSize = value * layer.ZoomFactor;
                GenerateDWGs();
            }
        }

        /// <summary>
        /// Gets or sets the relative position of the membDimension line to the object to be measured.
        /// </summary>
        public eDimensionLinePosition DimensionLinePosition
        {
            get
            {
                return this.dimensionLinePosition;
            }
            set
            {
                this.dimensionLinePosition = value;
                GenerateDWGs();
            }
        }

        /// <summary>
        /// Gets or sets the extra length of the extension lines from the membDimension line.
        /// </summary>
        public float ExtBeyondDimLines
        {
            get
            {
                return extBeyondDimLines;
            }
            set
            {
                this.extBeyondDimLines = value * layer.ZoomFactor;
                GenerateDWGs();
            }
        }

        /// <summary>
        /// Gets the layer on which the membDimension is drawn
        /// </summary>
        public eLayer Layer
        {
            get
            {
                return layer;
            }
        }

        /// <summary>
        /// Gets or sets the distance from the object to the first point of the extension lines.
        /// </summary>
        public float OffsetFromOrigin
        {
            get
            {
                return this.offsetFromOrigin;
            }
            set
            {
                this.offsetFromOrigin = value * layer.ZoomFactor;
                GenerateDWGs();
            }
        }

        /// <summary>
        /// Gets or sets the value if the first extension line is to be omitted.
        /// </summary>
        public bool SuppressExt1
        {
            get
            {
                return suppressExt1;
            }
            set
            {
                suppressExt1 = value;
                GenerateDWGs();
            }
        }

        /// <summary>
        /// Gets or sets the value if the second extension line is to be omitted.
        /// </summary>
        public bool SuppressExt2
        {
            get
            {
                return suppressExt2;
            }
            set
            {
                suppressExt2 = value;
                GenerateDWGs();
            }
        }

        /// <summary>
        /// Gets or sets whether to rotate the text_left in linear vertical membDimension by nignty degrees type.
        /// </summary>
        public bool RotateVerticalText
        {
            get
            {
                return this.rotateVerticalText;
            }
            set
            {
                this.rotateVerticalText = value;
                GenerateDWGs();
            }
        }

        /// <summary>
        /// Gets or sets the color of the Dimension.
        /// </summary>
        public eColor Color
        {
            get { return color; }
            set
            {
                color.ChangeBy = eChangeBy.ByObject;
                color = value;
                foreach (eIDrawing dwg in dwgObjects)
                    dwg.Color = value;
            }
        }

        /// <summary>
        /// Gets or sets the line type of the Dimension.
        /// </summary>
        public eLineType LineType
        {
            get { return lineType; }
            set
            {
                lineType.ChangeBy = eChangeBy.ByObject;
                lineType.SetLineType(value);
            }
        }

        /// <summary>
        /// Gets or sets the line weight of the Dimension.
        /// </summary>
        public eLineWeight LineWeight
        {
            get { return lineWeight; }
            set
            {
                lineWeight.ChangeBy = eChangeBy.ByObject;
                lineWeight.SetLineWeight(value);
            }
        }

        /// <summary>
        /// Gets or sets if the membDimension is visible.
        /// </summary>
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        }

        /// <summary>
        /// Gets or sets the value if the text_left is put outside the extension line when the space available is not enough for it.
        /// </summary>
        public bool PopText
        {
            get
            {
                return popText;
            }
            set
            {
                popText = value;
                GenerateDWGs();
            }
        }

        /// <summary>
        /// Gets the text_left object used to display the membDimension.
        /// </summary>
        public eText TextObjet
        {
            get
            {
                return this.textObj;
            }
        }

        public bool PopTextToTheLeft
        {
            get
            {
                return popTextToTheLeft;
            }
            set
            {
                popTextToTheLeft = value;
                GenerateDWGs();
            }
        }

        public bool PopTextToTheAbove
        {
            get
            {
                return popTextToTheAbove;
            }
            set
            {
                popTextToTheAbove = value;
                GenerateDWGs();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initalizes the initial default components of the dimension.
        /// </summary>
        private void InitializeComponents()
        {
            this.offsetFromOrigin = 3.0f * layer.ZoomFactor;
            this.extBeyondDimLines = 3.0f * layer.ZoomFactor;
            this.arrowSize = 5.0f * layer.ZoomFactor;
            this.suppressExt1 = false;
            this.suppressExt2 = false;
            this.rotateVerticalText = true;
            this.visible = true;
            this.popTextToTheAbove = false;
            this.popTextToTheLeft = true;
            this.popText = true;

            dwgObjects = new List<eIDrawing>();

        }

        /// <summary>
        /// Regenerates all the drawing components of the dimension.
        /// </summary>
        private void GenerateDWGs()
        {
            dwgObjects = new List<eIDrawing>();
            switch (this.type)
            {
                case eDimensionType.Aligned:
                    AddComponentsForAligned();
                    break;
                case eDimensionType.LinearHorizontal:
                    AddComponentsForLinHor();
                    break;
                case eDimensionType.LinearVertical:
                    AddComponentsForLinVer();
                    break;
            }
        }

        private void AddComponentsForLinHor()
        {
            bool textPopped = false;
            int sign;
            PointF st = start;
            PointF ed = end;

            if (dimensionLinePosition == eDimensionLinePosition.LeftOrAbove)
            {
                sign = -1;
                st.Y = ed.Y = Math.Min(start.Y, end.Y);
            }
            else
            {
                sign = 1;
                st.Y = ed.Y = Math.Max(start.Y, end.Y);
            }
            //
            //Extension lines
            //
            PointF es1 = new PointF();
            PointF ee1 = new PointF();
            PointF es2 = new PointF();
            PointF ee2 = new PointF();

            es1.X = ee1.X = st.X;
            es2.X = ee2.X = ed.X;

            es1.Y = start.Y + sign * offsetFromOrigin;
            es2.Y = end.Y + sign * offsetFromOrigin;

            if (start.Y < end.Y && sign == -1 || start.Y > end.Y && sign == 1)
                ee1.Y = ee2.Y = start.Y + sign * (offsetFromOrigin + shortExtLength + extBeyondDimLines);
            else
                ee1.Y = ee2.Y = end.Y + sign * (offsetFromOrigin + shortExtLength + extBeyondDimLines);

            if (!suppressExt1)
                dwgObjects.Add(new eLine(es1, ee1, layer));
            if (!suppressExt2)
                dwgObjects.Add(new eLine(es2, ee2, layer));            
            //
            //Text
            //
            PointF loc = new PointF((st.X + ed.X) / 2.0f, start.Y + sign * (offsetFromOrigin + shortExtLength));
            textObj = new eText(text, loc, layer);
            textObj.TextStyle = this.textStyle;
            dwgObjects.Add(textObj);
            if ((ed.X - st.X - 5.0f * arrowSize) < textObj.Width && popText) //The space between the extension lines is not enough and the text is going to be placed outside with a leader if popText property is true.
            {
                textPopped = true;
                int sign2;
                if (popTextToTheLeft) //The text is placed to the left of the middle of the dimension line.
                    sign2 = -1;
                else
                    sign2 = 1;

                PointF tp1 = new PointF(); //intersection of the line to the dimension line's middle
                PointF tp2 = new PointF(); //bending point
                PointF tp3 = new PointF(); //End of the horizontal line
                PointF tp4 = new PointF(); //Center of the text.

                tp1.X = (st.X + ed.X) / 2.0f;
                tp1.Y = ee1.Y - sign * extBeyondDimLines;

                tp2.X = tp1.X + sign2 * (extBeyondDimLines + 1.5f * textObj.Height);
                tp2.Y = tp1.Y + sign * (extBeyondDimLines + 1.5f * textObj.Height);

                tp3.X = tp2.X + sign2 * textObj.Width;
                tp3.Y = tp2.Y;

                tp4.X = (tp2.X + tp3.X) / 2.0f;
                tp4.Y = tp2.Y - textObj.Height / 2.0f;

                dwgObjects.Add(new eLine(tp1, tp2, layer));
                dwgObjects.Add(new eLine(tp2, tp3, layer));
                textObj.Location = tp4;
            }

            //
            //Dimension lines
            //
            PointF ds1 = ee1;
            PointF ds2 = ee2;
            PointF de1 = new PointF();
            PointF de2 = new PointF();

            if (textPopped)
            {
                ds1.Y = ds2.Y = ee1.Y - sign * extBeyondDimLines;

                dwgObjects.Add(new eLine(ds1, ds2, layer));

                dwgObjects.Add(new eArrowHead(ds1, 180, arrowSize, layer));
                dwgObjects.Add(new eArrowHead(ds2, 0, arrowSize, layer));
            }
            else
            {
                de1.Y = de2.Y = ds1.Y = ds2.Y = ee1.Y - sign * extBeyondDimLines;
                de1.X = ds1.X + (ds2.X - ds1.X - textObj.Width) / 2.0f;
                de2.X = ds2.X - (ds2.X - ds1.X - textObj.Width) / 2.0f;

                dwgObjects.Add(new eLine(ds1, de1, layer));
                dwgObjects.Add(new eLine(ds2, de2, layer));

                dwgObjects.Add(new eArrowHead(ds1, 0, arrowSize, layer));
                dwgObjects.Add(new eArrowHead(ds2, 180, arrowSize, layer));
            }
        }

        private void AddComponentsForLinVer()
        {
            int sign;
            bool textPopped = false;

            PointF st = start;
            PointF ed = end;

            if (dimensionLinePosition == eDimensionLinePosition.LeftOrAbove)
            {
                sign = -1;
                st.X = ed.X = Math.Min(start.X, end.X);
            }
            else
            {
                sign = 1;
                st.X = ed.X = Math.Max(start.X, end.X);
            }

            //
            // Extension lines
            //

            PointF es1 = start;
            PointF ee1 = start;
            PointF es2 = end;
            PointF ee2 = end;

            es1.X = start.X + sign * offsetFromOrigin;
            es2.X = end.X + sign * offsetFromOrigin;

            if (start.X < end.X && sign == -1 || start.X > end.X && sign == 1)
                ee1.X = ee2.X = start.X + sign * (offsetFromOrigin + shortExtLength + extBeyondDimLines);
            else
                ee1.X = ee2.X = end.X + sign * (offsetFromOrigin + shortExtLength + extBeyondDimLines);

            if (!suppressExt1)
                dwgObjects.Add(new eLine(es1, ee1, layer));
            if (!suppressExt2)
                dwgObjects.Add(new eLine(es2, ee2, layer));

            //
            //Text
            //

            PointF loc = new PointF(st.X + sign * (offsetFromOrigin + shortExtLength), (start.Y + end.Y) / 2.0f);
            textObj = new eText(text, loc, layer);
            textObj.TextStyle = this.textStyle;
            dwgObjects.Add(textObj);

            PointF tp1 = new PointF();
            PointF tp2 = new PointF();
            PointF tp3 = new PointF();
            PointF tp4 = new PointF();

            int sign2;
            if (popTextToTheAbove)
                sign2 = -1;
            else
                sign2 = 1;

            if (rotateVerticalText)
            {
                textObj.Angle = 90;

                if (Math.Abs(st.Y - ed.Y) - arrowSize * 5.0f < textObj.Width && popText)
                {
                    textPopped = true;

                    tp1.X = ee1.X - sign * extBeyondDimLines;
                    tp1.Y = (start.Y + end.Y) / 2.0f;

                    tp2.X = tp1.X + sign * (extBeyondDimLines + 1.5f * textObj.Height);
                    tp2.Y = tp1.Y + sign2 * (extBeyondDimLines + 1.5f * textObj.Height);

                    tp3.X = tp2.X;
                    tp3.Y = tp2.Y + sign2 * textObj.Width;

                    tp4.X = tp2.X - textObj.Height / 2.0f;
                    tp4.Y = (tp2.Y + tp3.Y) / 2.0f;

                    dwgObjects.Add(new eLine(tp1, tp2, layer));
                    dwgObjects.Add(new eLine(tp2, tp3, layer));

                    textObj.Location = tp4;
                }
            }
            else
            {
                if (Math.Abs(st.Y - ed.Y) - arrowSize * 5.0f < textObj.Height && popText)
                {
                    textPopped = true;

                    tp1.X = ee1.X - sign * extBeyondDimLines;
                    tp1.Y = (start.Y + end.Y) / 2.0f;

                    tp2.X = tp1.X + sign * (extBeyondDimLines + 1.5f * textObj.Height);
                    tp2.Y = tp1.Y + sign2 * (extBeyondDimLines + 1.5f * textObj.Height);

                    tp3.X = tp2.X + sign * textObj.Width;
                    tp3.Y = tp2.Y;

                    tp4.X = (tp2.X + tp3.X) / 2.0f;
                    tp4.Y = tp2.Y - textObj.Height / 2.0f;

                    dwgObjects.Add(new eLine(tp1, tp2, layer));
                    dwgObjects.Add(new eLine(tp2, tp3, layer));

                    textObj.Location = tp4;
                }
            }

            //
            //Dimension lines
            //

            PointF ds1 = ee1;
            PointF ds2 = ee2;
            PointF de1 = new PointF();
            PointF de2 = new PointF();

            ds1.X = ds2.X = de1.X = de2.X = ee1.X - sign * extBeyondDimLines;

            if (textPopped)
            {
                dwgObjects.Add(new eLine(ds1, ds2, layer));

                dwgObjects.Add(new eArrowHead(ds1, 270, arrowSize, layer));
                dwgObjects.Add(new eArrowHead(ds2, 90, arrowSize, layer));
            }
            else
            {
                float l = rotateVerticalText ? (Math.Abs(ds1.Y - ds2.Y) - textObj.Width) / 2.0f : (Math.Abs(ds1.Y - ds2.Y) - textObj.Height) / 2.0f;
                de1.Y = ds1.Y > ds2.Y ? ds1.Y - l : ds1.Y + l;
                de2.Y = ds1.Y > ds2.Y ? ds2.Y + l : ds2.Y - l;

                dwgObjects.Add(new eLine(ds1, de1, layer));
                dwgObjects.Add(new eLine(ds2, de2, layer));

                if (ds1.Y < ds2.Y)
                {
                    dwgObjects.Add(new eArrowHead(ds1, 90, arrowSize, layer));
                    dwgObjects.Add(new eArrowHead(ds2, 270, arrowSize, layer));
                }
                else
                {
                    dwgObjects.Add(new eArrowHead(ds1, 270, arrowSize, layer));
                    dwgObjects.Add(new eArrowHead(ds2, 90, arrowSize, layer));
                }
            }
        }

        private void AddComponentsForAligned()
        {
            if (start.X > end.X)
            {
                PointF temp = start;
                start = end;
                end = temp;
            }

            angle = (float)(Math.Atan2(start.Y - end.Y, end.X - start.X) * -180 / Math.PI);
            float length = (float)Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));

            PointF endOrig = end;
            end = new PointF(start.X + length, start.Y);
            AddComponentsForLinHor();
            end = endOrig;
        }

        public void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            this.arrowSize *= ZoomFactor;
            this.extBeyondDimLines *= ZoomFactor;
            this.offsetFromOrigin *= ZoomFactor;
            this.shortExtLength *= ZoomFactor;

            start.X = (start.X - ZoomCenter.X) * ZoomFactor + ZoomCenter.X;
            start.Y = (start.Y - ZoomCenter.Y) * ZoomFactor + ZoomCenter.Y;

            end.X = (end.X - ZoomCenter.X) * ZoomFactor + ZoomCenter.X;
            end.Y = (end.Y - ZoomCenter.Y) * ZoomFactor + ZoomCenter.Y;

            foreach (eIDrawing dwg in dwgObjects)
                dwg.Zoom(ZoomCenter, ZoomFactor);
        }

        public void Pan(float Xoffset, float Yoffset)
        {
            start.X += Xoffset;
            start.Y += Yoffset;

            end.X += Xoffset;
            end.Y += Yoffset;

            foreach (eIDrawing dwg in dwgObjects)
                dwg.Pan(Xoffset, Yoffset);
        }

        public void Draw(Graphics g)
        {
            if (!visible)
                return;
            Matrix m = new Matrix();
            if (type == eDimensionType.Aligned)
            {
                m.RotateAt(angle, start);
                g.Transform = m;
            }
            foreach(eIDrawing dwg in dwgObjects)
                dwg.Draw(g);

            if (type == eDimensionType.Aligned)
            {
                m.RotateAt(-angle, start);
                g.Transform = m;
            }
        }
        #endregion
    }
}
