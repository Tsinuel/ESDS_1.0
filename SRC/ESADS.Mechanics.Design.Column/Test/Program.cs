using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESADS.Code;
using ESADS.Mechanics.Design;
using ESADS.Mechanics.Design.Column;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            eBiaxial b = new eBiaxial(600, 600, new eConcrete(eConcreteGrade.C25), new eSteel(eSteelGrade.S300), 5680800, 407984000, 125970000,10,10);
            b.TypeOfDetail = eDetailType.Type4;
            b.MinDiam = 14;
            b.MaxDiam = 28;
            b.Design();
        }
    }
}
