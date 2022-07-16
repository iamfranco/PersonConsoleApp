using PersonApp.Models;
using PersonApp.StringFormatters;

namespace PersonApp.Tests.StringFormatter;
internal class PersonStringFormatterTests
{
    private IPersonStringFormatter _personStringFormatter;

    [SetUp]
    public void Setup()
    {
        // Arrange
        _personStringFormatter = new PersonStringFormatter();
    }

    [Test]
    public void GetPersonFormattedString_With_Null_As_Person_Input_Should_Throw_Null_Exception()
    {
        // Arrange
        Person person = null;

        // Act
        Action act = () => _personStringFormatter.GetPersonFormattedString(person);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void GetPersonFormattedString_With_Person_Should_Return_Correct_String()
    {
        // Arrange
        Person person = new()
        {
            Id = 1,
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
        };

        string expectedResult = "1 - Ulysses Mcwalters - Mcmahan, Ben L";

        // Act
        string result = _personStringFormatter.GetPersonFormattedString(person);

        // Assert
        result.Should().Be(expectedResult);
    }
}
