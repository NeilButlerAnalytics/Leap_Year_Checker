using System;
using System.IO;
using System.Text.Json;

public class LeapYearChecker
{
    public static void Main()
    {
        int currentYear = DateTime.Now.Year;
        int leapYearCount = 0;

        for (int year = 1; year <= currentYear; year++)
        {
            // If it is decided based on the calculation that the year is a leap year then increment our counter variable
            string isLeapYear = IsLeapYear(year) ? "Yes" : "No";
            if (isLeapYear == "Yes")
                leapYearCount++;
        }

        // Output the total number of leap years found to the console
        Console.WriteLine($"Total number of leap years found: {leapYearCount}");
    }

    // Function to determine if a year is a leap year
    private static bool IsLeapYear(int year)
    {
        /********** Calculation to check whether a year is a leap year **********/
        // the initial check 'year % 4 == 0' checks to see if the year is divisable by 4 without leaving a remainder
        // the next check 'year % 100 != 0' checks if the year is not divisible by 100 without leaving a remainder.
        // This condition ensures that the year is not a multiple of 100, except when it's also divisible by 400.
        // The final condition 'year % 400 == 0' checks if the year is divisible by 400 without leaving a remainder.
        // This condition accounts for the exception to the rule of not being divisible by 100,
        // ensuring that certain years divisible by 400 are still considered leap years.

        if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
            return true;
        else
            return false;
    }
}
