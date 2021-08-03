using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ESADS.GUI
{
    public partial class eAssignSlabLoad : Form
    {
        public eAssignSlabLoad()
        {
            InitializeComponent();
            cbxActionType.SelectedIndex = 0;
        }

        private void trvCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Text)
            {
                case "Category A":
                    lblDescription.Text = "Areas of demostic and residential use.\n\n" +
                                          "Example: Room in residential buildings \nand houses; " +
                                          "rooms and wards in hospials; \nkitchen and toilet";
                    ntxtMagnitude.DoubleValue = 2.0;
                    break;
                case "General":
                    ntxtMagnitude.DoubleValue = 2.0;
                    break;
                case "Stair":
                    ntxtMagnitude.DoubleValue = 3.0;
                    break;
                case "Balconies":
                    ntxtMagnitude.DoubleValue = 4.0;
                    break;
                case "Category B":
                    lblDescription.Text = "";
                    ntxtMagnitude.DoubleValue = 3.0;
                    break;
                case "Category C":
                    lblDescription.Text = "Areas where people may congregate (with\n" +
                                          "the exception of areas defined under\n" +
                                          "category A,B,D and E)";
                    ntxtMagnitude.DoubleValue = 3.0;
                    break;
                case "C1":
                    lblDescription.Text = "Areas with tables, etc.e.g. areas in\n"+
                                          "schools, cafes, restaurants, dininghall\n"+
                                          "s, reading rooms, receptions etc.";
                    ntxtMagnitude.DoubleValue = 3.0;
                    break;
                case "C2":
                    lblDescription.Text = "Areas with fIXed seats, e.g. areas in\n"+
                                          "churches, theatres or cinemas,\n"+ 
                                          "conference rooms,lecture halls, assembly\n"+                                            
                                          "halls, waiting rooms, etc.";
                    ntxtMagnitude.DoubleValue = 4.0;
                    break;
                case "C3":
                    lblDescription.Text = "Areas with fIXed seats, e.g. areas in\n" +
                                          "churches, theatres or cinemas,\n" +
                                          "conference rooms,lecture halls,\n" +
                                          "assembly halls, waiting rooms, etc.";
                    ntxtMagnitude.DoubleValue = 5.0;
                    break;
                case "C4":
                    lblDescription.Text = "Areas susceptible to overcrowding,\n"+
                                          "e.g. dance halls, gymnastic rooms,\n"+
                                          "stages, etc.";
                    ntxtMagnitude.DoubleValue = 5.0;
                    break;
                case "C5":
                    lblDescription.Text = "Areas susceptible to overcrowding,\n"+
                                          "e.g. in buildings for public-events like\n"+
                                          "concert halls, sports halls including\n"+
                                          "stands, terraces and access area, etc.";
                    ntxtMagnitude.DoubleValue = 5.0;
                    break;
                case "Category D":
                    lblDescription.Text = "Shopping areas";
                    ntxtMagnitude.DoubleValue = 5.0;
                    break;
                case "D1":
                    lblDescription.Text = "Areas in general retail shops, e.g.\n"+
                                          "areas in, warehouses, stationery and \n"+
                                          "office stores, etc.";
                    ntxtMagnitude.DoubleValue = 5.0;
                    break;
                case "D2":
                    lblDescription.Text = "";
                    ntxtMagnitude.DoubleValue = 5.0;
                    break;
                case "Category E":
                    lblDescription.Text = "Areas susceptible to accumulation of\ngoods, including access areas ";
                    ntxtMagnitude.DoubleValue = 6.0;
                    break;
            }
                     
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
          
        }

        private void cbxLoadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxActionType.SelectedIndex == 1)
                gbxCategory.Enabled = false;
            else
                gbxCategory.Enabled = true;
        }

        private void cbxLengthUnit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxForceUnit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
