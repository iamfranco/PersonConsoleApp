using PersonApp.AppUI;
using PersonApp.Contexts;
using PersonApp.Controllers;
using PersonApp.CsvParsers;
using PersonApp.StringFormatters;

IPersonContext personContext = new PersonContext();
IPersonCsvParser personCsvParser = new SimplePersonCsvParser();
PersonController personController = new PersonController(personContext, personCsvParser);
IPersonStringFormatter personStringFormatter = new PersonStringFormatter();

AskUser askUser = new AskUser(personController, personStringFormatter);

Console.Clear();
askUser.AskUserForCsvFilePath();