using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Specifies the way to drawType two dimensional drawings, specially closed perimenters.
    /// </summary>
    public enum eDrawType
    {       
        /// <summary>
        /// Draws the perimeter of the two dimensional drawing.
        /// </summary>
        Draw,
        /// <summary>
        /// Fills the object if the two dimensional drawing is a closed one.
        /// </summary>
        Fill,
        /// <summary>
        /// Fills the area and draws the perimeter as well.
        /// </summary>
        FillAndDraw,
        /// <summary>
        /// Hatchs the surounded by the object.
        /// </summary>
        Hatch,
        /// <summary>
        /// First hatchs the area and then draw the exterior
        /// </summary>
        HatchAndDraw,

    }
}
