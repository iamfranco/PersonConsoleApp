using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Moq;
using PersonApp.Contexts;
using PersonApp.Controllers;
using PersonApp.CsvParsers;
using PersonApp.Models;

namespace PersonApp.Tests.Controllers;
internal class PersonControllerTests
{
    private Mock<PersonContextBase> _mockPersonContext;
    private Mock<IPersonCsvParser> _mockPersonCsvParser;
    private PersonController _personController;

    [SetUp]
    public void Setup()
    {
        // Arrange
        _mockPersonContext = new Mock<PersonContextBase>();
        _mockPersonCsvParser = new Mock<IPersonCsvParser>();

        _personController = new PersonController(_mockPersonContext.Object, _mockPersonCsvParser.Object);
    }

    [Test]
    public void Constructor_With_Null_PersonContext_Should_Throw_Null_Exception()
    {
        // Act
        Action act = () => _personController = new PersonController(null, _mockPersonCsvParser.Object);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("personContext should not be null");
    }

    [Test]
    public void Constructor_With_Null_PersonCsvParser_Should_Throw_Null_Exception()
    {
        // Act
        Action act = () => _personController = new PersonController(_mockPersonContext.Object, null);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("personCsvParser should not be null");
    }

    [Test]
    public void LoadPeopleFromCsvFile_With_Null_FilePath_String_Should_Throw_Null_Exception()
    {
        // Arrange
        string filePath = null;

        // Act
        Action act = () => _personController.LoadPeopleFromCsvFile(filePath);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("filePath should not be null");
    }

    [Test]
    public void LoadPeopleFromCsvFile_With_Valid_FilePath_ToCsvFile_Should_Invoke_PersonCsvParser_Parse_And_PersonContext_AddPeople()
    {
        // Arrange
        string filePath = @"C:\Users\...\input.csv";
        List<Person> people = GetListOfPeople();

        _mockPersonCsvParser.Setup(p => p.Parse(filePath))
            .Returns(people);

        _mockPersonContext.Setup(p => p.AddPeople(people))
            .Verifiable();

        // Act
        _personController.LoadPeopleFromCsvFile(filePath);

        // Assert
        _mockPersonCsvParser.Verify(p => p.Parse(filePath), Times.Once);
        _mockPersonContext.Verify(p => p.AddPeople(people), Times.Once);
    }

    [Test]
    public void LoadPeopleFromCsvFile_With_Invalid_FilePath_With_PersonCsvParser_Throwing_Exception_Should_Throw_Same_Exception()
    {
        // Arrange
        string filePath = @"C:\Users\...\invalid\path\input.csv";

        _mockPersonCsvParser.Setup(p => p.Parse(filePath))
            .Throws<FileNotFoundException>();

        // Act
        Action act = () => _personController.LoadPeopleFromCsvFile(filePath);

        // Assert
        act.Should().Throw<FileNotFoundException>();
    }

    [Test]
    public void LoadPeopleFromCsvFile_With_Invalid_Csv_Format_With_PersonCsvParser_Throwing_Exception_Should_Throw_Same_Exception()
    {
        // Arrange
        string filePath = @"C:\Users\...\inputInvalidCsv.csv";

        _mockPersonCsvParser.Setup(p => p.Parse(filePath))
            .Throws<Exception>();

        // Act
        Action act = () => _personController.LoadPeopleFromCsvFile(filePath);

        // Assert
        act.Should().Throw<Exception>();
    }

    [Test]
    public void GetPeopleWithCompanyNameContainingEsq_Should_Return_Correct_List_Of_People()
    {
        // Arrange
        List<Person> people = GetListOfPeople();

        _mockPersonContext.Setup(p => p.People)
            .Returns(people);

        List<Person> expectedResult = new() { people[2] };

        // Act
        List<Person> result = _personController.GetPeopleWithCompanyNameContainingEsq();

        // Assert
        result.Should().Equal(expectedResult);
    }

    [Test]
    public void GetPeopleWithCountyDerbyshire_Should_Return_Correct_List_Of_People()
    {
        // Arrange
        List<Person> people = GetListOfPeople();

        _mockPersonContext.Setup(p => p.People)
            .Returns(people);

        List<Person> expectedResult = new() { people[1] };

        // Act
        List<Person> result = _personController.GetPeopleWithCountyDerbyshire();

        // Assert
        result.Should().Equal(expectedResult);
    }

    [Test]
    public void GetPeopleWithThreeDigitHouseNumber_Should_Return_Correct_List_Of_People()
    {
        // Arrange
        List<Person> people = GetListOfPeople();

        _mockPersonContext.Setup(p => p.People)
            .Returns(people);

        List<Person> expectedResult = new() { people[2] };

        // Act
        List<Person> result = _personController.GetPeopleWithThreeDigitHouseNumber();

        // Assert
        result.Should().Equal(expectedResult);
    }

    [Test]
    public void GetPeopleWithUrlLongerThan35Characters_Should_Return_Correct_List_Of_People()
    {
        // Arrange
        List<Person> people = GetListOfPeople();

        _mockPersonContext.Setup(p => p.People)
            .Returns(people);

        List<Person> expectedResult = new() { people[0] };

        // Act
        List<Person> result = _personController.GetPeopleWithUrlLongerThan35Characters();

        // Assert
        result.Should().Equal(expectedResult);
    }

    [Test]
    public void GetPeopleWithSingleDigitPostcodeArea_Should_Return_Correct_List_Of_People()
    {
        // Arrange
        List<Person> people = GetListOfPeople();

        _mockPersonContext.Setup(p => p.People)
            .Returns(people);

        List<Person> expectedResult = new() { people[0], people[2] };

        // Act
        List<Person> result = _personController.GetPeopleWithSingleDigitPostcodeArea();

        // Assert
        result.Should().Equal(expectedResult);
    }

    [Test]
    public void GetPeopleWithFirstPhoneNumberLargerThanSecondPhoneNumber_Should_Return_Correct_List_Of_People()
    {
        // Arrange
        List<Person> people = GetListOfPeople();

        _mockPersonContext.Setup(p => p.People)
            .Returns(people);

        List<Person> expectedResult = new() { people[1] };

        // Act
        List<Person> result = _personController.GetPeopleWithFirstPhoneNumberLargerThanSecondPhoneNumber();

        // Assert
        result.Should().Equal(expectedResult);
    }

    private List<Person> GetListOfPeople()
    {
        return new List<Person>
            {
                new()
                {
                    Id = 1,
                    FirstName = "Aleshia",
                    LastName = "Tomkiewicz",
                    Company = "Alan D Rosenburg Cpa Pc",
                    Address = "14 Taylor St",
                    City = "St. Stephens Ward",
                    County = "Kent",
                    Postal = "CT2 7PP",
                    Phone1 = "01835-703597",
                    Phone2 = "01944-369967",
                    Email = "atomkiewicz@hotmail.com",
                    Web = "http://www.alandrosenburgcpapc.co.uk"
                },
                new()
                {
                    Id = 2,
                    FirstName = "Evan",
                    LastName = "Zigomalas",
                    Company = "Cap Gemini America",
                    Address = "5 Binney St",
                    City = "Abbey Ward",
                    County = "Derbyshire",
                    Postal = "HP11 2AX",
                    Phone1 = "01937-864715",
                    Phone2 = "01714-737668",
                    Email = "evan.zigomalas@gmail.com",
                    Web = "http://www.capgeminiamerica.co.uk"
                },
                new()
                {
                    Id = 3,
                    FirstName = "France",
                    LastName = "Andrade",
                    Company = "Elliott, John W Esq",
                    Address = "846 Moor Place",
                    City = "East Southbourne and Tuckton W",
                    County = "Bournemouth",
                    Postal = "BH6 3BE",
                    Phone1 = "01347-368222",
                    Phone2 = "01935-821636",
                    Email = "france.andrade@hotmail.com",
                    Web = "http://www.elliottjohnwesq.co.uk"
                }
            };
    }
}
