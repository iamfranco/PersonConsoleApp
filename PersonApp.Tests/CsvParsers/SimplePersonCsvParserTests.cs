using PersonApp.CsvParsers;
using PersonApp.Models;

namespace PersonApp.Tests.CsvParsers;
internal class SimplePersonCsvParserTests
{
    private IPersonCsvParser _personCsvParser;

    [SetUp]
    public void Setup()
    {
        // Arrange
        _personCsvParser = new SimplePersonCsvParser();
    }

    [Test]
    public void Parse_With_Invalid_FilePath_Should_Throw_FileNotFoundException()
    {
        // Arrange
        string currentDirectory = Directory.GetCurrentDirectory();
        string filePath = currentDirectory + @"\CsvParsers\InputFile\testInputNonExistent.csv";

        // Act
        Action act = () => _personCsvParser.Parse(filePath);

        // Assert
        act.Should().Throw<FileNotFoundException>()
            .WithMessage("No file found matching filepath");
    }

    [Test]
    public void Parse_With_FilePath_TestInputCsv_Should_Return_Correct_List_Of_People()
    {
        // Arrange
        string currentDirectory = Directory.GetCurrentDirectory();
        string filePath = currentDirectory + @"\CsvParsers\InputFile\testInput.csv";

        List<Person> expectedResult = GetTestInputPeople();

        // Act
        List<Person> result = _personCsvParser.Parse(filePath);

        // Assert
        result.Count.Should().Be(expectedResult.Count);
        for (int i = 0; i < result.Count; i++)
        {
            result[i].Should().BeEquivalentTo(expectedResult[i]);
        }
    }

    [Test]
    public void Parse_BlankFile_Should_Throw_Exception()
    {
        // Arrange
        string currentDirectory = Directory.GetCurrentDirectory();
        string filePath = currentDirectory + @"\CsvParsers\InputFile\testInputBlankFile.csv";

        // Act
        Action act = () => _personCsvParser.Parse(filePath);

        // Assert
        act.Should().Throw<Exception>()
            .WithMessage("File has less than 2 lines, not suitable for loading list of people");
    }

    [Test]
    public void Parse_CsvFile_With_CorrectHeader_But_MissingContent_Should_Throw_Exception()
    {
        // Arrange
        string currentDirectory = Directory.GetCurrentDirectory();
        string filePath = currentDirectory + @"\CsvParsers\InputFile\testInputCorrectHeaderMissingContent.csv";

        // Act
        Action act = () => _personCsvParser.Parse(filePath);

        // Assert
        act.Should().Throw<Exception>()
            .WithMessage("file is incomplete (has missing fields in main content)");
    }

    [Test]
    public void Parse_LoremIpsumFile_Should_Throw_KeyNotFoundException()
    {
        // Arrange
        string currentDirectory = Directory.GetCurrentDirectory();
        string filePath = currentDirectory + @"\CsvParsers\InputFile\testInputLoremIpsum.csv";

        // Act
        Action act = () => _personCsvParser.Parse(filePath);

        // Assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void Parse_CsvFile_With_Wrong_Headers_Should_Throw_KeyNotFoundException()
    {
        // Arrange
        string currentDirectory = Directory.GetCurrentDirectory();
        string filePath = currentDirectory + @"\CsvParsers\InputFile\testInputWrongHeaders.csv";

        // Act
        Action act = () => _personCsvParser.Parse(filePath);

        // Assert
        act.Should().Throw<KeyNotFoundException>();
    }

    private List<Person> GetTestInputPeople()
    {
        return new()
        {
            new()
            {
                FirstName = "Ulysses",
                LastName = "Mcwalters",
                Company = "Mcmahan, Ben L",
                Address = "505 Exeter Rd",
                City = "Hawerby cum Beesby",
                County = "Lincolnshire",
                Postal = "DN36 5RP",
                Phone1 = "01912-771311",
                Phone2 = "01302-601380",
                Email = "ulysses@hotmail.com",
                Web = "http://www.mcmahanbenl.co.uk"
            },
            new()
            {
                FirstName = "Tyisha",
                LastName = "Veness",
                Company = "Champagne Room",
                Address = "5396 Forth Street",
                City = "Greets Green and Lyng Ward",
                County = "West Midlands",
                Postal = "B70 9DT",
                Phone1 = "01547-429341",
                Phone2 = "01290-367248",
                Email = "tyisha.veness@hotmail.com",
                Web = "http://www.champagneroom.co.uk"
            },
            new()
            {
                FirstName = "Eric",
                LastName = "Rampy",
                Company = "Thompson, Michael C Esq",
                Address = "9472 Lind St",
                City = "Desborough",
                County = "Northamptonshire",
                Postal = "NN14 2GH",
                Phone1 = "01969-886290",
                Phone2 = "01545-817375",
                Email = "erampy@rampy.co.uk",
                Web = "http://www.thompsonmichaelcesq.co.uk"
            },
            new()
            {
                FirstName = "Jules",
                LastName = "Hiltner",
                Company = "Benitez, Brigida Esq",
                Address = "5 Howe St",
                City = "Broxburn, Uphall and Winchburg",
                County = "West Lothian",
                Postal = "EH52 6NF",
                Phone1 = "01428-343825",
                Phone2 = "01814-878359",
                Email = "jules@yahoo.com",
                Web = "http://www.benitezbrigidaesq.co.uk"
            }
        };
    }
}
