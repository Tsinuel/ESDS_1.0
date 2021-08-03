using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS;
using ESADS.Mechanics.Analysis;
using ESADS.Code;
using ESADS.Code.EBCS_1995;

namespace ESADS.Mechanics.Analysis.Slab
{
    public class eSAreaLoad : eASLoad
    {

        private double area;
        private double[] areas;

        public eSAreaLoad(List<ePoint> points, eActionType actionType, double magnitude)
            : base(points, actionType, magnitude)
        {
            this.loadType = eSlabLoadTypes.AreaLoad;
            area = eMath.GetArea(points);
            FillAreas();
        }

        public double GetAreaOn(eAPanel panel)
        {
            return areas[touchingPanels.IndexOf(panel)];
        }

        private void FillAreas()
        {
            List<List<ePoint>> Areas;
            double A;
            areas = new double[touchingPanels.Count];
            for (int i = 0; i < touchingPanels.Count; i++)
            {
                Areas = eMath.GetAreas(points, touchingPanels[i].TL, touchingPanels[i].TR, touchingPanels[i].BR, touchingPanels[i].BL);
                A = 0;
                for (int j = 0; j < Areas.Count; j++)
                {
                    if (Areas[j].Count >= 3)
                    {
                        A += eMath.GetArea(Areas[i]);
                    }
                }
             areas[i] = A;
            }
        }

        public override double GetTotalLoadOn(eAPanel panel)
        {
            return magnitude * areas[touchingPanels.IndexOf(panel)];
        }
    }
}
