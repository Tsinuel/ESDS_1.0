using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESADS.Mechanics.Design;
namespace ESADS
{
    /// <summary>
    /// Contains static methods used to convert measurments from one unit system to the other and other utility methods.
    /// </summary>
    public static class eUtility
    {
        /// <summary>
        /// The force unit used in the calculations in the background. All other units are converted to this for use in low level libraries(eg. ESADS_Mechanics) 
        /// </summary>
        public const eForceUints SFU = eForceUints.N;

        /// <summary>
        /// The length unit used in the calculations in the background. All other units are converted to this for use in low level libraries(eg. ESADS_Mechanics) 
        /// </summary>
        public const eLengthUnits SLU = eLengthUnits.mm;

        /// <summary>
        /// Converts length from one unit system to the other.
        /// </summary>
        /// <param fileName="Length">Length that is goint to be converted from one unit system to the other.</param>
        /// <param fileName="InputUnit">The current unit system of the measurment.</param>
        /// <param fileName="OutputUnit">The unit system to which the measurment is going to be converted.</param>
        /// <returns></returns>
        public static double Convert(double Length, eLengthUnits InputUnit, eLengthUnits OutputUnit)
        {
            //switch between different possible conversions.
            switch (InputUnit)
            {
                case eLengthUnits.m:
                    switch (OutputUnit)
                    {
                        case eLengthUnits.cm:
                            Length *= 100;
                            break;
                        case eLengthUnits.mm:
                            Length *= 1000;
                            break;
                        case eLengthUnits.ft:
                            Length *= 3.2808398950131233595800524934383;
                            break;
                        case eLengthUnits.In:
                            Length *= 39.37007874015748031496062992126;
                            break;
                    }
                    break;
                case eLengthUnits.cm:
                    switch (OutputUnit)
                    {
                        case eLengthUnits.m:
                            Length *= 0.01;
                            break;
                        case eLengthUnits.mm:
                            Length *= 10;
                            break;
                        case eLengthUnits.ft:
                            Length *= 0.032808398950131233595800524934383;
                            break;
                        case eLengthUnits.In:
                            Length *= 0.3937007874015748031496062992126;
                            break;
                    }
                    break;
                case eLengthUnits.mm:
                    switch (OutputUnit)
                    {
                        case eLengthUnits.m:
                            Length *= 0.001;
                            break;
                        case eLengthUnits.cm:
                            Length *= 0.1;
                            break;
                        case eLengthUnits.ft:
                            Length *= 0.0032808398950131233595800524934383;
                            break;
                        case eLengthUnits.In:
                            Length *= 0.03937007874015748031496062992126;
                            break;
                    }
                    break;
                case eLengthUnits.ft:
                    switch (OutputUnit)
                    {
                        case eLengthUnits.m:
                            Length *= 0.3048;
                            break;
                        case eLengthUnits.cm:
                            Length *= 30.48;
                            break;
                        case eLengthUnits.mm:
                            Length *= 304.8;
                            break;
                        case eLengthUnits.In:
                            Length *= 12;
                            break;
                    }
                    break;
                case eLengthUnits.In:
                    switch (OutputUnit)
                    {
                        case eLengthUnits.m:
                            Length *= 0.0254;
                            break;
                        case eLengthUnits.cm:
                            Length *= 2.54;
                            break;
                        case eLengthUnits.mm:
                            Length *= 25.4;
                            break;
                        case eLengthUnits.ft:
                            Length *= 0.083333333333333333333333333333333333333333333333333333;
                            break;
                    }
                    break;
            }

            return Length;
        }

        /// <summary>
        /// Converts force from one unit system to the other.
        /// </summary>
        /// <param fileName="Force">Force that is going to be converted from one unit system to the other.</param>
        /// <param fileName="InputUnit">The current unit system of the measurment.</param>
        /// <param fileName="OutputUnit">The unit system to which the measurment is going to be converted.</param>
        /// <returns></returns>
        public static double Convert(double Force, eForceUints InputUnit, eForceUints OutputUnit)
        {
            //Switchs between different possibilities.
            switch (InputUnit)
            {
                case eForceUints.N:
                    switch (OutputUnit)
                    {
                        case eForceUints.KN:
                            Force *= 0.001;
                            break;
                        case eForceUints.lb:
                            Force *= 0.22480892365533914449413720807999;
                            break;
                        case eForceUints.Kip:
                            Force *= 0.00022480892365533914449413720807999;
                            break;
                    }
                    break;
                case eForceUints.KN:
                    switch (OutputUnit)
                    {
                        case eForceUints.N:
                            Force *= 1000;
                            break;
                        case eForceUints.lb:
                            Force *= 224.80892365533914449413720807999;
                            break;
                        case eForceUints.Kip:
                            Force *= 0.22480892365533914449413720807999;
                            break;
                    }
                    break;
                case eForceUints.lb:
                    switch (OutputUnit)
                    {
                        case eForceUints.KN:
                            Force *= 0.004448222;
                            break;
                        case eForceUints.N:
                            Force *= 4.448222;
                            break;
                        case eForceUints.Kip:
                            Force *= 0.001;
                            break;
                    }
                    break;
                case eForceUints.Kip:
                    switch (OutputUnit)
                    {
                        case eForceUints.KN:
                            Force *= 4.448222;
                            break;
                        case eForceUints.lb:
                            Force *= 1000;
                            break;
                        case eForceUints.N:
                            Force *= 4448.222;
                            break;
                    }
                    break;
            }
            return Force;
        }

        /// <summary>
        /// Converts stress from one unit system to the other.
        /// </summary>
        /// <param fileName="Stress">The magnitude of the stress that is going to be converted.</param>
        /// <param fileName="InputLengthUnit">Length unit used in the input stress.</param>
        /// <param fileName="InputFoceUnit">Force unit used in the input stress.</param>
        /// <param fileName="OutPutLengthUnit">Length unit system used in the out put stress.</param>
        /// <param fileName="OutPutForceUnit">Force unit system used in the out put stress.</param>
        /// <returns></returns>
        public static double Convert(double Stress, eLengthUnits InputLengthUnit, eForceUints InputFoceUnit, eLengthUnits OutPutLengthUnit, eForceUints OutPutForceUnit)
        {
            return Convert(Stress, InputFoceUnit, OutPutForceUnit) / Convert(1, InputLengthUnit, OutPutLengthUnit);
        }

        /// <summary>
        /// Converts moment from one unit sytem to the other.
        /// </summary>
        /// <param fileName="Moment">The magnitude of moment that is going to be converted.</param>
        /// <param fileName="InputlengthUnit">Length unit system used in the input moment.</param>
        /// <param fileName="OutPutLengthUnit">Length unit system used in the output moment.</param>
        /// <param fileName="InputForceUnit">Force unit system used in the input moment.</param>
        /// <param fileName="OutPutForceUnit">Force unit system used in the output moment.</param>
        /// <returns></returns>
        public static double Convert(double Moment, eLengthUnits InputlengthUnit, eLengthUnits OutPutLengthUnit, eForceUints InputForceUnit, eForceUints OutPutForceUnit)
        {
            return Convert(Moment, InputForceUnit, OutPutForceUnit) * Convert(1, InputlengthUnit, OutPutLengthUnit);
        }

        public static double ConvertArea(double Area, eLengthUnits InputUnit, eLengthUnits OutputUnit)
        {
            return Area * Convert(1, InputUnit, OutputUnit) * Convert(1, InputUnit, OutputUnit);
        }
        /// <summary>
        /// Sorts the one dimensional double array in the specified order.
        /// </summary>
        /// <param fileName="array">The array to be sorted.</param>
        /// <param fileName="inAscendignOrder">True to sort in ascending order, otherwise false.</param>
        public static void Sort(double[] array, bool inAscendignOrder = true)
        {
            if (inAscendignOrder)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = i + 1; j < array.Length; j++)
                    {
                        if (array[i] > array[j])
                        {
                            double temp = array[i];
                            array[i] = array[j];
                            array[j] = temp;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = i + 1; j < array.Length; j++)
                    {
                        if (array[i] < array[j])
                        {
                            double temp = array[i];
                            array[i] = array[j];
                            array[j] = temp;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Converts the given length from the given unit systyem to system unit.
        /// </summary>
        /// <param name="length">Length to be converted.</param>
        /// <param name="lengthUnit">Length unit from  which the length is goint to be converted. </param>
        /// <returns></returns>
        public static double ConvertTo(double length, eLengthUnits lengthUnit)
        {
            return Convert(length, lengthUnit, SLU);
        }

        /// <summary>
        /// Converts the given force from the given unit systyem to system unit.
        /// </summary>
        /// <param name="force">Force to be converted.</param>
        /// <param name="forceUnit">Force unit from  which the force is goint to be converted. </param>
        /// <returns></returns>
        public static double ConvertTo(double force, eForceUints forceUnit)
        {
            return Convert(force,forceUnit , SFU);
        }

        /// <summary>
        /// Converts the given moment from the given unit systyem to system unit.
        /// </summary>
        /// <param name="moment">Moment to be converted.</param>
        /// <param name="lengthUnit">Length unit from  which the length is goint to be converted. </param>
        /// <param name="forceUnit">Force unit from  which the force is goint to be converted.</param>
        /// <returns></returns>
        public static double ConvertTo(double moment, eLengthUnits lengthUnit, eForceUints forceUnit)
        {
            return Convert(moment, lengthUnit, SLU, forceUnit, SFU);
        }

        /// <summary>
        /// Converts the given length from system unit to the given unit system.
        /// </summary>
        /// <param name="length">Measurment to be converted.</param>
        /// <param name="lengthUnit">Length unit to which the given measuremtn is goint to be converted.</param>
        /// <returns></returns>
        public static double ConvertFrom(double length, eLengthUnits lengthUnit)
        {
            return Convert(length, SLU, lengthUnit);
        }

        /// <summary>
        /// Converts the given force from system unit to the given unit system.
        /// </summary>
        /// <param name="force">Measurment to be converted.</param>
        /// <param name="forceUnit">Force unit to which the given measuremtn is goint to be converted.</param>
        /// <returns></returns>
        public static double ConvertFrom(double force, eForceUints forceUnit)
        {
            return Convert(force,SFU,forceUnit);
        }

        /// <summary>
        /// Converts the given moment from system unit to the given unit system.
        /// </summary>
        /// <param name="moment">Measurment to be converted.</param>
        /// <param name="lengthUnit">Length unit to which the given measuremtn is goint to be converted.</param>
        /// <param name="forceUnit">Force unit to which the given measuremtn is goint to be converted.</param>
        /// <returns></returns>
        public static double ConvertFrom(double moment, eLengthUnits lengthUnit, eForceUints forceUnit)
        {
            return Convert(moment, SLU, lengthUnit, SFU, forceUnit);
        }

        /// <summary>
        /// Gets the linear interpolation of a point from two points.
        /// </summary>
        /// <param name="X1">First independent value.</param>
        /// <param name="Y1">First dependent value.</param>
        /// <param name="X2">Second independent value.</param>
        /// <param name="Y2">Second dependent value.</param>
        /// <param name="X">Dependent value for the required value.</param>
        public static double Interpolate(double X1, double Y1, double X2, double Y2, double X)
        {
            return Y2 + (X - X2) * (Y1 - Y2) / (X1 - X2);
        }

        /// <summary>
        /// Rounds a given double value to upper integral part.
        /// </summary>
        /// <param name="value">Value to be rounded.</param>
        /// <returns></returns>
        public static int RoundUp(double value)
        {
            if (Math.Truncate(value) != value)
                return (int)(Math.Truncate(value) + 1);
            else
                return (int)value;
        }

        /// <summary>
        /// Fills a combo box with all the elements of a given enumeration.
        /// </summary>
        /// <typeparam name="EnumType">Enumeration to fill the combo box with</typeparam>
        /// <param name="ComboBoxTobeFilled">The combo box to be filled with the enumeration</param>
        /// <param name="EraseFormerValues">Apends to existing values if the value is false, otherwise replaces them.</param>
        public static void FillComboBox<EnumType>(ComboBox ComboBoxTobeFilled, bool EraseFormerValues = false)
        {
            if (EraseFormerValues)
            {
                while (ComboBoxTobeFilled.Items.Count > 0)
                    ComboBoxTobeFilled.Items.RemoveAt(0);
            }
            EnumType[] daf = (EnumType[])Enum.GetValues(typeof(EnumType));

            for (int i = 0; i < daf.Length; i++)
            {
                ComboBoxTobeFilled.Items.Add(daf[i]);
            }
        }

        /// <summary>
        /// Fills a combo box with all the elements of a given enumeration.
        /// </summary>
        /// <typeparam name="EnumType">Enumeration to fill the combo box with</typeparam>
        /// <param name="ComboBoxTobeFilled">The combo box to be filled with the enumeration</param>
        /// <param name="EraseFormerValues">Apends to existing values if the value is false, otherwise replaces them.</param>
        public static void FillComboBox<EnumType>(ComboBox ComboBoxTobeFilled, EnumType selectedItem, bool EraseFormerValues = false)
        {
            if (EraseFormerValues)
            {
                while (ComboBoxTobeFilled.Items.Count > 0)
                    ComboBoxTobeFilled.Items.RemoveAt(0);
            }
            EnumType[] daf = (EnumType[])Enum.GetValues(typeof(EnumType));

            for (int i = 0; i < daf.Length; i++)
            {
                ComboBoxTobeFilled.Items.Add(daf[i]);
            }
            ComboBoxTobeFilled.SelectedItem = selectedItem;
        }

        /// <summary>
        /// Fills a given combo box with the elements of an enumeration from 'StartIndex' to 'StopIndex'.
        /// </summary>
        /// <typeparam name="EnumType">The enumeration to fill the combo box with.</typeparam>
        /// <param name="ComboBoxTobeFilled">Combo box to be filled.</param>
        /// <param name="StartIndex">Zero based index of the first required element of the enumeration.</param>
        /// <param name="StopIndex">Zero based index of the last required element of the enumeration</param>
        /// <param name="EraseFormerValues">Apends to existing values if the value is false, otherwise replaces them.</param>
        public static void FillComboBox<EnumType>(ComboBox ComboBoxTobeFilled, int StartIndex, int StopIndex, bool EraseFormerValues = false)
        {
            if (EraseFormerValues)
            {
                while (ComboBoxTobeFilled.Items.Count > 0)
                    ComboBoxTobeFilled.Items.RemoveAt(0);
            }
            EnumType[] daf = (EnumType[])Enum.GetValues(typeof(EnumType));

            for (int i = StartIndex; i < StopIndex; i++)
            {
                ComboBoxTobeFilled.Items.Add(daf[i]);
            }
        }

        /// <summary>
        /// Fills a combo box with elements of an enumeration starting from 'StartIndex' to the end.
        /// </summary>
        /// <typeparam name="EnumType">Enumeration to fill the combo box with.</typeparam>
        /// <param name="ComboBoxTobeFilled">The combo box to be filled.</param>
        /// <param name="StartIndex">Zero based index of the first element to be added to the combo box.</param>
        /// <param name="EraseFormerValues">Apends to existing values if the value is false, otherwise replaces them.</param>
        public static void FillComboBox<EnumType>(ComboBox ComboBoxTobeFilled, int StartIndex, bool EraseFormerValues = false)
        {
            if (EraseFormerValues)
            {
                while (ComboBoxTobeFilled.Items.Count > 0)
                    ComboBoxTobeFilled.Items.RemoveAt(0);
            }
            EnumType[] daf = (EnumType[])Enum.GetValues(typeof(EnumType));

            for (int i = StartIndex; i < daf.Length; i++)
            {
                ComboBoxTobeFilled.Items.Add(daf[i]);
            }
        }

        public static eConcrete GetConcret(List<eConcrete> concretes, string name)
        {
            for (int i = 0; i < concretes.Count; i++)
            {
                if (concretes[i].Name == name)
                    return concretes[i];
            }
            throw new Exception(" The specified Concrete material is not found.");
        }

        public static eSteel GetSteel(List<eSteel> steels, string name)
        {
            for (int i = 0; i < steels.Count; i++)
            {
                if (steels[i].Name == name)
                    return steels[i];
            }
            throw new Exception(" The specified steel material is not found.");
        }

       
    }

}
