using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESADS.Mechanics.Design
{
    /// <summary>
    /// Contains static methods used to arrange calculated area of steel considering economy,code requirement,symetry and congestion constraints.
    /// </summary>
    public static class eBarManager
    {
        #region Bar Combination Generator

        /// <summary>
        /// Gets all the possible bars combinations of the given bars constraints to cover the given area below a certain area limit.
        /// </summary>
        /// <param name="Diameters">Diameters of the bars constriants sorted in descending order of size.</param>
        /// <param name="AsCalc">The calculated area of steel which is going to be covered by each combination. It is the area to be covered by the barType1 passed.</param>
        /// <param name="AsMax">The maximum allowable area for a combination.</param>
        /// <param name="AsOriginal">The original area of steel for which the combinations are going to be computed.</param>
        /// <param name="Combinations">Used to hold all the combinations in the form of array of bars numbers in the same order as they appear in the constraints array.</param>
        /// <param name="Areas">Used to store the area of a combination found at the same index in the Combinations collection. Can be initialized with an appropriate new collection object.</param>
        /// <param name="Combination">Represents a single combination. The numbers represent the number of a bars at the same index in the constraints array. At the first call
        /// to the method pass a new object having equal dimention as that of the diameters array.</param>
        private static void GetCombinations(double[] Diameters, double AsCalc, double AsMax, double AsOriginal, List<int[]> Combinations, List<double> Areas, int[] Combination)
        {
            double AsCurrent;  //It is the area added to the combination by the last inserted bars.

            if (Diameters.Length == 1)  //Checks if the constriant contains a single bars only.
            {   //Case when one bars constraint is dealt.

                //Fills the combination with the number requiredEffectiveDepth to cover the calculated area with the single bars. Since the smallest bars is always at the start of the 
                //combination, this bars is inserted at the first index of the combination.
                Combination[0] = GetNumberOf(Diameters[0], AsCalc);
                //The total area of the barType1 inserted by the above line.
                AsCurrent = GetAreaOf(Diameters[0], Combination[0]);
                //The combination and its area are stored into their corresponding collections
                StoreCombination(Combinations, Combination, Areas, AsOriginal + (AsCurrent - AsCalc), AsMax);
            }
            else
            {//Case when more than one bars

                double AsRemainder; //The area of steel to be covered by the rest of the barType1 in the constriant barType1 array.

                //Runs from the minimum requiredEffectiveDepth number of the first bars in the bars constraint if it alone was used to cover the calculated area, to zero which means when non of it was used.
                for (int i = GetNumberOf(Diameters[Diameters.Length - 1], AsCalc); i >= 0; i--)
                {
                    //The number of bars for the iteration is filled into the combination
                    Combination[Diameters.Length - 1] = i;
                    //The area added to the combination by the last bars inserted is calculated.
                    AsCurrent = GetAreaOf(Diameters[Diameters.Length - 1], i);
                    //The remainder area is calculated based on two conditions, viz. when the area inserted is greater than the calculated area and when the reverse is true. When the inserted area is 
                    //greater, it means that all the calculated area is covered by the number of barType1 dealt with. Therefore, the remainder bars to be covered by the other barType1 is zero. Oherwise, the actual difference is taken.
                    AsRemainder = (AsCalc - AsCurrent) > 0.0 ? (AsCalc - AsCurrent) : 0.0;

                    if (AsRemainder > 0)    //Checks if there is remainder area to be covered by the other barType1.
                    {   //Case when there is remainder area
                        double[] RemainderDiameters;    //Array of bars diameters generated from the constriant barType1 by excluding the first bars since it is being dealt with by this loop.
                        //Generate the remainder barType1 to cover the remainder area
                        GenerateRemainderDiameters(Diameters, out RemainderDiameters);
                        //Consider the remainder barType1 and the remainder area as a new combination problem and pass all the arguments. Consider the method use of 'FillCombinations' as non first time user. 
                        GetCombinations(RemainderDiameters, AsRemainder, AsMax, AsOriginal, Combinations, Areas, Combination);
                    }
                    else
                    {   //Case when there is no remainder area

                        //Since no remainder area means there is no need of the other barType1 for this specific combination, their number should be zero for this combination.
                        FillTheRestOfBars(Combination, Diameters.Length - 1);
                        //Store the combination since it has fulfilled the requiredEffectiveDepth area.
                        StoreCombination(Combinations, Combination, Areas, AsOriginal + (AsCurrent - AsCalc), AsMax);
                    }
                }
            }
        }

        /// <summary>
        /// Completes a combination by assigning a value zero to the barType1 beyond a given index. This is used when the barType1 already inserted in the combination are enough
        /// and thus to make sure that the rest of the numbers are zero.
        /// </summary>
        /// <param name="Combination">The combination to be completed.</param>
        /// <param name="index">The index of the combination beyond which all the combination members are to be zero. The element at the index will not be altered.</param>
        private static void FillTheRestOfBars(int[] Combination, int index)
        {
            //Runs from the passed index plus 1 to the end of the combination filling all the values with zero.
            for (int j = index - 1; j >= 0; j--)
            {
                //Fills zero to the combination element at the iteration.
                Combination[j] = 0;
            }
        }

        /// <summary>
        /// Generates an array of diameters for next recursion from the current diameters array. 
        /// </summary>
        /// <param name="Diameters">The current array of diameters.</param>
        /// <param name="RemainderDiameters">The array to be passed to the next recursion. It is one element less than the current diameters array.</param>
        private static void GenerateRemainderDiameters(double[] Diameters, out double[] RemainderDiameters)
        {
            //The remainderDiameters array is initialized to dimension one less than the main diameters array.
            RemainderDiameters = new double[Diameters.Length - 1];
            //Runs from the second element to the last of the Diameters array.
            for (int i = 0; i < Diameters.Length - 1; i++)
            {
                //Assigns the elements of the diameters array to the remainder diameter array whose index lags the main diameters array by one.
                RemainderDiameters[i] = Diameters[i];
            }
        }

        /// <summary>
        /// Stores a completed combination and its total area to their corresponding collection at their appropriate location so that at the end both collection would be sorted in 
        /// ascending order of total area used.
        /// </summary>
        /// <param name="Combinations">Holds the arrays representing each of the combinations. They are sorted in ascending order of their total area.</param>
        /// <param name="Combination">A complete combination ready to be put into the collection of combination at its appropriate order.</param>
        /// <param name="Areas">Holds the areas of combinations lying in the combinations collection at the same index.</param>
        /// <param name="AsCombination">The total area of the completed combination. Both the combination and this area will have the same index in their corresponding collections.</param>
        /// <param name="AsMax">The maximum allowable for a combination to have. If a combination has an area more than this area, it won't be placed in combinations collection.</param>
        private static void StoreCombination(List<int[]> Combinations, int[] Combination, List<double> Areas, double AsCombination, double AsMax)
        {
            if (AsCombination > AsMax)     //Checks if the total area of the combination is greater than the maximum allowable area.
            {   //Case when the area of the combination is greater than the maximum allowable area
                //No need to deal with this combination. Therefore, the combination is just discarded.
                return;
            }
            //Runs through the areas collection searching for the first area greater than the area of the combination. Then both the area and the combination and the area are inserted at that index and returned from the method.
            for (int i = 0; i < Areas.Count; i++)
            {
                if (Areas[i] > AsCombination)   //Checks is the iteration element of the areas collection is greater than the area of the combination
                {   //Case when the area in the collection is greater than that of the area of the combination.
                    //Insert the area of the combination just before the area at the iteration index.
                    Areas.Insert(i, AsCombination);
                    //Insert the combination into the collection at the same index as the area.
                    Combinations.Insert(i, (int[])Combination.Clone());
                    //There is no need to continue any work since the objective of the method has been accomplished.
                    return;
                }
            }
            //When non of the areas in the areas collection is greater than the area of the combination, both the area and the combination are added at the end of their corresponding collections.
            Areas.Add(AsCombination);
            //Add the combination at the end of the combinations collection.
            Combinations.Add((int[])Combination.Clone());
            //End of work
            return;
        }

        /// <summary>
        /// Gets the area of a number of barType1 of the same diameter given their diameter and number only. 
        /// </summary>
        /// <param name="Diameter">The diameter of the barType1.</param>
        /// <param name="Number">The number of the barType1. For a single bars just leave this parameter unassigned.</param>
        /// <returns>The total area of the given number of barType1.</returns>
        private static double GetAreaOf(double Diameter, int Number = 1)
        {
            //The total area means the area of single bars((πd^2)/4) times the number of the bars.
            return Number * (Math.PI * Math.Pow(Diameter, 2) / 4);
        }

        /// <summary>
        /// Gets the minimum number of a certain bars to cover a given area.
        /// </summary>
        /// <param name="Diameter">The diameter of the bars.</param>
        /// <param name="Area">The area to covered by the number of barType1.</param>
        /// <returns>The minimum bars requiredEffectiveDepth.</returns>
        private static int GetNumberOf(double Diameter, double Area)
        {
            //The number when dividing the total area by the area of the single bars in the form of floating point number.
            double n = Area / GetAreaOf(Diameter);

            if ((int)n == n)    //Checks if the number is an integer, i.e. if its number after decimal is zero
            {   //Case when the area quotient is integer.
                //The integer part is returned
                return (int)n;
            }
            else
            {   //Case when the area quotient is non integer.
                //The quotient is returned being rounded up to the nearest 
                return (int)n + 1;
            }
        }

        #endregion

        #region Row Arranger

        /// <summary>
        /// Returns arranged rows of barType1 for a given combination.
        /// </summary>
        /// <param name="Diameters">Diameters of the barType1 used in the combination in descending order of diameter.</param>
        /// <param name="Combination">Number of each bars for each diameter in the combination.</param>
        /// <param name="EffectiveWidth">The usable width on which the barType1 can be palced.</param>
        /// <param name="MinSpacing">The minimum clear distance between two barType1.</param>
        /// <returns></returns>
        private static List<int[]> GetRows(double[] Diameters, int[] Combination, double EffectiveWidth, double MinSpacing)
        {

            List<int[]> Rows = new List<int[]>(); // Used to contian all rows and all barType1 in rows.
            double RemWidth = EffectiveWidth; // Width remaining after some intervals has been done.
            double[] MinRequiredWidth = GetMinWidthForEachDiam(Diameters, MinSpacing); // The minimum width requiredEffectiveDepth for one bars for each diameter. 
            int NumbForWidth = 0; //The maximum possible number of barType1 needed to fill a given width.
            Rows.Add(new int[Diameters.Length]);// Adds an empty row, since any combination should have at least one row.
            // Fills the barType1 in each rows.
            for (int i = 0; i < Combination.Length; )
            {
                //if number of bars in combination is zero it will continue to the next bars.
                if (Combination[i] == 0)
                {
                    i++;
                    continue;
                }
                NumbForWidth = GetNumbOfBarsForWidth(RemWidth, Diameters[i], MinSpacing);// Calculates the number of barType1 needed to fill a given width
                //with a given length and spacing.
                NumbForWidth = NumbForWidth < Combination[i] ? NumbForWidth : Combination[i]; // If the number of barType1 calculated is less than the 
                //available number, assigns the Number of barType1 for width to its calculated value, else assigns it to the available number of barType1.
                RemWidth -= GetWidthFor(Diameters[i], NumbForWidth, MinSpacing);// substracts the the used width from the available width.
                Combination[i] -= NumbForWidth;// Substact the arranged number of barType1 from the available number.
                Rows[Rows.Count - 1][i] = NumbForWidth;// Add the calculated number of barType1 to the row.
                // Checks if the remaining width is less the minimum width requiredEffectiveDepth to palce one bars.
                if ((RemWidth >= MinRequiredWidth[MinRequiredWidth.Length - 1]) && (RemWidth < MinRequiredWidth[i]))
                {
                    CompleteRow(Diameters, Combination, ref RemWidth, MinSpacing, Rows[Rows.Count - 1], MinRequiredWidth, i); //Complets
                    // the row by filling smaller barType1 if any space is remaining at the end and when it is not possible to place larger barType1.
                }
                //Checks if the number of barType1 remaining in the combination is zero.
                if (Combination[i] == 0)
                {
                    i++;
                }
                // strops the iteration if all the barType1 are placed in rows.
                if (Combination.Sum() == 0)
                {
                    break;
                }
                //Checks if the remaining width is less than the minimum space requiredEffectiveDepth for one bars of the smallest diameter or if all
                //elements after the current index are empty.
                if (RemWidth < MinRequiredWidth[MinRequiredWidth.Length - 1])
                {
                    Rows.Add(new int[Combination.Length]); // Adds an empty row.
                    RemWidth = EffectiveWidth; // Since empty row is added, the  effective width is assigned to the remaining width.
                }
                //Checks if the remaining width is not equal to the effective width
                else if (RemWidth != EffectiveWidth)
                {
                    RemWidth -= MinSpacing;// Remaining width should be given a space to palce the next type of bars.
                }
            }
            return Rows;
        }

        /// <summary>
        /// Returns minimum widths requiredEffectiveDepth for each diameters, if only one bars is used.
        /// </summary>
        /// <param name="Diametrs">Diameter of barType1 used.</param>
        /// <param name="MinSpacing">Minimum clear spacing between two barType1</param>
        /// <returns></returns>
        private static double[] GetMinWidthForEachDiam(double[] Diametrs, double MinSpacing)
        {
            double[] MinWidth = new double[Diametrs.Length]; // Contains the the minimum widths requiredEffectiveDepth for each barType1.

            // Fills all the minimum widths to the array.
            for (int i = 0; i < MinWidth.Length; i++)
            {
                MinWidth[i] = Diametrs[i] + MinSpacing; //Calculates the minimum width requiredEffectiveDepth for each bars by adding the 
                // diameter and the minimum spacing.
            }
            return MinWidth;
        }

        /// <summary>
        /// Returns the width requiredEffectiveDepth for a given diameter,number of barType1 and minimum spacing.
        /// </summary>
        /// <param name="Diameter">Diameter of the barType1 used</param>
        /// <param name="NumbOfBars">Number of barType1 in the width</param>
        /// <param name="MinSpacing">Minimum clear spacing between two barType1</param>
        /// <returns></returns>
        private static double GetWidthFor(double Diameter, int NumbOfBars, double MinSpacing)
        {
            //The spacing between barType1(s) is one less than the number of barType1(n). Thus, the total width occupied is [nd + (n-1)s] or when simplified, [n(d+s)-s]
            if (NumbOfBars == 0)
                return 0;
            else
                return NumbOfBars * (Diameter + MinSpacing) - MinSpacing;
        }

        /// <summary>
        /// Return the maximum possible number of barType1 in a given width for a given diameter and minimum spacing.
        /// </summary>
        /// <param name="Width"> Width on which the barType1 are going to be distributed.</param>
        /// <param name="Diameter">Diameter of the bars used.</param>
        /// <param name="MinSpacing">Minimum clear distance between two barType1.</param>
        /// <returns></returns>
        private static int GetNumbOfBarsForWidth(double Width, double Diameter, double MinSpacing)
        {
            return (int)((Width + MinSpacing) / (MinSpacing + Diameter));// calculates the the maximum number of barType1 needed to fill a given width.
        }

        /// <summary>
        /// Completes the given row by filling smaller barType1 if the space is not enough to place larger barType1.
        /// </summary>
        /// <param name="Diameters">Diameters used in the combination.</param>
        /// <param name="Combination">Number of each Diameter of barType1.</param>
        /// <param name="Width">The remaining width on which samaller barType1 are going to be placed.</param>
        /// <param name="MinSpacing">Minimum clear spacing between two barType1.</param>
        /// <param name="Row">Row which is needed to be complited.</param>
        /// <param name="MinWidth">Minimum width requiredEffectiveDepth for each diameters of bars if only one barType1 is used.</param>
        /// <param name="Index">The index of diameter from which smaller barType1 are going to be checked.</param>
        private static void CompleteRow(double[] Diameters, int[] Combination, ref double Width, double MinSpacing, int[] Row, double[] MinWidth, int Index)
        {
            int NumbForWidth = 0;//Number of barType1 requiredEffectiveDepth for a given width.
            //Completes the row by filling smaller barType1 in the remaining width.
            for (int i = Index + 1; i < Combination.Length; i++)
            {
                // checks if the width is greater than the minimum allowebale width to place at least one bars.
                if (Width >= MinWidth[i])
                {
                    NumbForWidth = GetNumbOfBarsForWidth(Width, Diameters[i], MinSpacing);
                    NumbForWidth = NumbForWidth < Combination[i] ? NumbForWidth : Combination[i];// Checks and corrects
                    //the calculate number of barType1 not to excced the allowable value.
                    Width -= (GetWidthFor(Diameters[i], NumbForWidth, MinSpacing) + MinSpacing);
                    Combination[i] -= NumbForWidth;
                    Row[i] = NumbForWidth;
                }
            }
        }

        /// <summary>
        /// checks if the elements of a combination are zero after a specified index.
        /// </summary>
        /// <param name="combination"> Combination to be checked</param>
        /// <param name="index">Index at which the checkin starts.</param>
        /// <returns></returns>
        private static bool IsAllElementsEmpty(int[] combination, int index)
        {
            //Iterates in the elements of the combination.
            for (int i = index + 1; i < combination.Length; i++)
            {
                //checks if there is at leas one zero element.
                if (combination[i] != 0)
                {
                    //If one element is different from zero it returns false.
                    return false;
                }
            }
            // If all elements are zero it returns true.
            return true;
        }
        #endregion

        #region Arrangement For Symmetry in a Row
        /*BRIEF ALGORITHM
             *Step 1: The entering bars numbers are categorized into odd and even numbered barType1.
             *Step 2: The odd numbers are then subtracted by one to make them even. Simultaneously, the bars diameters are stored in a separate array. 
             *Step 3: The even numbered barType1 are easily arranged by putting half of them to the left of the centerline of the the arrangement space, and 
             *        the other half to the opposite side.
             *Step 4: The remaining barType1 are checked for least centroid (which means best symmetry) by exchanging their places to address all possible 
             *        arrangements(The total number of possible arrangements can be calculated using permutation, i.e. n!.)
             *Step 5: The arrangement with the least centroid is then inserted into the middle of the former arrangements.
             */
        /// <summary>
        /// Gets the most symmetric arrangement of a row given the diameters and the corresponding numbers, effective width of the row. It also fills the
        /// centroid of the arrangement into a refernce variable.
        /// </summary>
        /// <param name="Diameters">An array containing all the diameters in the row. Some may exist evenif their number is zero.</param>
        /// <param name="NumberOfBars">Holds the number of the barType1 in the row. Its members may be odd, even or zero.</param>
        /// <param name="EffectiveWidth">The effective width occupied by the barType1 of the row.</param>
        /// <returns>The arrangement with diameters and the corresponding coordinates measured from the center line.</returns>
        private static double[,] GetSymmetricArrangements(double[] Diameters, int[] NumberOfBars, double EffectiveWidth)
        {
            double centroid = 0;
            //Holds the diameters which have odd number in the row.
            double[] oddDiameters;
            //The whole barType1 will be arranged in this array. The first row holds the bars diameters and the second the corresponding coordinates of the barType1.
            double[,] arrangement = new double[NumberOfBars.Sum(), 2];
            //An equal clear spacing between barType1 within the row.
            double spacing = GetSpacing(Diameters, NumberOfBars, EffectiveWidth);
            //Fill the oddDiameters array with barType1 taken from the odd numbered barType1 within the main array. It leaves the NumberOfBars array with all even numbers.
            oddDiameters = GetOddDiameters(Diameters, NumberOfBars);
            //Runs through the numberOfBars array which is holding all even numbers. The primary looping variable, 'j' is an index for the numberOfBars 
            //array. And the secondary looping variable, 'i' is an index of the arrangement array.
            for (int j = 0, i = 0; j < NumberOfBars.Length; j++)
            {
                //Runs from one to the half of the even number in the numberOfBars by filling the corresponding diameter into the main arrangement on both
                //sides of the center line.
                for (int k = 1; k <= NumberOfBars[j] / 2; k++)
                {
                    //Fills the corresponding diameter into the main arrangement on one side of the center line.
                    arrangement[i, 0] = Diameters[j];
                    //Fills the same bars on the other side of the center line.
                    arrangement[arrangement.GetUpperBound(0) - i, 0] = Diameters[j];
                    //increment the index for the main arrangement array.
                    i++;
                }
            }
            //Checks if there is an odd numbered bars in the row
            if (oddDiameters.Length > 0)
            {
                if (oddDiameters.Length > 1)//returns null if the arrangement is non semetrical.
                    return null;
                //Holds the arrangement of barType1 which had an odd number in the main bars numbers array. Non of the barType1 can be identical.
                double[,] middleArrangement = new double[oddDiameters.Length, 2];
                //Fills the arrangement with the least centroid from the center line.
                FillArrangement(middleArrangement, new double[middleArrangement.GetLength(0), 2], ref centroid, oddDiameters, spacing);
                //Fills the best into the middle of the main arrangement. The first index is the half of the total even numbers of barType1.
                for (int i = 0, j = NumberOfBars.Sum() / 2; i < middleArrangement.GetLength(0); i++, j++)
                {
                    arrangement[j, 0] = middleArrangement[i, 0];
                }
            }
            //Fills the coordinates of the arrangement after the middle arrangements are filled into the main arrangement.
            FillCoordinates(arrangement, spacing);
            //Fills the centroid of the main arrangement.
            centroid = GetCentroid(arrangement);
            //Returns the major arrangement.
            return arrangement;
        }
        /*BRIEF ALGORITH
         * Step 1: Check if the diameter entered is one.
    * Step 2: If the diameter entered is one, fill in the single bars at the end of the arrangement.
 * Step 3: Calculate the corresponding coordinates from the center line, of all barType1 provided in the arrangement depending on the clear spacing.
 * Step 4: Calculate the centroid of the arrangement from the center line of the width of intervals.
 * Step 5: If the absolute value of the calculated centroid is less than that of the best arrangement, replace the best centroid with the temporary arrangement and their centroid too. Then Stop.
 * Step 6: Else If the diamters entered is greater than one, Iterate through the barType1 entered by placing and replacing each after the last ber entered when the arrangement was received.
 * Step 7: Within each iteration, also produce new bars diamter array by leaving the entered bars to be candidates for the remaining places in the main arrangement.
 * Step 8: Using the new bars arrangement array as new diameter entered, goto step 1.
 * */
        /// <summary>
        /// Fills the arrangement of barType1 whose diameters and the clear spacing are passed in a one dimensional array. It fills the coordinate of the 
        /// arrangement with the least centroid. 
        /// </summary>
        /// <param name="BestArrangement">An array to hold the order of the best arrangement with its corresponding coordinates. Pass the variable to hold
        /// the final arrangement here.</param>
        /// <param name="TempArrangement">A temporary array to hold any arrangement until it will be compared to the best arrangement by their centroid.</param>
        /// <param name="CentroidOfBest">Holds by reference, the centroid of the best arrangement.</param>
        /// <param name="Diameters">The diameters of the barType1. If there are duplicates in these diameters, this method is no efficient, use GetSymmetricArrangements() method instead.</param>
        /// <param name="Spacing">The clear spacing between barType1.</param>
        private static void FillArrangement(double[,] BestArrangement, double[,] TempArrangement, ref double CentroidOfBest, double[] Diameters, double Spacing)
        {
            //Checks if the diameter entered is one.
            if (Diameters.Length == 1)
            {
                //Checks if the first bars to enter is the first for the arrangement.
                if (Diameters.Length == BestArrangement.GetLength(0))
                {
                    //Fills in the barType1 into the arrangement as the first trial for the best arrangement.
                    InitializeBestArrangement(BestArrangement, ref CentroidOfBest, Diameters, Spacing);
                }
                //Fills in the only bars in the diameters array into the arrangement at the last index.
                TempArrangement[TempArrangement.GetLength(0) - 1, 0] = Diameters[0];
                //Calculates the coordinates of the arrangement according to the spacing passed.
                FillCoordinates(TempArrangement, Spacing);
                //Gets the centroid of the arrangement and fills in the value to the local variable.
                double tempCentroid = GetCentroid(TempArrangement);
                //Checks if the absolute value of this temporary arrangement is less than that of the best arrangement.
                if (Math.Abs(tempCentroid) < Math.Abs(CentroidOfBest))
                {
                    //Fill the centroid of the current arrangement into that of the best arrangement. 
                    CentroidOfBest = tempCentroid;
                    //Copies the temporary arrangement to the best arrangement.
                    Copy(TempArrangement, BestArrangement);
                }
            }
            else
            {   //Case when the number of diameters entered is greater than one.
                //Checks if the first bars to enter is the first for the arrangement.
                if (Diameters.Length == BestArrangement.GetLength(0))
                    //Fills in the barType1 into the arrangement as the first trial for the best arrangement.
                    InitializeBestArrangement(BestArrangement, ref CentroidOfBest, Diameters, Spacing);
                //An array to hold the next set of diameters to be put into the arrangement.
                double[] subDiameters = new double[Diameters.Length - 1];
                //Runs through the diameters array alternating the diameters by putting each on the last index unoccupied by barType1.
                for (int i = 0; i < Diameters.Length; i++)
                {
                    //Fills the temporary arrangement with the current iteration element of the barType1 at the last index not filled with bars.
                    TempArrangement[TempArrangement.GetLength(0) - Diameters.Length, 0] = Diameters[i];
                    //Produces a set of diameters one less than the entered diameters from it by eliminating the diameter placed last by the above
                    //statement.
                    FillSubDiameters(Diameters, subDiameters, i);
                    //Sends the arrangement to be filled with the remaining set of barType1.
                    FillArrangement(BestArrangement, TempArrangement, ref CentroidOfBest, subDiameters, Spacing);
                }
            }
        }
        /// <summary>
        /// Copies the first two dimensional double array to the second two dimensional array elementwisely.
        /// </summary>
        /// <param name="From">The array to be copied.</param>
        /// <param name="To">The array to receive the copied array.</param>
        private static void Copy(double[,] From, double[,] To)
        {
            //Runs through the first indices of both arrays.
            for (int i = 0; i < From.GetLength(0); i++)
            {
                //Runs through the second indices of both arrays.
                for (int j = 0; j < From.GetLength(1); j++)
                {
                    //Copies the iteration element of the second array to that of the first.
                    To[i, j] = From[i, j];
                }
            }
        }
        /// <summary>
        /// Initializes the BestArrangement array by filling all the diameters passed by putting them in the same order as they appear in the diameters
        /// array. It simultaneously fills the centroid into a variable passed by reference.
        /// </summary>
        /// <param name="BestArrangement">The BestArray to be initialized. It has second dimension of two, for diameter and coordinate. The second dimension
        /// shall be equal to the length of the diameters array.</param>
        /// <param name="CentroidOfBest">The reference to a variable holding the centroid of the best arrangement.</param>
        /// <param name="Diameters">Array of bars diameters to be arranged into the row.</param>
        /// <param name="Spacing">The average clear spacing to be applied between the barType1.</param>
        private static void InitializeBestArrangement(double[,] BestArrangement, ref double CentroidOfBest, double[] Diameters, double Spacing)
        {
            //Runs through the best arrangement filling the first element of the second dimension with the corresponding element of the diameters element.
            for (int i = 0; i < Diameters.Length; i++)
            {
                //Fills the iteration element of the diameters to the iteration element, first index(diameter holder) of the best arrangement.
                BestArrangement[i, 0] = Diameters[i];
            }
            //Fills the coordinates of each barType1 in the arrangement relative to the center of the intervals width.
            FillCoordinates(BestArrangement, Spacing);
            //Gets the centroid of the best arrangement based on the coordinate and the area of each bars and assigns it to the CentroidOfBest variable passed by reference.
            CentroidOfBest = GetCentroid(BestArrangement);
        }
        /// <summary>
        /// Fills the coordinates of the barType1 whose diameters are specified in the first index of the second dimension based on the average spacing passed.
        /// </summary>
        /// <param name="Arrangement">An array holding an arrangement. It is a two dimensional array whose first index of its second dimension is filled with 
        /// the diameters of the barType1 in the arrangement. The coordinates are going to be filled into the second elements of the second dimension.</param>
        /// <param name="Spacing">Is the average clear spacing between barType1 to be adopting while placing the barType1.</param>
        private static void FillCoordinates(double[,] Arrangement, double Spacing)
        {
            /* * * * * * ALGORITHM * * * * * * * *
             * Step 1: Compute the total effective width to be occupied by all the barType1.
             * Step 2: For the first left element, calculate its coordinate by subtracting the half of the total effective width from the half of its diameter.
             * Step 3: For each of the barType1 compute its coordinate by adding the average spacing, half of its diameter and half of the diameter of the bars
             *         immediately to the left of the bars, to the coordiante of the bars immediately to the left of the bars.
             * */

            //Holds the total effective width occupied by all the barType1 in the arrangement when arranged with the specified spacing between them.
            double b = GetWidth(Arrangement, Spacing);
            //Fills the coordinate of the first bars by subtracting the half of the effective spacing from the half of the first diamter.
            Arrangement[0, 1] = (Arrangement[0, 0] - b) / 2;
            //Checks if the arrangement contains more than one element so as to decide whether to continue computing the coordinates.
            if (Arrangement.GetLength(0) > 1)
            {
                //Runs through the first dimension of the arrangement array filling the coordinate of each starting from the second element to the end.
                for (int i = 1; i < Arrangement.GetLength(0); i++)
                {
                    //Fills the coordinate of the iteration element by adding the spacing, half of both the current and previous diameters to the previous coordinate.
                    Arrangement[i, 1] = Arrangement[i - 1, 1] + Spacing + (Arrangement[i - 1, 0] + Arrangement[i, 0]) / 2;
                }
            }
        }
        /// <summary>
        /// Gets the total effective width occupied by all barType1 placed in an arrangement given the arrangement and the average clear spacing.
        /// </summary>
        /// <param name="Arrangement">A two dimensional array holding the diameters of the barType1 in the first index of the second dimension.</param>
        /// <param name="Spacing">The average clear spacing between barType1.</param>
        /// <returns>The total effective width occupied by the barType1 and the spacing between them.</returns>
        private static double GetWidth(double[,] Arrangement, double Spacing)
        {
            //Holds the total effective width of the arrangement. Is initialized to the diameter of the first bars.
            double b = Arrangement[0, 0];
            //runs through the first dimension of the arrangement array adding a single spacing and the additional diameter for the iteration bars.
            for (int i = 1; i < Arrangement.GetLength(0); i++)
            {
                //Adds one spacing and the diamter of the iteration bars to the total effective width.
                b += Arrangement[i, 0] + Spacing;
            }
            //Reterns the total effective width of the arrangement.
            return b;
        }
        /// <summary>
        /// Gets the centroid of an arrangement in the horizontal axis measured from the mid-point of the intervals width. Positive is considered to the right of the mid-point.
        /// </summary>
        /// <param name="Arrangement">A two dimensional array containing the diameters in its first index of the second dimension and its coordinates in the second index of the second
        /// dimension.</param>
        /// <returns>The centroid of an arrangement measured from the center of the intervals width.</returns>
        private static double GetCentroid(double[,] Arrangement)
        {
            //Holds the area of a single in an iteration.
            double area;
            //Holds the cumulative area of an arrangement.
            double cumulativeArea = 0;
            //Holds the cumulative of the product of the area of each bars and its coordinate.
            double cumulativeProduct = 0;
            //Runs through the first dimension of the arrangement array building the cumulative variables.
            for (int i = 0; i < Arrangement.GetLength(0); i++)
            {
                //Computes the area of the iteration bars using the area of circle.
                area = Math.PI * Math.Pow(Arrangement[i, 0], 2) / 4;
                //Adds the current area of the bars to the cumulative area of the arrangement.
                cumulativeArea += area;
                //Adds the product of the area and the iteration coordinate to the cumulative product.
                cumulativeProduct += (area * Arrangement[i, 1]);
            }
            //Returns the quotient of the cumulative product and cumulative area. This means that this quotient is the weighted average of the coordinates, the area used as 
            //weighting factor.
            return cumulativeProduct / cumulativeArea;
        }
        /// <summary>
        /// Fills the SubDiameters array which is one less than the diameters array with all the elements of the diameters array except the element at a specified index.
        /// </summary>
        /// <param name="Diameters">The source of the diameters to be filled into the subDiameters array.</param>
        /// <param name="SubDiameters">An array to be filled with diamters. Its length is one less than that of the main diamters array.</param>
        /// <param name="Index">The index of an elelement in the main diameters array which is not to be included in the subDiameters array.</param>
        private static void FillSubDiameters(double[] Diameters, double[] SubDiameters, int Index)
        {
            //Runs through the main diameters array copying all the elements to the subdiameters array except that at the specified index.
            for (int i = 0, j = 0; i < Diameters.Length; i++)
            {
                //Checks if the loop index is not equal to the passed index
                if (i != Index)
                {
                    //Copies the iteration element to the subdiameter array and increments the index of the subDiameters.
                    SubDiameters[j++] = Diameters[i];
                }
            }
        }
        /// <summary>
        /// Gets the average clear spacing between barType1, given their diameters and the total effective width for a row of barType1. It is assumed that when the number of barType1 
        /// is placed into the row the minimum spacing requirement is satisfied so that there is no need to check for it here.
        /// </summary>
        /// <param name="Diameters">An array containing the diameters of different barType1 found in the row.</param>
        /// <param name="NumberOfBars">The corresponding number of the barType1 whose diameters are passed by the Diameters array parameter.</param>
        /// <param name="EffectiveWidth">The total effective width occupied by both the barType1 and the clear spacing between them.</param>
        /// <returns>Returns the equal or average clear spacing between the barType1 of the row so that there is no spare space left at the end.</returns>
        private static double GetSpacing(double[] Diameters, int[] NumberOfBars, double EffectiveWidth)
        {
            //Holds the effective width occupied by the barType1 only
            double barsWidth = 0;
            //Runs through the diameters array accumulating the width occupied by the barType1 only.
            for (int i = 0; i < Diameters.Length; i++)
            {
                //Adds the total width occupied by a number of duplicates of the iteration bars type to the total width occupied by all barType1.
                barsWidth += (NumberOfBars[i] * Diameters[i]);
            }
            //Divides the remaining space available for clear spacing by the number of spacings( one less than the total number of barType1 in the row)
            return (EffectiveWidth - barsWidth) / (NumberOfBars.Sum() - 1);
        }
        /// <summary>
        /// Gets a one dimensional array containing the diameters of the barType1 whose number in the row when passed first was odd. It also alters the odd numbers in NumberOfBars
        /// array to even numbers by subtracting one from each.
        /// </summary>
        /// <param name="Diameters">The array containing the diameters of the barType1 contained in the row.</param>
        /// <param name="NumberOfBars">The number of the corresponding barType1 whose diameters are specified in the diameters array.</param>
        /// <returns>A one dimensional array containing the diameters of the barType1 whose number in the row was odd when first entered. There will be no duplicate in this array
        /// unless there was a duplicate diameter in the original diameters array with all odd numbered.</returns>
        private static double[] GetOddDiameters(double[] Diameters, int[] NumberOfBars)
        {
            //Holds the reference the method taking an integer and returning true if the integer value is odd and otherwise false.
            Func<int, bool> oddNumberSelector = IsOdd;
            //Holds the number of barType1 having odd number in the row. The built-in method, Count is used to do the count.
            int count = NumberOfBars.Count<int>(oddNumberSelector);
            //An array to hold the bars diameters with odd numbers.
            double[] oddDiams = new double[count];
            //Runs through the all the barType1 checking if their number is odd and if so, adds its diameter to the odd diameters array and subtracts one from its number to make it even.
            for (int i = 0, j = 0; i < NumberOfBars.Length; i++)
            {
                //Checks if the number of the iteration bars is odd
                if (NumberOfBars[i] % 2 == 1)
                {
                    //Adds the corresponding diameter to the oddDiams array.
                    oddDiams[j++] = Diameters[i];
                    //Subtracts one from the number of barType1 to make it even.
                    NumberOfBars[i]--;
                }
            }
            //Returns the array of odd numbered barType1.
            return oddDiams;
        }
        /// <summary>
        /// Checks if a number passed is odd number.
        /// </summary>
        /// <param name="num">The number to be checked for oddness.</param>
        /// <returns>true is the number is odd and false it is not odd, i.e. even.</returns>
        private static bool IsOdd(int num)
        {
            //Checks if the remainder when the number divided by two is zero.
            if ((num % 2) == 0)
            {//case even
                return false;
            }
            else
            {//case odd
                return true;
            }
        }
        #endregion

        #region Y-Coordinate Filler
        /// <summary>
        /// Fills the Y-coordinates measured from the furthest c compression fiber from the neutral axis, of each of the barType1 in a combination. The combination shall be 
        /// arranged into rows beforehand.
        /// </summary>
        /// <param name="RowsOfComb">List of the rows in which each row is expressed as two dimensional array, the first dimension equal to the bars types involved in a row and
        /// the second dimension of two. The first index of the second dimension of any bars is the dimeter of the bars. The second index is not used here.</param>
        /// <param name="GrossDepth">The total height of the c beam, i.e. from the furthest compression fiber to the furthest tension fiber of c.</param>
        /// <param name="ConcreteCover">The c cover provided for any steel to be far from the c surface.</param>
        /// <param name="StirrupDiameter">The diameter of the stirrup used to hold the longitudinal barType1 in place.</param>
        /// <param name="MinSpacing">Minimum allowable clear spacing between barType1 both horizontally and vertically.</param>
        /// <param name="StirrupPosn">The ralative position of the stirrups relative to the longitudinal bars that they hold. This doesn't concern the bottom row in which the 
        /// stirrup is always on the bottom of the longitudinal barType1.</param>
        /// <param name="Y_Coordinates">List to be filled by the array of coordinates in the same order as the RowsOfComb.</param>
        static void FillCombinationY_Coordinates(List<double[,]> RowsOfComb, double GrossDepth, double ConcreteCover, double StirrupDiameter, double MinSpacing,
            eRelativeStirrupPosition StirrupPosn, List<double[]> Y_Coordinates)
        {
            //Holds a representative coordinate of a row which is the coordinate of the point of contact of the stirrup and the rows which is common for all barType1 of a row.
            double basicCoordinate;
            //Adds an empty array to the Y-Coordinates list with equal length as that of the first dimension of the first element of the Rows.
            //It represents the first(bottom) row.
            Y_Coordinates.Add(new double[RowsOfComb[0].GetLength(0)]);
            //The basic coordinate of the first row is computed by subtracting the stirrup diameter and the c cover from the gross depth.
            basicCoordinate = GrossDepth - StirrupDiameter - ConcreteCover;
            //Fills the y-coordinates of the barType1 in the row.
            FillRowY_Coordinates(RowsOfComb[0], Y_Coordinates[0], basicCoordinate);
            //Checks if there is remaining row in the combination. If not, returns.
            if (RowsOfComb.Count < 2)
                return;
            //Checks if the stirrup position preference is stirrup at the bottom
            if (StirrupPosn == eRelativeStirrupPosition.StirrupAtBottom)
            {
                //Runs through the rows starting from the second row to the end, filling the bars coordinates for each.
                for (int i = 1; i < RowsOfComb.Count; i++)
                {
                    //Initializes an empty array to hold the coordinates for the iteration row.
                    Y_Coordinates.Add(new double[RowsOfComb[i].GetLength(0)]);
                    //computes the basic coordinate of the iteration row by subtracting diameter of the largest bars from the previous row, spacing and stirrup diameter.
                    basicCoordinate -= (RowsOfComb[i - 1][0, 0] + MinSpacing + StirrupDiameter);
                    //Fills the coordinates of the iteration row.
                    FillRowY_Coordinates(RowsOfComb[i], Y_Coordinates[i], basicCoordinate);
                }
            }
            else
            {   //Case the stirrup position preference is stirrup at the top.
                //Initializes an empty array to hold the coordinates for the second row.
                Y_Coordinates.Add(new double[RowsOfComb[1].GetLength(0)]);
                //computes the basic coordinate of the second row by subtracting diameter of the largest bars from the previous row, from the current row and spacing.
                basicCoordinate -= (RowsOfComb[0][0, 0] + MinSpacing + RowsOfComb[1][0, 0]);
                //Fills the coordinates of the second row.
                FillRowY_Coordinates(RowsOfComb[1], Y_Coordinates[1], basicCoordinate, StirrupPosn);
                //Runs through the elements of the Rows starting from the third to the last element filling the coordinates for each of the barType1 of each row.
                for (int i = 2; i < RowsOfComb.Count; i++)
                {
                    //Initializes an empty array to hold the coordinates for the iteration row.
                    Y_Coordinates.Add(new double[RowsOfComb[i].GetLength(0)]);
                    //computes the basic coordinate of the iteration row by subtracting diameter of the largest bars from the current row, spacing and stirrup diameter.
                    basicCoordinate -= (StirrupDiameter + MinSpacing + RowsOfComb[i][0, 0]);
                    //Fills the coordinates of the iteration row.
                    FillRowY_Coordinates(RowsOfComb[i], Y_Coordinates[i], basicCoordinate, StirrupPosn);
                }
            }
        }
        /// <summary>
        /// Fills the y-coordinates of each of the barType1 based on the basic coordinate of the row passed. 
        /// </summary>
        /// <param name="Diameters">Contains the diameters in the first index of the second dimension for each bars represented by an index in the first dimension.</param>
        /// <param name="RowY_Coordinates">Array to be filled with the y coordinates in the same order as they appear in the diameters array.</param>
        /// <param name="BasicCoordinate">The common coordinate for a row.</param>
        /// <param name="StirrupPosition">Condition of the stirrup whether it is on top of the main barType1 or not.</param>
        private static void FillRowY_Coordinates(double[,] Diameters, double[] RowY_Coordinates, double BasicCoordinate,
            eRelativeStirrupPosition StirrupPosition = eRelativeStirrupPosition.StirrupAtBottom)
        {
            //Checks if the stirrup position is stirrup at the bottom.
            if (StirrupPosition == eRelativeStirrupPosition.StirrupAtBottom)
            {
                //Runs through the RowY_Coordinates filling the coordinates.
                for (int i = 0; i < RowY_Coordinates.Length; i++)
                {
                    RowY_Coordinates[i] = BasicCoordinate - Diameters[i, 0] / 2;
                }
            }
            else
            {   //Case stirrup position at the top of main bars
                //Runs through the RowY_Coordinates filling the coordinates.
                for (int i = 0; i < RowY_Coordinates.Length; i++)
                {
                    RowY_Coordinates[i] = BasicCoordinate + Diameters[i, 0] / 2;
                }
            }
        }
        #endregion

        #region Check For AllCombinationsCongested
        /// <summary>
        /// Checks if a combination causes congestion by comparing the minimum reinforcement fiber coordinate.
        /// </summary>
        /// <param name="RowsOfComb">Holds the collection of rows each represented by the diameters of the barType1 arranged in descending order.</param>
        /// <param name="Y_Coordinates">The y-coordinates of the barType1 specified in the same order as the diameters.</param>
        /// <param name="NeutralAxisDepth">The depth of neutral axis measured from the furthest compressive c fiber.</param>
        /// <param name="LevelOfCongestion">Returns the depth up to which the top of the rinforcement has extend.</param>
        /// <returns>Returns true if the combinatio is congested and false otherwise.</returns>
        private static bool IsCongested(List<double[,]> RowsOfComb, List<double[]> Y_Coordinates, double NeutralAxisDepth,out double LevelOfCongestion)
        {
            //Checks if the coordinate of the nearest reinforcement fiber to the origin is less than the neutral axis depth
            if ((Y_Coordinates[Y_Coordinates.Count - 1][0] - RowsOfComb[RowsOfComb.Count - 1][0, 0]) < NeutralAxisDepth)
            {   //Case when congested.
                LevelOfCongestion = Y_Coordinates[Y_Coordinates.Count - 1][0] - RowsOfComb[RowsOfComb.Count - 1][0, 0];
                return true;
            }
            else
            {   //Case when not congested.
                 LevelOfCongestion = Y_Coordinates[Y_Coordinates.Count - 1][0] - RowsOfComb[RowsOfComb.Count - 1][0, 0];
                return false;
            }
        }

        #endregion

        #region Final Cobination Generator

        /// <summary>
        /// Returns effective width for a given beam section.
        /// </summary>
        /// <param name="GrossWidth">Gross width of the beam.</param>
        /// <param name="StirupDiam">Diameter of stirrup used in the beam.</param>
        /// <param name="ConcretCover">Thickness of c cover used in the design.</param>
        /// <returns></returns>
        private static double GetEffectiveWidth(double GrossWidth, double StirupDiam, double ConcretCover)
        {
            // Claculates effective width as GrossWidth-2x(StirrupDiameter + ConcreteCover)
            return GrossWidth - 2 * (StirupDiam + ConcretCover);
        }

        /// <summary>
        /// Sortes given Diameters of barType1 in descending order. The calculation is performed by refference.
        /// </summary>
        /// <param name="Diameters">Diameters used as a Constraint.</param>
        private static void SortDescending(double[] Diameters)
        {
            double temp;// Temporary variable used to hold values before swap is done.

            // Iterates  the array for simple sort.
            for (int i = 0; i < Diameters.Length; i++)
            {
                for (int j = i + 1; j < Diameters.Length; j++)
                {
                    // if the element at one index less than the other it swaps the elements.
                    if (Diameters[i] < Diameters[j])
                    {
                        temp = Diameters[i];
                        Diameters[i] = Diameters[j];
                        Diameters[j] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Returns list of rows removing all barType1 with zero number of barType1 as an element.
        /// </summary>
        /// <param name="Rows">Rows of a combination on which diameters with zero elements to be removed.</param>
        /// <param name="ConstraintDiameters">Set of diameters from which the combination is going to be generated.</param>
        /// <returns></returns>
        private static List<double[,]> RemoveBarsWithZeroNumber(List<int[]> Rows, double[] ConstraintDiameters)
        {
            List<double[,]> result = new List<double[,]>(); //contains rows with diameter zero removed.
            List<int> nonZeroDiamNumbers = new List<int>();// contains number of non-zero diameter barType1.
            List<double> nonZeroDiam = new List<double>();// contains diameter of non-zero barType1.
            double[,] rowBars; // contains diameter and number of barType1 in a row only for bars with non-zero number.

            //Iterates in the row to remove zero bars number diameters.
            for (int i = 0; i < Rows.Count; i++)
            {
                for (int j = 0; j < ConstraintDiameters.Length; j++)
                {
                    //checks if the diamter is not zero.
                    if (Rows[i][j] != 0)
                    {
                        nonZeroDiamNumbers.Add(Rows[i][j]); // adds the number of barType1 to the list.
                        nonZeroDiam.Add(ConstraintDiameters[j]);// adds the the diameter to the list.
                    }
                }

                rowBars = new double[nonZeroDiam.Count, 2];// initialises the barType1 and their number in the row.

                //fills elements of RowBars from already filled lists
                for (int k = 0; k < rowBars.GetLength(0); k++)
                {
                    rowBars[k, 0] = nonZeroDiam[k];
                    rowBars[k, 1] = nonZeroDiamNumbers[k];
                }
                result.Add(rowBars); // add the row to the final row list which is going to be returned.
                nonZeroDiamNumbers = new List<int>(); // resets the list before getting to other row.
                nonZeroDiam = new List<double>();// resets the list before getting to other row.
            }
            return result;
        }

        /// <summary>
        /// Retursn the combination with larger effective depth from a given set of combinations.
        /// </summary>
        /// <param name="Combinations"></param>
        /// <returns></returns>
        public static eCombination GetMaxDepthCombination(List<eCombination> Combinations)
        {
            //Holds a value conbinations with the same effective depth.
            List<eCombination> combinations = new List<eCombination>();
            //hods the the maximum effective depth from the given combinations.
            double effectiveDepth = Combinations[0].EffectiveDepth;
            //holds the index of the maximum effective depth combination.
            int indexofMaxDepth = 0;

            //Searchs for combination with largest effective depth.
            for (int i = 1; i < Combinations.Count; i++)
            {
                if (effectiveDepth <= Combinations[i].EffectiveDepth)
                {
                    effectiveDepth = Combinations[i].EffectiveDepth;
                    indexofMaxDepth = i;
                }
            }
            return Combinations[indexofMaxDepth];
        }

        /// <summary>
        /// Returns the most economical arrangment of bars combination with all properties filled. Used when there is a risk of design failur[like doubly reinforced beam section.]
        /// </summary>
        /// <param name="AsCalculated">Calculated area of steel for strength.</param>
        /// <param name="ConstraintDiameters">Set of bars diameter user preferences from which the combinations are to be generate from.</param>
        /// <param name="AsMax">Maximum allowable area of steel from code requirment.</param>
        /// <param name="GrossWidth">Gross width of the beam.</param>
        /// <param name="GrossDepth">Gross depth of the beam.</param>
        /// <param name="NutralAxisDepth">Distance of nutrual axis from top compresion fiber.</param>
        /// <param name="MinSpacing">Minimum clear spacing between two barType1.</param>
        /// <param name="ConcreteCover">Thickness of c cover used in the design.</param>
        /// <param name="StirupDiameter">Diameter of stirrup used in the beam.</param>
        /// <param name="StirrupPosition">Orientation of the stirrup i.e. Stirrup at bottom or Stirrup at top of logtudinal bars.</param>
        /// <param name="DesignFailur">A delegate used to fire a failur event that is expected when the combinations are generated.</param>
        /// <returns></returns>
        internal static eCombination GetCombinations(double AsCalculated, double[] ConstraintDiameters, double AsMax, double GrossWidth, double GrossDepth, double NutralAxisDepth, double MinSpacing, double ConcreteCover, double StirupDiameter, eRelativeStirrupPosition StirrupPosition, eDesignFailurLink DesignFailur)
        {

            List<eCombination> finalCombinations = new List<eCombination>(); //Contains final list of combination to be returned.
            List<int[]> combinations = new List<int[]>(); //Contains all possible combinations generated from matematical consideration.
            List<double> areas = new List<double>(); // Contains areas of all possible combinations.
            List<eRow> finalRows = new List<eRow>(); //Contains all list of rows in a combination defining all requiredEffectiveDepth data about the row.
            List<int[]> rows = new List<int[]>(); // Contains all list of rows defining only diameter and number of barType1 used in the rows.
            List<double[]> Y_Coordinates = new List<double[]>(); //Contains list of Y-Coordinates for all diameters of barType1 row-wisly.
            List<double[,]> arrangedRows = new List<double[,]>(); //Contains list of arranged bars diameters with their number row-wisly. All zero number barType1 removed.
            double[,] X_Coordinates; // Contains X-Coordinates of each bars in a row.
            double effectiveWidth = GetEffectiveWidth(GrossWidth, StirupDiameter, ConcreteCover); //Contains the effective with of the beam.
            double levelOfCongestion;// Contains the depth up to which the top surface of the bars at the top of the row has extend.
            SortDescending(ConstraintDiameters);//Sortes the array in dessending order.

            //Call the GetCombination method and get all local variables filled by passing them as a parameter.
            GetCombinations(ConstraintDiameters, AsCalculated, AsMax, AsCalculated, combinations, areas, new int[ConstraintDiameters.Length]);

            //Iterates between the all possible arrangements untile most economical,symetrical and non-congested arrangement is reached.
            for (int i = 0; i < combinations.Count; i++)
            {
                do // Iterates for combinations with the same economi i.e. the same area.
                {
                    //Fills the rows of a combination only by considering minimum space requirment.
                    rows = GetRows(ConstraintDiameters, combinations[i], effectiveWidth, MinSpacing);
                    //Removes all zero elements in the row and assins it to arrangemts
                    arrangedRows = RemoveBarsWithZeroNumber(rows, ConstraintDiameters);
                    //Fills Y-coordinates by passing the local variables as a parameter.
                    FillCombinationY_Coordinates(arrangedRows, GrossDepth, ConcreteCover, StirupDiameter, MinSpacing, StirrupPosition, Y_Coordinates);
                    for (int k = 0; k < rows.Count; k++)
                    {
                        X_Coordinates = GetSymmetricArrangements(ConstraintDiameters, rows[k], effectiveWidth); // Fills X-Coordinates after arranged for symetry.
                        if ((finalCombinations.Count == 0) && (i == combinations.Count - 1) && (X_Coordinates == null))
                        {
                            //returns null if no fissible combination is found.
                            DesignFailur(new eDesignFailedEventArgs(0, 0, eFailureTypes.NoSymetricCombination));
                            return new eCombination();
                        }
                        //Checks if X_Coordinates are null.
                        if (X_Coordinates == null)
                        {
                            // If the X_Coordinates-coordinates are null it assumes them as non symetrical.
                            finalRows = new List<eRow>();
                            break;
                        }
                        finalRows.Add(new eRow(X_Coordinates, Y_Coordinates[k])); //Fills the completed row in rows of a combination.
                    }
                    //Checks if rows are not empty and the arrangment is not congested.
                    if ((finalRows.Count != 0) && !IsCongested(arrangedRows, Y_Coordinates, NutralAxisDepth, out levelOfCongestion))
                    {
                        //adds the rows in the combination.
                        finalCombinations.Add(new eCombination(finalRows));
                    }
                    if (IsCongested(arrangedRows, Y_Coordinates, NutralAxisDepth, out levelOfCongestion) && (i == combinations.Count - 1))
                    {
                        DesignFailur(new eDesignFailedEventArgs(GrossDepth, GrossDepth + NutralAxisDepth - levelOfCongestion, eFailureTypes.AllCombinationsCongested));
                        return new eCombination();
                    }
                    // Checks if  the iteration is finished and no combinations is found.               
                    finalRows = new List<eRow>(); // resets the rows after completions of one combination.
                    Y_Coordinates = new List<double[]>(); //resets the y-coordinates after the completion of thecombination.
                    i++; // iterates to the next combination.
                    //Checks if the cobination is finished or if there is no next element.
                    if (i == combinations.Count)
                        break;
                } while (Math.Round(areas[i - 1], 6) == Math.Round(areas[i], 6));

                //Checks if fissible ,economical, non-congested and semetrical combination is found
                if (finalCombinations.Count != 0)
                {
                    // if combinations is found it will break the iteration for other less economical arrangements.
                    break;
                }
            }

            if ((finalCombinations == null) || (finalCombinations.Count == 0))
            {
                //returns null if no combination is found.
                return new eCombination();
            }
            else
            { 
                // returns combinations only having larger effective depth.
                return GetMaxDepthCombination(finalCombinations);
            }          
        }
      
        /// <summary>
        /// Returns the most economical arrangment of bars combination with all properties filled and when there is no risk of design failur[like singly reinforced beam].
        /// </summary>
        /// <param name="AsCalculated">Calculated area of steel for strength.</param>
        /// <param name="ConstraintDiameters">Set of bars diameter user preferences from which the combinations are to be generate from.</param>
        /// <param name="AsMax">Maximum allowable area of steel from code requirment.</param>
        /// <param name="GrossWidth">Gross width of the beam.</param>
        /// <param name="GrossDepth">Gross depth of the beam.</param>
        /// <param name="NutralAxisDepth">Distance of nutrual axis from top compresion fiber.</param>
        /// <param name="MinSpacing">Minimum clear spacing between two barType1.</param>
        /// <param name="ConcreteCover">Thickness of c cover used in the design.</param>
        /// <param name="StirupDiameter">Diameter of stirrup used in the beam.</param>
        /// <param name="StirrupPosition">Orientation of the stirrup i.e. Stirrup at bottom or Stirrup at top of logtudinal bars.</param>
        /// <returns></returns>
        public static eCombination GetCombinations(double AsCalculated, double[] ConstraintDiameters, double AsMax, double GrossWidth, double GrossDepth, double NutralAxisDepth, double MinSpacing, double ConcreteCover, double StirupDiameter, eRelativeStirrupPosition StirrupPosition)
        {

            List<eCombination> finalCombinations = new List<eCombination>(); //Contains final list of combination to be returned.
            List<int[]> combinations = new List<int[]>(); //Contains all possible combinations generated from matematical consideration.
            List<double> areas = new List<double>(); // Contains areas of all possible combinations.
            List<eRow> finalRows = new List<eRow>(); //Contains all list of rows in a combination defining all requiredEffectiveDepth data about the row.
            List<int[]> rows = new List<int[]>(); // Contains all list of rows defining only diameter and number of barType1 used in the rows.
            List<double[]> Y_Coordinates = new List<double[]>(); //Contains list of Y-Coordinates for all diameters of barType1 row-wisly.
            List<double[,]> arrangedRows = new List<double[,]>(); //Contains list of arranged bars diameters with their number row-wisly. All zero number barType1 removed.
            double[,] X_Coordinates; // Contains X-Coordinates of each bars in a row.
            double effectiveWidth = GetEffectiveWidth(GrossWidth, StirupDiameter, ConcreteCover); //Contains the effective with of the beam.
            double levelOfCongestion;// Contains the depth up to which the top surface of the bars at the top of the row has extend.
            SortDescending(ConstraintDiameters);//Sortes the array in dessending order.

            //Call the GetCombination method and get all local variables filled by passing them as a parameter.
            GetCombinations(ConstraintDiameters, AsCalculated, AsMax, AsCalculated, combinations, areas, new int[ConstraintDiameters.Length]);

            //Iterates between the all possible arrangements untile most economical,symetrical and non-congested arrangement is reached.
            for (int i = 0; i < combinations.Count; i++)
            {
                do // Iterates for combinations with the same economi i.e. the same area.
                {
                    //Fills the rows of a combination only by considering minimum space requirment.
                    rows = GetRows(ConstraintDiameters, combinations[i], effectiveWidth, MinSpacing);
                    //Removes all zero elements in the row and assins it to arrangemts
                    arrangedRows = RemoveBarsWithZeroNumber(rows, ConstraintDiameters);
                    //Fills Y-coordinates by passing the local variables as a parameter.
                    FillCombinationY_Coordinates(arrangedRows, GrossDepth, ConcreteCover, StirupDiameter, MinSpacing, StirrupPosition, Y_Coordinates);
                    for (int k = 0; k < rows.Count; k++)
                    {
                        X_Coordinates = GetSymmetricArrangements(ConstraintDiameters, rows[k], effectiveWidth); // Fills X-Coordinates after arranged for symetry.
                        if ((finalCombinations.Count == 0) && (i == combinations.Count - 1) && (X_Coordinates == null))
                        {
                            //returns null if no fissible combination is found.
                            return new eCombination();
                        }
                        //Checks if X_Coordinates are null.
                        if (X_Coordinates == null)
                        {
                            // If the X_Coordinates-coordinates are null it assumes them as non symetrical.
                            finalRows = new List<eRow>();
                            break;
                        }
                        finalRows.Add(new eRow(X_Coordinates, Y_Coordinates[k])); //Fills the completed row in rows of a combination.
                    }
                    //Checks if rows are not empty and the arrangment is not congested.
                    if ((finalRows.Count != 0) && !IsCongested(arrangedRows, Y_Coordinates, NutralAxisDepth, out levelOfCongestion))
                    {
                        //adds the rows in the combination.
                        finalCombinations.Add(new eCombination(finalRows));
                    }
                    if (IsCongested(arrangedRows, Y_Coordinates, NutralAxisDepth, out levelOfCongestion) && (i == combinations.Count - 1))
                    {
                        return new eCombination();
                    }
                    // Checks if  the iteration is finished and no combinations is found.               
                    finalRows = new List<eRow>(); // resets the rows after completions of one combination.
                    Y_Coordinates = new List<double[]>(); //resets the y-coordinates after the completion of thecombination.
                    i++; // iterates to the next combination.
                    //Checks if the cobination is finished or if there is no next element.
                    if (i == combinations.Count)
                        break;
                } while (Math.Round(areas[i - 1], 6) == Math.Round(areas[i], 6));

                //Checks if fissible ,economical, non-congested and semetrical combination is found
                if (finalCombinations.Count != 0)
                {
                    // if combinations is found it will break the iteration for other less economical arrangements.
                    break;
                }
            }

            if ((finalCombinations == null) || (finalCombinations.Count == 0))
            {
                //returns null if no combination is found.
                return new eCombination();
            }
            else
            {
                // returns combinations only having larger effective depth.
                return GetMaxDepthCombination(finalCombinations);
            }
        }

        #endregion;
    }
}

