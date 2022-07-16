using PersonApp.Models;

namespace PersonApp.Tests.Models;
internal class PersonTests
{
    Person _person;

    [SetUp]
    public void Setup()
    {
        // Arrange
        _person = new()
        {
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
        };
    }

    [Test]
    public void Clone_Should_Return_Person_With_Same_Values_But_Different_Address()
    {
        // Act
        Person personCloned = _person.Clone();

        // Assert
        personCloned.Should().BeEquivalentTo(_person);
        personCloned.Should().NotBe(_person);
    }
}
