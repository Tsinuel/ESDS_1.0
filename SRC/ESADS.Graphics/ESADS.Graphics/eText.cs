using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace ESADS.EGraphics
{
    /// <summary>
    /// Represets a textual type of drawing.
    /// </summary>
    [Serializable]
    public class eText : eIDrawing
    {
        #region Feilds
        /// <summary>
        /// Accesses the public property 'Angle'.
        /// </summary>
        private float angle;
        /// <summary>
        /// Accesses the public property 'Text'.
        /// </summary>
        private string text;
        /// <summary>
        /// Accesses the public property 'location'.
        /// </summary>
        private PointF location;
        /// <summary>
        /// Accesses the public property 'Layer'.
        /// </summary>
        private eLayer layer;
        /// <summary>
        /// Holds a value for public property 'RotationCenter'.
        /// </summary>
        private PointF rotationCenter;
        /// <summary>
        /// Holds a value for property 'TextStyle'.
        /// </summary>
        private eTextStyle textStyle;
        /// <summary>
        /// Holds a value for public property 'Color'.
        /// </summary>
        private eColor color;
        /// <summary>
        /// Value of 'Visible'
        /// </summary>
        private bool visible = true;
        /// <summary>
        /// Hold a value for public property 'ID'.
        /// </summary>
        private string id;
        #endregion
       

        #region Properties
        /// <summary>
        /// Gets or sets the ID of this object.
        /// </summary>
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// Gets or sets the rotation angle of the text_left in clockwise direction.
        /// </summary>
        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        /// <summary>
        /// Gets layer of the text_left on which it is drawn.
        /// </summary>
        public eLayer Layer
        {
            get { return layer; }
            set { layer = value; }
        }

        /// <summary>
        /// Gets or sets text_left to be drawn.
        /// </summary>
        public string Text
        {
            get { return text; }
            set {  text= value; }
        }

        /// <summary>
        /// Gets or sets the start of the text_left.
        /// </summary>
        public PointF Location
        {
            get { return location; }
            set
            {
                rotationCenter.X += value.X - location.X;
                rotationCenter.Y += value.Y - location.Y;
                location = value;
            }
        }

        /// <summary>
        /// Gets or sets the rotation center of the text_left.
        /// </summary>
        public PointF RotationCenter
        {
            get { return rotationCenter; }
            set { rotationCenter = value; }
        }
        /// <summary>
        /// Gets or sets the text_left style of this Text.
        /// </summary>
        public eTextStyle TextStyle
        {
            get { return textStyle; }
            set 
            {
                textStyle.Value = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the text_left.
        /// </summary>
        public eColor Color
        {
            get { return this.color; }
            set { color.SetColor(value); }
        }
        #endregion

        /// <summary>
        /// Gets the width of the text_left when drawn.
        /// </summary>
        public float Width
        {
            get
            {
                System.Windows.Forms.Label l = new System.Windows.Forms.Label();
                Graphics g = l.CreateGraphics();
                return g.MeasureString(this.text, this.textStyle).Width;
            }
        }

        /// <summary>
        /// Gets the height of the text_left when drawn.
        /// </summary>
        public float Height
        {
            get
            {
                Graphics g = (new System.Windows.Forms.Label()).CreateGraphics();
                return g.MeasureString(this.text, this.textStyle).Height;
            }
            set
            {
                textStyle.Height = value;
            }
        }

        /// <summary>
        /// Gets or sets if the texr is visible or not.
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

        public void SetFontHeight(double h)
        {
            this.TextStyle = new eTextStyle(new Font(textStyle.Value.Name, (float)h), eChangeBy.ByLayer);
        }
        #region Constructors
        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eText class from all necessary parameters.
        /// </summary>
        /// <param name="text_left">The text_left to be writen.</param>
        /// <param name="location">The start of the top left corner of the bounding rectangle for the text_left.</param>
        /// <param name="angle"> The ange of ration of the text_left in clockwise direction.</param>
        /// <param name="layer"> The layer on which the drawing is going to be done.</param>
        public eText( string text,PointF location,float angle ,eLayer layer)
        {
            this.text = text;
            this.location =  location;
            this.rotationCenter = location;
            this.layer = layer;
            this.angle = angle;
            this.color = layer.Color;
            this.textStyle = layer.TextStyle;
            this.id = "";
            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified);
        }

        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eText class for a given text_left, text_left start and color assuming "Arial" text_left style.
        /// </summary>
        /// <param name="text_left">The text_left to be drawn.</param>
        /// <param name="location">The start of the center of the rectangle containing the text_left.</param>
        /// <param name="layer">The layer where the drawing is going to be drawn.</param>
        public eText(string text, PointF location,eLayer layer)
        {
            this.text = text;
            this.location = location;
            this.rotationCenter = location;
            this.layer = layer;
            this.color = layer.Color;
            this.textStyle = layer.TextStyle;
            this.angle = 0;
            this.layer.Modified += new eLayerModifiedEventHandler(layer_Modified);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Zooms the given text using specified zoom center and zoom factor.
        /// </summary>
        /// <param name="ZoomCenter">The zoom origin from which the zooming is done.</param>
        /// <param name="ZoomFactor">The zoom factor by which the text is elarged.</param>
        public void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            this.location.X = (this.location.X - ZoomCenter.X) * ZoomFactor + ZoomCenter.X;
            this.location.Y = (this.location.Y - ZoomCenter.Y) * ZoomFactor + ZoomCenter.Y;
            this.rotationCenter.X = (this.rotationCenter.X - ZoomCenter.X) * ZoomFactor + ZoomCenter.X;
            this.rotationCenter.Y = (this.rotationCenter.Y - ZoomCenter.Y) * ZoomFactor + ZoomCenter.Y;
            this.textStyle = new eTextStyle((new Font(((Font)this.textStyle).FontFamily, ZoomFactor * ((Font)this.textStyle).Size)),this.textStyle.ChangeBy);
        }

        /// <summary>
        /// Pans or moves the text using the specified offsets in both axises.
        /// </summary>
        /// <param name="Xoffset">The X-Offset by which the text start is moved from the original start in X-direction.</param>
        /// <param name="Yoffset">The Y-Offset by which the text start is moved from the original start in Y-direction.</param>
        public void Pan(float Xoffset,float Yoffset)
        {
            //Transforms or mover the text by the specified vector.
            this.location.X += Xoffset;
            this.location.Y += Yoffset;
            this.rotationCenter.X += Xoffset;
            this.rotationCenter.Y += Yoffset;
        }

        /// <summary>
        /// Draws the text_left in the specified graphic object.
        /// </summary>
        /// <param name="g">The graphic object on which the drawing is going to be drawn.</param>
        public void Draw(Graphics g)
        {
            if (!visible)
                return;
            Matrix mOrig = g.Transform;
            if (angle != 0)
            {
                Matrix m = new Matrix();
                m.RotateAt(-angle, rotationCenter);
                g.Transform = m;
            }
            g.DrawString(text, this.textStyle, new SolidBrush(this.color), GetTextLocation(g));
            if (angle != 0)
            {
                g.Transform = mOrig;
            }
        }

        /// <summary>
        /// Returns the start of the string from 
        /// </summary>
        /// <param name="g">Graphic object used for measuring the string when it is drawn.</param>
        /// <returns></returns>
        private PointF GetTextLocation(Graphics g)
        {
            SizeF txtSize = g.MeasureString(text,this.textStyle);
            return new PointF(location.X - txtSize.Width / 2, location.Y - txtSize.Height / 2);         
        }

        /// <summary>
        /// Event handler for ESADS.EGraphics.eLayer.Modifeid event;
        /// </summary>
        /// <param name="sender">The layer sending this event.</param>
        /// <param name="e">Event argument containing all the necessary information related to the event.</param>
        void layer_Modified(eLayer sender, eLayerModifiedEventArgs e)
        {
            if (this.color.ChangeBy == eChangeBy.ByLayer)
                this.color.SetColor(e.Color);
            if (this.textStyle.ChangeBy == eChangeBy.ByLayer)
                this.textStyle.SetFont(e.TextStyle);
        }
        #endregion
    } 
}
