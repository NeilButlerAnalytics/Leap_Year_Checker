using NUnit.Framework;

[TestFixture]
public class LeapYearCheckerTests
{
    [TestCase(2000, true)]
    [TestCase(2004, true)]
    [TestCase(1900, false)]
    [TestCase(2001, false)]
    [TestCase(2020, true)]
    [TestCase(2021, false)]
    [TestCase(1844, true)]
    [TestCase(1850, false)]
    public void IsLeapYear_ValidYear(int year, bool expected)
    {
        // Act
        bool result = LeapYearChecker.IsLeapYear(year);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    // Check if the script calculates the number of leap years the same as human understanding
    public void IsLeapYear_sectionedCheckToConfirmExpectedResult()
    {
        // Arrange
        int startYear = 2000;
        int endYear = 2020;
        int expectedLeapYearCount = 6; // leap years: 2000, 2004, 2008, 2012, 2016, 2020

        // Act
        int leapYearCount = CountLeapYearsInRange(startYear, endYear);

        // Assert
        Assert.That(leapYearCount, Is.EqualTo(expectedLeapYearCount));
    }

    [Test]
    public void IsLeapYear_NegativeYearReturnsFalse()
    {
        // Arrange
        int negativeYear = -100;

        // Act
        bool result = LeapYearChecker.IsLeapYear(negativeYear);

        // Assert
        Assert.IsFalse(result);
    }

    private int CountLeapYearsInRange(int startYear, int endYear)
    {
        int leapYearCount = 0;
        for (int year = startYear; year <= endYear; year++)
        {
            if (LeapYearChecker.IsLeapYear(year))
                leapYearCount++;
        }
        return leapYearCount;
    }

    [Test]
    // We have tested leap years against human calculation, now check non leap years
    public void IsLeapYear_CountNonLeapYears()
    {
        // Arrange
        int startYear = 1901;
        int endYear = 2000;
        int expectedNonLeapYearCount = 75; // non-leap years: 1901-2000 excluding 1900

        // Act
        int nonLeapYearCount = CountNonLeapYearsInRange(startYear, endYear);

        // Assert
        Assert.That(nonLeapYearCount, Is.EqualTo(expectedNonLeapYearCount));
    }
    // Check if the same test can also return a FALSE
    public void IsLeapYear_CountNonLeapYears_FALSE()
    {
        // Arrange
        int startYear = 1901;
        int endYear = 2000;
        int expectedNonLeapYearCount = 80; // non-leap years: 1901-2000 excluding 1900

        // Act
        int nonLeapYearCount = CountNonLeapYearsInRange(startYear, endYear);

        // Assert
        Assert.That(nonLeapYearCount, Is.EqualTo(expectedNonLeapYearCount));
    }

    private int CountNonLeapYearsInRange(int startYear, int endYear)
    {
        int nonLeapYearCount = 0;
        for (int year = startYear; year <= endYear; year++)
        {
            if (!LeapYearChecker.IsLeapYear(year))
                nonLeapYearCount++;
        }
        return nonLeapYearCount;
    }
}