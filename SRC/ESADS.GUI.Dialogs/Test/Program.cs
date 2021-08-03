using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using ESADS.GUI;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms.Design;
using System.Windows.Forms.Layout;
using ESADS;


namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            eDocument doc = new eDocument(ESADS.Code.eStructureType.Column, eLengthUnits.m, eForceUints.KN);
            eNewColumnDialog nf = new eNewColumnDialog(doc);
            nf.ShowDialog();
            //eColumnReinforcementDistributionDialog d = new eColumnReinforcementDistributionDialog();
            //d.ShowDialog();
        }
    }
}
