using PersonApp.Contexts;
using PersonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonApp.Tests.Contexts;
internal class PersonContextTests
{
    private PersonContextBase _personContext;

    [SetUp]
    public void Setup()
    {
        // Arrange
        _personContext = new PersonContext();
    }

    [Test]
    public void People_Should_Initially_Be_Empty_List_Of_People()
    {
        // Act
        List<Person> result = _personContext.People;

        // Assert
        result.Should().BeOfType(typeof(List<Person>));
        result.Count.Should().Be(0);
    }

    [Test]
    public void AddPeople_With_Null_As_People_Should_Throw_Null_Exception()
    {
        // Arrange
        List<Person> people = null;

        // Act
        Action act = () => _personContext.AddPeople(people);

        // Arrange
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("people should not be null");
    }

    [Test]
    public void AddPeople_With_People_Then_PersonContextPeople_Should_Return_Input_People()
    {
        // Arrange
        List<Person> people = GetListOfPeople();

        // Act
        _personContext.AddPeople(people);

        // Assert
        _personContext.People.Should().Equal(people);
    }

    [Test]
    public void AddPeople_Multiple_Times_Then_PersonContextPeople_Should_Return_All_Added_Input_People()
    {
        // Arrange 
        List<Person> people = GetListOfPeople();
        List<Person> expectedResult = people.Concat(people).ToList();

        // Act
        _personContext.AddPeople(people);
        _personContext.AddPeople(people);

        // Assert
        _personContext.People.Should().Equal(expectedResult);
    }

    [Test]
    public void AddPeople_Then_Modify_Input_People_Should_Not_Modify_PersonContextPeople()
    {
        // Arrange 
        List<Person> people = GetListOfPeople();
        List<Person> expectedResult = people.ToList();

        // Act
        _personContext.AddPeople(people);
        people.Remove(people[0]);

        // Assert
        _personContext.People.Should().NotEqual(people);
        _personContext.People.Should().Equal(expectedResult);
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
