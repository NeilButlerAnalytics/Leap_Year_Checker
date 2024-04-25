using NUnit.Framework;
using static LeapYearChecker;

[TestFixture]
public class LeapYearCheckerTests
{

    private string csvFilePath = "test_leap_years.csv"; // Path for the test CSV file
    private string jsonFilePath = "test_leap_years.json"; // Path for the test JSON file

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
    public void CSVFileCreator_ConfirmFileAndHeadings()
    {
        // Arrange
        var csvFileCreator = new CSVFileCreator(csvFilePath);
        csvFileCreator.Close();
        // Assert
        Assert.IsTrue(File.Exists(csvFilePath), "CSV file was not created.");

        // Read the first line of the CSV file
        string? firstLine = File.ReadLines(csvFilePath).FirstOrDefault();

        // Assert
        Assert.That(firstLine, Is.EqualTo("Year,LeapYear"), "CSV file does not contain the correct header.");
    }
    [Test]
    public void CSVFileCreator_CreateFileWithData()
    {
        // Arrange
        var csvFileCreator = new CSVFileCreator(csvFilePath);

        // Act
        csvFileCreator.WriteToFile("2000,Yes");
        csvFileCreator.WriteToFile("2001,No");
        csvFileCreator.Close();

        // Assert
        string[] lines = File.ReadAllLines(csvFilePath);

        // Check to see if the file has been created with the expected data, the headers and the 2 data rows we have manually included
        Assert.That(lines.Length, Is.EqualTo(3), "Does not contain the expected number of rows.");
        // Check the first line is the header
        Assert.That(lines[0], Is.EqualTo("Year,LeapYear"), "Does not contain the correct header.");
        // Check the next two lines contain the data we specified above
        Assert.That(lines[1], Is.EqualTo("2000,Yes"), "Does not contain the expected data in row 1.");
        Assert.That(lines[2], Is.EqualTo("2001,No"), "Does not contain the expected data in row 2.");
    }

    [Test]
    public void JSONFileCreator_CreatesFileWithCorrectFormat()
    {
        // Arrange
        var jsonFileCreator = new JSONFileCreator(jsonFilePath);

        // Act
        jsonFileCreator.WriteToFile(new { Year = 1, LeapYear = "No" });
        jsonFileCreator.Close();

        // Assert
        Assert.IsTrue(File.Exists(jsonFilePath), "JSON file was not created.");

        string[] lines = File.ReadAllLines(jsonFilePath);

        // Check if the file starts with '[' and ends with ']'
        Assert.That(lines.First(), Is.EqualTo("["), "JSON file does not start with '['.");
        Assert.That(lines.Last(), Is.EqualTo("]"), "JSON file does not end with ']'.");

        // Check if the content is formatted correctly
        string content = string.Join("", lines.Skip(1).Take(lines.Length - 2));
        string expectedContent = @"{""Year"":1,""LeapYear"":""No""},";
        Assert.That(content, Is.EqualTo(expectedContent), "JSON file content is not formatted correctly.");
    }

    [Test]
    public void JSONFileCreator_CreatesFileWithDataForMultipleYears()
    {
        // Arrange
        var jsonFileCreator = new JSONFileCreator(jsonFilePath);

        // Act
        jsonFileCreator.WriteToFile(new { Year = 1, LeapYear = "No" });
        jsonFileCreator.WriteToFile(new { Year = 2, LeapYear = "No" });
        jsonFileCreator.Close();

        // Assert
        Assert.IsTrue(File.Exists(jsonFilePath), "JSON file was not created.");

        string[] lines = File.ReadAllLines(jsonFilePath);

        // Check if the file starts with '[' and ends with ']'
        Assert.That(lines.First(), Is.EqualTo("["), "JSON file does not start with '['.");
        Assert.That(lines.Last(), Is.EqualTo("]"), "JSON file does not end with ']'.");

        // Check if the content is formatted correctly
        string content = string.Join("", lines.Skip(1).Take(lines.Length - 2));
        string expectedContent = @"{""Year"":1,""LeapYear"":""No""},{""Year"":2,""LeapYear"":""No""},";
        Assert.That(content, Is.EqualTo(expectedContent), "JSON file content is not formatted correctly.");
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