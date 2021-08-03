using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Mechanics.Analysis.Beam;

namespace ESADS.Mechanics.Analysis.Slab
{
    public class eATwoWayPanel: eAPanel
    {
        private double mys1;
        private double mys2;

        public eATwoWayPanel(ePoint topLeft, ePoint topRight, ePoint bottomRight, ePoint bottomLeft)
            : base(topLeft, topRight, bottomRight, bottomLeft)
        {

        }

        protected override void CalculateMoments()
        {
            
        }
    }
}
