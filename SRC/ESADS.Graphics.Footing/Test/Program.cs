using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS;
using ESADS.EGraphics;
using ESADS.Code;
using ESADS.Mechanics.Design.Footing;
using System.Windows.Forms;
using ESADS.Mechanics.Design;
using System.Windows.Forms;
using ESADS.GUI;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            eDFooting f = new eDFooting(3000, 3000, new eSteel(eSteelGrade.S400), new eConcrete(eConcreteGrade.C30), 2000000, 100000000, 100000000);
            f.SetDetailingData(16, 28, 100, 300, 50);
            f.SetRectangularFootingColumnData(300, 300);
            f.Design();
        }
    }
}
