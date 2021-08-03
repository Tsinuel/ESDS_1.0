using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESADS;

namespace ESADS.GUI.Controls
{

    public partial class eNumericTextBox : TextBox
    {
        private bool automaticResize = false;
        private eMeasurment measurment;
        private eLengthUnits lengthUnit;
        private eForceUints forceUnit;

        /// <summary>
        /// Gets or sets the type of measurment used in the txt box.
        /// </summary>
        public eMeasurment Measurment
        {
            get { return measurment; }
            set { measurment = value; }
        }

        public eLengthUnits LengthUnit
        {
            get { return lengthUnit; }
            set 
            {
                ChangeValue(lengthUnit, value, forceUnit, forceUnit);
                lengthUnit = value;
            }
        }

        public eForceUints ForceUnit
        {
            get { return forceUnit; }
            set
            {
                ChangeValue(lengthUnit, lengthUnit, forceUnit, value);
                forceUnit = value;
            }
        }

        public bool AutomaticResize
        {
            get
            {
                return automaticResize;
            }
            set
            {
                automaticResize = value;
            }
        }

        public eNumericTextBox()
        {
            measurment = eMeasurment.None;
            InitializeComponent();
        }

        public event eUnitChangedEventHandler UnitChanged;

        private void OnUnitChanged(eLengthUnits lengthUnit, eForceUints forceUnit)
        {
            if (UnitChanged != null)
                UnitChanged(this, new eUnitChangedEventArgs(forceUnit, lengthUnit));
        }
        private eDataType dataType;

        public eDataType DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        public int IntValue
        {
            get
            {
                double d;
                if (!double.TryParse(this.Text, out d))
                    return 0;
                else
                    return (int)double.Parse(this.Text);
                //if (dataType == eDataType.Decimal)
                //    throw new FormatException("This textbox doesn't have integer value property. Use DoubleValue property instead of the IntValue property.");
                //else
                //    return int.Parse(this.Text);
            }
            set
            {
                this.Text = value.ToString();
            }
        }

        public double DoubleValue
        {
            get
            {
                double d;
                if (!double.TryParse(this.Text, out d))
                    return 0.0;
                else
                    return double.Parse(this.Text);
                //if (dataType == eDataType.Decimal)
                //    return double.Parse(this.Text);
                //else
                //    throw new FormatException("This textbox doesn't have double value property. Use IntValue property instead of the DoubleValue property.");
            }
            set
            {
                this.Text = value.ToString();
            }
        }

        private void NumericTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.NumPad0 && e.KeyCode != Keys.NumPad1 && e.KeyCode != Keys.NumPad2 && e.KeyCode != Keys.NumPad3 && e.KeyCode != Keys.NumPad4 &&
                e.KeyCode != Keys.NumPad5 && e.KeyCode != Keys.NumPad6 && e.KeyCode != Keys.NumPad7 && e.KeyCode != Keys.NumPad8 && e.KeyCode != Keys.NumPad9 &&
                e.KeyCode != Keys.D0 && e.KeyCode != Keys.D1 && e.KeyCode != Keys.D2 && e.KeyCode != Keys.D3 && e.KeyCode != Keys.D4 && e.KeyCode != Keys.D5 &&
                e.KeyCode != Keys.D6 && e.KeyCode != Keys.D7 && e.KeyCode != Keys.D8 && e.KeyCode != Keys.D9 && e.KeyCode != Keys.Back && e.KeyCode != Keys.Delete &&
                e.KeyCode != Keys.Enter && e.KeyCode != Keys.Tab && e.KeyCode != Keys.NumLock && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.OemMinus)
            {
                if (e.KeyCode == Keys.OemPeriod || e.KeyCode == Keys.Decimal)
                {
                    if (this.Text.Contains('.') || dataType == eDataType.Integer)
                        e.SuppressKeyPress = true;
                }
                else
                    e.SuppressKeyPress = true;
            }
        }

        private void eNumericTextBox_Leave(object sender, EventArgs e)
        {
            if (this.Text == "")
                this.Text = "0";
        }

        private void eNumericTextBox_TextChanged(object sender, System.EventArgs e)
        {
            if (automaticResize && this.Text.Length > 0)
            {
                Graphics g = this.CreateGraphics();
                SizeF s = g.MeasureString(this.Text, this.Font);
                this.Size = new Size((int)(s.Width * 1.4), (int)s.Height);
            }
            if (this.Text != null && this.Text.Contains('-') && this.Text[0] != '-')
            {
                int i = this.Text.IndexOf('-');
                this.Text = this.Text.Remove(i);
            }            
        }

        public static implicit operator double(eNumericTextBox txt)
        {
            return txt.DoubleValue;
        }

        public static implicit operator int (eNumericTextBox txt)
        {
            return txt.IntValue;
        }

        
        private void ChangeValue(eLengthUnits InputLengthUnit, eLengthUnits OutputLengthUnit, eForceUints InputForceUnit, eForceUints OutputForceUnit)
        {
            switch (measurment)
            {
                case eMeasurment.Length:
                    DoubleValue = eUtility.Convert(DoubleValue, InputLengthUnit, OutputLengthUnit);
                    break;
                case eMeasurment.Force:
                    DoubleValue = eUtility.Convert(DoubleValue, InputForceUnit, OutputForceUnit);
                    break;
                case eMeasurment.Moment:
                    DoubleValue = eUtility.Convert(DoubleValue, InputLengthUnit, OutputLengthUnit, InputForceUnit, OutputForceUnit);
                    break;
                case eMeasurment.Area:
                    DoubleValue = eUtility.ConvertArea(DoubleValue, InputLengthUnit, OutputLengthUnit);
                    break;
                case eMeasurment.Stress:
                    DoubleValue = eUtility.Convert(DoubleValue, InputLengthUnit, InputForceUnit, OutputLengthUnit, OutputForceUnit);
                    break;
                case eMeasurment.LineLoad:
                    DoubleValue = eUtility.Convert(DoubleValue, InputForceUnit, OutputForceUnit) / eUtility.Convert(1, InputLengthUnit, OutputLengthUnit);
                    break;
                case eMeasurment.UnitWeight:
                    DoubleValue = eUtility.Convert(DoubleValue, InputForceUnit, OutputForceUnit) / Math.Pow(eUtility.Convert(1.0, InputLengthUnit, OutputLengthUnit), 3);
                    break;
            }
        }

        /// <summary>
        /// Gets or sets values in system units.
        /// </summary>
        public double SU
        {
            get
            {
                switch (measurment)
                {
                    case eMeasurment.Length:
                        return eUtility.Convert(DoubleValue, lengthUnit, eUtility.SLU);
                    case eMeasurment.Force:
                        return eUtility.Convert(DoubleValue, forceUnit, eUtility.SFU);
                    case eMeasurment.Moment:
                        return eUtility.Convert(DoubleValue, lengthUnit, eUtility.SLU, forceUnit, eUtility.SFU);
                    case eMeasurment.Area:
                        return eUtility.ConvertArea(DoubleValue, lengthUnit, eUtility.SLU);
                    case eMeasurment.Stress:
                        return eUtility.Convert(DoubleValue, lengthUnit, forceUnit, eUtility.SLU, eUtility.SFU);
                    case eMeasurment.LineLoad:
                        return eUtility.Convert(DoubleValue, forceUnit, eUtility.SFU) / eUtility.Convert(1, lengthUnit, eUtility.SLU);
                    case eMeasurment.UnitWeight:
                        return eUtility.Convert(DoubleValue, forceUnit, eUtility.SFU) / Math.Pow(eUtility.Convert(1.0, lengthUnit, eUtility.SLU), 3);
                }
                return DoubleValue;
            }
            set
            {
                switch (measurment)
                {
                    case eMeasurment.Length:
                        DoubleValue = eUtility.Convert(value, eUtility.SLU, lengthUnit);
                        break;
                    case eMeasurment.Force:
                        DoubleValue = eUtility.Convert(value, eUtility.SFU, forceUnit);
                        break;
                    case eMeasurment.Moment:
                        DoubleValue = eUtility.Convert(value, eUtility.SLU, lengthUnit, eUtility.SFU, forceUnit);
                        break;
                    case eMeasurment.Area:
                        DoubleValue = eUtility.ConvertArea(value, eUtility.SLU, lengthUnit);
                        break;
                    case eMeasurment.Stress:
                        DoubleValue = eUtility.Convert(value, eUtility.SLU, eUtility.SFU, lengthUnit, forceUnit);
                        break;
                    case eMeasurment.LineLoad:
                        DoubleValue = eUtility.Convert(value, eUtility.SFU, forceUnit) / eUtility.Convert(1, eUtility.SLU, lengthUnit);
                        break;
                    case eMeasurment.UnitWeight:
                        DoubleValue = eUtility.Convert(value, eUtility.SFU, forceUnit) / Math.Pow(eUtility.Convert(1.0, eUtility.SLU, lengthUnit), 3);
                        break;
                    default:
                        DoubleValue = value;
                        break;
                }
            }
        }
    }
}
