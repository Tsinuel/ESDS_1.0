using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestWorkshop
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> names = new List<string>();

            names.Add("Pniel");
            names.Add("holla");
            names.Add("Abush");
            names.Add("Tsinu");
            names.Add("Ayt");

            //Comparison<string> comp = compare();

            for (int i = 0; i < names.Count - 1; i++)
            {
                for (int j = i + 1; j < names.Count; j++)
                {
                    if (string.Compare(names[i], names[j], StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        string temp = names[i];
                        names[i] = names[j];
                        names[j] = temp;
                    }
                }
            }
        }

        
    }
}
