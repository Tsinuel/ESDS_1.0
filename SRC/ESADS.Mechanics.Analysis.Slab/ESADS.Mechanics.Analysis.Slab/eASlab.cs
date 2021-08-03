using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Mechanics.Analysis;
using ESADS.Mechanics.Design.Slab;
namespace ESADS.Mechanics.Analysis.Slab
{
    public class eASlab
    {

        private eDSlab dslab;
        private List<eASBeam> beams;
        private List<eAPanel> panels;

        public eASlab()
        {
            this.beams = new List<eASBeam>();
            this.panels = new List<eAPanel>();
        }

        public List<eAPanel> Panels
        {
            get
            {
                return panels;
            }
            set
            {
                panels = value;
            }
        }

        public eDSlab DSlab
        {
            get
            {
                return dslab;
            }
            set
            {
                dslab = value;
            }
        }

        public List<eASBeam> Beams
        {
            get
            {
                return beams;
            }
            set
            {
                beams = value;
            }
        }

        private void FillPanelsInfo()
        {
        }

        private void AdjustMoments()
        {
        }

        private List<eAPanel> GetOneWaySlabs()
        {
            throw new NotFiniteNumberException ();
        }

        public eAPanel Add(eAPanel panel)
        {
            panels.Add(panel);
            return panel;
        }

        private void FillSpanType()
        {
        }

        public eASBeam AddBeam(eASBeam beam)
        {
            this.beams.Add(beam);
            return beam;
        }
    }
}
