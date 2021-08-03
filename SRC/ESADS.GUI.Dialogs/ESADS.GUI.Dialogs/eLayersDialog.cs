using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESADS.EGraphics;

namespace ESADS.GUI
{
    public partial class eLayersDialog : Form
    {
        /// <summary>
        /// Holds the value of the layers after the changes have been validated.
        /// </summary>
        private eLayers layers;
        /// <summary>
        /// Temporary storage for fonts
        /// </summary>
        private List<Font> fonts;
        /// <summary>
        /// Gets the layers after the acceptance of the last changes 
        /// </summary>
        public eLayers Layers
        {
            get { return this.layers; }
        }

        public eLayersDialog(eLayers layers)
        {
            this.layers = layers;
            InitializeComponent();
            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {
            dgrvLayers.Rows.Add(layers.Count);
            fonts = new List<System.Drawing.Font>();

            dgrvclmnLineType.Items.AddRange(Enum.GetNames(typeof(eLineTypes)));

            for (int i = 0; i < layers.Count; i++)
            {
                dgrvLayers[0, i].Value = layers[i].Name;

                dgrvLayers[1, i].Value = ((Color)(layers[i].Color)).Name;
                dgrvLayers[1, i].Style.BackColor = layers[i].Color;

                dgrvLayers[2, i].Value = layers[i].LineType.Type.ToString();

                dgrvLayers[3, i].Value = layers[i].LineWeight.LineWeight;

                dgrvLayers[4, i].Value = ((Font)layers[i].TextStyle).Name;
                dgrvLayers[4, i].Style.Font = layers[i].TextStyle;
                fonts.Add((Font)layers[i].TextStyle);

                dgrvLayers[5, i].Value = layers[i].LayerOn;
            }

            btnApply.Enabled = false;
        }

        private void dgrvLayers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                btnApply.Enabled = true;
                ColorDialog clDlg = new ColorDialog();
                clDlg.Color = dgrvLayers[e.ColumnIndex, e.RowIndex].Style.BackColor;

                if (clDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    dgrvLayers[e.ColumnIndex, e.RowIndex].Style.BackColor = clDlg.Color;
                    dgrvLayers[e.ColumnIndex, e.RowIndex].Value = clDlg.Color.Name;
                    dgrvLayers[e.ColumnIndex, e.RowIndex].Selected = false;
                }
            }
            if (e.ColumnIndex == 4)
            {
                btnApply.Enabled = true;
                FontDialog fontDlg = new FontDialog();
                fontDlg.Font = fonts[e.RowIndex];
                try
                {
                    if (fontDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fonts[e.RowIndex] = fontDlg.Font;
                        dgrvLayers[e.ColumnIndex, e.RowIndex].Value = fontDlg.Font.Name;
                        dgrvLayers[e.ColumnIndex, e.RowIndex].Style.Font = fontDlg.Font;
                        dgrvLayers[e.ColumnIndex, e.RowIndex].Selected = false;
                    }
                }
                catch { }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (CheckDataValidity())
            {
                for (int i = 0; i < layers.Count; i++)
                {
                    layers[i].Color = new eColor(dgrvLayers[1, i].Style.BackColor);
                    layers[i].LineType = new eLineType((eLineTypes)Enum.Parse(typeof(eLineTypes), dgrvLayers[2, i].Value.ToString()));
                    layers[i].LineWeight = new eLineWeight(float.Parse(dgrvLayers[3, i].Value.ToString()));
                    layers[i].TextStyle = new eTextStyle(this.fonts[i], eChangeBy.ByLayer);
                    layers[i].LayerOn = (bool)(dgrvLayers[5, i].Value);
                }
                btnApply.Enabled = false;
            }
        }

        private bool CheckDataValidity()
        {
            for (int i = 0; i < layers.Count; i++)
            {
                float res;
                if (!float.TryParse(dgrvLayers[3, i].Value.ToString(), out res))
                {
                    MessageBox.Show("Line weight value is not in the correct format.", "Line weight invalid!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgrvLayers[3, i].Selected = true;
                    return false;
                }
            }
            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckDataValidity())
            {
                btnApply_Click(sender, e);
                this.Close();
            }
        }

        private void dgrvLayers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            btnApply.Enabled = true;
        }
    }
}
