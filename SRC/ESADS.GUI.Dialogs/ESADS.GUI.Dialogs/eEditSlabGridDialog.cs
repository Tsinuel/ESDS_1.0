using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESADS.EGraphics.Slab;
using ESADS.EGraphics;

namespace ESADS.GUI
{
    public partial class eEditSlabGridDialog : Form
    {
        private eDocument document;
        private eLengthUnits lengthUnit;
        private double least_X;
        private double least_Y;
        private bool pauseValidation;
        public eEditSlabGridDialog(eDocument document)
        {
            this.document = document;
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            if (this.document == null)
                return;
            if (this.document.Slab == null)
                return;

            this.lengthUnit = document.LengthUnit;
            cbxLength.Items.AddRange(Enum.GetNames(typeof(eLengthUnits)));
            cbxLength.Text = document.LengthUnit.ToString();

            document.Slab.SortGrids();

            clmnHBubbleLocation.Items.AddRange(Enum.GetNames(typeof(eBubbleLocation)));
            clmnVBubbleLocation.Items.AddRange(Enum.GetNames(typeof(eBubbleLocation)));

            for (int i = 0; i < document.Slab.V_Grids.Count; i++)
            {
                dgvVert_Grid.RowCount = i + 2;

                dgvVert_Grid[0, i].Value = document.Slab.V_Grids[i].Name;
                dgvVert_Grid[1, i].Value = eUtility.Convert(document.Slab.V_Grids[i].Coordinate, eUtility.SLU, document.LengthUnit);
                dgvVert_Grid[2, i].Value = document.Slab.V_Grids[i].Show;
                dgvVert_Grid[3, i].Value = document.Slab.V_Grids[i].BubbleLocation.ToString();
            }

            for (int i = 0; i < document.Slab.H_Grids.Count; i++)
            {
                dgvHor_Grid.RowCount = i + 2;

                dgvHor_Grid[0, i].Value = document.Slab.H_Grids[i].Name;
                dgvHor_Grid[1, i].Value = eUtility.Convert(document.Slab.H_Grids[i].Coordinate, eUtility.SLU, document.LengthUnit);
                dgvHor_Grid[2, i].Value = document.Slab.H_Grids[i].Show;
                dgvHor_Grid[3, i].Value = document.Slab.H_Grids[i].BubbleLocation.ToString();
            }

            ntxtBubbleSize.DoubleValue = document.Slab.BubbleSize;
        }

        private void btnQuickGrid_Click(object sender, EventArgs e)
        {
            bool ordinate = radOrdinate.Checked;
            radSpacing.Checked = true;

            eQuickSlabGridDialog qsg = new eQuickSlabGridDialog(this.lengthUnit, dgvVert_Grid.RowCount - 1, dgvHor_Grid.RowCount - 1);

            if (qsg.ShowDialog() == DialogResult.OK)
            {
                dgvHor_Grid.RowCount = qsg.NumOfH_Grids + 1;

                int count = 0;
                char name = 'A';

                dgvHor_Grid[0, 0].Value = name++;
                dgvHor_Grid[1, 0].Value = eUtility.Convert(qsg.Y_Spacing, eUtility.SLU, lengthUnit);
                dgvHor_Grid[2, 0].Value = true;
                dgvHor_Grid[3, 0].Value = eBubbleLocation.TopOrLeft.ToString();

                for (int i = 1; i < qsg.NumOfH_Grids; i++)
                {
                    if (count == 0)
                    {
                        if ((int)name <= (int)'Z')
                            dgvHor_Grid[0, i].Value = name++.ToString();
                        else
                        {
                            name = 'A';
                            dgvHor_Grid[0, i].Value = name++.ToString() + (++count).ToString();
                        }
                    }
                    else
                    {
                        if ((int)name > (int)'Z')
                        {
                            name = 'A';
                            dgvHor_Grid[0, i].Value = name++.ToString() + (++count).ToString();
                        }
                        else
                            dgvHor_Grid[0, i].Value = name++.ToString() + (count).ToString();
                    }
                    dgvHor_Grid[1, i].Value = eUtility.Convert(qsg.Y_Spacing, eUtility.SLU, lengthUnit);
                    dgvHor_Grid[2, i].Value = true;
                    dgvHor_Grid[3, i].Value = eBubbleLocation.TopOrLeft.ToString();
                }

                dgvVert_Grid.RowCount = qsg.NumOfV_Grids + 1;

                int nam = 1;
                dgvVert_Grid[0, 0].Value = nam++;
                dgvVert_Grid[1, 0].Value = eUtility.Convert(qsg.X_Spacing, eUtility.SLU, lengthUnit);
                dgvVert_Grid[2, 0].Value = true;
                dgvVert_Grid[3, 0].Value = eBubbleLocation.TopOrLeft.ToString();

                for (int j = 1; j < qsg.NumOfV_Grids; j++)
                {
                    dgvVert_Grid[0, j].Value = nam++;
                    dgvVert_Grid[1, j].Value = eUtility.Convert(qsg.X_Spacing, eUtility.SLU, lengthUnit);
                    dgvVert_Grid[2, j].Value = true;
                    dgvVert_Grid[3, j].Value = eBubbleLocation.TopOrLeft.ToString();
                }
            }

            radOrdinate.Checked = ordinate;
        }

        private void dgvGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridView dgrv = sender as DataGridView;

            //if (dgrv == null)
            //    return;
            //if (e.RowIndex < 1)
            //    return;
            //if (this.pauseValidation)
            //    return;

            //if (e.ColumnIndex == 0) //Id changed
            //{
                
            //}
            //if (e.ColumnIndex == 1) //Coordinate has been changed
            //{
            //    double val ;
            //    if (dgrv[e.ColumnIndex, e.RowIndex].Value.ToString() == "")
            //    {
            //        MessageBox.Show("No grid can have an empty entry for coordinate or spacing!", "Data invalid!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    else if (double.TryParse(dgrv[e.ColumnIndex, e.RowIndex].Value.ToString(), out val))
            //    {
            //        if (radOrdinate.Checked)
            //        {
            //            for (int i = 0; i < dgrv.RowCount - 1; i++)
            //            {
            //                if (i != e.RowIndex && double.Parse(dgrv[1, i].Value.ToString()) == val)
            //                {
            //                    MessageBox.Show("Every grid must have a unique coordinate value. Overlapping grids are not allowed.", "Duplicate Grid Coordinate!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                    foreach (DataGridViewCell cell in dgrv.SelectedCells)
            //                        cell.Selected = false;
            //                    dgrv[e.ColumnIndex, e.RowIndex].Selected = true;
            //                    dgrv[e.ColumnIndex, i].Selected = true;
            //                    break;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            if (e.RowIndex < dgrv.RowCount - 2 && val <= 0.0)
            //            {
            //                MessageBox.Show("Spacing value cannot be zero or negative.", "Data invalid!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                foreach (DataGridViewCell cell in dgrv.SelectedCells)
            //                    cell.Selected = false;
            //                dgrv[e.ColumnIndex, e.RowIndex].Selected = true;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Coordinate or spacing value should only be given in correct number format", "Data invalid!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        foreach (DataGridViewCell cell in dgrv.SelectedCells)
            //            cell.Selected = false;
            //        dgrv[e.ColumnIndex, e.RowIndex].Selected = true;
            //    }
            //}
            //else if (e.ColumnIndex == 2) 
            //{

            //}
        }

        private void radSpacing_CheckedChanged(object sender, EventArgs e)
        {
            btnReorderCoordinates.Enabled = radOrdinate.Checked;
            this.pauseValidation = true;

            if (radOrdinate.Checked)
            {
                double prevS, coord;
                clmnXCoordinateOrSpacing.HeaderText = "Coordinate";
                clmnYCoordinateOrSpacing.HeaderText = "Coordinate";

                prevS = double.Parse(dgvHor_Grid[1, 0].Value.ToString());
                dgvHor_Grid[1, 0].Value = least_Y;

                for (int i = 1; i < dgvHor_Grid.RowCount - 1; i++)
                {
                    coord = prevS + double.Parse(dgvHor_Grid[1, i - 1].Value.ToString());
                    prevS = double.Parse(dgvHor_Grid[1, i].Value.ToString());
                    dgvHor_Grid[1, i].Value = coord;
                }

                dgvHor_Grid[1, dgvHor_Grid.RowCount - 2].ReadOnly = false;

                prevS = double.Parse(dgvVert_Grid[1, 0].Value.ToString());
                dgvVert_Grid[1, 0].Value = least_X;

                for (int i = 1; i < dgvVert_Grid.RowCount - 1; i++)
                {
                    coord = prevS + double.Parse(dgvVert_Grid[1, i - 1].Value.ToString());
                    prevS = double.Parse(dgvVert_Grid[1, i].Value.ToString());
                    dgvVert_Grid[1, i].Value = coord;
                }

                dgvVert_Grid[1, dgvVert_Grid.RowCount - 2].ReadOnly = false;
            }
            else
            {
                clmnXCoordinateOrSpacing.HeaderText = "Spacing";
                clmnYCoordinateOrSpacing.HeaderText = "Spacing";

                btnReorderCoordinates_Click(null, null);

                least_X = double.Parse(dgvVert_Grid[1, 0].Value.ToString());

                for (int i = 0; i < dgvVert_Grid.RowCount - 2; i++)
                {
                    dgvVert_Grid[1, i].Value = double.Parse(dgvVert_Grid[1, i + 1].Value.ToString()) - double.Parse(dgvVert_Grid[1, i].Value.ToString());
                }

                dgvVert_Grid[1, dgvVert_Grid.RowCount - 2].ReadOnly = true;
                dgvVert_Grid[1, dgvVert_Grid.RowCount - 2].Value = 0.0;

                least_Y = double.Parse(dgvHor_Grid[1, 0].Value.ToString());

                for (int i = 0; i < dgvHor_Grid.RowCount - 2; i++)
                {
                    dgvHor_Grid[1, i].Value = double.Parse(dgvHor_Grid[1, i + 1].Value.ToString()) - double.Parse(dgvHor_Grid[1, i].Value.ToString());
                }

                dgvHor_Grid[1, dgvHor_Grid.RowCount - 2].ReadOnly = true;
                dgvHor_Grid[1, dgvHor_Grid.RowCount - 2].Value = 0.0;

            }

            this.pauseValidation = false;
        }

        private void btnReorderCoordinates_Click(object sender, EventArgs e)
        {
            object temp;
            this.pauseValidation = true;

            for (int i = 0; i < dgvHor_Grid.RowCount - 1; i++)
            {
                for (int j = i; j < dgvHor_Grid.RowCount - 1; j++)
                {
                    if (double.Parse(dgvHor_Grid[1, i].Value.ToString()) > double.Parse(dgvHor_Grid[1, j].Value.ToString()))
                    {
                        temp = dgvHor_Grid[0, i].Value;
                        dgvHor_Grid[0, i].Value = dgvHor_Grid[0, j].Value;
                        dgvHor_Grid[0, j].Value = temp;

                        temp = dgvHor_Grid[1, i].Value;
                        dgvHor_Grid[1, i].Value = dgvHor_Grid[1, j].Value;
                        dgvHor_Grid[1, j].Value = temp;

                        temp = dgvHor_Grid[2, i].Value;
                        dgvHor_Grid[2, i].Value = dgvHor_Grid[2, j].Value;
                        dgvHor_Grid[2, j].Value = temp;

                        temp = dgvHor_Grid[3, i].Value;
                        dgvHor_Grid[3, i].Value = dgvHor_Grid[3, j].Value;
                        dgvHor_Grid[3, j].Value = temp;
                    }
                }
            }

            for (int i = 0; i <  dgvVert_Grid.RowCount - 1; i++)
            {
                for (int j = i; j < dgvVert_Grid.RowCount - 1; j++)
                {
                    if (double.Parse(dgvVert_Grid[1, i].Value.ToString()) > double.Parse(dgvVert_Grid[1, j].Value.ToString()))
                    {
                        temp = dgvVert_Grid[0, i].Value;
                        dgvVert_Grid[0, i].Value = dgvVert_Grid[0, j].Value;
                        dgvVert_Grid[0, j].Value = temp;

                        temp = dgvVert_Grid[1, i].Value;
                        dgvVert_Grid[1, i].Value = dgvVert_Grid[1, j].Value;
                        dgvVert_Grid[1, j].Value = temp;

                        temp = dgvVert_Grid[2, i].Value;
                        dgvVert_Grid[2, i].Value = dgvVert_Grid[2, j].Value;
                        dgvVert_Grid[2, j].Value = temp;

                        temp = dgvVert_Grid[3, i].Value;
                        dgvVert_Grid[3, i].Value = dgvVert_Grid[3, j].Value;
                        dgvVert_Grid[3, j].Value = temp;
                    }
                }
            }

            this.pauseValidation = false;
        }

        private void dgvGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null)
                return;

            if (radSpacing.Checked)
            {
                dgv[1, dgv.RowCount - 3].ReadOnly = false;
                dgv[1, dgv.RowCount - 2].ReadOnly = true;
                dgv[1, dgv.RowCount - 2].Value = 0;
            }
        }

        private void dgvGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null)
                return;
            if (dgv.RowCount < 3)
                dgv.RowCount++;

            if (radSpacing.Checked)
            {
                dgv[1, dgv.RowCount - 3].ReadOnly = false;
                dgv[1, dgv.RowCount - 2].ReadOnly = true;
            }
        }

        private void chkShowAllGrids_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowAllGrids.CheckState == CheckState.Checked)
            {
                for (int i = 0; i < dgvVert_Grid.RowCount - 1; i++)
                    dgvVert_Grid[2, i].Value = true;

                for (int i = 0; i < dgvHor_Grid.RowCount - 1; i++)
                    dgvHor_Grid[2, i].Value = true;
            }
            else if (chkShowAllGrids.CheckState == CheckState.Unchecked)
            {
                for (int i = 0; i < dgvVert_Grid.RowCount - 1; i++)
                    dgvVert_Grid[2, i].Value = false;

                for (int i = 0; i < dgvHor_Grid.RowCount - 1; i++)
                    dgvHor_Grid[2, i].Value = false;
            }
        }

        private void cbxLength_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pauseValidation = true;

            eLengthUnits lu_new = (eLengthUnits)Enum.Parse(typeof(eLengthUnits), cbxLength.Text);

            ntxtBubbleSize.LengthUnit = lu_new;

            for (int i = 0; i < dgvHor_Grid.RowCount - 1; i++)
            {
                dgvHor_Grid[1, i].Value = eUtility.Convert((double)(dgvHor_Grid[1, i].Value), lengthUnit, lu_new);
            }

            for (int i = 0; i < dgvVert_Grid.RowCount - 1; i++)
            {
                dgvVert_Grid[1, i].Value = eUtility.Convert((double)(dgvVert_Grid[1, i].Value), lengthUnit, lu_new);
            }

            this.lengthUnit = lu_new;

            this.pauseValidation = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!Valid())
                return;

            radOrdinate.Checked = true;

            document.Slab.ResetSystem();

            document.Slab.BubbleSize = ntxtBubbleSize.DoubleValue;

            double coord;
            eBubbleLocation bbll;

            for (int i = 0; i < dgvHor_Grid.RowCount - 1; i++)
            {
                coord = eUtility.Convert(double.Parse(dgvHor_Grid[1, i].Value.ToString()), lengthUnit, eUtility.SLU);
                bbll = (eBubbleLocation)Enum.Parse(typeof(eBubbleLocation), dgvHor_Grid[3, i].Value.ToString());

                document.Slab.H_Grids.Add(new eSGrid(document.Slab, coord, eGridType.Horizontal));
                
                document.Slab.H_Grids[document.Slab.H_Grids.Count - 1].Name = dgvHor_Grid[0, i].Value.ToString();
                document.Slab.H_Grids[document.Slab.H_Grids.Count - 1].BubbleLocation = bbll;
                document.Slab.H_Grids[document.Slab.H_Grids.Count - 1].Show = (bool)(dgvHor_Grid[2, i].Value);
            }

            for (int i = 0; i < dgvVert_Grid.RowCount - 1; i++)
            {
                coord = eUtility.Convert(double.Parse(dgvVert_Grid[1, i].Value.ToString()), lengthUnit, eUtility.SLU);
                bbll = (eBubbleLocation)Enum.Parse(typeof(eBubbleLocation), dgvVert_Grid[3, i].Value.ToString());

                document.Slab.V_Grids.Add(new eSGrid(document.Slab, coord, eGridType.Horizontal));

                document.Slab.V_Grids[document.Slab.V_Grids.Count - 1].Name = dgvVert_Grid[0, i].Value.ToString();
                document.Slab.V_Grids[document.Slab.V_Grids.Count - 1].BubbleLocation = bbll;
                document.Slab.V_Grids[document.Slab.V_Grids.Count - 1].Show = (bool)(dgvVert_Grid[2, i].Value);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool Valid()
        {
            bool valid = true;

            foreach (DataGridViewCell cell in dgvHor_Grid.SelectedCells)
                cell.Selected = false;
            foreach (DataGridViewCell cell in dgvVert_Grid.SelectedCells)
                cell.Selected = false;

            for (int i = 0; i < dgvHor_Grid.RowCount - 1; i++)
            {
                try
                {
                    double.Parse(dgvHor_Grid[1, i].Value.ToString());
                }
                catch
                {
                    valid = false;
                    dgvHor_Grid[1, i].Selected = true;
                }
                try
                {
                    Enum.Parse(typeof(eBubbleLocation), dgvHor_Grid[3, i].Value.ToString());
                }
                catch
                {
                    valid = false;
                    dgvHor_Grid[3, i].Selected = true;
                }
            }

            for (int i = 0; i < dgvVert_Grid.RowCount - 1; i++)
            {
                try
                {
                    double.Parse(dgvVert_Grid[1, i].Value.ToString());
                }
                catch
                {
                    valid = false;
                    dgvVert_Grid[1, i].Selected = true;
                }
                try
                {
                    Enum.Parse(typeof(eBubbleLocation), dgvVert_Grid[3, i].Value.ToString());
                }
                catch
                {
                    valid = false;
                    dgvVert_Grid[3, i].Selected = true;
                }
            }

            if (radOrdinate.Checked && valid)
            {
                for (int i = 0; i < dgvHor_Grid.RowCount - 1; i++)
                {
                    for (int j = i + 1; j < dgvHor_Grid.RowCount - 1; j++)
                    {
                        if (double.Parse(dgvHor_Grid[1, i].Value.ToString()) == double.Parse(dgvHor_Grid[1, j].Value.ToString()))
                        {
                            valid = false;
                            dgvHor_Grid[1, i].Selected = true;
                            dgvHor_Grid[1, j].Selected = true;
                        }
                    }
                }

                for (int i = 0; i < dgvVert_Grid.RowCount - 1; i++)
                {
                    for (int j = i + 1; j < dgvVert_Grid.RowCount - 1; j++)
                    {
                        if (double.Parse(dgvVert_Grid[1, i].Value.ToString()) == double.Parse(dgvVert_Grid[1, j].Value.ToString()))
                        {
                            valid = false;
                            dgvVert_Grid[1, i].Selected = true;
                            dgvVert_Grid[1, j].Selected = true;
                        }
                    }
                }
            }
            else if(valid)
            {
                for (int i = 0; i < dgvHor_Grid.RowCount - 2; i++)
                {
                    if (double.Parse(dgvHor_Grid[1, i].Value.ToString()) <= 0.0)
                    {
                        valid = false;
                        dgvHor_Grid[1, i].Selected = true;
                    }
                }

                for (int i = 0; i < dgvVert_Grid.RowCount - 2; i++)
                {
                    if (double.Parse(dgvVert_Grid[1, i].Value.ToString()) <= 0.0)
                    {
                        valid = false;
                        dgvVert_Grid[1, i].Selected = true;
                    }
                }
            }

            if (!valid)
            {
                MessageBox.Show("Some of the data input is invalid!\n\n   Each value of coordinate or spacing should be in correct number format.\n   Coordiantes should not be repeated.\n   "+
                    "Spacings cannot be zero or negative.\n   Bubble location should have certain value.\n\nCheck the selected cells and retry.",
                    "Date invalid!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (ntxtBubbleSize.SU <= 0)
            {
                valid = false;
                MessageBox.Show("Bubble size cannot be zero or negative", "Data invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return valid;
        }
    }
}
