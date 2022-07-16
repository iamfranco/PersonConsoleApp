using PersonApp.AppUI;
using PersonApp.Contexts;
using PersonApp.Controllers;
using PersonApp.CsvParsers;

IPersonContext personContext = new PersonContext();
IPersonCsvParser personCsvParser = new SimplePersonCsvParser();
PersonController personController = new PersonController(personContext, personCsvParser);

AskUser askUser = new AskUser(personController);

Console.Clear();
askUser.AskUserForCsvFilePath();