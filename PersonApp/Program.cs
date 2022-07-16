using PersonApp.AppUI;
using PersonApp.Contexts;
using PersonApp.Controllers;
using PersonApp.CsvParsers;
using PersonApp.StringFormatters;

IPersonContext personContext = new PersonContext();
IPersonCsvParser defaultPersonCsvParser = new SimplePersonCsvParser();
IPersonStringFormatter personStringFormatter = new PersonStringFormatter();

Dictionary<string, IPersonCsvParser> csvParserDictionary = new()
{
    {"Default CSV Parser", defaultPersonCsvParser }
};

PersonController personController = new PersonController(personContext, defaultPersonCsvParser);

AskUser askUser = new AskUser(personController, personStringFormatter, csvParserDictionary);

Console.Clear();
askUser.AskUserToLoadCsvFileOrChangeCsvParser();