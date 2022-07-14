using PersonApp.CsvParsers;
using PersonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        for (int i=0; i<result.Count; i++)
        {
            result[i].Should().BeEquivalentTo(expectedResult[i]);
        }
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
            }
        };
    }
}
