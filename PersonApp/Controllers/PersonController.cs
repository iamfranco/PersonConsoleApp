using PersonApp.Contexts;
using PersonApp.CsvParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonApp.Controllers;
public class PersonController
{
    private IPersonContext _personContext;
    private IPersonCsvParser _personCsvParser;

    public PersonController(IPersonContext personContext, IPersonCsvParser personCsvParser)
    {
        if (personContext is null)
            throw new ArgumentNullException(null, "personContext should not be null");

        if (personCsvParser is null)
            throw new ArgumentNullException(null, "personCsvParser should not be null");

        _personContext = personContext;
        _personCsvParser = personCsvParser;
    }
}
