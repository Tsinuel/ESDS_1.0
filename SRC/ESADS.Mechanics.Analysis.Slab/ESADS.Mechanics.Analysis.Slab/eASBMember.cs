using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Mechanics.Analysis;

namespace ESADS.Mechanics.Analysis.Slab
{
    public class eASBMember
    {
        private double width;
        private double depth;
        private double length;
        private double gk;
        private double qk;
        private eAPanel leftPanel;
        private eAPanel rightPanel;
        private eAPanel topPanel;
        private eAPanel bottomPanel;
        private ePoint start;
        private ePoint end;
        public eASBMember(ePoint start, ePoint end, eAPanel leftPanel, eAPanel rightPanel)
        {
            this.start = start;
            this.end = end;
            this.length = eMath.GetLength(start, end);
            FillPanels(leftPanel, rightPanel);
        }

        private void FillPanels(eAPanel leftPanel, eAPanel rightPanel)
        {
            if (start.X == end.X && start.Y < end.Y)
            {
                this.leftPanel = leftPanel;
                this.rightPanel = rightPanel;
            }

            if (start.X == end.X && start.Y > end.Y)
            {
                this.leftPanel = rightPanel;
                this.rightPanel = leftPanel;
            }

            if (start.Y == end.Y && start.X < end.Y)
            {
                this.topPanel = leftPanel;
                this.bottomPanel = rightPanel;
            }

            if (start.Y == end.Y && start.X > end.Y)
            {
                this.topPanel = rightPanel;
                this.bottomPanel = leftPanel;
            }

        }

        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        public double Depth
        {
            get
            {
                return depth;
            }
            set
            {
                depth = value;
            }
        }

        public double Length
        {
            get
            {
                return length;
            }
        }

        public double Gk
        {
            get
            {
                return gk;
            }

            internal set
            {
                gk = value;
            }
        }

        public double Qk
        {
            get
            {
                return qk;
            }
           internal set
            {
                qk = value;
            }
        }

        public ePoint Start
        {
            get
            {
                return start;
            }
        }

        public ePoint End
        {
            get
            {
                return end;
            }
        }

        public eAPanel LeftPanel
        {
            get
            {
                return leftPanel;
            }
            internal set
            {
                leftPanel = value;
            }
        }

        public eAPanel RightPanel
        {
            get
            {
                return rightPanel;
            }
            internal set
            {
                rightPanel = value;
            }
        }

        public eAPanel TopPanel
        {
            get
            {
                return topPanel;
            }
        }

        public eAPanel BottomPanel
        {
            get
            {
                return bottomPanel;
            }

        }
    }
}
