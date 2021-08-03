using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Represents a line with or without arrow head to show a specific place in drawing.
    /// </summary>
    [Serializable]
    public class eLeader : eIDrawing
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
        private eLineWeight lineWeigth;
        /// <summary>
        /// Holds a value for public property 'TextStyle'.
        /// </summary>
        private eTextStyle textStyle;
        /// <summary>
        /// The end arrow pointing to the direction of the point to be indicated.
        /// </summary>
        private eArrowHead arrowHead;
        /// <summary>
        /// The set of points in the order from the start of the leader to its tip(arrow).
        /// </summary>
        private PointF[] points;
        /// <summary>
        /// The dot at the start of the leader.
        /// </summary>
        private eCircle dot;
        /// <summary>
        /// Holds the value of the 'ArrowAndDotSize' property.
        /// </summary>
        private float arrowAndDotSize;
        /// <summary>
        /// Holds the value of the 'Layer' propery.
        /// </summary>
        private eLayer layer;
        /// <summary>
        /// Holds the value of the 'SuppressArrow' property.
        /// </summary>
        private bool suppressArrow;
        /// <summary>
        /// Holds the value of the 'SuppressDot' property.
        /// </summary>
        private bool suppressDot;
        /// <summary>
        /// Holds the value of the 'Type' property.
        /// </summary>
        private eLeaderType type;
        /// <summary>
        /// Holds the value of the 'Curvature' property.
        /// </summary>
        private float curvature;
        /// <summary>
        /// The arc object for arc type leader.
        /// </summary>
        private eArc arc;
        /// <summary>
        /// Holds the value of the 'TopLeft' property.
        /// </summary>
        private PointF start;
        /// <summary>
        /// Holds the value of the 'BottomRight' property.
        /// </summary>
        private PointF end;
        /// <summary>
        /// Holds the vallue of the 'CureveDirection' property.
        /// </summary>
        private eLeaderCurveDirection curveDirection;
        /// <summary>
        /// The text_left object to be drawn together with the leader.
        /// </summary>
        private eText text;
        /// <summary>
        /// Holds the value of the 'UnderLine' property.
        /// </summary>
        private bool underLine;
        /// <summary>
        /// Holds the value of the 'CircleText' property.
        /// </summary>
        private bool circleText = false;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates new instance of a leader to indicate a point. This constructor cannot be used to form an arc type leader.
        /// </summary>
        /// <param name="type">The type of leader to be drawn. It cannot be an arc type leader.</param>
        /// <param name="layer">The layer on which to draw the leader.</param>
        /// <param name="text_left">The text_left to be drawn at the start of the leader.</param>
        /// <param name="underline">Boolean value to indicate whether to underline the text_left or not.</param>
        /// <param name="points">The sequence of points through which the leader passes.</param>
        public eLeader(eLeaderType type, eLayer layer, string text = null, bool underline = false, params PointF[] points)
        {
            if (points.Length < 2)
                throw new eGraphicsException("Leader cannot be formed with points less than two points.");
            if (type == eLeaderType.Arc)
                throw new eGraphicsException("Arc type leader cannot be formed with this constructor.");

            this.type = type;
            this.layer = layer;
            this.color = layer.Color;
            this.lineType = layer.LineType;
            this.lineWeigth = layer.LineWeight;
            this.textStyle = layer.TextStyle;
            this.points = points;

            this.arrowAndDotSize = 5;
            this.start = points[0];
            this.end = points[points.Length - 1];
            this.suppressArrow = false;
            this.suppressDot = false;
            this.underLine = underline;

            float ang = (float)(curvature + Math.Atan2(this.points[points.Length - 1].Y - this.points[points.Length - 2].Y, this.points[points.Length - 1].X - this.points[points.Length - 2].X) * 180.0 / Math.PI);
            this.arrowHead = new eArrowHead(this.points[points.Length - 1], 180 + ang, 0.83f * this.arrowAndDotSize, 2.5f * this.arrowAndDotSize, this.layer);
            this.dot = new eCircle(start, 0.5f * this.arrowAndDotSize, eDrawType.Fill, HatchStyle.Cross, layer);
            dot.FillColor = this.color;

            if (text != null)
            {
                this.text = new eText(text, start, this.layer);
                if (underline)
                    suppressDot = true;
            }
            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified);
        }

        /// <summary>
        /// Creates new instance of a leader to indicate a point. This constructor cannot be used to form an arc type leader.
        /// </summary>
        /// <param name="type">The type of the leader to be drawn. It cannot be an arc type leader.</param>
        /// <param name="layer">The layer on which to draw the leader on.</param>
        /// <param name="lineType">The type of line of the leader.</param>
        /// <param name="suppressArrow">Omits the arrow if true.</param>
        /// <param name="suppressDot">Omits the dot at the start if true.</param>
        /// <param name="text_left">The text_left to be written with the  leader.</param>
        /// <param name="underline">Indicates whether to underline the text_left or not.</param>
        /// <param name="points">The sequence of points through which the leader passes.</param>
        public eLeader(eLeaderType type, eLayer layer, eLineTypes lineType, bool suppressArrow, bool suppressDot, string text = null, bool underline = false, params System.Drawing.PointF[] points)
        {
            if (points.Length < 2)
                throw new eGraphicsException("Leader cannot be formed with points less than two points.");
            if (type == eLeaderType.Arc)
                throw new eGraphicsException("Arc type leader cannot be formed with this constructor.");

            this.type = type;
            this.layer = layer;
            this.color = layer.Color;
            this.lineType = layer.LineType;
            this.lineWeigth = layer.LineWeight;
            this.points = points;
            this.start = points[0];
            this.end = points[points.Length - 1];

            this.arrowAndDotSize = 5;
            this.start = points[0];
            this.end = points[points.Length - 1];
            this.suppressArrow = suppressArrow;
            this.suppressDot = suppressDot;
            this.underLine = underline;

            float ang = (float)(curvature + Math.Atan2(this.points[points.Length - 1].Y - this.points[points.Length - 2].Y, this.points[points.Length - 1].X - this.points[points.Length - 2].X) * 180.0 / Math.PI);
            this.arrowHead = new eArrowHead(this.points[points.Length - 1], arc.Rotation + 180, 0.83f * this.arrowAndDotSize, 2.5f * this.arrowAndDotSize, this.layer);
            this.dot = new eCircle(start, 0.5f * this.arrowAndDotSize, eDrawType.Fill, HatchStyle.Cross, layer);
            dot.FillColor = this.color;

            if (text != null)
            {
                this.text = new eText(text, start, this.layer);
                if (underline)
                    suppressDot = true;
            }
            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified);
        }

     

        /// <summary>
        /// Creates an arc type leader with the start and end points and with a given curvature value.
        /// </summary>
        /// <param name="startPoint">The start point of the leader where the text_left is to be placed.</param>
        /// <param name="endPoint">The end point of the leader at the tip of the arrow.</param>
        /// <param name="curvature">The curvature value for the arc from the startPoint to the end leader with a number between 0 and 90.</param>
        /// <param name="layer">the layer on which the leader is drawn.</param>
        /// <param name="lineType">The line type of the leader.</param>
        /// <param name="text_left">The text_left to be written with the leader.</param>
        /// <param name="underline">Boolean value whether to underline the text_left or not.</param>
        public eLeader(PointF startPoint, PointF endPoint, float curvature, eLayer layer, eLineTypes lineType, string text = null, eLeaderCurveDirection curveDirection = eLeaderCurveDirection.ToTheLeft,
            bool circleText = false, bool underline = false, bool suppressArrow = false, bool suppressDot = false)
        {
            this.start = startPoint;
            this.end = endPoint;
            this.arc = new eArc(startPoint, endPoint, curvature, layer);
            this.layer = layer;
            this.color = layer.Color;
            this.lineType = layer.LineType;
            this.lineWeigth = layer.LineWeight;
            this.suppressArrow = suppressArrow;
            this.suppressDot = suppressDot;
            this.circleText = circleText;
            this.arrowAndDotSize = 5;
            this.curvature = curvature;
            this.arc.Curvature = curvature;
            this.underLine = underline;

            this.type = eLeaderType.Arc;
            this.arrowHead = new eArrowHead(endPoint, arc.Rotation + 180.0f, 0.83f * this.arrowAndDotSize, 2.5f * this.arrowAndDotSize, this.layer);
            this.dot = new eCircle(startPoint, 0.5f * this.arrowAndDotSize, eDrawType.Fill, HatchStyle.Cross, layer);
            dot.FillColor = this.color;

            if (text != null)
            {
                this.text = new eText(text, startPoint, this.layer);
                if (underline)
                    suppressDot = true;
            }
            this.CurveDirection = curveDirection;
            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified);
        }
        #endregion

        #region Properties
        public eTextStyle TextStyle
        {
            get { return textStyle; }
            set { textStyle.SetFont(value); }
        }
        /// <summary>
        /// Gets or sets the first point of the leader where the dot is located.
        /// </summary>
        public PointF Location
        {
            get
            {
                return this.start;
            }
            set
            {
                this.start = value;
            }
        }

        /// <summary>
        /// Gets or sets the layer on which the leader is drawn.
        /// </summary>
        public eLayer Layer
        {
            get { return this.layer; }
        }

        /// <summary>
        /// Gets or sets the arrow and dot size of the leader.
        /// </summary>
        public float ArrowAndDotSize
        {
            get
            {
                return this.arrowAndDotSize;
            }
            set
            {
                this.arrowAndDotSize = value;
                this.dot.Radius = 0.5f * value;
                this.arrowHead.Width = 0.83f * value;
                this.arrowHead.Height = 2.5f * value;
            }
        }

        /// <summary>
        /// Gets or sets the value if the head arrow is to be omited.
        /// </summary>
        public bool SuppressArrow
        {
            get
            {
                return this.suppressArrow;
            }
            set
            {
                this.suppressArrow = value;
            }
        }

        /// <summary>
        /// Gets or sets the value if the start dot is to be omitted.
        /// </summary>
        public bool SuppressDot
        {
            get
            {
                return this.suppressDot;
            }
            set
            {
                this.suppressDot = value;
            }
        }

        /// <summary>
        /// Gets or sets the startPoint of the leader.
        /// </summary>
        public eLeaderType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        /// <summary>
        /// Gets or sets the corner points of the leader in the order from the start to the arrow tip.
        /// </summary>
        public PointF[] Points
        {
            get
            {
                return this.points;
            }
            set
            {
                this.points = value;
            }
        }

        /// <summary>
        /// Gets or sets the curvature value for the arc from the startPoint to the end leader with a number between 0 and 90.
        /// </summary>
        /// <remarks>A small value will make the curve more straight. A larger value cuves the leader highly near the arrow. A default value is 45. If a value greater than 90 is entered, its remainder when divided by 90 is taken. And if the value is negative, the same is done with its absolute value.</remarks>
        public float Curvature
        {
            get
            {
                return this.arc.Curvature;
            }
            set
            {
                this.type = eLeaderType.Arc;
                this.arc.Curvature = value;
            }
        }

        /// <summary>
        /// Gets or sets the start point of the leader.
        /// </summary>
        public PointF Start
        {
            get
            {
                return this.start;
            }
            set
            {
                this.start = value;
                if (!suppressDot)
                    this.dot.Location = value;
            }
        }

        /// <summary>
        /// Gets or sets the end point of the leader.
        /// </summary>
        public PointF End
        {
            get
            {
                return this.end;
            }
            set
            {
                this.end = value;
                if (!suppressArrow)
                    this.arrowHead.Location = value;
            }
        }

        /// <summary>
        /// Gets or sets the direction of curve for an arc type leader. The won't have effect for arc types other than arc type.
        /// </summary>
        public eLeaderCurveDirection CurveDirection
        {
            get
            {
                return this.curveDirection;
            }
            set
            {
                if (this.curveDirection != value)
                {
                    PointF temp = this.arc.Start;
                    this.arc.Start = this.arc.End;
                    this.arc.End = temp;
                    if (value == eLeaderCurveDirection.ToTheLeft)
                        this.arrowHead.Rotation = this.arc.Rotation + 90.0f;
                    else
                        this.arrowHead.Rotation = this.arc.Rotation + 180.0f;
                    this.curveDirection = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the text_left at the start of the leader.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text.Text;
            }
            set
            {
                this.text.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the value whether to put a line under the text_left emanating from the first point of the leader.
        /// </summary>
        public bool UnderLine
        {
            get
            {
                return this.underLine;
            }
            set
            {
                this.underLine = value;
            }
        }

        /// <summary>
        /// Gets or sets the text_left object of the leader.
        /// </summary>
        public eText TextObject
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }

        /// <summary>
        /// Gets or sets the value whether to circle the text_left or not.
        /// </summary>
        public bool CircleText
        {
            get
            {
                return this.circleText;
            }
            set
            {
                this.circleText = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the Leader.
        /// </summary>
        public eColor Color
        {
            get { return color; }
            set
            {
                color.SetColor(value);
            }
        }

        /// <summary>
        /// Gets or sets the line type of the Leader.
        /// </summary>
        public eLineType LineType
        {
            get { return lineType; }
            set
            {
                lineType.SetLineType(value);
            }
        }

        /// <summary>
        /// Gets or sets the line weight of the Leader.
        /// </summary>
        public eLineWeight LineWeight
        {
            get { return lineWeigth; }
            set
            {
                lineWeigth.SetLineWeight(value);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Zooms the leader with a zoom center and zoom factor.
        /// </summary>
        /// <param name="ZoomCenter">The scaling origin relative to which the leader is going to be zoomed.</param>
        /// <param name="ZoomFactor">The scale factor to zoom the leader with.</param>
        public void Zoom(System.Drawing.PointF ZoomCenter, float ZoomFactor)
        {
            this.start.X = (start.X - ZoomCenter.X) * ZoomFactor + ZoomCenter.X;
            this.start.Y = (start.Y - ZoomCenter.Y) * ZoomFactor + ZoomCenter.Y;
            this.end.X = (end.X - ZoomCenter.X) * ZoomFactor + ZoomCenter.X;
            this.end.Y = (end.Y - ZoomCenter.Y) * ZoomFactor + ZoomCenter.Y;

            if (this.type == eLeaderType.Arc)
                this.arc.Zoom(ZoomCenter, ZoomFactor);
            else
            {
                for (int i = 0; i < this.points.Length; i++)
                {
                    points[i].X = (points[i].X - ZoomCenter.X) * ZoomFactor + ZoomCenter.X;
                    points[i].Y = (points[i].Y - ZoomCenter.Y) * ZoomFactor + ZoomCenter.Y;
                }
            }

            if (!this.suppressArrow)
                this.arrowHead.Zoom(ZoomCenter, ZoomFactor);
            if (!this.suppressDot)
                this.dot.Zoom(ZoomCenter, ZoomFactor);

            if (this.text != null)
                this.text.Zoom(ZoomCenter, ZoomFactor);
        }

        /// <summary>
        /// Pans the leader with a given x and y offset.
        /// </summary>
        /// <param name="Xoffset">Amount of offset in x direction.</param>
        /// <param name="Yoffset">Amount of offset in y direction.</param>
        public void Pan(float Xoffset, float Yoffset)
        {
            if (this.type == eLeaderType.Arc)
                this.arc.Pan(Xoffset, Yoffset);
            else
            {
                for (int i = 0; i < this.points.Length; i++)
                {
                    points[i].X += Xoffset;
                    points[i].Y += Yoffset;
                }
            }

            this.start.X += Xoffset;
            this.start.Y += Yoffset;
            this.end.X += Xoffset;
            this.end.Y += Yoffset;

            if (!this.suppressArrow)
                this.arrowHead.Pan(Xoffset, Yoffset);
            if (!this.suppressDot)
                this.dot.Pan(Xoffset, Yoffset);

            if (this.text != null)
                this.text.Pan(Xoffset, Yoffset);
        }

        /// <summary>
        /// Draws the leader given a graphics object.
        /// </summary>
        /// <param name="g">The graphics object on which to draw the leader on.</param>
        
        public void Draw(System.Drawing.Graphics g)
        {
            Pen pen = new Pen(this.color,this.lineWeigth);
            if (this.lineType != eLineTypes.Continuous)
                pen.DashPattern = this.lineType.DashPatern;
            pen.EndCap = LineCap.ArrowAnchor;

            switch (this.type)
            {
                case eLeaderType.Arc:
                    this.arc.Draw(g);
                    break;
                case eLeaderType.Spline:
                    g.DrawCurve(pen, this.points);
                    break;
                case eLeaderType.Straight:
                    g.DrawLines(pen, points);
                    break;
            }

            if (!this.suppressDot)
                this.dot.Draw(g);
            if (!this.suppressArrow)
                this.arrowHead.Draw(g);

            if (this.text != null)
            {
                if (circleText)
                    IncircleText(g, pen);
                else
                    UnderLineText(g);
                this.text.Draw(g);
            }
        }

        private void IncircleText(Graphics g, Pen pen)
        {
            if (this.type == eLeaderType.Arc)
            {
                SizeF txtsz = g.MeasureString(this.text.Text, this.textStyle);
                float d = txtsz.Width > txtsz.Height ? txtsz.Width : txtsz.Height;
                RectangleF rect = new RectangleF(new PointF(this.start.X, this.start.Y - d / 2.0f), new SizeF(d, d));
                float ang = (float)Math.Atan2(end.Y - start.Y, start.X - end.X);

                this.text.Location = new PointF((float)(this.start.X + (d / 2.0) * Math.Cos(ang)), (float)(start.Y - (d / 2.0) * Math.Sin(ang)));

                ang *= (float)(-180.0 / Math.PI);

                Matrix m = new Matrix();
                m.RotateAt(ang, this.start);
                g.Transform = m;
                g.DrawEllipse(pen, rect);
                g.ResetTransform();
            }
        }

        /// <summary>
        /// Adds a line at the start of the leader so that the text_left is underlined.
        /// </summary>
        /// <param name="g">A graphics object to measure the size of the string .</param>
        private void UnderLineText(Graphics g)
        {
            PointF endPt = new PointF();
            PointF txtLoc = new PointF();
            SizeF txtSize = g.MeasureString(this.text.Text, this.textStyle);
            float ang = (float)(this.text.Angle * Math.PI / 180.0f);

            float b;

            endPt.X = (float)(start.X + txtSize.Width * Math.Cos(ang));
            endPt.Y = (float)(start.Y - txtSize.Width * Math.Sin(ang));

            if (0.0f <= ang && (Math.Round(ang, 3) * 180.0f / Math.PI) <= 90.0f)
            {
                b = 0.9f * txtSize.Height;
                txtLoc.X = (float)((start.X + endPt.X) / 2.0f + (b * Math.Sin(ang)));
                txtLoc.Y = (float)((start.Y + endPt.Y) / 2.0f - (b * Math.Cos(ang)));
            }
            else if (Math.PI / 2.0f < ang && ang < Math.PI)
            {
                b = 0.9f * txtSize.Height;
                txtLoc.X = (float)((start.X + endPt.X) / 2.0f + (b * Math.Sin(ang))) + 0.5f * b;
                txtLoc.Y = (float)((start.Y + endPt.Y) / 2.0f + (b * Math.Cos(ang))) + b;
            }
            else if (Math.PI < ang && ang < 3.0f * Math.PI / 2.0f)
            {
                b = 1.7f * txtSize.Height;
                txtLoc.X = (float)((start.X + endPt.X) / 2.0f - (b * Math.Sin(ang)));
                txtLoc.Y = (float)((start.Y + endPt.Y) / 2.0f + (b * Math.Cos(ang)));
            }
            else
            {
                b = 0.9f * txtSize.Height;
                txtLoc.X = (float)((start.X + endPt.X) / 2.0f - (b * Math.Sin(ang))) + 0.5f * b;
                txtLoc.Y = (float)((start.Y + endPt.Y) / 2.0f - (b * Math.Cos(ang))) - 2.0f * b;
            }

            this.text.Location = txtLoc;

            if (this.underLine)
            {
                eLine l = new eLine(this.start, endPt, this.layer);
                l.Draw(g);
            }
        }

        /// <summary>
        /// Sets the path of the leader in the order from the start to the end.
        /// </summary>
        /// <param name="point">The points through which the leader passes in the order from the start to the end(arrow).</param>
        public void SetPoints(params PointF[] point)
        {
            this.points = point;
            this.Start = point[0];
            this.End = point[point.Length - 1];

            float ang = (float)(curvature + Math.Atan2(this.points[points.Length - 1].Y - this.points[points.Length - 2].Y, this.points[points.Length - 1].X - this.points[points.Length - 2].X) * 180.0 / Math.PI);
            this.arrowHead.Rotation = ang + 180;
        }

        /// <summary>
        /// Event handler for ESADS.EGraphics.eLayer.Modifeid event;
        /// </summary>
        /// <param name="sender">The layer sending this event.</param>
        /// <param name="e">Event argument containing all the necessary information related to the event.</param>
        private void layer_Modified(eLayer sender, eLayerModifiedEventArgs e)
        {
            if (this.color.ChangeBy == eChangeBy.ByLayer)
                this.color.SetColor(e.Color);
            if (this.textStyle.ChangeBy == eChangeBy.ByLayer)
                this.textStyle.SetFont(e.TextStyle);
            if (this.lineWeigth.ChangeBy == eChangeBy.ByLayer)
                this.lineWeigth.SetLineWeight(e.LineWeight);
            if (this.lineType.ChangeBy == eChangeBy.ByLayer)
                this.lineType.SetLineType(e.LineType);
        }
        #endregion
    }
}