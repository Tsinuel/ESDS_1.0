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
    public partial class ProjectInformationDialog : Form
    {
        
        #region Feilds
        private string[,] projectInfo;
        #endregion

        #region Constructors 

        public ProjectInformationDialog(string[,] projectInfo)
        {
            InitializeComponent();
            this.projectInfo = (string[,])projectInfo.Clone();
            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {
            foreach (DataGridViewRow row in dgvProjectInfo.Rows)
            {
                row.Height = 22;
            }
            
            dgvProjectInfo.RowCount = projectInfo.GetLength(0);

            for (int i = 0; i < projectInfo.GetLength(0); i++)
            {
                for (int j = 0; j < projectInfo.GetLength(1); j++)
                {
                    dgvProjectInfo[j, i].Value = projectInfo[i, j];
                }
            }

            FillRowHeadNumbers();// fils the the head text of each row with their row count order.

        }

        #endregion

        #region Event Handlers

        private void btnAddRow_Click(object sender, EventArgs e)
        { 
            //Incriments the number of rows.
            dgvProjectInfo.RowCount++;
            //Assigns the row header text of the last to to the number of rows.
            dgvProjectInfo.Rows[dgvProjectInfo.RowCount - 1].HeaderCell.Value = dgvProjectInfo.RowCount.ToString();
        }

        private void btnDeletRow_Click(object sender, EventArgs e)
        {
            int index = 0; //contains the index at which check for selection is made.
            while (index < dgvProjectInfo.RowCount)
            {
                //checks if the row is selected.
                if (dgvProjectInfo.Rows[index].Selected)
                {
                    //removes the selected row.
                    dgvProjectInfo.Rows.RemoveAt(index);
                }
                else
                {   
                    //increment if the row is not selected.
                    index++;
                }
            }

            FillRowHeadNumbers(); //Fills the header text with their prope order afte deletion of a row.
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //This loop clears  the selected cells of the data grid.
            for (int i = 0; i < dgvProjectInfo.RowCount; i++)
            {
                for (int j = 0; j < dgvProjectInfo.ColumnCount; j++)
                {
                    if (dgvProjectInfo[j, i].Selected)
                    {
                        dgvProjectInfo[j, i].Value = "";
                    }
                }
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            //This loop clears all the cells of the data grid.
            for (int i = 0; i < dgvProjectInfo.RowCount; i++)
            {
                for (int j = 0; j < dgvProjectInfo.ColumnCount; j++)
                {
                    dgvProjectInfo[j, i].Value = "";
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // closes the dialog.
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FillNullData();//Fills the datagridVeiw null elements.

            this.projectInfo = new string[dgvProjectInfo.RowCount, dgvProjectInfo.ColumnCount];

            for (int i = 0; i < dgvProjectInfo.RowCount; i++)
            {
                for (int j = 0; j < dgvProjectInfo.ColumnCount; j++)
                {
                    this.projectInfo[i, j] = dgvProjectInfo[j, i].Value.ToString();
                }
            }

            this.Close();
        }

        private void btnInsertRow_Click(object sender, EventArgs e)
        {
            //Inserts a row above the selected row in data grid.
            for (int i = 0; i < dgvProjectInfo.RowCount; i++)
            {
                if (dgvProjectInfo.Rows[i].Selected)
                {
                    dgvProjectInfo.Rows.Insert(i, new DataGridViewRow());
                    break; //adds only one row or terminates the loop.
                }
            }

            FillRowHeadNumbers();//Fills the header text with their prope order afte deletion of a row.
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //Reloads the built in items.
            InitializeCustomComponent();

            //This loop resets all the elements in the data row.
            for (int i = 0; i < dgvProjectInfo.Rows.Count; i++)
            {
                dgvProjectInfo[1, i].Value = "";
            }
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Fills the rows header text with their proper number order.
        /// </summary>
        private void FillRowHeadNumbers()
        {
            //Fills all the rows head text with their row number.
            for (int i = 0; i < dgvProjectInfo.RowCount; i++)
            {
                dgvProjectInfo.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        /// <summary>
        /// Fills all null cells with empty sting value.
        /// </summary>
        private void FillNullData()
        {
            //This loop fills all null datagriveview cells with empty string = "".
            for (int i = 0; i < dgvProjectInfo.RowCount; i++)
            {
                for (int j = 0; j < dgvProjectInfo.ColumnCount; j++)
                {
                    if (dgvProjectInfo[j,i].Value == null)
                    {
                        dgvProjectInfo[j, i].Value = "";
                    }
                }
            }
        }
        #endregion

        #region Properties

        /// <summary>
        /// Get the project information filled in the table.
        /// </summary>
        public string[,] ProjectInformation
        {
            get
            {
                return this.projectInfo;
            }
        }

        #endregion
    }
}
