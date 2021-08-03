using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ESADS.GUI;
using System.Windows.Forms;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Represents the universal coordinate system axis icon.
    /// </summary>
    [Serializable]
    public struct eAxisIcon
    {
        #region Feilds

        /// <summary>
        /// Holds a value for public property 'location'.
        /// </summary>
        private PointF location;
        /// <summary>
        /// Holds a value for public property 'Color'.
        /// </summary>
        private Color color;
        /// <summary>
        /// Contains the drawing form on which the axis icon
        /// </summary>
        [NonSerialized]
        private Form dwgForm;

        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of ESADS.eAxisIcon structure given the form on which drawing is done.
        /// </summary>
        /// <param name="dwgForm">The drawing form where the axis icon found.</param>
        public eAxisIcon(Form dwgForm)
        {
            this.dwgForm = dwgForm;
            this.location = new PointF(5.0f, dwgForm.ClientSize.Height - 5.0f);
            this.color = Color.FromArgb(255 - (int)dwgForm.BackColor.R, 255 - (int)dwgForm.BackColor.G, 255 - (int)dwgForm.BackColor.B);
            this.dwgForm.BackColorChanged += new EventHandler(dwgForm_BackColorChanged);
            this.dwgForm.SizeChanged += new EventHandler(dwgForm_SizeChanged);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the color of the axis icon.
        /// </summary>
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        /// <summary>
        /// Gets or sets the start of the axis icon.
        /// </summary>
        public PointF Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Zooms the axis icon on which it is called.
        /// </summary>
        /// <param name="ZoomCenter">The zoom center from which zooming is done.</param>
        /// <param name="ZoomFactor">The factor by which the drawing is elarged.</param>
        public void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            this.location.X = ZoomFactor * (this.location.X - ZoomCenter.X) + ZoomCenter.X;
            this.location.Y = ZoomFactor * (this.location.Y - ZoomCenter.Y) + ZoomCenter.Y;
        }

        /// <summary>
        /// Pans or moves the axis icon by the specifeid offesets in both axis.
        /// </summary>
        /// <param name="XOffset">The distance moved in x-direction</param>
        /// <param name="YOffset">The distance moved in y-direction</param>
        public void Pan(float XOffset, float YOffset)
        {
            this.location.X += XOffset;
            this.location.Y += YOffset;
        }

        /// <summary>
        /// Draws the ixis icon on which it is called.
        /// </summary>
        /// <param name="g">The graphic object on which drawing is done.</param>
        public void Draw(Graphics g)
        {

            Pen p = new Pen(color, 1.0f);

            //Calculates the minimum window size from the drawing Form.
            float minWidowDim = dwgForm.ClientSize.Width > dwgForm.ClientSize.Height ? dwgForm.ClientSize.Height : dwgForm.ClientSize.Width;

            //draws the rectangle of the universal coordinate system.
            g.DrawRectangle(p, location.X - minWidowDim / 120f, location.Y - minWidowDim / 120f, minWidowDim / 60f, minWidowDim / 60f);

            //Draws  the axis of the universal coordinate system.
            g.DrawLine(p, location, new PointF(location.X, location.Y - minWidowDim / 10f));
            g.DrawLine(p, location, new PointF(location.X + minWidowDim / 10f, location.Y));
            g.DrawString("Y", new Font("Arial", 15), new SolidBrush(Color), new PointF(location.X - 10, location.Y - minWidowDim /5.6f));
            g.DrawString("X", new Font("Arial", 15), new SolidBrush(Color), new PointF(location.X + minWidowDim / 7f, location.Y - 10));

            //Draws the arrows of the universal coordinate system.
            g.DrawPolygon(p, new PointF[3] { new PointF(location.X + minWidowDim / 10.0f, location.Y - minWidowDim / 120f), new PointF(location.X + minWidowDim / 10f + minWidowDim / 30f, location.Y), new PointF(location.X + minWidowDim / 10f, location.Y + minWidowDim / 120f) });
            g.DrawPolygon(p, new PointF[3] { new PointF(location.X - minWidowDim / 120f, location.Y - minWidowDim / 10f), new PointF(location.X, location.Y - minWidowDim / 10f - minWidowDim / 30f), new PointF(location.X + minWidowDim / 120f, location.Y - minWidowDim / 10f) });

        }

        /// <summary>
        /// Event handler which changes the color of the axis icon when ever the color of the draw from change.The color changed to give best contrast.
        /// </summary>
        /// <param name="sender">The object sending the event</param>
        /// <param name="e">The event argument.</param>
        private void dwgForm_BackColorChanged(object sender, EventArgs e)
        {
            this.color = Color.FromArgb(255 - (int)dwgForm.BackColor.R, 255 - (int)dwgForm.BackColor.G, 255 - (int)dwgForm.BackColor.B);
            this.dwgForm.Invalidate();
        }

        /// <summary>
        /// Event hadler which change the size of the axis icon when ever the size of the draw from change.
        /// </summary>
        /// <param name="sender">The object sending the event</param>
        /// <param name="e">The event argument.</param>
        private void dwgForm_SizeChanged(object sender, EventArgs e)
        {
            this.dwgForm.Invalidate();
        }

        public static implicit operator PointF(eAxisIcon ucs)
        {
            return ucs.location;
        }
        #endregion

        /// <summary>
        /// Converts a point from UCS coordinates to screen coordinates.
        /// </summary>
        /// <param name="point">The point to be converted.</param>
        public ePoint Convert(ePoint point)
        {
            return new ePoint(point.X - location.X, location.Y - point.Y);
        }

        /// <summary>
        /// Converts a point from UCS coordinates to screen coordinates.
        /// </summary>
        /// <param name="point">The point to be converted.</param>
        public ePoint Convert(PointF point)
        {
            return new ePoint(point.X - location.X, location.Y - point.Y);
        }

    }
}
