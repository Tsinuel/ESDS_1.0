using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ESADS.EGraphics.Beam
{
    /// <summary>
    /// Represents a grid point which responds to different Syste.Widows.Form events.
    /// </summary>
    public class eGridPoint : eIDrawing
    {
        #region Feilds
        /// <summary>
        /// Holds a value for property 'Color'.
        /// </summary>
        private eColor color;
        /// <summary>
        /// Holds a value for property 'Layer'.
        /// </summary>
        private eLayer layer;
        /// <summary>
        /// Holds a value for property 'location'.
        /// </summary>
        private PointF location;
        /// <summary>
        /// Holds a value for property 'SnapRadius'.
        /// </summary>
        private float snapRadius;
        /// <summary>
        /// Holds a value for property 'GridPointRadius'.
        /// </summary>
        private float gridPointRadius;
        /// <summary>
        /// Holds a value for property 'Region'.
        /// </summary>
        private Region region;
        /// <summary>
        /// Holds a value for property 'Active'.
        /// </summary>
        private bool active;
        /// <summary>
        /// Holds a value for property 'Selected'.
        /// </summary>
        private bool selected;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or set the location of the point.
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

        /// <summary>
        /// Gets the layer of the grid point.
        /// </summary>
        public eLayer Layer
        {
            get { return layer; }
        }

        /// <summary>
        /// Gets or sets the color of the grid point.
        /// </summary>
        public eColor Color
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
        /// Gets or sets the radius of the grid snap for which the point glows when the mouse aproach.
        /// </summary>
        public float SnapRadius
        {
            get
            {
                return snapRadius;
            }
            set
            {
                snapRadius = value;
            }
        }

        /// <summary>
        /// Gets or sets the radius of the grid point.
        /// </summary>
        public float GridPointRadius
        {
            get
            {
                return gridPointRadius;
            }
            set
            {
                gridPointRadius = value;
            }
        }

        /// <summary>
        /// Gets the region of the grid point including the snap radius.
        /// </summary>
        public Region Region
        {
            get
            {
                return region;
            }
            set
            {
                region = value;
            }
        }

        /// <summary>
        /// Get the value indicating whether the grid poit is active or not.
        /// </summary>
        public bool Active
        {
            get
            {
                return active;
            }
        }

        /// <summary>
        /// Gets or sets the value indicatin wheter the grid point is selected or not.
        /// </summary>
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
            }
        }
        #endregion

        #region Constructors
        public eGridPoint(PointF location,eLayer layer,Form dwgForm)
        {
            this.location = location;
            this.active = false;
            this.selected = false;
            this.snapRadius = 7;
            this.gridPointRadius = 3;
            this.layer = layer;
            dwgForm.MouseClick += new MouseEventHandler(dwgForm_MouseClick);
            dwgForm.MouseMove += new MouseEventHandler(dwgForm_MouseMove);

        }

       
        #endregion

        #region Methods
        /// <summary>
        /// Zooms the grid point on which it is called.
        /// </summary>
        /// <param name="ZoomCenter">The zoom center from which zooming is done.</param>
        /// <param name="ZoomFactor">The factor by which the drawing is elarged.</param>
        public void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            this.location.X = ZoomFactor * (this.location.X - ZoomCenter.X) + ZoomCenter.X;
            this.location.Y = ZoomFactor * (this.location.Y - ZoomCenter.Y) + ZoomCenter.Y;
        }

        /// <summary>
        /// Pans or moves the grid point by the specifeid offesets in both axis.
        /// </summary>
        /// <param name="XOffset">The distance moved in x-direction</param>
        /// <param name="YOffset">The distance moved in y-direction</param>
        public void Pan(float XOffset, float YOffset)
        {
            this.location.X += XOffset;
            this.location.Y += YOffset;
        }

        public void Draw( Graphics g)
        {

        }

        void dwgForm_MouseMove(object sender, MouseEventArgs e)
        {
         
        }

        void dwgForm_MouseClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Forms the region of the Grid Point.
        /// </summary>
        private void FormRegion()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(new RectangleF(location.X - snapRadius, location.Y - snapRadius, 2 * snapRadius, 2 * snapRadius));
            region = new Region(path);
        }
        #endregion
    }
}
