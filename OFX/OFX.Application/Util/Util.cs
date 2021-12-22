using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace OFX.Application.Util
{
    public class Util
    {
        private const int MAX_VALID_YR = 9999;
        private const int MIN_VALID_YR = 1800;

        /// <summary>
        /// Returns true if given year is valid. 
        /// </summary>
        /// <param name="year">year</param>
        /// <returns></returns>
        private static bool isLeap(int year)
        {

            // Return true if year is a
            // multiple of 4 and not
            // multiple of 100. OR year
            // is multiple of 400.
            return (((year % 4 == 0) &&
                     (year % 100 != 0)) ||
                     (year % 400 == 0));
        }

        // Returns true if given
        // year is valid or not.
        /// <summary>
        /// Returns true if given year is valid or not
        /// </summary>
        /// <param name="d">day</param>
        /// <param name="m">month</param>
        /// <param name="y">year</param>
        /// <returns></returns>
        public static bool isValidDate(ref int d,
                                        int m,
                                        int y)
        {

            // If year, month and day
            // are not in given range
            if (y > MAX_VALID_YR ||
                y < MIN_VALID_YR)
                return false;
            if (m < 1 || m > 12)
                return false;
            if (d < 1 || d > 31)
                return false;

            // Handle February month
            // with leap year
            if (m == 2)
            {
                if (isLeap(y))
                {
                    if (d <= 29)
                    {
                        return true;
                    }
                    else
                    {
                        d -= (d - 29);
                    }
                }
                else
                {
                    if (d <= 28)
                    {
                        return true;
                    }
                    {
                        d -= (d - 28);
                    }
                }
            }

            // Months of April, June,
            // Sept and Nov must have
            // number of days less than
            // or equal to 30.
            if (m == 4 || m == 6 ||
                m == 9 || m == 11)
                return (d <= 30);

            return true;
        }


    }
}
