using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Code;
using ESADS.Code.EBCS_1995;
using ESADS.Mechanics.Design;
using ESADS.Mechanics.Design.Footing;

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