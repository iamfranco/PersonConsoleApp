using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using PersonApp.Contexts;
using PersonApp.Controllers;
using PersonApp.CsvParsers;

namespace PersonApp.Tests.Controllers;
internal class PersonControllerTests
{
    private Mock<IPersonContext> _mockPersonContext;
    private Mock<IPersonCsvParser> _mockPersonCsvParser;
    private PersonController _personController;

    [SetUp]
    public void Setup()
    {
        _mockPersonContext = new Mock<IPersonContext>();
        _mockPersonCsvParser = new Mock<IPersonCsvParser>();

        _personController = new PersonController(_mockPersonContext.Object, _mockPersonCsvParser.Object);
    }

    [Test]
    public void Constructor_With_Null_PersonContext_Should_Throw_Null_Exception()
    {
        Action act = () => _personController = new PersonController(null, _mockPersonCsvParser.Object);

        act.Should().Throw<ArgumentNullException>()
            .WithMessage("personContext should not be null");
    }

    [Test]
    public void Constructor_With_Null_PersonCsvParser_Should_Throw_Null_Exception()
    {
        Action act = () => _personController = new PersonController(_mockPersonContext.Object, null);

        act.Should().Throw<ArgumentNullException>()
            .WithMessage("personCsvParser should not be null");
    }

}
