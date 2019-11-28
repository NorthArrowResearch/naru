using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace naru.math
{
    public static class Statistics
    {
        /// <summary>
        /// Returns the mean for a list of values
        /// </summary>
        /// <param name="dValues">List of doudbles</param>
        /// <param name="nCount">Return value specifying the number of elements that were used to generate the count</param>
        /// <param name="bIgnoreNeg9999">True indicates that values in the dictionary of -9999 should be ignored</param>
        /// <returns>Statistical mean, or zero if there are no values in the list</returns>
        /// <remarks></remarks>
        public static double Mean(Dictionary<double, double>.ValueCollection dValues, out int nCount, bool bIgnoreNeg9999 = true)
        {
            if (dValues is Dictionary<double, double>.ValueCollection)
            {
                if (dValues.Count < 1)
                {
                    return 0;
                }
            }
            else
            {
                throw new Exception("Invalid list of values passed as argument.");
            }

            nCount = 0;
            double fMean = 0;
            foreach (double value in dValues)
            {
                if (!bIgnoreNeg9999 || (bIgnoreNeg9999 && value > -9998))
                {
                    fMean += value;
                    nCount += 1;
                }
            }

            if (fMean != 0 && nCount != 0)
            {
                fMean = fMean / nCount;
            }
            else
            {
                fMean = 0;
                nCount = 0;
            }

            return fMean;
        }

        /// <summary>
        /// Precision as an Integer. Positive numbers are decimals of 10. Negative numbers mean 0.1 0.01 etc.
        /// </summary>
        /// <param name="fVal"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static int GetPrecision(double fVal)
        {
            string numberAsString = fVal.ToString();
            int indexOfDecimalPoint = numberAsString.IndexOf(".");

            if ((fVal == 0))
            {
                return 0;
            }
            //Decimal place
            if ((indexOfDecimalPoint != -1))
            {
                int numberOfDecimals = numberAsString.Substring(indexOfDecimalPoint + 1).Length;
                // Small decimal points are rare so throw out a warning.
                if ((numberOfDecimals > 10))
                {
                    Debug.WriteLine(string.Format("WARNING: Possible Double/Float precision problem. Number: {0}", numberAsString));
                }
                return -(numberOfDecimals);
            }
            else
            {
                // No decimal found. We're into powers of 10 here.
                dynamic numberOfTens = 0;
                while (numberAsString[numberAsString.Length - numberOfTens - 1] == "0")
                {
                    numberOfTens += 1;
                }
                return numberOfTens;
            }
        }

        public static double FilteredMean(Dictionary<double, double>.ValueCollection dValues, double fStdDevThreshold, out int nCount, bool bIgnoreNeg9999 = true)
        {
            if (dValues is Dictionary<double, double>.ValueCollection)
            {
                if (dValues.Count < 1)
                {
                    return 0;
                }
            }
            else
            {
                throw new Exception("Invalid list of values passed as argument.");
            }

            if (fStdDevThreshold <= 0)
            {
                throw new Exception("Invalid threshold provided.");
            }
            //
            // Get the mean and standard deviation of all the records in the dictionary
            //
            double fFullMean = Mean(dValues, out nCount);
            dynamic fFullStdDev = StandardDeviation(dValues, fFullMean);
            nCount = 0;
            double fFilteredMean = 0;
            double fDifferenceThreshold = (fStdDevThreshold * fFullStdDev);

            foreach (double value in dValues)
            {
                if (Math.Abs(value - fFullMean) < fDifferenceThreshold)
                {
                    if (!bIgnoreNeg9999 || (bIgnoreNeg9999 && value > -9998))
                    {
                        fFilteredMean += value;
                        nCount += 1;
                    }
                }
            }

            if (fFilteredMean != 0 && nCount != 0)
            {
                fFilteredMean = fFilteredMean / nCount;
            }
            else
            {
                fFilteredMean = 0;
                nCount = 0;
            }

            return fFilteredMean;

        }

        /// <summary>
        /// Calculate the standard deviation of the filtered set of values
        /// </summary>
        /// <param name="dValues">Full list of values</param>
        /// <param name="fStdDevThreshold">The number of standard deviations beyond the full mean that will be excluded from the calculation</param>
        /// <param name="fFilteredMean">The filtered mean value. i.e. the mean of the values that are within the threshold of the filtered mean.</param>
        /// <returns>Filtered standard deviation</returns>
        /// <remarks>This method first calculates the full mean and standard deviation. It then calculates the threshold to exclude values. This
        /// is a certain number of standard deviations from the mean. The filtered standard deviation is then calculated for just those values that
        /// are within the threshold of the full mean.</remarks>
        public static double FilteredStandardDeviation(Dictionary<double, double>.ValueCollection dValues, double fStdDevThreshold, double fFilteredMean, bool bIgnoreNeg9999 = true)
        {

            if (dValues is Dictionary<double, double>.ValueCollection)
            {
                if (dValues.Count < 1)
                {
                    return 0;
                }
            }
            else
            {
                throw new Exception("Invalid list of values passed as argument.");
            }

            if (fStdDevThreshold <= 0)
            {
                throw new Exception("Invalid threshold provided.");
            }

            int nCount = 0;
            double fFullMean = Mean(dValues, out nCount);
            double fFullStdDev = StandardDeviation(dValues, fFullMean);
            double fDifferenceThreshold = (fStdDevThreshold * fFullStdDev);

            double fDifference = 0;
            nCount = 0;
            foreach (double value in dValues)
            {
                if (Math.Abs(value - fFullMean) < fDifferenceThreshold)
                {
                    if (!bIgnoreNeg9999 || (bIgnoreNeg9999 && value > -9998))
                    {
                        fDifference += Math.Pow(fFilteredMean - value, 2);
                        nCount += 1;
                    }
                }
            }

            if (fDifference > 0 && nCount != 0)
            {
                fDifference = fDifference / nCount;
                fDifference = Math.Sqrt(fDifference);
            }
            else
            {
                fDifference = 0;
            }

            return fDifference;
        }

        /// <summary>
        /// Overloaded standard deviation calculation. See other method for comments.
        /// </summary>
        /// <param name="dValues"></param>
        /// <param name="fStdDevThreshold"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double FilteredStandardDeviation(Dictionary<double, double>.ValueCollection dValues, double fStdDevThreshold, out int nFilteredCount)
        {
            int nCount = 0;
            double fFilteredMean = FilteredMean(dValues, fStdDevThreshold, out nCount);
            return FilteredStandardDeviation(dValues, fStdDevThreshold, nFilteredCount);
        }

        /// <summary>
        /// Statistical standard deviation
        /// </summary>
        /// <param name="dValues">List of double values</param>
        /// <param name="fMean">The mean of the values</param>
        /// <returns>The statistical standard deviation or zero if there are no values in the list</returns>
        /// <remarks>http://en.wikipedia.org/wiki/Standard_deviation</remarks>
        public static double StandardDeviation(Dictionary<double, double>.ValueCollection dValues, double fMean)
        {
            if (dValues is Dictionary<double, double>.ValueCollection)
            {
                if (dValues.Count < 1)
                {
                    return 0;
                }
            }
            else
            {
                throw new Exception("Invalid list of values passed as argument.");
            }

            double fDifference = 0;
            int nCount = 0;

            foreach (double value in dValues)
            {
                fDifference += Math.Pow(fMean - value, 2);
                nCount += 1;
            }

            if (fDifference > 0 && nCount != 0)
            {
                fDifference = fDifference / nCount;
                fDifference = Math.Sqrt(fDifference);
            }
            else
            {
                fDifference = 0;
            }

            return fDifference;
        }

        /// <summary>
        /// Statistical standard deviation
        /// </summary>
        /// <param name="dValues">List of double values</param>
        /// <returns>Statistical standard deviation or zero if there are no values in the list</returns>
        /// <remarks>Calling this overloaded version will pre-calculate the mean for you.</remarks>
        public static double StandardDeviation(Dictionary<double, double>.ValueCollection dValues)
        {
            int nCount = 0;
            double fMean = Mean(dValues, out nCount);
            return StandardDeviation(dValues, fMean);
        }

        public static double CoefficientOfVariation(Dictionary<double, double>.ValueCollection dValues)
        {
            int nCount = 0;
            double fMean = Mean(dValues, out nCount);
            double fStandardDeviation = StandardDeviation(dValues, fMean);

            double fCV = 0;
            if (fMean != 0 && fStandardDeviation != 0)
            {
                fCV = (fStandardDeviation / fMean);
            }

            return fCV;
        }

        public static double CoefficientOfVariation(double fStandardDeviation, double fMean)
        {
            double fCV = 0;

            if (fMean != 0 && fStandardDeviation != 0)
            {
                fCV = (fStandardDeviation / fMean);
            }

            return fCV;
        }
    }
}