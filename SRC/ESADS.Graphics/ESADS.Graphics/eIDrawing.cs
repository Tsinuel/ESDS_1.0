using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Represent a graphic object than can adergo graphic processes like zooming,paning,and drawing.
    /// </summary>
    public interface eIDrawing
    {

        /// <summary>
        /// Gets or sets the start of the drawing object.
        /// </summary>
        PointF Location
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the layer of the drawing on which the graphic object  is drawn.
        /// </summary>
        eLayer Layer
        {
            get;
        }

        /// <summary>
        /// Gets or sets the color of this drawing object.
        /// </summary>
        eColor Color
        {
            get;
            set;
        }    

        /// <summary>
        /// Zooms the graphic object on which it is called by the given zoom factor and zooming center.
        /// </summary>
        /// <param name="ZoomCenter">The zoom ceter from which the zooming is done.</param>
        /// <param name="ZoomFactor">The zoom factor by which the drawing object is enlarged.</param>
        void Zoom(PointF ZoomCenter,float ZoomFactor);

        /// <summary>
        /// Pans or moves graphic object on which it is called by by specified offset in both axis.
        /// </summary>
        ///<param name="Xoffset">The X-Offset by which the object is moved.</param>
        ///<param name="Yoffset">The Y-Offset by which the object is moved.</param>
        void Pan(float Xoffset,float Yoffset);

        /// <summary>
        /// Draws graphic object on which it is called.
        /// </summary>
        ///<param name="g"> The graphic object on which the element is to be drawn.</param>
        void Draw(Graphics g);
    }
}
