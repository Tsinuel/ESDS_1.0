using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Code;
using ESADS.Code.EBCS_1995;

namespace ESADS.Mechanics.Analysis.Slab
{
    public abstract class eASLoad
    {
        protected double magnitude;
        protected eActionType actionType;
        protected string name;
        protected List<eAPanel> touchingPanels;
        protected List<ePoint> points;
        protected eSlabLoadTypes loadType;

        public eASLoad(List<ePoint> points, eActionType actionType ,double magnitude)
        {
            this.points = points;
            this.actionType = actionType;
        }

        public eSlabLoadTypes LoadType
        {
            get
            {
                return loadType;
            }
        }
        
        public double Magnitude
        {
            get
            {
                return magnitude;
            }
            set
            {
                magnitude = value;
            }
        }

        public eActionType ActionType
        {
            get
            {
                return actionType;
            }
            set
            {
                actionType = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public List<eAPanel> TouchingPanels
        {
            get
            {
                return touchingPanels;
            }
            set
            {
                touchingPanels = value;
            }
        }

        public List<ePoint> Points
        {
            get
            {
                return points;
            }
        }

        public abstract double GetTotalLoadOn(eAPanel panel);   
    
    }
}
